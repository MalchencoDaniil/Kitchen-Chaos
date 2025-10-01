using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Kitchen
{
    [CreateAssetMenu(menuName = "Configs/Recipe", fileName = "new RecipeConfig")]
    public class RecipeConfig : ScriptableObject
    {
        [SerializeField] private string _recipeName;
        [SerializeField] private List<KitchenObjectConfig> _recipeList;

        public string RecipeName => _recipeName;
        public List<KitchenObjectConfig> RecipeList => _recipeList;
    }
}