using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Payment_ model
  /// </summary>
  public class PaymentModel : IValidatableObject
  {
    public int Id { get; set; }

    public DateTime cardExpirationDate { get; set; }

    public string cardNumber { get; set; }

    public string securityCode { get; set; }

    public string cardName { get; set; }

    public int AccountId { get; set; }
    public AccountModel Account { get; set; }

    /// <summary>
    /// Represents the _Payment_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(cardName))
      {
        yield return new ValidationResult("cardName cannot be null.");
      }
      if (string.IsNullOrEmpty(cardNumber))
      {
        yield return new ValidationResult("cardNumber cannot be null.");
      }
    }
  }
}
