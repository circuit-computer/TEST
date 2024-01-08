Imports System
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Data
Imports System.Data.DataRow
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Data.SqlTypes
Imports CrystalDecisions.CrystalReports.Engine



Imports CrystalDecisions.CrystalReports.Engine.ReportDocument
Imports CrystalDecisions.ReportSource


Imports System.Configuration
Imports CrystalDecisions.Shared


Public Enum enmQRYCols
    spName = 0
    RpNmae = 1
    dttbName = 2
    No_of_para = 3
    fmCriteria1 = 4
    fmUser = 5
    CCID_PrintedBY = 6
    frRptTitle = 7
    PDF_1_EXLS_2 = 8

    val_1 = 9
    val_2 = 10
    val_3 = 11

End Enum


Partial Class Site_frmrpt_ReportViewer
    Inherits System.Web.UI.Page


    Private Sub get_DTTB(ByRef objMN As modMain, ByRef obj_DTST As DataSet, ByVal spName As String, _
                         ByVal RpNmae As String, ByVal dttbName As String, _
                         ByVal Nopara As Integer)

        Try
            Dim objPara(Nopara) As clsPara
            Dim objectpara As New Object
            objectpara = Session("objPara")
            Dim I As Integer = 0
            For i = 0 To Nopara
                objPara(i) = objectpara(i)
            Next
            '--- NEW-LINE TO AVOID MULTILINES
            obj_DTST.Tables.Clear()

            obj_DTST = objMN.DTTB_Fill_GenericRpt(spName, dttbName, Session("OBJDTST"), objPara)

        Catch ex As Exception
            Response.Write(ex.Message)
            Exit Sub
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim str_spName As String = Nothing
        Dim str_RpNmae As String = Nothing
        Dim str_dttbName As String = Nothing
        Dim n_Nopara As Integer = 0
        Dim str_fmCriteria1 As String = ""
        Dim str_fmUser As String = ""
        Dim strWHR As String = ""
        Dim nCCID_PrintedBy As Integer = 0


        '------------------------------------
        Dim str_fmComp As String = ""
        Dim str_fmCompPh As String = ""
        Dim str_fmCompAdd As String = ""
        Dim str_fmCompemail As String = ""
        Dim str_frRptTitle As String = ""
        Dim PDF_1_EXLS_2 As Integer = 0

        Dim val_1 As String = ""
        Dim val_2 As String = ""
        Dim val_3 As String = ""
        '------------------------------------

        Dim objRpt As New ReportDocument()
        Dim obj_DTST As New DataSet

        Try

            Dim str_QRY_VALS As String = ""
            str_QRY_VALS = Request.QueryString("ID")

            Dim ARR_QRY As String() = str_QRY_VALS.Split("?"c)

            str_spName = ARR_QRY(enmQRYCols.spName)
            str_RpNmae = ARR_QRY(enmQRYCols.RpNmae)
            str_dttbName = ARR_QRY(enmQRYCols.dttbName)
            n_Nopara = ARR_QRY(enmQRYCols.No_of_para)
            str_fmCriteria1 = ARR_QRY(enmQRYCols.fmCriteria1)
            str_fmUser = ARR_QRY(enmQRYCols.fmUser)
            nCCID_PrintedBy = ARR_QRY(enmQRYCols.CCID_PrintedBY)
            str_frRptTitle = ARR_QRY(enmQRYCols.frRptTitle)
            PDF_1_EXLS_2 = ARR_QRY(enmQRYCols.PDF_1_EXLS_2)

            If ARR_QRY.Length > 9 Then
                val_1 = ARR_QRY(enmQRYCols.val_1)
                val_2 = ARR_QRY(enmQRYCols.val_2)
                val_3 = ARR_QRY(enmQRYCols.val_3)

            End If


            Dim objMN As New modMain
            strWHR = " CCID = " & nCCID_PrintedBy & " "

            objMN.get_CCID_Report_Values(strWHR, Session("gobjCCID"), str_fmComp, str_fmCompPh, str_fmCompAdd, str_fmCompemail)


            get_DTTB(objMN, obj_DTST, str_spName, str_RpNmae, str_dttbName, n_Nopara)




            objRpt.Load(Server.MapPath(str_RpNmae))
            '*****************************************************************
            Dim frmFldS As FormulaFieldDefinitions
            frmFldS = objRpt.DataDefinition.FormulaFields()


            frmFldS.Item("fmComp").Text = " '" & str_fmComp & "' "
            frmFldS.Item("fmCompPh").Text = " '" & str_fmCompPh & "' "
            frmFldS.Item("fmCompAdd").Text = " '" & str_fmCompAdd & "' "
            frmFldS.Item("fmCompemail").Text = " '" & str_fmCompemail & "' "

            If Len(Trim(str_frRptTitle)) <> 0 Then
                frmFldS.Item("fmRptTitle").Text = " '" & str_frRptTitle & "' "
            End If
            If Len(Trim(str_fmCriteria1)) <> 0 Then
                frmFldS.Item("fmCriteria1").Text = " '" & str_fmCriteria1 & "' "
            End If

            If Len(Trim(val_1)) <> 0 Then
                frmFldS.Item("fm_val_1").Text = " '" & val_1 & "' "
            End If
            If Len(Trim(val_2)) <> 0 Then
                frmFldS.Item("fm_val_2").Text = " '" & val_2 & "' "
            End If
            If Len(Trim(val_3)) <> 0 Then
                frmFldS.Item("fm_val_3").Text = " '" & val_3 & "' "
            End If

            Dim strRptRef As String = ""
            getRPT_REF(str_RpNmae, strRptRef)

            frmFldS.Item("fmRptRef").Text = " '" & strRptRef & "' "

            frmFldS.Item("fmUser").Text = "'" & str_fmUser & "'"

            '*****************************************************************
            '*****************************************************************



            '' This is the Crystal Report file created at Design Time
            objRpt.SetDataSource(obj_DTST)
            ' Set the SetDataSource property of the Report to the Dataset
            CrystalRptVwr.ReportSource = objRpt
            'Set the Crystal Report Viewer's property to the oRpt Report object that we created


            Dim oStream As New MemoryStream
            If PDF_1_EXLS_2 = 1 Then
                oStream = objRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/pdf"
            End If
            If PDF_1_EXLS_2 = 2 Then
                oStream = objRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel)
                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/vnd.ms-excel"
            End If
            If PDF_1_EXLS_2 = 3 Then
                oStream = objRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows)
                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/msword"
            End If

            '' ''objRpt.PrintToPrinter(1, False, 1, 1)
            Try
                Response.BinaryWrite(oStream.ToArray())
                Response.End()
            Catch err As Exception
                Response.Write("< BR >")
                Response.Write(err.Message.ToString)
            End Try
            'crystalReport.Export("Report.xls", CrystalReportViewer1)
            Session.Remove("OBJDTST")
            Session.Remove("objPara")
          
        Catch ex As Exception
            'ERR_MSG(ex.Message)
            'Throw New Exception(ex.Message)
            Response.Write(ex.Message)
            Exit Sub
        Finally
            objRpt.Dispose()
            objRpt.Close()
            obj_DTST.clear()
        End Try

    End Sub


    Private Sub getRPT_REF(ByVal rptName As String, ByRef strREF As String)
        Dim x As Integer = 0
        Try
            For i = 1 To rptName.Length - 1
                If rptName.Substring(i, 1) = "/" Then
                    x = i
                End If
            Next
            strREF = rptName.Substring(x + 1)
            strREF = Replace(strREF, ".rpt", "")
        Catch ex As Exception
            Response.Write("AT REPORT LOAD LEVEL .. " & ex.Message)
            Exit Sub
        End Try

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        'Try
        '    objRpt.Dispose()
        '    objRpt.Close()
        'Catch ex As Exception
        '    Response.Write("AT Dispose LEVEL " & ex.Message)
        '    Exit Sub
        'End Try


    End Sub
End Class
