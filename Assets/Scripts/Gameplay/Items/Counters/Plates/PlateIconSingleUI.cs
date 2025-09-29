using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos.Items.Counters.Plates
{
    public class PlateIconSingleUI : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;

        public void SetKitchenObjectSprite(KitchenObjectConfig _kitchebObjectConfig)
        {
            _iconImage.sprite = _kitchebObjectConfig._objectIcon;
        }
    }
}