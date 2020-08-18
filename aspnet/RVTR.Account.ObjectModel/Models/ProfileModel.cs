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

<<<<<<< HEAD
        private string _family;
=======
        private string _familyname;
>>>>>>> 04b11969a93a3132793562777ab489aa08685024
        public string familyName
        {
          get => _familyname;
          set
          {
            if (string.IsNullOrEmpty(value))
            {
              throw new ArgumentException("First name cannot be null.", nameof(value));
            }
            _familyname = value;
          }
        }

        
<<<<<<< HEAD
        private string _given;
=======
        private string _givenname;
>>>>>>> 04b11969a93a3132793562777ab489aa08685024
        public string givenName
        {
          get => _givenname;
          set
          {
            if (string.IsNullOrEmpty(value))
            {
              throw new ArgumentException("Last name cannot be null.", nameof(value));
            }
            _givenname = value;
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
        public string Type;

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
