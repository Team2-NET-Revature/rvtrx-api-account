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

    internal static async Task<bool> getValidation(string address, HttpClient client)
    {
      try
      {
        var geoKey = Environment.GetEnvironmentVariable("GOOGLE_GEOCODE_API_KEY");
        HttpResponseMessage response = await client.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={geoKey}");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseBody);
        JObject result = JObject.Parse(responseBody);
        List<string> acceptedTypes = new List<string>();
        acceptedTypes.Add("subpremise");
        acceptedTypes.Add("street_address");
      
        JToken resultStatus = result.GetValue("status");
        if(resultStatus.ToString().Contains("OK"))
        {
          JToken resultType = result.GetValue("results");
          Console.WriteLine($"result type: {resultType.ToString()}");
          if(resultType.ToString().Contains("subpremise") || result.ToString().Contains("street_address"))
            return true;
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