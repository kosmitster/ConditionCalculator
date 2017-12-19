using System;
using ConditionCalculator.Repository;
using Dto;

namespace UnitTest.PreTest
{
    public static class FillData
    {
        public static void CreateOperandTaskParts(IRepository repository)
        {
            var codeArray = new[]
            {
                "000000423B",
                "000000999",
                "00000423BM",
                "000010006",
                "000010006E",
                "000010007F",
                "000010018C",
                "000010100A",
                "000010227C",
                "000010230A",
                "000010302S",
                "000010685G",
                "000012008",
                "000012009",
                "000012107",
                "000012147",
                "000012147A",
                "000012162",
                "000012277",
                "000012278",
                "000012279",
                "000012279A",
                "000012280",
                "000012280A",
                "000012381",
                "000012499",
                "00001290CON",
                "00001290DUN",
                "00001290EUR",
                "00001290GOD",
                "00001290KLE",
                "00001290MAB",
                "00001290MIC",
                "00001290PIR",
                "000017233",
                "000017770",
                "000017771",
                "000017772",
                "000017819A",
                "000019000",
                "000019001",
                "000019003",
                "000019004",
                "000019005",
                "000019010",
                "000019101",
                "000019220",
                "000019220A",
                "000019221",
                "000019221A",
                "000019222",
                "000019222A",
                "000019223",
                "000019230",
                "000019230A",
                "000050800C 041",
                "000050800D 041",
                "000050800E GXU",
                "000050800F",
                "000050800G YCC",
                "000050800H 041",
                "000050800H C9A",
                "000050800H GQF",
                "000050800H S1A",
                "000050800H S6A",
                "000050800H Z00",
                "000050800J",
                "000050800K"
            };

            foreach (var code in codeArray)
            {
                repository.CreateOperandTask(new OperandTaskDto
                {
                    Key = Guid.NewGuid(),
                    Type = 1,
                    Value = code,
                    ContractItemId = 1
                });
            }
        }

        public static void CreateAllReference()
        {
            IRepository repository = new Repository();

            

            repository.CreateTypeValue(new TypeValueDto
            {
                Id = 1,
                Name = "Fix"
            });
            repository.CreateTypeValue(new TypeValueDto
            {
                Id = 2,
                Name = "Hep"
            });
            repository.CreateTypeValue(new TypeValueDto
            {
                Id = 3,
                Name = "Upe"
            });
            repository.CreateTypeValue(new TypeValueDto
            {
                Id = 4,
                Name = "Whole"
            });
            repository.CreateTypeValue(new TypeValueDto
            {
                Id = 5,
                Name = "Retail"
            });

            repository.CreateTypeSettlement(new TypeSettlementDto
            {
                Id = 1,
                Name = "Услуги"
            });
            repository.CreateTypeSettlement(new TypeSettlementDto
            {
                Id = 2,
                Name = "Товары"
            });

            repository.CreateTypeTask(new TypeTaskDto
            {
                Id = 1,
                Name = "CODE",
                Priority = 1
            });
            repository.CreateTypeTask(new TypeTaskDto
            {
                Id = 2,
                Name = "BRAND",
                Priority = 3
            });
            repository.CreateTypeTask(new TypeTaskDto
            {
                Id = 3,
                Name = "MODEL",
                Priority = 2
            });
            repository.CreateTypeTask(new TypeTaskDto
            {
                Id = 4,
                Name = "VIN",
                Priority = 1
            });
            repository.CreateTypeTask(new TypeTaskDto
            {
                Id = 5,
                Name = "CLASS",
                Priority = 4
            });
            repository.CreateTypeTask(new TypeTaskDto
            {
                Id = 6,
                Name = "ISWARRANTY",
                Priority = 1
            });
        }
    }
}