# Motion Master
This repository contains a robust input system for a fighting game developed using Unity. It focuses on three core areas: user and device management, reading and receiving input, and processing input commands and actions. The system is designed to support multiple players and input devices, ensure responsive gameplay, and provide flexibility for defining complex input commands.

The overall goal of the system is to provide a real example of how one could go about developing a fighting game input system within the Unity game engine and utilizing the input system package. There are not many fighting game development resources out there so I hope this helps any aspiring developers. Lastly, any of the features and components explained are completely changeable and extendable so feel free to implement them in your own way.
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
- **Input Manager:** A singleton that handles most of the user and device management functionalities. It also has 3 events that objects can subscribe to when the main user's device changes, when a new user gets added, and when a user gets removed.
## Reading & Receiving Input
This section of the system handles reading input from connected devices and transforming it into usable data within Unity. The input reading is designed to be responsive and supports both analog and digital input types.
### Features
- **Input Reading:** Reads inputs from gamepads, keyboards, and other supported devices using Unity’s Input System package.
- **Analog & Digital Support:** Processes both analog inputs (e.g., joystick positions) and digital inputs (e.g., button presses).
- **Input Polling:** Continuously polls for active inputs, ensuring that the latest input state is always available for processing.
### Components
- **Input Reader:** This class reads inputs from devices paired to the input action asset using callbacks. Additionally, it has events that signify when inputs are pressed or released (canceled) and provides functionality to change the action map for the input action asset.
- **Input Receiver:** This is an abstract class that is designed to be inherited by any game object that receives input. Thus, it provides common functionality for each receiver object which includes being able to pair (or set) an input user for the object and dynamic responses to events invoked in the Input Manager class.
## Input Commands & Actions
The Input Commands & Actions component handles interpreting raw inputs into commands for character actions, such as moves, attacks, and combos. It allows developers to define custom command sequences and map them to specific actions within the game.
### Features
- **Command Mapping:** Allows developers to define sequences of inputs (e.g., quarter-circle forward + punch) and map them to specific character actions.
- **Combo System:** Supports complex combo strings and special moves, using an input buffer to recognize sequences with precise timing.
- **Customization:** Define a multitude of different priorities and conditions for how a command is checked such as altering the check (or timing) window, changing the command's priority, and more.
### Components
- **Input Data:** Represents the player's input on a single frame as well as other important aspects such as how long they held the input, whether the input was used for a command, and whether it's in an executable state.
- **Input Buffer:** A custom data structure that holds the player's inputs for a predefined number of frames and is what's used to check for complex motion commands.
- **Input Command:** A scriptable object that allows for customization of in-game commands such as the sequence of inputs needed to execute the command, the timing window allowed to execute the command, and the priority of the command.
## Bit Flags
Using bit flags to represent in-game inputs is something I recommend anyone should do since it makes things such as multiple button presses easier. For example, in Street Fighter 6 parry is performed by pressing MP + MK. With bit flags, you can easily represent that command with bitwise operators like this *MP_and_MK = MP | MK*.
## Additional Implementations
This is simply a list of features that one could look into to enhance the system further. I will also try to implement them into this project when I have the chance but what's there now can serve as a base for a "simple" version of a fighting game input system.
### Features
- **Player Profiles**
- **Input Rebinding**
- **Negative Edge**
- **Input Recording**
