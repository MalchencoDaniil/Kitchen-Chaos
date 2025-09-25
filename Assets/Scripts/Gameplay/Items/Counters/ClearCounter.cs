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
                if (!_player.HasKitchenObject())
                {
                    GetKitchenObject().SetKitchenObjectParent(_player);
                }
            }
        }
    }
}