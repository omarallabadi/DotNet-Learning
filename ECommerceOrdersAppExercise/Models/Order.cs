using System.ComponentModel.DataAnnotations;

namespace ECommerceOrdersAppExercise.Models
{
    public class Order : IValidatableObject
    {
        public int? OrderNo { get; set; }

        [Required(ErrorMessage = "OrderDate can't be blank")]
        public DateTime? OrderDate { get; set; }

        public List<Product> Products { get; set; }

        [Required(ErrorMessage = "invoice price can't be blank")]

        public double? InvoicePrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            double total = Products.Sum(p => p.Quantity * p.Price);
            if (total != (double)InvoicePrice)
            {
                yield return new ValidationResult("InvoicePrice doesn't match with the total cost of the specified products in the order.", new[] { nameof(InvoicePrice) });
            }
            if (OrderDate < new DateTime(2000, 1, 1))
            {
                yield return new ValidationResult(
                    "OrderDate should be greater than or equal to 2000-01-01",
                    new[] { nameof(OrderDate) });
            }
            if (Products == null || Products.Count == 0)
            {
                yield return new ValidationResult(
                    "At least one product is required",
                    new[] { nameof(Products) });
            }

        }




        public override string ToString()
        {
            return $"OrderDate={OrderDate}&InvoicePrice={InvoicePrice}&Products[0].ProductCode={Products[0].ProductCode}&Products[0].Price={Products[0].Price}&Products[0].Quantity={Products[0].Quantity}&Products[1].ProductCode={Products[1].ProductCode}&Products[1].Price={Products[1].Price}&Products[1].Quantity={Products[1].Quantity}";
        }


    }
}
