using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Recipe.Gateway.Business.Model;
using Recipe.Gateway.Extensions;
using Recipe.Gateway.Extensions.Business;

namespace Recipe.Gateway.Controllers;

[ApiController]
[Route("api/v1.0/recipes")]
public class RecipeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService, IMapper mapper)
    {
        _recipeService = recipeService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var responseBody = await _recipeService.GetRecipeById(id);
            var responseViewModel = _mapper.Map<RecipeViewModel>(responseBody);
            return Ok(responseViewModel);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("inner exception: " + ex.InnerException?.Message);
            return BadRequest(ex.Message);
        }
    }
[ProducesResponseType(typeof(List<RecipeViewModel>),200)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var responseBody = await _recipeService.GetAllRecipes();
            var responseViewModel = _mapper.Map<List<RecipeViewModel>>(responseBody);
            return Ok(responseViewModel);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("inner exception: " + ex.InnerException?.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe(CreateRecipeViewModel recipeViewModel)
    {
        if (recipeViewModel == null)
        {
            return BadRequest("invalid data in recipeviewmodel.");
        }

        try
        {
            var createRecipeDataModel = _mapper.Map<CreateRecipeDataModel>(recipeViewModel);
            var responseBody = await _recipeService.CreateRecipe(createRecipeDataModel);
            if (responseBody)
            {
                // Başarılı bir şekilde oluşturulduysa, 200 OK yanıtı dön
                return Ok();
            }

            // Oluşturma işlemi başarısız olduysa, uygun bir hata mesajı dön
            return BadRequest("recipe creation failed."); // veya başka bir hata mesajı
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("inner exception: " + ex.InnerException?.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRecipe(UpdateRecipeViewModel recipeRecipeViewModel)
    {
        try
        {
            // var createRecipeDataModel = _mapper.Map<CreateRecipeDataModel>(recipeViewModel);

            if (recipeRecipeViewModel == null)
            {
                // Mapper dönüşümü sırasında hata oluştu, işlem başarısız.
                return BadRequest("invalid data in recipeviewmodel."); // veya başka bir hata mesajı
            }

            var responseBody = await _recipeService.UpdateRecipe(recipeRecipeViewModel);
            if (responseBody != null)
            {
                // Başarılı bir şekilde oluşturulduysa, 200 OK yanıtı dön
                return Ok(responseBody);
            }

            // Oluşturma işlemi başarısız olduysa, uygun bir hata mesajı dön
            return BadRequest("recipe creation failed."); // veya başka bir hata mesajı

            // var updateRecipeDataModel = _mapper.Map<UpdateDataModel>(recipeViewModel);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("inner exception: " + ex.InnerException?.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRecipe(int id)
    {
        try
        {
            var responseBody = await _recipeService.DeleteRecipe(id);
            return Ok(responseBody);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("inner exception: " + ex.InnerException?.Message);
            return BadRequest(ex.Message);
        }
    }
}