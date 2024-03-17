using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using CouchifyTravelApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CouchifyTravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlinesController(INamedBucketProvider buckedProvider) : ControllerBase
    {
        private readonly INamedBucketProvider _buckedProvider = buckedProvider;

        [HttpGet("GetByKey")]
        public async Task<string> Get(string key)
        {
            try
            {
                var bucket = await _buckedProvider.GetBucketAsync();
                var scope = bucket.Scope("inventory");
                var collection = scope.Collection("airline");

                var result = (await collection.GetAsync(key)).ContentAs<JObject>();

                var resultObj = JsonConvert.DeserializeObject<Airline>(result.ToString());

                return $"Fetch document success. Result: {result}";
            }
            catch (CouchbaseException ex)
            {
                return ex.Message;
            }

        }

        [HttpGet("GetAll")]
        public async Task<List<Airline>> Get()
        {
            try
            { 
                var bucket = await _buckedProvider.GetBucketAsync();
                var scope = bucket.Scope("inventory");

                var query = "Select callsign, country, iata, icao, id, name, type from airline";

                var queryResult = await scope.QueryAsync<Airline>(query);

                var result = await queryResult.Rows.ToListAsync();
              
                return result;
            }
            catch
            {
                return null;
            }

        }
    }
}
