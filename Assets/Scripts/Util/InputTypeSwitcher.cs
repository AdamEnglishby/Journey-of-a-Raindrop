using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Morrigan.Scripts.ControllerUtil
{
    public class InputTypeSwitcher : MonoBehaviour
    {
        private static InputTypeSwitcher _instance;
        private InputDevice _lastInputDevice;
        
        public static event Action<InputDevice> OnActiveControllerTypeChange;
        
        [RuntimeInitializeOnLoadMethod]
        private static void Reload()
        {
            InputSystem.onActionChange -= _ActionChange;
            OnActiveControllerTypeChange -= _ActiveControllerTypeChange;
        }
        
        private void Start()
        {
            _instance = this;
            
            InputSystem.onActionChange += _ActionChange;
            OnActiveControllerTypeChange += _ActiveControllerTypeChange;
        }

        private static void _ActionChange(object obj, InputActionChange change)
        {
            if (change != InputActionChange.ActionPerformed) return;
            var inputAction = (InputAction) obj;
            var lastControl = inputAction.activeControl;
            var lastDevice = lastControl.device;

            if (_instance._lastInputDevice != lastDevice)
            {
                OnActiveControllerTypeChange?.Invoke(lastDevice);
            }
        }

        private static void _ActiveControllerTypeChange(InputDevice newDevice)
        {
            _instance._lastInputDevice = newDevice;
            Debug.Log("Switched to " + newDevice.name);
        }

    }
}