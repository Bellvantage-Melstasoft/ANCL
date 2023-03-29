﻿
var inEditMode = false;
var editIndex = 0;
var prMaster = null;
var prDetails = [];
var itemSpecs = [];
var replacementImages = [];
var fileUploads = [];
var supportiveDocs = [];
var capexDocs = [];

loadWarehouse();
loadCategory();
PrTypeOnchange();

PurchaseTypeOnchange();
$('#dvImportItem').addClass('hidden');

ImportItemTypeOnchange();
$('#dvSparepart').addClass('hidden');


function showMessage(type, title, message) {
    swal.fire({
        title: title,
        html: message,
        type: type,
        showCancelButton: false,
        confirmButtonClass: 'btn btn-info btn-styled',
        buttonsStyling: false
    })
}

function showServerError() {
    showMessage('error', 'ERROR!', 'Internal Server Error Occured');
}

function showAjaxError() {
   // showMessage('error', 'ERROR!', 'A Connection Error Occured. Please Check your Internet Connection');
}

function showSessionExpiry() {
    //Login Module Should come get
    //showMessage('error', 'ERROR!', 'Session Expired');

    login();
}

var warehouseAjax = null;
function loadWarehouse() {
    $('#warehouseLoad').removeClass('hidden');

    warehouseAjax = $.ajax({
        type: "GET",
        url: 'CreatePR_V2.aspx/GetWarehouses',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        beforeSend: function () {
            if (warehouseAjax != null) {
                warehouseAjax.abort();
            }
        },
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                var warehouses = response.Data;

                var html = ``;
                for (var i = 0; i < warehouses.length; i++) {
                    if (i == 0) {
                        html += `<option value="` + warehouses[i].WarehouseID + `" selected>` + warehouses[i].WarehouseName + `</option>`;
                    }
                    else {
                        html += `<option value="` + warehouses[i].WarehouseID + `">` + warehouses[i].WarehouseName + `</option>`;
                    }
                }

                $('#ddlWarehouse').html(html);
                $('.select2').select2();

                $('#warehouseLoad').addClass('hidden');
            }
            else if (response.Status == 500) {
                showServerError();
                $('#warehouseLoad').addClass('hidden');
            }
            else {
                showSessionExpiry();
                $('#warehouseLoad').addClass('hidden');
            }
        },
        error: function (error) {
            showAjaxError();
            $('#warehouseLoad').addClass('hidden');
        }
    });

}

var categoryAjax = null;
function loadCategory() {
    $('#txtSparePart').val('');
    if (!$('#ddlPRCategory').prop('disabled')) {
        $('#categoryLoad').removeClass('hidden');

        $('#ddlPRSubCategory').html('');

        categoryAjax = $.ajax({
            type: "GET",
            url: 'CreatePR_V2.aspx/GetCategories',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            beforeSend: function () {
                if (categoryAjax != null) {
                    categoryAjax.abort();
                }
            },
            success: function (response) {
                response = JSON.parse(response.d);
                if (response.Status == 200) {
                    var categories = response.Data;

                    var html = ``;
                    for (var i = 0; i < categories.length; i++) {
                        if (i == 0) {
                            html += `<option value="` + categories[i].CategoryId + `" selected>` + categories[i].CategoryName + `</option>`;
                        }
                        else {
                            html += `<option value="` + categories[i].CategoryId + `">` + categories[i].CategoryName + `</option>`;
                        }
                    }

                    $('#ddlPRCategory').html(html);
                    $('.select2').select2();

                    $('#categoryLoad').addClass('hidden');
                    loadSubCategory();
                }
                else if (response.Status == 500) {
                    showServerError();
                    $('#categoryLoad').addClass('hidden');
                    $('.select2').select2();
                }
                else {
                    showSessionExpiry();
                    $('#categoryLoad').addClass('hidden');
                    $('.select2').select2();
                }
            },
            error: function (error) {
                showAjaxError();
                $('#categoryLoad').addClass('hidden');
                $('.select2').select2();
            }
        });
    }
    else {
        $('#categoryNotAllowed').removeClass('hidden');

        setTimeout(function () {
            $('#categoryNotAllowed').addClass('hidden');
        }, 3000);

    }

}

var subCategoryAjax = null;
function loadSubCategory() {

    if (!$('#ddlPRSubCategory').prop('disabled')) {
        $('#subCategoryLoad').removeClass('hidden');
        $('#ddlItem').html('');
        if ($('#ddlPRCategory option:selected').val() != "" && $('#ddlPRCategory option:selected').val() != undefined && $('#ddlPRCategory option:selected').val() != null) {

            subCategoryAjax = $.ajax({
                type: "GET",
                url: 'CreatePR_V2.aspx/GetSubCategories?CategoryId=' + $('#ddlPRCategory option:selected').val(),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                beforeSend: function () {
                    if (subCategoryAjax != null) {
                        subCategoryAjax.abort();
                    }
                },
                success: function (response) {
                    response = JSON.parse(response.d);
                    if (response.Status == 200) {
                        var subCategories = response.Data;

                        var html = ``;
                        for (var i = 0; i < subCategories.length; i++) {
                            if (i == 0) {
                                html += `<option value="` + subCategories[i].SubCategoryId + `" selected>` + subCategories[i].SubCategoryName + `</option>`;
                            }
                            else {
                                html += `<option value="` + subCategories[i].SubCategoryId + `">` + subCategories[i].SubCategoryName + `</option>`;
                            }
                        }

                        $('#ddlPRSubCategory').html(html);
                        $('.select2').select2();

                        $('#subCategoryLoad').addClass('hidden');
                        loadItem();
                    }
                    else if (response.Status == 500) {
                        showServerError();
                        $('#subCategoryLoad').addClass('hidden');
                        $('.select2').select2();
                    }
                    else {
                        showSessionExpiry();
                        $('#subCategoryLoad').addClass('hidden');
                        $('.select2').select2();
                    }
                },
                error: function (error) {
                    showAjaxError();
                    $('#subCategoryLoad').addClass('hidden');
                    $('.select2').select2();
                }
            });
        }
        else {

            $('#ddlPRSubCategory').html('');
            $('.select2').select2();
            $('#subCategoryLoad').addClass('hidden');
        }
    }
    else {
        $('#subCategoryNotAllowed').removeClass('hidden');

        setTimeout(function () {
            $('#subCategoryNotAllowed').addClass('hidden');
        }, 3000);

    }
}


function PrTypeOnchange() {
    //debugger;
    loadItem();
    if ($('#ddlPRType option:selected').val() == "1") {
        $('#rdoCapex').prop('disabled', 'disabled');

        //$('.budgetRequired').addClass('hidden');
        //$('.expenseRemarksRequired').removeClass('hidden');
        //$('#txtBudgetAmount').prop('disabled', true);
        //$('#txtBudgetInformation').prop('disabled', true);
        //$('#dvCapexDoc').addClass('hidden');

        $('#rdoCapex').removeProp('checked');
        $('#rdoOpex').prop('checked', 'checked');
        //$("#rdoOpex").attr('checked', 'checked');
        setExpenseRequired();
    }

    if ($('#ddlPRType option:selected').val() == "2") {
        $('#rdoCapex').removeProp('disabled');
        $('#rdoOpex').removeProp('checked');
        $('#rdoCapex').prop('checked', 'checked');
        setExpenseRequired();
    }

}

