$(function () {
    $(".btn-primary").click(function myfunction() {
        $(this).prev().toggleClass('post-body');
        //$(this).children().first().toggleClass('glyphicon-chevron-left glyphicon-chevron-right');
    });
});