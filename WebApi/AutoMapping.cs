using AutoMapper;
using BLL.Companies;
using DAL.Models;
using WebApi.DataTransferObjects;
using WebApi.ViewModels;

namespace WebApi
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            // ViewModels to Models
            CreateMap<OrderViewModel, Order>(MemberList.Source);
            CreateMap<ProductViewModel, Product>(MemberList.Source);
            CreateMap<ProductOrderViewModel, ProductOrder>(MemberList.Source);

            // Models to DataTransferObjects
            CreateMap<Order, OrderDto>(MemberList.None);
            CreateMap<Product, ProductOrderDto>(MemberList.None)
                .ForMember(dest => dest.Price, options => options.Ignore());
            CreateMap<ProductOrder, ProductOrderDto>(MemberList.None)
                .ForMember(dest => dest.Id, options => options.MapFrom(source => source.ProductId));
        }
    }
}
