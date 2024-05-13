async function loadEntries() {
    const listId = $('#select-list').val();

    const response = await fetch('/Home/GetTasks', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(listId)
    });
    if (response.ok) {
        const data = await response.text();
        $('#entries-list').html(data);
    }
    else {
        throw new Error('Network response was not ok');
    }
}

function createTask() {
    $('#create-task').on("click", async () => {
        const textInput = $('#task-name');
        const taskName = textInput.val();
        textInput.val('');
        const listId = $('#select-list').val();

        const response = await fetch('/Entry/Create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ Text: taskName, ListId: listId })
        });
        if (response.ok) {
            loadEntries();
        }
        else {
            throw new Error('Network response was not ok');
        }
    }); 
}

function createList() {
    $('#btn-create-list').on("click", async () => {
        const textInput = $('#list-name');
        const listName = textInput.val();
        textInput.val('');

        const response = await fetch('/List/Create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ Name: listName })
        });
        if (response.ok) {
            const data = await response.json();
            const newList = $('<option>', {
                value: data.id,
                text: data.name
            });

            $('#select-list').append(newList);
        }
        else {
            throw new Error('Network response was not ok');
        }
    })
}

function deleteList() {
    $('#btn-delete-list').on("click", async () => {
        const select = $('#select-list');
        const listId = select.val();

        const response = await fetch('/List/Delete', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(listId)
        });
        if (response.ok) {
            const selectedOption = select.find('option:selected');
            selectedOption.remove();
            loadEntries();
        }
        else {
            throw new Error('Network response was not ok');
        }
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
