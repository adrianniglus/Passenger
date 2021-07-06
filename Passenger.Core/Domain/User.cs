using System;
using System.Text.RegularExpressions;

namespace Passenger.Core.Domain
{
    public class User
    {
        public Guid Id {get; protected set;}
        public string Email {get; protected set;}
        public string Password {get; protected set;}
        public string Salt {get; protected set;}
        public string Username {get; protected set;} 
        public string FullName {get; protected set;}
        public string Role {get; protected set;}
        public DateTime CreatedAt {get; protected set;}
        public DateTime UpdatedAt {get; protected set;}

        protected User()
        {
        }

        public User(Guid userId, string email, string username, string password, string role, string salt)
        {
            Id = userId;

            SetEmail(email);
            
            SetUsername(username);

            SetPassword(password);

            SetRole(role);

            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {

            if(string.IsNullOrEmpty(email))
            {
                throw new Exception("Email is empty!");
            }

            if(!ValidateEmail(email))
            {
                throw new Exception("Email is invalid");
            }

            if(Email == email)
            {
                return;
            }

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }



        public void SetPassword(string password)
        {

            if(!ValidatePassword(password))
            {
                throw new Exception("Password is invalid!");
            }

            if(Password == password)
            {
                return;
            }

            //if(password.Length >= 30)
            //{
            //    throw new Exception("Password is too long");
            //}

            Password = password;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetUsername(string username)
        {

            if(string.IsNullOrEmpty(username))
            {
                throw new Exception("Username can't be empty!");
            }
            if(!ValidateUsername(username))
            {
                throw new Exception("This is not a valid username! First character need to be a letter, can contain only letters and numbers, 6 to 12 characters!");
            }

            if(Username == username)
            {
                return;
            }

            Username = username.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;

        }

        public void SetRole(string role)
        {
            if(string.IsNullOrEmpty(role))
            {
                throw new Exception("Role can't be empty!");
            }
            if(Role == role)
            {
                return;
            }
            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }



        public bool ValidateEmail(string emailAddress)
        {
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            return isValid;
        }

        public bool ValidatePassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            bool isValid = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);

            return isValid;
        }

        public bool ValidateUsername(string username)
        {
            string pattern;
            pattern = @"^[a-zA-Z][a-zA-Z0-9]{5,11}$"; //first is letter, only letter and number, between 6 and 12 chars
        
            Regex regex = new Regex(pattern);
            return regex.IsMatch(username);
        }
    }
}