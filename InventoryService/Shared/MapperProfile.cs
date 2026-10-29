using InventoryService.Features.Product;
using InventoryService.Features.Product.Dtos;
using InventoryService.Features.Reservation;
using InventoryService.Features.Reservation.Dtos;

namespace InventoryService.Shared;

public class MapperProfile : AutoMapper.Profile
{
    public MapperProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Reservation, ReservationDto>().ForMember(x => x.Status,
            opt => opt.MapFrom(src => src.Status.ToString()));
    }
}