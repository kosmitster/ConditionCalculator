using System.Collections.Generic;
using Dto;

namespace ConditionCalculator.Repository
{
    public interface IRepository
    {
        void CreateContract(ContractDto contractDto);
        void AddContractItem(ContractItemDto contractItemDto);
        void AddOperandTask(OperandTaskDto operandTaskDto);
        void AddOperandValue(OperandValueDto operandValueDto);
        void CreateVariant(VariantDto variantDto);
        void AddTypeTask(TypeTaskDto typeTaskDto);
        void AddTypeValue(TypeValueDto typeValueDto);
        void CreateContractItem(ContractItemDto contractItemDto);
        void CreateContractCondition(ContractConditionDto contractConditionDto);

        List<ResponseSchemeDto> Calculation(List<RequestSchemaDto> requests);

        /*WARNING!!!*/
        void ClearBase();

        List<ContractDto> GetContracts();
        List<VariantDto> GetVariants();
        List<TypeTaskDto> GetTypeTasks();
        List<TypeValueDto> GetTypeValues();
        List<ContractItemDto> GetContractItems();
        List<OperandTaskDto> GetOperandTasks();
        List<OperandValueDto> GetOperandValues();
        List<ContractConditionDto> GetContractConditions();
    }
}
