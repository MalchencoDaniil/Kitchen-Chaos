using UnityEngine;

namespace KitchenChaos.Items.Counters
{
    public class ContainerCounter : MonoBehaviour, IInteractable, IKitchenObjectParent
    {
        private PlayerPickUp _player;

        [SerializeField] private KitchenObjectConfig _kitchenObjectConfig;

        [SerializeField] private Transform _counterTopPoint;

        private KitchenObject _kitchenObject;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerPickUp>();
        }

        public void Interact()
        {
            if (_kitchenObject == null)
            {
                Transform _kitchebObjectPrefab = Instantiate(_kitchenObjectConfig._prefab, _counterTopPoint.position, Quaternion.identity);
                Debug.Log(_kitchebObjectPrefab.GetComponent<KitchenObject>().GetKitchenObjectConfig());

                _kitchenObject = _kitchebObjectPrefab.GetComponent<KitchenObject>();
                _kitchenObject.SetKitchenObjectParent(this);
            }
            else
            {
                _kitchenObject.SetKitchenObjectParent(_player);
            }
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return _counterTopPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;

        public KitchenObject GetKitchenObject()
        {
            return _kitchenObject;
        }

        public void ClearKitchenObject() => _kitchenObject = null;

        public bool HasKitchenObject()
        {
            return _kitchenObject != null;
        }
    }
}