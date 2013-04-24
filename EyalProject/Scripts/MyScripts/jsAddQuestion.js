function changeQuestionType(questionType) {
    // Hide all the inner answear type divs and show the chosen one
    $("#dvAnswear").find("div").hide();
    $("#dv" + questionType + "AnswerType").show()
                                           .children().show();
}

// Function used for adding/removing multiple choice answear
function editMultiAnswerCorrect(action) {
    var numberOfCorrectAnswers = $("#dvMoreThanOneCorrect>.answer").length;
    var numberOfAnswers = $("#dvMoreThanOneAnswerType .answer").length;
    if (action === 'add') {
        // Check that we dont have above max number of answers
        if (numberOfAnswers < maxNumberOfMoreThanOneAnswers) {
            $("#dvMoreThanOneCorrect").append(
                '<br/> ' +
                '<input type="text" class="answer" /> ');
        }
    } else {
        // Check that we dont have below min number of answers
        if ((numberOfAnswers > minNumberOfMoreThanOneAnswers) &&
            (numberOfCorrectAnswers > 1)) {
            $("#dvMoreThanOneCorrect :nth-last-child(-n+2)").remove();
        }
    }
}

// Function used for adding/removing multiple choice answear
function editMultiAnswerIncorrect(action) {
    var numberOfAnswers = $("#dvMoreThanOneAnswerType .answer").length;
    var numberOfIncorrectAnswers = $("#dvMoreThanOneIncorrect>.answer").length;
    if (action === 'add') {
        // Check that we dont have above max number of answers
        if (numberOfAnswers < maxNumberOfMoreThanOneAnswers) {
            $("#dvMoreThanOneIncorrect").append(
                '<br/> ' +
                '<input type="text" class="answer" /> ');
        }
    } else {
        // Check that we dont have below min number of answers
        if (numberOfAnswers > minNumberOfMoreThanOneAnswers &&
            (numberOfIncorrectAnswers > 1)) {
            $("#dvMoreThanOneIncorrect :nth-last-child(-n+2)").remove();
        }
    }
}

// Function used for adding/removing multiple choice answear
function editAmericanAnswerIncorrect(action) {
    var numberOfIncorrectAnswers = $("#dvAmericanIncorrect>.answer").length;
    if (action === 'add') {
        // Check that we dont have above max number of answers
        if (numberOfIncorrectAnswers + 1 < maxNumberOfAmericanAnswers) {
            $("#dvAmericanIncorrect").append(
                '<br/> ' +
                '<input type="text" class="answer" /> ');
        }
    } else {
        // Check that we dont have below min number of answers
        if (numberOfIncorrectAnswers + 1 > minNumberOfAmericanAnswers &&
            (numberOfIncorrectAnswers > 1)) {
            $("#dvAmericanIncorrect :nth-last-child(-n+2)").remove();
        }
    }
}

function addQuestion() {
    var urlString = "";
    var correctAnswers;
    var inCorrectAnswers;
    var question = new Object();
    question.QuestionText = $("#txtQuestion").val();
    question.SubSubjectId = $("#ddlSubsubjects").val();
    question.DifficultyId = $("#ddlDifficulties").val();
    question.QuestionType = $("#ddlTypes").val();

    switch (question.QuestionType) {
    case "Open":
        urlString = "AddOpenQuestion";
        question.CorrectAnswer = $("#txtOpenAnswer").val();
    break;
    case "TrueFalse":
        urlString = "AddTrueFalseQuestion";
        question.IsTrue = $("input:radio[name='rdbTrueFalse']:checked").val();
    break;
    case "American":
        urlString = "AddAmericanQuestion";
        question.CorrectAnswer = $("#txtAmericanCorrect").val();

        question.IncorrectAnswersArray = new Array();

        $("#dvAmericanIncorrect>.answer").each(function () {
            question.IncorrectAnswersArray.push($(this).val());
        });
    break;
    case "MoreThanOne":
        urlString = "AddMoreThanOneQuestion";

        question.CorrectAnswersArray = new Array();

        $("#dvMoreThanOneCorrect>.answer").each(function () {
            question.CorrectAnswersArray.push($(this).val());
        });

        question.IncorrectAnswersArray = new Array();

        $("#dvMoreThanOneIncorrect>.answer").each(function () {
            question.IncorrectAnswersArray.push($(this).val());
        });

    break;
        
    default:
    }

    $.ajax({
        url: urlString,
        type: 'POST',
        data: JSON.stringify(question),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            alert("שאלה נוספה בהצלחה!");
        }
    });
}

function changeCourse(courseId) {
    $.ajax({
        url: 'LoadCourseSubjects',
        type: 'POST',
        data: JSON.stringify({ courseId: courseId }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            $('select#ddlSubjects').empty();

            $.each(data, function () {
                $('select#ddlSubjects').append(
                '<option value="' + this.Id + '">'
                + this.Name +
                '</option>');
            });

            changeSubject($("#ddlSubjects").val());
        }
    });
}

function changeSubject(subjectId) {
   $.ajax({
        url: 'LoadSubjectSubsubjects',
        type: 'POST',
        data: JSON.stringify({ subjectId: subjectId }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            $('select#ddlSubsubjects').empty();

            $.each(data, function () {
                $('select#ddlSubsubjects').append(
                '<option value="' + this.Id + '">'
                + this.Name +
                '</option>');
            });
        }
    });
}

$(function () {
    changeCourse($("#ddlCourses").val());
    changeSubject($("#ddlSubjects").val());
    changeQuestionType($("#ddlTypes").val());

    $("#btnCreate").on("click", addQuestion);
});