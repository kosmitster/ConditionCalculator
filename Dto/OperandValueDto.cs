using System;

namespace Dto
{
    public class OperandValueDto
    {
        public Guid Key { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public int ContractItemId { get; set; }
    }
}
