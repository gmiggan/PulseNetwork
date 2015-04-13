using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using PulseNetwork.Models;
using PulseNetwork.Utils;

namespace PulseNetwork.Service
{
    /// <summary>
    /// Summary description for PulseWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PulseWebService : System.Web.Services.WebService
    {
        ApplicationDbContext db = new ApplicationDbContext();
        BusinessLogic bl = new BusinessLogic();
        

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void UpdateVote(int id, int score, string type)
        {
            int updateValue = 1;
            //Update score after vote:
            if (type.Equals("q"))
            {
                Question q = db.Questions.Find(id);
                q.Count = score;
                db.SaveChanges();
            }
            else
            {
                Answer a = db.Answers.Find(id);
                if (a.Count > score)
                {
                    updateValue = -1;
                }
                a.Count = score;
                db.SaveChanges();
            }


            //Add Skill to user and point for that skill
            if(type.Equals("q"))
            {

            }
            else
            {
                ApplicationUser user = db.Answers.Find(id).ApplicationUser;
                List<Skill> skilllist = new List<Skill>(db.Answers.Find(id).getQuestionSkills());
                foreach (Skill skill in skilllist)
                {
                    UserSkill us = bl.FindUserSkill(skill.ID, user.Id, db);
                    if (us == null)
                    {
                        us = new UserSkill();
                        us.UserID = user.Id;
                        us.skillID = skill.ID;
                        us.Score += updateValue;
                        db.UserSkills.Add(us);
                        db.SaveChanges();
                   
                    }
                    else
                    {
                        us.Score += updateValue;
                        db.SaveChanges();
                        
                    }
                    us = null; //reset for next loop
                }
                user.updateExperiencePoints();
            }
        }
    }
}
