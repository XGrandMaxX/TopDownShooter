using System.Collections;
using UnityEngine;

namespace Game.Scripts.Player
{
    public sealed class PlayerInvincible : MonoBehaviour
    {
        [Min(0), SerializeField] private float _invincibleTime;
        
        private PlayerCollision _playerCollision;
        private Coroutine _invincibleCoroutine;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _playerCollision = GetComponent<PlayerCollision>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void Activate() 
            => _invincibleCoroutine ??= StartCoroutine(TemporaryInvincible(_invincibleTime));

        private IEnumerator TemporaryInvincible(float invincibleTime)
        {
            SetInvincible(true);
            yield return new WaitForSeconds(invincibleTime);
            SetInvincible(false);
            
            _invincibleCoroutine = null;
        }

        private void SetInvincible(bool value)
        {
            _playerCollision._invincible = value;

            _spriteRenderer.color = _playerCollision._invincible ? Color.cyan : Color.white;
        }
    }
}
