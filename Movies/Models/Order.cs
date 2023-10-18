using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Movies.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime DateCreated { get; set; }=DateTime.Now;

        [Required(ErrorMessage ="Total price is required")]
        [Column(TypeName ="decimal(9,2)")]
        public decimal Total { get; set; }

        //Billing
        #region Billing

        [Required(ErrorMessage ="First name is required")]
        [StringLength(100,MinimumLength=2)]
        public string BillingFirstName { get; set; }


        [Required(ErrorMessage ="Last name is required")]
        [StringLength(100,MinimumLength=2)]
        public string BillingLastName { get; set; }

        [Required(ErrorMessage ="Email Address is required")]
        [StringLength(200)]
        [DataType(DataType.EmailAddress,ErrorMessage ="E-mail is not valid")]
        public string BillingEmail { get; set; }

        [Required(ErrorMessage ="Phone number is required")]
        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string BillingPhone { get; set; }

        [Required(ErrorMessage ="Address is required")]
        [StringLength(200)]
        public string BillingAddress { get; set; }

        [Required(ErrorMessage ="City is required")]
        [StringLength(200)]
        public string BillingCity { get; set; }

        [Required(ErrorMessage ="Postal code is required")]
        [StringLength(10)]
        [DataType(DataType.PostalCode)]
        public string BillingPostalCode { get; set; }


        [Required(ErrorMessage ="Country is required")]
        [StringLength(100)]
        public string BillingCountry { get; set; }

        #endregion

        //Shipping
        #region Shipping

        [Required(ErrorMessage ="First name is required")]
        [StringLength(100)]
        public string ShippingFirstName { get; set; }

        [Required(ErrorMessage ="Last name is required")]
        [StringLength(100)]
        public string ShippingLastName { get; set; }

        [Required(ErrorMessage ="Email Address is required")]
        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        public string ShippingEmail { get; set; }

        [Required(ErrorMessage ="Phone number is required")]
        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string ShippingPhone { get; set; }

        [Required(ErrorMessage ="Address is required")]
        [StringLength(200)]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage ="City is required")]
        [StringLength(200)]
        public string ShippingCity { get; set; }

        [Required(ErrorMessage ="Postal code is required")]
        [StringLength(10)]
        [DataType(DataType.PostalCode)]
        public string ShippingPostalCode { get; set; }

        [Required(ErrorMessage ="Country is required")]
        [StringLength(100)]
        public string ShippingCountry { get; set; }

        #endregion

        [AllowNull]
        public string? Message { get; set; }

        public string UserId { get; set; }

        [ForeignKey("OrderId")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
