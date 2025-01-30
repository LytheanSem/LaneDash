import cv2

import socket
import mediapipe as mp

# Initialize MediaPipe Pose
mp_pose = mp.solutions.pose
pose = mp_pose.Pose()
mp_drawing = mp.solutions.drawing_utils

# UDP socket setup
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
unity_address = ("127.0.0.1", 65432)  # IP and Port of Unity

# Capture video feed
cap = cv2.VideoCapture(0)

# Define thresholds for jump and bend down detection based on nose y-coordinate
jump_threshold = 0.3  # Adjust for your environment
bend_threshold = 0.7  # Adjust for your environment

# Scaling factor for distance in meters (calibrated experimentally)
scaling_factor = 0.5  # Example value; replace with your calibrated factor

while cap.isOpened():
    ret, frame = cap.read()
    if not ret:
        break

    # Flip the frame horizontally (mirror effect)
    frame = cv2.flip(frame, 1)

    # Convert the frame to RGB
    rgb_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

    # Perform pose detection
    result = pose.process(rgb_frame)

    # Get frame dimensions
    frame_height, frame_width = frame.shape[:2]
    center_x = frame_width // 2
    middle_margin = 0.18 * frame_width  # Increase margin for wider middle detection

    # Draw grid
    # Vertical lines for left, center, and right detection
    cv2.line(frame, (int(center_x - middle_margin), 0), (int(center_x - middle_margin), frame_height), (255, 0, 0), 2)
    cv2.line(frame, (int(center_x + middle_margin), 0), (int(center_x + middle_margin), frame_height), (255, 0, 0), 2)
    
    # Horizontal lines for jump and bend down thresholds
    jump_y = int(jump_threshold * frame_height)
    bend_y = int(bend_threshold * frame_height)
    cv2.line(frame, (0, jump_y), (frame_width, jump_y), (0, 255, 0), 2)
    cv2.line(frame, (0, bend_y), (frame_width, bend_y), (0, 255, 0), 2)

    # Check if landmarks are detected
    if result.pose_landmarks:
        # Get the landmarks
        landmarks = result.pose_landmarks.landmark
        nose = landmarks[mp_pose.PoseLandmark.NOSE]

        # Calculate x positions for left, middle, right detection
        if nose.x * frame_width < center_x - middle_margin:
            position = "Left"
        elif nose.x * frame_width > center_x + middle_margin:
            position = "Right"
        else:
            position = "Center"

        # Jump and bend down detection based on nose y-coordinate
        action = ""
        if nose.y < jump_threshold:
            action = "Jump"
        elif nose.y > bend_threshold:
            action = "Crouch"

        # Calculate distance in meters
        distance = abs(nose.z) * scaling_factor  # Convert z-value to meters
        distance_message = f"Distance: {distance:.2f} m"

        # Send position, action, and distance to Unity via UDP
        message = f"{position},{action},{distance:.2f}"
        sock.sendto(message.encode(), unity_address)

        # Display result on the screen
        cv2.putText(frame, position, (50, 50), cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 255, 0), 2)
        cv2.putText(frame, action, (50, 100), cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 255, 0), 2)
        cv2.putText(frame, distance_message, (50, 150), cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 255, 0), 2)

        # Draw the landmarks
        mp_drawing.draw_landmarks(frame, result.pose_landmarks, mp_pose.POSE_CONNECTIONS)

    # Display the frame
    cv2.imshow('Movement Detection', frame)

    if cv2.waitKey(10) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()
