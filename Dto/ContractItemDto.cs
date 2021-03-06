﻿using System;

namespace Dto
{
    public class ContractItemDto
    {
        public int Id { get; set; }
        public Guid ContractUid { get; set; }
        public int TypeValueId { get; set; }
        public decimal Factor { get; set; }
        public decimal? FixValue { get; set; }
        public int TypeSettlementId { get; set; }
    }
}
