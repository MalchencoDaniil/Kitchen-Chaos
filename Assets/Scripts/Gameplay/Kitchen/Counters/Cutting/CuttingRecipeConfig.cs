using UnityEngine;

namespace KitchenChaos.Kitchen.Counters.Cutting
{
    [CreateAssetMenu(menuName = "Configs/CuttingRecipeConfig", fileName = "new CuttingRecipeConfig")]
    public class CuttingRecipeConfig : ScriptableObject
    {
        [SerializeField] private KitchenObjectConfig _input;
        [SerializeField] private KitchenObjectConfig _output;
        [SerializeField] private int _maxCuttingProgress = 3;

        public KitchenObjectConfig Input => _input;
        public KitchenObjectConfig Output => _output;
        public int MaxCuttingProgress => _maxCuttingProgress;
    }
}