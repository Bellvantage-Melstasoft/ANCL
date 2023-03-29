
var UserName;
var Password;

function login() {
    swal.fire({
        title: 'Session Expired',
        html: "Please Login to Continue</br></br>"
            + "<strong id='usr'>User Name</strong>"
            + "<input id='txtUsrName' type='text' class ='form-control' required='required'/></br>"
            + "<strong id='pass'>Password</strong>"
            + "<input id='txtPass' type='password' class ='form-control' required='required'/></br>",
        type: 'warning',
        cancelButtonColor: '#d33',
        showConfirmButton: true,
        confirmButtonText: 'Login',
        allowOutsideClick: false,
        preConfirm: function () {
            if ($('#txtUsrName').val() == '' || $('#txtPass').val() == '') {
                $('#usr').prop('style', 'color:red');
                $('#pass').prop('style', 'color:red');
                swal.showValidationError('Please Enter Your Login Credentials');
                return false;
            }
            else {
                UserName = $('#txtUsrName').val();
                Password = $('#txtPass').val();
            }

        }
    }).then((result) => {
        if (result.value) {

            $.ajax({
                type: "GET",
                url: "LoginPage.aspx/LoginViaService?UserName='" + UserName + "'&Password='" + Password+"'",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    response = JSON.parse(response.d);
                    if (response.Status != 200) {
                        swal.fire({
                            title: 'ERROR',
                            html: response.Data,
                            type: 'error',
                            showCancelButton: false,
                            confirmButtonClass: 'btn btn-info btn-styled',
                            allowOutsideClick: false,
                            buttonsStyling: false
                        }).then((result) => {
                            if (result.value) {
                                login();
                            }
                        });
                    }
                },
                error: function (error) {
                    console.log(error);
                    showAjaxError();
                }
            });
        }
    });
}