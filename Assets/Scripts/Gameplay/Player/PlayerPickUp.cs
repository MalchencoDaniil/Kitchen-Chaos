using UnityEngine;
using KitchenChaos.Kitchen;

public class PlayerPickUp : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform _pickUpPoint;

    private KitchenObject _kitchenObject;

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
        return _pickUpPoint;
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