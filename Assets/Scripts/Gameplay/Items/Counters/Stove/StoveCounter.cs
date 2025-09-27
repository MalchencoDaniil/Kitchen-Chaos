using UnityEngine;

namespace KitchenChaos.Items.Counters.Stove
{
    public class StoveCounter : BaseCounter, IInteractable
    {
        private StateMachine _stoveStateMachine;

        [SerializeField] private FryingRecipeConfig[] _fryingRecipeConfigs;

        private PlayerPickUp _player;

        [Header("Stove States")]
        private Idle _idle;
        private Frying _frying;
        private Fried _fried;

        [HideInInspector]
        public FryingRecipeConfig _fryingRecipeConfig;

        private void Start()
        {
            _player = FindObjectOfType<PlayerPickUp>();

            _idle = GetComponent<Idle>();
            _frying = GetComponent<Frying>();
            _fried = GetComponent<Fried>();

            _stoveStateMachine = new StateMachine();
            _stoveStateMachine.Initialize(_idle);
        }

        private void Update()
        {
            _stoveStateMachine._currentState.UpdateState();
        }

        public void FriedState() => _stoveStateMachine.ChangeState(_fried);

        public void IdleState() => _stoveStateMachine.ChangeState(_idle);

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

                        _stoveStateMachine.ChangeState(_frying);
                    }
                }
            }
            else
            {
                if (!_player.HasKitchenObject())
                {
                    GetKitchenObject().SetKitchenObjectParent(_player);
                    IdleState();
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