

//--------------------- Tabulation Approval  Start ------------//
var prMaster = null;
var SupQutItem = null;
var ajxQuotation = null;
var TabulationDet = null;
var currency = null;
function ViewQuotations(bidId) {

    ajxQuotation = $.ajax({
        type: "GET",
        url: 'ViewPrForTabulationsheetApprovals.aspx/LoadQutationView?bidId=' + bidId + '',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        beforeSend: function () {
            if (ajxQuotation != null) {
                ajxQuotation.abort();
            }
        },
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {

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




}

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
    showMessage('error', 'ERROR!', 'A Connection Error Occured. Please Check your Internet Connection');
}

function showSessionExpiry() {

    login();
}

function TabulationReject() {

    $('#mdlQuotations').modal('hide');

    var tableRow = $(this).closest('tr').find('td');
    $('#ContentSection_hdnQuotationItemId').val($(tableRow).eq(1).text());
    $('#ContentSection_hdnQuotationId').val($(tableRow).eq(2).text());
    var supplierName = $(tableRow).eq(5).text();

    swal.fire({
        title: 'Are you sure?',
        html: "Are you sure you want to <strong>reject</strong> the quotation from <strong>" + supplierName + "</strong>?</br></br>"
        + "<strong id='dd'>Remarks</strong>"
        + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
        type: 'warning',
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: 'Yes. Reject It!',
        cancelButtonText: 'No',
        allowOutsideClick: false,
        preConfirm: function () {
            if ($('#ss').val() == '') {
                $('#dd').prop('style', 'color:red');
                swal.showValidationError('Remarks Required');
                return false;
            }
        }
    }).then((result) => {
        if (result.value) {

            //$('#ContentSection_btnReject').click();
            TbRejectClick($('#ss').val());

        } else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {

            $('#mdlQuotations').modal('show');

        }
    });
}

function TbRejectClick(remarks) {

    prMaster = new PrMaster();
    prMaster.TerminationRemarks = remarks;
    $.ajax({
        type: "POST",
        url: 'ViewQuotationTRApproval.aspx/RejectClick',
        contentType: "application/json; charset=utf-8",
        data: '{"prMaster": ' + JSON.stringify(prMaster) + '}',
        dataType: 'json',
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                swal.fire({
                    title: 'SUCCESS',
                    html: ' <b>' + response.Data + '</b> Rejected Successfully..!!',
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonClass: 'btn btn-info btn-styled',
                    buttonsStyling: false
                }).then((result) => {
                    if (result.value) {
                        window.location = 'ViewPrForTabulationsheetApprovals.aspx';
                    }
                });
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


function PopupGvItemsModelDoc() {

    $('#mdlAttachments').modal('hide');
    $('#mdlQuotations').modal('show');
}



function PopupGvItemsModelHis() {

    $('#mdlItems').modal('hide');
    $('#mdlQuotations').modal('show');
    
}


function TabulationRejectSupplier(QuotationItemId,Status) {
   
    $('#ContentSection_hdnQuotationItemId').val(QuotationItemId);
    $('#mdlQuotations').modal('hide');
    swal.fire({
        title: 'Are you sure?',
        html: "Are you sure you want to <strong>Reject</strong> the Quotation?</br></br>"
        + "<strong id='dd'>Remarks</strong>"
        + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
        type: 'warning',
        cancelButtonColor: '#d33',
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: 'Reject',
        cancelButtonText: 'No',
        allowOutsideClick: false,
        preConfirm: function () {
            if ($('#ss').val() == '') {
                $('#dd').prop('style', 'color:red');
                swal.showValidationError('Remarks Required');
                return false;
            }

        }
    }
    ).then((result) => {
        if (result.value) {
            

            TabulationRejectSupplierClick($('#ss').val(), QuotationItemId, Status);
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            $('#mdlQuotations').modal('show');
        }
    });

}

function TabulationRejectSupplierClick(remarks, QuotationItemId,status) {

    SupQutItem = new SupplierQutItem();
    SupQutItem.Remark = remarks;
    SupQutItem.QuotationItemId = QuotationItemId;
    SupQutItem.Status = status;

    $.ajax({
        type: "POST",
        url: 'ViewQuotationTRApproval.aspx/RejectItemClick',
        contentType: "application/json; charset=utf-8",
        data: '{"SupQutItem": ' + JSON.stringify(SupQutItem) + '}',
        dataType: 'json',
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                swal.fire({
                    title: 'SUCCESS',
                    html: 'Quotation Rejected Successfully..!!',
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonClass: 'btn btn-info btn-styled',
                    buttonsStyling: false
                }).then((result) => {
                    if (result.value) {
                        $('#ContentSection_hdnRejectedQuotationCount').val(QuotationItemId);
                        $('#mdlQuotations').modal('show');
                    }
                });
            }
            else if (response.Status == 500) {
                if (response.Data != "Error") {
                    showMessage('error', 'ERROR!', response.Data);
                } else {
                    showServerError();
                    $('#iWait').addClass('hidden');
                }
                
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


function TabulationApprove(e, BidId) {
    var proceedRemark = $('.txtRemark').val();
    swal.fire({
        title: 'Are you sure?',
        html: "Are you sure you want to <strong>Approve</strong> the Bid?</br>"
        + "<strong id='dd'>Remarks</strong>"
        + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
        type: 'warning',
        cancelButtonColor: '#d33',
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: 'Approve',
        cancelButtonText: 'No',
        allowOutsideClick: false,
        preConfirm: function () {
            if ($('#ss').val() == '') {
                $('#dd').prop('style', 'color:red');
                swal.showValidationError('Remarks Required');
                return false;
            }
           
        }
    }
    ).then((result) => {
        if (result.value) {
            
            TabulationApproveClick($('#ss').val(), BidId, proceedRemark);
        } else if (result.dismiss === Swal.DismissReason.cancel) {

        }
    });

}

function TabulationApproveClick(remarks, BidId, proceedRemark) {
    SupQutItem = new SupplierQutItem();
    SupQutItem.Remark = remarks;
    SupQutItem.BidId = BidId;
    SupQutItem.ProceedRemark = proceedRemark;


    $.ajax({
        type: "POST",
        url: 'ViewQuotationTRApprovalNew.aspx/ApproveClick',
        contentType: "application/json; charset=utf-8",
        data: '{"SupQutItem": ' + JSON.stringify(SupQutItem) + '}',
        dataType: 'json',
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                swal.fire({
                    title: 'SUCCESS',
                    html: '<b> Your work has been saved. <b> Bid Approved Successfully..!! ',
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonClass: 'btn btn-info btn-styled',
                    buttonsStyling: false
                }).then((result) => {
                    if (result.value) {
                        window.location = 'ViewPrForTabulationsheetApprovals.aspx';
                    }
                });
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


function CloseModelQuotations() {
    $('#mdlQuotations').modal('hide');
    $('#ContentSection_hdnbtnEnablestatus').click();
   
}

//--------------------- Tabulation Approval  End ------------//


//--------------------- Tabulation BidComparison  Start ------------//
function Calculation(elmt) {
    var UnitPrice = $(elmt).closest('tr').find('.lblUnitPrice').text();
    var RequestingQty = $(elmt).closest('tr').find('.txtRequestingQty').val();
    var HasVat = $(elmt).closest('tr').find('.lblHasVat').text();
    var HasNbt = $(elmt).closest('tr').find('.lblHasNbt').text();
    var NbtType = $(elmt).closest('tr').find('.lblNBTType').text();
    var RequestedTotQty = $(elmt).closest('tr').find('.txtReqTotQty').val();
    var TotNBT = $(elmt).closest('tr').find('.txtTotNBT').val();
    var TotVAT = $(elmt).closest('tr').find('.txtTotVAT').val();


    var Subtotal = UnitPrice * RequestingQty;
    var Vat = 0.00;
    if (HasVat == 1) {
        // Vat = Subtotal * 0.08;
        Vat = (TotVAT / RequestedTotQty) * RequestingQty;
    }

    var Nbt = 0.00;
    if (HasNbt == 1) {
        //if (NbtType == 1) {
        //    Nbt = Subtotal * 0.0204
        //}
        //else if (NbtType == 2) {
        //    Nbt = Subtotal * 0.0200
        //}
        Nbt = (TotNBT / RequestedTotQty) * RequestingQty;


    }
    var NetTotal = Subtotal + Nbt + Vat;


    $(elmt).closest('tr').find('.lblSubTotal').text(Subtotal.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    $(elmt).closest('tr').find('.lblNbt').text(Nbt.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    $(elmt).closest('tr').find('.lblVat').text(Vat.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    $(elmt).closest('tr').find('.lblNetTotal').text(NetTotal.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));

    $(elmt).closest('tr').find('.txtSubTotal').val(Subtotal.toFixed(2));
    $(elmt).closest('tr').find('.txtNbt').val(Nbt.toFixed(2));
    $(elmt).closest('tr').find('.txtVat').val(Vat.toFixed(2));
    $(elmt).closest('tr').find('.txtNetTotal').val(NetTotal.toFixed(2));
    $(elmt).closest('tr').find('.txtSelectedQ').val("1")

}


function CalculationSupplierNew(elmt) {
    var UnitPrice = $(elmt).closest('tr').find('.lblUnitPriceSu').text();

    var RequestingQty = $(elmt).closest('tr').find('.txtRequestingQtySu').val();
    var HasVat = $(elmt).closest('tr').find('.lblHasVatSu').text();
    var HasNbt = $(elmt).closest('tr').find('.lblHasNbtSu').text();
    var NbtType = $(elmt).closest('tr').find('.lblNBTTypeSu').text();
    var RequestedTotQty = $(elmt).closest('tr').find('.txtReqTotQtySu').val();
    var TotNBT = $(elmt).closest('tr').find('.txtTotNBTSu').val();
    var TotVAT = $(elmt).closest('tr').find('.txtTotVATSu').val();


    var Subtotal = UnitPrice * RequestingQty;
    var Vat = 0.00;
    if (HasVat == 1) {
        // Vat = Subtotal * 0.08;
        Vat = (TotVAT / RequestedTotQty) * RequestingQty;
    }

    var Nbt = 0.00;
    if (HasNbt == 1) {
        //if (NbtType == 1) {
        //    Nbt = Subtotal * 0.0204
        //}
        //else if (NbtType == 2) {
        //    Nbt = Subtotal * 0.0200
        //}
        Nbt = (TotNBT / RequestedTotQty) * RequestingQty;


    }
    var NetTotal = Subtotal + Nbt + Vat;


    $(elmt).closest('tr').find('.lblSubTotalSu').text(Subtotal.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    //$(elmt).closest('tr').find('.lblNbt').text(Nbt.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    $(elmt).closest('tr').find('.lblVatSu').text(Vat.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    $(elmt).closest('tr').find('.lblNetTotalSu').text(NetTotal.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));



    $(elmt).closest('tr').find('.txtSubTotalSu').val(Subtotal.toFixed(2));
    $(elmt).closest('tr').find('.txtNbtSu').val(Nbt.toFixed(2));
    $(elmt).closest('tr').find('.txtVatSu').val(Vat.toFixed(2));
    $(elmt).closest('tr').find('.txtNetTotalSu').val(NetTotal.toFixed(2));
    $(elmt).closest('tr').find('.txtSelectedQSu').val("1")

}



function CalculationSupplier(elemt) {
   
    var QuotationId = $(elemt).closest('tr').find('.txtQuotationIdSu').val();


    var Qty = elemt.value;
    var IndexSelected = $(elemt).closest('tr').index();
    IndexSelected = IndexSelected - 1;
    var TotalRequestedQty = $(elemt).closest('tr').find('.txtReqTotQtySu').val(); 
    var QuotedQty = 0;
    SetSupplierCalculations(elemt, IndexSelected);
}


function SetSupplierCalculations(elmt,index) {
    var UnitPrice = parseFloat($(elmt).closest('tr').find('.lblUnitPriceSu').text().replace(/,/g, ''));
        
    var RequestingQty = $(elmt).closest('tr').find('.txtRequestingQtySu').val();
    var HasVat = $(elmt).closest('tr').find('.lblHasVatSu').text();
    var HasNbt = $(elmt).closest('tr').find('.lblHasNbtSu').text();
    var NbtType = $(elmt).closest('tr').find('.lblNBTTypeSu').text();
    var RequestedTotQty = $(elmt).closest('tr').find('.txtReqTotQtySu').val();
    var TotNBT = $(elmt).closest('tr').find('.txtTotNBTSu').val();
    var TotVAT = $(elmt).closest('tr').find('.txtTotVATSu').val();


    var Subtotal = UnitPrice * RequestingQty;
    var Vat = 0.00;
    if (HasVat == 1) {
        // Vat = Subtotal * 0.08;
        Vat = (TotVAT / RequestedTotQty) * RequestingQty;
    }

    var Nbt = 0.00;
    if (HasNbt == 1) {
        //if (NbtType == 1) {
        //    Nbt = Subtotal * 0.0204
        //}
        //else if (NbtType == 2) {
        //    Nbt = Subtotal * 0.0200
        //}
        Nbt = (TotNBT / RequestedTotQty) * RequestingQty;


    }
    var NetTotal = Subtotal + Nbt + Vat;

   
    $(elmt).closest('tr').find('.lblSubTotalSu').text(Subtotal.toFixed(2));
    $(elmt).closest('tr').find('.txtSubTotalSu').val(Subtotal.toFixed(2));
    $(elmt).closest('tr').find('.txtNbtSu').val(Nbt.toFixed(2));
    $(elmt).closest('tr').find('.txtVatSu'). val(Vat.toFixed(2));
    $(elmt).closest('tr').find('.txtNetTotalSu').val(NetTotal.toFixed(2));
    $(elmt).closest('tr').find('.txtSelectedQSu').val("1");
}


function SelectBidClick(elemt, status,pageStatus) {
    $('#mdlQuotations').modal('hide');
    var Qty;
    var InitQty;
    if (status == "Items") {
        Qty = $(elemt).closest('tr').find('.txtRequestingQty').val();
        InitQty = $(elemt).closest('tr').find('.txtReqTotQty').val();
    } else {
        Qty = $(elemt).closest('tr').find('.txtReqTotQtySu').val();
        InitQty = $(elemt).closest('tr').find('.txtReqTotQtySu').val();
    }

    if (parseInt(Qty, 10) > parseInt(InitQty, 10)) {
        swal.fire({
            title: 'Quantity Exceeded?',
            html: "Requested Amount Exceeded ! Are you sure you want to <strong>Select</strong> the Quotation?</br></br>"
                + "<strong id='dd'>Remarks</strong>"
                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
            type: 'warning',
            cancelButtonColor: '#d33',
            showCancelButton: true,
            showConfirmButton: true,
            confirmButtonText: 'Select',
            cancelButtonText: 'No',
            allowOutsideClick: false,
            preConfirm: function () {
                if ($('#ss').val() == '') {
                    $('#dd').prop('style', 'color:red');
                    swal.showValidationError('Remarks Required');
                    return false;
                }

            }
        }
        ).then((result) => {
            if (result.value) {
                $('#mdlQuotations').modal('hide');
                TabulationDet = new TabulationDetail();

                if (status == "Items") {
                    $(elemt).closest('tr').find('.txtSelectedQ').val("1");
                    TabulationDet = GetBidItems(elemt, $('#ss').val(), TabulationDet);

                    SelectBidItems(TabulationDet, elemt, pageStatus);
                    //ShowSelectedRowsInClick(TabulationDet);
                    //ShowSelectedRows();
                    //$('#mdlQuotations').modal('show');

                } else {
                    $(elemt).closest('tr').find('.txtSelectedQSu').val("1");
                    TabulationDet = GetBidItemsSupplier(elemt, $('#ss').val(), TabulationDet);

                    SelectBidItems(TabulationDet, elemt, pageStatus);
                    //ShowSelectedRowsInClick(TabulationDet);
                    //ShowSelectedRows();
                    //$('#mdlQuotations').modal('show');
                }



                //$('.textboxesCl').removeProp('disabled');

            } else if (result.dismiss === Swal.DismissReason.cancel) {
                $(elmt).closest('tr').find('.lblSelected').text("Not Selected");
                // $(elmt).closest('tr').find('.txtSelectedQ').val("0");

                $(elmt).closest('tr').find('.lblSubTotal').text("0.00");
                $(elmt).closest('tr').find('.lblVat').text("0.00");
                $(elmt).closest('tr').find('.lblNbt').text("0.00");
                $(elmt).closest('tr').find('.lblNetTotal').text("0.00");
                $(elmt).closest('tr').find('.txtRequestingQty').val("0.00");

            }
        });
    }
    else if (Qty > 0) {
        swal.fire({
            title: 'Are you sure?',
            html: "Are you sure you want to <strong>Select</strong> the Quotation?</br></br>"
            + "<strong id='dd'>Remarks</strong>"
            + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
            type: 'warning',
            cancelButtonColor: '#d33',
            showCancelButton: true,
            showConfirmButton: true,
            confirmButtonText: 'Select',
            cancelButtonText: 'No',
            allowOutsideClick: false,
            preConfirm: function () {
                if ($('#ss').val() == '') {
                    $('#dd').prop('style', 'color:red');
                    swal.showValidationError('Remarks Required');
                    return false;
                }

            }
        }
        ).then((result) => {
            if (result.value) {
                $('#mdlQuotations').modal('hide');
                TabulationDet = new TabulationDetail();

                if (status == "Items") {
                    $(elemt).closest('tr').find('.txtSelectedQ').val("1");
                    TabulationDet = GetBidItems(elemt, $('#ss').val(), TabulationDet);

                    SelectBidItems(TabulationDet, elemt, pageStatus);
                    //ShowSelectedRowsInClick(TabulationDet);
                    //ShowSelectedRows();
                    //$('#mdlQuotations').modal('show');

                } else {
                    $(elemt).closest('tr').find('.txtSelectedQSu').val("1");
                    TabulationDet = GetBidItemsSupplier(elemt, $('#ss').val(), TabulationDet);
                
                    SelectBidItems(TabulationDet, elemt, pageStatus);
                    //ShowSelectedRowsInClick(TabulationDet);
                    //ShowSelectedRows();
                    //$('#mdlQuotations').modal('show');
                }



                //$('.textboxesCl').removeProp('disabled');

            } else if (result.dismiss === Swal.DismissReason.cancel) {
                $(elmt).closest('tr').find('.lblSelected').text("Not Selected");
                // $(elmt).closest('tr').find('.txtSelectedQ').val("0");

                $(elmt).closest('tr').find('.lblSubTotal').text("0.00");
                $(elmt).closest('tr').find('.lblVat').text("0.00");
                $(elmt).closest('tr').find('.lblNbt').text("0.00");
                $(elmt).closest('tr').find('.lblNetTotal').text("0.00");
                $(elmt).closest('tr').find('.txtRequestingQty').val("0.00");

            }
        });
    } else {

        swal.fire({
            title: 'ERROR',
            html: "Please enter requesting quantity value</br></br>",
            type: 'error',
            allowOutsideClick: false,
            preConfirm: function () {
            }
        }
        ).then((result) => {

        });



    }

   



}






function SelectBidItems(TabulationDet, element,status) {

    var urllink;
    if (status == "Imports") {
        urllink = 'CompareQuotationsNewImports.aspx/SelectBidItemClick';
    } else {
        urllink = 'CompareQuotationsNew.aspx/SelectBidItemClick';
    }

    $.ajax({
        type: "POST",
        url: urllink,
        contentType: "application/json; charset=utf-8",
        data: '{"TabulationDet": ' + JSON.stringify(TabulationDet) + '}',
        dataType: 'json',
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                swal.fire({
                    title: 'SUCCESS',
                    html: '<b> Your work has been saved ' + '\n' + response.Data,
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonClass: 'btn btn-info btn-styled',
                    buttonsStyling: false
                }).then((result) => {
                    if (result.value) {


                        ////$('#mdlQuotations').modal('toggle');
                        ////ShowSelectedRows();
                        //if (response.ButtonStatus == "Select") {
                        //    $(element).removeClass('btn-primary');
                        //    $(element).addClass('disabled btn-success');
                        //    $(element).text("Selected");
                        //    $(element).closest('tr').find('.deleteItem').removeClass('hidden');
                        //    $(element).closest('tr').find('.txtRequestingQty').attr("disabled", "disabled"); 
                        //    $(element).closest('tr').find('.txtRequestingQtySu').attr("disabled", "disabled");
                        //    $(element).width(60);
                        //}
                        //else {
                        //    $(element).removeClass('disabled btn-success');
                        //    $(element).addClass('btn-primary');
                        //    $(element).text("Select");
                        //    $(element).closest('tr').find('.deleteItem').addClass('hidden');
                        //    $(element).closest('tr').find('.txtRequestingQty').removeAttr("disabled"); 
                        //    $(element).closest('tr').find('.txtRequestingQtySu').removeAttr("disabled", "disabled");
                        //    $(element).width(100);

                        //    $(elmt).closest('tr').find('.lblSubTotal').text("0.00");
                        //    $(elmt).closest('tr').find('.lblVat').text("0.00");
                        //    $(elmt).closest('tr').find('.lblNbt').text("0.00");
                        //    $(elmt).closest('tr').find('.lblNetTotal').text("0.00");
                        //    $(elmt).closest('tr').find('.txtRequestingQty').val("0.00");
                        //}
                        //window.location = 'CompareQuotationsNew.aspx';
                        //$('#mdlQuotations').modal('toggle');
                    }
                });
            }
            else if (response.Status == 500) {

                if (response.Data != 'Error') {
                    $('#mdlQuotations').modal('hide');
                    showMessage('error', 'ERROR!', response.Data);
                    $('#mdlQuotations').modal('show');
                } else {
                    $('#mdlQuotations').modal('hide');
                    showServerError();
                    $('#iWait').addClass('hidden');
                    $('#mdlQuotations').modal('show');
                }
              
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

function DeleteBidItems(TabulationDet, element, status) {

    var urllink;
    if (status == "Imports") {
        urllink = 'CompareQuotationsNewImports.aspx/DeleteBidItemClick';
    } else {
        urllink = 'CompareQuotationsNew.aspx/DeleteBidItemClick';
    }

    $.ajax({
        type: "POST",
        url: urllink,
        contentType: "application/json; charset=utf-8",
        data: '{"TabulationDet": ' + JSON.stringify(TabulationDet) + '}',
        dataType: 'json',
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                swal.fire({
                    title: 'SUCCESS',
                    html: '<b> Your work has been saved ' + '\n' + response.Data,
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonClass: 'btn btn-info btn-styled',
                    buttonsStyling: false
                }).then((result) => {
                    if (result.value) {
                        
                    }
                });
            }
            else if (response.Status == 500) {

                if (response.Data != 'Error') {
                    $('#mdlQuotations').modal('hide');
                    showMessage('error', 'ERROR!', response.Data);
                    $('#mdlQuotations').modal('show');
                } else {
                    $('#mdlQuotations').modal('hide');
                    showServerError();
                    $('#iWait').addClass('hidden');
                    $('#mdlQuotations').modal('show');
                }

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

function GetBidItemsSupplier(elemt, remark, TabulationDet) {

    var index = $(elemt).closest('tr').index();
    
    TabulationDet.TotQty = $(elemt).closest('tr').find('.txtRequestingQtySu').val();
    TabulationDet.VAtAmount = $(elemt).closest('tr').find('.txtVatSu').val();
    TabulationDet.NbtAmount = $(elemt).closest('tr').find('.txtNbtSu').val();
    TabulationDet.SubTotal = $(elemt).closest('tr').find('.txtSubTotalSu').val();
    TabulationDet.NetTotal = $(elemt).closest('tr').find('.txtNetTotalSu').val();
    TabulationDet.ApprovalRemark = remark;
    TabulationDet.QuotationId = $(elemt).closest('tr').find('.txtQuotationIdSu').val();
    TabulationDet.TabulationId = $(elemt).closest('tr').find('.lblTablationIdSu').text();
    TabulationDet.SupplierId = $(elemt).closest('tr').find('.lblSelectedSupplierIDSu').text();
    TabulationDet.ItemId = $(elemt).closest('tr').find('.lblItemIdSu').text();
    TabulationDet.SelectedValue = $(elemt).closest('tr').find('.txtSelectedQSu').val();
    TabulationDet.QuotationItemId = $(elemt).closest('tr').find('.lblQuotationItemIdSu').text();
    TabulationDet.Qty = parseInt($(elemt).closest('tr').find('.txtReqTotQtySu').val());
    TabulationDet.SupplierMentionedItemName = $(elemt).closest('tr').find('.lblSupplierMentionedItemNameSu').val();
    return TabulationDet;
}


function GetBidItems(elemt, remark, TabulationDet) {

    var index = $(elemt).closest('tr').index();
    var tableRow = $(elemt).closest('tr').find('td');
    TabulationDet.TotQty = $(elemt).closest('tr').find('.txtRequestingQty').val();
    TabulationDet.VAtAmount = $(elemt).closest('tr').find('.txtVat').val();
    TabulationDet.NbtAmount = $(elemt).closest('tr').find('.txtNbt').val();
    TabulationDet.SubTotal = $(elemt).closest('tr').find('.txtSubTotal').val();
    TabulationDet.NetTotal = $(elemt).closest('tr').find('.txtNetTotal').val();
    TabulationDet.ApprovalRemark = remark;
    TabulationDet.QuotationId = tableRow.eq(2).text();
    TabulationDet.TabulationId = $(elemt).closest('tr').find('.lblTablationId').text();
    TabulationDet.SupplierId = tableRow.eq(4).text();
    TabulationDet.ItemId = $(elemt).closest('tr').find('.lblItemId').text();
    TabulationDet.SelectedValue = $(elemt).closest('tr').find('.txtSelectedQ').val();
    TabulationDet.QuotationItemId = tableRow.eq(1).text();
    TabulationDet.Qty = parseInt($(elemt).closest('tr').find('.txtReqTotQty').val());
    TabulationDet.SupplierMentionedItemName = tableRow.eq(15).text();
    return TabulationDet;
}


function RemoveBidItemClick(elemt, status,pageStatus) {
    $('#mdlQuotations').modal('hide');
    swal.fire({
        title: 'Are you sure?',
        html: "Are you sure you want to <strong>Remove</strong> Selected Item ?</br></br>",
        type: 'warning',
        cancelButtonColor: '#d33',
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        allowOutsideClick: false,
        preConfirm: function () {
            if ($('#ss').val() == '') {
                $('#dd').prop('style', 'color:red');
                
                return false;
            }

        }
    }
    ).then((result) => {
        if (result.value) {

            TabulationDet = new TabulationDetail();

            if (status == "Items") {
                $(elemt).closest('tr').find('.txtSelectedQ').val("0");
                TabulationDet = GetBidItems(elemt, $('#ss').val(), TabulationDet);

                // ** SelectBidItems(TabulationDet, elemt, pageStatus);
                DeleteBidItems(TabulationDet, elemt, pageStatus);

                //ShowSelectedRowsInClick(TabulationDet, "remove");
                //ShowSelectedRows();
                //$('#mdlQuotations').modal('toggle');

            } else {
                $(elemt).closest('tr').find('.txtSelectedQSu').val("0");
                TabulationDet = GetBidItemsSupplier(elemt, $('#ss').val(), TabulationDet);

                // ** SelectBidItems(TabulationDet, elemt, pageStatus);
                DeleteBidItems(TabulationDet, elemt, pageStatus);

                //ShowSelectedRowsInClick(TabulationDet, "remove");
                //ShowSelectedRows();
                //$('#mdlQuotations').modal('toggle');
            }



            //$('.textboxesCl').removeProp('disabled');

        } else if (result.dismiss === Swal.DismissReason.cancel) {
          

        }
    });

}






function FinalizeTabulationClick(status) {

    $('#mdlQuotations').modal('hide');
    swal.fire({
        title: 'Are you sure?',
        html: "Are you sure you want to <strong>Finalize Tabulation</strong>?</br></br>"
        + "<strong id='dd'>Remarks</strong>"
        + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
        type: 'warning',
        cancelButtonColor: '#d33',
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: 'Finalize',
        cancelButtonText: 'No',
        allowOutsideClick: false,
        preConfirm: function () {
            if ($('#ss').val() == '') {
                $('#dd').prop('style', 'color:red');
                swal.showValidationError('Remarks Required');
                return false;
            }
        }
    }
    ).then((result) => {
        if (result.value) {


            FinalizeTabulation($('#ss').val(), status);
        } else if (result.dismiss === Swal.DismissReason.cancel) {

        }
    });

}

function FinalizeTabulation(remark, status) {
    SupQutItem = new SupplierQutItem();
    SupQutItem.Remark = remark;


    var urllink;
    if (status == "Imports") {
        urllink = 'CompareQuotationsNewImports.aspx/FinalizeTabulationClick';
    } else {
        urllink = 'CompareQuotationsNew.aspx/FinalizeTabulationClick';
    }
   
    $.ajax({
        type: "POST",
        url: urllink,
        contentType: "application/json; charset=utf-8",
        data: '{"SupQutItem": ' + JSON.stringify(SupQutItem) + '}',
        dataType: 'json',
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                swal.fire({
                    title: 'SUCCESS',
                    html: '<b> ' + response.Data  + ' Your work has been saved ..!!',
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonClass: 'btn btn-info btn-styled',
                    buttonsStyling: false
                }).then((result) => {
                    if (result.value) {
                        window.location = 'ViewPrForQuotationComparison.aspx';
                    }
                });
            }
            else if (response.Status == 500) {

                if (response.Data != 'Error') {

                    showMessage('error', 'ERROR!', response.Data);
                } else {
                    showServerError();
                    $('#iWait').addClass('hidden');
                }

            }
            else if (response.Status == 600) {

                if (response.Data == 'Error') {

                    showMessage('error', 'ERROR!', 'Assign Recommandation and approval limits');
                } else {
                    showServerError();
                    $('#iWait').addClass('hidden');
                }

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


function ReOpenBidClickC(element) {
    var tableRow = $(element).closest('tr:not(:has(>td>table))')[0].cells;
    var BidId = $(tableRow).eq(1).text();

        swal.fire({
            title: 'Are you sure?',
            html: "Reopening Bid will discard all the selections and terminations done.</br></br>"
            + "<strong id='dd'>Duration (Days)</strong>"
            + "<input id='ss' type='number' value='1' min='1' class ='form-control' required='required'/></br></br>"
            + "<strong id='ee'>Remark</strong>"
            + "<input id='pp'  class ='form-control' required='required'/></br>",
            type: 'warning',
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            showCancelButton: true,
            showConfirmButton: true,
            confirmButtonText: 'OK',
            cancelButtonText: 'Cancel',
            allowOutsideClick: false,
            preConfirm: function () {
                if ($('#ss').val() == '' || $('#pp').val() == '') {
                    $('#dd').prop('style', 'color:red');
                    $('#ee').prop('style', 'color:red');
                    swal.showValidationError('Field Cannot Be Empty');
                    return false;
                }
                else {
                    $('#ContentSection_hdnReOpenDays').val($('#ss').val());
                    $('#ContentSection_hdnReopenRemarks').val($('#pp').val());
                }
            }
        }).then((result) => {
            if (result.value) {

                ReOpenBid($('#ss').val(), $('#pp').val(), BidId);

            }
        });

    
}

function ReOpenBid(Days, Remarks, BidId) {
    SupQutItem = new SupplierQutItem();
    SupQutItem.Remark = Remarks;
    SupQutItem.Days = Days;
    SupQutItem.BidId = BidId

    $.ajax({
        type: "POST",
        url: 'CompareQuotationsNew.aspx/ReOpenBidClick',
        contentType: "application/json; charset=utf-8",
        data: '{"SupQutItem": ' + JSON.stringify(SupQutItem) + '}',
        dataType: 'json',
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                swal.fire({
                    title: 'SUCCESS',
                    html: '<b> ' + response.Data + ' Your work has been saved ..!!',
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonClass: 'btn btn-info btn-styled',
                    buttonsStyling: false
                }).then((result) => {
                    if (result.value) {
                        window.location = 'ViewPrForQuotationComparison.aspx';
                    }
                });
            }
            else if (response.Status == 500) {

                if (response.Data != 'Error') {

                    showMessage('error', 'ERROR!', response.Data);
                } else {
                    showServerError();
                    $('#iWait').addClass('hidden');
                }

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
            location.reload();
        }
    });

}

function ReOpenBidClick(element) {
    var tableRow = $(element).closest('tr:not(:has(>td>table))')[0].cells;
    var BidId = $(tableRow).eq(1).text();

    swal.fire({
        title: 'Are you sure?',
        html: "Reopening Bid will discard all the selections and terminations done.</br></br>"
            + "<strong id='dd'>Duration (Days)</strong>"
            + "<input id='ss' type='number' value='1' min='1' class ='form-control' required='required'/></br></br>"
            + "<strong id='ee'>Remark</strong>"
            + "<input id='pp'  class ='form-control' required='required'/></br>",
        type: 'warning',
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: 'OK',
        cancelButtonText: 'Cancel',
        allowOutsideClick: false,
        preConfirm: function () {
            if ($('#ss').val() == '' || $('#pp').val() == '') {
                $('#dd').prop('style', 'color:red');
                $('#ee').prop('style', 'color:red');
                swal.showValidationError('Field Cannot Be Empty');
                return false;
            }
            else {
                $('#ContentSection_hdnReOpenDays').val($('#ss').val());
                $('#ContentSection_hdnReopenRemarks').val($('#pp').val());
            }
        }
    }).then((result) => {
        if (result.value) {

            ReOpenBidTabRev($('#ss').val(), $('#pp').val(), BidId);

        }
    });


}

function ReOpenBidTabRev(Days, Remarks, BidId) {
    SupQutItem = new SupplierQutItem();
    SupQutItem.Remark = Remarks;
    SupQutItem.Days = Days;
    SupQutItem.BidId = BidId

    $.ajax({
        type: "POST",
        url: 'CompareQuotationsNew.aspx/ReOpenBidClick',
        contentType: "application/json; charset=utf-8",
        data: '{"SupQutItem": ' + JSON.stringify(SupQutItem) + '}',
        dataType: 'json',
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                swal.fire({
                    title: 'SUCCESS',
                    html: '<b> ' + response.Data + ' Your work has been saved ..!!',
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonClass: 'btn btn-info btn-styled',
                    buttonsStyling: false
                }).then((result) => {
                    if (result.value) {
                        window.location = 'ViewPrForTabulationsheetApprovals.aspx';
                    }
                });
            }
            else if (response.Status == 500) {

                if (response.Data != 'Error') {

                    showMessage('error', 'ERROR!', response.Data);
                } else {
                    showServerError();
                    $('#iWait').addClass('hidden');
                }

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
            location.reload();
        }
    });

}

//--------------------- Tabulation BidComparison  End ------------//


//--------------------- Tabulation BidComparison  Start - Imports------------//

function SaveRates() {

    var dataList =[];
    $("#ContentSection_gvRatss tr").each(function () {
        dataList.push({
            CurrencyTypeId : $(this).find('td').eq(2).text(),
            SellingRate : $(this).find('td').find('.txtRate').val()
        });
    });

   

    $.ajax({
        type: "POST",
        url: 'CompareQuotationsNewImports.aspx/SaveRatesClick',
        contentType: "application/json; charset=utf-8",
        data: '"dataList": [ { ' + JSON.stringify(dataList) + '}]',
        dataType: 'json',
        success: function (response) {
            response = JSON.parse(response.d);
            if (response.Status == 200) {
                swal.fire({
                    title: 'SUCCESS',
                    html: '<b> ' + response.Data + ' Your work has been saved ..!!',
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonClass: 'btn btn-info btn-styled',
                    buttonsStyling: false
                }).then((result) => {
                    if (result.value) {
                        window.location = 'ViewPrForQuotationComparison.aspx';
                    }
                });
            }
            else if (response.Status == 500) {

                if (response.Data != 'Error') {

                    showMessage('error', 'ERROR!', response.Data);
                } else {
                    showServerError();
                    $('#iWait').addClass('hidden');
                }

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
            location.reload();
        }
    });
    
}


function PopupRateSection(SeletedBidId) {

 

    $('#mdlRates').modal('hide'); 
    swal.fire({
        title: 'SUCCESS',
        html: 'Your work has been saved',
        type: 'success',
        showCancelButton: false,
        confirmButtonClass: 'btn btn-info btn-styled',
        buttonsStyling: false
    }).then((result) => {
        if (result.value) {
            $('.modal-backdrop').remove();
            $("#ContentSection_gvBids tr").each(function () {
                var bidID = $(this).find('td').eq(1).text();
                if (bidID == SeletedBidId) {
                    $('.btnTab').removeClass('hidden');
                    $('.btnReopn').removeClass('hidden');
                    $('.btnConfirm').addClass('hidden');
                }
               
            });
        }
    });
    
    
}


function CloseModelImportsModel() {
    $('#mdlImportDetails').modal('hide');
    $('.modal-backdrop').remove();
    $('#mdlQuotations').modal('show');

}


function ShowSelectedRows() {

    $("#ContentSection_gvItems tr").each(function () {
        var index = $(this).closest('tr').index();
       
        $(this).find('td').each(function () {
            var myIndex = $(this).parent().index();
            var tableRow = $(this).closest('tr').find('td');
          
            var selectedNew = $('#' + 'ContentSection_gvItems_gvQuotationItems_' + myIndex + '_lblIsSelectedTB_' + index).text();
            //var selectedSupplier = $(this).find('td').find('.lblIsSelectedTBSu').text();
            //var selected = $(this).find('td').find('.lblIsSelectedTB').text();
            var selected = tableRow.find('.lblIsSelectedTB').text();
                if (selected == 1) {
                    var quotitem = tableRow.eq(1).text();
                    tableRow.find('.btnSelect').removeClass('btn-primary');
                    tableRow.find('.btnSelect').addClass('disabled btn-success');
                    tableRow.find('.btnSelect').text("Selected");
                    tableRow.find('.deleteItem').removeClass('hidden');
                    tableRow.find('.txtRequestingQty').attr("disabled", "disabled");
                    tableRow.find('.txtRequestingQtySu').attr("disabled", "disabled");
                    tableRow.find('.btnSelect').width(60);
                }
            }); 

        
       
       

    });

    $("#ContentSection_gvQuotationItemsSup tr").each(function () {
        var index = $(this).closest('tr').index();

        $(this).find('td').each(function () {
            var myIndex = $(this).parent().index();
            var tableRowSu = $(this).closest('tr').find('td');

            var selectedNew = $('#' + 'ContentSection_gvQuotationItemsSup_gvItemSupllier_' + myIndex + '_lblIsSelectedTBSu_' + index).text();
            //var selectedSupplier = $(this).find('td').find('.lblIsSelectedTBSu').text();
            var selectedSupplier = tableRowSu.find('.lblIsSelectedTBSu').text();
            if (selectedSupplier == 1) {
                var quotitem = tableRowSu.eq(1).text();
                tableRowSu.find('.btnSelectSu').removeClass('btn-primary');
                tableRowSu.find('.btnSelectSu').addClass('disabled btn-success');
                tableRowSu.find('.btnSelectSu').text("Selected");
                tableRowSu.find('.deleteItem').removeClass('hidden');
                tableRowSu.find('.txtRequestingQtySu').attr("disabled", "disabled");
                tableRowSu.find('.btnSelectSu').width(60);
            }




        });
       

    }); 
}



//function ShowSelectedRowsInClick(TabulationDet) {

//    $("#ContentSection_gvItems tr").each(function () {
//        var index = $(this).closest('tr').index();

//        $(this).find('td').each(function () {
//            var myIndex = $(this).parent().index();
//            var tableRow = $(this).closest('tr').find('td');

//            var selectedNew = $('#' + 'ContentSection_gvItems_gvQuotationItems_' + myIndex + '_lblIsSelectedTB_' + index).text();
//            var selected = tableRow.find('.lblIsSelectedTB').text();
//            var selectedQuot = tableRow.eq(1).text();
//            if (TabulationDet.QuotationItemId == selectedQuot) {
//                var quotitem = tableRow.eq(1).text();
//                tableRow.find('.lblIsSelectedTB').val("1");
                
//            }
//        });





//    });

//    $("#ContentSection_gvQuotationItemsSup tr").each(function () {
//        var index = $(this).closest('tr').index();

//        $(this).find('td').each(function () {
//            var myIndex = $(this).parent().index();
//            var tableRowSu = $(this).closest('tr').find('td');

//            var selectedNew = $('#' + 'ContentSection_gvQuotationItemsSup_gvItemSupllier_' + myIndex + '_lblIsSelectedTBSu_' + index).text();
//            //var selectedSupplier = $(this).find('td').find('.lblIsSelectedTBSu').text();
//            var selectedSupplier = tableRowSu.find('.lblIsSelectedTBSu').text();
//            var quotitem = tableRowSu.find('.lblQuotationItemIdSu').text();
//            if (TabulationDet.QuotationItemId == quotitem) {
//               tableRowSu.find('.lblIsSelectedTBSu').val("1");
                
                
//            }




//        });


//    });
//}

