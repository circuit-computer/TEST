<%@ Page Language="VB" MasterPageFile="~/Site/MasterPage.master" AutoEventWireup="false" CodeFile="frm_ComplainResponse.aspx.vb" Inherits="Site_frm_ComplainResponse" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<%@ Register Src="../sysCtrl/cmdBar.ascx" TagName="cmdBar" TagPrefix="uc1" %>

<%@ Register Src="../sysCtrl/AAI_OCX.ascx" TagName="aai_ocx" TagPrefix="uc2" %>
<%@ Register Src="../sysCtrl/msg_Box.ascx" TagName="msg_Box" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
     
        //www.codescratcher.com/javascript/search-gridview-using-javascript/
        function Search_Gridview(strKey) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById('<%= Gv.ClientID%>');
            var rowData;

            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
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

                <div class="col-md-12">
                    <!-- Horizontal Form -->
                    <div class="box">

                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="form-horizontal">
                            <div class="box-body">

                                <div class="form-group">
                                    <label class="col-sm-2 tdLBL">Complain No</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtTickectNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>


                                    </div>

                                </div>

                                <div class="form-group">
                                    <label class="col-sm-2 tdLBL">First Response Date</label>

                                    <div class="col-sm-5">
                                        <div class="input-group date">
                                            <div class="input-group-addon ">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFirstResDate" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label class="col-sm-2 tdLBL">Completion Date</label>

                                    <div class="col-sm-5">

                                        <div class="input-group date">
                                            <div class="input-group-addon ">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtComDate" runat="server" CssClass="form-control datetimepicker" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-2 tdLBL">Complain Status</label>

                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cmbQueryStatus" runat="server" CssClass="form-control">

                                            <asp:ListItem Selected="True" Value="9999">Empty</asp:ListItem>
                                            <asp:ListItem Value="1">Inprocess</asp:ListItem>
                                            <asp:ListItem Value="2">Completed</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">

                                    <%-- <div class="row">
                                <div class="col-lg-12">
                                    <div class="text-center">
                                        <h4>Issue Details</h4>
                                    </div>
                                </div>
                            </div>--%>

                                    <label class="col-sm-2 tdLBL">Fixing Detail</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtFixingDetail" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 tdLBL">Total Days</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtTotalDays" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 tdLBL">Total Hours</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtTotalHours" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-3">
                                        <uc1:cmdBar ID="cmdBar1" runat="server" />
                                    </div>
                                </div>
                                  <div class="row">
                                    <div class="col-md-12">
                                        <hr />
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-9">
                                    </div>
                                    <div class="col-md-3">

                                        <div style="text-align: right">
                                            <asp:TextBox ID="txt_search" runat="server" Style="padding: 5px"
                                                CssClass="form-control" placeholder="Search.." onkeyup="Search_Gridview(this)"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <hr />
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <asp:Panel ID="pnlGid" runat="server" ScrollBars="Vertical" Height="300px">
                                            <asp:GridView ID="Gv" class="table table-bordered" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select" ShowHeader="False">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                                <HeaderStyle BackColor="cornflowerblue" BorderColor="Silver" ForeColor="White" />
                                                <RowStyle BorderColor="Silver" />

                                            </asp:GridView>

                                        </asp:Panel>


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
            
        </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>
