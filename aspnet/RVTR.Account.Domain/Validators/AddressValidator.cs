using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RVTR.Account.Domain.Interfaces;

namespace RVTR.Account.Domain.Validators
{
  public class AddressValidator : IValidator
  {
    static readonly HttpClient client;
    public string address;

    static async Task<bool> getValidation()
    {
      try
      {
        HttpResponseMessage response = await client.GetAsync("https://maps.googleapis.com/maps/api/geocode/json?address={address}&key=AIzaSyDbnHwx_6BDq2gcBfETYjbvVn-Y0QUa6mo");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseBody);
        dynamic result = JObject.Parse(responseBody);
        string resultStatus = result.status;
        List<string> types = result.results.types;
        List<string> acceptedTypes = new List<string>();
        acceptedTypes.Add("subpremise");
        acceptedTypes.Add("street_address");
        if (resultStatus == "OK")
        {
          foreach (var type in acceptedTypes)
          {
            if (types.Contains(type))
            {
              return true;
            }
          }
          return false;
        }
      }
      catch (HttpRequestException e)
      {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
        return false;
      }
      return false;
    }
  }
}