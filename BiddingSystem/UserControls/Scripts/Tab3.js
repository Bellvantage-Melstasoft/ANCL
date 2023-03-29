
Sys.Application.add_load(function () {
    //onload set date value
    var this1 = $("#ContentSection_Tab3_txtOInsuranceDate");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
    this1 = $("#ContentSection_Tab3_txtPerformanceDate");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
});

$(".customDate").on("change", function () {
    if (this.value) {
        $(this).attr('data-date', moment(this.value, 'YYYY-MM-DD').format($(this).attr('data-date-format')));
    } else {
        $(this).attr('data-date', '');
    }
}).trigger("change")

function dateChange(obj) {
    if (obj.value) {
        $(obj).attr('data-date', moment(obj.value, 'YYYY-MM-DD').format($(obj).attr('data-date-format')));
    } else {
        $(obj).attr('data-date', '');
    }
}
