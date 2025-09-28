using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Items
{
    public class PlateKitchenObject : KitchenObject
    {
        [SerializeField] private List<KitchenObjectConfig> _validKitchenObjects;

        private List<KitchenObjectConfig> _kitchenObjectConfigs;

        private void Awake()
        {
            _kitchenObjectConfigs = new List<KitchenObjectConfig>();
        }

        public bool TryAddIngredient(KitchenObjectConfig _kitchenObjectConfig)
        {
            if (!_validKitchenObjects.Contains(_kitchenObjectConfig))
                return false;

            if (_kitchenObjectConfigs.Contains(_kitchenObjectConfig))
            {
                return false;
            }
            else
            {
                _kitchenObjectConfigs.Add(_kitchenObjectConfig);
                return true;
            }
        }
    }
}