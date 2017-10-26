using System;

namespace Dto
{
    public class ContractConditionDto
    {
        public int Id { get; set; }
        public int Do { get; set; }
        public bool IsTrueOperantOne { get; set; }
        public Guid OperantOne { get; set; }
        public bool IsTrueOperantTwo { get; set; }
        public Guid OperantTwo { get; set; }
        public int ContractorItemId { get; set; }

    }
}
