using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MoonBussiness.Interface;
using MoonDataAccess;
using MoonModels;
using MoonModels.DTO.RequestDTO;
using MoonModels.DTO.ResponseDTO;
using MoonModels.Paging;

namespace MoonBussiness.Repository
{
    public class OderRepository : IOderRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        public OderRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext; 
            _mapper = mapper;
        }

        public async Task<OderResponse> CreateOder(OderRequest oderRequest)
        {
            var oder = _mapper.Map<Order>(oderRequest);
            oder.CreatedAt= DateTime.UtcNow;
            oder.UpdatedAt= DateTime.UtcNow;
            var oderitem = new List<OrderItem>();
            foreach (var oderItemRequest in oderRequest.OrderItems)
            {
                var oderItem = _mapper.Map<OrderItem>(oderItemRequest);
                oderItem.CreatedAt= DateTime.UtcNow;
                oderItem.UpdatedAt= DateTime.UtcNow;
                oderitem.Add(oderItem);
            }
            oder.OrderItems = oderitem;
            _dbContext.Orders.Add(oder);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<OderResponse>(oder);
        }

        public Pagination<OderResponse> GetOder(int currentPage, int pageSize)
        {
            var totalRecords = _dbContext.Orders.Count();

            var oders = _dbContext.Orders
                .Include(o => o.OrderItems) // Include OrderItems in the query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Now, you need to map the data to OderResponse and OrderItemResponse.

            var odersResponse = oders.Select(order => new OderResponse
            {
                id = order.Id,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                TableId = order.TableId,
                OrderItems = order.OrderItems.Select(orderItem => new OrderItemResponse
                {
                    id = orderItem.Id,
                    Status = orderItem.Status,
                    CreatedAt = orderItem.CreatedAt,
                    UpdatedAt = orderItem.UpdatedAt,
                    FoodId = orderItem.FoodId,
                    ComboId = orderItem.ComboId,
                    OrderId = orderItem.OrderId,
                    TotalPrice = orderItem.TotalPrice
                }).ToList()
            }).ToList();

            return new Pagination<OderResponse>(odersResponse, totalRecords, currentPage, pageSize);
        }
    }
}
