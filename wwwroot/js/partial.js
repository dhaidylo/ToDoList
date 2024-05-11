function updateTaskStatus() {
    $(":checkbox").on("change", function () {
        var id = $(this).attr("id");
        $.ajax({
            url: '/Entry/UpdateStatus',
            type: 'POST',
            data: { id: id },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });
    });
}

function deleteTask() {
    $(".btn-delete-task").on("click", function () {
        var id = $(this).attr("id");
        $.ajax({
            url: '/Entry/Delete',
            type: 'POST',
            data: { id: id },
            success: function () {
                loadEntries();
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        })
    })
}

$(function () {
    updateTaskStatus();
    deleteTask();
})