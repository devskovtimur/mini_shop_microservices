namespace OrderService.Features.Order;

public enum OrderStatus
{
    Created = 0,
    Confirmed = 1,
    Completed = 2,
    Canceled = 3,
    Deleted = 4,
}