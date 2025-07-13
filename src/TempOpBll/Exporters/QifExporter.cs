using System.Globalization;
using System.Text;
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

        /// <summary>
        /// Exports all operations to a formatted string using the specified exporter.
        /// </summary>
        /// <remarks>The returned string starts with a header line indicating the type of export
        /// ("!Type:Cash") followed by the exported representation of each operation. The format of each operation in
        /// the output depends on the implementation of the provided <paramref name="exporter"/>.</remarks>
        /// <param name="operations">A collection of operations to be exported. Cannot be null.</param>
        /// <param name="exporter">The exporter used to convert each operation to its string representation. Cannot be null.</param>
        /// <returns>A formatted string containing the exported operations. The string begins with a header and includes the
        /// exported representation of each operation.</returns>
        public string ExportAll(IEnumerable<Operation> operations, IExporter<Operation> exporter)
        {
            if (operations == null) throw new ArgumentNullException(nameof(operations));
            if (exporter == null) throw new ArgumentNullException(nameof(exporter));

            var sb = new StringBuilder();
            sb.AppendLine("!Type:Cash");

            foreach (var op in operations)
            {
                sb.AppendLine(exporter.Export(op));
            }

            return sb.ToString();
        }
    }
}
