using System;

namespace LegacyApp
{
    public class UserService
    {

        public bool IsNullFullName(string firstName,string lastName)
        {
            if (string.IsNullOrEmpty(firstName)||string.IsNullOrEmpty(lastName))
            {
                return true;
            }

            return false;
        }

        public bool ValidationEmail(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return true;
            }
            return false;
        }

        public bool IsUserOlder21(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return true;
            }

            return false;
        }

        protected User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else
            if (user.HasCreditLimit)
            {
                var userCreditService = new UserCreditService();
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                if (user.Client == "ImportantClient")
                {
                    creditLimit *= 2;
                }
                user.CreditLimit = creditLimit;
            }
            return user;
        }

        
        protected bool IsValidCreditLimit(User user)
        {
            return !user.HasCreditLimit || user.CreditLimit >= 500;
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsNullFullName(firstName,lastName)) return false;
            
            if (ValidationEmail(email)) return false;

            if (IsUserOlder21(dateOfBirth)) return false;

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = CreateUser(firstName, lastName, email, dateOfBirth, client);
            

            if (!IsValidCreditLimit(user))
                return false;

            
            UserDataAccess.AddUser(user);
            return true;
        }
    }
}

