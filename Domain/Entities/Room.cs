using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ChatApplication.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public IList<User> Users{ get; set; }
        public IList<Message> Messages{ get; set; }

        private Room()
        {
            Id = Guid.NewGuid();
        }

        public static Room CreateRoom(
            IList<User> users
            )
        {
            Room room = new Room();
            room.Users = users;
            return room;
        }
    }
}
