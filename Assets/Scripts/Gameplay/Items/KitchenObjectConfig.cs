using UnityEngine;

namespace KitchenChaos.Items
{
    [CreateAssetMenu(menuName = "Configs/KitchenObject", fileName = "new KitchenObjectConfig")]
    public class KitchenObjectConfig : ScriptableObject
    {
        [field: SerializeField] public Transform _prefab;
        [field: SerializeField] public Sprite _objectIcon;
        [field: SerializeField] public string _objectName;
    }
}