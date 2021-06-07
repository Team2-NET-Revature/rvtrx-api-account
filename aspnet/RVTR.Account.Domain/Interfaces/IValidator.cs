using System.Net.Http;
using System.Threading.Tasks;

namespace RVTR.Account.Domain.Interfaces
{
  public interface IValidator
  {
    //suppresses message stating we do not use await.
    #pragma warning disable 1998
    internal static async Task<bool> getValidation(string input)
    {
      return false;
    }
    #pragma warning restore 1998
  }
}