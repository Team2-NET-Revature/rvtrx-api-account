using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Address_ model
  /// </summary>
  public class AddressModel : IValidatableObject
  {
    public int Id { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string PostalCode { get; set; }

    public string StateProvince { get; set; }

    public string Street { get; set; }

    /// <summary>
    /// Represents the _Address_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
