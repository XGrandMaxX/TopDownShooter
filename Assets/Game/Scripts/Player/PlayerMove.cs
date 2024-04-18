using Game.Scripts.Systems;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Player
{
    [RequireComponent(
        typeof(Rigidbody2D),
        typeof(Animator))]
    public sealed class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private InputActions _inputActions;
        
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;

        private Vector2 _lastMoveDirection;
        private Vector2 _moveDirection;

        [Inject]
        private void Construct(InputController inputController)
        {
            _inputActions = inputController.InputActions;

            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update() => GetInput();
        private void FixedUpdate() => Move();

        private void GetInput() 
            => _moveDirection = _inputActions.Player.Move.ReadValue<Vector2>().normalized;

        private void Move()
        {
            _rigidbody2D.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * _speed;

            if (_moveDirection.magnitude > 0) 
                _lastMoveDirection = _moveDirection;
            
            AnimateMove();
        }

        private void AnimateMove()
        {
            _animator.SetFloat("LastMoveX", _lastMoveDirection.x);
            _animator.SetFloat("LastMoveY", _lastMoveDirection.y);
            
            _animator.SetFloat("MoveX", _moveDirection.x);
            _animator.SetFloat("MoveY", _moveDirection.y);
            
            _animator.SetFloat("MoveMagnitude", _moveDirection.magnitude);
        }
    }
}
