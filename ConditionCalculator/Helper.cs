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
        public static int GetWeight(this ContractItem contractItem) => contractItem.Relationships.Aggregate(0,
            (current, condition) => current + condition.TypeTask.Priority);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static List<ContractItem> SortByWeight(this Contract contract) => contract.ContractItems
            .OrderBy(s => s.GetWeight())
            .ToList();


        /// <summary>
        /// Верно ли условие?
        /// </summary>
        /// <param name="contractItem">условие</param>
        /// <param name="requestSchemaDto">запрашиваемые данные</param>
        public static bool IsTrue(this ContractItem contractItem, RequestSchemaDto requestSchemaDto) =>
            contractItem.Relationships.OrderBy(s => s.Id)
                .Select(relationship => relationship.IsTrue
                    ? relationship.TypeTask.IsTrue(requestSchemaDto)
                    : !relationship.TypeTask.IsTrue(requestSchemaDto))
                .All(allIsTrue => allIsTrue);

        public static bool IsTrue(this TypeTask typeTask, RequestSchemaDto requestSchemaDto) =>
            requestSchemaDto.Conditions.Exists(s => s.Key.ToUpper() == typeTask.Name.ToUpper())
            && typeTask.OperandTasks.Any(x => x.Value == requestSchemaDto
                                                  .Conditions
                                                  .Single(s => s.Key.ToUpper() == typeTask.Name.ToUpper())
                                                  .Value);
    }
}