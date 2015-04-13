using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PulseNetwork.Models
{
    public class Skill
    {
        [Key]
        public int ID { get; set; }
        public String skillName { get; set; }
        public String skillDescription { get; set; }



        public int FindIdByName(string name)
        {
            int id = 0;
            using (var context = new ApplicationDbContext())
            {
                id = (int) (from u in context.Skills
                            where u.skillName == name
                            select u.ID
                            ).SingleOrDefault();
                
            }
            return id;
        }
    }

   // public class SkillDBContext : PulseDbContext
   // {
    //    public DbSet<Skill> Skills { get; set; }
   // }
}
