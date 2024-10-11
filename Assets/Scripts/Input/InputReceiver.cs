using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MotionMaster.Input
{
    public abstract class InputReceiver : MonoBehaviour
    {
        protected InputUser user;

        protected virtual void Awake()
        {

        }

        protected virtual void OnEnable()
        {
            SetUser();
            SetDevicePairing();

            InputManager.Instance.onUserAdded += UserAdded;
            InputManager.Instance.onUserRemoved += UserRemoved;
            InputManager.Instance.onMainUserDeviceChange += MainUserDeviceChange;
        }

        protected void SetDevicePairing()
        {

        }

        protected abstract void SetUser();

        protected abstract void UserAdded(InputUser user);

        protected abstract void UserRemoved(InputUser user);

        protected void MainUserDeviceChange() => SetDevicePairing();
    }
}
