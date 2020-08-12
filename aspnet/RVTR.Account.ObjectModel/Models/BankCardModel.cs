using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _BankCard_ model
  /// </summary>
  public class BankCardModel : IValidatableObject
  {
    public int Id { get; set; }

    public DateTime Expiry { get; set; }

    public string Number { get; set; }

    /// <summary>
    /// Represents the _BankCard_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
