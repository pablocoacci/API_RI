using Microsoft.AspNetCore.Identity;

namespace Core.Data.Entities
{
    public class User : IdentityUser
    {
        public User() { }

        public User(string userName, string firsName, string lastName) : base(userName)
        {
            FirstName = firsName;
            LastName = lastName;
            CreatedOn = DateTimeOffset.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
