<%@ Control Language="VB" AutoEventWireup="false" CodeFile="msg_Box.ascx.vb" Inherits="msg_Box" %>

<style>
    #pnlItem {
        border-collapse: separate;
        border: solid #DBDBDB 2px;
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


    .button {
        background-color: #4CAF50; /* Green */
        border: none;
        color: white;
        letter-spacing: 1px;
        padding: 5px 22px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 13px;
        margin: 4px 5px;
        -webkit-transition-duration: 0.2s; /* Safari */
        transition-duration: 0.2s;
        cursor: pointer;
        width: 100px;
    }

    .buttonXl {
        background-color: white;
        color: black;
        border: 2px solid #4CAF50;
    }

        .buttonXl:hover {
            background-color: #4CAF50;
            color: white;
        }

    .buttonprint {
        background-color: white;
        color: black;
        border: 2px solid #823737;
    }

        .buttonprint:hover {
            background-color: #823737;
            color: white;
        }

    .imgtbn {
        margin-left: 10px;
    }

</style>

<div class="panel-group">
    <div class="panel panel-warning">
        <div class="panel-heading">

            <div class="row">
                <div class="col-md-12">


                    <asp:Image ID="imglogo" runat="server" Height="35px" ImageUrl="~/Site/images/PngFile.png" CssClass="imgtbn" />
                    &nbsp;<span style="margin: 0px 10px;" >DEMO EXCHANGE</span>

                    <asp:ImageButton ID="btnClose" runat="server" Text="X" Font-Bold="true"
                        Style="float: right; margin-right: 15px; float: right; margin-top: 0;" ImageUrl="~/Site/images/close.png" AccessKey="w" ToolTip="Close Window ( Alt + W)" />

                </div>
            </div>

        </div>
        <div class="panel-body">
            <div style="height: 150px; width: 500px;" id="pnlItem">
                <br />
                <div class="row">

                    <div class="col-md-12">
                        <center>
                     
                                <asp:Image ID="imgmsg" runat="server" Height="35px" ImageUrl="~/Site/images/like.png" Visible="false" />
                                <asp:Image ID="imgerr" runat="server" Height="35px" ImageUrl="~/Site/images/stop.png" Visible="false" />
                            
                        </center>

                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lbl_msg" runat="server" Font-Names="Calibri"></asp:Label>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                         <center>
                        <asp:Button ID="btn_YES" runat="server" Text="YES" Font-Bold="True" CssClass="button buttonXl" Visible="False" Width="100px" AccessKey="y" />
                        &nbsp;<asp:Button ID="btn_NO" runat="server" Text="NO" Font-Bold="True" CssClass="button buttonprint" Visible="False" Width="100px" AccessKey="n" />
                        &nbsp;<asp:Button ID="btn_OK" runat="server" Text="OK" Font-Bold="True" CssClass="button buttonXl" Width="100px" AccessKey="o" />
                        </center>
                    </div>
                </div>
              
                <br />
                <div class="row">
                    <div class="col-md-12">


                        <asp:Label ID="Label1" runat="server" Font-Size="X-Small" ForeColor="Silver">© Circuit Computer  </asp:Label>
                        <asp:Label ID="lbldatetime" runat="server" Font-Size="X-Small" ForeColor="Silver"></asp:Label>
                        <asp:TextBox ID="txtSenderRef" runat="server" Visible="False"></asp:TextBox>

                    </div>
                </div>
            </div>

        </div>
    </div>

</div>



