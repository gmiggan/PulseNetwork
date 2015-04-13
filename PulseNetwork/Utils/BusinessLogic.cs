using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PulseNetwork.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;

namespace PulseNetwork.Utils
{
    public class BusinessLogic
    {
        ApplicationDbContext db = new ApplicationDbContext();

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

        public List<Question> availableQuestions(String userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            List<Question> availableQuestions = new List<Question>();
            List<Question> allQuestions = db.Questions.ToList();
            foreach(var question in allQuestions)
            { 
                
                if(maxLevel(question) <= user.Level)
                {
                    availableQuestions.Add(question);
                }
            }
            return availableQuestions;
        }

        public int maxLevel(Question question)
        {
            var level = 0;
            try
            {
                level = question.ApplicationUser.Level;
            }
            catch
            {
                return 1000;
            }
            TimeSpan timeposted = (TimeSpan) question.TimePosted;
            TimeSpan currentTime = new TimeSpan();
            var maximumlevel = level;
            TimeSpan duration = currentTime - timeposted;
            for (int i = 0; i <= duration.TotalHours; i++ )
            {
                maximumlevel++;
            }
            return maximumlevel;

        }

    }

}

