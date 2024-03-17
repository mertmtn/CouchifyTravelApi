using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CouchifyTravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlinesController(INamedBucketProvider buckedProvider) : ControllerBase
    {
        private readonly INamedBucketProvider _buckedProvider = buckedProvider;

        [HttpGet]
        public async Task<string> Get(string key)
        {
            try
            {
                var bucket = await _buckedProvider.GetBucketAsync();
                var scope = bucket.Scope("inventory");
                var collection = scope.Collection("airline");

                JObject result = (await collection.GetAsync(key)).ContentAs<JObject>();
                return $"Fetch document success. Result: {result}";
            }
            catch (CouchbaseException ex)
            {
                return ex.Message;
            }
            
        }
    }
}
