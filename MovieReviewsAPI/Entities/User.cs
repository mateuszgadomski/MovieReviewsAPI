using System;

namespace MovieReviewsAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public int FirstName { get; set; }
        public int LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int ReviewCount { get; set; }

        public int RoleId { get; set; } = 1;
        public virtual Role Role { get; set; }
    }
}