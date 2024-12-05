using Microsoft.EntityFrameworkCore;
using Pets_Project_Backend.Context;
using Pets_Project_Backend.Data.Models.OrderModel;
using Pets_Project_Backend.Data.Models.OrderModel.Order_Dto;
using Pets_Project_Backend.Data.Models.ProductModel;

namespace Pets_Project_Backend.Services.Order_Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _context;
        public OrderService(ApplicationContext context)
        {
            _context = context;
        }

        public async  Task<bool> indvidual_ProductBuy(int userId, int productId, CreateOrder_Dto order_Dto)
        {
            try
            {
                var pro= await _context.Products.FirstOrDefaultAsync(a=>a.ProductId==productId);
                if (pro==null)
                {
                    throw new Exception("Product not found");
                }

                if (pro.StockId <= 0)
                {
                    throw new Exception("Product is out of stock");
                }

              var order = await _context.Order
             .Include(a => a._Items)
             .ThenInclude(b => b._product)
             .FirstOrDefaultAsync(c => c.userId == userId);

                    var new_order = new Order
                    {
                        userId = userId,
                        OrderDate=DateTime.Now,
                        CustomerName = order_Dto.CustomerName,
                        CustomerEmail = order_Dto.CustomerEmail,
                        CustomerPhone = order_Dto.CustomerPhone,
                        Total = order_Dto.Total,
                        CustomerCity = order_Dto.CustomerCity,
                        HomeAddress = order_Dto.HomeAddress,
                        OrderString = order_Dto.OrderString,
                        TransactionId = order_Dto.TransactionId,
                        _Items = new List<OrderItem>()
                    };

                var orderItem = new OrderItem
                {
                    ProductId = pro.ProductId,
                    Quantity=1,
                    TotalPrice=pro.ProductPrice*1
                };

                new_order._Items?.Add(orderItem);
                await _context.Order.AddAsync(new_order);

                pro.StockId -= 1;
                _context.Products.Update(pro);

                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving changes: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<bool> CreateOrder_CheckOut(int userId, CreateOrder_Dto createOrderDto)
        {
            try
            {
                var cart =await _context.Cart
                    .Include(b=>b._Items)
                    .ThenInclude(c=>c._Product)
                    .FirstOrDefaultAsync(z=>z.UserId == userId);

                if (cart == null)
                {
                    throw new Exception("Cart is empty");
                }

                var order = new Order
                {
                    userId = userId,
                    OrderDate=DateTime.Now,
                    CustomerName= createOrderDto.CustomerName,
                    CustomerEmail= createOrderDto.CustomerEmail,
                    CustomerCity= createOrderDto.CustomerCity,
                    CustomerPhone= createOrderDto.CustomerPhone,
                    HomeAddress= createOrderDto.HomeAddress,
                    Total= createOrderDto.Total,
                    OrderString= createOrderDto.OrderString,
                    TransactionId= createOrderDto.TransactionId,
                    _Items=cart._Items.Select(a=> new OrderItem
                    {
                        
                        ProductId=a._Product.ProductId,
                        Quantity=a.ProductQty,
                        TotalPrice=a._Product.ProductPrice*a.ProductQty

                    }).ToList()
                };

                foreach (var item in cart._Items)
                {
                    var pro=await _context.Products.FirstOrDefaultAsync(a=>a.ProductId==item.ProductId);
                    if(pro != null)
                    {
                        if (pro.StockId <= item.ProductQty)
                        {
                            return false;
                        }

                        pro.StockId-= item.ProductQty;
                    }
                }

             await   _context.Order.AddAsync(order);
                _context.Cart.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<OrderView_Dto>> GetOrderDetails(int userId)
        {
            try
            {
               var orders=await _context.Order.Include(a=>a._Items)
                    .ThenInclude(b=>b._product)
                    .Where(c=>c.userId==userId)
                    .ToListAsync();

                var orderDetails= orders.Select(a=> new OrderView_Dto
                {
                    OrderId=a.OrderId,
                    OrderDate = a.OrderDate.Value,
                    Items =a._Items.Select(b=> new OrderItemDto
                    {
                        OrderItemId=b.OrderId,
                        OrderId =b._order.OrderId,
                        ProductId=b.ProductId,
                        ProductImage=b._product.ImageUrl,
                        ProductName=b._product.ProductName,
                        Quantity=b.Quantity,
                        TotalPrice=b.TotalPrice,
                    }).ToList(),
                }).ToList();

                return orderDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OrderAdminViewDto>> GetOrderDetailsAdmin()
        {
            try
            {
                var orders=  await _context.Order.Include(a=>a._Items).ToListAsync();
                if (orders.Count > 0)
                {
                    var details = orders.Select(a => new OrderAdminViewDto
                    {
                        OrderId = a.OrderId,
                        OrderDate = a.OrderDate.Value,
                        OrderString = a.OrderString,
                        TransactionId = a.TransactionId,
                        CustomerEmail = a.CustomerEmail,
                        CustomerName = a.CustomerName,
                        CustomerPhone = a.CustomerPhone,

                    }).ToList();

                    return details;
                }

                return new List<OrderAdminViewDto>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<decimal> TotalRevenue()
        {
            try
            {
                var Total = await _context.OrderItem.SumAsync(i => i.TotalPrice);
                return Convert.ToDecimal(Total);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> TotalProductsPurchased()
        {
            try
            {
                var total_pro = await _context.OrderItem.SumAsync(i => i.Quantity);
                return Convert.ToInt32(total_pro);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OrderView_Dto>> GetOrderDetailsAdmin_byuserId(int userId)
        {
            try
            {
                var orders = await _context.Order.Include(a => a._Items)
                   .ThenInclude(b => b._product)
                   .Where(c => c.userId == userId)
                   .ToListAsync();

                var orderDetails = orders.Select(a => new OrderView_Dto
                {
                    OrderId = a.OrderId,
                    OrderDate = a.OrderDate.Value,
                    Items = a._Items.Select(b => new OrderItemDto
                    {
                        OrderItemId = b.OrderId,
                        OrderId = b._order.OrderId,
                        ProductId = b.ProductId,
                        ProductImage = b._product.ImageUrl,
                        ProductName = b._product.ProductName,
                        Quantity = b.Quantity,
                        TotalPrice = b.TotalPrice,
                    }).ToList(),
                }).ToList();

                return orderDetails;
            }
            catch (Exception ex)
            {
                    throw new Exception(ex.Message);
            }
        }



    }
}
