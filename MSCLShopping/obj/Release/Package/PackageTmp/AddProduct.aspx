<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" ValidateRequest="false" Inherits="AfluexShopping.AddProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="hdf">
    <!-- Required meta tags -->

    <title>Add Products</title>
    <!-- Latest compiled and minified CSS -->

    <%-- <!-- Fontawesome CSS -->
    <link rel="stylesheet" href="../../AdminCSS/css/font-awesome.min.css" />
    <link href="../../AdminCSS/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Feathericon CSS -->
    <link rel="stylesheet" href="../../AdminCSS/css/feathericon.min.css" />

    <link rel="stylesheet" href="../../AdminCSS/plugins/morris/morris.css" />

    <!-- Main CSS -->
    <link rel="stylesheet" href="../../AdminCSS/css/style.css" />--%>









    <!-- META DATA -->
    <%--  <meta charset="UTF-8">
    <meta name='viewport' content='width=device-width, initial-scale=1.0, user-scalable=0'>--%>

    <!-- TITLE -->
    <%-- <title>Afluex Shopping</title>--%>

    <!-- FAVICON -->
    <link rel="shortcut icon" type="image/x-icon" href="~/AssetsAdmin/images/brand/favicon.png" />
    <!-- BOOTSTRAP CSS -->
    <link href="~/AssetsAdmin/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- STYLE CSS -->
    <link href="~/AssetsAdmin/css/style.css" rel="stylesheet" />
    <link href="~/AssetsAdmin/css/dark-style.css" rel="stylesheet" />
    <link href="~/AssetsAdmin/css/skin-modes.css" rel="stylesheet" />
    <!-- SIDE-MENU CSS -->
    <%-- <link href="~/AssetsAdmin/css/sidemenu.css" rel="stylesheet" id="sidemenu-theme">--%>
    <!--C3 CHARTS CSS -->
    <link href="~/AssetsAdmin/plugins/charts-c3/c3-chart.css" rel="stylesheet" />
    <!-- P-scroll bar css-->
    <link href="~/AssetsAdmin/plugins/p-scroll/perfect-scrollbar.css" rel="stylesheet" />
    <!--- FONT-ICONS CSS -->
    <link href="~/AssetsAdmin/plugins/icons/icons.css" rel="stylesheet" />
    <!-- SIDEBAR CSS -->
    <link href="~/AssetsAdmin/plugins/sidebar/sidebar.css" rel="stylesheet" />
    <!-- SELECT2 CSS -->
    <link href="~/AssetsAdmin/plugins/select2/select2.min.css" rel="stylesheet" />
    <!-- INTERNAL Data table css -->
    <link href="~/AssetsAdmin/plugins/datatable/css/dataTables.bootstrap5.css" rel="stylesheet" />
    <link href="~/AssetsAdmin/plugins/datatable/responsive.bootstrap5.css" rel="stylesheet" />
    <!-- COLOR SKIN CSS -->
    <link id="theme" rel="stylesheet" type="text/css" media="all" href="~/AssetsAdmin/colors/color1.css" />
    <!-- INTERNAL Switcher css -->
    <link href="~/AssetsAdmin/switcher/css/switcher.css" rel="stylesheet" />
    <link href="~/AssetsAdmin/switcher/demo.css" rel="stylesheet" />
    <style>
        .errortext {
            border: 1px solid red !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <%--//////////////////////////////////////////////////////////////////////////////////////////////////////////--%>
        <div class="page-header">
            <div>
                <h1 class="page-title">Add Product</h1>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Add Product</li>
                </ol>
            </div>
        </div>
        <div class="row">
            <div class="card">
                <%--   <div>
                <p style="color: @TempData["Class"]">
                    @TempData["Banner"]
                </p>
            </div>--%>
                <div class="card-body">
                    <%--   <h4 class="card-title">Add Product</h4>--%>
                    <div class="row">
                        <div class="col-md-12">
                            <h4 style="color: red">Note: Please select the size of image atleast 1000*1000</h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>IsTimeProduct </label>
                                <asp:CheckBox ID="IsTimeProduct" runat="server" CssClass="form-control"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Main Category <span style="color: red">*</span></label>
                                <asp:DropDownList ID="ddlmaincategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlmaincategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Category <span style="color: red">*</span></label>
                                <asp:DropDownList ID="ddlcategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Sub Category <span style="color: red">*</span></label>
                                <asp:DropDownList ID="ddlsubcategory" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Model Name<span style="color: red">*</span></label>
                                <asp:TextBox ID="txtproduct" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>HSN No. <span style="color: red">*</span></label>
                                <asp:TextBox ID="txthsnno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Brand <span style="color: red">*</span></label>
                                <asp:DropDownList ID="ddlbrand" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4" id="Delivery" runat="server">
                            <div class="form-group">
                                <label>Delivery Charge <span style="color: red">*</span></label>
                                <asp:TextBox ID="txtDeliveryCharge" runat="server" CssClass="form-control" onkeypress="return isNumberOrDecimal(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="form-group">
                                <label>Short Description<span style="color: red">*</span></label>
                                <asp:TextBox ID="txtshortdescription" runat="server" CssClass="form-control" Style="height: 60px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Description<span style="color: red">*</span></label>
                                <div id="summernoteInclusions" class="click2edit">
                                    <p></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="display: none">
                        <asp:TextBox ID="txtdescription" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <%--//////////////////////////////////////////////////////////////////////////////////////////////////////////--%>


        <%--
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Add Product</h4>
                <div class="row">
                    <div class="col-md-12">
                        <h4 style="color:red">Note: Please select the size of image atleast 1000*1000</h4>
                    </div>
                </div>
                  <div class="row">
                 <div class="col-md-4">
                        <div class="form-group">
                            <label>IsTimeProduct </label>
                            <asp:CheckBox ID="IsTimeProduct" runat="server" CssClass="form-control"></asp:CheckBox>
                        </div>
                    </div> </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Main Category <span style="color: red">*</span></label>
                            <asp:DropDownList ID="ddlmaincategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlmaincategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Category <span style="color: red">*</span></label>
                            <asp:DropDownList ID="ddlcategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Sub Category <span style="color: red">*</span></label>
                            <asp:DropDownList ID="ddlsubcategory" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Model Name<span style="color: red">*</span></label>
                            <asp:TextBox ID="txtproduct" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>HSN No. <span style="color: red">*</span></label>
                            <asp:TextBox ID="txthsnno" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Brand <span style="color: red">*</span></label>
                            <asp:DropDownList ID="ddlbrand" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col-md-4" id="Delivery" runat="server" visible="false">
                        <div class="form-group">
                            <label>Delivery Charge <span style="color: red">*</span></label>
                            <asp:TextBox ID="txtDeliveryCharge" runat="server" CssClass="form-control" onkeypress="return isNumberOrDecimal(event)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="form-group">
                            <label>Short Description<span style="color: red">*</span></label>
                           
                           
                            <asp:TextBox ID="txtshortdescription" runat="server" CssClass="form-control" Style="height: 60px"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>Description<span style="color: red">*</span></label>
                              <div id="summernoteInclusions" class="click2edit"><p></p></div>

                           
                            
                        </div>
                    </div>


                </div>
                <div style="display:none">
                     <asp:TextBox ID="txtdescription" runat="server" ></asp:TextBox>
                </div>
            </div>
        </div>--%>

        <asp:ScriptManager ID="scr" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udp" runat="server">
            <ContentTemplate>
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Add Quantity</h4>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        Unit  <span style="color: red">*</span>
                                    </label>
                                    <asp:DropDownList ID="ddlunit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlunit_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2" id="divsize" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Size
                                    </label>
                                    <asp:DropDownList ID="ddlsize" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2" id="divflavor" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Flavor
                                    </label>
                                    <asp:DropDownList ID="ddlflavour" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2" id="divmaterial" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Material
                                    </label>
                                    <asp:DropDownList ID="ddlmaterial" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2" id="divcolor" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Color
                                    </label>
                                    <asp:DropDownList ID="ddlcolor" runat="server" CssClass="form-control"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-md-2" id="divram" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        RAM
                                    </label>
                                    <asp:DropDownList ID="ddlram" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2" id="divstorage" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Storage
                                    </label>
                                    <asp:DropDownList ID="ddlstorage" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2" id="divstarrating" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Star Rating
                                    </label>
                                    <asp:DropDownList ID="ddlstarrating" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        Quantity   <span style="color: red">*</span>
                                    </label>
                                    <asp:TextBox ID="txtqty" runat="server" CssClass="form-control" onkeypress="return isNumberOrDecimal(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" id="divBV" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        BV   <span style="color: red">*</span>
                                    </label>
                                    <asp:TextBox ID="txtbv" runat="server" CssClass="form-control" onkeypress="return isNumberOrDecimal(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        MRP  <span style="color: red">*</span>
                                    </label>
                                    <asp:TextBox ID="txtmrp" runat="server" CssClass="form-control" onkeypress="return isNumberOrDecimal(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        Offered Price  <span style="color: red">*</span>
                                    </label>
                                    <asp:TextBox ID="txtofferedprice" runat="server" CssClass="form-control" onkeypress="return isNumberOrDecimal(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="display: none">
                                <div class="form-group">
                                    <label>
                                        Dealer Price<span style="color: red">*</span>
                                    </label>
                                    <asp:TextBox ID="txtdealerprice" runat="server" CssClass="form-control" onkeypress="return isNumberOrDecimal(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        CGST (%) <span style="color: red">*</span>
                                    </label>
                                    <asp:TextBox ID="txtcgst" runat="server" CssClass="form-control" onkeypress="return isNumberOrDecimal(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        SGST (%) <span style="color: red">*</span>
                                    </label>
                                    <asp:TextBox ID="txtsgst" runat="server" CssClass="form-control" onkeypress="return isNumberOrDecimal(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        IGST (%) <span style="color: red">*</span>
                                    </label>
                                    <asp:TextBox ID="txtigst" runat="server" CssClass="form-control" onkeypress="return isNumberOrDecimal(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" id="redeemshopping" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Shopping Point Redeem Perc <span style="color: red">*</span>
                                    </label>
                                    <asp:TextBox ID="txtshoppingpointredemperc" runat="server" CssClass="form-control" onkeypress="return isNumberOrDecimal(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    Upload Images(You can select multiple images)
                        <asp:FileUpload runat="server" ID="flpimages" AllowMultiple="true" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <%--<asp:Button ID="btnadd" runat="server" CssClass="btn btn-primary btn-sm" Text="Add Varients" OnClick="btnadd_Click" OnClientClick="return fvalidate1();" />--%>

                                    <asp:Button ID="btnadd" runat="server" CssClass="btn btn-primary" Text="Add Varients" OnClick="btnadd_Click" OnClientClick="return fvalidate1();" />
                                </div>
                            </div>
                        </div>
                        <div class="row table-responsive">
                            <asp:GridView ID="grdvarients" runat="server" class="display nowrap table table-hover table-striped table-bordered dataTable no-footer" Visible="false" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="UnitName" HeaderText="Unit" />
                                    <asp:BoundField DataField="SizeName" HeaderText="Size" />
                                    <asp:BoundField DataField="FlavorName" HeaderText="Flavor" />
                                    <asp:BoundField DataField="ColorName" HeaderText="Color" />
                                    <asp:BoundField DataField="RAM" HeaderText="RAM" />
                                    <asp:BoundField DataField="StorageName" HeaderText="Storage" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                                    <asp:BoundField DataField="BV" HeaderText="BV" />
                                    <asp:BoundField DataField="MRP" HeaderText="MRP" />
                                    <asp:BoundField DataField="shoppingpointredemperc" HeaderText="Shopping Point Redem Perc" />
                                    <%--    <asp:BoundField DataField="DealerPrice" HeaderText="DealerPrice" />--%>
                                    <asp:BoundField DataField="CGST" HeaderText="CGST" />
                                    <asp:BoundField DataField="SGST" HeaderText="SGST" />
                                    <asp:BoundField DataField="IGST" HeaderText="IGST" />

                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <asp:Button ID="btnsave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnsave_Click" OnClientClick="return fvalidate();" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnadd" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
<script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote.js" defer></script>
<link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote.css" rel="stylesheet" />
<script src="http://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.js"></script>
<%--<script src="http://netdna.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.js"></script> --%>
<script>
    $(document).ready(function () {
        var dd = $("#txtdescription").val();
        document.getElementById('summernoteInclusions').innerHTML = dd;

        //$("#divload").css({ 'display': 'none' });
        $('#summernoteInclusions').summernote(
            {
                height: 150,
                toolbar: [
                  // [groupName, [list of button]]
                  ['style', ['bold', 'italic', 'underline', 'clear']],
                  ['font', ['strikethrough', 'superscript', 'subscript']],
                  ['fontsize', ['fontsize']],
                  ['para', ['ul', 'ol', 'paragraph']],
                  ['height', ['height']]
                ]
            });
        $('#summernoteExclusions').summernote(
            {
                height: 150,
                toolbar: [
                  // [groupName, [list of button]]
                  ['style', ['bold', 'italic', 'underline', 'clear']],
                  ['font', ['strikethrough', 'superscript', 'subscript']],
                  ['fontsize', ['fontsize']],
                  ['para', ['ul', 'ol', 'paragraph']],
                  ['height', ['height']]
                ]
            });


    });
    function fvalidate() {

        if (document.getElementById("<%=ddlmaincategory.ClientID%>").value == "0") {
            document.getElementById("<%=ddlmaincategory.ClientID%>").style.borderColor = "Red";
            document.getElementById("<%=ddlmaincategory.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=ddlcategory.ClientID%>").value == "0") {
            document.getElementById("<%=ddlcategory.ClientID%>").style.borderColor = "Red";
            document.getElementById("<%=ddlcategory.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=ddlsubcategory.ClientID%>").value == "0") {
            document.getElementById("<%=ddlsubcategory.ClientID%>").style.borderColor = "Red";
            document.getElementById("<%=ddlsubcategory.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=ddlsubcategory.ClientID%>").value == "-1") {
            document.getElementById("<%=ddlsubcategory.ClientID%>").style.borderColor = "Red";
            document.getElementById("<%=ddlsubcategory.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=txtproduct.ClientID%>").value == "") {
            document.getElementById("<%=txtproduct.ClientID%>").style.borderColor = "Red";
            document.getElementById("<%=txtproduct.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=txthsnno.ClientID%>").value == "") {
            document.getElementById("<%=txthsnno.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=txthsnno.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=ddlbrand.ClientID%>").value == "0") {
            document.getElementById("<%=ddlbrand.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=ddlbrand.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=txtshortdescription.ClientID%>").value == "") {
            document.getElementById("<%=txtshortdescription.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=txtshortdescription.ClientID%>").focus();
             return false;
         }
         var DietHTML = $('#summernoteInclusions').summernote('code');
         $('#txtdescription').val(DietHTML);
         if (document.getElementById("<%=txtdescription.ClientID%>").value == "") {
            document.getElementById("<%=txtdescription.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=txtdescription.ClientID%>").focus();
             return false;
         }


     }
     function fvalidate1() {

         if (document.getElementById("<%=ddlmaincategory.ClientID%>").value == "0") {
              document.getElementById("<%=ddlmaincategory.ClientID%>").style.borderColor = "Red";
              document.getElementById("<%=ddlmaincategory.ClientID%>").focus();
              return false;
          }
          if (document.getElementById("<%=ddlcategory.ClientID%>").value == "0") {
              document.getElementById("<%=ddlcategory.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=ddlcategory.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=ddlsubcategory.ClientID%>").value == "0") {
              document.getElementById("<%=ddlsubcategory.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=ddlsubcategory.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=ddlsubcategory.ClientID%>").value == "-1") {
              document.getElementById("<%=ddlsubcategory.ClientID%>").style.borderColor = "Red";
            document.getElementById("<%=ddlsubcategory.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=txtproduct.ClientID%>").value == "") {
              document.getElementById("<%=txtproduct.ClientID%>").style.borderColor = "Red";
            document.getElementById("<%=txtproduct.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=txthsnno.ClientID%>").value == "") {
              document.getElementById("<%=txthsnno.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=txthsnno.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=ddlbrand.ClientID%>").value == "0") {
              document.getElementById("<%=ddlbrand.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=ddlbrand.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=txtshortdescription.ClientID%>").value == "") {
              document.getElementById("<%=txtshortdescription.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=txtshortdescription.ClientID%>").focus();
             return false;
         }
         var DietHTML = $('#summernoteInclusions').summernote('code');
         $('#txtdescription').val(DietHTML);
         if (document.getElementById("<%=txtdescription.ClientID%>").value == "") {
              document.getElementById("<%=txtdescription.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=txtdescription.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=ddlunit.ClientID%>").value == "0") {
              document.getElementById("<%=ddlunit.ClientID%>").style.borderColor = "Red";
              document.getElementById("<%=ddlunit.ClientID%>").focus();
              return false;
          }
          if (document.getElementById("<%=txtqty.ClientID%>").value == "") {
              document.getElementById("<%=txtqty.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=txtqty.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=txtqty.ClientID%>").value == "0") {
              document.getElementById("<%=txtqty.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=txtqty.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=txtbv.ClientID%>").value == "") {
              document.getElementById("<%=txtbv.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=txtbv.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=txtmrp.ClientID%>").value == "") {
              document.getElementById("<%=txtmrp.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=txtmrp.ClientID%>").focus();
             return false;
         }
         if (document.getElementById("<%=txtmrp.ClientID%>").value == "0") {
              document.getElementById("<%=txtmrp.ClientID%>").style.borderColor = "Red";
              document.getElementById("<%=txtmrp.ClientID%>").focus();
              return false;
          }

          if (document.getElementById("<%=txtcgst.ClientID%>").value == "") {
              document.getElementById("<%=txtcgst.ClientID%>").style.borderColor = "Red";
              document.getElementById("<%=txtcgst.ClientID%>").focus();
              return false;
          }
          if (document.getElementById("<%=txtsgst.ClientID%>").value == "") {
              document.getElementById("<%=txtsgst.ClientID%>").style.borderColor = "Red";
             document.getElementById("<%=txtsgst.ClientID%>").focus();
             return false;
         }

         if (document.getElementById("<%=txtigst.ClientID%>").value == "") {
              document.getElementById("<%=txtigst.ClientID%>").style.borderColor = "Red";
               document.getElementById("<%=txtigst.ClientID%>").focus();
               return false;
           }
       }
       function isNumberOrDecimal(evt) {
           var charCode = (evt.which) ? evt.which : evt.keyCode;
           if (charCode != 46 && charCode > 31
             && (charCode < 48 || charCode > 57))
               return false;

           return true;
       }
</script>
