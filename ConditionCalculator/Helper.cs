using System.Collections.Generic;
using System.Linq;
using ConditionCalculator.Model;
using Dto;

namespace ConditionCalculator
{
    public static class Helper
    {
        public static int GetWeight(this ContractItem contractItem)
        {
            var x = 0;
            foreach (var condition in contractItem.ContractConditions)
            {
                x = x + condition.OperandOne.TypeTask.Priority;
                x = x + condition.OperandTwo.TypeTask.Priority;
            }
            return x;
        }

        public static List<ContractItem> SortByWeight(this Contract contract)
        {
            return contract.ContractItems.OrderBy(s => s.GetWeight()).ToList();
        }

        /// <summary>
        /// Верно ли условие?
        /// </summary>
        /// <param name="contractItem">условие</param>
        /// <param name="requestSchemaDto">запрашиваемые данные</param>
        public static bool IsTrue(this ContractItem contractItem, RequestSchemaDto requestSchemaDto)            
        {
            


            foreach (var contractCondition in contractItem.ContractConditions.OrderBy(s => s.Id))
            {
                

                //В зависимости от логического условия 
                switch (contractCondition.Do)
                {
                    // И
                    case 1:
                        if (contractCondition.OperandOne.IsTrue(requestSchemaDto) &&
                            contractCondition.OperandTwo.IsTrue(requestSchemaDto))
                        {
                            return true;
                        }
                        break;
                    // ИЛИ
                    case 2:
                        if (contractCondition.OperandOne.IsTrue(requestSchemaDto) ||
                            contractCondition.OperandTwo.IsTrue(requestSchemaDto))
                        {
                            return true;
                        }
                        break;
                }
            }
            return true;
        }

        public static bool IsTrue(this OperandTask operandTask, RequestSchemaDto requestSchemaDto)
        {
            var result = requestSchemaDto.Conditions.Exists(s => s.Key.ToUpper() == operandTask.TypeTask.Name.ToUpper())
                   && requestSchemaDto
                       .Conditions.Single(s => s.Key.ToUpper() == operandTask.TypeTask.Name.ToUpper())
                       .Value == operandTask.Value;
            return result;
        }
    }
}
