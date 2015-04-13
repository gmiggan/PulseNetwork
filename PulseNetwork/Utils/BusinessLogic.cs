using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PulseNetwork.Models;

namespace PulseNetwork.Utils
{
    public class BusinessLogic
    {
        public UserSkill FindUserSkill(int skillid, string userid, ApplicationDbContext db)
        {
            
                UserSkill userskill = (UserSkill)(from u in db.UserSkills
                                                  where u.skillID == skillid &&
                                                        u.UserID == userid
                                                  select u).SingleOrDefault();



                return userskill;

            

        }


        public void FindSumOfUserSkillPoints(string userid)
        {
            int totalExperience = 0;
            using (var context = new ApplicationDbContext())
            {
                List<UserSkill> userskills = (List<UserSkill>)(from u in context.UserSkills
                                                  where  u.UserID == userid
                                                               select u).ToList() ;


                foreach (UserSkill skill in userskills)
                {
                    totalExperience = totalExperience + skill.Score;
                }

                context.Users.Find(userid).ExperiencePoints = totalExperience;
                context.SaveChanges();

            }
        }

    }

}

