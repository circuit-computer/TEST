<%@ Page Language="VB" MasterPageFile="~/Site/MasterPage.master" AutoEventWireup="false" CodeFile="frm_Pending_List.aspx.vb" Inherits="Site_frm_Pending_List" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<%@ Register Src="../sysCtrl/cmdBar.ascx" TagName="cmdBar" TagPrefix="uc1" %>

<%@ Register Src="../sysCtrl/AAI_OCX.ascx" TagName="aai_ocx" TagPrefix="uc2" %>
<%@ Register Src="../sysCtrl/msg_Box.ascx" TagName="msg_Box" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="mainPanel" runat="Server">

  


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
                                    <label class="col-sm-2 tdLBL">Date</label>

                                    <div class="col-sm-5">
                                        <div class="input-group date">
                                            <div class="input-group-addon ">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-md-7"></div>
                                    <div class="col-md-5">
                                        <uc1:cmdBar ID="cmdBar1" runat="server" />
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
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
    </div>


         
</asp:Content>

