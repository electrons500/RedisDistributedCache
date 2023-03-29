using System.Net;

namespace RedisDistributedCache.Data.Model
{
    public class Users
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? PersonalStatement { get; set; }
        public string[] Skills { get; set; } = Array.Empty<string>();
        public Address? Address { get; set; }


    }
}
