using UnityEngine;

namespace KitchenChaos.Items.Counters.Delivery
{
    public class DeliveryCounter : BaseCounter, IInteractable
    {
        private PlayerPickUp _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerPickUp>();
        }

        public void Interact()
        {
            if (_player.HasKitchenObject())
            {
                if (_player.GetKitchenObject().TryGetPlate(out PlateKitchenObject _plateKitchenObject))
                {
                    _player.GetKitchenObject().DestroySelf();
                }
            }
        }
    }
}