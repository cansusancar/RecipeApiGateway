using Recipe.Gateway.Business.Model;

namespace Recipe.Gateway.Extensions.Business;

public interface IRecipeService
{
    Task<List<RecipeDataModel>> GetAllRecipes();

    Task<RecipeDataModel> GetRecipeById(int id);
    Task<bool> CreateRecipe(CreateRecipeDataModel model);
    Task<bool> UpdateRecipe(UpdateRecipeViewModel recipeDataModel);
    Task<bool> DeleteRecipe(int id);
}