function PurchaseTypeOnchange() {
    $('#txtSparePart').val('');
    //loadItem();
    if ($('#ddlPurchasingType option:selected').val() == "1") {
        $('#dvImportItem').addClass('hidden');
        $('#dvSparepart').addClass('hidden');

    }

    else if ($('#ddlPurchasingType option:selected').val() == "2") {
        $('#dvImportItem').removeClass('hidden');
    }

    else {
        $('#dvImportItem').addClass('hidden');
        $('#dvSparepart').addClass('hidden');
    }

}
function ImportItemTypeOnchange() {
    $('#txtSparePart').val('');

    if ($('#ddlItemType option:selected').val() == "1") {
        $('#dvSparepart').removeClass('hidden');
    }

    else {
        $('#dvSparepart').addClass('hidden');
    }
}


var itemAjax = null;
function loadItem() {
    //debugger;
    if (!inEditMode) {
        $('#itemLoad').removeClass('hidden');
        if ($('#ddlPRCategory option:selected').val() != "" && $('#ddlPRCategory option:selected').val() != undefined && $('#ddlPRCategory option:selected').val() != null) {
            if ($('#ddlPRSubCategory option:selected').val() != "" && $('#ddlPRSubCategory option:selected').val() != undefined && $('#ddlPRSubCategory option:selected').val() != null) {
                if ($('#ddlPRType option:selected').val() != "" && $('#ddlPRType option:selected').val() != undefined && $('#ddlPRType option:selected').val() != null) {

                    itemAjax = $.ajax({
                        type: "GET",
                        url: 'CreatePR_V2.aspx/GetItems?CategoryId=' + $('#ddlPRCategory option:selected').val() + '&SubCategoryId=' + $('#ddlPRSubCategory option:selected').val() + '&ItemType=' + $('#ddlPRType option:selected').val(),
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        beforeSend: function () {
                            if (itemAjax != null) {
                                itemAjax.abort();
                            }
                        },
                       
                        success: function (response) {
                            
                                response = JSON.parse(response.d);
                            if (response.Status == 200) {
                                var items = response.Data;

                                var html = ``;
                                for (var i = 0; i < items.length; i++) {
                                    if (i == 0) {
                                        html += `<option value="` + items[i].ItemId + `" measurement="` + items[i].MeasurementShortName + `" selected>` + items[i].ItemName + `</option>`;
                                    }
                                    else {
                                        html += `<option value="` + items[i].ItemId + `" measurement="` + items[i].MeasurementShortName + `" >` + items[i].ItemName + `</option>`;
                                    }
                                }
                                
                                $('#ddlItem').html(html);
                                $('.select2').select2();

                                $('#itemLoad').addClass('hidden');
                                    if ($('#ddlItem option:selected').val() != "" && $('#ddlItem option:selected').val() != undefined) {
                                    updateMeasurement();

                                }
                                }
                                else if (response.Status == 500) {
                                    showServerError();
                                    $('#itemLoad').addClass('hidden');
                                }
                                else {
                                    showSessionExpiry();
                                    $('#itemLoad').addClass('hidden');
                                }
                               
                        },
                        error: function (error) {
                            showAjaxError();
                            $('#itemLoad').addClass('hidden');
                        }
                    });



                }
                else {

                    $('#ddlItem').html('');
                    $('.select2').select2();
                    $('#itemLoad').addClass('hidden');
                }

            }
            else {

                $('#ddlItem').html('');
                $('.select2').select2();
                $('#itemLoad').addClass('hidden');
            }
        }
        else {

            $('#ddlItem').html('');
            $('.select2').select2();
            $('#itemLoad').addClass('hidden');
        }
    }
    else {
        $('#itemNotAllowed').removeClass('hidden');

        setTimeout(function () {
            $('#itemNotAllowed').addClass('hidden');
        }, 3000);

    }
}

var purchaseHistoryAjax = null;
function loadPurchaseHistory() {

    if ($('#ddlItem option:selected').val() != "" && $('#ddlItem option:selected').val() != undefined && $('#ddlItem option:selected').val() != null) {

        purchaseHistoryAjax = $.ajax({
            type: "GET",
            url: 'CreatePR_V2.aspx/GetItemPurchaseHistories?ItemId=' + $('#ddlItem option:selected').val(),
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            beforeSend: function () {
                if (purchaseHistoryAjax != null) {
                    purchaseHistoryAjax.abort();
                }
            },
            success: function (response) {
                response = JSON.parse(response.d);
                if (response.Status == 200) {
                    var purchaseHistory = response.Data;

                    //   //debugger;
                    var html = ``;
                    if (purchaseHistory != null && purchaseHistory.length > 0) {
                        for (var i = 0; i < purchaseHistory.length; i++) {
                            html += `<tr>
                                        <td>` + purchaseHistory[i].PoCode + `</td>
                                        <td>` + purchaseHistory[i].CreatedDate.split('T')[0] + `</td>
                                        <td>` + purchaseHistory[i].Quantity + `</td>
                                        <td>` + purchaseHistory[i].ItemPrice + `</td>
                                    </tr>`;
                        }
                    }
                    else {
                        html += `<td colspan="4">Not Found</td>`;
                    }

                    $('#tbPurchaseHistory').html(html);

                    $('#mdlPurchaseHistory').modal('show');
                }
                else if (response.Status == 500) {
                    showServerError();
                }
                else {
                    showSessionExpiry();
                }
            },
            error: function (error) {
                showAjaxError();
            }
        });
    }

}

//function updateMeasurement() {
//    $('#txtMeasurement').val($('#ddlItem option:selected').attr('measurement'));
//}

$(function () {


    $('.date1').datepicker({ dateFormat: 'd-M-yy' })
    $('.select2').select2();

    $('.btn-circle').on('click', function () {
        $('.btn-circle.btn-info').removeClass('btn-info').addClass('btn-default');
        $(this).addClass('btn-info').removeClass('btn-default').blur();
    });

    $('.next-step, .prev-step').on('click', function (e) {
        var $activeTab = $('.tab-pane.active');

        $('.btn-circle.btn-info').removeClass('btn-info').addClass('btn-default');

        if ($(e.target).hasClass('next-step')) {
            var nextTab = $activeTab.next('.tab-pane').attr('id');
            $('[href="#' + nextTab + '"]').addClass('btn-info').removeClass('btn-default');
            $('[href="#' + nextTab + '"]').tab('show');
        }
        else {
            var prevTab = $activeTab.prev('.tab-pane').attr('id');
            $('[href="#' + prevTab + '"]').addClass('btn-info').removeClass('btn-default');
            $('[href="#' + prevTab + '"]').tab('show');
        }

        window.scrollTo({ top: 0, behavior: 'smooth' });
    });

    $('.nav-tabs').on('shown.bs.tab', function (event) {
        $('.select2').select2();
        $('[data-toggle="tooltip"]').tooltip();
    });

    $("#rdoCapex").change(function () {
        setExpenseRequired();
    });

    $("#rdoOpex").change(function () {
        setExpenseRequired();
    });

    $("#rdoBudgetYes").change(function () {
        setExpenseRequired();
    });

    $("#rdoBudgetNo").change(function () {
        setExpenseRequired();
    });

    setExpenseRequired();

});

