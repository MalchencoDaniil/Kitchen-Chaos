using KitchenChaos.Items;
using UnityEngine;

namespace KitchenChaos.Items
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

        public KitchenObjectConfig GetKitchenObjectSO()
        {
            return _kitchenObjectConfig;
        }
    }
}