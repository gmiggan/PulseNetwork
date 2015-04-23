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
    public class Workspace
    {
        [Key]
        public int id { get; set; }
        public String name { get; set; }
        public String creatorID { get; set; }
        [ForeignKey("creatorID")]
        public virtual ApplicationUser creator { get; set; }
        public virtual List<ApplicationUser> users { get; set; }
    
    }
}