Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Data.SqlTypes

Partial Class Site_frm_Complain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ERR_MSG("")



            If Not IsPostBack Then

                SET_HDR_LBL("Complain Form")

                initform()


            End If

            Dim objMN As New modMain
            Dim Comp_Representative_Name As String = ""

            objMN.getCompany_Representative_Name(Session("gobjCOMPANY"), Comp_Representative_Name)

            txtCompRepId.Text = Comp_Representative_Name
            cmdBar1.isAuth_L1 = False
            cmdBar1.isAuth_L2 = False
            cmdBar1.isPrint = False
            'LoadDataGridView()

        Catch ex As Exception

            ERR_MSG(ex.Message)
            Exit Sub

        End Try
    End Sub

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

    Protected Sub cmdBar1_mNewEvent() Handles cmdBar1.mNewEvent
        initform()
    End Sub

    
    Protected Sub cmdBar1_mSaveEvent() Handles cmdBar1.mSaveEvent
        If Not (validateForm()) Then
            Exit Sub
        End If
        mSAVE()
    End Sub
    Public Sub mSAVE()
        Dim objMod As New modMain
        Dim comp_GID As String = ""
        objMod.getCompany_GID(Session("gobjCOMPANY"), comp_GID)

        Dim cmd As SqlCommand = New SqlCommand
        cmd.CommandText = "dbo.[stp_TblTicket_Insert]"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = objMod.gCon

        Try
            objMod.setCon(True)
            cmd.Parameters.Add(New SqlParameter("@company_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, comp_GID))
            'cmd.Parameters.Add(New SqlParameter("@Representative_Id", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CInt(0)))
            cmd.Parameters.Add(New SqlParameter("@queryType", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, cmbQueryType.Text))
            cmd.Parameters.Add(New SqlParameter("@issueDetail1", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, txtIssueDetail1.Text))
            cmd.Parameters.Add(New SqlParameter("@issueDetail2", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, txtIssueDetail2.Text))
            cmd.Parameters.Add(New SqlParameter("@issueDetail3", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, txtIssueDetails3.Text))
            If cmd.ExecuteNonQuery() Then
                ERR_MSG("Record Save Successfully", False)
                initform()
            End If
        Catch ex As Exception
            cmd.Dispose()
            objMod.setCon(False)

            Exit Sub
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
                ctrl_MSG.is_ERR = False
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

            txtCompId.Text = ""
            txtIssueDetail1.Text = ""
            txtIssueDetail2.Text = ""
            txtIssueDetails3.Text = ""
            cmbQueryType.SelectedIndex = -1
            cmbReportedBy.SelectedIndex = -1


        Catch ex As Exception
            ERR_MSG(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Function validateForm() As Boolean
        validateForm = False
        Try



            'If Len(Trim(txtCompId.Text)) = 0 Then


            '    ERR_MSG("Please enter Company Representative to save this record ...")
            '    txtCompId.Focus()
            '    Exit Function

            'End If
            If cmbQueryType.Text = "Empty" Then


                ERR_MSG("Please select Complain Type to save this record ...")
                cmbQueryType.Focus()
                Exit Function

            End If
            If Len(Trim(txtIssueDetail1.Text)) = 0 Then


                ERR_MSG("Please enter Issue Detail to save this record ...")
                txtIssueDetail1.Focus()
                Exit Function

            End If



            'If (Trim(cmbQueryStatus.SelectedItem.Text)) = modMain.gStr_Empty Then
            '    ERR_MSG("Please select Query Status to save this record ....")
            '    cmbQueryStatus.Focus()

            '    Exit Function
            'End If

            validateForm = True

        Catch ex As Exception
            ERR_MSG(ex.Message)

        End Try

    End Function
End Class
