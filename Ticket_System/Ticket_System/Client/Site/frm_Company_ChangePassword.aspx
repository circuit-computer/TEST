<%@ Page Language="VB" MasterPageFile="~/Site/MasterPage.master" AutoEventWireup="false" CodeFile="frm_Company_ChangePassword.aspx.vb" Inherits="Site_frm_Company_ResetPassword" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<%@ Register Src="../sysCtrl/cmdBar.ascx" TagName="cmdBar" TagPrefix="uc1" %>

<%@ Register Src="../sysCtrl/AAI_OCX.ascx" TagName="aai_ocx" TagPrefix="uc2" %>
<%@ Register Src="../sysCtrl/msg_Box.ascx" TagName="msg_Box" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
                    <!-- Horizontal Form -->
                    <div class="box">

                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="form-horizontal">
                            <div class="box-body">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Login ID</label>

                                    <div class="col-sm-5">
                                        <%--<asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>--%>
                                        <asp:TextBox ID="txt_Login_ID" runat="server" CssClass="form-control"
                                            Enabled="True"></asp:TextBox>

                                    </div>
                                    <%--<div class="col-sm-2">
                                <asp:Button ID="btnCustID" runat="server" CssClass="btn btn-default" Text="Search" />

                            </div>--%>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Old Password</label>

                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txt_OldPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-3 control-label">New Password</label>

                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txt_Password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Re - Password</label>

                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txt_RePassWord" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <!-- /.box-body -->
                            <div class="box-footer">
                                <div class="row">
                                    <div class="col-md-7">
                                    </div>
                                    <div class="col-md-5">
                                        <uc1:cmdBar ID="cmdBar1" runat="server" />
                                    </div>
                                </div>

                            </div>
                            <!-- /.box-footer -->
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnlMSG" runat="server" Visible="False">
                    <uc1:msg_Box ID="ctrl_MSG" runat="server" />
                </asp:Panel>

                <asp:Panel ID="Panel2" runat="server" Visible="False">
                    <br />
                    <uc2:aai_ocx ID="aai_ocx1" runat="server" />
                    <br />
                </asp:Panel>
            </div>
        </div>
    </div>

            </ContentTemplate>
            </asp:UpdatePanel>

</asp:Content>
