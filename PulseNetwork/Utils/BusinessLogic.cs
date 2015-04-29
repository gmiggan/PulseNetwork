﻿using System;
using System.Collections.Generic;
using System.Linq;
using PulseNetwork.Models;
using Microsoft.AspNet.Identity;

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

        public WorkspaceInvite FindInviteByUserid(String id, int workspaceId, ApplicationDbContext context)
        {
            
                WorkspaceInvite invite = (WorkspaceInvite)(from u in context.WorkspacesInvites
                                                           where u.workspaceid == workspaceId &&
                                                                 u.userId == id
                                                           select u).SingleOrDefault();

                return invite;
            
        }

        public List<Workspace> FindUsersWorkspace(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                List<WorkspaceInvite> wi = (List<WorkspaceInvite>)(from u in context.WorkspacesInvites
                                                                   where u.userId == id
                                                                   select u).ToList();
                List<Workspace> wo = new List<Workspace>();
                foreach (var item in wi)
                {
                    var workspace = (Workspace)(from u in context.Workspaces
                                                where u.id == item.workspaceid
                                                select u).SingleOrDefault();
                    wo.Add(workspace);
                }
                return wo;
            }
        }

        public List<ApplicationUser> FindUsersInWorkspace(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                List<WorkspaceInvite> wi = (List<WorkspaceInvite>)(from u in context.WorkspacesInvites
                                                                   where u.workspaceid == id
                                                                   select u).ToList();
                List<ApplicationUser> users = new List<ApplicationUser>();
                foreach (var item in wi)
                {
                    users.Add(db.Users.Find(item.userId));
                }

                return users;

            }
        }

        public void FindSumOfUserSkillPoints(string userid)
        {
            int totalExperience = 0;
            using (var context = new ApplicationDbContext())
            {
                List<UserSkill> userskills = (List<UserSkill>)(from u in context.UserSkills
                                                               where u.UserID == userid
                                                               select u).ToList();


                foreach (UserSkill skill in userskills)
                {
                    totalExperience = totalExperience + skill.Score;
                }

                context.Users.Find(userid).ExperiencePoints = totalExperience;
                context.SaveChanges();

            }
        }


        public List<Question> usersQuestions(string userid)
        {

            using (var context = new ApplicationDbContext())
            {
                List<Question> questions = (List<Question>)(from u in context.Questions
                                                            where u.UserID == userid
                                                            select u).ToList();

                return questions;



            }
        }

        public List<Question> availableQuestions(String userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            List<Question> availableQuestions = new List<Question>();
            List<Question> allQuestions = db.Questions.ToList();
            foreach (var question in allQuestions)
            {

                if (maxLevel(question) <= user.Level)
                {
                    availableQuestions.Add(question);
                }
            }
            return availableQuestions;
        }

        public List<UserSkill> UserSkillList(String userid)
        {
            using (var context = new ApplicationDbContext())
            {
                List<UserSkill> userskills = (List<UserSkill>)(from u in context.UserSkills
                                                               where u.UserID == userid
                                                               select u).ToList();


                return userskills;

            }
        }

        public ApplicationUser FindUserByUsername(string username)
        {
            using (var context = new ApplicationDbContext())
            {
                ApplicationUser user = (ApplicationUser)(from u in context.Users
                                                         where u.UserName == username
                                                         select u).SingleOrDefault();


                return user;

            }
        }


        public bool canAnswer(Question question, String userid)
        {
            ApplicationUser user = db.Users.Find(userid);
            if (maxLevel(question) <= user.Level)
            {
                return true;
            }
            else return false;
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
            TimeSpan timeposted = (TimeSpan)question.TimePosted;
            TimeSpan currentTime = new TimeSpan();
            var maximumlevel = level;
            TimeSpan duration = currentTime - timeposted;
            for (int i = 0; i <= duration.TotalHours; i++)
            {
                maximumlevel++;
            }
            return maximumlevel;

        }

    }

}

