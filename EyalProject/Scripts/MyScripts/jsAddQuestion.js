var multiAnswearcounter = 4;
var americanAnswerCounter = 4;

function changeQuestionType(questionType) {
    // Hide all the inner answear type divs and show the chosen one
    $("#dvAnswear").find("div").hide();
    $("#dv" + questionType + "AnswearType").show()
                                           .children().show();
}

// Function used for adding/removing multiple choice answear
function editMultiAnswear(action) {
    if (action === 'add') {
        // Check that we dont have above 8 answears
        if (multiAnswearcounter < 8) {
            multiAnswearcounter++;
            $("#dvMultiAnswearContainer").append(
                '<br/> ' +
                '<label class="form-label">תשובה ' + multiAnswearcounter + '</label> ' +
                '<input type="text" id="txtMulti' + multiAnswearcounter + '"/> ' +
                '<input type="checkbox" name="chkbxMulti" id="chkbxMulti' + multiAnswearcounter + '"/>');
        }
    } else {
        // Check that we dont have below 2 answears
        if (multiAnswearcounter > 2) {
            multiAnswearcounter--;
            $("#dvMultiAnswearContainer :nth-last-child(-n+4)").remove();
        }
    }
}

// Function used for adding/removing american answer
function editAmericanAnswer(action) {
    if (action === 'add') {
        // Check that we dont have above 5 answers
        if (americanAnswerCounter < 5) {
            americanAnswerCounter++;
            $("#dvAmericanAnswerContainer").append(
                '<br/> ' +
                '<label class="form-label">תשובה ' + americanAnswerCounter + '</label> ' +
                '<input type="radio" name="rdbAmerican" id="rdbAmerican' + americanAnswerCounter + '"/>' +
                '<input type="text" id="txtAmerican' + americanAnswerCounter + '"/> ');
        }
    } else {
        // Check that we dont have below 4 answers
        if (americanAnswerCounter > 4) {
            americanAnswerCounter--;
            $("#dvAmericanAnswerContainer :nth-last-child(-n+4)").remove();
        }
    }
}

function addQuestion() {

    var urlString = "";
    var correctAnswers;
    var inCorrectAnswers;
    var question = new Object();
    question.QuestionText = $("#txtQuestion").val();
    question.SubSubjectId = $("#ddlSubSubjects").val();
    question.DifficultyId = $("#ddlDifficulties").val();
    question.TypeId = $("#ddlTypes").val();

    switch (question.TypeId) {
    case "open":
        urlString = "AddOpenQuestion";
        question.CorrectAnswer = $("#txtOpenAnswer").val();
    break;
    case "trueFalse":
        urlString = "AddTrueFalseQuestion";
        question.IsTrue = $("input:radio[name='rdbTrueFalse']:checked").val();
    break;
    case "american":
        urlString = "AddAmericanQuestion";
        question.CorrectAnswer = $("#txtAmericanCorrect").val();

        inCorrectAnswers = $("#dvAmericanIncorrectAnswers :text");

        for (var i = 0; i < inCorrectAnswers.length; i++) {
            question.IncorrectAnswers[i] = inCorrectAnswers[i];
        }
    break;
    case "moreThanOne":
        urlString = "AddMoreThanOneQuestion";
        correctAnswers = $("#dvAmericanIncorrectAnswers :text");

        for (var i = 0; i < correctAnswers.length; i++) {
            question.CorrectAnswers[i] = correctAnswers[i];
        }
        
        inCorrectAnswers = $("#dvAmericanIncorrectAnswers :text");

        for (var i = 0; i < inCorrectAnswers.length; i++) {
            question.IncorrectAnswers[i] = inCorrectAnswers[i];
        }
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
        }
    });
}

$(function () {
    changeCourse($("#ddlCourses").val());
    changeCourses();
});