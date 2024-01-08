<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AAI_OCX.ascx.vb" Inherits="WebUserControl" %>

<style>
    #pnlItem {
        border-collapse: separate;
        border: solid #0575E6 2px;
        border-radius: 5px !important;
        -moz-border-radius: 5px;
        background-color: whitesmoke;
    }

    .buttonclose {
        background-color: white; /* Green */
        border: none;
        color: red;
        padding: 2px 4px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        margin: 4px 4px;
        cursor: pointer;
        border-radius: 2px;
        border: 2px solid #f44336;
    }
</style>
<script type="text/javascript">
    //www.codescratcher.com/javascript/search-gridview-using-javascript/
    function Search_Gridview(strKey) {

        var strData = strKey.value.toLowerCase().split(" ");
        var tblData = document.getElementById('<%= dg.ClientID%>');
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
<div class="panel-group">
    <div class="panel panel-info">
        <div class="panel-heading">

            <asp:TextBox ID="SearchByTagTB" runat="server" Style="width: 150px; margin-left: 15px" placeholder="Search" AccessKey="s" onkeyup="Search_Gridview(this)"></asp:TextBox>

            &nbsp;<asp:Button ID="Button1" runat="server" Text="FIND" Font-Bold="True" CssClass="btnCmd_RED" AccessKey="o" ToolTip="SEARCH ( Alt + O)" />
            &nbsp;<asp:TextBox ID="txtSenderRef" runat="server" Visible="False"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="lblfilter" runat="server" Text="Filter" Font-Bold="True" Height="25px" Style="font-family: Arial, Helvetica, sans-serif;" Font-Size="Small"></asp:Label>
            &nbsp;&nbsp;
            <asp:DropDownList ID="cmb_field" runat="server" CssClass="txt_1" TabIndex="5">
            </asp:DropDownList>

            <asp:TextBox ID="txt_field_search" runat="server" Style="width: 150px" placeholder="Search by Feild"></asp:TextBox>

            &nbsp;<asp:Button ID="btn_Find_2" runat="server" Text="FIND BY FIELD" Font-Bold="True" CssClass="btnCmd_RED" />
            &nbsp; &nbsp; &nbsp;
              <asp:ImageButton ID="btnClose" runat="server" Text="X" Font-Bold="true" Style="float: right; margin-right: 20px; display: inline" ImageUrl="~/Site/images/close.png" ToolTip="Close Window ( ALT + W )" AccessKey="w" />


        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12">
                    <div style="height: 344px;" id="pnlItem">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div style="width: 800px; height: 297px; overflow: auto">
                                    <asp:GridView ID="dg" runat="server" CssClass="table  table-sm table-striped table-bordered table-condensed table-dark"
                                        AllowSorting="True" OnSorting="SortCommand" Width="876px"
                                        EmptyDataText="No Record Found!">

                                        <Columns>
                                            <asp:BoundField />
                                        </Columns>

                                    </asp:GridView>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btn_Find_2" EventName="Click" />

                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>


        </div>
    </div>

</div>

<asp:Button ID="Button2" runat="server" Text="Select" Visible="false" />
<asp:TextBox ID="txt_get_by" runat="server" Visible="False"></asp:TextBox>
<asp:TextBox ID="txt_getTotal_Cols" runat="server"
    Visible="False" Width="16px"></asp:TextBox>
<asp:TextBox ID="txtCOL_1" runat="server" Visible="False"
    Width="16px"></asp:TextBox>
<asp:TextBox ID="txtCOL_2" runat="server" Visible="False"
    Width="16px"></asp:TextBox>
<asp:TextBox ID="txtCOL_3" runat="server" Visible="False"
    Width="16px"></asp:TextBox>
<asp:TextBox ID="txtCOL_4" runat="server" Visible="False"
    Width="16px"></asp:TextBox>
<asp:TextBox ID="txtCOL_1_hide" runat="server" Visible="False"
    Width="16px"></asp:TextBox>
<asp:TextBox ID="txtCOL_2_hide" runat="server" Visible="False"
    Width="16px"></asp:TextBox>


<asp:TextBox ID="txtCOL_3_hide" runat="server" Visible="False"
    Width="16px"></asp:TextBox>
<asp:TextBox ID="txtCOL_4_hide" runat="server" Visible="False"
    Width="16px"></asp:TextBox>

<asp:CheckBox ID="chkISDTTB" runat="server" Visible="False" />
<asp:TextBox ID="txtCOL_5_hide" runat="server" Visible="False"
    Width="16px"></asp:TextBox>

<asp:TextBox ID="txtCOL_6_hide" runat="server" Visible="False"
    Width="16px"></asp:TextBox>

<asp:TextBox ID="txtCOL_5" runat="server" Visible="False"
    Width="16px"></asp:TextBox>

<br />

<asp:TextBox ID="txtCOL_6" runat="server" Visible="False"
    Width="16px"></asp:TextBox>

