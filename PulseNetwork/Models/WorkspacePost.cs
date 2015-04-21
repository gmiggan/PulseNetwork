using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PulseNetwork.Models
{
    public class WorkspacePost
    {
        [Key]
        public int id { get; set; }
        public int workspaceId { get; set; }
        public String posterId { get; set; }
        public DateTime TimePosted { get; set; }
        public String postDetails { get; set; }
        [ForeignKey("workspaceId")]
        public virtual WorkspacePage workspacePage { get; set; }
        [ForeignKey("posterId")]
        public virtual ApplicationUser poster { get; set; }
    }
}