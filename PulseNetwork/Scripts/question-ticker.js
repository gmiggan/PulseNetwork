$(function () {

    if (userAuthorized) {
        var ticker = $.connection.questionTickerHub, // the generated client-side hub proxy
        up = '▲',
        down = '▼',
        $tickerDiv = $('#tickerDiv'),
        $tickerDivList = $tickerDiv.find('ul'),
        rowTemplate = '';

        function init() {
            ticker.server.getAllQuestions().done(function (questions) {
                $tickerDivList.empty();
                $.each(questions, function () {
                    var question = this;
                    $tickerDivList.append('<li>' + question.QuestionTitle + '</li>');
                });
            });
        }

        // Add a client-side hub method that the server will call
        ticker.client.updateQuestions = function (question) {
            $tickerDivList.append('<li>' + question.QuestionTitle + '</li>');
        }

        // Start the connection
        $.connection.hub.start().done(init);
    } else {

        $('#tickerDiv').css("float", "");
        $('#mainbar').css("float", "center");

    }
});