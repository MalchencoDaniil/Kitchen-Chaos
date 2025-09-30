using UnityEngine;

namespace KitchenChaos.Kitchen
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectConfig _kitchenObjectConfig;

        private IKitchenObjectParent _kitchenObjectParent;

        public KitchenObjectConfig GetKitchenObjectConfig()
        { 
            return _kitchenObjectConfig; 
        }

        public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
        {
            if (_kitchenObjectParent != null)
            {
                _kitchenObjectParent.ClearKitchenObject();
            }

            kitchenObjectParent.SetKitchenObject(this);
            _kitchenObjectParent = kitchenObjectParent;

            transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public IKitchenObjectParent GetKitchenObjectParent()
        {
            return _kitchenObjectParent;
        }

        public void DestroySelf()
        {
            _kitchenObjectParent.ClearKitchenObject();

            Destroy(gameObject);
        }

        public bool TryGetPlate(out PlateKitchenObject _plateKitchenObject)
        {
            if (this is PlateKitchenObject)
            {
                _plateKitchenObject = this as PlateKitchenObject;
                return true;
            }

            _plateKitchenObject = null;
            return false;
        }

        public static KitchenObject SpawnKitchenObject(KitchenObjectConfig _kitchebObjecyConfig, IKitchenObjectParent _kitchenObjectParent)
        {
            Transform _kitchenObjectTransform = Instantiate(_kitchebObjecyConfig._prefab);
            KitchenObject _kitchenObject = _kitchenObjectTransform.GetComponent<KitchenObject>();

            _kitchenObject.SetKitchenObjectParent(_kitchenObjectParent);

            return _kitchenObject;
        }
    }
}