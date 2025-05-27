using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Order_Aggregate;


namespace Talabat.API.Dtos
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        public AddressDTo  ShippingAddress { get; set; }
    }
}
