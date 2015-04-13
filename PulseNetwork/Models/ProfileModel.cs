using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PulseNetwork.Models
{
    public class Profile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
       
        public virtual ApplicationUser ApplicationUser { get; set; }

        public ApplicationUser ProfileUser()
        {
            using (var context = new ApplicationDbContext())
            {
                ApplicationUser userObject = (ApplicationUser)(from u in context.Users
                                                                 where u.Id == this.UserID
                                                                 select u).SingleOrDefault();



                return userObject;

            }
        }

        public ApplicationUser FindByUserName(string usernmame)
        {
            using (var context = new ApplicationDbContext())
            {
                ApplicationUser userObject = (ApplicationUser)(from u in context.Users
                                                               where u.UserName == usernmame
                                                               select u).SingleOrDefault();



                return userObject;

            }
        }



    }

    public class ProfileDBContext : PulseDbContext
    {
        public DbSet<Profile> Profiles { get; set; }
    }
}