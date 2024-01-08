<%@ Control Language="VB" AutoEventWireup="false" CodeFile="cmdBar.ascx.vb" Inherits="cmdBar1" %>


<script type="text/javascript">
    function ClientSideClick(myButton) {
        // Client side validation
        if (typeof (Page_ClientValidate) == 'function') {
            if (Page_ClientValidate() == false)
            { return false; }
        }

        //make sure the button is not of type "submit" but "button"
        if (myButton.getAttribute('type') == 'button') {
            // diable the button
            myButton.disabled = true;
            myButton.className = "btn-inactive";
            //  myButton.value = "processing...";              
        }
        return true;
    }
</script>



<div class="row" align="right">
    <div class="col-md-12">

        <div class="btn-toolbar" role="toolbar" aria-label="Toolbar with button groups">
            <div class="btn-group" role="group" aria-label="First group">
                <asp:Button ID="btnSave" runat="server" AccessKey="s"
                    CssClass="btn btn-success" OnClientClick="ClientSideClick(this)" TabIndex="100" Text="Save"
                    ToolTip="Save The Transaction ( ALT + s )" UseSubmitBehavior="False" />

            </div>
            <div class="btn-group" role="group" aria-label="second group">

                <asp:Button ID="btnNEW" runat="server" Text="New" CssClass="btn btn-primary"
                    TabIndex="101" ToolTip="Refresh The Form ( ALT + n )" AccessKey="n" />

            </div>
            <div class="btn-group" role="group" aria-label="second group">
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-info"
                    TabIndex="102" ToolTip="Report Printing" AccessKey="p" />
            </div>
            <div class="btn-group" role="group" aria-label="second group">

                <asp:Button ID="btnLevel1" runat="server" Text="Commit" CssClass="btn btn-danger"
                    TabIndex="103" ToolTip="Report Printing" />

            </div>
            <div class="btn-group" role="group" aria-label="second group">


                <asp:Button ID="btnLevel2" runat="server" Text="Auth.L2" CssClass="btn btn-warning"
                    TabIndex="104" />
            </div>
            <div class="btn-group" role="group" aria-label="second group">
                <asp:Button ID="btnPDF" runat="server" Text="PDF" CssClass="btn btn-danger"
                    TabIndex="105" />
            </div>
            <div class="btn-group" role="group" aria-label="second group">
                <asp:Button ID="btnExcel" runat="server" Text="EXCEL" CssClass="btn btn-success"
                    TabIndex="106" />
            </div>
            <div class="btn-group" role="group" aria-label="second group">


                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                    TabIndex="104" ToolTip="Cancel The Transaction" />
            </div>
        </div>

    </div>
</div>
