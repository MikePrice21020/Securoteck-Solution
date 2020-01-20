using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Xml;

namespace SecuroteckWebApplication.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ApiKey { get; set; }
        public string UserName { get; set; }
        public DateTime Date_Of_Creation { get; set; }
        public User() { }

    }
#region Task11?
    // TODO: You may find it useful to add code here for Log
    #endregion

    public class UserDatabaseAccess
    {
        public User CreateNewUser(string input_name)
        {
            using (var ctx = new UserContext())
            {
                DateTime LocalDate = DateTime.Now;
                User user = new User() { UserName = input_name, Date_Of_Creation = LocalDate };

                try
                {
                    ctx.Users.Add(user);
                }
                catch (Exception)
                {
                    // Insert error warning
                }
                ctx.SaveChanges();
                return user;
            }

        }
        public bool Check_Api_exists(Guid api)
        {
            try
            {
                using (var ctx = new UserContext())
                {
                    var user = ctx.Users.Find(api);
                    if (user == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch(FormatException)
            {
                return false;
            }
        }


            public string Retrieve_username(Guid api)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.Find(api);
                if (user.UserName == null)
                {
                    return null;
                }
                else
                {
                    return user.UserName;
                }
            }
        }
        public bool Check_user_exists(string input_name)
        {
            using (var ctx = new UserContext())
            {
                //try
                //{
                var query = ctx.Users
                    .Where(b => b.UserName == input_name)
                    .FirstOrDefault();
                //var user = ctx.Users.Find(input_name);

                if (query == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                //}
               // catch
               // {
               //     return false;
               // }
            }
        }
        public bool Check_Username_and_Api_exists(Guid api, string username)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.Find(api);
                if (user == null || user.UserName != username)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public User Api_exists_Return_Username(Guid api)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.Find(api);
                return user;
            }
        }

            public void Api_deleteUser(Guid api)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.Find(api);
                ctx.Users.Remove(user);
                ctx.SaveChanges();
            }
        }

}
}