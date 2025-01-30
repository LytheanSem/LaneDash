import socket
import cv2
import mediapipe as mp

# Initialize MediaPipe Pose
mp_pose = mp.solutions.pose
pose = mp_pose.Pose()
mp_drawing = mp.solutions.drawing_utils

# UDP socket setup
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
unity_address = ("127.0.0.1", 65432)  # Unity IP and Port

# Capture video feed
cap = cv2.VideoCapture(0)

# Define thresholds for jump and bend down detection based on nose y-coordinate
jump_threshold = 0.3  # Adjust as needed
bend_threshold = 0.7  # Adjust as needed

def draw_threshold_grid(frame, jump_thresh, bend_thresh):
    height, width, _ = frame.shape
    jump_y = int(jump_thresh * height)
    bend_y = int(bend_thresh * height)
    center_x = width // 2
    middle_margin = int(0.15 * width)

    # Draw horizontal lines for jump and bend thresholds
    cv2.line(frame, (0, jump_y), (width, jump_y), (0, 255, 0), 2)  # Green for jump
    cv2.line(frame, (0, bend_y), (width, bend_y), (0, 0, 255), 2)  # Red for crouch

    # Draw vertical lines for left, center, and right detection
    cv2.line(frame, (center_x - middle_margin, 0), (center_x - middle_margin, height), (255, 0, 0), 2)  # Left boundary
    cv2.line(frame, (center_x + middle_margin, 0), (center_x + middle_margin, height), (255, 0, 0), 2)  # Right boundary

while cap.isOpened():
    ret, frame = cap.read()
    if not ret:
        break

    # Flip the frame horizontally (mirror effect)
    frame = cv2.flip(frame, 1)

    # Draw threshold grid
    draw_threshold_grid(frame, jump_threshold, bend_threshold)
    
    # Convert the frame to RGB
    rgb_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

    # Perform pose detection
    result = pose.process(rgb_frame)

    # Check if landmarks are detected
    if result.pose_landmarks:
        # Get landmarks
        landmarks = result.pose_landmarks.landmark
        nose = landmarks[mp_pose.PoseLandmark.NOSE]

        # Calculate x positions for left, middle, right detection
        frame_width = frame.shape[1]
        center_x = frame_width // 2
        middle_margin = 0.15 * frame_width  # Adjust margin as needed

        # Determine player position
        if nose.x * frame_width < center_x - middle_margin:
            position = "Left"
        elif nose.x * frame_width > center_x + middle_margin:
            position = "Right"
        else:
            position = "Center"

        # Determine jump or bend down action
        action = ""
        if nose.y < jump_threshold:
            action = "Jump"
        elif nose.y > bend_threshold:
            action = "Bend Down"

        # Send position and action to Unity via UDP
        message = f"{position},{action}"
        sock.sendto(message.encode(), unity_address)

        # Display results
        cv2.putText(frame, position, (50, 50), cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 255, 0), 2)
        cv2.putText(frame, action, (50, 100), cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 255, 0), 2)
        mp_drawing.draw_landmarks(frame, result.pose_landmarks, mp_pose.POSE_CONNECTIONS)

    # Show frame
    cv2.imshow('Movement Detection', frame)

    if cv2.waitKey(10) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()