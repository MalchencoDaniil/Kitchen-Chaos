using UnityEngine;

namespace KitchenChaos.Items.Counters
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] protected Transform _counterTopPoint;

        protected KitchenObject _kitchenObject;

        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }

        public KitchenObject GetKitchenObject()
        {
            return _kitchenObject;
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return _counterTopPoint;
        }

        public bool HasKitchenObject()
        {
            return _kitchenObject != null;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
        }
    }
}