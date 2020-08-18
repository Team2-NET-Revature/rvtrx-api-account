using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Account.ObjectModel.Models
{
    /// <summary>
    /// Represents the _Profile_ model
    /// </summary>
    public class ProfileModel : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Email address cannot be null.", nameof(value));
                }
                _email = value;
            }
        }

        private string _family;
        public string familyName
        {
          get => _family;
          set
          {
            if (string.IsNullOrEmpty(value))
            {
              throw new ArgumentException("First name cannot be null.", nameof(value));
            }
            _family = value;
          }
        }

        
        private string _given;
        public string givenName
        {
          get => _given;
          set
          {
            if (string.IsNullOrEmpty(value))
            {
              throw new ArgumentException("Last name cannot be null.", nameof(value));
            }
            _given = value;
          }
        }


        
        private string _phone;
        public string Phone
        {
          get => _phone;
          set
          {
            if (string.IsNullOrEmpty(value))
            {
              throw new ArgumentException("Phone number cannot be null.", nameof(value));
            }
            _phone = value;
          }
        }

        [ForeignKey("Account")]
        [Required]
        public int? AccountId { get; set; }

        
        public AccountModel Account { get; set; }

        /// <summary>
        /// Represents the _Profile_ `Validate` method
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
    }
}