function setExpenseRequired() {
    $('#txtBudgetAmount').parent().removeClass('has-error');
    $('#txtBudgetInformation').parent().removeClass('has-error');
    $('.expenseRemarksRequired').parent().removeClass('has-error');

    if ($('#rdoCapex').prop('checked') == true) {
        $('#spnBudgetRequired').removeClass('hidden');
        $('#rdoBudgetYes').removeProp('disabled');
        $('#rdoBudgetNo').removeProp('disabled');
        $('#dvCapexDoc').removeClass('hidden');

        if ($('#rdoBudgetYes').prop('checked') == true) {
            $('.budgetRequired').removeClass('hidden');
            $('.expenseRemarksRequired').addClass('hidden');
            $('#txtBudgetAmount').removeProp('disabled');
            $('#txtBudgetInformation').removeProp('disabled');
        }
        else {
            $('.budgetRequired').addClass('hidden');
            $('.expenseRemarksRequired').removeClass('hidden');
            $('#txtBudgetAmount').prop('disabled', true);
            $('#txtBudgetInformation').prop('disabled', true);
        }
    }
    else {
        $('#spnBudgetRequired').addClass('hidden');
        $('.budgetRequired').addClass('hidden');
        $('.expenseRemarksRequired').addClass('hidden');
        $('#txtBudgetAmount').prop('disabled', true);
        $('#txtBudgetInformation').prop('disabled', true);
        $('#rdoBudgetYes').prop('disabled', true);
        $('#rdoBudgetNo').prop('disabled', true);
        $('#dvCapexDoc').addClass('hidden');
    }
}

function generateUniqueKey() {
    var date = new Date();
    return parseInt((date.getMinutes() + "" + date.getSeconds() + "" + date.getMilliseconds()));
}

function replacementFileUploaded(input) {
    if ((input.files.length + replacementImages.length) <= 5) {
        for (var i = 0; i < input.files.length; i++) {

            var replacementImage = new PrReplacementFileUpload();

            replacementImage.PrdId = generateUniqueKey();
            replacementImage.FileName = input.files[i].name;
            replacementImage.FilePath = URL.createObjectURL(input.files[i]);
            replacementImage.Todo = 1;

            var reader = new FileReader();

            reader.onload = function (e) {

                $('#base64ReplacementImageStorage').append(`<div>` + e.target.result.substr(e.target.result.indexOf(',') + 1) + `</div>`);
            }
            reader.readAsDataURL(input.files[i]);

            replacementImages.push(replacementImage);
        }

        $('#spnReplacementImgCount').text(' ' + replacementImages.length);
    }
    else {
        showMessage('error', 'Maximum upload limit exceeded.', 'The maximum upload limit is 5 files')
    }
}

function populateReplacementImages() {
    var html = '';
    if (replacementImages.length > 0) {
        for (var i = 0; i < replacementImages.length; i++) {
            html += `
                            <div class="thumb-container">
                                <img src="`+ replacementImages[i].FilePath + `" />
                                <button class="btn-remove" onclick="removeReplacementImageAt(`+ i + `);"><span class="glyphicon glyphicon-remove"></span></button>
                            </div>`;
        }
    }
    else {
        html += `<h4>Not Found.</h4>`;
    }
    $('#dvReplacementImages').html(html);
}

function viewReplacementImages() {
    populateReplacementImages();
    $('#mdlReplacementImages').modal('show');
}

function removeReplacementImageAt(index) {
    replacementImages.splice(index, 1);
    $('#dvReplacementImages').children().eq(index).remove();
    $('#base64ReplacementImageStorage').children().eq(index).remove();
    $('#spnReplacementImgCount').text(' ' + replacementImages.length);
    populateReplacementImages();
}

function standardImageUploaded(input) {
    if ((input.files.length + fileUploads.length) <= 5) {
        for (var i = 0; i < input.files.length; i++) {

            var standardImage = new PrFileUpload();

            standardImage.PrdId = generateUniqueKey();
            standardImage.FileName = input.files[i].name;
            standardImage.FilePath = URL.createObjectURL(input.files[i]);
            standardImage.Todo = 1;

            var reader = new FileReader();

            reader.onload = function (e) {

                $('#base64StandardImageStorage').append(`<div>` + e.target.result.substr(e.target.result.indexOf(',') + 1) + `</div>`);
            }
            reader.readAsDataURL(input.files[i]);

            fileUploads.push(standardImage);
        }

        $('#spnStandardImgCount').text(' ' + fileUploads.length);
    }
    else {
        showMessage('error', 'Maximum upload limit exceeded.', 'The maximum upload limit is 5 files')
    }
}

function populateStandardImages() {
    var html = '';
    if (fileUploads.length > 0) {
        for (var i = 0; i < fileUploads.length; i++) {
            html += `
                            <div class="thumb-container">
                                <img src="`+ fileUploads[i].FilePath + `" />
                                <button class="btn-remove" onclick="removeStandardImageAt(`+ i + `);"><span class="glyphicon glyphicon-remove"></span></button>
                            </div>`;
        }
    }
    else {
        html += `<h4>Not Found.</h4>`;
    }
    $('#dvStandardImages').html(html);
}

function viewStandardImages() {
    populateStandardImages();
    $('#mdlFileUpload').modal('show');
}

function removeStandardImageAt(index) {
    fileUploads.splice(index, 1);
    $('#dvStandardImages').children().eq(index).remove();
    $('#base64StandardImageStorage').children().eq(index).remove();
    $('#spnStandardImgCount').text(' ' + fileUploads.length);
    populateStandardImages();
}

function supportiveDocsUploaded(input) {
    if ((input.files.length + supportiveDocs.length) <= 5) {
        for (var i = 0; i < input.files.length; i++) {

            var doc = new PrSupportiveDocs();

            doc.PrdId = generateUniqueKey();
            doc.FileName = input.files[i].name;
            doc.FilePath = URL.createObjectURL(input.files[i]);
            doc.Todo = 1;

            var reader = new FileReader();

            reader.onload = function (e) {

                $('#base64SupportiveDocsStorage').append(`<div>` + e.target.result.substr(e.target.result.indexOf(',') + 1) + `</div>`);
            }
            reader.readAsDataURL(input.files[i]);

            supportiveDocs.push(doc);
        }

        $('#spnSupportiveDocCount').text(' ' + supportiveDocs.length);
    }
    else {
        showMessage('error', 'Maximum upload limit exceeded.', 'The maximum upload limit is 5 files')
    }
}

function populatesupportiveDocs() {
    var html = '';
    if (supportiveDocs.length > 0) {
        for (var i = 0; i < supportiveDocs.length; i++) {
            html += `
                            <tr>
                                <td>`+ supportiveDocs[i].FileName + `</td>
                                <td>
                                    <a href="#" style="margin-right: 5px;" class="text-aqua" data-toggle="tooltip" data-placement="top" title="View" onclick="viewSupportiveDocAt(event,`+ i + `);"><span class="glyphicon glyphicon-search"></span></a>
                                    <a href="#" class="text-red" data-toggle="tooltip" data-placement="top" title="Remove" onclick="removesupportiveDocAt(event,`+ i + `);"><span class="glyphicon glyphicon-trash"></span></a>
                                </td>
                            </tr>`;
        }
    }
    else {
        html += `<tr><td colspan="2">Not Found.</td></tr>`;
    }
    $('#tbSupportiveDocs').html(html);
}

