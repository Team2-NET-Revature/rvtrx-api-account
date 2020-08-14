using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Account.ObjectModel.Models
{
    /// <summary>
    /// Represents the _Name_ model
    /// </summary>
    public class NameModel : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //public string Family { get; set; }
        private string _family;
        public string Family
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

        //public string Given { get; set; }
        private string _given;
        public string Given
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

        public int? ProfileId { get; set; }

        public ProfileModel Profile { get; set; }

        /// <summary>
        /// Represents the _Name_ `Validate` method
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
    }
}
