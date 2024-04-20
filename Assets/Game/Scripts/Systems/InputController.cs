using UnityEngine;

namespace Game.Scripts.Systems
{
    public sealed class InputController
    {
        private readonly InputActions _inputActions;
        public InputActions InputActions => _inputActions;

        public InputController()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
        }

        ~InputController() => _inputActions.Disable();
    }
}
