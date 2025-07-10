using System.Globalization;

namespace TempOpBll.Models
{
    public class Operation
    {
        #region Properties
        public decimal Amount { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Label { get; set; } = string.Empty;
        public int? Quantity { get; set; } = null;
        #endregion

        /// <summary>
        /// Outputs a string representation of the operation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var qtyText = Quantity.HasValue ? $"{Quantity.Value}× " : "";
            var commentText = string.IsNullOrWhiteSpace(Comment) ? "" : $" ({Comment})";
            return $"{Category} {qtyText}{Label}{commentText} {Amount:0.00}€";
        }

        /// <summary>
        /// Output operation details to QIF format
        /// </summary>
        /// <returns></returns>
        public string ToQifString()
        {
            string date = Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string amount = Amount.ToString("-0.00", CultureInfo.InvariantCulture);
            string label = Quantity.HasValue ? $"{Label} x{Quantity}" : Label;
            string memo = !string.IsNullOrWhiteSpace(Comment)
                ? $"{Category} - {Comment}"
                : Category;

            return $"D{date}{Environment.NewLine}T{amount}{Environment.NewLine}P{label}{Environment.NewLine}M{memo}{Environment.NewLine}^";
        }
    }
}
