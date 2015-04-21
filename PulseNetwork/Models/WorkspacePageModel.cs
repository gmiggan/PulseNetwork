using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PulseNetwork.Models
{
    public class WorkspacePage
    {
        [Key]
        public int id { get; set; }
        public int workspaceid { get; set; }
        public String userId { get; set; }
        [ForeignKey("workspaceid")]
        public virtual Workspace workspace { get; set; }
        [ForeignKey("userId")]
        public virtual ApplicationUser user { get; set; }
        
    }
}