using AutoMapper;
using Recipe.Gateway.Business.Model;
using Recipe.Gateway.Client;
using Recipe.Gateway.Client.Models;

namespace Recipe.Gateway.Extensions.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RecipeDataModel, RecipeViewModel>();
        CreateMap<UpdateRecipeViewModel, UpdateDataModel>();
        CreateMap<CreateRecipeViewModel, CreateRecipeDataModel>();
        CreateMap<ClientModel, RecipeDataModel>();

        CreateMap<UpdateDataModel, ClientModel>();
        
        CreateMap<CreateRecipeDataModel, CreateRecipeClientModel>();
      CreateMap<CreateRecipeViewModel, CreateRecipeDataModel>();
    }
}