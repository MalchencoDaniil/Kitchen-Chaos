using UnityEngine;
using KitchenChaos.Kitchen.Counters;

namespace KitchenChaos.Kitchen.Delivery
{
    public class DeliveryCounter : BaseCounter, IInteractable
    {
        private PlayerPickUp _player;
        private DeliveryManager _deliveryManager;
        
        private void Awake()
        {
            _deliveryManager = FindObjectOfType<DeliveryManager>();
            _player = FindObjectOfType<PlayerPickUp>();
        }

        public void Interact()
        {
            if (_player.HasKitchenObject())
            {
                if (_player.GetKitchenObject().TryGetPlate(out PlateKitchenObject _plateKitchenObject))
                {
                    _deliveryManager.DeliverRecipe(_plateKitchenObject);
                    _player.GetKitchenObject().DestroySelf();
                }
            }
        }
    }
}