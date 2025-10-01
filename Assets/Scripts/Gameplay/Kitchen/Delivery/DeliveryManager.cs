using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Kitchen.Delivery
{
    public class DeliveryManager : MonoBehaviour, IListManager<RecipeConfig>
    {
        public event EventHandler OnRecipeSpawned;
        public event EventHandler OnRecipeCompleted;

        [SerializeField] private List<RecipeConfig> _recipeLists;
        private List<RecipeConfig> _waitingRecipeList;

        private float _spawnRecipeTimerMax = 4f;
        private int _waitingRecipesMax = 4;
        private int _successfulRecipesAmount;

        private void Start()
        {
            _waitingRecipeList = new List<RecipeConfig>();

            RecipeSpawningCoroutine().Forget();
        }

        private async UniTaskVoid RecipeSpawningCoroutine()
        {
            while (true)
            {
                await UniTask.WaitForSeconds(_spawnRecipeTimerMax);

                if (_waitingRecipeList.Count < _waitingRecipesMax)
                {
                    RecipeConfig _waitingRecipeConfig = _recipeLists[UnityEngine.Random.Range(0, _recipeLists.Count)];
                    Debug.Log("New Recipe: " + _waitingRecipeConfig.name);

                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);

                    _waitingRecipeList.Add(_waitingRecipeConfig);
                }

                await UniTask.Yield();
            }
        }

        public void DeliverRecipe(PlateKitchenObject _plateKitchenObject)
        {
            for (int i = 0; i < _waitingRecipeList.Count; i++)
            {
                RecipeConfig _waitingRecipeConfig = _waitingRecipeList[i];

                if (_waitingRecipeConfig.RecipeList.Count == _plateKitchenObject.GetKitchenObjectList().Count)
                {
                    bool _plateContentsMatchesRecipe = true;

                    foreach (KitchenObjectConfig recipeKitchenObjectSO in _waitingRecipeConfig.RecipeList)
                    {
                        bool ingredientFound = false;

                        foreach (KitchenObjectConfig plateKitchenObjectSO in _plateKitchenObject.GetKitchenObjectList())
                        {
                            if (plateKitchenObjectSO == recipeKitchenObjectSO)
                            {
                                ingredientFound = true;
                                break;
                            }
                        }
                        if (!ingredientFound)
                        {
                            _plateContentsMatchesRecipe = false;
                        }
                    }

                    if (_plateContentsMatchesRecipe)
                    {
                        _successfulRecipesAmount++;

                        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                        Debug.Log("Recipe correct!");

                        _waitingRecipeList.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        public void Add(RecipeConfig _newRecipe)
        {
            _recipeLists.Add(_newRecipe);
        }

        public void Remove(RecipeConfig _newRecipe)
        {
            _recipeLists.Remove(_newRecipe);
        }

        public List<RecipeConfig> GetWaitingRecipeList()
        {
            return _waitingRecipeList;
        }

        public int GetSuccsessfulRecipesAmount()
        {
            return _successfulRecipesAmount;
        }
    }
}