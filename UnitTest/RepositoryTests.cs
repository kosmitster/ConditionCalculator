﻿using System;
using System.Collections.Generic;
using System.Linq;
using ConditionCalculator.Repository;
using Dto;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class RepositoryTests
    {

        [SetUp]
        public void TestInit()
        {
            /*Очистка базы данных*/
            IRepository repository = new Repository();
            repository.ClearBase();

            /*Заполняем справочники*/
            PreTest.FillData.CreateAllReference();
        }


        [Test]
        public void CreateContractTest()
        {
            IRepository repository = new Repository();
            repository.CreateContract(new ContractDto
            {
                Uid = Guid.NewGuid(),
                Name = "Договор №1"
            });

            Assert.IsTrue(repository.GetContracts().Any());
        }

        [Test]
        public void CreateTypeTaskTest()
        {
            IRepository repository = new Repository();

            Assert.IsTrue(repository.GetTypeTasks().Any());
        }

        [Test]
        public void CreateTypeValueTest()
        {
            IRepository repository = new Repository();

            Assert.IsTrue(repository.GetTypeValues().Any());
        }

        [Test]
        public void CreateContractItemTest()
        {
            var contractUid = Guid.Parse("b8f4e1e1-dbc0-48fc-a6bf-de4175a250d1");
            IRepository repository = new Repository();

            repository.CreateContract(new ContractDto
            {
                Uid = contractUid,
                Name = "Договор №1"
            });

            repository.CreateContractItem(new ContractItemDto
            {
                Id = 1,
                ContractUid = contractUid,
                TypeSettlementId = 1,
                Factor = 1,
                FixValue = 150,
                TypeValueId = 1
            });

            Assert.IsTrue(repository.GetContractItems().Any());
        }

        [Test]
        public void CreateOperandTaskTest()
        {

            IRepository repository = new Repository();

            var contractUid = Guid.Parse("b8f4e1e1-dbc0-48fc-a6bf-de4175a250d1");

            /*CREATE Договор*/
            repository.CreateContract(new ContractDto
            {
                Uid = contractUid,
                Name = "Договор №1"
            });

            /*CREATE Строка документа*/
            repository.CreateContractItem(new ContractItemDto
            {
                Id = 1,
                ContractUid = contractUid,
                TypeValueId = 1,
                Factor = 1,
                FixValue = 250,
                TypeSettlementId = 1
            });

            var operandTaskUid = Guid.Parse("771152b0-480f-41c8-a395-fd7f830081c7");

            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = operandTaskUid,
                Type = 6,
                Value = "CodeTest",
                ContractItemId = 1                
            });

            Assert.IsTrue(repository.GetOperandTasks().Any());
        }

        [Test]
        public void CreateContractConditionTest()
        {
            var contractUid = Guid.Parse("b8f4e1e1-dbc0-48fc-a6bf-de4175a250d1");
            var operandOneUid = Guid.Parse("828145e9-8cff-4642-af33-b95bf1fef65d");
            var operandTwoUid = Guid.Parse("0e69c042-1a9d-4f7d-8444-ca09ccc73890");
            IRepository repository = new Repository();

            /*CREATE Договор*/
            repository.CreateContract(new ContractDto
            {
                Uid = contractUid,
                Name = "Договор №1"
            });

            /*CREATE Строка документа*/
            repository.CreateContractItem(new ContractItemDto
            {
                Id = 1,
                ContractUid = contractUid,
                TypeValueId = 1,
                Factor = 1,
                FixValue = 250,
                TypeSettlementId = 1
            });

            /*CREATE Тип = CODE*/
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = operandOneUid,
                Type = 1,
                Value = "TestCode",
                ContractItemId = 1
            });
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = operandTwoUid,
                Type = 1,
                Value = "AnotherTestCode",
                ContractItemId = 1
            });

            /*CREATE отношения*/
            repository.CreateRelationship(new RelationshipDto
            {
                Id = 1,
                ContractItemId = 1,
                TaskId = 1,
                IsTrue = true
            });

            Assert.IsTrue(repository.GetRelationships().Any());
        }

        [Test]
        public void CalculationWorkTest()
        {
            Guid contractUid = Guid.Parse("b8f4e1e1-dbc0-48fc-a6bf-de4175a250d1");
            IRepository repository = new Repository();

            /*CREATE Договор*/
            repository.CreateContract(new ContractDto
            {
                Uid = contractUid,
                Name = "Договор №1"
            });
            /*CREATE Строка документа*/
            repository.CreateContractItem(new ContractItemDto
            {
                Id = 1,
                ContractUid = contractUid,
                TypeValueId = 1,
                Factor = 1,
                FixValue = 1500,
                TypeSettlementId = 2
            });

            /*CREATE Тип - ISWARRANTY*/
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = Guid.NewGuid(),
                Type = 6,
                Value = "true",
                ContractItemId = 1
            });

            /*CREATE Тип - BRAND*/
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = Guid.NewGuid(),
                Type = 2,
                Value = "MAZDA",
                ContractItemId = 1
            });
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = Guid.NewGuid(),
                Type = 2,
                Value = "OPEL",
                ContractItemId = 1
            });

            /*CREATE отношения типов*/
            repository.CreateRelationship(new RelationshipDto
            {
                Id = 1,
                ContractItemId = 1,
                TaskId = 6,
                IsTrue = true
            });
            repository.CreateRelationship(new RelationshipDto
            {
                Id = 2,
                ContractItemId = 1,
                TaskId = 2,
                IsTrue = true
            });

            var calculatedValueUid = Guid.Parse("720b2a2d-52e6-4cd3-ab5f-ebe280b4da22");
            var results = repository.Calculation(new List<RequestSchemaDto>
            {
                new RequestSchemaDto
                {
                    Uid = calculatedValueUid,
                    Conditions = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("ISWARRANTY", "true"),
                        new KeyValuePair<string, string>("BRAND", "MAZDA")
                    },
                    Costs = new List<KeyValuePair<string, decimal>>
                    {
                        new KeyValuePair<string, decimal>("default", 2500)
                    }
                }
            });

            Assert.IsTrue(results.Single(s => s.Uid == calculatedValueUid).Result == 1500);
        }

        [Test]
        public void CalculationPartTest()
        {
            Guid contractUid = Guid.Parse("b8f4e1e1-dbc0-48fc-a6bf-de4175a250d1");
            IRepository repository = new Repository();

            /*CREATE Договор*/
            repository.CreateContract(new ContractDto
            {
                Uid = contractUid,
                Name = "Договор №1"
            });
            /*CREATE Строка документа*/
            repository.CreateContractItem(new ContractItemDto
            {
                Id = 1,
                ContractUid = contractUid,
                TypeValueId = 2,
                Factor = new decimal(1.7),
                FixValue = null,
                TypeSettlementId = 2

            });

            /*CREATE Тип - Code*/
            PreTest.FillData.CreateOperandTaskParts(repository);

            /*CREATE Тип - BRAND*/
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = Guid.NewGuid(),
                Type = 2,
                Value = "MAZDA",
                ContractItemId = 1
            });
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = Guid.NewGuid(),
                Type = 2,
                Value = "OPEL",
                ContractItemId = 1
            });

            /*CREATE отношения типов*/
            repository.CreateRelationship(new RelationshipDto
            {
                Id = 1,
                ContractItemId = 1,
                TaskId = 1,
                IsTrue = true
            });
            repository.CreateRelationship(new RelationshipDto
            {
                Id = 2,
                ContractItemId = 1,
                TaskId = 2,
                IsTrue = true
            });

            var calculatedValue1Uid = Guid.NewGuid();
            var calculatedValue2Uid = Guid.NewGuid();
            var calculatedValue3Uid = Guid.NewGuid();

            var results = repository.Calculation(new List<RequestSchemaDto>
            {
                new RequestSchemaDto
                {
                    Uid = calculatedValue1Uid,
                    Conditions = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Code", "000050800C 041"),
                        new KeyValuePair<string, string>("BRAND", "MAZDA")
                    },
                    Costs = new List<KeyValuePair<string, decimal>>
                    {
                        new KeyValuePair<string, decimal>("HEP", new decimal(1256.35)),
                        new KeyValuePair<string, decimal>("default", new decimal(3000.45))
                    }
                },
                new RequestSchemaDto
                {
                    Uid = calculatedValue2Uid,
                    Conditions = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Code", "000010230A"),
                        new KeyValuePair<string, string>("BRAND", "OPEL")
                    },
                    Costs = new List<KeyValuePair<string, decimal>>
                    {
                        new KeyValuePair<string, decimal>("HEP", new decimal(5642.77)),
                        new KeyValuePair<string, decimal>("default", new decimal(6005.45))
                    }
                },
                new RequestSchemaDto
                {
                    Uid = calculatedValue3Uid,
                    Conditions = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Code", "1235gfgfg456"),
                        new KeyValuePair<string, string>("BRAND", "OPEL")
                    },
                    Costs = new List<KeyValuePair<string, decimal>>
                    {
                        new KeyValuePair<string, decimal>("HEP", new decimal(12344.77)),
                        new KeyValuePair<string, decimal>("default", new decimal(10000.55))
                    }
                }
            });

            Assert.IsTrue(results.Single(s => s.Uid == calculatedValue1Uid).Result == new decimal(2135.80) &&
                          results.Single(s => s.Uid == calculatedValue2Uid).Result == new decimal(9592.71) &&
                          results.Single(s => s.Uid == calculatedValue3Uid).Result == new decimal(10000.55));
        }
    }
}