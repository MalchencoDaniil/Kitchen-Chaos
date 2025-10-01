using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos.Kitchen.Delivery
{
    public class DeliveryRecipeCellUI : MonoBehaviour
    {
        [SerializeField] private Text _recipeName;
        [SerializeField] private Transform _iconContainer;
        [SerializeField] private Transform _iconTemplate;

        private void Awake()
        {
            _iconTemplate.gameObject.SetActive(false);
        }

        public void SetRecipeUIConfig(RecipeConfig _recipeConfig)
        {
            _recipeName.text = _recipeConfig.RecipeName;

            foreach (Transform _child in _iconContainer)
            {
                if (_child == _iconTemplate) continue;

                Destroy(_child.gameObject);
            }

            foreach (KitchenObjectConfig _kitchenObjectConfig in _recipeConfig.RecipeList)
            {
                Transform _iconTransform = Instantiate(_iconTemplate, _iconContainer);
                _iconTransform.gameObject.SetActive(true);
                _iconTransform.GetComponent<Image>().sprite = _kitchenObjectConfig._objectIcon;
            }
        }
    }
}