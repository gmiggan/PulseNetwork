using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PulseNetwork.Models
{
    public class UserSkill
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("ApplicationUser")]
        public String UserID { get; set; }
        [ForeignKey("Skill")]
        public int skillID { get; set; }
        public int Score { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Skill Skill { get; set; }


        public String getSkillName()
        {
            using (var context = new ApplicationDbContext())
            {
                Skill skill = (Skill)(from u in context.Skills
                                                  where u.ID == this.skillID
                                                      
                                                  select u).SingleOrDefault();



                return skill.skillName;

            }

        }



        public UserSkill FindUserSkill(int skillid, string userid)
        {
            using (var context = new ApplicationDbContext())
            {
                UserSkill userskill = (UserSkill)(from u in context.UserSkills
                                                                 where u.skillID == skillid &&
                                                                       u.UserID == userid
                                                                 select u).SingleOrDefault();



                return userskill;

            }
            
        }
    }

    public class UserSkillDBContext : PulseDbContext
    {
        public DbSet<UserSkill> UserSkills { get; set; }
    }
}