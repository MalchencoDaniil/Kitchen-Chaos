using UnityEngine;

namespace KitchenChaos.Items.Counters.Plates
{
    public class PlateIconsUI : MonoBehaviour
    {
        [SerializeField] private PlateKitchenObject _plateKitchenObject;
        [SerializeField] private Transform _iconTemplate;

        private void Awake()
        {
            _plateKitchenObject.OnIngredientAdded += PlateOnIngridientAdded;

            _iconTemplate.gameObject.SetActive(false);
        }

        private void PlateOnIngridientAdded(object _sender, PlateKitchenObject.OnIngredientAddedEventArgs _eventArgs)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform child in transform)
            {
                if (child == _iconTemplate)
                {
                    continue;
                }

                Destroy(child.gameObject);
            }

            foreach (KitchenObjectConfig _kitchenObjectConfig in _plateKitchenObject.GetKitchenObjectList())
            {
                Transform _iconTransform = Instantiate(_iconTemplate, transform);
                _iconTransform.gameObject.SetActive(true);
                _plateKitchenObject.GetComponent<PlateIconSingle>().SetKitchenObjectSprite(_kitchenObjectConfig);
            }
        }

        private void OnDestroy()
        {
            _plateKitchenObject.OnIngredientAdded -= PlateOnIngridientAdded;
        }
    }
}