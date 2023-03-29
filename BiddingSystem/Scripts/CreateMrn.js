
var inEditMode = false;
var editIndex = 0;
var mrnMaster = null;
var mrnDetails = [];
var itemSpecs = [];
var replacementImages = [];
var fileUploads = [];
var supportiveDocs = [];
var capexDocs = [];
var mrnBom = null;

loadDepartments();
loadWarehouse();
loadCategory();
MrnTypeOnchange();
PurchaseTypeOnchange();
ImportItemTypeOnchange();
$('#dvImportItem').addClass('hidden');
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

var departmentAjax = null;
function loadDepartments() {
  

    $('#depLoad').removeClass('hidden');

    departmentAjax = $.ajax({
        type: "GET",
        url: 'CreateMRN_V2.aspx/GetDepartments',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        beforeSend: function () {
            if (departmentAjax != null) {
                departmentAjax.abort();
            }
        },
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                var departments = response.Data;

                // debugger;
                var html = ``;
                for (var i = 0; i < departments.length; i++) {
                    if (i == 0) {
                        html += `<option value="` + departments[i].DepartmentId + `" is-head="` + departments[i].IsHead + `" selected>` + departments[i].DepartmentName + `</option>`;
                    }
                    else {
                        html += `<option value="` + departments[i].DepartmentId + `" is-head="` + departments[i].IsHead + `">` + departments[i].DepartmentName + `</option>`;
                    }
                }

                $('#ddlDepartment').html(html);
                $('.select2').select2();

                $('#depLoad').addClass('hidden');
            }
            else if (response.Status == 500) {
                showServerError();
                $('#depLoad').addClass('hidden');
            }
            else {
                showSessionExpiry();
                $('#depLoad').addClass('hidden');
            }
        },
        error: function (error) {
            showAjaxError();
            $('#depLoad').addClass('hidden');
        }
    });

}

