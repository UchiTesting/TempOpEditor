using System.Reflection.Emit;
using System.Xml.Linq;
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
PWater 2L ×24
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
PPerfumed Litter ×8
MMisc:Pets - Il has 7L
^";

            // Operations with zero or negative amounts.

            Operation operation3 = new Operation
            {
                Date = new DateTime(2023, 10, 07),
                Amount = 0m,
                Label = "Water 2L",
                Quantity = 24,
                Category = "Misc:Food",
                Comment = "Great for tea"
            };

            const string expectedOutput3 = @"D07/10/2023
T-0.00
PWater 2L ×24
MMisc:Food - Great for tea
^";

            Operation operation4 = new Operation
            {
                Date = new DateTime(2025, 07, 10),
                Amount = -123.45m,
                Label = "Perfumed Litter",
                Quantity = 8,
                Category = "Misc:Pets",
                Comment = "Il has 7L"
            };

            const string expectedOutput4 = @"D10/07/2025
T--123.45
PPerfumed Litter ×8
MMisc:Pets - Il has 7L
^";
            // Operations with empty or null labels and comments.

            Operation operation5 = new Operation
            {
                Date = new DateTime(2023, 10, 07),
                Amount = 123.45m,
                Label = null,
                Quantity = null,
                Category = "Misc:Food",
                Comment = "Great for tea"
            };

            const string expectedOutput5 = @"D07/10/2023
T-123.45
P
MMisc:Food - Great for tea
^";

            Operation operation6 = new Operation
            {
                Date = new DateTime(2025, 07, 10),
                Amount = 123.45m,
                Label = "Perfumed Litter",
                Quantity = 8,
                Category = "Misc:Pets",
                Comment = null
            };

            const string expectedOutput6 = @"D10/07/2025
T-123.45
PPerfumed Litter ×8
MMisc:Pets
^";

            yield return new object[] { operation1, expectedOutput1 };
            yield return new object[] { operation2, expectedOutput2 };
            yield return new object[] { operation3, expectedOutput3 };
            yield return new object[] { operation4, expectedOutput4 };
            yield return new object[] { operation5, expectedOutput5 };
            yield return new object[] { operation6, expectedOutput6 };
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