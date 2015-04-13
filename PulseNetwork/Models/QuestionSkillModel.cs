using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PulseNetwork.Models
{
    public class QuestionSkill
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        [ForeignKey("Skill")]
        public int SkillID { get; set; }
       

        public virtual Skill Skill { get; set; }
        public virtual Question Question { get; set; }
    }

  
}