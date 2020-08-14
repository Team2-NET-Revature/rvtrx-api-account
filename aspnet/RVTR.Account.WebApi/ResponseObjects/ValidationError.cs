using System;
namespace RVTR.Account.WebApi.ResponseObjects
{
  public class ValidationError : ErrorObject
  {
    public ValidationError(ArgumentException e) : base (e.Message)
    {
    }
  }
}