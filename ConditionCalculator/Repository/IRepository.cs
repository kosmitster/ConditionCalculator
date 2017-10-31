using System.Collections.Generic;
using Dto;

namespace ConditionCalculator.Repository
{
    public interface IRepository
    {
        void CreateContract(ContractDto contractDto);
        void CreateOperandTask(OperandTaskDto operandTaskDto);
        void CreateOperandValue(OperandValueDto operandValueDto);
        void CreateTypeTask(TypeTaskDto typeTaskDto);
        void CreateTypeValue(TypeValueDto typeValueDto);
        void CreateContractItem(ContractItemDto contractItemDto);
        void CreateRelationship(RelationshipDto relationshipDto);

        List<ResponseSchemeDto> Calculation(List<RequestSchemaDto> requests);

        /*WARNING!!!*/
        void ClearBase();

        List<ContractDto> GetContracts();
        List<TypeTaskDto> GetTypeTasks();
        List<TypeValueDto> GetTypeValues();
        List<ContractItemDto> GetContractItems();
        List<OperandTaskDto> GetOperandTasks();
        List<OperandValueDto> GetOperandValues();
        List<RelationshipDto> GetRelationships();
    }
}
