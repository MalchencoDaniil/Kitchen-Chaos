using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Kitchen.Counters.Cutting
{
    public class CuttingCounter : BaseCounter, IInteractable, IInteractAlternative, IHasProgress
    {
        private PlayerPickUp _player;

        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler OnCut;

        [SerializeField] private CuttingRecipeConfig[] _cuttingRecipeConfgigs;

        private int _cuttingProgress;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerPickUp>();
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
                        _cuttingProgress = 0;

                        CuttingRecipeConfig _cuttingRecipeConfig = GetCuttingRecipeConfigWithInput(GetKitchenObject().GetKitchenObjectConfig());

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            _progressNormalized = (float)_cuttingProgress / _cuttingRecipeConfig.MaxCuttingProgress
                        });
                    }
                }
            }
            else
            {
                if (_player.HasKitchenObject())
                {
                    if (_player.GetKitchenObject().TryGetPlate(out PlateKitchenObject _plateKitchenObject))
                    {
                        if (_plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectConfig()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                }
                else
                {
                    GetKitchenObject().SetKitchenObjectParent(_player);

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        _progressNormalized = 0
                    });
                }
            }

            Debug.Log("Interact base");
        }

        public void InteractAlternative()
        {
            if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectConfig()))
            {
                _cuttingProgress++;

                OnCut?.Invoke(this, EventArgs.Empty);

                CuttingRecipeConfig _cuttingRecipeConfig = GetCuttingRecipeConfigWithInput(GetKitchenObject().GetKitchenObjectConfig());
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    _progressNormalized = (float)_cuttingProgress / _cuttingRecipeConfig.MaxCuttingProgress
                });

                if (_cuttingProgress >= _cuttingRecipeConfig.MaxCuttingProgress)
                {
                    KitchenObjectConfig _outputKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectConfig());
                    Debug.Log(_outputKitchenObject);

                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(_outputKitchenObject, this);
                }
            }
        }

        private bool HasRecipeWithInput(KitchenObjectConfig _inputKitchenObject)
        {
            CuttingRecipeConfig _cuttingRecipeConfig = GetCuttingRecipeConfigWithInput(_inputKitchenObject);
            return _cuttingRecipeConfig != null;
        }

        private KitchenObjectConfig GetOutputForInput(KitchenObjectConfig _inputKitchenObject)
        {
            CuttingRecipeConfig _cuttingRecipeConfig = GetCuttingRecipeConfigWithInput(_inputKitchenObject);
            if (_cuttingRecipeConfig != null)
                return _cuttingRecipeConfig.Output;
            else
                return _cuttingRecipeConfig.Input;
        }

        private CuttingRecipeConfig GetCuttingRecipeConfigWithInput(KitchenObjectConfig _inputKitchenObject)
        {
            foreach (CuttingRecipeConfig _cuttingRecipeConfig in _cuttingRecipeConfgigs)
            {
                Debug.Log(_cuttingRecipeConfig.Input, _inputKitchenObject);

                if (_cuttingRecipeConfig.Input == _inputKitchenObject)
                {
                    return _cuttingRecipeConfig;
                }
            }

            return null;
        }
    }
}