# 🏃‍♂️ LaneDash — 3D Motion-Controlled Fitness Game

> A 3D endless runner that turns exercise into play — control your character using real-life body movements through computer vision.

## 🎮 Overview
**LaneDash** is an interactive 3D endless runner fitness game where players use body movements—such as jumping, crouching, and lane switching—to control their in-game character.  
Using **OpenCV** and **MediaPipe**, the system tracks real-time body motion through a webcam and sends movement data to **Unity** via **UDP communication**.  

The game also includes a calorie tracking system, estimating energy expenditure based on player input (height, weight) and detected activity intensity.  
LaneDash transforms physical exercise into a fun, engaging, and immersive gaming experience.

## 📸 Preview
*(Add your images below)*  
![Main Menu Placeholder](<img width="456" height="256" alt="image" src="https://github.com/user-attachments/assets/b6c609c4-e4de-4ccc-84b8-c07a3f484264" />
)  
![Gameplay Screenshot](<img width="455" height="256" alt="image" src="https://github.com/user-attachments/assets/9b1e1d21-700d-4b9b-924a-846b7f1af7a7" />
)  
![Motion Detection Preview](<img width="454" height="256" alt="image" src="https://github.com/user-attachments/assets/c5cb57fe-1668-4914-96d4-67ae493fb72a" />
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

