using UnityEngine;

namespace KitchenChaos.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        public PlayerInput _playerInput;

        public PlayerTransform _playerTransform {  get; private set; }

        [Header("References")]
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField] private CharacterController _characterController;

        private Camera _mainCamera;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerTransform = new PlayerTransform();

            _mainCamera = Camera.main;
        }

        private Vector3 _movementDirection = Vector3.zero;
        private Vector3 _movementInput = Vector3.zero;

        private void Update()
        {
            Move();

            if (_movementDirection != Vector3.zero)
            {
                transform.forward = Vector3.Slerp(transform.forward, _movementDirection, Time.deltaTime * _movementConfig.RotationSpeed);
            }
        }

        private void Move()
        {
            _movementInput = Quaternion.Euler(0, _mainCamera.transform.eulerAngles.y, 0) * new Vector3(_playerInput.MovementInput().x, 0, _playerInput.MovementInput().y).normalized;
            _movementDirection = _movementInput.normalized;

            _playerTransform.MovementDirection = _movementDirection;
            _playerTransform.MovementInput = _movementInput;

            _characterController.Move(_movementDirection * _movementConfig.MovementSpeed * Time.deltaTime);
        }

        private void Rotate()
        {

        }
    }
}