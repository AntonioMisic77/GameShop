using AutoMapper;

namespace ProdavaonicaIgaraAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Models.Article, Data.Articles.ArticleDto>().ReverseMap();

            CreateMap<Models.Supplier,Data.Supplier.SupplierDto>().ReverseMap();

            CreateMap<Models.Supplier,Data.Supplier.SupplierDto>().ReverseMap();

            CreateMap<Models.User,Data.User.UserDto>().ReverseMap();

            CreateMap<Models.Receipt, Data.Receipt.ReceiptDto>().ReverseMap();

            CreateMap<Models.Company, Data.Company.CompanyDto>().ReverseMap();

            CreateMap<Models.ReceiptItem, Data.ReceiptItem.ReceiptItemDto>().ReverseMap();
        }
    }
}
