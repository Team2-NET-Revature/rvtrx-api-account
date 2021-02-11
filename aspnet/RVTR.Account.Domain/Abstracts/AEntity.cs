using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.Domain
{
  public abstract class AEntity : IValidatableObject
  {
    public int EntityID { get; set; }

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      throw new System.NotImplementedException();
    }
  }
}
