async function loadEntries() {
    try {
        const listId = $('#select-list').val();

        if (!listId) {
            throw new Error('List ID is required');
        }

        const response = await fetch('/Home/GetTasks', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(listId)
        });
        if (!response.ok) {
            throw new Error(`Network response was not ok: ${response.statusText}`);
        }
        const data = await response.text();

        if (!data) {
            throw new Error('No data received from the server');
        }

        $('#entries-list').html(data);
    }
    catch {
        handleError(error);
    }
}

function createTask() {
    $('#create-task').on("click", async function () {
        const button = $(this);

        try {
            const textInput = $('#task-name');
            const taskName = textInput.val();
            const listId = $('#select-list').val();

            if (!taskName) {
                alert('Task name cannot be empty');
                return;
            }

            if (!listId) {
                alert('Please select a list');
                return;
            }

            textInput.val('');

            button.prop('disabled', true);

            const response = await fetch('/Entry/Create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ Text: taskName, ListId: listId })
            });

            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }

            await loadEntries();
        }
        catch {
            handleError(error);
        }
        finally {
            button.prop('disabled', false);
        }
    }); 
}

function createList() {
    $('#btn-create-list').on("click", async function () {
        const button = $(this);
        try {
            const textInput = $('#list-name');
            const listName = textInput.val();

            if (!listName) {
                alert('Please, enter a list name');
                return;
            }

            textInput.val('');

            button.prop('disabled', true);

            const response = await fetch('/List/Create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ Name: listName })
            });

            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }

            const data = await response.json();

            if (!data) {
                throw new Error('No data received from the server');
            }

            const newList = $('<option>', {
                value: data.id,
                text: data.name
            });

            $('#select-list').append(newList);
        }
        catch {
            handleError(error);
        }
        finally {
            button.prop('disabled', false);
        }
    })
}

function deleteList() {
    $('#btn-delete-list').on("click", async function () {
        const button = $(this);
        try {
            const select = $('#select-list');
            const listId = select.val();

            if (!listId) {
                alert('Please, select a list');
            }

            button.prop('disabled', true);

            const response = await fetch('/List/Delete', {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(listId)
            });

            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }

            const selectedOption = select.find('option:selected');
            selectedOption.remove();
            await loadEntries();
        }
        catch {
            handleError(error);
        }
        finally {
            button.prop('disabled', false);
        }
    })
}

function handleError(error) {
    console.error(`Error creating task: ${error}`);
    alert(`An error occurred while creating the task: ${error.message}`);
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
