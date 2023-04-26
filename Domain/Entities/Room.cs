using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ChatApplication.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public ICollection<User> Users{ get; set; }
        public ICollection<Message> Messages{ get; set; }

        private Room()
        {
            Id = Guid.NewGuid();
        }

        public static Room CreateRoom(
            ICollection<User> users
            )
        {
            Room room = new Room();
            room.Users = users;
            return room;
        }
    }
}
