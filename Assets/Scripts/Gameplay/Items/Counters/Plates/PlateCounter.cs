using System;
using UnityEngine;

namespace KitchenChaos.Items.Counters.Plates
{
    public class PlateCounter : BaseCounter ,IInteractable
    {
        [SerializeField] private KitchenObjectConfig _kitchenObjectConfig;

        private PlayerPickUp _player;

        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemove;

        private float _spawnPlateTimer;
        private float _spawnPlateTimerMax = 4;

        private int _platesSpawnedAmount;
        private int _platesSpawnedAmountMax = 3;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerPickUp>();
        }

        private void Update()
        {
            _spawnPlateTimer += Time.deltaTime;

            if (_spawnPlateTimer > _spawnPlateTimerMax)
            {
                _spawnPlateTimer = 0;

                if (_platesSpawnedAmount < _platesSpawnedAmountMax)
                {
                    _platesSpawnedAmount++;

                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void Interact()
        {
            if (!_player.HasKitchenObject())
            {
                if (_platesSpawnedAmount > 0)
                {
                    _platesSpawnedAmount--;

                    KitchenObject.SpawnKitchenObject(_kitchenObjectConfig, _player);
                    OnPlateRemove?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}