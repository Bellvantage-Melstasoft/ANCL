<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="BiddingSystem.TestPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
 <style>
        input[type="file"]
        {
            display: block;
        }
        .imageThumb
        {
            max-height: 75px;
            border: 2px solid;
            margin: 10px 10px 0 0;
            padding: 1px;
        }
    </style>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <h2>
            preview multiple images before upload using jQuery</h2>
        <input type="file" id="files" name="files[]" multiple />
        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
        <br />
        <br />
      
    </div>
    </form>


   <%-- <script src="http://code.jquery.com/jquery-1.11.1.min.js" type="text/javascript"></script>--%>
     <script src="jquery-(1.11.1).min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
           // debugger;
            $("[id*=files]").click(function () {
                if (window.File && window.FileList && window.FileReader) {
                    $("#files").on("change", function (e) {
                        var files = e.target.files,
                    filesLength = files.length;
                        for (var i = 0; i < filesLength; i++) {
                            var f = files[i]
                            var fileReader = new FileReader();
                            fileReader.onload = (function (e) {
                                var file = e.target;
                                $("<img></img>", {
                                    class: "imageThumb",
                                    src: e.target.result,
                                    title: file.name,
                                    name: 'test'
                                }).insertAfter("#files");
                            });
                            fileReader.readAsDataURL(f);
                        }
                    });
                } else { alert("Your browser doesn't support to File API") }
            });
        });
             
    </script>
</body>
</asp:Content>
