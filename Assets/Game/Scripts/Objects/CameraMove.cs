using UnityEngine;

namespace Game.Scripts.Objects
{
    public sealed class CameraMove : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private Vector3 _offset;

        private Vector3 _desiredPosition;
        private Vector3 _smoothedPosition;

        private void LateUpdate()
        {
            _desiredPosition = _target.position + _offset;
            _smoothedPosition = Vector3.Lerp(
                transform.position,
                _desiredPosition,
                _smoothSpeed * Time.deltaTime);

            transform.position = _smoothedPosition;
        }
    }
}
