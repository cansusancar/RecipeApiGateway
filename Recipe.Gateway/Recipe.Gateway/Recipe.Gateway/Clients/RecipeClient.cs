using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using Recipe.Gateway.Business.Model;
using Recipe.Gateway.Client.Models;
using Recipe.Gateway.Extensions;

namespace Recipe.Gateway.Client;

public class RecipeClient : IRecipeClient
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public RecipeClient(IMapper mapper, HttpClient httpClient)
    {
        _mapper = mapper;
        _httpClient = httpClient;
    }

    public async Task<ClientModel> GetRecipeByIdAsync(int id)
    {
        // string apiUrl = id.ToString();
        var url = $"api/v1.0/recipes/{id}";

        try
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var recipe = JsonConvert.DeserializeObject<ClientModel>(responseBody);
                return recipe;
            }

            throw new HttpRequestException($"API request failed with status code: {response.StatusCode}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
            throw;
        }
    }

    public async Task<ClientModel[]> GetAllRecipesAsync()
    {
        var url = "api/v1.0/recipes/";
        try
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var recipes = JsonConvert.DeserializeObject<ClientModel[]>(responseBody);
                return recipes;
            }

            throw new HttpRequestException($"API request failed with status code: {response.StatusCode}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
            throw;
        }
    }

    public async Task<bool> CreateRecipeAsync(CreateRecipeClientModel recipe)
    {
        var url = "api/v1.0/recipes/";
        try
        {
            //  var recipeClientModel = _mapper.Map<ClientModel>(recipe);
            // var serializedRecipe = JsonConvert.SerializeObject(recipeClientModel);
            //   var serializedRecipe = JsonConvert.SerializeObject(recipe);
            //    var response = await _httpClient.PostJsonAsync(url, serializedRecipe);
            var response = await _httpClient.PostJsonAsync(url, recipe);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"API request failed with status code: {response.StatusCode}. Error message: {errorMessage}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
            throw;
        }
    }

    public async Task<bool> UpdateRecipeAsync(UpdateRecipeViewModel recipe)//neden controllerdakini direkt buraya aktardık mapper olmadan??
    {//bu mtodun parametresiyle apideki controllerda aynı metodun parametresinin türü eşleşmeli: bunu api dökomantasyonundan alıyoruz
        var url = "api/v1.0/recipes/";
        try
        {
            var response = await _httpClient.PutJsonAsync(url, recipe);
            if (response.IsSuccessStatusCode)
            {
              //  var responseBody = await response.Content.ReadAsStringAsync();
               // var updatedRecipe = JsonConvert.DeserializeObject<ClientModel>(responseBody);
               return true; // return updatedRecipe;

            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"API request failed with status code: {response.StatusCode}. Error message: {errorMessage}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
            throw;
        }

    }

    public async Task<bool> DeleteRecipeAsync(int id)
    {
        var apiUrl = $"api/v1.0/recipes/{id}";
       // var apiUrl = id.ToString();
        try
        {
            var response = await _httpClient.DeleteAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
              //  var responseBody = await response.Content.ReadAsStringAsync();
              //  var deletedRecipe = JsonConvert.DeserializeObject<ClientModel>(responseBody);
              //  return deletedRecipe;
              return true;
            }

            throw new HttpRequestException($"API request failed with status code: {response.StatusCode}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
            throw;
        }
    }
}