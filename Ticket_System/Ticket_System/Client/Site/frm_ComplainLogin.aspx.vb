Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Data.SqlTypes
Partial Class Site_frm_ComplainLogin
    Inherits System.Web.UI.Page



    Private Function validateForm() As Boolean
        validateForm = False
        Try

            If Len(Trim(txt_Login_ID.Text)) = 0 Then


                ERR_MSG("Please enter Login ID to save this record ...")
                txt_Login_ID.Focus()
                Exit Function

            End If

            If Len(Trim(txtPassword.Text)) = 0 Then


                ERR_MSG("Please enter Password to save this record ...")
                txtPassword.Focus()
                Exit Function

            End If
            
            validateForm = True

        Catch ex As Exception
            ERR_MSG(ex.Message)

        End Try

    End Function

    Private Sub createNewSessionID()

        Try

            Dim Manager As New SessionState.SessionIDManager()
            Dim NewID As String = Manager.CreateSessionID(HttpContext.Current)
            Dim OldID As String = HttpContext.Current.Session.SessionID
            Dim IsAdded As Boolean = True
            Manager.SaveSessionID(HttpContext.Current, NewID, False, IsAdded)

        Catch ex As Exception



        End Try

    End Sub

    Private Sub ERR_MSG(ByVal strErr As String, Optional ByVal isError As Boolean = True)
         Try

            'Dim mpLabel As Label
            'mpLabel = CType(Me.FindControl("lblERR"), Label)
            If Len(Trim(strErr)) = 0 Then
                lblVER.Visible = False
                Exit Sub
            End If

            'mpLabel.ForeColor = Drawing.Color.Pink
            If Not isError Then
                lblVER.ForeColor = Drawing.Color.LightGreen
            End If

            lblVER.Text = strErr
            'mpLabel.Visible = True

        Catch ex As Exception
            'Dim mpLabel As Label
            'mpLabel = CType(Me.FindControl("lblERR"), Label)
            lblVER.Text = ex.Message
            'mpLabel.Text = ex.Message
            'mpLabel.Visible = True
        End Try
    End Sub

    Protected Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit.Click
        validateForm()

        Dim objMod As New modMain
        Dim dtTB As New DataTable()
        Try
            objMod.setCon(True)
            objMod.strSql = "SELECT Company_GID, Company_Name, Company_Email, Company_ContactNo, Representative_Name, M_Date  From tbl_Company_Registration where ( Login_Name='" & Trim(txt_Login_ID.Text) & "' AND Login_Password='" & Trim(txtPassword.Text) & "' )  "

            dtTB = objMod.DTTB_Fill(objMod.strSql)
            If dtTB.Rows.Count = 0 Then
                ERR_MSG("Invalid Login / Password", True)
                createNewSessionID()
                Exit Sub
            End If

            Session("gobjCOMPANY") = dtTB



            objMod.strSql = "SELECT top(1) * ,dbo.fun_GetCCID_Name(locid)  as CCNAME, dbo.fun_GetCCID_Short_Name(locid) as compnameshort From tblUser   "
            dtTB = objMod.DTTB_Fill(objMod.strSql)


            Session("gobjUSER") = dtTB


            dtTB.Dispose()
            dtTB = Nothing
            dtTB = New DataTable
            objMod.strSql = " select * from tluCompBranchInfo"
            dtTB = objMod.DTTB_Fill(objMod.strSql)
            Session("gobjCCID") = dtTB
          

            Session("IsLogin") = 1

            Response.Redirect("frm_Complain.aspx")



        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then
                createNewSessionID()
            End If
            If Not Session("IsLogin") = Nothing Then
                Server.Transfer("frmIndex.aspx")
            End If
            ' Login_Direct()
        Catch ex As Exception
        End Try
    End Sub


End Class
