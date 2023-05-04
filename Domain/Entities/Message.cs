using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Domain.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }

        public static Message Create(
            string text, 
            Room room,
            User user
        )
        {
            return new Message()
            {
                Id = Guid.NewGuid(),
                Text = text,
                Timestamp = DateTime.Now,
                RoomId = room.Id,
                Room = room,
                User = user
            };
        }
    }
}