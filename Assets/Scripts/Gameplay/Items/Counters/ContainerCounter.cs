using System;
using UnityEngine;

namespace KitchenChaos.Items.Counters
{
    public class ContainerCounter : BaseCounter, IInteractable
    {
        private PlayerPickUp _player;

        [SerializeField] private KitchenObjectConfig _kitchenObjectConfig;

        public event EventHandler OnGrabledObject;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerPickUp>();
        }

        public void Interact()
        {
            if (!_player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(_kitchenObjectConfig, _player);

                OnGrabledObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}