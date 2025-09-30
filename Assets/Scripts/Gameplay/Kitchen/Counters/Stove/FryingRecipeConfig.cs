using UnityEngine;

namespace KitchenChaos.Kitchen.Counters.Stove
{
    [CreateAssetMenu(menuName = "Configs/FryingRecipeConfig", fileName = "new FryingRecipeConfig")]
    public class FryingRecipeConfig : ScriptableObject
    {
        [SerializeField] private KitchenObjectConfig _input;
        [SerializeField] private KitchenObjectConfig _output;
        [SerializeField] private int _maxFryingTimer = 3;

        public KitchenObjectConfig Input => _input;
        public KitchenObjectConfig Output => _output;
        public int MaxFryingTimer => _maxFryingTimer;
    }
}