using System;

namespace Dto
{
    public class ContractItemDto
    {
        public int Id { get; set; }
        public char Type { get; set; }
        public Guid ContractUid { get; set; }
    }
}
