using UnityEngine;

namespace KitchenChaos.Items.Counters.Stove
{
    public class StoveCounter : BaseCounter, IInteractable
    {
        [SerializeField] private FryingRecipeConfig[] _fryingRecipeConfigs;

        private PlayerPickUp _player;

        private float _fryingTimer;
        private FryingRecipeConfig _fryingRecipeConfig;

        private void Start()
        {
            _player = FindObjectOfType<PlayerPickUp>();
        }

        private void Update()
        {
            if (HasKitchenObject())
            {
                _fryingTimer += Time.deltaTime;
                _fryingRecipeConfig = GetFryingRecipeConfigWithInput(GetKitchenObject().GetKitchenObjectConfig());

                if (_fryingTimer > _fryingRecipeConfig.MaxFryingTimer)
                {
                    _fryingTimer = 0;

                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_fryingRecipeConfig.Output, this);
                }
            }
        }

        public void Interact()
        {
            if (!HasKitchenObject())
            {
                if (_player.HasKitchenObject())
                {
                    if (HasRecipeWithInput(_player.GetKitchenObject().GetKitchenObjectConfig()))
                    {
                        _player.GetKitchenObject().SetKitchenObjectParent(this);
                        _fryingRecipeConfig = GetFryingRecipeConfigWithInput(GetKitchenObject().GetKitchenObjectConfig());
                    }
                }
            }
            else
            {
                if (!_player.HasKitchenObject())
                {
                    GetKitchenObject().SetKitchenObjectParent(_player);
                }
            }

            Debug.Log("Interact base");
        }

        private bool HasRecipeWithInput(KitchenObjectConfig _inputKitchenObject)
        {
            FryingRecipeConfig _fryingRecipeConfig = GetFryingRecipeConfigWithInput(_inputKitchenObject);
            return _fryingRecipeConfig != null;
        }

        private KitchenObjectConfig GetOutputForInput(KitchenObjectConfig _inputKitchenObject)
        {
            FryingRecipeConfig _fryingRecipeConfig = GetFryingRecipeConfigWithInput(_inputKitchenObject);
            if (_fryingRecipeConfig != null)
                return _fryingRecipeConfig.Output;
            else
                return _fryingRecipeConfig.Input;
        }

        private FryingRecipeConfig GetFryingRecipeConfigWithInput(KitchenObjectConfig _inputKitchenObject)
        {
            foreach (FryingRecipeConfig _fryingRecipeConfig in _fryingRecipeConfigs)
            {
                Debug.Log("This check: " + _fryingRecipeConfig.Input + " | " + _inputKitchenObject);

                if (_fryingRecipeConfig.Input == _inputKitchenObject)
                {
                    return _fryingRecipeConfig;
                }
            }

            return null;
        }
    }
}