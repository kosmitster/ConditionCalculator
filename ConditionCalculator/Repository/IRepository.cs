using System.Collections.Generic;
using Dto;

namespace ConditionCalculator.Repository
{
    public interface IRepository
    {
        void CreateContract(ContractDto contractDto);
        void CreateOperandTask(OperandTaskDto operandTaskDto);
        void CreateTypeTask(TypeTaskDto typeTaskDto);
        void CreateTypeValue(TypeValueDto typeValueDto);
        void CreateContractItem(ContractItemDto contractItemDto);
        void CreateTypeSettlement(TypeSettlementDto typeSettlementDto);

        List<ResponseSchemeDto> Calculation(List<RequestSchemaDto> requests);

        /*WARNING!!!*/
        void ClearBase();

        List<ContractDto> GetContracts();
        List<TypeTaskDto> GetTypeTasks();
        List<TypeValueDto> GetTypeValues();
        List<ContractItemDto> GetContractItems();
        List<OperandTaskDto> GetOperandTasks();
        
    }
}
