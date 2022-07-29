using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit
{
    public class InputManager : SingletonBase<InputManager>
    {
        public InputManager()
        {
            Enable();
            EnableWorldInput();
            EnableUiInput();
            MonoManager.instance.AddUpdateListener(OnUpdate);
        }

        private void OnUpdate()
        {
            if (!IsActive)
                return;

            CheckAxis();
            CheckAxisRaw();
        }

        #region  PROPERTIES
        public float Horizontal
        {
            get
            {
                if (IsActive)
                    return Input.GetAxisRaw("Horizontal");
                else
                    return 0;
            }
        }

        public float Vertical
        {
            get
            {
                if (IsActive)
                    return Input.GetAxisRaw("Vertical");
                else
                    return 0;
            }
        }

        public bool UseWorldInput
        {
            get;
            private set;
        }
        public bool UseUiInput
        {
            get;
            private set;
        }

        #endregion

        #region PUBLIC UNIVERSAL
        public void EnableWorldInput() => UseWorldInput = true;
        public void DisableWorldInput() => UseWorldInput = false;
        public void EnableUiInput() => UseUiInput = true;
        public void DisableUiInput() => UseUiInput = false;
        #endregion

        #region PUBLIC KEY
        public bool GetKey(KeyCode key)
        {
            if (!IsActive)
                return false;
            return Input.GetKey(key);
        }

        public bool GetKeyDown(KeyCode key)
        {
            if (!IsActive)
                return false;
            return Input.GetKeyDown(key);
        }

        public bool GetKeyUp(KeyCode key)
        {
            if (!IsActive)
                return false;
            return Input.GetKeyUp(key);
        }

        public bool GetWorldKey(KeyCode key)
        {
            if (!IsActive || !UseWorldInput)
                return false;
            return Input.GetKey(key);
        }

        public bool GetWorldKeyDown(KeyCode key)
        {
            if (!IsActive || !UseWorldInput)
                return false;
            return Input.GetKeyDown(key);
        }

        public bool GetWorldKeyUp(KeyCode key)
        {
            if (!IsActive || !UseWorldInput)
                return false;
            return Input.GetKeyUp(key);
        }

        public bool GetUiKey(KeyCode key)
        {
            if (!IsActive || !UseUiInput)
                return false;
            return Input.GetKey(key);
        }

        public bool GetUiKeyDown(KeyCode key)
        {
            if (!IsActive || !UseUiInput)
                return false;
            return Input.GetKeyDown(key);
        }

        public bool GetUiKeyUp(KeyCode key)
        {
            if (!IsActive || !UseUiInput)
                return false;
            return Input.GetKeyUp(key);
        }
        #endregion

        #region PUBLIC MOUSE
        public bool GetMouseButton(int button)
        {
            if (!IsActive)
                return false;
            return Input.GetMouseButton(button);
        }

        public bool GetMouseButtonDown(int button)
        {
            if (!IsActive)
                return false;
            return Input.GetMouseButtonDown(button);
        }

        public bool GetMouseButtonUp(int button)
        {
            if (!IsActive)
                return false;
            return Input.GetMouseButtonUp(button);
        }

        public bool GetWorldMouseButton(int button)
        {
            if (!IsActive || !UseWorldInput)
                return false;
            return Input.GetMouseButton(button);
        }

        public bool GetWorldMouseButtonDown(int button)
        {
            if (!IsActive || !UseWorldInput)
                return false;
            return Input.GetMouseButtonDown(button);
        }

        public bool GetWorldMouseButtonUp(int button)
        {
            if (!IsActive || !UseWorldInput)
                return false;
            return Input.GetMouseButtonUp(button);
        }

        public bool GetUiMouseButton(int button)
        {
            if (!IsActive || !UseUiInput)
                return false;
            return Input.GetMouseButton(button);
        }

        public bool GetUiMouseButtonDown(int button)
        {
            if (!IsActive || !UseUiInput)
                return false;
            return Input.GetMouseButtonDown(button);
        }

        public bool GetUiMouseButtonUp(int button)
        {
            if (!IsActive || !UseUiInput)
                return false;
            return Input.GetMouseButtonUp(button);
        }
        #endregion

        #region PRIVATE
        private void CheckKeyCode(KeyCode key, String target)
        {
            if (Input.GetKeyDown(key))
            {
                EventManager.instance.EventTrigger("KeyDown", key);
            }
            if (Input.GetKeyUp(key))
            {
                EventManager.instance.EventTrigger("KeyUp", key);
            }
        }

        private void CheckKeyCode(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                EventManager.instance.EventTrigger("KeyDown", key);
            }
            if (Input.GetKeyUp(key))
            {
                EventManager.instance.EventTrigger("KeyUp", key);
            }
        }

        private void CheckButton(String name, String target)
        {
            if (Input.GetButtonDown(name))
                EventManager.instance.EventTrigger(target + "ButtonDown", name);

            if (Input.GetButtonUp(name))
                EventManager.instance.EventTrigger(target + "ButtonUp", name);
        }

        private void CheckButton(String name)
        {
            if (Input.GetButtonDown(name))
                EventManager.instance.EventTrigger("ButtonDown", name);

            if (Input.GetButtonUp(name))
                EventManager.instance.EventTrigger("ButtonUp", name);
        }

        private void CheckAxis()
        {
            float Horizontal = Input.GetAxis("Horizontal");
            float Vertical = Input.GetAxis("Vertical");
            (float, float) Axis = (Horizontal, Vertical);
            EventManager.instance.EventTrigger<(float, float)>("Axis", Axis);
        }

        private void CheckAxis(String target)
        {
            float Horizontal = Input.GetAxis("Horizontal");
            float Vertical = Input.GetAxis("Vertical");
            (float, float) Axis = (Horizontal, Vertical);
            EventManager.instance.EventTrigger<(float, float)>(target + "Axis", Axis);
        }

        private void CheckAxisRaw()
        {
            float Horizontal = Input.GetAxisRaw("Horizontal");
            float Vertical = Input.GetAxisRaw("Vertical");
            (float, float) AxisRaw = (Horizontal, Vertical);
            EventManager.instance.EventTrigger<(float, float)>("AxisRaw", AxisRaw);
        }

        private void CheckAxisRaw(String target)
        {
            float Horizontal = Input.GetAxisRaw("Horizontal");
            float Vertical = Input.GetAxisRaw("Vertical");
            (float, float) AxisRaw = (Horizontal, Vertical);
            EventManager.instance.EventTrigger<(float, float)>(target + "AxisRaw", AxisRaw);
        }
        #endregion
    }
}
