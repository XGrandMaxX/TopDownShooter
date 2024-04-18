using Game.Scripts.Objects.Projectiles;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Scripts.Player.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public const byte PROJECTILE_PRELOAD_COUNT = 20;

        #region attributes
        
        [SerializeField] private protected Transform _firePoint;
        [Min(0), SerializeField] protected internal float _attackRate;
        
        private protected bool _attacking;
        private float _nextAttackTime;
        
        private InputActions _inputActions;
        private ProjectileData _projectileData;

        #endregion

        #region ProjectilePool

        private ObjectPool<PlayerProjectile> _projectilePool;

        #endregion


        [Inject]
        private void Construct(InputController inputController, ProjectileData projectileData)
        {
            _inputActions = inputController.InputActions;
            _inputActions.Player.Shoot.performed += Shoot;
            _inputActions.Player.Shoot.canceled += CancelShoot;

            _projectileData = projectileData;

            _projectilePool = new ObjectPool<PlayerProjectile>(
                Preload,
                GetAction,
                ReturnAction,
                PROJECTILE_PRELOAD_COUNT);
            
            Debug.Log($"weapon - {name} successfully initialize!");
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
        }
        
        protected virtual void RotateWeapon()
        {
            Vector2 direction = _inputActions.Player.Move.ReadValue<Vector2>().normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = targetRotation;
        }

        private void CancelShoot(InputAction.CallbackContext obj) => _attacking = false;

        private PlayerProjectile Preload()
            => Instantiate(_projectileData.ProjectilePrefab, transform, true);

        private void ReturnAction(PlayerProjectile playerProjectile)
            => playerProjectile.gameObject.SetActive(false);

        private void GetAction(PlayerProjectile playerProjectile)
        {
            playerProjectile.transform.SetPositionAndRotation(
                _firePoint.position,
                transform.rotation);
            
            playerProjectile.gameObject.SetActive(true);
        }
    }
}
