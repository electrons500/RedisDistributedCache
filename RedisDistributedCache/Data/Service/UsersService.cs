using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualBasic;
using RedisDistributedCache.Data.Model;
using System.Text.Json;

namespace RedisDistributedCache.Data.Service
{
    public class UsersService
    {
        private IDistributedCache _DistributedCache;
        public UsersService(IDistributedCache distributedCache)
        {
            _DistributedCache = distributedCache;
        }

        //Save data in cache
        public bool AddUserDataToCache(Users users)
        {
            var cacheOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60), //Expired time for the data
                SlidingExpiration = null  // If the data has not be accessed within the set time,it will 
                                          // expired 
            };

            var jsonString = JsonSerializer.Serialize(users);

            var userInsertTask = _DistributedCache.SetStringAsync($"user{users.Id}", jsonString, cacheOptions);

            return true;
        } 

        //Get data By key
        public async Task<Users> GetUsersAsync(string keyName)
        {
            
            var getUsers = await _DistributedCache.GetStringAsync(keyName);
            var objectData = JsonSerializer.Deserialize<Users>(getUsers);
            Users model = new()
            {
                Id =objectData!.Id,
                FirstName = objectData.FirstName,
                LastName = objectData.LastName,
                Age = objectData.Age,
                PersonalStatement = objectData.PersonalStatement,
                Skills = objectData.Skills,
                Address = new Address
                {
                    StreetName = objectData.Address!.StreetName,
                    City = objectData.Address!.City,
                    State = objectData.Address!.State,
                    PostalCode = objectData.Address!.PostalCode,
                    Country = objectData.Address!.Country
                }
            };

            return model;
        }


    }
}
