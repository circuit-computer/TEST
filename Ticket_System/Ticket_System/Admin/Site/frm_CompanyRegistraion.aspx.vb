Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Data.SqlTypes
Partial Class Site_frm_CompanyRegistraion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ERR_MSG("")


            If Not IsPostBack Then

                SET_HDR_LBL("Company Registration Form")

                initform()


            End If



            cmdBar1.isAuth_L1 = False
            cmdBar1.isAuth_L2 = False
            cmdBar1.isEXCEL = False
            cmdBar1.isPrint = False
            'LoadDataGridView()

        Catch ex As Exception

            ERR_MSG(ex.Message)
            Exit Sub

        End Try
    End Sub

    Private Sub initform()
        Try
            txt_Company_Name.Text = ""
            txt_Company_Email.Text = ""
            txt_Company_Contact_No.Text = ""
            txt_Company_Rep_Name.Text = ""
            txt_Login_Name.Text = ""
            txt_Login_Password.Text = ""


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

    Protected Sub msg_Box_1_msg_Close_Event(strSENDERID As String) Handles ctrl_MSG.msg_Close_Event
        Try
            pnlMSG.Visible = False
        Catch ex As Exception
            ERR_MSG(ex.Message)
        End Try
    End Sub

    Private Function validateForm() As Boolean
        validateForm = False
        Try

            If Len(Trim(txt_Company_Name.Text)) = 0 Then


                ERR_MSG("Please enter Company Name to save this record ...")
                txt_Company_Name.Focus()
                Exit Function

            End If

            If Len(Trim(txt_Company_Email.Text)) = 0 Then


                ERR_MSG("Please enter Company Email to save this record ...")
                txt_Company_Email.Focus()
                Exit Function

            End If
            If Len(Trim(txt_Company_Contact_No.Text)) = 0 Then


                ERR_MSG("Please enter Company Contact Number to save this record ...")
                txt_Company_Contact_No.Focus()
                Exit Function

            End If
            If Len(Trim(txt_Company_Rep_Name.Text)) = 0 Then


                ERR_MSG("Please enter Company Representative Name to save this record ...")
                txt_Company_Rep_Name.Focus()
                Exit Function

            End If
            If Len(Trim(txt_Login_Name.Text)) = 0 Then


                ERR_MSG("Please enter Login Name to save this record ...")
                txt_Login_Name.Focus()
                Exit Function

            End If

            If Len(Trim(txt_Login_Password.Text)) = 0 Then


                ERR_MSG("Please enter Password to save this record ...")
                txt_Login_Password.Focus()
                Exit Function

            End If

            validateForm = True

        Catch ex As Exception
            ERR_MSG(ex.Message)

        End Try

    End Function

    Private Sub init_Report(ByVal PDF_1_EXLS_2 As Integer)

        Try
            Dim gobj_ALFA_dtstsys As New dtst_Companies

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

            Dim str_frRptTitle As String = "Company Listing"
            Dim nSelectedRpt As Integer = 0


            n_Nopara = 0
            Dim objPara(n_Nopara) As clsPara
            objMN.setPara(objPara(0), "tickectId", 0, "", clsPara.ColType.Int)

            Session("objPara") = objPara
            str_spName = "stp_RPT_Tbl_Company_Registration"

            str_RpName = "rpt_Comp_Reg_new.rpt"
            Session("objDTST") = gobj_ALFA_dtstsys
            str_dttbName = "dttb_Companies"



            Qstring = str_spName & "?" + str_RpName & "?" & str_dttbName & "?" & n_Nopara & "?" & strCR_1 & "?" & strUSER & "?" & nCCID_PrintedBy & "?" & str_frRptTitle & "?" & PDF_1_EXLS_2


            If PDF_1_EXLS_2 = 2 Then
                Response.Redirect("frmrpt/ReportViewer.aspx?ID=" + Qstring)
            Else
                Dim strPage As String = "../frmrpt/ReportViewer.aspx?ID=" + Qstring
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
        mSAVE_Company()

    End Sub
    Public Sub mSAVE_Company()

        If Not (validateForm()) Then
            Exit Sub
        End If

        Dim objMod As New modMain

        Dim cmd As SqlCommand = New SqlCommand
        cmd.CommandText = "dbo.[stp_TblCompany_Registration_Insert]"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = objMod.gCon

        Try
            objMod.setCon(True)
            cmd.Parameters.Add(New SqlParameter("@Company_Name", SqlDbType.NChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, txt_Company_Name.Text))
            cmd.Parameters.Add(New SqlParameter("@Company_Email", SqlDbType.NChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, txt_Company_Name.Text))
            cmd.Parameters.Add(New SqlParameter("@Company_ContactNo", SqlDbType.NChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, txt_Company_Contact_No.Text))

            cmd.Parameters.Add(New SqlParameter("@Representative_Name", SqlDbType.NChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, txt_Company_Rep_Name.Text))
            cmd.Parameters.Add(New SqlParameter("@Login_Name", SqlDbType.NChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, txt_Login_Name.Text))
            cmd.Parameters.Add(New SqlParameter("@Login_Password", SqlDbType.NChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, txt_Login_Password.Text))
            If cmd.ExecuteNonQuery() Then
                ERR_MSG("Record Successfully Update", False)
                initform()
            End If
        Catch ex As Exception
            cmd.Dispose()
            objMod.setCon(False)
            Exit Sub
        End Try



    End Sub
End Class
