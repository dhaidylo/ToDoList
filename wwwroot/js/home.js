function loadEntries() {
    var listId = $('#select-list').val();

    $.ajax({
        url: '/Home/GetTasks',
        type: 'GET',
        data: { listId: listId },
        success: function (data) {
            $('#entries-list').html(data);
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
}

function createTask() {
    $('#create-task').on("click", function () {
        var textInput = $('#task-name');
        var taskName = textInput.val();
        textInput.val('');
        var listId = $('#select-list').val();

        $.ajax({
            url: '/Entry/Create',
            type: 'POST',
            data: { Text: taskName, ListId: listId },
            success: function () {
                loadEntries();
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });
    });
}

function createList() {
    $('#btn-create-list').on("click", function () {
        var textInput = $('#list-name');
        var listName = textInput.val();
        textInput.val('');

        $.ajax({
            url: '/List/Create',
            type: 'POST',
            data: { Name: listName },
            success: function (data) {
                var newList = $('<option>', {
                    value: data.id,
                    text: data.name
                });

                $('#select-list').append(newList);
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        })
    })
}

function deleteList() {
    $('#btn-delete-list').on("click", function () {
        var select = $('#select-list');
        var listId = select.val();

        $.ajax({
            url: '/List/Delete',
            type: 'POST',
            data: { id: listId },
            success: function () {
                var selectedOption = select.find('option:selected');
                selectedOption.remove();
                loadEntries();
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        })
    })
}

$(function () {
    loadEntries();

    createTask();
    createList();
    deleteList();

    $('#select-list').on("change", function () {
        loadEntries();
    });

    $('#btn-new-list').on("click", function () {
        $('#new-list').toggle();
    })
})
