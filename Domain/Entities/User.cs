using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApplication.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updateAt { get; set; }
        public ICollection<Room> Rooms { get; set; }


        public static User Create(string username, string email, string password)
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                Username = username,
                Password = password,
                Email = email,
                createdAt = DateTime.Now,
                updateAt = DateTime.Now,
                Rooms = new Collection<Room>()
            };
        }
    }
}
