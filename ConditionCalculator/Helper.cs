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
                .OrderBy(x => x.OperandTasks.Min(y => y.TypeTask.Priority))
                .ToList();


        /// <summary>
        /// Все ли условия договора верны утверждению 
        /// </summary>
        /// <param name="contractItem">Утверждение договора</param>
        /// <param name="requestSchemaDto">запрашиваемые данные</param>
        public static bool IsTrue(this ContractItem contractItem, RequestSchemaDto requestSchemaDto) =>
            contractItem.OperandTasks
                .Select(x => x.TypeTask.IsTrue(contractItem.Id, requestSchemaDto))
                .All(allIsTrue => allIsTrue);

        /// <summary>
        /// Верно ли утверждение 
        /// </summary>
        /// <param name="typeTask"></param>
        /// <param name="contractItemId"></param>
        /// <param name="requestSchemaDto"></param>
        /// <returns></returns>
        private static bool IsTrue(this TypeTask typeTask, int contractItemId, RequestSchemaDto requestSchemaDto) =>
            requestSchemaDto.Conditions.Exists(s => string.Equals(s.Key.ToUpper(), typeTask.Name.ToUpper()))
            && typeTask.OperandTasks.Where(x => x.ContractItemId == contractItemId)
                .Any(x => x.Value.ToUpper() == requestSchemaDto
                              .Conditions
                              .Single(s => string.Equals(s.Key.ToUpper(), typeTask.Name.ToUpper()))
                              .Value.ToUpper());
    }
}