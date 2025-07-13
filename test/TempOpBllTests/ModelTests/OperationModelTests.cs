using System.Globalization;
using TempOpBll.Exporters;
using TempOpBll.Interfaces;
using TempOpBll.Models;

namespace TempOpBllTests.ModelTests
{
    public class OperationModelTests
    {
        #region Sample Data
        static Operation operation1 = new Operation
        {
            Date = new DateTime(2025, 07, 10),
            Amount = 123.45m,
            Label = "Water 2L",
            Quantity = 24,
            Category = "Misc:Food",
            Comment = "Great for tea"
        };


        static Operation operation2 = new Operation
        {
            Date = new DateTime(2025, 07, 10),
            Amount = 123.45m,
            Label = "Perfumed Litter",
            Quantity = 8,
            Category = "Misc:Pets",
            Comment = "Il has 7L"
        };

        // Operations with zero or negative amounts.

        static Operation operation3 = new Operation
        {
            Date = new DateTime(2025, 07, 10),
            Amount = 0m,
            Label = "Water 2L",
            Quantity = 24,
            Category = "Misc:Food",
            Comment = "Great for tea"
        };

        static Operation operation4 = new Operation
        {
            Date = new DateTime(2025, 07, 10),
            Amount = -123.45m,
            Label = "Perfumed Litter",
            Quantity = 8,
            Category = "Misc:Pets",
            Comment = "Il has 7L"
        };

        // Operations with empty or null labels and comments.

        static Operation operation5 = new Operation
        {
            Date = new DateTime(2025, 07, 10),
            Amount = 123.45m,
            Label = null,
            Quantity = null,
            Category = "Misc:Food",
            Comment = "Great for tea"
        };

        static Operation operation6 = new Operation
        {
            Date = new DateTime(2025, 07, 10),
            Amount = 123.45m,
            Label = "Perfumed Litter",
            Quantity = 8,
            Category = "Misc:Pets",
            Comment = null
        };


        // Expected outputs for the operations in QIF format.

        const string expectedOutput1 = @"D10/07/2025
T-123.45
PWater 2L ×24
MMisc:Food - Great for tea
^";

        const string expectedOutput2 = @"D10/07/2025
T-123.45
PPerfumed Litter ×8
MMisc:Pets - Il has 7L
^";

        const string expectedOutput3 = @"D10/07/2025
T-0.00
PWater 2L ×24
MMisc:Food - Great for tea
^";

        const string expectedOutput4 = @"D10/07/2025
T--123.45
PPerfumed Litter ×8
MMisc:Pets - Il has 7L
^";


        const string expectedOutput5 = @"D10/07/2025
T-123.45
P
MMisc:Food - Great for tea
^";

        const string expectedOutput6 = @"D10/07/2025
T-123.45
PPerfumed Litter ×8
MMisc:Pets
^";

        #endregion

        public static IEnumerable<object[]> OperationToStringTestCases()
        {

            yield return new object[] { operation1, expectedOutput1 };
            yield return new object[] { operation2, expectedOutput2 };
            yield return new object[] { operation3, expectedOutput3 };
            yield return new object[] { operation4, expectedOutput4 };
            yield return new object[] { operation5, expectedOutput5 };
            yield return new object[] { operation6, expectedOutput6 };
        }

        [Theory]
        [MemberData(nameof(OperationToStringTestCases))]
        public void ToQifString_ShouldReturnQifFormattedOutput(Operation operation, string expectedOutput)
        {
            //Arrange - n/a
            IExporter<Operation> exporter = new QifExporter();

            //Act
            string output = exporter.Export(operation);

            //Assert
            Assert.Equal(expectedOutput, output);
        }

        [Fact]
        public void ToQifString_ShouldUseInvariantCulture_ForAmountFormatting()
        {
            // Arrange
            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

            var op = new Operation
            {
                Date = new DateTime(2023, 10, 7),
                Amount = 123.45m,
                Label = "Test",
                Category = "Test",
            };

            IExporter<Operation> exporter = new QifExporter();

            // Act
            string result = exporter.Export(op);

            // Assert
            Assert.Contains("T-123.45", result);
        }
        //*
        [Fact]
        public void ExportAll_ShouldExportMultipleOperationsWithHeader()
        {
            // Arrange
            var ops = new List<Operation>
            {
                operation1,
                operation2,
                operation3,
                operation4,
                operation5,
                operation6
            };

            IExporter<Operation> exporter = new QifExporter();
            // Act
            var result = exporter.ExportAll(ops, exporter);

            // Assert
            Assert.StartsWith("!Type:Cash", result);
            Assert.Equal(6, result.Split('^').Length - 1); // Deux blocs
            Assert.Contains("P", result);
            Assert.Contains("T", result);
            //*/
        }//*/
    }
}
