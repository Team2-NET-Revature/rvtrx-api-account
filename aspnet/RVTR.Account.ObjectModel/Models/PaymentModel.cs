using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Account.ObjectModel.Models
{
    /// <summary>
    /// Represents the _Payment_ model
    /// </summary>
    public class PaymentModel : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       public DateTime Expiry { get; set; }

        
        private string _number;
        public string Number
        {
            get => _number;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Credit card number cannot be null.", nameof(value));
                }
                _number = value;
            }
        }
        
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Bank name cannot be null.", nameof(value));
                }
                _name = value;
            }
        }

        [ForeignKey("Account")]
        [Required]
        public int? AccountId { get; set; }

        public AccountModel Account { get; set; }

        /// <summary>
        /// Represents the _Payment_ `Validate` method
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
    }
}
