using Recipe.Gateway.Business.Model;
using Recipe.Gateway.Client.Models;
using Recipe.Gateway.Extensions;

namespace Recipe.Gateway.Client;

public interface IRecipeClient
{
    Task<ClientModel> GetRecipeByIdAsync(int id);
    Task<ClientModel[]> GetAllRecipesAsync();
    Task<bool> CreateRecipeAsync(CreateRecipeClientModel recipe);
    Task<ClientModel> UpdateRecipeAsync(UpdateRecipeViewModel recipe);
    Task<ClientModel> DeleteRecipeAsync(int id);
}