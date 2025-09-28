using System;
using UnityEngine;

namespace KitchenChaos.Items.Counters.Stove
{
    public class StoveCounter : BaseCounter, IInteractable, IHasProgress
    {
        protected internal StateMachine _stoveStateMachine;

        [SerializeField] private FryingRecipeConfig[] _fryingRecipeConfigs;

        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public class OnStateChangedEventArgs : EventArgs
        {
            public State state;
        }

        private PlayerPickUp _player;

        [Header("Stove States")]
        private Idle _idle;
        private Frying _frying;
        private Burned _burned;

        [HideInInspector]
        public FryingRecipeConfig _fryingRecipeConfig;
        protected internal float _fryingTimer;

        private void Start()
        {
            _player = FindObjectOfType<PlayerPickUp>();

            _idle = GetComponent<Idle>();
            _frying = GetComponent<Frying>();
            _burned = GetComponent<Burned>();

            _stoveStateMachine = new StateMachine();
            _stoveStateMachine.Initialize(_idle);
        }

        public void RaiseOnStateChanged(State newState)
        {
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
                state = newState
            });
        }

        public void RaiseOnProgressChanged(float _fryingTimer)
        {
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                _progressNormalized = _fryingTimer / _fryingRecipeConfig.MaxFryingTimer
            });
        }

        private void Update()
        {
            _stoveStateMachine._currentState.UpdateState();
        }

        public void BurnedState() => _stoveStateMachine.ChangeState(_burned);

        public void IdleState() => _stoveStateMachine.ChangeState(_idle);

        public void FryingState() => _stoveStateMachine.ChangeState(_frying);

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

                        FryingState();

                        RaiseOnStateChanged(_stoveStateMachine._currentState);
                        RaiseOnProgressChanged(_fryingTimer);
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

                            IdleState();

                            RaiseOnStateChanged(_stoveStateMachine._currentState);
                            RaiseOnProgressChanged(0);
                        }
                    }
                }
                else
                {
                    GetKitchenObject().SetKitchenObjectParent(_player);
                    IdleState();

                    RaiseOnStateChanged(_stoveStateMachine._currentState);
                    RaiseOnProgressChanged(0);
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

        protected internal FryingRecipeConfig GetFryingRecipeConfigWithInput(KitchenObjectConfig _inputKitchenObject)
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