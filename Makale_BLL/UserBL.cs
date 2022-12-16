using Makale.Common;
using Makale_DAL;
using Makale_Entities;
using Makale_Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class UserBL
    {
        ResponsesBL<User> response = new ResponsesBL<User>();

        Repository<User> repoUsr = new Repository<User>();
        public ResponsesBL<User> SignUp(SignupModel sngUsr)
        {
            User usr = repoUsr.Find(x => x.Username == sngUsr.Username || x.Email == sngUsr.Email);
            if (usr != null)
            {
                if (usr.Username == sngUsr.Username)
                {
                    response.errors.Add("Username Is Already In Use");
                }
                if (usr.Email == sngUsr.Email)
                {
                    response.errors.Add("Email Is Already In Use");
                }
            }
            else
            {
                int success = repoUsr.Create(new User()
                {
                    Email = sngUsr.Email,
                    Username = sngUsr.Username,
                    Password = sngUsr.Password,
                    RegisterationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    UpdatedBy = "system",
                    ActivationGuid = Guid.NewGuid(),
                    Admin = false,
                    Active = false

                });
                if (success > 0)
                {
                    response.Obj = repoUsr.Find(x => x.Email == sngUsr.Email && x.Username == sngUsr.Username);
                    Guid activeGuid = response.Obj.ActivationGuid;
                    //Activation Mail
                    string siteUrl = ConfigHelper.Get<string>("SiteRootUri");
                    string activationUrl = $"{siteUrl}/Home/UserActivate/{activeGuid}";
                    string body = $" Welcome! {response.Obj.Username} <br> To activate your account please click on the link below <a href={activationUrl}> Click To Activate </a>";
                    MailHelper.SendMail(body, response.Obj.Email, "Account Activaion");

                }
            }
            return response;
        }

        public ResponsesBL<User> LogIn(LoginModel lgnUsr)
        {
            ResponsesBL<User> response = new ResponsesBL<User>();

            User usr = repoUsr.Find(x => x.Username == lgnUsr.Username && x.Password == lgnUsr.Password);
            if (usr != null)
            {
                if (!usr.Active)
                {
                    response.errors.Add("Account Is Not Activated Yet, Please Activate Your Account Via Activation Mail");
                }
            }
            else
            {
                response.errors.Add("User Not Found");
            }
            return response;

        }
    }
}
