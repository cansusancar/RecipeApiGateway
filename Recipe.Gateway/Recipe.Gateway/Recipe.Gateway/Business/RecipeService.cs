using AutoMapper;
using Recipe.Gateway.Business.Model;
using Recipe.Gateway.Client;
using Recipe.Gateway.Client.Models;

namespace Recipe.Gateway.Extensions.Business;

public class RecipeService : IRecipeService
{
    private readonly IMapper _mapper;
    private readonly IRecipeClient _recipeClient;

    public RecipeService(IRecipeClient recipeClient, IMapper mapper)
    {
        _recipeClient = recipeClient;
        _mapper = mapper;
    }

    public async Task<List<RecipeDataModel>> GetAllRecipes()
    {
        var allRecipes = await _recipeClient.GetAllRecipesAsync();
        var response = _mapper.Map<List<RecipeDataModel>>(allRecipes);
        if (response == null)
            return null;

        return response.ToList();
    }

    public async Task<RecipeDataModel> GetRecipeById(int id)
    {
        var recipe = await _recipeClient.GetRecipeByIdAsync(id);

        var recipeDataModel = _mapper.Map<RecipeDataModel>(recipe);
        return recipeDataModel;
    }

    public async Task<bool> CreateRecipe(CreateRecipeDataModel model)
    {
        var recipeClientModel = _mapper.Map<CreateRecipeClientModel>(model);
        var result = await _recipeClient.CreateRecipeAsync(recipeClientModel);

        return result;
    }

    public async Task<bool> UpdateRecipe(UpdateRecipeViewModel updateRecipeRecipeModel)
    {
        var updateResult = await _recipeClient.UpdateRecipeAsync(updateRecipeRecipeModel);
        var isSuccess = updateResult != null;

        return isSuccess;
    }

    public async Task<bool> DeleteRecipe(int id)
    {
        var deleteResult = await _recipeClient.DeleteRecipeAsync(id);
        var isSuccess = deleteResult != null;

        return isSuccess;
    }
}