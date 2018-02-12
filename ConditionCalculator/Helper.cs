using System.Collections.Generic;
using System.Linq;
using ConditionCalculator.Model;
using Dto;

namespace ConditionCalculator
{
    public static class Helper
    {
        /// <summary>
        /// Сортировка строк договора по приоритету
        /// </summary>
        /// <param name="contract">Договор</param>
        /// <param name="requestSchemaDto">Схема</param>
        /// <returns>Отсортированный список строк договора</returns>
        public static List<ContractItem> SortByWeight(this Contract contract, RequestSchemaDto requestSchemaDto) =>
            contract.ContractItems.Where(s => s.TypeSettlement.Name == requestSchemaDto.TypeSettlement)
                .OrderBy(x => x.Relationships.Min(s => s.TypeTask.Priority))
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