using MotionMaster.Utils;
using System;
using System.Linq;
using UnityEngine.InputSystem;

namespace MotionMaster.Input
{
    /// <summary>
    /// handles device and user management by making sure both are added, removed, and paired correctly.
    /// </summary>
    public class InputManager : Singleton<InputManager>
    {
        /// <summary>
        /// the max amount of local users that can be on the system
        /// </summary>
        private const int MAX_USER_AMOUNT = 3;

        /// <summary>
        /// event that is invoked when the main user's device undergoes a change
        /// </summary>
        public event Action onMainUserDeviceChange;
        /// <summary>
        /// event that is invoked when a local user gets added to the system
        /// </summary>
        public event Action<InputUser> onUserAdded;
        /// <summary>
        /// event that is invoked when a local user gets removed from the system
        /// </summary>
        public event Action<InputUser> onUserRemoved;

        /// <summary>
        /// the main user (or super user) for the current session
        /// </summary>
        private InputUser mainUser;
        /// <summary>
        /// a list of local users in the system
        /// </summary>
        private FixedList<InputUser> users;
        /// <summary>
        /// a list of the allowed devices currently connected to the system
        /// </summary>
        private FixedList<InputDevice> devices;

        /// <summary>
        /// an array of names that contain the allowed devices for the system
        /// </summary>
        private string[] allowedDeviceNames =
        {
            "Gamepad",
        };

        public InputUser MainUser { get { return mainUser; } }
        public FixedList<InputUser> Users { get { return users; } }
        public FixedList<InputDevice> Devices { get { return devices; } }

        private void Awake()
        {
            mainUser = new InputUser(1);
            users = new FixedList<InputUser>(MAX_USER_AMOUNT);
            devices = new FixedList<InputDevice>(MAX_USER_AMOUNT + 1);
            Initialize();
        }

        private void OnEnable()
        {
            InputSystem.onDeviceChange +=
                (device, change) =>
                {
                    switch (change)
                    {
                        case InputDeviceChange.Added:
                            DeviceAdded(device); 
                            break;
                        case InputDeviceChange.Removed:
                            DeviceRemoved(device);
                            break;
                    }
                };
        }

        /// <summary>
        /// method that runs when the game is initialized.
        /// It finds all the connected devices and pairs them to users adequately
        /// </summary>
        private void Initialize()
        {
            foreach(var device in InputSystem.devices)
            {
                if (IsAllowedDevice(device))
                {
                    devices.Add(device);
                }
            }

            if(devices.Count > 0)
            {
                mainUser.SetDevice(devices.ElementAt(0));
                onMainUserDeviceChange?.Invoke();

                if(devices.Count > 1)
                {
                    for(int i = 1; i < devices.Count; i++)
                    {
                        CreateUser(devices.ElementAt(i));
                    }
                }
            }
        }

        /// <summary>
        /// method called when a device gets connected to the system
        /// </summary>
        /// <param name="device">the device that was connected</param>
        private void DeviceAdded(InputDevice device)
        {
            if (!IsAllowedDevice(device) || devices.Full)
            {
                return;
            }

            if (mainUser.IsReady())
            {
                mainUser.SetDevice(device);
                onMainUserDeviceChange?.Invoke();
            }
            else
            {
                CreateUser(device);
            }

            devices.Add(device);
        }

        /// <summary>
        /// method called when a device is removed from the system
        /// </summary>
        /// <param name="device">the device that was removed</param>
        private void DeviceRemoved(InputDevice device)
        {
            if (!IsUserDevice(device))
            {
                if(mainUser.Device == device)
                {
                    mainUser.ResetDevice();
                    onMainUserDeviceChange?.Invoke();
                }
            }

            devices.Remove(device);
        }

        /// <summary>
        /// creates a local user
        /// </summary>
        /// <param name="device">the device that will be assigned to the user</param>
        private void CreateUser(InputDevice device)
        {
            InputUser newUser = new InputUser(users.Count + 2, device);
            users.Add(newUser);
            onUserAdded?.Invoke(newUser);
        }

        /// <summary>
        /// determines if a device belongs to a local user
        /// </summary>
        /// <param name="device">the device we are checking for</param>
        /// <returns></returns>
        private bool IsUserDevice(InputDevice device)
        {
            foreach(var user in users)
            {
                if(user.Device == device)
                {
                    user.ResetDevice();
                    users.Remove(user);
                    onUserRemoved?.Invoke(user);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// determines if the device is an allowed device
        /// </summary>
        /// <param name="device">the device we are checking for</param>
        /// <returns></returns>
        private bool IsAllowedDevice(InputDevice device)
        {
            foreach (var name in allowedDeviceNames)
            {
                if (device.name.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }

        private void OnGUI()
        {
            
        }
    }
}
