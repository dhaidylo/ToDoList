function updateTaskStatus() {
    $(":checkbox").on("change", async function () {
        const checkbox = $(this);
        const taskId = $(this).attr("id");

        try {
            checkbox.prop('disabled', true);

            const response = await fetch('/Entry/UpdateStatus', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(taskId)
            })

            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }
        }
        catch {
            handleError(error);
        }
        finally {
            checkbox.prop('disabled', false);
        }
    });
}

function deleteTask() {
    $(".btn-delete-task").on("click", async function () {
        const button = $(this);
        const taskId = $(this).attr("id");

        try {
            button.prop('disabled', true);

            const response = await fetch('/Entry/Delete', {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(taskId)
            })

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
    })
}

$(function () {
    updateTaskStatus();
    deleteTask();
})