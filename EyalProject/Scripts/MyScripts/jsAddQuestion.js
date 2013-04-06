var multiAnswearcounter = 4;

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
                '<option value="' + this.Value + '">'
                + this.Text +
                '</option>');
            });
        }
    });
}