function viewsupportiveDocs() {
    populatesupportiveDocs();
    $('#mdlSupportiveDocs').modal('show');
}

function viewSupportiveDocAt(e, index) {
    e.preventDefault();
    var tab = window.open(supportiveDocs[index].FilePath);
}

function removesupportiveDocAt(e, index) {
    e.preventDefault();
    supportiveDocs.splice(index, 1);
    $('#tbSupportiveDocs').children().eq(index).remove();
    $('#base64SupportiveDocsStorage').children().eq(index).remove();
    $('#spnSupportiveDocCount').text(' ' + supportiveDocs.length);
    populatesupportiveDocs();
}

function capexDocUploaded(input) {
    if ((input.files.length + capexDocs.length) <= 5) {
        for (var i = 0; i < input.files.length; i++) {

            var doc = new PrCapexDoc();

            doc.PrId = generateUniqueKey();
            doc.FileName = input.files[i].name;
            doc.FilePath = URL.createObjectURL(input.files[i]);
            doc.Todo = 1;

            var reader = new FileReader();

            reader.onload = function (e) {

                $('#base64CapexDocsStorage').append(`<div>` + e.target.result.substr(e.target.result.indexOf(',') + 1) + `</div>`);
            }
            reader.readAsDataURL(input.files[i]);

            capexDocs.push(doc);
        }

        $('#spnCapexDocsCount').text(' ' + capexDocs.length);
    }
    else {
        showMessage('error', 'Maximum upload limit exceeded.', 'The maximum upload limit is 5 files')
    }
}

function populateCapexDocs() {
    var html = '';
    if (capexDocs.length > 0) {
        for (var i = 0; i < capexDocs.length; i++) {
            html += `
                            <tr>
                                <td>`+ capexDocs[i].FileName + `</td>
                                <td>
                                    <a href="#" style="margin-right: 5px;" class="text-aqua" data-toggle="tooltip" data-placement="top" title="View" onclick="viewCapexDocAt(event,`+ i + `);"><span class="glyphicon glyphicon-search"></span></a>
                                    <a href="#" class="text-red" data-toggle="tooltip" data-placement="top" title="Remove" onclick="removeCapexDocAt(event,`+ i + `);"><span class="glyphicon glyphicon-trash"></span></a>
                                </td>
                            </tr>`;
        }
    }
    else {
        html += `<tr><td colspan="2">Not Found.</td></tr>`;
    }
    $('#tbCapexDocs').html(html);
}

function viewCapexDocs() {
    populateCapexDocs();
    $('#mdlCapexDocs').modal('show');
}

function viewCapexDocAt(e, index) {
    e.preventDefault();
    var tab = window.open(capexDocs[index].FilePath);
}

function removeCapexDocAt(e, index) {
    e.preventDefault();
    capexDocs.splice(index, 1);
    $('#tbCapexDocs').children().eq(index).remove();
    $('#base64CapexDocsStorage').children().eq(index).remove();
    $('#spnCapexDocsCount').text(' ' + capexDocs.length);
    populateCapexDocs();
}

//debugger;
function populateSpecs() {
    var html = '';
    if (itemSpecs.length > 0) {
        for (var i = 0; i < itemSpecs.length; i++) {
            if (itemSpecs[i].ItemId == $('#ddlItem option:selected').val()) {
                html += `
                            <tr>
                                <td>`+ itemSpecs[i].Material + `</td>
                                <td>`+ itemSpecs[i].Description + `</td>
                                <td>
                                    <a href="#" class="text-red" data-toggle="tooltip" data-placement="top" title="Remove" onclick="removeSpec(event,`+ i + `);"><span class="glyphicon glyphicon-trash"></span></a>
                                </td>
                            </tr>`;
            }
        }
    }
    else {
        html += '<tr><td colspan="3">Not Found</td></tr>';
    }

    $('#tbSpecs').html(html);
}

function viewItemSpec() {
    populateSpecs();
    $('#mdlItemSpec').modal('show');
}

function addSpec() {
    if ($('#txtSpec').val() != '' && $('#txtSpecDescription').val() != '') {
        var bom = new PrBom();
        bom.Material = $('#txtSpec').val();
        bom.Description = $('#txtSpecDescription').val();

        bom.IsAdded = 2;
        bom.ItemId = $('#ddlItem option:selected').val();

        itemSpecs.push(bom);

        $('#spnSpecCount').text(' ' + itemSpecs.length);

        populateSpecs();

        $('#txtSpec').val('');
        $('#txtSpecDescription').val('');

        $('#txtSpec').parent().removeClass('has-error');
        $('#txtSpecDescription').parent().removeClass('has-error');
    }
    else {
        $('#txtSpec').parent().addClass('has-error');
        $('#txtSpecDescription').parent().addClass('has-error');
    }
}

function removeSpec(e, index) {
    e.preventDefault();
    itemSpecs.splice(index, 1);
    $('#spnSpecCount').text(' ' + itemSpecs.length);
    populateSpecs();
}

function validateItem() {
    var isValid = true;

    if ($('#ddlItem option:selected').length > 0) {
        $('#ddlItem').parent().removeClass('has-error');
    }
    else {
        $('#ddlItem').parent().addClass('has-error');
        isValid = false;
    }
    if (/^[0-9]+(\.)?[0-9]*$/.test($('#txtUnitPrice').val())) {
        $('#txtUnitPrice').parent().removeClass('has-error');
    }
    else {
        $('#txtUnitPrice').parent().addClass('has-error');
        isValid = false;
    }
    if (/^[0-9]+(\.)?[0-9]*$/.test($('#txtQty').val())) {
        $('#txtQty').parent().removeClass('has-error');
    }
    else {
        $('#txtQty').parent().addClass('has-error');
        isValid = false;
    }
    if ($('#txtWarehouseStock').val()) {
        $('#txtWarehouseStock').parent().removeClass('has-error');
    }
    else {
        $('#txtWarehouseStock').parent().addClass('has-error');
        isValid = false;
    }


    if (!isValid) {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }

    if (!inEditMode) {
        for (var i = 0; i < prDetails.length; i++) {
            if ($('#ddlItem option:selected').val() == prDetails[i].ItemId) {
                isValid = false;
                swal({
                    title: 'Item Already Exists In The List. ',
                    text: "Please edit the existing item to do any modifications",
                    type: 'error',
                    confirmButtonClass: "btn btn-info btn-styled",
                    buttonsStyling: false
                });
            }
        }
    }
    return isValid;
}

