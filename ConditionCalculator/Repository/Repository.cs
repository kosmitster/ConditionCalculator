﻿using System.Collections.Generic;
using System.Linq;
using ConditionCalculator.Model;
using Dto;

namespace ConditionCalculator.Repository
{
    public class Repository : IRepository
    {
        public Repository()
        {
            if (!CfgMapper.IsInitialize) CfgMapper.CreateMaps();
        }

        public void CreateContract(ContractDto contractDto)
        {
            var contract = AutoMapper.Mapper.Map<ContractDto, Contract>(contractDto);
            using (var context = new ConditionCalculatorEntities())
            {
                context.Contracts.Add(contract);
                context.SaveChanges();
            }
        }

        public void AddContractItem(ContractItemDto contractItemDto)
        {
            var contractItem = AutoMapper.Mapper.Map<ContractItemDto, ContractItem>(contractItemDto);
            using (var context = new ConditionCalculatorEntities())
            {
                context.ContractItems.Add(contractItem);
                context.SaveChanges();
            }
        }

        public void AddOperandTask(OperandTaskDto operandTaskDto)
        {
            var operandTask = AutoMapper.Mapper.Map<OperandTaskDto, OperandTask>(operandTaskDto);
            using (var context = new ConditionCalculatorEntities())
            {
                context.OperandTasks.Add(operandTask);
                context.SaveChanges();
            }
        }

        public void AddOperandValue(OperandValueDto operandValueDto)
        {
            var operandValue = AutoMapper.Mapper.Map<OperandValueDto, OperandValue>(operandValueDto);
            using (var context = new ConditionCalculatorEntities())
            {
                context.OperandValues.Add(operandValue);
                context.SaveChanges();
            }
        }

        public void CreateVariant(VariantDto variantDto)
        {
            var variant = AutoMapper.Mapper.Map<VariantDto, Variant>(variantDto);
            using (var context = new ConditionCalculatorEntities())
            {
                context.Variants.Add(variant);
                context.SaveChanges();
            }
        }

        public void AddTypeTask(TypeTaskDto typeTaskDto)
        {
            var typeTask = AutoMapper.Mapper.Map<TypeTaskDto, TypeTask>(typeTaskDto);
            using (var context = new ConditionCalculatorEntities())
            {
                context.TypeTasks.Add(typeTask);
                context.SaveChanges();
            }
        }

        public void AddTypeValue(TypeValueDto typeValueDto)
        {
            var typeValue = AutoMapper.Mapper.Map<TypeValueDto, TypeValue>(typeValueDto);
            using (var context = new ConditionCalculatorEntities())
            {
                context.TypeValues.Add(typeValue);
                context.SaveChanges();
            }
        }

        public void CreateContractItem(ContractItemDto contractItemDto)
        {
            var contractItem = AutoMapper.Mapper.Map<ContractItemDto, ContractItem>(contractItemDto);
            using (var context = new ConditionCalculatorEntities())
            {
                context.ContractItems.Add(contractItem);
                context.SaveChanges();
            }
        }

        public void CreateContractCondition(ContractConditionDto contractConditionDto)
        {
            var contractCondition =
                AutoMapper.Mapper.Map<ContractConditionDto, ContractCondition>(contractConditionDto);
            using (var context = new ConditionCalculatorEntities())
            {
                context.ContractConditions.Add(contractCondition);
                context.SaveChanges();
            }
        }

        public List<ResponseSchemeDto> Calculation(List<RequestSchemaDto> requests) => requests.Select(Calculation).ToList();

        private static ResponseSchemeDto Calculation(RequestSchemaDto requestSchemaDto)
        {
            

            if (requestSchemaDto.Costs.Exists(s => s.Key == "Retail"))
            {
                return new ResponseSchemeDto
                {
                    Uid = requestSchemaDto.Uid,
                    Result = requestSchemaDto.Costs.Single(s => s.Key == "Retail").Value
                };
            }

            return new ResponseSchemeDto {Uid = requestSchemaDto.Uid, Result = decimal.Zero};
        }

        public void ClearBase()
        {
            using (var context = new ConditionCalculatorEntities())
            {
                context.ContractConditions.RemoveRange(context.ContractConditions.AsEnumerable());
                context.OperandTasks.RemoveRange(context.OperandTasks.AsEnumerable());
                context.OperandValues.RemoveRange(context.OperandValues.AsEnumerable());
                context.TypeTasks.RemoveRange(context.TypeTasks.AsEnumerable());
                context.TypeValues.RemoveRange(context.TypeValues.AsEnumerable());
                context.Variants.RemoveRange(context.Variants.AsEnumerable());
                context.ContractItems.RemoveRange(context.ContractItems.AsEnumerable());
                context.Contracts.RemoveRange(context.Contracts.AsEnumerable());
                context.SaveChanges();
            }
        }

        public List<ContractDto> GetContracts()
        {
            List<ContractDto> result;
            using (var context = new ConditionCalculatorEntities())
            {
                result = AutoMapper.Mapper.Map<List<Contract>, List<ContractDto>>(context.Contracts.ToList());
            }
            return result;
        }

        public List<VariantDto> GetVariants()
        {
            List<VariantDto> result;
            using (var context = new ConditionCalculatorEntities())
            {
                result = AutoMapper.Mapper.Map<List<Variant>, List<VariantDto>>(context.Variants.ToList());
            }
            return result;
        }

        public List<TypeTaskDto> GetTypeTasks()
        {
            List<TypeTaskDto> result;
            using (var context = new ConditionCalculatorEntities())
            {
                result = AutoMapper.Mapper.Map<List<TypeTask>, List<TypeTaskDto>>(context.TypeTasks.ToList());
            }
            return result;
        }

        public List<TypeValueDto> GetTypeValues()
        {
            List<TypeValueDto> result;
            using (var context = new ConditionCalculatorEntities())
            {
                result = AutoMapper.Mapper.Map<List<TypeValue>, List<TypeValueDto>>(context.TypeValues.ToList());
            }
            return result;
        }

        public List<ContractItemDto> GetContractItems()
        {
            List<ContractItemDto> result;
            using (var context = new ConditionCalculatorEntities())
            {
                result =
                    AutoMapper.Mapper.Map<List<ContractItem>, List<ContractItemDto>>(context.ContractItems.ToList());
            }
            return result;

        }

        public List<OperandTaskDto> GetOperandTasks()
        {
            List<OperandTaskDto> result;
            using (var context = new ConditionCalculatorEntities())
            {
                result =
                    AutoMapper.Mapper.Map<List<OperandTask>, List<OperandTaskDto>>(context.OperandTasks.ToList());
            }
            return result;
        }

        public List<OperandValueDto> GetOperandValues()
        {
            List<OperandValueDto> result;
            using (var context = new ConditionCalculatorEntities())
            {
                result =
                    AutoMapper.Mapper.Map<List<OperandValue>, List<OperandValueDto>>(context.OperandValues.ToList());
            }
            return result;
        }

        public List<ContractConditionDto> GetContractConditions()
        {
            List<ContractConditionDto> result;
            using (var context = new ConditionCalculatorEntities())
            {
                result =
                    AutoMapper.Mapper.Map<List<ContractCondition>, List<ContractConditionDto>>(context.ContractConditions.ToList());
            }
            return result;

        }
    }
}