﻿using Talabat.Core.Entities;

namespace Talabat.API.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
