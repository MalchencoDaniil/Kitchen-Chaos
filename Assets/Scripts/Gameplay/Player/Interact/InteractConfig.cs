using UnityEngine;

namespace KitchenChaos.Player
{
    [CreateAssetMenu(menuName = "Configs/Interact", fileName = "new InteractConfig")]
    public class InteractConfig : ScriptableObject
    {
        [field: SerializeField] public float InteractDistance { get; private set; }
        [field: SerializeField] public LayerMask InteractLayer { get; private set; }
    }
}