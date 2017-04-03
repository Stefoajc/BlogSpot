AddAntiForgeryToken = function(data) {
    data.__RequestVerificationToken = $('#__AjaxAntiForgeryForm input[name=__RequestVerificationToken]').val();
    return data;
};

$(function () {
    $('.glyphicon-edit').click(function () {
        var id = $(this).attr('id');
        $.ajax({
            url: "/Comments/Edit",
            cache: false,
            type: "POST",
            data: AddAntiForgeryToken({id: parseInt($(this).attr('id'))}),
            success: function (data, status) {
                if (status == 'success') {
                    var selector = '.media' + id;
                    $(selector).remove();
                }
            }
        });
    });
});