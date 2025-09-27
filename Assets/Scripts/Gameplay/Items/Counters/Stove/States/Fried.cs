using UnityEngine;
using static UnityEngine.CullingGroup;

namespace KitchenChaos.Items.Counters.Stove
{
    public class Fried : State
    {
        private StoveCounter _stove;

        private float _burnedTimer;

        private void Awake()
        {
            _stove = GetComponent<StoveCounter>();
        }

        public override void UpdateState()
        {
            _burnedTimer += Time.deltaTime;

            if (_burnedTimer > _stove._fryingRecipeConfig.MaxFryingTimer)
            {
                _stove.GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(_stove._fryingRecipeConfig.Output, _stove);

                _stove.IdleState();
            }
        }
    }
}