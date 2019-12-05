$(function () {
    $("#currentAnswer").focus();
});

$(".btn-group > .btn").click(function () {
    $(this).addClass("active").siblings().removeClass("active");
});

function changeCategory(catId) {
    $('.loader').show();

    var queryString = {
        catId: catId
    };

    $.ajax({
        url: "/Challenge/FilterQuestions",
        method: "GET",
        data: queryString,
        dataType: "json"
    }).done(function (data) {
        //setScore(data.currentScore);
        resetFields();

        //set next question info
        $('#questionId').val(data.question.questionId);
        $('#questionNum').html(data.number);
        $('#currentQuestion').html(data.question.body);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        alert("Message: " + jqXHR.responseText + "\nStatus code: " + textStatus + "\nError: " + errorThrown);
    }).always(function () {
        $('.loader').hide();
    });
}

function getNextQuestion() {
    $('.loader').show();
    var queryString = {
        fromFilter: false
    };
    
    $.ajax({
        url: "/Challenge/GetNextQuestion",
        method: "GET",
        data: queryString,
        dataType: "json"
    }).done(function (data) {
        //setScore(data.currentScore);
        resetFields();

        //set next question info
        $('#questionId').val(data.question.questionId);
        $('#questionNum').html(data.number);
        $('#currentQuestion').html(data.question.body);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        alert("Message: " + jqXHR.responseText + "\nStatus code: " + textStatus + "\nError: " + errorThrown);
    }).always(function () {
        $('.loader').hide();
    });
}

function resetFields() {
    $('#answerBtn').prop("disabled", false);
    $('#correct').hide();
    $('#incorrect').hide();
    $('#nextBtn').hide();
    $('#currentAnswer').val('');
}

function setScore(score) {
    var total = $('#originalScore').val();
    //set the scores
    $('#totalEarned').html(parseInt(total) + parseInt(score));
    $('#earnedNow').html(parseInt(score));
}

function checkAnswer() {
    var qId = $('#questionId').val();
    var answer = $('#currentAnswer').val();

    if (answer.trim().length === 0) {
        alert("Answer cannot be empty");
        return;
    }

    $('.loader').show();

    var jsonBody = {
        qId: qId,
        answer: answer
    };

    $.ajax({
        url: "/Challenge/SubmitAnswer",
        method: "POST",
        data: jsonBody,
        dataType: "json"
    }).done(function (data) {
        if (data.success) {
            $('#correct').show();
            $('#incorrect').hide();

            setScore(data.currentScore);
            $('#answerBtn').prop("disabled", true);
        } else {
            $('#correct').hide();
            $('#incorrect').show();
        }
        $('#nextBtn').show();
    }).fail(function (jqXHR, textStatus, errorThrown) {
        alert("Message: " + jqXHR.responseText + "\nStatus code: " + textStatus + "\nError: " + errorThrown);
    }).always(function () {
        $('.loader').hide();
    });
}
