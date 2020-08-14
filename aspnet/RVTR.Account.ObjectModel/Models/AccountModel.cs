using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Account.ObjectModel.Models
{
    /// <summary>
    /// Represents the _Account_ model
    /// </summary>
    public class AccountModel : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public AddressModel Address { get; set; }

        //public string Name { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Account name cannot be null.", nameof(value));
                }
                _name = value;
            }
        }

        public IEnumerable<PaymentModel> Payments { get; set; }

        public IEnumerable<ProfileModel> Profiles { get; set; }

        /// <summary>
        /// Represents the _Account_ `Validate` method
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
    }
}
