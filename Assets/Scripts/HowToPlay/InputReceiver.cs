using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace MainMenu {
    public class InputReceiver : MonoBehaviour {
        public static Action onKeyPressed;
        [SerializeField] PlayerInput input;
        [SerializeField] inputEvent[] events;

        int pressesCount = 0;
        int maxPressesCount = 0;

        private void Awake() {
            input.actions["Click"].canceled += OnButtonPressed;
            input.actions["RightClick"].canceled += OnGoBackButtonPressed;

            for(int i = 0; i < events.Length; i++) {
                maxPressesCount = Math.Max(maxPressesCount, events[i].requiredClicks);
            }
        }

        private void OnDestroy() {
            if(input == null) return;

            input.actions["Click"].canceled -= OnButtonPressed;
            input.actions["RightClick"].canceled -= OnGoBackButtonPressed;
        }

        void OnButtonPressed(InputAction.CallbackContext context) {
            if(!context.canceled) return;

            UpdatePressesCount(1);
            MoveCamera.nextPosition?.Invoke();
        }

        void OnGoBackButtonPressed(InputAction.CallbackContext context) {
            if(!context.canceled) return;

            UpdatePressesCount(-1);
            MoveCamera.prevPosition?.Invoke();
        }

        void UpdatePressesCount(int delta) {
            pressesCount = Math.Clamp(pressesCount + delta, 0, maxPressesCount);

            for(int i = 0; i < events.Length; i++) {
                if(events[i].requiredClicks <= pressesCount && !events[i].triggered) {
                    events[i].onTrigger?.Invoke();
                    events[i].triggered = true;

                } else if(events[i].requiredClicks > pressesCount && events[i].triggered && events[i].reversable) {
                    events[i].onUnTrigger?.Invoke();
                    events[i].triggered = false;
                }
            }
        }

        [System.Serializable]
        struct inputEvent {
            public int requiredClicks;
            public bool triggered;
            public bool reversable;

            public UnityEvent onTrigger;

            [Space(2)]
            [Header("This should be the undo of the onTrigger events")]
            public UnityEvent onUnTrigger;
        }
    }

}