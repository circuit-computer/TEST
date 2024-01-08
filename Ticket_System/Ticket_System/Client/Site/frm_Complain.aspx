<%@ Page Language="VB" MasterPageFile="~/Site/MasterPage.master" AutoEventWireup="false" CodeFile="frm_Complain.aspx.vb" Inherits="Site_frm_Complain" %>

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
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-7">
                            <!-- Horizontal Form -->
                            <div class="box">

                                <!-- /.box-header -->
                                <!-- form start -->
                                <div class="form-horizontal">
                                    <div class="box-body">



                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Representative Name</label>

                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtCompRepId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                <asp:TextBox ID="txtCompId" Visible="false" runat="server"></asp:TextBox>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Complain Type</label>

                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="cmbQueryType" runat="server" CssClass="form-control">

                                                    <asp:ListItem Selected="True">Empty</asp:ListItem>
                                                    <asp:ListItem>Customization</asp:ListItem>
                                                    <asp:ListItem>Support</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group hidden">
                                            <label class="col-sm-3 control-label">Reported By</label>

                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="cmbReportedBy" runat="server" CssClass="form-control">

                                                    <asp:ListItem Selected="True">Empty</asp:ListItem>
                                                    <asp:ListItem>Message</asp:ListItem>
                                                    <asp:ListItem>Call</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-1"></div>
                                                <div class="col-md-10">
                                                    <div class="bg-gray-light">

                                                        <h3 class="text-center border-light-gray">Issue Details</h3>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="row">
                                <div class="col-lg-12">
                                    <div class="text-center">
                                        <h4>Issue Details</h4>
                                    </div>
                                </div>
                            </div>--%>

                                            <label class="col-sm-3 control-label">Detail 1</label>

                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtIssueDetail1" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Detail 2</label>

                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtIssueDetail2" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Detail 3</label>

                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtIssueDetails3" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row">
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
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <div class="row">
                                            <div class="col-md-7"></div>
                                            <div class="col-md-5">
                                                <uc1:cmdBar ID="cmdBar1" runat="server" />
                                            </div>
                                        </div>

                                    </div>
                                    <!-- /.box-footer -->
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    

</asp:Content>
