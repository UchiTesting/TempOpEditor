namespace TempOpBll.Models
{
    public class Operation
    {
        public string Label { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int? Quantity { get; set; } = null;

        public override string ToString()
        {
            var qtyText = Quantity.HasValue ? $"{Quantity.Value}× " : "";
            var commentText = string.IsNullOrWhiteSpace(Comment) ? "" : $" ({Comment})";
            return $"{Category} {qtyText}{Label}{commentText} {Amount:0.00}€";
        }
    }
}
