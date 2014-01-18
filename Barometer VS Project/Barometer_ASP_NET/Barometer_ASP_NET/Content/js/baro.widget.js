/***
 * Baro meter widget 
 * Syntax als volgt : 
 * <a title="" class="displayBaroWidget" data-modal-id="modal-baro-widget" data-reporter-id="" data-subject-id="" data-project-id="">
        <span class="glyphicon glyphicon-eye-open"></span>
   </a>
 * 
 */
$(function () {
    var getBaroWidget = function () {
        var $a = $(this);
        var options = {
            url: "/Baro/WidgetHolder",
            data: {
                reporter_id: $a.data('reporter-id'),
                subject_id: $a.data('subject-id'),
                project_id: $a.data('project-id')
            },
            type: "GET",
            dataType: "HTML"
        };

        $.ajax(options).done(function (data) {
            var modal = "#" + $a.data('modal-id');
            var target = $(modal).find(".modal-content");
            $(target).html(data);
            $(modal).modal("show");
        });

        return false;
    };



    $('.displayBaroWidget').on('click', getBaroWidget);

    $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Collapse this branch');
    $('.tree li.parent_li > span').on('click', function (e) {
        var children = $(this).parent('li.parent_li').find(' > ul > li');
        if (children.is(":visible")) {
            children.hide('fast');
            $(this).attr('title', 'Expand this branch').find(' > i').addClass('icon-plus-sign').removeClass('icon-minus-sign');
        } else {
            children.show('fast');
            $(this).attr('title', 'Collapse this branch').find(' > i').addClass('icon-minus-sign').removeClass('icon-plus-sign');
        }
        e.stopPropagation();
    });
});