using UnityEngine;

namespace KitchenChaos.Kitchen.Counters
{
    public class TrashCounter : BaseCounter, IInteractable
    {
        private PlayerPickUp _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerPickUp>();
        }

        public void Interact()
        {
            if (_player.HasKitchenObject())
            {
                _player.GetKitchenObject().DestroySelf();
            }
        }
    }
}