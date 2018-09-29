using ConditionCalculator.Model;
using Dto;

namespace ConditionCalculator
{
    public static class CfgMapper
    {
        public static bool IsInitialize { get; set; }

        public static void CreateMaps()
        {

            IsInitialize = true;
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Contract, ContractDto>();
                cfg.CreateMap<ContractItem, ContractItemDto>();
                cfg.CreateMap<OperandTask, OperandTaskDto>();
                cfg.CreateMap<TypeTask, TypeTaskDto>();
                cfg.CreateMap<TypeValue, TypeValueDto>();
                cfg.CreateMap<TypeSettlement, TypeSettlementDto>();

                cfg.CreateMap<ContractDto, Contract>();
                cfg.CreateMap<ContractItemDto, ContractItem>();
                cfg.CreateMap<OperandTaskDto, OperandTask>();
                cfg.CreateMap<TypeTaskDto, TypeTask>();
                cfg.CreateMap<TypeValueDto, TypeValue>();
                cfg.CreateMap<TypeSettlementDto, TypeSettlement>();

            });

        }

    }
}