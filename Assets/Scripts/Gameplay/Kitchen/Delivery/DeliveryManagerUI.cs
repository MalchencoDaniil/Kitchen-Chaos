using Cysharp.Threading.Tasks;
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
            _recipeTemplate.gameObject.SetActive(false);
        }

        private void Start()
        {
            _deliveryManager = FindObjectOfType<DeliveryManager>();

            _deliveryManager.OnRecipeCompleted += RecipeCompleted;
            _deliveryManager.OnRecipeSpawned += RecipeSpawned;

            UpdateVisual().Forget();
        }

        private void OnDestroy()
        {
            _deliveryManager.OnRecipeCompleted -= RecipeCompleted;
            _deliveryManager.OnRecipeSpawned -= RecipeSpawned;
        }

        private void RecipeSpawned(object _sender, EventArgs _eventArgs)
        {
            UpdateVisual().Forget();
        }

        private void RecipeCompleted(object _sender, EventArgs _eventArgs)
        {
            UpdateVisual().Forget();
        }

        private async UniTaskVoid UpdateVisual()
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

                _newRecipeTemplate.GetComponent<DeliveryRecipeCellUI>().SetRecipeUIConfig(_recipeCongif);
            }
        }
    }
}