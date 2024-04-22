using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Enemy : EnemyData
    {
        protected internal override void Initialize(byte health)
        {
            Health = health;

            _audioSource = GetComponent<AudioSource>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _chaseTarget ??= FindObjectOfType<PlayerMove>().transform;
        }

        private void Update()
        {
            if(_chaseTarget != null)
                ChaseThePlayer();
        }
        
        protected internal override void ChaseThePlayer()
        {
            _moveDirection = (_chaseTarget.position - transform.position).normalized;
            
            AnimateMove();
            
            transform.position = Vector2.MoveTowards(
                transform.position, 
                _chaseTarget.position, 
                MoveSpeed * Time.deltaTime);
        }
        
        private void AnimateMove()
        {
            _animator.SetFloat("MoveX", _moveDirection.x);
            _animator.SetFloat("MoveY", _moveDirection.y);
        }

        protected internal override void TakeDamage(byte amount)
        {
            Health -= amount;
            
            if (Health <= 0)
                Die();
        }
    }
}
