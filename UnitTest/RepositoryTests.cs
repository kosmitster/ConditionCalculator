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
        public void CreateVariantTest()
        {
            IRepository repository = new Repository();
            repository.CreateVariant(new VariantDto
            {
                Id = 1,
                Name = "AND"
            });
            repository.CreateVariant(new VariantDto
            {
                Id = 2,
                Name = "OR"
            });
            Assert.IsTrue(repository.GetVariants().Any());
        }

        [Test]
        public void CreateTypeTaskTest()
        {
            IRepository repository = new Repository();
            repository.AddTypeTask(new TypeTaskDto
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
            repository.AddTypeValue(new TypeValueDto
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

            repository.AddTypeTask(new TypeTaskDto
            {
                Id = 6,
                Name = "CODE",
                Priority = 1
            });

            repository.AddOperandTask(new OperandTaskDto
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

            repository.AddTypeValue(new TypeValueDto
            {
                Id = 1,
                Name = "Fix"
            });
            repository.AddOperandValue(new OperandValueDto
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
            repository.AddTypeTask(new TypeTaskDto
            {
                Id = 1,
                Name = "CODE",
                Priority = 1
            });
            repository.AddOperandTask(new OperandTaskDto
            {
                Key = operandOneUid,
                Type = 1,
                Value = "TestCode"
            });
            repository.AddOperandTask(new OperandTaskDto
            {
                Key = operandTwoUid,
                Type = 1,
                Value = "AnotherTestCode"
            });
            repository.CreateVariant(new VariantDto
            {
                Id = 1,
                Name = "AND"
            });
            repository.CreateVariant(new VariantDto
            {
                Id = 2,
                Name = "OR"
            });

            repository.CreateContractCondition(new ContractConditionDto
            {
                Id = 1,
                ContractorItemId = 1,
                Do = 2,
                IsTrueOperantOne = true,
                OperantOne = operandOneUid,
                IsTrueOperantTwo = true,
                OperantTwo = operandTwoUid
            });

            Assert.IsTrue(repository.GetContractConditions().Any());
        }

        [Test]
        public void CalculationTest()
        {
            Guid contractUid = Guid.Parse("b8f4e1e1-dbc0-48fc-a6bf-de4175a250d1");
            Guid operandOneUid = Guid.Parse("828145e9-8cff-4642-af33-b95bf1fef65d");
            Guid operandTwoUid = Guid.Parse("0e69c042-1a9d-4f7d-8444-ca09ccc73890");
            Guid operandValueUid = Guid.Parse("0fd6a38f-2eb3-4a52-960a-25f3d52bcb2b");
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
            repository.AddTypeTask(new TypeTaskDto
            {
                Id = 1,
                Name = "CODE",
                Priority = 1
            });
            repository.AddOperandTask(new OperandTaskDto
            {
                Key = operandOneUid,
                Type = 1,
                Value = "TestCode"
            });
            repository.AddOperandTask(new OperandTaskDto
            {
                Key = operandTwoUid,
                Type = 1,
                Value = "AnotherTestCode"
            });
            repository.CreateVariant(new VariantDto
            {
                Id = 1,
                Name = "AND"
            });
            repository.CreateVariant(new VariantDto
            {
                Id = 2,
                Name = "OR"
            });

            repository.CreateContractCondition(new ContractConditionDto
            {
                Id = 1,
                ContractorItemId = 1,
                Do = 2,
                IsTrueOperantOne = true,
                OperantOne = operandOneUid,
                IsTrueOperantTwo = true,
                OperantTwo = operandTwoUid
            });

            repository.AddTypeValue(new TypeValueDto
            {
                Id = 1,
                Name = "Fix"
            });
            repository.AddOperandValue(new OperandValueDto
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
                        new KeyValuePair<string, string>("Code", "AnotherTestCode")  
                    },
                    Costs = new List<KeyValuePair<string, decimal>>
                    {
                        new KeyValuePair<string, decimal>("Retail", 2500),
                        new KeyValuePair<string, decimal>("fix", 1500)
                    }
                }
            });

            Assert.IsTrue(results.Exists(s => s.Uid == calculatedValueUid));
        }
    }
}