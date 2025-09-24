using System;
using UnityEngine;

namespace KitchenChaos.Player
{
    public class Interact : MonoBehaviour
    {
        [SerializeField] private InteractConfig _interactConfig;

        public static event Action<GameObject> OnInteractableFound;
        public static event Action OnInteractableLost;

        private Movement _movementController;
        private GameObject _lastInteractableObject;
        private bool _wasInteractableInRange;

        private void Awake()
        {
            _movementController = GetComponent<Movement>();
        }

        private void Update()
        {
            CheckRaycast();
            CheckInteraction();
        }

        private void CheckRaycast()
        {
            if (Physics.Raycast(transform.position, _movementController._playerTransform.MovementDirection,
                out RaycastHit hit, _interactConfig.InteractDistance, _interactConfig.InteractLayer))
            {
                var interactables = hit.collider.GetComponents<IInteractable>();
                bool hasInteractables = interactables.Length > 0;

                HandleInteractableDetection(hit.collider.gameObject, hasInteractables);

                if (hasInteractables)
                    Debug.Log($"Interactable in range: {hit.collider.name}");
            }
            else
            {
                HandleInteractableDetection(null, false);
            }
        }

        private void HandleInteractableDetection(GameObject hitObject, bool hasInteractables)
        {
            bool _isInteractableInRange = hitObject != null && hasInteractables;
            GameObject _currentInteractable = _isInteractableInRange ? hitObject : null;

            if (_isInteractableInRange != _wasInteractableInRange ||
                _currentInteractable != _lastInteractableObject)
            {
                if (_isInteractableInRange)
                {
                    OnInteractableFound?.Invoke(_currentInteractable);
                    _lastInteractableObject = _currentInteractable;
                }
                else
                {
                    OnInteractableLost?.Invoke();
                    _lastInteractableObject = null;
                }

                _wasInteractableInRange = _isInteractableInRange;
            }
        }

        private void CheckInteraction()
        {
            if (_lastInteractableObject != null && _movementController._playerInput.CanInteract())
            {
                var _interactables = _lastInteractableObject.GetComponents<IInteractable>();
                
                foreach (var _interactable in _interactables)
                {
                    _interactable.Interact();
                }

                Debug.Log($"Interacted with: {_lastInteractableObject.name}");

                ResetInteractable();
            }
        }

        private void ResetInteractable()
        {
            if (_lastInteractableObject != null)
            {
                OnInteractableLost?.Invoke();
                _lastInteractableObject = null;
                _wasInteractableInRange = false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_movementController != null && _interactConfig != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position,
                    _movementController._playerTransform.MovementDirection * _interactConfig.InteractDistance);
            }
        }
    }
}