using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        //private readonly IGenericRepository<Product> _productsReposity;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryRepo;
        private readonly IUnitofWork _unitofWork;

        public OrderService(IBasketRepository basketRepository,
            //,IGenericRepository<Product> productsReposity,
            //IGenericRepository<DeliveryMethod> deliveryRepo,
            IUnitofWork unitofWork
            )

            
        {
          _basketRepository = basketRepository;
            //_productsReposity = productsReposity;
            //_deliveryRepo = deliveryRepo;
           _unitofWork = unitofWork;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            //Get Basket From Baskets Repo
            var basket = await _basketRepository.GetBasketAsync(basketId);
            // Get Selected Items at Basket From Products
            var orderItems = new List<OrderItem>();
            if(basket?.Items?.Count>0)
            {
                var Repository = _unitofWork.Repository<Product>();
                foreach (var item in basket.Items)
                {
                    var product = await Repository.GetAsync(item.Id);
                    var productItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, item.Quantity, product.Price);
                    orderItems.Add(orderItem);

                }
            }
            //Calculate SubTotal
            var subtotal = orderItems.Sum(orderItem => orderItem.Quantity * orderItem.Price);
            //Get Delivery Method From DeliveryMethods Repo
            var deliveryMethod = await _unitofWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);
            // Create Order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal);
            await _unitofWork.Repository<Order>().AddAsync(order);
            // Save To Database [TODO]
            var result = await _unitofWork.CompleteAsync(); // number of order demand + number of products in Table

            if (result <= 0) return null;

            return order;




         
        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
