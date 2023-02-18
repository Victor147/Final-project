using AutoMapper;
using final_project.Data.Entities;
using final_project.Models;
using final_project.ViewModels;

namespace final_project.MappingProfiles;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<RegisterModel, User>();
        //CreateMap<Product, ProductModel>();
        CreateMap<ProductModel, Product>();
        CreateMap<Product, ProductViewModel>();
    }
}