namespace E_Learning.Areas.Payment.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserId { get; set; }  // User making the payment

        [Required]
        [StringLength(100)]
        public string PaymentStatus { get; set; }  // Pending, Completed, Failed

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }  // Total amount of the payment

        [Required]
        [StringLength(10)]
        public string Currency { get; set; }  // Currency code, e.g., "USD", "EUR"

        [Required]
        [StringLength(255)]
        public string Description { get; set; }  // Payment description

        [Required]
        public DateTime CreatedAt { get; set; }  // Payment creation date/time

        public DateTime? CompletedAt { get; set; }  // Payment completion date/time (nullable)

        [StringLength(255)]
        public string PayPalPaymentId { get; set; }  // The PayPal Payment ID

        [StringLength(255)]
        public string PayerId { get; set; }  // PayPal payer ID

        public string InvoiceNumber { get; set; }  // Unique invoice number for the transaction
    }

}
