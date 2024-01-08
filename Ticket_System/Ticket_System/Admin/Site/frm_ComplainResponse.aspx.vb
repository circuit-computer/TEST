Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Data.SqlTypes
Partial Class Site_frm_ComplainResponse
    Inherits System.Web.UI.Page

    Private Enum grdCol
        btnView = 0
        tickectId = 1
        querySubmitDate = 2
        company_Name = 3

        Rep_Name = 4
        issueDetail1 = 5
        queryType = 6
        Company_GID = 7
        nTotalCols = 8


    End Enum


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ERR_MSG("")


            If Not IsPostBack Then

                SET_HDR_LBL("Complain Response")

                initform()
                set_Grid()


            End If



            cmdBar1.isAuth_L1 = False
            cmdBar1.isAuth_L2 = False
            cmdBar1.isPrint = False
            cmdBar1.isEXCEL = False
            'LoadDataGridView()

        Catch ex As Exception

            ERR_MSG(ex.Message)
            Exit Sub

        End Try
    End Sub

    Protected Sub cmdBar1_mPDF_Event() Handles cmdBar1.mPDF_Event

    End Sub

    Protected Sub cmdBar1_mPrintEvent() Handles cmdBar1.mPrintEvent
        Try

            init_Report(1)

        Catch ex As Exception
            ERR_MSG(ex.Message)
            Exit Sub
        Finally
        End Try
    End Sub


    Protected Sub cmdBar1_mSaveEvent() Handles cmdBar1.mSaveEvent
        If Not (validateForm()) Then
            Exit Sub
        End If

        mSAVE()
    End Sub

    Protected Sub cmdBar1_mNewEvent() Handles cmdBar1.mNewEvent
        Try
            ERR_MSG("")
            initform()
            set_Grid()

        Catch ex As Exception
            ERR_MSG(ex.Message)
            Exit Sub
        End Try
    End Sub

    Protected Sub cmdBar1_mEXCEL_Event() Handles cmdBar1.mEXCEL_Event
        Try

            init_Report(2)

        Catch ex As Exception
            ERR_MSG(ex.Message)
            Exit Sub
        Finally
        End Try
    End Sub


    Private Sub init_Report(ByVal PDF_1_EXLS_2 As Integer)

        Try
            Dim gobj_ALFA_dtstsys As New dtst_Tickect

            Dim Qstring As String = Nothing
            Dim str_spName As String = Nothing
            Dim str_RpName As String = Nothing
            Dim str_dttbName As String = Nothing

            Dim n_Nopara As Integer = 0

            Dim objMN As New modMain



            '-----------------------------------------------
            Dim nCCID As Integer = 0
            Dim strCCNAMe As String = " - CONSOLIDATED "

            Dim strCR_1 As String = ""


            Dim nUserID As Integer = 0
            Dim strUSERNAME As String = Nothing
            Dim str_CCName As String = Nothing
            objMN.getUSER_INFO_Name(Session("gobjUSER"), nUserID, nCCID, strUSERNAME, str_CCName)
            Dim strUSER As String = strUSERNAME & " ,  " & str_CCName
            Dim nCCID_PrintedBy As Integer = nCCID

            Dim str_frRptTitle As String = "USER COMPLAIN REPORT"
            Dim nSelectedRpt As Integer = 0


            n_Nopara = 0
            Dim objPara(n_Nopara) As clsPara
            objMN.setPara(objPara(0), "tickectId", 0, "", clsPara.ColType.Int)

            Session("objPara") = objPara
            str_spName = "[stp_RPT_tbl_Ticket]"

            str_RpName = "rptSys_Tickect.rpt"
            Session("objDTST") = gobj_ALFA_dtstsys
            str_dttbName = "dttb_Ticket"



            Qstring = str_spName & "?" + str_RpName & "?" & str_dttbName & "?" & n_Nopara & "?" & strCR_1 & "?" & strUSER & "?" & nCCID_PrintedBy & "?" & str_frRptTitle & "?" & PDF_1_EXLS_2


            If PDF_1_EXLS_2 = 2 Then
                Response.Redirect("frmrpt/ReportViewer.aspx?ID=" + Qstring)
            Else
                Dim strPage As String = "frmrpt/ReportViewer.aspx?ID=" + Qstring
                NewWindow(strPage)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub mSAVE()

        If Not (validateForm()) Then
            Exit Sub
        End If

        Dim objMod As New modMain
        Dim o_firstResponseDate As Date
        Dim o_complitionDate As Date

        objMod.getMMDDYYYY(txtFirstResDate.Text, o_firstResponseDate)
        objMod.getMMDDYYYY(txtComDate.Text, o_complitionDate)

        Dim n_ComplainStatus As Integer = 0

        n_ComplainStatus = cmbQueryStatus.SelectedItem.Value

        Dim cmd As SqlCommand = New SqlCommand
        Dim cmdLog As SqlCommand = New SqlCommand

        cmdLog.CommandText = "stp_tbl_Ticket_Log"
        cmdLog.CommandType = CommandType.StoredProcedure
        cmdLog.Connection = objMod.gCon

        cmd.CommandText = "stp_TblTicket_Update"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = objMod.gCon


        Try
            objMod.setCon(True)
            cmd.Parameters.Add(New SqlParameter("@tickectId", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 20, 0, "", DataRowVersion.Proposed, CLng(txtTickectNo.Text)))
            cmd.Parameters.Add(New SqlParameter("@firstResponseDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, o_firstResponseDate))
            cmd.Parameters.Add(New SqlParameter("@complitionDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, o_complitionDate))

            cmd.Parameters.Add(New SqlParameter("@statusId", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, n_ComplainStatus))

            cmd.Parameters.Add(New SqlParameter("@fixingDetail", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, txtFixingDetail.Text))
            cmd.Parameters.Add(New SqlParameter("@totalDays", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, txtTotalDays.Text))
            cmd.Parameters.Add(New SqlParameter("@totalHours", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, txtTotalHours.Text))

            cmdLog.Parameters.Add(New SqlParameter("@tickectId", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 20, 0, "", DataRowVersion.Proposed, CLng(txtTickectNo.Text)))
            cmdLog.Parameters.Add(New SqlParameter("@firstResponseDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, o_firstResponseDate))
            cmdLog.Parameters.Add(New SqlParameter("@complitionDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, o_complitionDate))

            cmdLog.Parameters.Add(New SqlParameter("@statusId", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, n_ComplainStatus))

            cmdLog.Parameters.Add(New SqlParameter("@fixingDetail", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, txtFixingDetail.Text))
            cmdLog.Parameters.Add(New SqlParameter("@totalDays", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, txtTotalDays.Text))
            cmdLog.Parameters.Add(New SqlParameter("@totalHours", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, txtTotalHours.Text))

            If cmd.ExecuteNonQuery() Then
                ERR_MSG("Record Successfully Update", False)

                initform()
            End If

            If cmdLog.ExecuteNonQuery() Then

            End If

        Catch ex As Exception
            cmd.Dispose()
            cmdLog.Dispose()
            objMod.setCon(False)
            Exit Sub
        End Try



    End Sub


    Private Sub init_GridTitle()

        For Each row As GridViewRow In Me.Gv.Rows

            row.Cells(grdCol.Company_GID).Visible = False

            '--------------------------------------------------------------------

            row.Cells(grdCol.btnView).HorizontalAlign = HorizontalAlign.Center
        Next

        Try
            Gv.HeaderRow.Cells(grdCol.btnView).Text = ""
            Gv.HeaderRow.Cells(grdCol.tickectId).Text = "Complain NO"
            Gv.HeaderRow.Cells(grdCol.company_Name).Text = "Company Name"
            Gv.HeaderRow.Cells(grdCol.Rep_Name).Text = "Representative Name"
            Gv.HeaderRow.Cells(grdCol.issueDetail1).Text = "Detail of Issue"
            Gv.HeaderRow.Cells(grdCol.querySubmitDate).Text = "Submit Date"
            Gv.HeaderRow.Cells(grdCol.queryType).Text = "Complain Type"

            Gv.HeaderRow.Cells(grdCol.Company_GID).Visible = False

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Sub set_Grid()

        Dim isDATA As Boolean = False
        Dim dtTB As New DataTable

        Try

            get_DTTB(dtTB)
            Gv.DataSource = dtTB
            Gv.DataBind()

            For Each row As GridViewRow In Me.Gv.Rows

                isDATA = True


            Next

            If isDATA Then
                init_GridTitle()

            End If

        Catch ex As Exception
            ERR_MSG(ex.Message)
        Finally
            dtTB.Dispose()
            dtTB = Nothing
        End Try

    End Sub

    Protected Sub Gv_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gv.RowCreated
        Try


            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lbtn As New LinkButton()

                lbtn.Text = "view"
                lbtn.CssClass = "btn btn-info"

                e.Row.Cells(0).Controls.Add(lbtn)
                AddHandler lbtn.Click, AddressOf SelectIL_Click
            End If
        Catch ex As Exception
            ERR_MSG(ex.Message)
        End Try
    End Sub

    Protected Sub SelectIL_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            Dim gvRow As GridViewRow = DirectCast(TryCast(sender, Control).Parent.Parent, GridViewRow)

            Dim index As Integer = gvRow.RowIndex
            initform()
            txtTickectNo.Text = Gv.Rows(index).Cells((grdCol.tickectId)).Text

        Catch ex As Exception
            ERR_MSG(ex.Message)
        Finally

        End Try

    End Sub

    Private Sub ERR_MSG(ByVal strErr As String, Optional ByVal isError As Boolean = True)


        Try
            If Len(Trim(strErr)) = 0 Then Exit Sub
            ctrl_MSG.str_MSG = strErr
            'mpLabel.ForeColor = Drawing.Color.Pink
            If isError Then
                ctrl_MSG.is_ERR = True
            End If
            If Not isError Then
                ctrl_MSG.is_MSG = True
            End If
            ctrl_MSG.str_DATE_TIME = Date.Now
            pnlMSG.Attributes.Add("class", "pnl_popup_msg")
            pnlMSG.Visible = True
            ctrl_MSG.showControl()

        Catch ex As Exception


        Finally

        End Try

    End Sub

    Protected Sub msg_Box_1_msg_Close_Event(strSENDERID As String) Handles ctrl_MSG.msg_Close_Event
        Try
            pnlMSG.Visible = False
        Catch ex As Exception
            ERR_MSG(ex.Message)
        End Try
    End Sub

    Private Sub initform()
        Try
            txtTickectNo.Text = ""
            txtFirstResDate.Text = ""
            txtComDate.Text = ""
            txtFixingDetail.Text = ""
            txtTotalDays.Text = ""
            txtTotalHours.Text = ""
            cmbQueryStatus.SelectedIndex = 0


        Catch ex As Exception
            ERR_MSG(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Function validateForm() As Boolean
        validateForm = False
        Try
            If Len(Trim(txtTickectNo.Text)) = 0 Then


                ERR_MSG("Please Select Complain from table ...")
                txtTickectNo.Focus()
                Exit Function

            End If

            If Len(Trim(txtFirstResDate.Text)) = 0 Then


                ERR_MSG("Please enter Response Date to save this record ...")
                txtFirstResDate.Focus()
                Exit Function

            End If
            If Len(Trim(txtComDate.Text)) = 0 Then


                ERR_MSG("Please enter Completion Date to save this record ...")
                txtComDate.Focus()
                Exit Function

            End If
            If Len(Trim(txtFixingDetail.Text)) = 0 Then


                ERR_MSG("Please enter Fixing Details to save this record ...")
                txtFixingDetail.Focus()
                Exit Function

            End If
            If Len(Trim(txtTotalDays.Text)) = 0 Then


                ERR_MSG("Please enter Total Days to save this record ...")
                txtTotalDays.Focus()
                Exit Function

            End If

            If Len(Trim(txtTotalHours.Text)) = 0 Then


                ERR_MSG("Please enter Total Hours to save this record ...")
                txtTotalHours.Focus()
                Exit Function

            End If

            If (Trim(cmbQueryStatus.SelectedItem.Text)) = modMain.gStr_Empty Or cmbQueryStatus.SelectedItem.Text = "Empty" Then
                ERR_MSG("Please select Complain Status to save this record ....")
                cmbQueryStatus.Focus()

                Exit Function
            End If

            validateForm = True

        Catch ex As Exception
            ERR_MSG(ex.Message)

        End Try

    End Function

    Private Sub SET_HDR_LBL(ByVal strText As String)
        Try
            Dim mpLabel As Label
            mpLabel = CType(Master.FindControl("lblTitle"), Label)
            If Len(Trim(strText)) = 0 Then
                mpLabel.Visible = False
                Exit Sub
            End If
            mpLabel.Text = strText
            mpLabel.Visible = True
        Catch ex As Exception
            ERR_MSG(ex.Message)
        End Try

    End Sub

    Private Sub get_DTTB(ByRef dtTB As DataTable)

        Dim objMN As New modMain
        Dim dtTb_data As New DataTable

        Try

            objMN.setCon(True)

            Dim cmd As SqlCommand = New SqlCommand
            cmd.CommandText = "dbo.[stp_get_Complain]"
            cmd.CommandType = CommandType.StoredProcedure

           objMN.strSql = "stp_get_Complain"

            dtTb_data = objMN.DTTB_Fill(objMN.strSql, objMN.gCon)

            Dim oRow As DataRow
            dtTB.Columns.Add("Ticket_Id", System.Type.GetType("System.String"))
            dtTB.Columns.Add("Submit_Date", System.Type.GetType("System.String"))
            dtTB.Columns.Add("company_Name", System.Type.GetType("System.String"))
            dtTB.Columns.Add("Rep_Name", System.Type.GetType("System.String"))
            dtTB.Columns.Add("Detail_1", System.Type.GetType("System.String"))
            dtTB.Columns.Add("Ticket_Type", System.Type.GetType("System.String"))
            dtTB.Columns.Add("Company_GID", System.Type.GetType("System.String"))


            Dim I As Integer = 0

            For I = 0 To dtTb_data.Rows.Count - 1
                oRow = dtTB.NewRow

                oRow("Ticket_Id") = dtTb_data.Rows(I).Item("Ticket_Id")
                oRow("Company_GID") = dtTb_data.Rows(I).Item("Company_GID")
                oRow("Submit_Date") = dtTb_data.Rows(I).Item("Submit_Date")
                oRow("company_Name") = dtTb_data.Rows(I).Item("company_Name")
                oRow("Rep_Name") = dtTb_data.Rows(I).Item("Rep_Name")
                oRow("Detail_1") = dtTb_data.Rows(I).Item("Detail_1")
                oRow("Ticket_Type") = dtTb_data.Rows(I).Item("Ticket_Type")

                dtTB.Rows.Add(oRow)
            Next

        Catch ex As Exception

            ERR_MSG(ex.Message)
        Finally

            objMN.gCon.Close()
            objMN = Nothing


        End Try


    End Sub

    Protected Sub NewWindow(ByVal url As String)


        'Dim alertScript As String = "<script language='javascript'>detailedresults=window.open('" & url & "',toolbar=0,menubar=0,resizable=1)</script>"

        'Page.ClientScript.RegisterStartupScript(GetType(Page), "PopupScript", alertScript)
        Response.Write(String.Format("<script>window.open('" & url & "','_blank','top=10,left=200,height=800px,width=1200,addressbar=yes,toolbar=no,directories=no,status=yes,scrollbars=yes,menubar=no,resizable=yes'); </script>"))

    End Sub
    
    'Protected Sub txtFirstResDate_TextChanged(sender As Object, e As EventArgs) Handles txtFirstResDate.TextChanged
    '    Dim startTime As DateTime = txtFirstResDate

    '    Dim endTime As DateTime = o_complitionDate

    '    Dim span As TimeSpan = endTime.Subtract(startTime)
    '    txtTotalHours.Text = span.Hours
    '    txtTotalDays.Text = span.Days

    'End Sub


    Protected Sub txtComDate_TextChanged(sender As Object, e As EventArgs) Handles txtComDate.TextChanged
        Dim o_firstResponseDate As Date
        Dim o_complitionDate As Date
        Dim objMN As New modMain
        objMN.getMMDDYYYY(txtFirstResDate.Text, o_firstResponseDate)
        objMN.getMMDDYYYY(txtComDate.Text, o_complitionDate)

        Dim startTime As DateTime = o_firstResponseDate

        Dim endTime As DateTime = o_complitionDate

        Dim span As TimeSpan = endTime.Subtract(startTime)
        txtTotalHours.Text = span.Days * 24

        If span.Days < 0 Then
            ERR_MSG("Please enter valid Completion Date", False)
        End If

        If span.Days >= 0 Then
            txtTotalDays.Text = span.Days
        End If



    End Sub
End Class
