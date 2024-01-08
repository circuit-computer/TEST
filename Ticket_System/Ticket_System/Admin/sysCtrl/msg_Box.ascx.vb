Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing

Partial Class msg_Box
    Inherits System.Web.UI.UserControl
    Dim QueryStr As String
    Dim Strref As String

    Dim strSearch As String

    Private mID As String
    Private strSql As String
    'Private mVarFindByCol As Integer
    'Private mVarFindByCol_2 As Integer
    'Private mVarFindByCol_3 As Integer
    'Private mVarFindByCol_4 As Integer

    Public str_MSG As String
    Public str_DATE_TIME As String
    Public is_MSG As Boolean = False
    Public is_ERR As Boolean = False
    Public is_YES_NO As Boolean = False



    Public strGetBy As String = ""
    Dim _lnkID As Integer = 0
    Private mVar_isCCY As Boolean
    'Public Event SelectEvent(ByVal strID As String, ByVal strSENDERID As String)
    'Public Event SelectEvent_2(ByVal strID As String, ByVal strID2 As String, ByVal strSENDERID As String)
    'Public Event SelectEvent_3(ByVal strID As String, ByVal strID2 As String, ByVal strID3 As String, ByVal strSENDERID As String)
    'Public Event SelectEvent_4(ByVal strID As String, ByVal strID2 As String, ByVal strID3 As String, ByVal strID4 As String, ByVal strSENDERID As String)
    'Public Event SelectEvent_6(ByVal strID As String, ByVal strID2 As String, ByVal strID3 As String, ByVal strID4 As String, ByVal strID5 As String, ByVal strID6 As String, ByVal strSENDERID As String)

    Public Event msg_Close_Event(ByVal strSENDERID As String)
    Public Event msg_YES_NO(ByVal strSENDERID As String, ByRef is_YES As Boolean)





    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Not IsPostBack Then
        '    txtSenderRef.Text = strSENDERID
        'End If
        imgmsg.Visible = False
        imgerr.Visible = False
        btn_OK.Focus()
    End Sub



    Public Sub showControl()


        Try
            btn_OK.Visible = False
            btn_YES.Visible = False
            btn_NO.Visible = False

            If is_MSG = True Then
                lbl_msg.ForeColor = Color.Green
                btn_OK.Visible = True
                imgmsg.Visible = True
                imgerr.Visible = False
                'imgmsg.ImageUrl = "~/Site/images/like.png"
                'lbl_msg.ForeColor = Drawing.Color.Maroon
            End If
            If is_ERR = True Then
                btn_OK.Visible = True
                imgerr.Visible = True
                imgmsg.Visible = False
                'imgmsg.ImageUrl = "~/Site/images/stop.png"
                'lbl_msg.ForeColor = Drawing.Color.Navy
                lbl_msg.ForeColor = Color.Red
                str_MSG = str_MSG & vbNewLine & " Plz contact to sys. admin."
            End If

            If is_YES_NO = True Then
                btn_YES.Visible = True
                btn_NO.Visible = True
                imgmsg.ImageUrl = "~/Site/images/yesno.png"
                lbl_msg.ForeColor = Drawing.Color.Navy
            End If

            lbl_msg.Text = str_MSG
            lbldatetime.Text = str_DATE_TIME
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally


        End Try



    End Sub




    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        RaiseEvent msg_Close_Event(txtSenderRef.Text)
    End Sub




    Protected Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        RaiseEvent msg_Close_Event(txtSenderRef.Text)
    End Sub




    Protected Sub btn_YES_Click(sender As Object, e As EventArgs) Handles btn_YES.Click
        RaiseEvent msg_YES_NO(txtSenderRef.Text, True)
    End Sub

    Protected Sub btn_NO_Click(sender As Object, e As EventArgs) Handles btn_NO.Click
        RaiseEvent msg_YES_NO(txtSenderRef.Text, False)
    End Sub
End Class