function updateStock() {
    debugger;
    if ($('#ddlItem option:selected').val() != undefined) {
        getStock();
        updateMeasurement();
    }
}
var warehouseAjax = null;
function loadWarehouse() {
    $('#warehouseLoad').removeClass('hidden');

    warehouseAjax = $.ajax({
        type: "GET",
        url: 'CreateMRN_V2.aspx/GetWarehouses',
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

                // debugger;
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
                //loadCategory();
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
    if (!$('#ddlMRNCategory').prop('disabled')) {
        $('#categoryLoad').removeClass('hidden');

        $('#ddlMRNSubCategory').html('');

        categoryAjax = $.ajax({
            type: "GET",
            url: 'CreateMRN_V2.aspx/GetCategories',
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

                    // debugger;
                    var html = ``;
                    for (var i = 0; i < categories.length; i++) {
                        if (i == 0) {
                            html += `<option value="` + categories[i].CategoryId + `" selected>` + categories[i].CategoryName + `</option>`;
                        }
                        else {
                            html += `<option value="` + categories[i].CategoryId + `">` + categories[i].CategoryName + `</option>`;
                        }
                    }

                    $('#ddlMRNCategory').html(html);
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

    if (!$('#ddlMRNSubCategory').prop('disabled')) {
        $('#subCategoryLoad').removeClass('hidden');
        $('#ddlItem').html('');
        if ($('#ddlMRNCategory option:selected').val() != "" && $('#ddlMRNCategory option:selected').val() != undefined && $('#ddlMRNCategory option:selected').val() != null) {

            subCategoryAjax = $.ajax({
                type: "GET",
                url: 'CreateMRN_V2.aspx/GetSubCategories?CategoryId=' + $('#ddlMRNCategory option:selected').val(),
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

                        //debugger;
                        var html = ``;
                        for (var i = 0; i < subCategories.length; i++) {
                            if (i == 0) {
                                html += `<option value="` + subCategories[i].SubCategoryId + `" selected>` + subCategories[i].SubCategoryName + `</option>`;
                            }
                            else {
                                html += `<option value="` + subCategories[i].SubCategoryId + `">` + subCategories[i].SubCategoryName + `</option>`;
                            }
                        }

                        $('#ddlMRNSubCategory').html(html);
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

            $('#ddlMRNSubCategory').html('');
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

function MrnTypeOnchange() {
    
    loadItem();
    if ($('#ddlMRNType option:selected').val() == "2") {
        //$('#rdoCapex').prop('disabled', 'disabled');
        $('#rdoOpex').removeProp('disabled');
        
        $('#rdoCapex').removeProp('checked');
        $('#rdoOpex').prop('checked', 'checked');
        
        setExpenseRequired();
    }

    if ($('#ddlMRNType option:selected').val() == "1") {
        $('#rdoOpex').prop('disabled', 'disabled');
        $('#rdoCapex').removeProp('disabled');
        $('#rdoOpex').removeProp('checked');
        $('#rdoCapex').prop('checked', 'checked');
        setExpenseRequired();
    }

}

function PurchaseTypeOnchange() {
    
    //loadItem();
    $('#txtSparePart').val('');

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


    //if ($('#ddlMRNType').val() == 1) {

    //    $('#capitalEx').prop('disabled', true);
    //    $("#rdoCapex").prop("checked", false);
    //    $("#rdoCapex").prop('disabled', true);
    //    $("#rdoOpex").prop("checked", true);
    //    $('#dvCapexDoc').hide();
    //    setExpenseRequired();
    //} else {
    //    $('#capitalEx').prop('disabled', false);
    //    $("#rdoCapex").prop("checked", true);
    //    $("#rdoCapex").prop('disabled', false);
    //    $("#rdoOpex").prop("checked", false);
    //    $('#dvCapexDoc').show();
    //    setExpenseRequired();
    //}
    
    if (!inEditMode) {
        $('#itemLoad').removeClass('hidden');
        if ($('#ddlMRNCategory option:selected').val() != "" && $('#ddlMRNCategory option:selected').val() != undefined && $('#ddlMRNCategory option:selected').val() != null) {
            if ($('#ddlMRNSubCategory option:selected').val() != "" && $('#ddlMRNSubCategory option:selected').val() != undefined && $('#ddlMRNSubCategory option:selected').val() != null) {

                itemAjax = $.ajax({
                    type: "GET",
                    url: 'CreateMRN_V2.aspx/GetItems?CategoryId=' + $('#ddlMRNCategory option:selected').val() + '&SubCategoryId=' + $('#ddlMRNSubCategory option:selected').val() + '&ItemType=' + $('#ddlMRNType option:selected').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    beforeSend: function () {
                        if (itemAjax != null) {
                            itemAjax.abort();
                        }
                    },
                    success: function (response) {
                        response = JSON.parse(response.d);
                        //console.log(response);
                        if (response.Status == 200) {
                            var items = response.Data;

                            // debugger;
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
        $('#itemNotAllowed').removeClass('hidden');

        setTimeout(function () {
            $('#itemNotAllowed').addClass('hidden');
        }, 3000);

    }
}

//get all measuremnts-pasindu-2020/04/21
var measurementAjax = null;
var changeAvaAjax = null;

function updateMeasurement() {
    //debugger
    //$('#txtMeasurement').val($('#ddlItem option:selected').attr('measurement'));
    if ($('#ddlItem option:selected').val() != "" && $('#ddlMRNCategory option:selected').val() != undefined && $('#ddlMRNCategory option:selected').val() != null) {

        measurementAjax = $.ajax({
            type: "GET",
            url: 'CreateMRN_V2.aspx/LoadMeasurement?itemId=' + $('#ddlItem option:selected').val(),
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            beforeSend: function () {
                if (measurementAjax != null) {
                    measurementAjax.abort();
                }
            },
            success: function (response) {
                console.log(response);
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
                            if (mesaurments[i].DetailId == mrnDetails[editIndex].DetailId) {
                                html += `<option value="` + mesaurments[i].DetailId + `" selected>` + mesaurments[i].ShortCode + `</option>`;
                            }
                            else {
                                html += `<option value="` + mesaurments[i].DetailId + `">` + mesaurments[i].ShortCode + `</option>`;
                            }
                        }
                    }

                    //debugger;
                    $('#ddlMeasurement').html(html);
                    $('#subCategoryLoad').addClass('hidden');
                    $('.select2').select2();
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

    debugger;

    changeAvaAjax = $.ajax({
        type: "GET",
        url: 'CreateMRN_V2.aspx/getStock?ddlItemName=' + $('#ddlItem option:selected').val() + '&ddlWarehouse=' + $('#ddlWarehouse option:selected').val() + '&ddlMeasurement=' + $('#ddlMeasurement option:selected').val(),
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
                $('#txtDepartmentStock').val(unitConvert);

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
            //debugger;
            showAjaxError();

        }
    });
}
///////////////////////////////end-get stock//////////////////////////////////////////////////////

//get all unit conversion-pasindu-2020/04/21
var changeAvaAjax = null;
function changeAvaStock() {
   
    if ($('#ddlItem option:selected').val() != "" && $('#ddlMRNCategory option:selected').val() != undefined && $('#ddlMRNCategory option:selected').val() != null && $('#ddlMeasurement option:selected').val() != "" && $('#ddlMeasurement option:selected').val() != undefined) {

        changeAvaAjax = $.ajax({
            type: "GET",
            url: 'CreateMRN_V2.aspx/UnitConversion?ddlItemName=' + $('#ddlItem option:selected').val() + '&ddlWarehouse=' + $('#ddlWarehouse option:selected').val() + '&ddlMeasurement=' + $('#ddlMeasurement option:selected').val() + '&ddlMeasurementUnit="' + $('#ddlMeasurement option:selected').html() + '"',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            beforeSend: function () {
                if (changeAvaAjax != null) {
                    changeAvaAjax.abort();
                }
            },
            success: function (response) {
                response = JSON.parse(response.d);
                console.log(response);
                if (response.Status == 200) {
                    var unitConvert = response.Data;
                    var aftrCOn = formatNumber(unitConvert) + " " + $('#ddlMeasurement option:selected').html();
                    $('#txtDepartmentStock1').val(aftrCOn);
                    

                }
                else if (response.Status == 500) {
                    showServerError();

                }
                else {
                    showSessionExpiry();

                }
            },
            error: function (error) {
                //console.log(error);
                //debugger;
                showAjaxError();

            }
        });
    }
    else {

    }
}
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

            var replacementImage = new MrnReplacementFileUpload();

            replacementImage.MrndId = generateUniqueKey();
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

            var standardImage = new MrnFileUpload();

            standardImage.MrndId = generateUniqueKey();
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

            var doc = new MrnSupportiveDocs();

            doc.MrndId = generateUniqueKey();
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

            var doc = new MrnCapexDoc();

            doc.MrnId = generateUniqueKey();
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
        var bom = new MrnBom();
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
    //if (itemSpecs[index].IsAdded == 1) {



    //    DeleteItemSpec(index);

        
    //} else {
        itemSpecs.splice(index, 1);
        $('#spnSpecCount').text(' ' + itemSpecs.length);
        populateSpecs();
    //}
    
   
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
    if ($('#txtDepartmentStock').val()) {
        $('#txtDepartmentStock').parent().removeClass('has-error');
    }
    else {
        $('#txtDepartmentStock').parent().addClass('has-error');
        isValid = false;
    }

    if (!isValid) {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }

    if (!inEditMode) {
        for (var i = 0; i < mrnDetails.length; i++) {
            if ($('#ddlItem option:selected').val() == mrnDetails[i].ItemId) {
                isValid = false;
                swal({
                    title: 'You Can Create MRN in One Department to One Warehouse in One Time',
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
//format number tousand separate
function formatNumber(num) {
    //debugger;
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')
}



function clearItem() {
    itemSpecs = [];
    replacementImages = [];
    fileUploads = [];
    supportiveDocs = [];
    $('#ddlItem').removeProp('disabled');
    $('#txtDescription').val('');
    $('#txtUnitPrice').val('');
    $('#txtDescription').val('');
    $('#txtDepartmentStock1').val('');
    $('#txtQty').val('');
    $('#txtDepartmentStock').val('');
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
    //debugger
    if (validateItem()) {
        var mrnd = new MrnDetails();

        mrnd.MrndId = generateUniqueKey();
        mrnd.ItemId = $('#ddlItem option:selected').val();
        mrnd.ItemName = $('#ddlItem option:selected').text();
        mrnd.Description = $('#txtDescription').val();
        mrnd.EstimatedAmount = $('#txtUnitPrice').val();
        mrnd.RequestedQty = $('#txtQty').val();
        mrnd.SparePartNo = $('#txtSparePart').val();
        var dStock = $('#txtDepartmentStock').val();
        var ret = dStock.split(" ");
        var str1 = ret[0];
        mrnd.DepartmentStock = str1;

        var dStock1 = $('#txtDepartmentStock1').val();
        var ret1 = dStock1.split(" ");
        var str2 = ret1[0];
        mrnd.DepartmentStock1 = str2;
       
        //
        //mrnd.MeasurementShortName = $('#txtMeasurement').val();
        //mrnd.MeasurementShortName = $("#ddlMeasurement option:selected").val();
        mrnd.MeasurementShortName = $("#ddlMeasurement option:selected").html();
        mrnd.DetailId = $("#ddlMeasurement option:selected").val();

        if ($('#rdoFileSampleYes').prop('checked')) {
            mrnd.FileSampleProvided = 1;
        }
        else {
            mrnd.FileSampleProvided = 0;
        }
        if ($('#rdoReplacementYes').prop('checked')) {
            mrnd.Replacement = 1;
        }
        else {
            mrnd.Replacement = 0;
        }
        mrnd.Remarks = $('#txtRemarks').val();

        //mrnd.MrnBoms = JSON.parse(JSON.stringify(itemSpecs.filter(x=> x.IsAdded == 1)));
        mrnd.MrnBoms = JSON.parse(JSON.stringify(itemSpecs));

        for (var i = 0; i < replacementImages.length; i++) {
            replacementImages[i].FileData = $('#base64ReplacementImageStorage').children().eq(i).html();
        }

        for (var i = 0; i < fileUploads.length; i++) {
            fileUploads[i].FileData = $('#base64StandardImageStorage').children().eq(i).html();
        }

        for (var i = 0; i < supportiveDocs.length; i++) {
            supportiveDocs[i].FileData = $('#base64SupportiveDocsStorage').children().eq(i).html();
        }

        mrnd.MrnReplacementFileUploads = JSON.parse(JSON.stringify(replacementImages));
        mrnd.MrnFileUploads = JSON.parse(JSON.stringify(fileUploads));
        mrnd.MrnSupportiveDocuments = JSON.parse(JSON.stringify(supportiveDocs));

        if (!inEditMode) {
            mrnDetails.push(mrnd);
        }
        else {
            mrnDetails[editIndex] = mrnd;
        }

        populateAddedItems();
        clearItem();
    }
}

function populateAddedItems() {
    var totalEstimatedAmount = 0;
    var html = ``;
    if (mrnDetails.length > 0) {
        $('#ddlMRNCategory').prop('disabled', 'disabled');
        $('#ddlMRNSubCategory').prop('disabled', 'disabled');
        $('#ddlMRNType').prop('disabled', 'disabled');
        $('#rdoCapex').prop('disabled', 'disabled');
        $('#rdoOpex').prop('disabled', 'disabled');

        for (var i = 0; i < mrnDetails.length; i++) {

            totalEstimatedAmount += parseFloat(mrnDetails[i].RequestedQty) * parseFloat(mrnDetails[i].EstimatedAmount);

            html += `
                        <tr>
        <td>`+ mrnDetails[i].ItemName + `</td>
        <td>`+ parseFloat(mrnDetails[i].RequestedQty).toFixed(2) + `</td>
        <td>`+ mrnDetails[i].MeasurementShortName + `</td>
        <td>`+ parseFloat(mrnDetails[i].EstimatedAmount).toFixed(2) + `</td>
        <td>`+ mrnDetails[i].Description + `</td>
        <td>
            <ul class="list-inline">`;
            if (mrnDetails[i].Replacement == 1) {
                html += `
                                    <li>Replacement: Yes</li>`;
            }
            else {
                html += `
                                    <li>Replacement: No</li>`;
            }
            if (mrnDetails[i].FileSampleProvided == 1) {
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
        $('#totalEstAmount').html(totalEstimatedAmount.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    }
    else {
        $('#totalEstAmount').html(0.00);
        $('#ddlMRNCategory').removeProp('disabled');
        $('#ddlMRNSubCategory').removeProp('disabled');
        $('#ddlMRNType').removeProp('disabled');
        $('#rdoCapex').removeProp('disabled');
        $('#rdoOpex').removeProp('disabled');

        if ($('#ddlMRNType option:selected').val() == "1") {
            $('#rdoCapex').prop('disabled', 'disabled');

            $('#rdoCapex').removeProp('checked');
            $('#rdoOpex').prop('checked', 'checked');

            setExpenseRequired();
        }

        if ($('#ddlMRNType option:selected').val() == "2") {
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
    if (mrnDetails[index].Remarks != '') {
        swal({
            title: 'Remarks',
            text: mrnDetails[index].Remarks,
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
    if (mrnDetails[index].MrnReplacementFileUploads.length > 0) {
        for (var i = 0; i < mrnDetails[index].MrnReplacementFileUploads.length; i++) {
            html += `
                            <div class="thumb-container">
                                <img src="`+ mrnDetails[index].MrnReplacementFileUploads[i].FilePath + `" />
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
    if (mrnDetails[index].MrnFileUploads.length > 0) {
        for (var i = 0; i < mrnDetails[index].MrnFileUploads.length; i++) {
            html += `
                            <div class="thumb-container">
                                <img src="`+ mrnDetails[index].MrnFileUploads[i].FilePath + `" />
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
    if (mrnDetails[index].MrnSupportiveDocuments.length > 0) {
        for (var i = 0; i < mrnDetails[index].MrnSupportiveDocuments.length; i++) {
            html += `
                            <tr>
                                <td>`+ mrnDetails[index].MrnSupportiveDocuments[i].FileName + `</td>
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
    if (mrnDetails[index].MrnBoms.length > 0) {
        for (var i = 0; i < mrnDetails[index].MrnBoms.length; i++) {
            html += `
                            <tr>
                                <td>`+ mrnDetails[index].MrnBoms[i].Material + `</td>
                                <td>`+ mrnDetails[index].MrnBoms[i].Description + `</td>
                            </tr>`;
        }
    }
    else {
        html += '<tr><td colspan="2">Not Found</td></tr>';
    }

    $('#tbSpecsFromList').html(html);
    $('#mdlItemSpecFromList').modal('show');
}

function viewSupportiveDocAtFromList(e, mrndIndex, docIndex) {
    e.preventDefault();
    var tab = window.open(mrnDetails[mrndIndex].MrnSupportiveDocuments[docIndex].FilePath);
}

function removeItem(e, index) {
    e.preventDefault();
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
            if ($('#ddlItem option:selected').val() == mrnDetails[index].ItemId) {
                clearItem();
                loadItem();
            }
            mrnDetails.splice(index, 1);
            populateAddedItems();
        }
    }).catch(swal.noop)
}

function editItem(e, mrndIndex) {
   
    e.preventDefault();
    editIndex = mrndIndex;
    inEditMode = true;
    $('#btnAddItem').text('Update List');
    $('#btnAddItem').removeClass('btn-success');
    $('#btnAddItem').addClass('btn-warning');

    $('#ddlItem option').removeProp('selected');
    $(`#ddlItem option[value='` + mrnDetails[mrndIndex].ItemId + `']`).prop('selected', true);
    $('.select2').select2();
    updateMeasurement();
    $('#ddlItem').prop('disabled', 'disabled');
    $('.select2').select2();
    $('#txtDescription').val(mrnDetails[mrndIndex].Description);
    $('#txtUnitPrice').val(mrnDetails[mrndIndex].EstimatedAmount);
    $('#txtQty').val(mrnDetails[mrndIndex].RequestedQty);
    $('#txtDepartmentStock').val(mrnDetails[mrndIndex].DepartmentStock);
    $('#txtDepartmentStock1').val(mrnDetails[mrndIndex].DepartmentStock1);
    $('#txtSparePart').val(mrnDetails[mrndIndex].SparePartNo);


    
    if (mrnDetails[mrndIndex].FileSampleProvided == 1) {
        $('#rdoFileSampleYes').prop('checked', 'checked');
    }
    else {
        $('#rdoFileSampleNo').prop('checked', 'checked');
    }

    if (mrnDetails[mrndIndex].Replacement == 1) {
        $('#rdoReplacementYes').prop('checked', 'checked');
    }
    else {
        $('#rdoReplacementNo').prop('checked', 'checked');
    }

    var html = ``;
    for (var i = 0; i < mrnDetails[mrndIndex].MrnReplacementFileUploads.length; i++) {
        html += `
                        <div>` + mrnDetails[mrndIndex].MrnReplacementFileUploads[i].FileData + `</div>`;
    }
    $('#base64ReplacementImageStorage').html(html);

    html = ``;
    for (var i = 0; i < mrnDetails[mrndIndex].MrnFileUploads.length; i++) {
        html += `
                        <div>` + mrnDetails[mrndIndex].MrnFileUploads[i].FileData + `</div>`;
    }
    $('#base64StandardImageStorage').html(html);

    html = ``;
    for (var i = 0; i < mrnDetails[mrndIndex].MrnSupportiveDocuments.length; i++) {
        html += `
                        <div>` + mrnDetails[mrndIndex].MrnSupportiveDocuments[i].FileData + `</div>`;
    }
    $('#base64SupportiveDocsStorage').html(html);

    $('#txtRemarks').val(mrnDetails[mrndIndex].Remarks);
    $('#spnReplacementImgCount').text(' ' + mrnDetails[mrndIndex].MrnReplacementFileUploads.length);
    $('#spnStandardImgCount').text(' ' + mrnDetails[mrndIndex].MrnFileUploads.length);
    $('#spnSupportiveDocCount').text(' ' + mrnDetails[mrndIndex].MrnSupportiveDocuments.length);
    $('#spnSpecCount').text(' ' + mrnDetails[mrndIndex].MrnBoms.length);

    itemSpecs = mrnDetails[mrndIndex].MrnBoms;
    replacementImages = mrnDetails[mrndIndex].MrnReplacementFileUploads;
    fileUploads = mrnDetails[mrndIndex].MrnFileUploads;
    supportiveDocs = mrnDetails[mrndIndex].MrnSupportiveDocuments;

    window.scrollTo({ top: 0, behavior: 'smooth' });
}

function validateSave() {
    if (mrnDetails.length <= 0) {
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
        //debugger
        $('#btnSave').addClass('hidden');
        $('#iWait').removeClass('hidden');

        mrnMaster = new MrnMaster();
        mrnMaster.MrnId = generateUniqueKey();
        mrnMaster.CompanyId = 0;
        mrnMaster.SubDepartmentId = $('#ddlDepartment option:selected').val();
        mrnMaster.WarehouseId = $('#ddlWarehouse option:selected').val();
        mrnMaster.MrnType = $('#ddlMRNType option:selected').val();
        mrnMaster.PurchaseType = $('#ddlPurchasingType option:selected').val();
        mrnMaster.ExpectedDate = $('#txtRequiredDate').val();
        mrnMaster.ImportItemType = $('#ddlItemType option:selected').val();
        //mrnMaster.mesureId = $('#ddlMeasurement option:selected').val();
        if ($('#rdoProcedureNormal').prop('checked')) {
            mrnMaster.PurchaseProcedure = 1;
        }
        else {
            mrnMaster.PurchaseProcedure = 3;
        }
        mrnMaster.RequiredFor = $('#txtRequiredFor').val();
        mrnMaster.MrnCategoryId = $('#ddlMRNCategory option:selected').val();
        mrnMaster.MrnSubCategoryId = $('#ddlMRNSubCategory option:selected').val();

        if ($('#rdoCapex').prop('checked')) {
            mrnMaster.ExpenseType = 1;
        }
        else {
            mrnMaster.ExpenseType = 2;
        }

        if ($('#rdoBudgetYes').prop('checked')) {
            mrnMaster.ISBudget = 1;
        }
        else {
            mrnMaster.ISBudget = 2;
        }

        if ($('#txtBudgetAmount').val() == '') {
            mrnMaster.BudgetAmount = 0;
        }
        else {
            mrnMaster.BudgetAmount = $('#txtBudgetAmount').val();
        }
        mrnMaster.BudgetInfo = $('#txtBudgetInformation').val();
        mrnMaster.ExpenseRemarks = $('#txtExpenseRemarks').val();
        mrnMaster.CreatedBy = 0;

        for (var i = 0; i < capexDocs.length; i++) {
            capexDocs[i].FileData = $('#base64CapexDocsStorage').children().eq(i).html();
        }

        mrnMaster.MrnCapexDocs = capexDocs;
        mrnMaster.MrnDetails = mrnDetails;

        $.ajax({
            type: "POST",
            url: 'CreateMRN_V2.aspx/Save',
            contentType: "application/json; charset=utf-8",
            data: '{"mrnMaster": ' + JSON.stringify(mrnMaster) + '}',
            dataType: 'json',
            success: function (response) {
                response = JSON.parse(response.d);
                if (response.Status == 200) {
                    swal.fire({
                        title: 'SUCCESS',
                        html: 'Material Request Note Successfully Created With the Code: <b>' + response.Data + '</b>',
                        //html: 'Material Request Note Successfully Created ',
                        type: 'success',
                        showCancelButton: false,
                        confirmButtonClass: 'btn btn-info btn-styled',
                        buttonsStyling: false
                    }).then((result) => {
                        if (result.value) {
                            window.location = 'CreateMRN_V2.aspx';
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

//-----------------------------------------by Adee----------------------------
var Itemdesc = null;
function GetItemsBOM() {

 
    Itemdesc = $.ajax({
                type: "GET",
                url: 'CreateMRN_V2.aspx/GetItemDescription?ItemId=' + $('#ddlItem option:selected').val(),
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

                            var bom = new MrnBom();
                            bom.Material = value.Material;
                            bom.Description = value.Description;
                            bom.IsAdded = 1;
                            bom.ItemId = $('#ddlItem option:selected').val();

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


function DeleteItemSpec(index) {


    mrnBomDetails = new MrnBom();
    mrnBomDetails.Material =itemSpecs[index].Material;
    mrnBomDetails.Description = itemSpecs[index].Description;
    mrnBomDetails.ItemId = itemSpecs[index].ItemId;
   
    $.ajax({
        type: "POST",
        url: 'CreateMRN_V2.aspx/DeleteItemSpecClick',
        contentType: "application/json; charset=utf-8",
        data: '{"mrnBomDetails": ' + JSON.stringify(mrnBomDetails) + '}',
        dataType: 'json',
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {

                itemSpecs.splice(index, 1);
                $('#spnSpecCount').text(' ' + itemSpecs.length);
                populateSpecs();

            }
            else if (response.Status == 500) {
                showServerError();
          
                $('#iWait').addClass('hidden');
            }
            else {
                showSessionExpiry();
              
                $('#iWait').addClass('hidden');
            }
        },
        error: function (error) {
            console.log(error);
            showAjaxError();
           
            $('#iWait').addClass('hidden');
        }
    });

}