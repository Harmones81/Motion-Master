# Motion Master
This repository contains a robust input system for a fighting game developed using Unity. It focuses on three core areas: user and device management, reading and receiving input, and processing input commands and actions. The system is designed to support multiple players and input devices, ensure responsive gameplay, and provide flexibility for defining complex input commands.
## Table of Contents
1. User & Device Management
2. Reading & Receiving Input
3. Input Commands & Actions
## User & Device Management
This part of the system ensures the smooth handling of players and their respective input devices. It manages multiple controllers, allowing for easy addition and removal of players during gameplay.
### Features
- **Main User:** The main user functions as the core user of the application and; thus, gets various privileges: access to the keyboard, always getting the first connected device, and never being removed from the system.
- **Dynamic Player Registration:** Automatically recognizes when a new controller or keyboard is connected and assigns it to a player.
- **Device Mapping:** Maps different devices (e.g., gamepads, keyboards) to user profiles, enabling easy swapping or reconnecting without data loss.
- **Player Disconnect/Reconnect:** Handles cases where a player’s device is disconnected and allows different reconnection behaviors depending on the input object.
### Components
- **Input User:** A class that represents a user in the system.
- **Input Manager:** The
