$(document).ready(function () {
    $('a#button').click(function () {
        $(this).toggleClass("down");
        if ($(this).attr('class') == 'down') {
            $.ajax({
                url: "/Account/Follow/" + $(".username").text(),
                cache: false,
                type: "POST",
                success: function (data, status) {
                    alert(status);
                }
            });
        } else {
            $.ajax({
                url: "/Account/UnFollow/" + $(".username").text(),
                cache: false,
                type: "POST",
                success: function (data, status) {
                    alert(status);
                }
            });
        }
    });
});