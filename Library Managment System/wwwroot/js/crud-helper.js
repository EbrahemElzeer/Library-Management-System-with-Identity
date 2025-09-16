// crud-helper.js

function loadForm(controller, action, containerId) {
    $.ajax({
        url: `/${controller}/${action}`,
        type: 'GET',
        success: function (result) {
            $(`#${containerId}`).html(result);
        },
        error: function (xhr) {
            alert("خطأ في تحميل الفورم ❌");
            console.log(xhr.responseText);
        }
    });
}

function submitForm(formSelector, controller, saveAction, reloadUrl, containerId) {
    $(document).off('submit', formSelector).on('submit', formSelector, function (event) {
        event.preventDefault();
        const form = $(this);
        const data = form.serialize();
        const submitBtn = form.find('button[type="submit"]');
        submitBtn.prop('disabled', true);

        $.ajax({
            url: `/${controller}/${saveAction}`,
            type: 'POST',
            data: data,
            success: function (response) {
                if (response.success) {
                    $('#addFormContainer').html('');
                    $('#editFormContainer').html('');
                    $(`#${containerId}`).load(`/${controller}/${reloadUrl}`);
                } else {
                    alert(response.message || "حدث خطأ أثناء الحفظ");
                }
            },
            error: function () {
                alert("فشل الاتصال بالسيرفر");
            },
            complete: function () {
                submitBtn.prop('disabled', false);
            }
        });
    });
}

function deleteItem(controller, deleteAction, id, reloadAction, containerId) {
    if (confirm('هل أنت متأكد من الحذف؟')) {
        $.ajax({
            url: `/${controller}/${deleteAction}`,
            type: 'POST',
            data: { id: id },
            success: function (response) {
                if (response.success) {
                    alert('تم الحذف');
                    $(`#${containerId}`).load(`/${controller}/${reloadAction}`);
                } else {
                    alert('فشل في الحذف');
                }
            },
            error: function () {
                alert('فشل الاتصال بالسيرفر');
            }
        });
    }
}
