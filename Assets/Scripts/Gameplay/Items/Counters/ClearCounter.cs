using UnityEngine;

namespace KitchenChaos.Items.Counters
{
    public class ClearCounter : BaseCounter, IInteractable
    {
        private PlayerPickUp _player;

        [SerializeField] private KitchenObjectConfig _kitchenObjectConfig;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerPickUp>();
        }

        public void Interact()
        {
            if (!HasKitchenObject())
            {
                if (_player.HasKitchenObject())
                {
                    _player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
            else
            {
                if (_player.HasKitchenObject())
                {
                    if (_player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectConfig()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                    else
                    {
                        if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                        {
                            if (plateKitchenObject.TryAddIngredient(_player.GetKitchenObject().GetKitchenObjectConfig()))
                            {
                                _player.GetKitchenObject().DestroySelf();
                            }
                        }
                    }
                }
                else
                {
                    GetKitchenObject().SetKitchenObjectParent(_player);
                }
            }
        }
    }
}