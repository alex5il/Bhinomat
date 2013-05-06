function changeCourse(courseId) {
    $.ajax({
        url: 'SetTheCourseSession',
        type: 'POST',
        data: JSON.stringify({ courseId: courseId }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
        },
        error: function(parameters) {
        }
    });
}