function clearItem() {
    //debugger;
    itemSpecs = [];
    replacementImages = [];
    fileUploads = [];
    supportiveDocs = [];
    $('#ddlItem').removeProp('disabled');
    $('#ddlItem option').removeProp('selected');
    $('#txtDescription').val('');
    $('#txtUnitPrice').val('');
    $('#txtDescription').val('');
    $('#txtQty').val('');
    $('#txtWarehouseStock').val('');
    $('#rdoFileSampleYes').prop('checked', 'checked');
    $('#rdoReplacementYes').prop('checked', 'checked');
    $('#base64ReplacementImageStorage').html('');
    $('#base64StandardImageStorage').html('');
    $('#base64SupportiveDocsStorage').html('');
    $('#txtRemarks').val('');
    $('#spnReplacementImgCount').text(' 0');
    $('#spnStandardImgCount').text(' 0');
    $('#spnSupportiveDocCount').text(' 0');
    $('#spnSpecCount').text(' 0');
    $('#btnAddItem').text('Add To List');
    $('#btnAddItem').removeClass('btn-warning');
    $('#btnAddItem').addClass('btn-success');
    $('#txtSparePart').val('');
    loadItem();
    $('#itemNotAllowed').addClass('hidden');
    inEditMode = false;
    editIndex = 0;


    window.scrollTo({ top: 0, behavior: 'smooth' });
}

function clearItemFields() {
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonClass: 'btn btn-success btn-styled',
        cancelButtonClass: 'btn btn-danger btn-styled',
        confirmButtonText: 'Yes',
        buttonsStyling: false
    }).then((result) => {
        if (result.value) {
            clearItem();
        }
    }).catch(swal.noop)
}

function addItem() {
    if (validateItem()) {
        var prd = new PrDetails();

        prd.PrdId = generateUniqueKey();
        prd.ItemId = $('#ddlItem option:selected').val();
        prd.ItemName = $('#ddlItem option:selected').text();
        prd.Description = $('#txtDescription').val();
        prd.EstimatedAmount = $('#txtUnitPrice').val();
        prd.RequestedQty = $('#txtQty').val();
        prd.SparePartNo = $('#txtSparePart').val();
        // prd.WarehouseStock = $('#txtWarehouseStock').val();

        var dStock = $('#txtWarehouseStock').val();
        var ret = dStock.split(" ");
        var str1 = ret[0];
        prd.WarehouseStock = str1;
        //prd.MeasurementShortName = $('#txtMeasurement').val();
        prd.MeasurementShortName = $("#ddlMeasurement option:selected").html();
        prd.DetailId = $("#ddlMeasurement option:selected").val();

        if ($('#rdoFileSampleYes').prop('checked')) {
            prd.FileSampleProvided = 1;
        }
        else {
            prd.FileSampleProvided = 0;
        }
        if ($('#rdoReplacementYes').prop('checked')) {
            prd.Replacement = 1;
        }
        else {
            prd.Replacement = 0;
        }
        prd.Remarks = $('#txtRemarks').val();

        prd.PrBoms = JSON.parse(JSON.stringify(itemSpecs));

        for (var i = 0; i < replacementImages.length; i++) {
            replacementImages[i].FileData = $('#base64ReplacementImageStorage').children().eq(i).html();
        }

        for (var i = 0; i < fileUploads.length; i++) {
            fileUploads[i].FileData = $('#base64StandardImageStorage').children().eq(i).html();
        }

        for (var i = 0; i < supportiveDocs.length; i++) {
            supportiveDocs[i].FileData = $('#base64SupportiveDocsStorage').children().eq(i).html();
        }

        prd.PrReplacementFileUploads = JSON.parse(JSON.stringify(replacementImages));
        prd.PrFileUploads = JSON.parse(JSON.stringify(fileUploads));
        prd.PrSupportiveDocuments = JSON.parse(JSON.stringify(supportiveDocs));

        if (!inEditMode) {
            prDetails.push(prd);
        }
        else {
            prDetails[editIndex] = prd;
        }

        populateAddedItems();
        clearItem();
    }
}

