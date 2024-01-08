Imports System.Data.SqlClient
Imports System.Data
Partial Class WebUserControl
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

    Public strID, strID2, strID3, strID4, strID5, strID6 As String
    Dim strFields As String = "ID"
    Public dataTableObject As DataTable
    Public isDataTable As Boolean
    Public is_filter_hide As Boolean = False
    Public strSql3 As String = ""

    Public strSql_NEW As String = ""
    Public strSENDERID As String = ""
    Public str_Field_list As String = ""
    Public str_COL_1 As String = ""
    Public str_COL_2 As String = ""
    Public str_COL_3 As String = ""
    Public str_COL_4 As String = ""
    Public str_COL_5 As String = ""
    Public str_COL_6 As String = ""
    Public isDtTB As Boolean
    Public strWHERE As String


    Public str_Col_hide_1 As String = ""
    Public str_Col_hide_2 As String = ""
    Public str_Col_hide_3 As String = ""
    Public str_Col_hide_4 As String = ""
    Public str_Col_hide_5 As String = ""
    Public str_Col_hide_6 As String = ""




    Public gwSortby As String = "" '
    Public DetailCode, SubCode, SubCodeName As String
    Public DetailCode1, SubCode1, SubCodeName1 As String
    'Dim objquerystring As MainClass

    'Private connectionstr As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
    'Dim Con As New SqlConnection(connectionstr)

    Public strGetBy As String = ""
    Dim _lnkID As Integer = 0
    Private mVar_isCCY As Boolean
    Public Event SelectEvent(ByVal strID As String, ByVal strSENDERID As String)
    Public Event SelectEvent_2(ByVal strID As String, ByVal strID2 As String, ByVal strSENDERID As String)
    Public Event SelectEvent_3(ByVal strID As String, ByVal strID2 As String, ByVal strID3 As String, ByVal strSENDERID As String)
    Public Event SelectEvent_4(ByVal strID As String, ByVal strID2 As String, ByVal strID3 As String, ByVal strID4 As String, ByVal strSENDERID As String)
    Public Event SelectEvent_6(ByVal strID As String, ByVal strID2 As String, ByVal strID3 As String, ByVal strID4 As String, ByVal strID5 As String, ByVal strID6 As String, ByVal strSENDERID As String)
    Public Event CloseEvent(ByVal strSENDERID As String)
    Public Event SearchEvent(ByRef strSql As String, ByRef strSearchBy As String, ByVal strSENDERID As String)
    Public Event SearchEvent_By_Field(ByRef strSql As String, ByRef strSearchBy As String, ByVal strSENDERID As String, ByVal Field_NAME As String)


    Public Event SearchEvent_By_Field_WITH_TWO_PARAS(ByRef strSql As String, ByRef strSearchBy As String, ByVal strSENDERID As String, ByVal Field_NAME As String, ByRef SearchByTagTB As String)


    Public Event Hide_SearchEvent_By_Field()





    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Not IsPostBack Then
        '    txtSenderRef.Text = strSENDERID
        'End If
        SearchByTagTB.Focus()
    End Sub

    Private Sub SelRec()
        'txtSenderRef0.Text = newstr
        'If txt_get_by.Text = modMain.gStrSelectEvent Then
        '    Dim row As GridViewRow = dg.SelectedRow
        '    nID = _lnkID
        '    RaiseEvent SelectEvent(nID, txtSenderRef.Text)
        '    'If Len(Trim(nID2)) <> 0 Then
        '    RaiseEvent SelectEvent_2(nID, nID2, txtSenderRef.Text)
        '    'End If
        '    If Len(Trim(nID3)) <> 0 Then
        '        RaiseEvent SelectEvent_3(nID, nID2, nID3)
        '    End If
        '    If Len(Trim(nID4)) <> 0 Then
        '        RaiseEvent SelectEvent_4(nID, nID2, nID3, nID4)
        '    End If
        'End If

        'If txt_get_by.Text = modMain.gStrSelectEvent_getAccount Then
        '    RaiseEvent getAccountsEvent(DetailCode, SubCode, SubCodeName)
        'End If
    End Sub


    Public Sub showControl()

        Dim objMod As New modMain
        Try
         

            'lComm.Connection = objMod.gCon

            '**************************************************
            '**************************************************
            '--- initialize
            txtSenderRef.Text = strSENDERID
            txt_get_by.Text = strGetBy
            txtCOL_1.Text = str_COL_1
            txtCOL_2.Text = str_COL_2
            txtCOL_3.Text = str_COL_3
            txtCOL_4.Text = str_COL_4
            txtCOL_5.Text = str_COL_5
            txtCOL_6.Text = str_COL_6
            txtCOL_1_hide.Text = str_Col_hide_1
            txtCOL_2_hide.Text = str_Col_hide_2
            txtCOL_3_hide.Text = str_Col_hide_3
            txtCOL_4_hide.Text = str_Col_hide_4
            txtCOL_5_hide.Text = str_Col_hide_5
            txtCOL_6_hide.Text = str_Col_hide_6
            chkISDTTB.Checked = isDataTable
            '**************************************************
            '**************************************************
            init_CMB_Field(str_Field_list)

            Try
                If Len(Trim(strSql_NEW)) = 0 Then
                    dg.Controls.Clear()
                    Exit Sub
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
                Exit Sub
            End Try

            objMod.setCon(True)

            If Not chkISDTTB.Checked Then
                Dim Da As SqlClient.SqlDataAdapter = New SqlDataAdapter(strSql_NEW, objMod.gCon)
                Dim dt As New DataTable()
                Da.Fill(dt)
                dg.DataSource = dt
                dg.DataBind()

                If dt.Rows.Count = 0 Then
                    dg.Controls.Clear()
                End If
                ViewState("dataset") = dt

                set_Grid_Format()

            End If

            If chkISDTTB.Checked Then
                Dim dt As New DataTable()
                Dim dt2 As New DataTable()
                dt = Session("gobjCOA")
                Dim trows As DataRow() = dt.Select(strSql_NEW)
                dt2.Columns.Add("dtcd")
                dt2.Columns.Add("sbcd")
                dt2.Columns.Add("accName")
                dt2.Columns.Add("ACC")
                Dim tbRow1 As DataRow
                For Each tbRow As DataRow In trows
                    tbRow1 = dt2.NewRow()
                    tbRow1(0) = tbRow("dtcd")
                    tbRow1(1) = tbRow("sbcd")
                    tbRow1(2) = tbRow("accName")
                    tbRow1(3) = tbRow("ACC")
                    dt2.Rows.Add(tbRow1)
                Next
                dg.DataSource = dt2
                dg.DataBind()
                ViewState("dataset") = dt2
            End If

            'Dim gRow As GridViewRow
            'For Each gRow In dg.Rows
            '    gRow.Height = 10
            'Next

            objMod.gCon.Close()


            If is_filter_hide = True Then
                lblfilter.Visible = False
                cmb_field.Visible = False
                txt_field_search.Visible = False
                btn_Find_2.Visible = False
            End If

            If is_filter_hide = False Then
                lblfilter.Visible = True
                cmb_field.Visible = True
                txt_field_search.Visible = True
                btn_Find_2.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            objMod = Nothing
            'dg.Controls.Clear()

        End Try



    End Sub


    Public Sub set_Grid_Format()
        Dim isDATA As Boolean
        Try
            For Each row As GridViewRow In Me.dg.Rows
                isDATA = True
                If Len(Trim(txtCOL_1_hide.Text)) <> 0 Then
                    row.Cells(CInt(txtCOL_1_hide.Text)).Visible = False
                End If
                If Len(Trim(txtCOL_2_hide.Text)) <> 0 Then
                    row.Cells(CInt(txtCOL_2_hide.Text)).Visible = False
                End If
                If Len(Trim(txtCOL_3_hide.Text)) <> 0 Then
                    row.Cells(CInt(txtCOL_3_hide.Text)).Visible = False
                End If
                If Len(Trim(txtCOL_4_hide.Text)) <> 0 Then
                    row.Cells(CInt(txtCOL_4_hide.Text)).Visible = False
                End If
                If Len(Trim(txtCOL_5_hide.Text)) <> 0 Then
                    row.Cells(CInt(txtCOL_5_hide.Text)).Visible = False
                End If
                If Len(Trim(txtCOL_6_hide.Text)) <> 0 Then
                    row.Cells(CInt(txtCOL_6_hide.Text)).Visible = False
                End If
            Next

            If isDATA = True Then
                If Len(Trim(txtCOL_1_hide.Text)) <> 0 Then
                    Me.dg.HeaderRow.Cells(CInt(txtCOL_1_hide.Text)).Visible = False
                End If
                If Len(Trim(txtCOL_2_hide.Text)) <> 0 Then
                    Me.dg.HeaderRow.Cells(CInt(txtCOL_2_hide.Text)).Visible = False
                End If
                If Len(Trim(txtCOL_3_hide.Text)) <> 0 Then
                    Me.dg.HeaderRow.Cells(CInt(txtCOL_3_hide.Text)).Visible = False
                End If
                If Len(Trim(txtCOL_4_hide.Text)) <> 0 Then
                    Me.dg.HeaderRow.Cells(CInt(txtCOL_4_hide.Text)).Visible = False
                End If
                If Len(Trim(txtCOL_5_hide.Text)) <> 0 Then
                    Me.dg.HeaderRow.Cells(CInt(txtCOL_5_hide.Text)).Visible = False
                End If
                If Len(Trim(txtCOL_6_hide.Text)) <> 0 Then
                    Me.dg.HeaderRow.Cells(CInt(txtCOL_6_hide.Text)).Visible = False
                End If
            End If

          
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Public Sub showControl_bySearch(ByVal strSql As String)
        Dim objMod As New modMain
        Try
            If Not chkISDTTB.Checked Then
                objMod.setCon(True)
                Dim Da As SqlClient.SqlDataAdapter = New SqlDataAdapter(strSql, objMod.gCon)
                Dim dt As New DataTable()
                Da.Fill(dt)
                dg.DataSource = dt
                dg.DataBind()

                ViewState("dataset") = dt
                objMod.gCon.Close()
                set_Grid_Format()
            End If

            If chkISDTTB.Checked Then
                Dim dt As New DataTable()
                Dim dt2 As New DataTable()
                dt = Session(strSql)
                Dim trows As DataRow() = dt.Select(strWHERE)
                dt2.Columns.Add("dtcd")
                dt2.Columns.Add("sbcd")
                dt2.Columns.Add("accName")

                Dim tbRow1 As DataRow
                For Each tbRow As DataRow In trows
                    tbRow1 = dt2.NewRow()
                    tbRow1(0) = tbRow(0)
                    tbRow1(1) = tbRow(1)
                    tbRow1(2) = tbRow(2)
                    dt2.Rows.Add(tbRow1)
                Next
                dg.DataSource = dt2
                dg.DataBind()
                ViewState("dataset") = dt2
            End If
            SearchByTagTB.Text = ""
            txt_field_search.Text = ""


            If is_filter_hide = True Then
                lblfilter.Visible = False
                cmb_field.Visible = False
                txt_field_search.Visible = False
                btn_Find_2.Visible = False
            End If

            If is_filter_hide = False Then
                lblfilter.Visible = True
                cmb_field.Visible = True
                txt_field_search.Visible = True
                btn_Find_2.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            objMod = Nothing

        End Try


    End Sub




    Protected Sub SelectIL_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim gvRow As GridViewRow = DirectCast(TryCast(sender, Control).Parent.Parent, GridViewRow)

        Dim index As Integer = gvRow.RowIndex
        Dim lnkBtn As LinkButton = DirectCast(sender, LinkButton)

        lnkBtn.CommandArgument = dg.Rows(index).Cells(1).Text
        '_lnkID = Integer.Parse(lnkBtn.CommandArgument)
        Dim count As Integer = dg.Rows(index).Cells.Count
        'Dim STR As String = "Null"
        'Dim STR1 As String = "Null"
        'Dim STR2 As String = "Null"

        Try


            If (txt_get_by.Text) = CStr(modMain.gStrSelectEvent) Then
                strID = dg.Rows(index).Cells(txtCOL_1.Text).Text
                RaiseEvent SelectEvent(strID, txtSenderRef.Text)
                RaiseEvent CloseEvent(txtSenderRef.Text)
                Exit Sub
            End If
            If (txt_get_by.Text) = CStr(modMain.gStrSelectEvent_2) Then
                strID = dg.Rows(index).Cells(txtCOL_1.Text).Text
                strID2 = dg.Rows(index).Cells(txtCOL_2.Text).Text
                RaiseEvent SelectEvent_2(strID, strID2, txtSenderRef.Text)
                RaiseEvent CloseEvent(txtSenderRef.Text)
                Exit Sub
            End If
            If (txt_get_by.Text) = CStr(modMain.gStrSelectEvent_3) Then
                strID = dg.Rows(index).Cells(txtCOL_1.Text).Text
                strID2 = dg.Rows(index).Cells(txtCOL_2.Text).Text
                strID3 = dg.Rows(index).Cells(txtCOL_3.Text).Text
                RaiseEvent SelectEvent_3(strID, strID2, strID3, txtSenderRef.Text)
                RaiseEvent CloseEvent(txtSenderRef.Text)
                Exit Sub
            End If
            If (txt_get_by.Text) = CStr(modMain.gStrSelectEvent_4) Then
                strID = dg.Rows(index).Cells(txtCOL_1.Text).Text
                strID2 = dg.Rows(index).Cells(txtCOL_2.Text).Text
                strID3 = dg.Rows(index).Cells(txtCOL_3.Text).Text
                strID4 = dg.Rows(index).Cells(txtCOL_4.Text).Text
                RaiseEvent SelectEvent_4(strID, strID2, strID3, strID4, txtSenderRef.Text)
                RaiseEvent CloseEvent(txtSenderRef.Text)
                Exit Sub
            End If

            If (txt_get_by.Text) = CStr(modMain.gStrSelectEvent_6) Then
                strID = dg.Rows(index).Cells(txtCOL_1.Text).Text
                strID2 = dg.Rows(index).Cells(txtCOL_2.Text).Text
                strID3 = dg.Rows(index).Cells(txtCOL_3.Text).Text
                strID4 = dg.Rows(index).Cells(txtCOL_4.Text).Text
                strID5 = dg.Rows(index).Cells(txtCOL_5.Text).Text
                strID6 = dg.Rows(index).Cells(txtCOL_6.Text).Text
                RaiseEvent SelectEvent_6(strID, strID2, strID3, strID4, strID5, strID6, txtSenderRef.Text)
                RaiseEvent CloseEvent(txtSenderRef.Text)
                Exit Sub
            End If
            'If count > 1 Then
            '    DetailCode = dg.Rows(index).Cells(1).Text
            '    DetailCode1 = dg.Rows(index).Cells(1).Text
            '    STR = dg.Rows(index).Cells(2).Text
            'End If
            'If count > 3 Then
            '    SubCode = dg.Rows(index).Cells(2).Text
            '    SubCode1 = dg.Rows(index).Cells(2).Text
            '    SubCodeName = dg.Rows(index).Cells(3).Text
            '    SubCodeName1 = dg.Rows(index).Cells(3).Text
            '    STR1 = dg.Rows(index).Cells(3).Text
            'End If
            'If count > 4 Then
            '    STR2 = dg.Rows(index).Cells(4).Text
            'End If


            'If (IsNumeric(STR)) Then
            '    nID2 = Integer.Parse(STR)
            'End If
            'If (IsNumeric(STR1)) Then
            '    nID3 = Integer.Parse(STR1)
            'End If
            'If (IsNumeric(STR2)) Then
            '    nID4 = Integer.Parse(STR2)
            'End If




            'SelRec()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        RaiseEvent CloseEvent(txtSenderRef.Text)
    End Sub

    Sub SortCommand(ByVal s As Object, ByVal e As GridViewSortEventArgs)
        'strFields = e.SortEXPRESSion
        'showControl()

    End Sub




    Private Property GridViewSortDirection() As String

        Get
            Return IIf(ViewState("SortDirection") = Nothing, "DESC", ViewState("SortDirection"))
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set

    End Property

    Private Property GridViewSortEXPRESSion() As String

        Get
            Return IIf(ViewState("SortEXPRESSion") = Nothing, String.Empty, ViewState("SortEXPRESSion"))
        End Get
        Set(ByVal value As String)
            ViewState("SortEXPRESSion") = value
        End Set

    End Property
    Private Function GetSortDirection() As String

        Select Case GridViewSortDirection
            Case "ASC"
                GridViewSortDirection = "DESC"
            Case "DESC"
                GridViewSortDirection = "ASC"
        End Select
        Return GridViewSortDirection

    End Function
    Protected Function SortDataTable(ByVal dataTable As Data.DataTable, ByVal isPageIndexChanging As Boolean) As Data.DataView
        If Not dataTable Is Nothing Then
            Dim dataView As New Data.DataView(dataTable)
            If GridViewSortEXPRESSion <> String.Empty Then
                If isPageIndexChanging Then
                    dataView.Sort = String.Format("{0} {1}", GridViewSortEXPRESSion, GridViewSortDirection)
                Else
                    dataView.Sort = String.Format("{0} {1}", GridViewSortEXPRESSion, GetSortDirection())
                End If
            End If
            Return dataView
        Else
            Return New Data.DataView()
        End If

    End Function

    Protected Sub dg_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dg.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbtn As New LinkButton()

            lbtn.Text = "view"
            lbtn.CssClass = "btn btn-info"

            e.Row.Cells(0).Controls.Add(lbtn)

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lbtn)
            AddHandler lbtn.Click, AddressOf SelectIL_Click

        End If
    End Sub



    Protected Sub dg_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles dg.Sorting
        GridViewSortEXPRESSion = e.SortExpression
        Dim pageIndex As Integer = dg.PageIndex
        dg.DataSource = SortDataTable((ViewState("dataset")), False)
        dg.DataBind()
        dg.PageIndex = pageIndex
    End Sub



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            RaiseEvent SearchEvent(QueryStr, SearchByTagTB.Text, txtSenderRef.Text)
            showControl_bySearch(QueryStr)

        Catch ex As Exception
            'Throw New Exception(ex.Message)
            Exit Sub
        End Try


    End Sub
    Private Sub init_CMB_Field(str_Field_list As String)

        Dim str_cols() As String
        Dim str_cols_Name As String = ""

        cmb_field.Items.Clear()
        If Len(Trim(str_Field_list)) = 0 Then Exit Sub

        Try
            'cmb_field.Items.Add(New System.Web.UI.WebControls.ListItem(modMain.gStr_Empty, "9999"))

            str_cols = Split(str_Field_list, ",")
            For j = 0 To str_cols.GetUpperBound(0)
                cmb_field.Items.Add(New System.Web.UI.WebControls.ListItem(str_cols(j).ToString(), j))
            Next
            cmb_field.SelectedIndex = 0
        Catch ex As Exception

        End Try


    End Sub


    Protected Sub btn_Find_2_Click(sender As Object, e As EventArgs) Handles btn_Find_2.Click
        Try

            If Len(Trim(SearchByTagTB.Text)) = 0 Then
                RaiseEvent SearchEvent_By_Field(QueryStr, txt_field_search.Text, txtSenderRef.Text, cmb_field.SelectedItem.Text)
            End If

            If Len(Trim(SearchByTagTB.Text)) <> 0 Then
                RaiseEvent SearchEvent_By_Field_WITH_TWO_PARAS(QueryStr, txt_field_search.Text, txtSenderRef.Text, cmb_field.SelectedItem.Text, SearchByTagTB.Text)
            End If


            showControl_bySearch(QueryStr)

        Catch ex As Exception
            'Throw New Exception(ex.Message)
            Exit Sub
        End Try
    End Sub

    Protected Sub Hide_SearchEvent_By_ExactField()
        Try
            RaiseEvent Hide_SearchEvent_By_Field()
            lblfilter.Visible = False
            cmb_field.Visible = False
            txt_field_search.Visible = False
            btn_Find_2.Visible = False
        Catch ex As Exception
            'Throw New Exception(ex.Message)
        End Try

    End Sub
End Class
