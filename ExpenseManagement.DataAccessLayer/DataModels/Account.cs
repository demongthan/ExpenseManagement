using Microsoft.AspNetCore.Identity;

namespace ExpenseManagement.DataAccessLayer.DataModels
{
    public class Account : IdentityUser
    {

        public Saving Saving { get; set; }
        public LoginModel LoginModel { get; set; }
    }
}
