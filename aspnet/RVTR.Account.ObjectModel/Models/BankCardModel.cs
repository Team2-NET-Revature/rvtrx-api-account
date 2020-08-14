using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Account.ObjectModel.Models
{
    /// <summary>
    /// Represents the _BankCard_ model
    /// </summary>
    public class BankCardModel : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Expiry { get; set; }

        //public string Number { get; set; }
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

        /// <summary>
        /// Represents the _BankCard_ `Validate` method
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
    }
}
