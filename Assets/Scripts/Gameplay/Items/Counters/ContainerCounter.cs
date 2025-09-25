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
                Transform _kitchebObjectPrefab = Instantiate(_kitchenObjectConfig._prefab, _counterTopPoint.position, Quaternion.identity);
                Debug.Log(_kitchebObjectPrefab.GetComponent<KitchenObject>().GetKitchenObjectConfig());

                _kitchenObject = _kitchebObjectPrefab.GetComponent<KitchenObject>();
                _kitchenObject.SetKitchenObjectParent(_player);

                OnGrabledObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}