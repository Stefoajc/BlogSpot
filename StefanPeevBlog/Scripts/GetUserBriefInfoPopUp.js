$(function () {
    $(".lead > a").mouseover(function () {
        $.ajax({
            url: "../Account/GetUserInfoPopUp/"+$(this).attr('id'),
            cache: false,
            type: "GET",
            success: function (data, status) {
                alert(status);
            }
        });
    })
});