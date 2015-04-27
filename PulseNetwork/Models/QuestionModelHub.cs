using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace PulseNetwork.Models
{
    [HubName("questionTickerHub")]
    public class QuestionModelHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        private readonly QuestionTicker questionTicker;

        public QuestionModelHub() : this(QuestionTicker.Instance) { }

        public QuestionModelHub(QuestionTicker questionTicker)
        {
            this.questionTicker = questionTicker;
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return questionTicker.GetAllQuestions();
        }
    }
}