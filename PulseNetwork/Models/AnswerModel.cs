using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PulseNetwork.Models
{
    [Serializable]
    public class Answer
    {
        public int ID { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        public string AnswerBody { get; set; }
        public Double points { get; set; }
        public int Count { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Question Question { get; set; }

        public ApplicationUser findUserById(int answerId)
        {
            using (var context = new ApplicationDbContext())
            {
                Answer answer = (Answer)(from a in context.Answers
                                                                 where a.ID == answerId
                                                                 select a).SingleOrDefault();



                return answer.ApplicationUser;

            }
        }

        public List<Skill> getQuestionSkills()
        {
            using (var context = new ApplicationDbContext())
            {
                Question question = (Question)(from q in context.Questions
                                         where q.ID == this.QuestionID
                                         select q).SingleOrDefault();

                return question.getQuestionSkills().ToList();
            }
        }
    }

    public class AnswerDbContext : PulseDbContext
    {
        public DbSet<Answer> Answers { get; set; }
    }
}