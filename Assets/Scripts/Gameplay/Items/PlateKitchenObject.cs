using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Items
{
    public class PlateKitchenObject : KitchenObject
    {
        private List<KitchenObjectConfig> _kitchenObjectConfigs;

        private void Awake()
        {
            _kitchenObjectConfigs = new List<KitchenObjectConfig>();
        }

        public void TryAddIngredient(KitchenObjectConfig _kitchenObjectConfig)
        {
            _kitchenObjectConfigs.Add(_kitchenObjectConfig);
        }
    }
}