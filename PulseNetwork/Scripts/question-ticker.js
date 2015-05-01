$(function () {

    if (userAuthorized) {

        $('#tickerDiv').css("display", "block");

        var ticker = $.connection.questionTickerHub, // the generated client-side hub proxy
            $tickerDiv = $('#tickerDiv'),
            $tickerDivList = $tickerDiv.find('ul');
       
        function init() {
            ticker.server.getAllQuestions().done(function (questions) {
                $tickerDivList.empty();
                var qs = JSON.parse(questions);
                for (var question in qs) {
                    var q = qs[question];
                    var element = '<li><div class="tickerItem">';
                    element += '<a target="_blank" href="/Question/ViewQuestion/' + q.ID + '">' + q.QuestionTitle + '</a><br/>'
                    element += '<p><b>Posted At: </b>' + q.DatePosted + '<br/><b>By: </b>' + q.ApplicationUser.FullName + '</p>';
                    element += '</div></li>';
                    $tickerDivList.append(element);
                }
            });
        }

        // Add a client-side hub method that the server will call
        ticker.client.updateQuestions = function (question) {
            var q = JSON.parse(question);
            var element = '<li><div class="tickerItem">';
            element += '<a target="_blank" href="/Question/ViewQuestion/' + q.ID + '">' + q.QuestionTitle + '</a>'
            element += '<p><b>Posted At: </b>' + q.DatePosted + '<br/><b>By: </b>' + q.ApplicationUser.FullName + '</p>';
            element += '</div></li>';
            $tickerDivList.prepend(element);
        }

        // Start the connection
        $.connection.hub.start().done(init);
    } 
});