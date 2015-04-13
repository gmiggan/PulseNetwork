using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using PulseNetwork.Utils;

namespace PulseNetwork.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Picture { get; set; }
        public long ExperiencePoints { get; set; }
        public int Level { get { return 1 + (int)Math.Floor(Math.Pow(ExperiencePoints, 1 / 3.0)); } }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            
            return userIdentity;
        }

        public void updateExperiencePoints()
        {
            BusinessLogic bl = new BusinessLogic();
            
            bl.FindSumOfUserSkillPoints(this.Id);
            
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DatabaseConnection", throwIfV1Schema: false)
        {
        }

        
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<QuestionSkill> QuestionSkills { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        //    //modelBuilder.Entity<Question>()
        //    //    .HasMany(q => q.Skills).WithMany(i => i.Courses)
        //    //    .Map(t => t.MapLeftKey("CourseID")
        //    //        .MapRightKey("InstructorID")
        //    //        .ToTable("CourseInstructor"));
        //}
    }
}