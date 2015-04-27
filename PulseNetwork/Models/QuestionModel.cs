using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PulseNetwork.Models
{
    [Serializable]
    public class Question
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionBody { get; set; }
        public DateTime? DatePosted { get; set; }
        public TimeSpan? TimePosted { get; set; }
        public int Count { get; set; }
        public Double Points { get; set; }
        public  virtual ApplicationUser ApplicationUser {get; set;}
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual String tags { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }

        

        public List<string> getSkills()
        {
            List<string> list;
            using (var context = new ApplicationDbContext())
            {
                list = (List<string>)(from u in context.Skills select u.skillName
             ).ToList();
                return list;
            }
        }

        public Skill getSkillIDByName(string skillname)
        {
            Skill skill;
            using (var context = new ApplicationDbContext())
            {
                skill = (Skill) (from u in context.Skills
                           where u.skillName == skillname
                           select u
                            ).SingleOrDefault();

            }
             
            return skill;

        }

        public List<Skill> getQuestionSkills()
        {
            List<int> skillIDs = new List<int>();
            List<Skill> skills = new List<Skill>();

            using (var context = new ApplicationDbContext())
            {
                skillIDs = (List<int>)(from s in context.QuestionSkills
                                     where s.QuestionID == this.ID
                                     select s.SkillID
                            ).ToList();
            }
            using (var context = new ApplicationDbContext())
            {
                foreach(var sid in skillIDs)
                {
                    Skill sk = (from s in context.Skills
                                where s.ID == sid
                                select s
                                ).SingleOrDefault();
                    skills.Add(sk);
                }
            }
            return skills;

        }

        public ApplicationUser PosterDetails()
        {
            using (var context = new ApplicationDbContext())
            {
                ApplicationUser posterObject = (ApplicationUser) (from u in context.Users
                                                                where u.Id == this.UserID
                                                                select u).SingleOrDefault();

                
                
                return posterObject;

            }
        }

        public string getPicture()
        {
             string Picture = PosterDetails().Picture;
             return Picture;
        }
        public string getFullName()
        {
            string FullName = PosterDetails().FullName;
            return FullName;
        }
        

       
        
        
   
    
    
    }

    

    public class QuestionDbContext : PulseDbContext
    {
        public DbSet<Question> Questions { get; set; }
    }
}