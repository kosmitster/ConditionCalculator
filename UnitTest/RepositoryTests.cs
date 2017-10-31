using System;
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
            repository.CreateTypeTask(new TypeTaskDto
            {
                Id = 1,
                Name = "CODE",
                Priority = 1
            });
            Assert.IsTrue(repository.GetTypeTasks().Any());
        }

        [Test]
        public void CreateTypeValueTest()
        {
            IRepository repository = new Repository();
            repository.CreateTypeValue(new TypeValueDto
            {
                Id = 1,
                Name = "Fix"
            });
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
                Type = Convert.ToChar("Р")
            });

            Assert.IsTrue(repository.GetContractItems().Any());
        }

        [Test]
        public void CreateOperandTaskTest()
        {
            var operandTaskUid = Guid.Parse("771152b0-480f-41c8-a395-fd7f830081c7");
            IRepository repository = new Repository();

            repository.CreateTypeTask(new TypeTaskDto
            {
                Id = 6,
                Name = "CODE",
                Priority = 1
            });

            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = operandTaskUid,
                Type = 6,
                Value = "CodeTest"
            });

            Assert.IsTrue(repository.GetOperandTasks().Any());
        }

        [Test]
        public void CreateOperandValueTest()
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
                Type = Convert.ToChar("Р")
            });

            repository.CreateTypeValue(new TypeValueDto
            {
                Id = 1,
                Name = "Fix"
            });
            repository.CreateOperandValue(new OperandValueDto
            {
                Key = Guid.NewGuid(),
                Type = 1,
                Value = "1920",
                ContractItemId = 1
            });

            Assert.IsTrue(repository.GetOperandValues().Any());
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
                Type = Convert.ToChar("Р")
            });
            /*CREATE Типы*/
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
                Priority = 1
            });

            /*CREATE Тип = CODE*/
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = operandOneUid,
                Type = 1,
                Value = "TestCode"
            });
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = operandTwoUid,
                Type = 1,
                Value = "AnotherTestCode"
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
        public void CalculationTest()
        {
            Guid contractUid = Guid.Parse("b8f4e1e1-dbc0-48fc-a6bf-de4175a250d1");
            Guid operandValueUid = Guid.Parse("0fd6a38f-2eb3-4a52-960a-25f3d52bcb2b");
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
                Type = Convert.ToChar("Р")
            });
            /*CREATE Типы*/
            repository.CreateTypeTask(new TypeTaskDto
            {
                Id = 1,
                Name = "ISWARRANTY",
                Priority = 1
            });
            repository.CreateTypeTask(new TypeTaskDto
            {
                Id = 2,
                Name = "BRAND",
                Priority = 1
            });

            /*CREATE Тип - ISWARRANTY*/
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = Guid.NewGuid(),
                Type = 1,
                Value = "true"
            });

            /*CREATE Тип - BRAND*/
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = Guid.NewGuid(),
                Type = 2,
                Value = "MAZDA"
            });
            repository.CreateOperandTask(new OperandTaskDto
            {
                Key = Guid.NewGuid(),
                Type = 2,
                Value = "OPEL"
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

            /*CREATE результат операции*/
            repository.CreateTypeValue(new TypeValueDto
            {
                Id = 1,
                Name = "Fix"
            });
            repository.CreateOperandValue(new OperandValueDto
            {
                Key = operandValueUid,
                Type = 1,
                Value = "1920",
                ContractItemId = 1
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
                        new KeyValuePair<string, decimal>("Retail", 2500),
                        new KeyValuePair<string, decimal>("fix", 1500)
                    }
                }
            });

            Assert.IsTrue(results.Single(s => s.Uid == calculatedValueUid).Result == 1500);
            //Assert.IsTrue(results.Exists(s => s.Uid == calculatedValueUid));
        }
    }
}