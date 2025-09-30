using UnityEngine;

namespace KitchenChaos.Kitchen.Counters.Stove
{
    public class Frying : State
    {
        private StoveCounter _stove;

        private void Awake()
        {
            _stove = GetComponent<StoveCounter>();
        }

        public override void Enter()
        {
            _stove._fryingTimer = 0;
        }

        public override void UpdateState()
        {
            _stove._fryingTimer += Time.deltaTime;

            _stove.RaiseOnProgressChanged(_stove._fryingTimer);

            if (_stove._fryingTimer > _stove._fryingRecipeConfig.MaxFryingTimer)
            {
                _stove.GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(_stove._fryingRecipeConfig.Output, _stove);

                _stove._fryingTimer = 0f;
                _stove._fryingRecipeConfig = _stove.GetFryingRecipeConfigWithInput(_stove.GetKitchenObject().GetKitchenObjectConfig());

                _stove.RaiseOnStateChanged(_stove._stoveStateMachine._currentState);

                _stove.BurnedState();
            }
        }
    }
}