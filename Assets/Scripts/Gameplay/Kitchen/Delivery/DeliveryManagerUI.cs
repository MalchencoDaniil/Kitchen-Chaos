using System;
using UnityEngine;

namespace KitchenChaos.Kitchen.Delivery
{
    public class DeliveryManagerUI : MonoBehaviour
    {
        private DeliveryManager _deliveryManager;

        [SerializeField] private Transform _container;
        [SerializeField] private Transform _recipeTemplate;

        private void Awake()
        {
            _deliveryManager = FindObjectOfType<DeliveryManager>();

            _recipeTemplate.gameObject.SetActive(false);

            _deliveryManager.OnRecipeCompleted += RecipeCompleted;
            _deliveryManager.OnRecipeSpawned += RecipeSpawned;
        }

        private void OnDestroy()
        {
            _deliveryManager.OnRecipeCompleted -= RecipeCompleted;
            _deliveryManager.OnRecipeSpawned -= RecipeSpawned;
        }

        private void RecipeSpawned(object _sender, EventArgs _eventArgs)
        {
            UpdateVisual();
        }

        private void RecipeCompleted(object _sender, EventArgs _eventArgs)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform _child in _container)
            {
                if (_child == _recipeTemplate) continue;

                Destroy(_child.gameObject);
            }

            foreach (RecipeConfig _recipeCongif in _deliveryManager.GetWaitingRecipeList())
            {
                Transform _newRecipeTemplate = Instantiate(_recipeTemplate, _container);
                _newRecipeTemplate.gameObject.SetActive(true);
            }
        }
    }
}