function populateAddedItems() {
    //debugger;
    var totalEstimatedAmount = 0;
    var html = ``;
    if (prDetails.length > 0) {
        $('#ddlPRCategory').prop('disabled', 'disabled');
        $('#ddlPRSubCategory').prop('disabled', 'disabled');
        $('#ddlPRType').prop('disabled', 'disabled');
        $('#rdoCapex').prop('disabled', 'disabled');

        for (var i = 0; i < prDetails.length; i++) {

            totalEstimatedAmount += parseFloat(prDetails[i].RequestedQty) * parseFloat(prDetails[i].EstimatedAmount);

            html += `
                        <tr>
        <td>`+ prDetails[i].ItemName + `</td>
        <td>`+ parseFloat(prDetails[i].RequestedQty).toFixed(2) + `</td>
        <td>`+ prDetails[i].MeasurementShortName + `</td>
        <td>`+ parseFloat(prDetails[i].EstimatedAmount).toFixed(2) + `</td>
        <td>`+ prDetails[i].Description + `</td>
        <td>
            <ul class="list-inline">`;
            if (prDetails[i].Replacement == 1) {
                html += `
                                    <li>Replacement: Yes</li>`;
            }
            else {
                html += `
                                    <li>Replacement: No</li>`;
            }
            if (prDetails[i].FileSampleProvided == 1) {
                html += `
                                    <li>File/Sample: Yes</li>`;
            }
            else {
                html += `
                                    <li>File/Sample: No</li>`;
            }
            html += `
                                </ul>
        </td>
        <td>
            <a href="#" class="text-aqua" data-toggle="tooltip" data-placement="top" title="Remarks" onclick="viewRemarksFromList(event,`+ i + `);"><span class="glyphicon glyphicon-align-justify"></span></a>
            <a href="#" class="text-orange" data-toggle="tooltip" data-placement="top" title="Replacement Images" onclick="viewReplacementImagesFromList(event,`+ i + `);"><span class="glyphicon glyphicon-picture"></span></a>
            <a href="#" class="text-green" data-toggle="tooltip" data-placement="top" title="Standard Images" onclick="viewStandardImagesFromList(event,`+ i + `);"><span class="glyphicon glyphicon-picture"></span></a>
            <a href="#" class="text-red" data-toggle="tooltip" data-placement="top" title="Supportive Documents" onclick="viewsupportiveDocsFromList(event,`+ i + `);"><span class="glyphicon glyphicon-file"></span></a>
            <a href="#" class="text-navy" data-toggle="tooltip" data-placement="top" title="Item Specifications" onclick="viewItemSpecFromList(event,`+ i + `);"><span class="glyphicon glyphicon-list"></span></a>
        </td>
        <td>
            <a href="#" class="text-orange" data-toggle="tooltip" data-placement="top" title="Edit Item" onclick="editItem(event,`+ i + `);"><span class="glyphicon glyphicon-edit"></span></a>
            <a href="#" class="text-red" data-toggle="tooltip" data-placement="top" title="Remove Item" onclick="removeItem(event,`+ i + `);"><span class="glyphicon glyphicon-trash"></span></a>
        </td>
    </tr >`;
        }
        //debugger;

        //totalEstimatedAmount = 1000.50
        //// Keep only digits and decimal points:
        //var x = totalEstimatedAmount.toString()
        //        .replace(/[^\d.]/g, "")
        //        // Keep only two digits past the decimal point:
        //        .replace(/\.(\d{2})\d+/, '.$1')
        //        // Add thousands separators:
        //    .replace(/\B(?=(\d{3})+(?!\d))/g, ",")

        //$('#totalEstAmount').html(x);

        $('#totalEstAmount').html(totalEstimatedAmount.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
        // $('#totalEstAmount').html(totalEstimatedAmount.toFixed(2));
    }
    else {
        $('#totalEstAmount').html(0.00);
        $('#ddlPRCategory').removeProp('disabled');
        $('#ddlPRSubCategory').removeProp('disabled');
        $('#ddlPRType').removeProp('disabled');
        $('#rdoCapex').removeProp('disabled');
        $('#rdoOpex').removeProp('disabled');


        if ($('#ddlPRType option:selected').val() == "1") {
            $('#rdoCapex').prop('disabled', 'disabled');
            
            $('#rdoCapex').removeProp('checked');
            $('#rdoOpex').prop('checked', 'checked');
           
            setExpenseRequired();
        }

        if ($('#ddlPRType option:selected').val() == "2") {
            $('#rdoCapex').removeProp('disabled');
            $('#rdoOpex').removeProp('checked');
            $('#rdoCapex').prop('checked', 'checked');
            setExpenseRequired();
        }

    }
    $('#tbAddedItems').html(html);
    $('[data-toggle="tooltip"]').tooltip();
}

function viewRemarksFromList(e, index) {
    e.preventDefault();
    if (prDetails[index].Remarks != '') {
        swal({
            title: 'Remarks',
            text: prDetails[index].Remarks,
            confirmButtonClass: "btn btn-info btn-styled",
            confirmButtonText: "Close",
            buttonsStyling: false
        });
    }
    else {
        swal({
            title: 'Remarks',
            type: 'info',
            text: 'Not Found',
            confirmButtonClass: "btn btn-info btn-styled",
            confirmButtonText: "Close",
            buttonsStyling: false
        });
    }
}

function viewReplacementImagesFromList(e, index) {
    e.preventDefault();
    var html = '';
    if (prDetails[index].PrReplacementFileUploads.length > 0) {
        for (var i = 0; i < prDetails[index].PrReplacementFileUploads.length; i++) {
            html += `
                            <div class="thumb-container">
                                <img src="`+ prDetails[index].PrReplacementFileUploads[i].FilePath + `" />
                            </div>`;
        }
    }
    else {
        html += `<h4>Not Found.</h4>`;
    }
    $('#dvReplacementImages').html(html);
    $('#mdlReplacementImages').modal('show');
}

function viewStandardImagesFromList(e, index) {
    e.preventDefault();
    var html = '';
    if (prDetails[index].PrFileUploads.length > 0) {
        for (var i = 0; i < prDetails[index].PrFileUploads.length; i++) {
            html += `
                            <div class="thumb-container">
                                <img src="`+ prDetails[index].PrFileUploads[i].FilePath + `" />
                            </div>`;
        }
    }
    else {
        html += `<h4>Not Found.</h4>`;
    }
    $('#dvStandardImages').html(html);
    $('#mdlFileUpload').modal('show');
}

function viewsupportiveDocsFromList(e, index) {
    e.preventDefault();
    var html = '';
    if (prDetails[index].PrSupportiveDocuments.length > 0) {
        for (var i = 0; i < prDetails[index].PrSupportiveDocuments.length; i++) {
            html += `
                            <tr>
                                <td>`+ prDetails[index].PrSupportiveDocuments[i].FileName + `</td>
                                <td>
                                    <a href="#" style="margin-right: 5px;" class="text-aqua" data-toggle="tooltip" data-placement="top" title="View" onclick="viewSupportiveDocAtFromList(event,`+ index + `,` + i + `);"><span class="glyphicon glyphicon-search"></span></a>
                                </td>
                            </tr>`;
        }
    }
    else {
        html += `<tr><td colspan="2">Not Found.</td></tr>`;
    }
    $('#tbSupportiveDocs').html(html);
    $('[data-toggle="tooltip"]').tooltip();
    $('#mdlSupportiveDocs').modal('show');
}

function viewItemSpecFromList(e, index) {
    e.preventDefault();
    var html = '';
    if (prDetails[index].PrBoms.length > 0) {
        for (var i = 0; i < prDetails[index].PrBoms.length; i++) {
            html += `
                            <tr>
                                <td>`+ prDetails[index].PrBoms[i].Material + `</td>
                                <td>`+ prDetails[index].PrBoms[i].Description + `</td>
                            </tr>`;
        }
    }
    else {
        html += '<tr><td colspan="2">Not Found</td></tr>';
    }

    $('#tbSpecsFromList').html(html);
    $('#mdlItemSpecFromList').modal('show');
}

function viewSupportiveDocAtFromList(e, prdIndex, docIndex) {
    e.preventDefault();
    var tab = window.open(prDetails[prdIndex].PrSupportiveDocuments[docIndex].FilePath);
}

function removeItem(e, index) {
    e.preventDefault();
    //debugger;
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonClass: 'btn btn-success btn-styled',
        cancelButtonClass: 'btn btn-danger btn-styled',
        confirmButtonText: 'Yes',
        buttonsStyling: false
    }).then((result) => {
        if (result.value) {
            if ($('#ddlItem option:selected').val() == prDetails[index].ItemId) {
                clearItem();
                loadItem();
            }
            prDetails.splice(index, 1);
            populateAddedItems();
        }
    }).catch(swal.noop)
}


function updateStock() {
    //debugger;
    if ($('#ddlItem option:selected').val() != undefined) {
        getStock();
    }
}


//get all measuremnts-pasindu-2020/04/21
var measurementAjax = null;
var changeAvaAjax = null;

