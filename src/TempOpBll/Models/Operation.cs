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
    }
}
