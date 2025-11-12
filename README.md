# 🏃‍♂️ LaneDash — 3D Motion-Controlled Fitness Game

> A 3D endless runner that turns exercise into play — control your character using real-life body movements through computer vision.

## 🎮 Overview
**LaneDash** is an interactive 3D endless runner fitness game where players use body movements—such as jumping, crouching, and lane switching—to control their in-game character.  
Using **OpenCV** and **MediaPipe**, the system tracks real-time body motion through a webcam and sends movement data to **Unity** via **UDP communication**.  

The game also includes a calorie tracking system, estimating energy expenditure based on player input (height, weight) and detected activity intensity.  
LaneDash transforms physical exercise into a fun, engaging, and immersive gaming experience.

## 📸 Preview 
![Main Menu Placeholder](![Main_Menu](https://github.com/user-attachments/assets/ce29b4ff-3e8b-4834-951b-050e7c4b10be)
)  
![Gameplay Screenshot](<img width="2090" height="1177" alt="gameplay1" src="https://github.com/user-attachments/assets/8acaf269-49ef-4c39-a8d7-f5b20a6a3605" />
)  
![Motion Detection Preview](<img width="2090" height="1182" alt="gameplay2" src="https://github.com/user-attachments/assets/9cc08b6b-1739-4c7e-98f2-80949ff39f61" />
)  
 
## ⚙️ Features
- 🎥 Real-time body motion control via webcam (no external sensors required)  
- 🕹️ 3D Endless Runner gameplay with increasing difficulty  
- 🔢 Calorie tracking system based on movement and user data  
- 💾 Leaderboard with top 3 player rankings  
- 💡 Engaging UI/UX and responsive feedback  
- 🧠 Accessible and affordable — no VR headset or special hardware needed  

## 🧩 System Architecture
LaneDash consists of two main components:

1. **Motion Tracking Module (Python)**  
   - Uses OpenCV + MediaPipe to detect body movement  
   - Sends movement data via UDP sockets to Unity  

2. **Game Engine (Unity)**  
   - Receives motion input from Python  
   - Controls the 3D character and environment  
   - Displays real-time statistics (calories, coins, score)  

*(Insert system architecture diagram here)*  
![System Architecture Placeholder]()  

## 🧠 Technologies Used
| Component | Technology |
|------------|-------------|
| Game Engine | Unity |
| Motion Tracking | OpenCV, MediaPipe (Python) |
| Communication | UDP Socket |
| Game Assets | Mixamo |
| Data Handling | C#, Python |

## 📋 How It Works
1. **Start the Game**  
   Enter your name, height, and weight to personalize calorie tracking.  
2. **Move to Control**  
   - Jump → Character jumps  
   - Crouch → Character slides  
   - Lean left/right → Switch lanes  
3. **Collect Coins & Avoid Obstacles**  
   Stay in motion, collect rewards, and burn calories while playing.  
4. **Track Your Progress**  
   - Real-time display of calories burned and coins collected  
   - View leaderboard after each session  

## 🧮 Calorie Estimation Formula
LaneDash uses the **Metabolic Equivalent of Task (MET)** model:


| Movement | MET Value |
|-----------|------------|
| Jumping | 10 |
| Crouching | 7 |
| Lane Switching | 5 |

## 📊 User Evaluation Highlights
- ✅ 97% of testers said they would play again  
- ⚙️ 76% found game speed “just right”  
- 🕵️ 63% experienced smooth motion detection  
- 💬 Common feedback:  
  - Improve tracking accuracy  
  - Add more levels and obstacles  
  - Optional difficulty/speed settings  

*(Insert user feedback chart image here)*  
![User Survey Results Placeholder]()  

## 🚀 Future Improvements
- 🤖 AI-enhanced motion tracking for higher accuracy  
- 🌐 Multiplayer mode for shared fitness sessions  
- 📱 Mobile version support  
- 🧩 New levels, obstacles, and gameplay modes  
- 🩺 Integration with health metrics (heart rate, etc.)

## 👨‍💻 Developers
| Name | Student ID |
|------|-------------|
| **Paveena Chuayaem** | 6430103 |
| **Vibol Rothmony Seng** | 6430170 |
| **Lythean Sem** | 6511925 |

**Advisor:** Asst. Prof. Dr. Darun Kesrarat  
**Institution:** Vincent Mary School of Science and Technology, Assumption University  

## 📚 References
- [OpenCV](https://opencv.org/)  
- [MediaPipe](https://mediapipe.dev/)  
- [Mixamo (Character & Animations)](https://www.mixamo.com/)  
- [Unity OpenCV Integration Asset](https://assetstore.unity.com/packages/tools/integration/opencv-plus-unity-85928)  
- [Subway Surfers (Gameplay Inspiration)](https://www.subwaysurfers.com/)  
- [Ring Fit Adventure (Nintendo)](https://www.nintendo.com/)  

