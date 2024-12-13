using Microsoft.EntityFrameworkCore;
using Pets_Project_Backend.ApiResponse;
using Pets_Project_Backend.Context;
using Pets_Project_Backend.Data.Models.OrderModel;
using Pets_Project_Backend.Data.Models.OrderModel.Order_Dto;
using Pets_Project_Backend.Data.Models.ProductModel;
using Razorpay.Api;

namespace Pets_Project_Backend.Services.Order_Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;
        public OrderService(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //razor pay

        public async Task<string> RazorPayOrderCreate(long price)
        {
            try
            {
                Dictionary<string, object> input = new Dictionary<string, object>();
                Random random = new Random();
                string TrasactionId = random.Next(0, 1000).ToString();
                input.Add("amount", Convert.ToDecimal(price) * 100);
                input.Add("currency", "INR");
                input.Add("receipt", TrasactionId);

                string key = _configuration["Razorpay:KeyId"];
                string secret = _configuration["Razorpay:KeySecret"];

                RazorpayClient client = new RazorpayClient(key, secret);
                Razorpay.Api.Order order = client.Order.Create(input);
                var OrderId = order["id"].ToString();

                return OrderId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //razor pay paynent verfication
        public async Task<bool> RazorPayment(PaymentDto payment)
        {

            if (payment == null ||
               string.IsNullOrEmpty(payment.razorpay_payment_id) ||
               string.IsNullOrEmpty(payment.razorpay_order_id) ||
               string.IsNullOrEmpty(payment.razorpay_signature))
            {
                return false;
            }

            try
            {
                RazorpayClient client = new RazorpayClient(
                   _configuration["Razorpay:KeyId"],
                   _configuration["Razorpay:KeySecret"]
               );

                Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "razorpay_payment_id", payment.razorpay_payment_id },
                    { "razorpay_order_id", payment.razorpay_order_id },
                    { "razorpay_signature", payment.razorpay_signature }
                };

                Utils.verifyPaymentSignature(attributes);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message);
            }
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

                    var new_order = new Data.Models.OrderModel.Order
                    {
                        userId = userId,
                        OrderDate=DateTime.Now,
                        AddressId=order_Dto.AddId,
                        Total = order_Dto.Total,
                        OrderString = order_Dto.OrderString,
                        TransactionId = order_Dto.TransactionId,
                        _Items = new List<OrderItem>()
                    };

                var orderItem = new OrderItem
                {
                    ProductId = pro.ProductId,
                    Quantity=1,
                    TotalPrice=pro.OfferPrize*1
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

                var order = new Data.Models.OrderModel.Order
                {
                    userId = userId,
                    OrderDate=DateTime.Now,
                    AddressId=createOrderDto.AddId,
                    Total= createOrderDto.Total,
                    OrderString= createOrderDto.OrderString,
                    TransactionId= createOrderDto.TransactionId,
                    _Items=cart._Items.Select(a=> new OrderItem
                    {
                        
                        ProductId=a._Product.ProductId,
                        Quantity=a.ProductQty,
                        TotalPrice=a._Product.OfferPrize*a.ProductQty

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

                var orderDetails= orders
                    .Where(r=>r._Items?.Count>0)
                    .Select(a=> new OrderView_Dto
                {
                    OrderId=a.OrderId,
                    OrderDate = a.OrderDate.Value,
                    OrderStatus = a.OrderStatus,
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
                var orders=  await _context.Order
                    .Include(z=>z._UserAd)
                    .Include(a=>a._Items).ToListAsync();
                if (orders.Count > 0)
                {
                    var details = orders.Select(a => new OrderAdminViewDto
                    {
                        OrderId = a.OrderId,
                        OrderDate = a.OrderDate.Value,
                        OrderString = a.OrderString,
                        OrderStatus = a.OrderStatus,
                        TransactionId = a.TransactionId,
                        UserName=a._UserAd.CustomerName,
                        Phone=a._UserAd.CustomerPhone,
                        UserAddress=a._UserAd.HomeAddress,


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
                    OrderStatus = a.OrderStatus,
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

        public async Task<string> UpdateOrderStatus( int oId)
        {
            try
            {
                var order=await _context.Order.FirstOrDefaultAsync(a=>a.OrderId == oId);

                if(order == null)
                {
                    throw new Exception("Order not found");
                }

                const string? OrderPlaced = "OrderPlaced";
                const string? Delivered = "Delivered";

                if (order.OrderStatus == OrderPlaced)
                {
                    order.OrderStatus = Delivered;  
                }
              

                await _context.SaveChangesAsync();
                return order.OrderStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message);
            }
        }



    }
}
