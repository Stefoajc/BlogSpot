$(function () {
    $('.glyphicon-plus').click(function () {
        $('.commentinput' + $(this).attr('id')).toggle();
    });
});

$(function () {
    $('.glyphicon-edit').click(function () {
        $('.commenteditinput' + $(this).attr('id')).toggle();
    });
});