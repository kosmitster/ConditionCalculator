using System.Collections.Generic;
using System.Linq;
using ConditionCalculator.Model;
using Dto;

namespace ConditionCalculator
{
    public static class Helper
    {
        /// <summary>
        /// Сортируй условие договора по весу каждого члена
        /// </summary>
        /// <param name="contractItem"></param>
        /// <returns></returns>
        public static int GetWeight(this ContractItem contractItem) => 
            contractItem.Relationships.Aggregate(0, (current, condition) => current + condition.TypeTask.Priority);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="requestSchemaDto"></param>
        /// <returns></returns>
        public static List<ContractItem> SortByWeight(this Contract contract, RequestSchemaDto requestSchemaDto) =>
            contract.ContractItems.Where(s => s.TypeSettlement.Name == requestSchemaDto.TypeSettlement)
                .OrderBy(s => s.GetWeight())
                .ToList();


        /// <summary>
        /// Верно ли условие?
        /// </summary>
        /// <param name="contractItem">условие</param>
        /// <param name="requestSchemaDto">запрашиваемые данные</param>
        public static bool IsTrue(this ContractItem contractItem, RequestSchemaDto requestSchemaDto) =>
            contractItem.Relationships.Any() && contractItem.Relationships.OrderBy(s => s.Id)
                .Select(relationship => relationship.IsTrue
                    ? relationship.TypeTask.IsTrue(contractItem.Id, requestSchemaDto)
                    : !relationship.TypeTask.IsTrue(contractItem.Id, requestSchemaDto))
                .All(allIsTrue => allIsTrue);

        public static bool IsTrue(this TypeTask typeTask, int contractItemId ,RequestSchemaDto requestSchemaDto) =>
            requestSchemaDto.Conditions.Exists(s => string.Equals(s.Key.ToUpper(), typeTask.Name.ToUpper()))
            && typeTask.OperandTasks.Where(x => x.ContractItemId == contractItemId).Any(x => x.Value.ToUpper() == requestSchemaDto
                                                  .Conditions
                                                  .Single(s => string.Equals(s.Key.ToUpper(), typeTask.Name.ToUpper()))
                                                  .Value.ToUpper());
    }
}