using System.Globalization;
using TempOpBll.Interfaces;
using TempOpBll.Models;

namespace TempOpBll.Exporters
{

    public class QifExporter : IExporter<Operation>
    {
        /// <summary>
        /// Exports the specified operation to a QIF string representation.
        /// </summary>
        /// <param name="input">The operation object to be exported. Cannot be null.</param>
        /// <returns>A string representation of the operation in QIF format.</returns>
        public string Export(Operation op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            var date = op.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            var amount = op.Amount.ToString("-0.00", CultureInfo.InvariantCulture);
            var label = op.Quantity.HasValue ? $"{op.Label} ×{op.Quantity}" : op.Label;
            var memo = !string.IsNullOrWhiteSpace(op.Comment)
                ? $"{op.Category} - {op.Comment}"
                : op.Category;

            return $"D{date}{Environment.NewLine}T{amount}{Environment.NewLine}P{label}{Environment.NewLine}M{memo}{Environment.NewLine}^";
        }
    }
}