function updateMeasurement() {
    //$('#txtMeasurement').val($('#ddlItem option:selected').attr('measurement'));
    if ($('#ddlItem option:selected').val() != "" && $('#ddlPRCategory option:selected').val() != undefined && $('#ddlPRCategory option:selected').val() != null) {

        measurementAjax = $.ajax({
            type: "GET",
            url: 'CreatePR_V2.aspx/LoadMeasurement?itemId=' + $('#ddlItem option:selected').val(),
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            beforeSend: function () {
                if (measurementAjax != null) {
                    measurementAjax.abort();
                }
            },
            success: function (response) {
                //console.log(response);
                response = JSON.parse(response.d);
                if (response.Status == 200) {
                    var mesaurments = response.Data;

                    var html = ``;
                    for (var i = 0; i < mesaurments.length; i++) {

                        if (!inEditMode) {
                            if (i == 0) {
                                html += `<option value="` + mesaurments[i].DetailId + `" selected>` + mesaurments[i].ShortCode + `</option>`;
                            }
                            else {
                                html += `<option value="` + mesaurments[i].DetailId + `">` + mesaurments[i].ShortCode + `</option>`;
                            }
                        }
                        else {
                            if (mesaurments[i].DetailId == prDetails[editIndex].DetailId) {
                                html += `<option value="` + mesaurments[i].DetailId + `" selected>` + mesaurments[i].ShortCode + `</option>`;
                            }
                            else {
                                html += `<option value="` + mesaurments[i].DetailId + `">` + mesaurments[i].ShortCode + `</option>`;
                            }
                        }

                        //if (i == 0) {
                        //    html += `<option value="` + mesaurments[i].DetailId + `" selected>` + mesaurments[i].ShortCode + `</option>`;
                        //}
                        //else {
                        //    html += `<option value="` + mesaurments[i].DetailId + `">` + mesaurments[i].ShortCode + `</option>`;
                        //}
                    }

                    ////debugger;
                    $('#ddlMeasurement').html(html);
                    $('.select2').select2();

                    $('#subCategoryLoad').addClass('hidden');
                    changeAvaStock();
                    //loadItem();
                }
                else if (response.Status == 500) {
                    showServerError();
                    $('#ddlMeasurement').addClass('hidden');
                    $('.select2').select2();
                }
                else {
                    showSessionExpiry();
                    $('#ddlMeasurement').addClass('hidden');
                    $('.select2').select2();
                }
            },
            error: function (error) {
                ////debugger
                console.log(Error);
                showAjaxError();
                $('#ddlMeasurement').addClass('hidden');
                $('.select2').select2();
            }
        });

    }
    else {

        $('#ddlMeasurement').html('');
        $('.select2').select2();
        $('#ddlMeasurement').addClass('hidden');
    }
}
//////////////////////////////get stock- pasindu-2020/04/21///////////////////////////////////////////////////////
function getStock() {

    //debugger;

    changeAvaAjax = $.ajax({
        type: "GET",
        url: 'CreatePR_V2.aspx/getStock?ddlItemName=' + $('#ddlItem option:selected').val() + '&ddlWarehouse=' + $('#ddlWarehouse option:selected').val() + '&ddlMeasurement=' + $('#ddlMeasurement option:selected').val(),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        beforeSend: function () {
            if (changeAvaAjax != null) {
                changeAvaAjax.abort();
            }
        },
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                var unitConvert = response.Data;
                $('#txtWarehouseStock').val(unitConvert);

            }
            else if (response.Status == 500) {
                showServerError();

            }
            else {
                showSessionExpiry();

            }
        },
        error: function (error) {
            console.log(error);
            ////debugger;
            showAjaxError();

        }
    });
}
///////////////////////////////end-get stock//////////////////////////////////////////////////////
//get all unit conversion-pasindu-2020/04/21
var changeAvaAjax = null;
function changeAvaStock() {
    //debugger
    if ($('#ddlItem option:selected').val() != "" && $('#ddlPRCategory option:selected').val() != undefined && $('#ddlPRCategory option:selected').val() != null && $('#ddlMeasurement option:selected').val() != "" && $('#ddlMeasurement option:selected').val() != undefined) {

        changeAvaAjax = $.ajax({
            type: "GET",
            url: 'CreatePR_V2.aspx/UnitConversion?ddlItemName=' + $('#ddlItem option:selected').val() + '&ddlWarehouse=' + $('#ddlWarehouse option:selected').val() + '&ddlMeasurement=' + $('#ddlMeasurement option:selected').val() + '&ddlMeasurementUnit="' + $('#ddlMeasurement option:selected').html() + '"',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            beforeSend: function () {
                if (changeAvaAjax != null) {
                    changeAvaAjax.abort();
                }
            },
            success: function (response) {
                response = JSON.parse(response.d);
                //console.log(response);
                if (response.Status == 200) {
                    var unitConvert = response.Data;
                    $('#txtWarehouseStock').val(unitConvert);

                }
                else if (response.Status == 500) {
                    showServerError();

                }
                else {
                    showSessionExpiry();

                }
            },
            error: function (error) {
                console.log(error);
                ////debugger;
                showAjaxError();

            }
        });
    }
    else {

    }
}
function editItem(e, prdIndex) {
    //debugger;
    e.preventDefault();
    editIndex = prdIndex;
    inEditMode = true;
    $('#btnAddItem').text('Update List');
    $('#btnAddItem').removeClass('btn-success');
    $('#btnAddItem').addClass('btn-warning');

    $('#ddlItem option').removeProp('selected');
    $(`#ddlItem option[value='` + prDetails[prdIndex].ItemId + `']`).prop('selected', true);
    $('.select2').select2();
    $('#ddlItem').prop('disabled', 'disabled');
    updateMeasurement();
    $('#ddlMeasurement option').removeProp('selected');
    $(`#ddlMeasurement option[value='` + prDetails[prdIndex].DetailId + `']`).prop('selected', true);
    $('.select2').select2();

    $('#txtDescription').val(prDetails[prdIndex].Description);
    $('#txtUnitPrice').val(prDetails[prdIndex].EstimatedAmount);
    $('#txtQty').val(prDetails[prdIndex].RequestedQty);
    //$('#txtWarehouseStock').val(prDetails[prdIndex].WarehouseStock);
    $('#txtSparePart').val(prDetails[prdIndex].SparePartNo);
    var dprtmntStock = prDetails[prdIndex].WarehouseStock + " " + prDetails[prdIndex].MeasurementShortName;
    $('#txtWarehouseStock').val(dprtmntStock);

    if (prDetails[prdIndex].FileSampleProvided == 1) {
        $('#rdoFileSampleYes').prop('checked', 'checked');
    }
    else {
        $('#rdoFileSampleNo').prop('checked', 'checked');
    }

    if (prDetails[prdIndex].Replacement == 1) {
        $('#rdoReplacementYes').prop('checked', 'checked');
    }
    else {
        $('#rdoReplacementNo').prop('checked', 'checked');
    }

    var html = ``;
    for (var i = 0; i < prDetails[prdIndex].PrReplacementFileUploads.length; i++) {
        html += `
                        <div>` + prDetails[prdIndex].PrReplacementFileUploads[i].FileData + `</div>`;
    }
    $('#base64ReplacementImageStorage').html(html);

    html = ``;
    for (var i = 0; i < prDetails[prdIndex].PrFileUploads.length; i++) {
        html += `
                        <div>` + prDetails[prdIndex].PrFileUploads[i].FileData + `</div>`;
    }
    $('#base64StandardImageStorage').html(html);

    html = ``;
    for (var i = 0; i < prDetails[prdIndex].PrSupportiveDocuments.length; i++) {
        html += `
                        <div>` + prDetails[prdIndex].PrSupportiveDocuments[i].FileData + `</div>`;
    }
    $('#base64SupportiveDocsStorage').html(html);

    $('#txtRemarks').val(prDetails[prdIndex].Remarks);
    $('#spnReplacementImgCount').text(' ' + prDetails[prdIndex].PrReplacementFileUploads.length);
    $('#spnStandardImgCount').text(' ' + prDetails[prdIndex].PrFileUploads.length);
    $('#spnSupportiveDocCount').text(' ' + prDetails[prdIndex].PrSupportiveDocuments.length);
    $('#spnSpecCount').text(' ' + prDetails[prdIndex].PrBoms.length);

    itemSpecs = prDetails[prdIndex].PrBoms;
    replacementImages = prDetails[prdIndex].PrReplacementFileUploads;
    fileUploads = prDetails[prdIndex].PrFileUploads;
    supportiveDocs = prDetails[prdIndex].PrSupportiveDocuments;

    window.scrollTo({ top: 0, behavior: 'smooth' });
}

