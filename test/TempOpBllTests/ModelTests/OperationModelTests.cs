using TempOpBll.Models;

namespace TempOpBllTests.ModelTests
{
    public class OperationModelTests
    {

        public static IEnumerable<object[]> OperationToStringTestCases()
        {
            Operation operation1 = new Operation
            {
                Date = new DateTime(2023, 10, 07),
                Amount = 123.45m,
                Label = "Water 2L",
                Quantity = 24,
                Category = "Misc:Food",
                Comment = "Great for tea"
            };

            const string expectedOutput1 = @"D07/10/2023
T-123.45
PWater 2L x24
MMisc:Food - Great for tea
^";

            Operation operation2 = new Operation
            {
                Date = new DateTime(2025, 07, 10),
                Amount = 123.45m,
                Label = "Perfumed Litter",
                Quantity = 8,
                Category = "Misc:Pets",
                Comment = "Il has 7L"
            };

            const string expectedOutput2 = @"D10/07/2025
T-123.45
PPerfumed Litter x8
MMisc:Pets - Il has 7L
^";

            yield return new object[] { operation1, expectedOutput1 };
            yield return new object[] { operation2, expectedOutput2 };
        }

        [Theory]
        [MemberData(nameof(OperationToStringTestCases))]
        public void ToStringShallExportQifCompatibleLine(Operation operation, string expectedOutput)
        {
            //Arrange - n/a

            //Act
            string output = operation.ToQifString();

            //Assert
            Assert.Equal(expectedOutput, output);
        }
    }
}