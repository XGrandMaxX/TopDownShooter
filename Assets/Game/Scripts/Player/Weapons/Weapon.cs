using Game.Scripts.Objects.Projectiles;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.Systems;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Player.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        private const byte PROJECTILE_PRELOAD_COUNT = 20;

        #region attributes
        
        [SerializeField] private Transform _parentObject;
        [SerializeField] private protected Transform _firePoint;
        [Min(0), SerializeField] protected internal float _attackRate;
        
        private protected bool _attacking;
        private float _nextAttackTime;
        
        private InputActions _inputActions;
        private ProjectileData _projectileData;
        private ProjectileListener _projectileListener;
        private SpriteRenderer _spriteRenderer;

        private Vector2 _lastRotatePosition;

        #endregion

        #region ProjectilePool

        internal ObjectPool<PlayerProjectile> _projectilePool { get; private set;  }

        #endregion


        [Inject]
        private void Construct(
            InputController inputController,
            ProjectileData projectileData,
            ProjectileListener projectileListener)
        {
            _inputActions = inputController.InputActions;
            _inputActions.Player.Shoot.performed += context => Shoot();
            _inputActions.Player.Shoot.canceled += context => ShootCanceled();

            _projectileData = projectileData;

            _projectileListener = projectileListener;

            _projectilePool = new ObjectPool<PlayerProjectile>(
                Preload,
                GetAction,
                ReturnAction,
                PROJECTILE_PRELOAD_COUNT);

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        protected virtual void Shoot()
        {
            if(Time.time < _nextAttackTime)
                return;

            _attacking = true;
            
            _nextAttackTime = Time.time + 1.0f / _attackRate;

            PlayerProjectile projectile = _projectilePool.Get();
            projectile.Launch(_firePoint.rotation);
            
            _projectileListener.Subscribe(projectile);
        }
        
        protected virtual void RotateWeapon()
        {
            Vector2 direction = _inputActions.Player.Move.ReadValue<Vector2>().normalized;

            if (direction.magnitude > 0)
                _lastRotatePosition = direction;

            _spriteRenderer.flipY = direction.x switch
            {
                < 0 => true,
                > 0 => false,
                _ => _spriteRenderer.flipY,
            };

            float angle = Mathf.Atan2(_lastRotatePosition.y, _lastRotatePosition.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = targetRotation;
        }
        
        private void ShootCanceled() => _attacking = false;
        
        #region ProjectilePoolMethods
        
        private PlayerProjectile Preload() 
            => Instantiate(
                _projectileData.ProjectilePrefab,
                _parentObject, 
                true);

        private void ReturnAction(PlayerProjectile playerProjectile) 
            => playerProjectile.gameObject.SetActive(false);

        private void GetAction(PlayerProjectile playerProjectile)
        {
            playerProjectile.transform.SetPositionAndRotation(
                _firePoint.position,
                transform.rotation);
            
            playerProjectile.gameObject.SetActive(true);
        }
        
        #endregion
    }
}
