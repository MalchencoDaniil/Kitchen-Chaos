using UnityEngine;

namespace KitchenChaos.Items.Counters.Stove
{
    public class Frying : State
    {
        private StoveCounter _stove;

        private float _fryingTimer;

        private void Awake()
        {
            _stove = GetComponent<StoveCounter>();
        }

        public override void UpdateState()
        {
            _fryingTimer += Time.deltaTime;

            if (_fryingTimer > _stove._fryingRecipeConfig.MaxFryingTimer)
            {
                _stove.GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(_stove._fryingRecipeConfig.Output, _stove);

                _stove.FriedState();
                _fryingTimer = 0f;
                _stove._fryingRecipeConfig = _stove.GetFryingRecipeConfigWithInput(_stove.GetKitchenObject().GetKitchenObjectConfig());
            }
        }
    }
}