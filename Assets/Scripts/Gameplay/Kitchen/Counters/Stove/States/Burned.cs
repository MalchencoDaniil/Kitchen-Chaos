using UnityEngine;
using static UnityEngine.CullingGroup;

namespace KitchenChaos.Kitchen.Counters.Stove
{
    public class Burned : State
    {
        private StoveCounter _stove;

        private float _burnedTimer;

        private void Awake()
        {
            _stove = GetComponent<StoveCounter>();
        }

        public override void Enter()
        {
            _burnedTimer = 0;
        }

        public override void UpdateState()
        {
            _burnedTimer += Time.deltaTime;

            _stove.RaiseOnProgressChanged(_burnedTimer);

            if (_burnedTimer > _stove._fryingRecipeConfig.MaxFryingTimer)
            {
                _stove.GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(_stove._fryingRecipeConfig.Output, _stove);

                _stove.IdleState();

                _stove.RaiseOnStateChanged(_stove._stoveStateMachine._currentState);
                _stove.RaiseOnProgressChanged(0);
            }
        }
    }
}