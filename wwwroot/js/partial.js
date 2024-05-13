function updateTaskStatus() {
    $(":checkbox").on("change", async function () {
        const id = await $(this).attr("id");
        const response = await fetch('/Entry/UpdateStatus', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(id)
        })
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
    });
}

function deleteTask() {
    $(".btn-delete-task").on("click", async function () {
        const id = $(this).attr("id");
        const response = await fetch('/Entry/Delete', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(id)
        })
        if (response.ok) {
            loadEntries();
        }
        else {
            throw new Error('Network response was not ok');
        }
    })
}

$(function () {
    updateTaskStatus();
    deleteTask();
})