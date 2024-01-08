Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Data.SqlTypes
Partial Class Site_frm_Company_ResetPassword
    Inherits System.Web.UI.Page
    'objMod.strSql = "SELECT Company_GID, Company_Name, Company_Email, Company_ContactNo, Representative_Name, M_Date  From tbl_Company_Registration where ( Login_Name='" & Trim(txt_Login_ID.Text) & "' AND Login_Password='" & Trim(txtPassword.Text) & "' )  "
    Public isPassword As Boolean
    

    

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

    Private Function validateForm() As Boolean

        Try



            If Len(Trim(txt_Password.Text)) = 0 Then

                ERR_MSG("Please enter New Password to save this record")
                txt_Password.Focus()
                Exit Function
            End If
            If Len(Trim(txt_OldPassword.Text)) = 0 Then

                ERR_MSG("Please enter  Password to save this record")
                txt_OldPassword.Focus()
                Exit Function
            End If
            If Len(Trim(txt_RePassWord.Text)) = 0 Then

                ERR_MSG("Please enter  RePassWord to save this record")
                txt_RePassWord.Focus()
                Exit Function
            End If

            If txt_Password.Text <> txt_RePassWord.Text Then
                ERR_MSG("System is unable to change password. Please enter valid password. ...")
                txt_Password.Text = ""
                txt_RePassWord.Text = ""
                txt_Password.Focus()
                Exit Function
            End If


            validateForm = True






        Catch ex As Exception
            ERR_MSG("")

        End Try

    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ERR_MSG("")
            If Not IsPostBack Then

            End If
            SET_HDR_LBL("CHANGE PASSWORD ")
            'getdate()
            '*************************************************
            cmdBar1.isAuth_L1 = False
            cmdBar1.isAuth_L2 = False

            cmdBar1.isNew = True
            cmdBar1.isPrint = False
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

    Private Sub chkPassword(ByVal strLogInName As String, ByRef isPassword As Boolean)
        Dim strSql As String = ""
        Dim objMod As New modMain
        Dim loReader As SqlClient.SqlDataReader = Nothing
        Dim loComm As New SqlClient.SqlCommand
        loComm.Connection = objMod.gCon
        strSql = " select Login_Password from   tbl_Company_Registration  where Login_Password = '" + strLogInName + "'  "

        'txtCustid_NW.Text = strCustID
        'Dim chkAccountName As Boolean
        Try
            objMod.setCon(True)

            loComm.CommandType = CommandType.Text
            loComm.CommandText = strSql
            loReader = loComm.ExecuteReader
            If loReader.Read Then
                isPassword = True
            Else
                isPassword = False
            End If


        Catch ex As Exception
            'ERR_MSG(ex.Message)
            Exit Sub
            'gstrMSG = vbNewLine + ex.Message
            'MsgBox(gMSG_Exception + gstrMSG, MsgBoxStyle.Exclamation, setMSG_title())
        Finally
            loReader.Close()
            loComm.Dispose()

            objMod.setCon(False)

        End Try


    End Sub

    Protected Sub cmdBar1_mSaveEvent() Handles cmdBar1.mSaveEvent
        Try
            ERR_MSG("")
            If Not validateForm() Then
                Exit Sub
            End If
            chkPassword(txt_OldPassword.Text, isPassword)
            If isPassword = True Then
                mSaveEvent()

            Else
                ERR_MSG("Please enter  Valid User Name & PassWord to change Password")
                txt_OldPassword.Text = ""
                txt_Password.Text = ""
                txt_RePassWord.Text = ""
            End If
        Catch ex As Exception
            ERR_MSG(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub mSaveEvent()




        Dim strSql As String = Nothing
        Dim objMod As New modMain

        Dim strCompanyID_Gid As String = ""
        

        '*****************************************************
        Dim dtTb_Company As New DataTable
        dtTb_Company = Session("gobjCOMPANY")
        
        strCompanyID_Gid = dtTb_Company.Rows(0)("Company_GID").ToString
        

        strSql = "UPDATE tbl_Company_Registration set Login_Password = '" & GetDPassword(txt_Password.Text) & "' where Login_Name = '" & txt_Login_ID.Text & "' "

        Dim strERR As String = ""
        objMod.Execute_nonSQL(strSql, strERR)

        If Len(Trim(strERR)) = 0 Then
            ERR_MSG("Password changed successfully .......", False)
            
            Response.Redirect("frm_ComplainLogin.aspx")
        Else
            ERR_MSG(strERR)
        End If
        'getdate()
    End Sub

    Public Function GetDPassword(ByVal strPassword As String) As String
        GetDPassword = strPassword
        Return GetDPassword


    End Function


End Class
