
Sys.Application.add_load(function () {
    //onload set date value

    var this1 = $("#ContentSection_Tab2_txtDateValue1");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
    this1 = $("#ContentSection_Tab2_txtDateValue2");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
    this1 = $("#ContentSection_Tab2_txtDateValue3");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
    this1 = $("#ContentSection_Tab2_txtDateValue4");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
    this1 = $("#ContentSection_Tab2_txtDateValue5");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
    this1 = $("#ContentSection_Tab2_txtDateValue6");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
    this1 = $("#ContentSection_Tab2_txtDateValue7");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
    this1 = $("#ContentSection_Tab2_txtDateValue8");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
    this1 = $("#ContentSection_Tab2_txtDateValue9");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
    this1 = $("#ContentSection_Tab2_txtDateValue10");
    if (this1.val() != "") {
        this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
    }
    this1 = $("#ContentSection_Tab2_txtDateValue11");
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
}).trigger("change");
