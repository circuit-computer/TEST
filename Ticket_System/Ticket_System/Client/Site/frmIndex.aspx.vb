Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.DataVisualization.Charting
Imports System.Drawing

Partial Class Site_frmIndex
    Inherits System.Web.UI.Page

   
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        Try


            MyBase.OnPreRender(e)
            Dim strDisAbleBackButton As String
            strDisAbleBackButton = "<SCRIPT language=javascript>" & vbLf
            strDisAbleBackButton += "window.history.forward(1);" & vbLf
            strDisAbleBackButton += vbLf & "</SCRIPT>"
            ClientScript.RegisterClientScriptBlock(Me.Page.[GetType](), "wee", strDisAbleBackButton)
        Catch ex As Exception
            '  ERR_MSG(ex.Message, )
            Exit Sub
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
    End Sub
    Private Sub SET_HDR_LBL(ByVal strText As String)
        Try
            Dim mpLabel As Label
            Dim mpLabel_tree As Label
            mpLabel = CType(Master.FindControl("lblTitle"), Label)
            If Len(Trim(strText)) = 0 Then
                mpLabel.Visible = False
                Exit Sub
            End If
            mpLabel.Text = strText
            mpLabel.Visible = True


            mpLabel_tree = CType(Master.FindControl("lbl_show_tree_view"), Label)
            mpLabel_tree.Text = "hide tree"
        Catch ex As Exception
            '  ERR_MSG(ex.Message)
        End Try

    End Sub
    Private Sub SET_HDR_LBL_Hide(ByVal isHide As Boolean)
        Try
            Dim mpLabel As Label
            mpLabel = CType(Master.FindControl("lblTitle"), Label)

            mpLabel.Visible = Not (isHide)

        Catch ex As Exception

        End Try

    End Sub
  


   
End Class
