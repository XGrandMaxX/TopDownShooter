using Game.Scripts.Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using Zenject;

namespace Game.Scripts.Player.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private OnScreenStick _screenStick; 
        [SerializeField] private protected Transform _firePoint;
        [Min(0), SerializeField] protected internal float _attackRate;

        private Vector3 _mousePosition;
        private InputActions _inputActions;

        private protected bool _attacking;
        private float _nextAttackTime;

        [Inject]
        private void Construct(InputController inputController)
        {
            _inputActions = inputController.InputActions;
            _inputActions.Player.Shoot.performed += Shoot;
            _inputActions.Player.Shoot.canceled += CancelShoot;
        }

        private void OnDisable()
        {
            _inputActions.Player.Shoot.performed -= Shoot;
            _inputActions.Player.Shoot.canceled -= CancelShoot;
        }


        protected virtual void Shoot(InputAction.CallbackContext obj = default)
        {
            if(Time.time < _nextAttackTime)
                return;

            _attacking = true;
            
            _nextAttackTime = Time.time + 1.0f / _attackRate;
            Debug.Log("SHOOT");
        }

        //WIP
        protected virtual void RotateRifle()
        {
            Vector2 direction = _inputActions.Player.Move.ReadValue<Vector2>().normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = targetRotation;
        }

        private void CancelShoot(InputAction.CallbackContext obj) => _attacking = false;
    }
}
