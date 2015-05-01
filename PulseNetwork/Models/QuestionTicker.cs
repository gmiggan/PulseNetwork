using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PulseNetwork.Utils;

namespace PulseNetwork.Models
{
    public class QuestionTicker
    {
        private readonly static Lazy<QuestionTicker> instance = new Lazy<QuestionTicker>(() => new QuestionTicker(GlobalHost.ConnectionManager.GetHubContext<QuestionModelHub>().Clients));

        private readonly ConcurrentDictionary<string, Question> questions = new ConcurrentDictionary<string, Question>();

        private DateTime now = DateTime.MinValue;

        private readonly TimeSpan fetchInterval = TimeSpan.FromMilliseconds(250);

        private readonly Timer timer;

        private bool firstTime;

        private QuestionTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            if (now == DateTime.MinValue)
            {
                now = DateTime.Now;
                firstTime = true;
            }
            
            timer = new Timer(FetchQuestions, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(30));
        }

        public static QuestionTicker Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public List<Question> GetAllQuestions()
        {
            return questions.Values.ToList();
        }

        private void FetchQuestions(object state)
        {
            BusinessLogic bl = new BusinessLogic();
            string userId = "136382c6-911f-4179-82db-70299baf901c";

            questions.Clear();
            List<Question> availableQuestions = new List<Question>();
            if (firstTime)
            {
                availableQuestions = bl.availableQuestions(userId).OrderBy(x => x.DatePosted).ToList();
                firstTime = false;
            }
            else
            {
                availableQuestions = bl.availableQuestions(userId).Where(d => d.DatePosted >= now).OrderBy(x => x.DatePosted).ToList();
            }

            availableQuestions.ForEach(question => questions.TryAdd(question.ID.ToString(), question));

            foreach (var question in questions.Values)
            {
                BroadcastQuestions(question);
            }
        }

        private void BroadcastQuestions(Question question)
        {
            var serializedQuestion = JsonConvert.SerializeObject(question, Formatting.None,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            Clients.All.updateQuestions(serializedQuestion);
        }
    }
}