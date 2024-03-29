﻿using Article.Common;
using Article_DAL;
using Article_Entities;
using Article_Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article_BLL
{
    public class UserBL
    {
        ResponsesBL<User> response = new ResponsesBL<User>();

        Repository<User> repoUsr = new Repository<User>();
        public List<User> GetUsers()
        {
            return repoUsr.Read();
        }

        public ResponsesBL<User> SignUp(SignupModel sngUsr)
        {
            User usr = new User();
            usr.Username = sngUsr.Username;
            usr.Email = sngUsr.Email;

            response = UserControl(usr);

            if (response.errors.Count > 0)
            {
                response.Obj = usr;
                return response;
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
                //Activion mail logic. Use another server if required 
                if (success > 0)
                {
                    response.Obj = repoUsr.Find(x => x.Email == sngUsr.Email && x.Username == sngUsr.Username);
                    Guid activeGuid = response.Obj.ActivationGuid;

                    string siteUrl = ConfigHelper.Get<string>("SiteRootUri");
                    string activationUrl = $"{siteUrl}/Home/UserActivate/{activeGuid}";
                    string body = $" Welcome! {response.Obj.Username} <br> To activate your account please click on the link below <a href={activationUrl}> Click To Activate </a>";
                    MailHelper.SendMail(body, response.Obj.Email, "Account Activation");
                }
            }
            return response;
        }

        public ResponsesBL<User> LogIn(LoginModel lgnUsr)
        {
            ResponsesBL<User> response = new ResponsesBL<User>();

            response.Obj = repoUsr.Find(x => x.Username == lgnUsr.Username && x.Password == lgnUsr.Password);
            if (response.Obj != null)
            {
                if (!response.Obj.Active)
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

        public ResponsesBL<User> ActivateUser(Guid id)
        {
            ResponsesBL<User> response = new ResponsesBL<User>();

            response.Obj = repoUsr.Find(x => x.ActivationGuid == id);

            if (response.Obj != null)
            {
                if (response.Obj.Active)
                {
                    response.errors.Add("User is Already Activated");
                    return response;
                }
                response.Obj.Active = true;
                repoUsr.Update(response.Obj);

            }
            else
            {
                response.errors.Add("User Could Not Be Found");
            }

            return response;
        }

        public ResponsesBL<User> UpdateUser(User usr)
        {

            response = UserControl(usr);

            if (response.errors.Count > 0)
            {
                response.Obj = usr;
                return response;
            }
            response.Obj = repoUsr.Find(x => x.Id == usr.Id);
            response.Obj.Name = usr.Name;
            response.Obj.Surname = usr.Surname;
            response.Obj.Email = usr.Email;
            response.Obj.Password = usr.Password;
            if (!string.IsNullOrEmpty(usr.Avatar))
            {
                response.Obj.Avatar = usr.Avatar;
            }
            int updateResult = repoUsr.Update(response.Obj);
            if (updateResult < 1)
            {
                response.errors.Add("Failed to Update the Profile, Please Check Your Inputs");
            }
            return response;
        }
        public ResponsesBL<User> DeleteUser(User usr)
        {
            ResponsesBL<User> response = new ResponsesBL<User>();
            response.Obj = repoUsr.Find(x => x.Username == usr.Username || x.Email == usr.Email);
            if (response.Obj != null)
            {
                repoUsr.Delete(response.Obj);
            }
            else
            {
                response.errors.Add("User Cannot Be Found");
            }
            return response;
        }

        public User GetUserById(int id)
        {
            return repoUsr.Find(x => x.Id == id);
        }
        public ResponsesBL<User> CreateUserByAdmin(User sngUsr)
        {
            response = UserControl(sngUsr);
            if (response.errors.Count > 0)
            {
                response.Obj = sngUsr;
                return response;
            }
            else
            {
                int success = repoUsr.Create(sngUsr);
                if (success < 0)
                {
                    response.errors.Add("User Could not be Saved");
                }
            }
            return response;
        }
        private ResponsesBL<User> UserControl(User usr)
        {
            User userEdited = repoUsr.Find(x => x.Username == usr.Username);
            User userEdited2 = repoUsr.Find(y => y.Email == usr.Email);

            if (userEdited != null && userEdited.Id != usr.Id)
            {
                response.errors.Add("Username Is Already In Use");
            }
            if (userEdited2 != null && userEdited2.Id != usr.Id)
            {
                response.errors.Add("Username Is Already In Use");
            }
            return response;
        }
    }

}
