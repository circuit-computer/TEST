<%@ Page Language="VB" MasterPageFile="~/Site/MasterPage.master" AutoEventWireup="false" CodeFile="frm_CompanyRegistraion.aspx.vb" Inherits="Site_frm_CompanyRegistraion" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<%@ Register Src="../sysCtrl/cmdBar.ascx" TagName="cmdBar" TagPrefix="uc1" %>

<%@ Register Src="../sysCtrl/AAI_OCX.ascx" TagName="aai_ocx" TagPrefix="uc2" %>
<%@ Register Src="../sysCtrl/msg_Box.ascx" TagName="msg_Box" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        //$(document).ready(function () {
        //    $(".datetimepicker").datetimepicker();
        //});

        // <!-- UnComment When You Want Calendar In UpdatePanel -->

        //function date() {
        //    $(".datetimepicker").datetimepicker();
        //}
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="mainPanel" runat="Server">


    <script type="text/javascript">
        function date() {
            $(".datetimepicker").datetimepicker();
        }
    </script>
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" />


      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
                Sys.Application.add_load(date);
            </script>


        <div class="div_main">


            <div class="jumbotron jumbotron-fluid">

                <div class="container">
                    <div class="container">

                        <div class="col-md-12">
                            <!-- Horizontal Form -->
                            <div class="box">

                                <!-- /.box-header -->
                                <!-- form start -->
                                <div class="form-horizontal">
                                    <div class="box-body">

                                        <div class="form-group">
                                            <label class="col-sm-2 tdLBL">Name</label>

                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txt_Company_Name" runat="server" CssClass="form-control"></asp:TextBox>


                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 tdLBL">Email</label>

                                            <div class="col-sm-5">

                                                <asp:TextBox ID="txt_Company_Email" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 tdLBL">Contact No</label>

                                            <div class="col-sm-5">



                                                <asp:TextBox ID="txt_Company_Contact_No" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 tdLBL">Representative Name</label>

                                            <div class="col-sm-5">



                                                <asp:TextBox ID="txt_Company_Rep_Name" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 tdLBL">Login Name</label>

                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txt_Login_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 tdLBL">Login Password</label>

                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txt_Login_Password" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div>
                                            <div class="col-md-5">
                                            </div>
                                            <div class="col-md-3">
                                                <uc1:cmdBar ID="cmdBar1" runat="server" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Panel ID="pnlMSG" runat="server" Visible="False">
                                                    <uc1:msg_Box ID="ctrl_MSG" runat="server" />
                                                </asp:Panel>

                                            </div>
                                        </div>



                                    </div>

                                </div>
                            </div>
                        </div>


                    </div>
                </div>

            </div>
        </div>

        </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
