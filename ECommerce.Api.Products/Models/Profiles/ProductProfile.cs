namespace ECommerce.Api.Products.Models.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Db.Product, Models.Product>();
        }
    }
}