function validateSave() {
    if (prDetails.length <= 0) {
        showMessage('error', 'No Item Found', 'Please add atleast one item to continue.');
        $('#btnItem').click();
        return false;
    }

    if ($('#txtRequiredDate').val() == '') {
        showMessage('error', 'Missing Information', 'Please fill all the required fields in basic Information');
        $('#txtRequiredDate').parent().addClass('has-error');
        $('#btnBasic').click();
        return false;
    }
    else {
        $('#txtRequiredDate').parent().removeClass('has-error');
    }


    if ($('#rdoCapex').prop('checked') == true) {
        if (capexDocs.length <= 0) {
            showMessage('error', 'File Not Found', 'Please upload atleast one Capital Expense Document');
            $('#spnCapexDocsCount').parent().addClass('has-error');
            $('#btnBasic').click();
            return false;
        }

        if ($('#rdoBudgetYes').prop('checked') == true) {
            $('.expenseRemarksRequired').parent().removeClass('has-error');
            var isExpenseValid = true;
            if (/^[0-9]+(\.)?[0-9]*$/.test($('#txtBudgetAmount').val())) {
                $('#txtBudgetAmount').parent().removeClass('has-error');
            }
            else {
                $('#txtBudgetAmount').parent().addClass('has-error');
                isExpenseValid = false;
            }
            if ($('#txtBudgetInformation').val() != '') {
                $('#txtBudgetInformation').parent().removeClass('has-error');
            }
            else {
                $('#txtBudgetInformation').parent().addClass('has-error');
                isExpenseValid = false;
            }

            return isExpenseValid;
        }
        else {
            $('#txtBudgetAmount').parent().removeClass('has-error');
            $('#txtBudgetInformation').parent().removeClass('has-error');
            if ($('#txtExpenseRemarks').val() != '') {
                $('#txtExpenseRemarks').parent().removeClass('has-error');
                return true;
            }
            else {
                $('#txtExpenseRemarks').parent().addClass('has-error');
                return false;
            }
        }
    }
    else {
        return true;
        $('#txtBudgetAmount').parent().removeClass('has-error');
        $('#txtBudgetInformation').parent().removeClass('has-error');
        $('.expenseRemarksRequired').parent().removeClass('has-error');
    }

}

function save() {

    if (validateSave()) {

        $('#btnSave').addClass('hidden');
        $('#iWait').removeClass('hidden');

        prMaster = new PrMaster();
        prMaster.PrId = generateUniqueKey();
        prMaster.CompanyId = 0;
        prMaster.WarehouseId = $('#ddlWarehouse option:selected').val();
        prMaster.PrType = $('#ddlPRType option:selected').val();
        prMaster.PurchaseType = $('#ddlPurchasingType option:selected').val();
        prMaster.ExpectedDate = $('#txtRequiredDate').val();
        prMaster.ImportItemType = $('#ddlItemType option:selected').val();
        if ($('#rdoProcedureNormal').prop('checked')) {
            prMaster.PurchaseProcedure = 1;
        }
        else {
            prMaster.PurchaseProcedure = 3;
        }
        prMaster.RequiredFor = $('#txtRequiredFor').val();
        prMaster.PrCategoryId = $('#ddlPRCategory option:selected').val();
        prMaster.PrSubCategoryId = $('#ddlPRSubCategory option:selected').val();

        if ($('#rdoCapex').prop('checked')) {
            prMaster.ExpenseType = 1;
        }
        else {
            prMaster.ExpenseType = 2;
        }

        if ($('#rdoBudgetYes').prop('checked')) {
            prMaster.ISBudget = 1;
        }
        else {
            prMaster.ISBudget = 2;
        }

        if ($('#txtBudgetAmount').val() == '') {
            prMaster.BudgetAmount = 0;
        }
        else {
            prMaster.BudgetAmount = $('#txtBudgetAmount').val();
        }
        prMaster.BudgetInfo = $('#txtBudgetInformation').val();
        prMaster.ExpenseRemarks = $('#txtExpenseRemarks').val();
        prMaster.CreatedBy = 0;

        for (var i = 0; i < capexDocs.length; i++) {
            capexDocs[i].FileData = $('#base64CapexDocsStorage').children().eq(i).html();
        }

        prMaster.PrCapexDocs = capexDocs;

        prMaster.PrDetails = prDetails;


        $.ajax({
            type: "POST",
            url: 'CreatePR_V2.aspx/Save',
            contentType: "application/json; charset=utf-8",
            data: '{"prMaster": ' + JSON.stringify(prMaster) + '}',
            dataType: 'json',
            success: function (response) {
                response = JSON.parse(response.d);
                if (response.Status == 200) {
                    swal.fire({
                        title: 'SUCCESS',
                       html: 'Purchase Request Note Successfully Created With the Code: <b>' + response.Data + '</b>',
                        //html: 'Purchase Request Note Successfully Created ',
                        type: 'success',
                        showCancelButton: false,
                        confirmButtonClass: 'btn btn-info btn-styled',
                        buttonsStyling: false
                    }).then((result) => {
                        if (result.value) {
                            window.location = 'CreatePR_V2.aspx';
                        }
                    });
                }
                else if (response.Status == 500) {
                    showServerError();
                    $('#btnSave').removeClass('hidden');
                    $('#iWait').addClass('hidden');
                }
                else {
                    showSessionExpiry();
                    $('#btnSave').removeClass('hidden');
                    $('#iWait').addClass('hidden');
                }
            },
            error: function (error) {
                console.log(error);
                showAjaxError();
                $('#btnSave').removeClass('hidden');
                $('#iWait').addClass('hidden');
            }
        });
    }
}

var Itemdesc = null;
function GetItemsBOM() {
    Itemdesc = $.ajax({
        type: "GET",
        url: 'CreatePR_V2.aspx/GetItemDescription?ItemId=' + $('#ddlItem option:selected').val(),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        beforeSend: function () {
            if (subCategoryAjax != null) {
                subCategoryAjax.abort();
            }
        },
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {

                var ListSpec = response.Data;

                $.each(ListSpec, function (key, value) {

                    var bom = new PrBom();
                    bom.Material = value.Material;
                    bom.Description = value.Description;
                    bom.IsAdded = 1;
                    bom.ItemId = $('#ddlItem option:selected').val();

                //    if (itemSpecs.length == 0) {
                //        itemSpecs.push(bom);
                //    } else {

                //        $.each(itemSpecs, function (key, valueSpec) {

                //            if (valueSpec.Material != bom.Material && valueSpec.Description != bom.Description) {
                //                itemSpecs.push(bom);
                //            }

                //        })

                //    }




                //});
                    itemSpecs.push(bom);


                });


                var result = [];
                $.each(itemSpecs, function (i, e) {
                    if ($.inArray(e, result) == -1) result.push(e);
                });


                result = itemSpecs;

                if (ListSpec.length == 0) {
                    $('#spnSpecCount').text(' ' + "0");
                } else {
                    $('#spnSpecCount').text(' ' + itemSpecs.filter(x => x.ItemId == $('#ddlItem option:selected').val()).length);
                }



            }

        },
        error: function (error) {
            showAjaxError();

        }
    });



}

function ItemClick() {
    itemSpecs = [];
    updateMeasurement();
    GetItemsBOM();
}