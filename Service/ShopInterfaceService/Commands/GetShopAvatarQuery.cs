﻿using MediatR;
using System.Collections.Generic;

namespace ShopInterfaceService.Commands
{
    public class GetShopAvatarQuery : IRequest<Dictionary<int, string>>
    {
        public int[] ShopIds { get; set; }
    }
}
