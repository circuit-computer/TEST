Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Data.SqlTypes
Partial Class Site_frm_Pending_List
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ERR_MSG("")


            If Not IsPostBack Then

                SET_HDR_LBL("Pending List")
                initform()
            End If

            cmdBar1.isPrint = False
            cmdBar1.isAuth_L1 = False
            cmdBar1.isAuth_L2 = False
            cmdBar1.isNew = False
            cmdBar1.isPDF = True
            cmdBar1.isEXCEL = True
            cmdBar1.isSave = False
            'LoadDataGridView()

        Catch ex As Exception

            ERR_MSG(ex.Message)
            Exit Sub

        End Try
    End Sub

    Private Sub initform()
        Try
            txtDate.Text = ""
        Catch ex As Exception
            ERR_MSG(ex.Message)
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

            Dim str_frRptTitle As String = "Pending Complains"
            Dim nSelectedRpt As Integer = 0

            'Dim status As Integer = cmbQueryStatus.SelectedValue

            Dim o_txtDate As Date

            objMN.getMMDDYYYY(txtDate.Text, o_txtDate)

            n_Nopara = 0
            Dim objPara(n_Nopara) As clsPara
            objMN.setPara(objPara(0), "Submit_Date", o_txtDate, "", clsPara.ColType.dt)

            Session("objPara") = objPara
            str_spName = "[stp_RPT_Pending_List]"

            str_RpName = "rpt_Pending_List.rpt"
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

    Protected Sub NewWindow(ByVal url As String)


        'Dim alertScript As String = "<script language='javascript'>detailedresults=window.open('" & url & "',toolbar=0,menubar=0,resizable=1)</script>"

        'Page.ClientScript.RegisterStartupScript(GetType(Page), "PopupScript", alertScript)
        Response.Write(String.Format("<script>window.open('" & url & "','_blank','top=10,left=200,height=800px,width=1200,addressbar=yes,toolbar=no,directories=no,status=yes,scrollbars=yes,menubar=no,resizable=yes'); </script>"))

    End Sub

    Protected Sub cmdBar1_mEXCEL_Event() Handles cmdBar1.mEXCEL_Event
        Try
            validateForm()
            init_Report(2)

        Catch ex As Exception
            ERR_MSG(ex.Message)
            Exit Sub
        Finally
        End Try
    End Sub

    Protected Sub cmdBar1_mPDF_Event() Handles cmdBar1.mPDF_Event
        Try
            validateForm()
            init_Report(1)

        Catch ex As Exception
            ERR_MSG(ex.Message)
            Exit Sub
        Finally
        End Try
    End Sub

    Private Function validateForm() As Boolean
        validateForm = False
        Try

            If Len(Trim(txtDate.Text)) = 0 Then


                ERR_MSG("Please enter Date to save this record ...")
                txtDate.Focus()
                Exit Function

            End If
           
            validateForm = True

        Catch ex As Exception
            ERR_MSG(ex.Message)

        End Try

    End Function




End Class
