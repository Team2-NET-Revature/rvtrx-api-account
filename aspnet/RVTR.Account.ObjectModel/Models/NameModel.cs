using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Name_ model
  /// </summary>
  public class NameModel : IValidatableObject
  {
    public int Id { get; set; }

    public string Family { get; set; }

    public string Given { get; set; }

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
