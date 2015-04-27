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

        private readonly TimeSpan fetchInterval = TimeSpan.FromMilliseconds(120000);

        private readonly Timer timer;

        private QuestionTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            BusinessLogic bl = new BusinessLogic();
            questions.Clear();
            string userId = "136382c6-911f-4179-82db-70299baf901c";
            List<Question> availableQuestions = bl.availableQuestions(userId).OrderByDescending(x => x.DatePosted).ToList();
            availableQuestions.ForEach(question => questions.TryAdd(question.ID.ToString(), question));

            timer = new Timer(FetchQuestions, null, TimeSpan.FromSeconds(0), fetchInterval);
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

        public IEnumerable<Question> GetAllQuestions()
        {
            return questions.Values;
        }

        private void FetchQuestions(object state)
        {
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