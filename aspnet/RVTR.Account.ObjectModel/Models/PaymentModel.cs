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

    [Required]
    public DateTime CardExpirationDate { get; set; }

    [Required(ErrorMessage = "Card number required")]
    [StringLength(16)]
    [RegularExpression(@"[0-9]{16}", ErrorMessage = "Credit card must be properly formatted")]
    public string CardNumber { get; set; }

    [Required(ErrorMessage = "Security code required")]
    [StringLength(3)]
    [RegularExpression(@"[0-9]{3}", ErrorMessage = "Security code must be properly formatted")]
    public string SecurityCode { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only use letters.")]
    public string CardName { get; set; }

    public int AccountId { get; set; }

    [Required(ErrorMessage = "Account is required")]
    public AccountModel Account { get; set; }


    /// <summary>
    /// Represents the _Payment_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(CardName))
      {
        yield return new ValidationResult("cardName cannot be null.");
      }
      if (string.IsNullOrEmpty(CardNumber))
      {
        yield return new ValidationResult("cardNumber cannot be null.");
      }
    }
  }
}
