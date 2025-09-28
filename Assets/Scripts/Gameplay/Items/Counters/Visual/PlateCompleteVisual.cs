using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Items.Counters.Visual
{
    public class PlateCompleteVisual : CounterVisual
    {
        [Serializable]
        public struct KitchenObjectConfigGameObject
        {
            public KitchenObjectConfig KitchenObjectConfig;
            public GameObject GameObject;
        }

        [SerializeField] private PlateKitchenObject _plateKitchenObject;
        [SerializeField] private List<KitchenObjectConfigGameObject> _kitchenObjectsList;

        private void Awake()
        {
            _plateKitchenObject.OnIngredientAdded += PlateObjectOnIngredientAdded;

            foreach (KitchenObjectConfigGameObject _kitchenObjectConfigGameObject in _kitchenObjectsList)
            {
                _kitchenObjectConfigGameObject.GameObject.SetActive(false);
            }
        }

        public void PlateObjectOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs eventArgs)
        {
            Debug.Log("True: PlateObjectOnIngredientAdded");

            foreach (KitchenObjectConfigGameObject _kitchenObjectConfigGameObject in _kitchenObjectsList)
            {
                if (_kitchenObjectConfigGameObject.KitchenObjectConfig == eventArgs._kitchenObjectConfig)
                    _kitchenObjectConfigGameObject.GameObject.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            _plateKitchenObject.OnIngredientAdded -= PlateObjectOnIngredientAdded;
        }
    }
}