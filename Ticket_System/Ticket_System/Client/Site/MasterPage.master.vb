Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Xml

Imports System.Data.SqlClient


Partial Class Site_MasterPage
    Inherits System.Web.UI.MasterPage

    
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    If (Session("gmenuDTTB") Is Nothing) Then
    '        Response.Redirect("../frmLogin.aspx")
    '        'Server.End()
    '    End If
    '    'CHECKSESSIONTIMEOUT()
    '    Dim dtTB_mneu As New DataTable()

    '    Dim dtParent As New DataTable()
    '    Dim dtChild As New DataTable()
    '    Dim relation As DataRelation
    '    Dim dtFilter As New DataTable()
    '    Dim ds2 As DataSet
    '    Dim relation2 As DataRelation


    '    Dim ds As New DataSet("Menus")


    '    Dim path As String = ""
    '    Dim dtTB As New DataTable

    '    If Not IsPostBack Then
    '        Try
    '            ds = New DataSet("Menus")
    '            dtTB = Session("gmenuDTTB")
    '            dtTB.TableName = "Menu"
    '            ds.Tables.Add(dtTB.Copy)
    '            relation = New DataRelation("ParentChild", ds.Tables("Menu").Columns("MenuID"), ds.Tables("Menu").Columns("ParentID"), True)
    '            relation.Nested = True
    '            ds.Relations.Add(relation)
    '            XmlDataSource.Data = ds.GetXml()
    '            '*************************************
    '            'For TreeView
    '            path = HttpContext.Current.Request.Url.AbsoluteUri
    '            'Url LIKE '%frm_TreeView.aspx'
    '            dtFilter = dtTB.[Select]("Url LIKE '%" + path.Split("/"c).Last + "'").CopyToDataTable()
    '            ds2 = New DataSet("Menus")
    '            If IsDBNull(dtFilter.Rows(0)("Pagelevel")) Then
    '                dtParent = dtTB.[Select]("MenuID=" + dtFilter.Rows(0)("ParentID").ToString()).CopyToDataTable() 'Get Parent
    '                dtChild = dtTB.[Select]("ParentID=" + dtFilter.Rows(0)("ParentID").ToString()).CopyToDataTable() 'Get Child against Parent
    '                dtParent.Rows(0)("ParentID") = DBNull.Value
    '                dtChild.Merge(dtParent)
    '                '*************************************
    '                dtChild.TableName = "Menu"
    '                ds2.Tables.Add(dtChild.Copy)
    '                relation2 = New DataRelation("ParentChild", ds2.Tables("Menu").Columns("MenuID"), ds2.Tables("Menu").Columns("ParentID"), True)
    '                relation2.Nested = True
    '                ds2.Relations.Add(relation2)
    '                xmlDataSource1.Data = ds2.GetXml()
    '            Else
    '                xmlDataSource1.Data = ds2.GetXml()
    '            End If
    '        Catch ex As Exception
    '            Response.Write("ERROR IN GROUP CREATION...." & vbNewLine & ex.Message)
    '        Finally
    '            dtTB.Dispose()
    '            dtTB = Nothing
    '            ds.Dispose()
    '            ds = Nothing
    '            ds = Nothing
    '            dtTB_mneu = Nothing
    '            dtParent = Nothing
    '            dtChild = Nothing
    '            relation = Nothing
    '            dtFilter = Nothing
    '            ds2 = Nothing
    '            relation2 = Nothing
    '            XmlDataSource.Dispose()
    '            XmlDataSource = Nothing
    '            xmlDataSource1.Dispose()
    '            xmlDataSource1 = Nothing
    '        End Try

    '        '------------------------------------------------
    '        '------------------------------------------------





    '        Dim dtTB_2 As New DataTable()
    '        Dim objMod As New modMain
    '        Dim objMN As New modMain
    '        Try
    '            'Dim str As String = ""
    '            'Dim nCCID As Integer = 1


    '            'dtTB_2 = Session("gobjUSER")

    '            'str = dtTB_2.Rows(0)("Name")
    '            'str = str & " ," & Format(CDate(dtTB_2.Rows(0)("dt")), "dd-MM-yyyy HH:mm")
    '            'str = str & " ," & dtTB_2.Rows(0)("CCNAME")
    '            'str = str & ", PWD. EXP:" & Format(CDate(dtTB_2.Rows(0)("passwordDate")), "dd-MM-yyyy")

    '            'str = str & modMain.gSTRVER

    '            'nCCID = dtTB_2.Rows(0)("locid")

    '            'lblUserName.Text = str
    '            'Dim dt As Date = dtTB_2.Rows(0)("passwordDate")
    '            'dt = DateAdd(DateInterval.Day, -5, dt)

    '            'Dim strDT As String = objMod.DateSerial_SFTX_STR(dt.Day, dt.Month, dt.Year)

    '            'Dim o_dtIDDateOfIssue As Date
    '            ''Dim o_dtIDExpiryDate As Date
    '            'objMod.getMMDDYYYY(strDT, o_dtIDDateOfIssue)
    '            ''objMod.getMMDDYYYY(dtExpiryDate.Text, o_dtIDExpiryDate)

    '            'Dim o_Now As Date = Now.Date

    '            'If objMod.isDATEValid_GREATER(o_dtIDDateOfIssue, o_Now) Then
    '            '    lblUserName.BackColor = Drawing.Color.Red
    '            'End If

    '            ''************************************************************
    '            'Dim str_fmComp As String = ""
    '            'Dim str_fmCompPh As String = ""
    '            'Dim str_fmCompAdd As String = ""
    '            'Dim str_fmCompemail As String = ""
    '            'Dim strWHR As String = " CCID = " & nCCID & " "


    '            'objMN.get_CCID_Report_Values(strWHR, Session("gobjCCID"), str_fmComp, str_fmCompPh, str_fmCompAdd, str_fmCompemail)
    '            'lblCompanyName.Text = str_fmComp
    '            'lblCompanyPhoneFax.Text = str_fmCompPh & " , " & str_fmCompemail & " , " & str_fmCompAdd


    '            ''************************************************************

    '        Catch ex As Exception
    '            lblErr.Text = ex.Message
    '        Finally
    '            dtTB_2.Dispose()
    '            dtTB_2 = Nothing
    '            objMN = Nothing
    '            objMod = Nothing

    '        End Try



    '    End If

    '    'pnlMnCntPane.Height = 520
    'End Sub

    ''Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
    ''    MyBase.OnPreRender(e)
    ''    Dim strDisAbleBackButton As String
    ''    strDisAbleBackButton = "<SCRIPT language=javascript>" & vbLf
    ''    strDisAbleBackButton += "window.history.forward(1);" & vbLf
    ''    strDisAbleBackButton += vbLf & "</SCRIPT>"
    ''    ClientScript.RegisterClientScriptBlock(Me.Page.[GetType](), "wee", strDisAbleBackButton)
    ''End Sub

    ''Public Sub CHECKSESSIONTIMEOUT()
    ''    Dim msgSession As String = "'Warning: Within next 3 minutes, if you do not do anything, '+"
    ''    ' our system will redirect to the login page. Please save changed data"

    ''    Dim int_MilliSecondsTimeReminder As Integer = (Me.Session.Timeout * 60000) - 3 * 60000
    ''    'time to redirect, 5 milliseconds before session ends
    ''    Dim int_MilliSecondsTimeOut As Integer = (Me.Session.Timeout * 60000) - 5

    ''    Dim str_Script As String = vbCr & vbLf & "            var myTimeReminder, myTimeOut; " & vbCr & vbLf & "            clearTimeout(myTimeReminder); " & vbCr & vbLf & "            clearTimeout(myTimeOut); " & "var sessionTimeReminder = " & int_MilliSecondsTimeReminder.ToString() & "; " & "var sessionTimeout = " & int_MilliSecondsTimeOut.ToString() & ";" & "function doReminder(){ alert('" & msgSession & "'); }" & "function doRedirect(){ window.location.href='login.aspx'; }" & vbCr & vbLf & "            myTimeReminder=setTimeout('doReminder()', sessionTimeReminder); " & vbCr & vbLf & "            myTimeOut=setTimeout('doRedirect()', sessionTimeout); "

    ''    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "CheckSessionOut", str_Script, True)

    ''End Sub
    'End Sub

    'https:www.aspforums.net/Threads/108804/Database-Driven-N-Level-MultiLevel-BootstrapResponsive-Vertical-Menu-using-C-and-jQuery-in-ASPNet/
    Private Menus As DataTable = New DataTable()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

        'If (Session("gmenuDTTB") Is Nothing) Then
        '    Response.Redirect("~/Site/frmLogin.aspx")

        'End If

        If Not IsPostBack Then

            'BindMenu()

            Dim dtTB_2 As New DataTable()

            Dim objMod As New modMain
            Dim objMN As New modMain
            Try
                Dim str As String = ""
                Dim str_1 As String = ""
                Dim nccid As Integer = 1


                dtTB_2 = Session("gobjCOMPANY")

                str_1 = dtTB_2.Rows(0)("Company_Name")
                
                lblUserName.Text = str_1
                lblUserName_1.Text = str_1

                '************************************************************
                Dim str_fmcomp As String = ""
                Dim str_fmcompph As String = ""
                Dim str_fmcompadd As String = ""
                Dim str_fmcompemail As String = ""
                Dim strwhr As String = " ccid = " & nccid & " "


                objMN.get_CCID_Report_Values(strwhr, Session("gobjccid"), str_fmcomp, str_fmcompph, str_fmcompadd, str_fmcompemail)
                lblCompanyName.Text = "Complain System"
                '  lblcompanyphonefax.text = str_fmcompph & " , " & str_fmcompemail & " , " & str_fmcompadd


                ' '************************************************************

            Catch ex As Exception
                lblerr.text = ex.Message
            Finally
                'dtTB_2.Dispose()
                'dtTB_2 = Nothing
                objMN = Nothing
                objMod = Nothing

            End Try

        End If
    End Sub

    'Protected Sub rptMenu_OnItemBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
    '    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

    '        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

    '            If Menus IsNot Nothing Then
    '                Dim drv As DataRowView = TryCast(e.Item.DataItem, DataRowView)
    '                Dim ID As String = drv("MenuId").ToString()
    '                Dim Title As String = drv("Name").ToString()
    '                Dim rows As DataRow() = Menus.[Select]("ParentId=" & ID)

    '                If rows.Length > 0 Then
    '                    Dim sb As StringBuilder = New StringBuilder()

    '                    sb.Append("<ul id='" & Title & "' class='treeview-menu' >")

    '                    For Each item In rows
    '                        Dim parentId As String = item("MenuId").ToString()
    '                        Dim parentTitle As String = item("Name").ToString()
    '                        Dim parentRow As DataRow() = Menus.[Select]("ParentId=" & parentId)

    '                        If parentRow.Count() > 0 Then
    '                            sb.Append("<li class='treeview' data-target='#" & parentTitle & "'><a href='/" & item("Url") & "' > <i class='" & item("CssFont") & "'></i>" & item("Name") & "<span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>")

    '                        Else
    '                            sb.Append("<li ><a href='/" & item("Url") & "'>" + item("Name") & "</a>")
    '                            sb.Append("</li>")
    '                        End If

    '                        sb = CreateChild(sb, parentId, parentTitle, parentRow)
    '                    Next

    '                    sb.Append("</ul>")

    '                    TryCast(e.Item.FindControl("ltrlSubMenu"), Literal).Text = sb.ToString()
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Function CreateChild(ByVal sb As StringBuilder, ByVal parentId As String, ByVal parentTitle As String, ByVal parentRows As DataRow()) As StringBuilder
    '    If parentRows.Length > 0 Then
    '        sb.Append("<ul id='" & parentTitle & "' class='treeview-menu' > ")

    '        For Each item In parentRows
    '            Dim childId As String = item("MenuId").ToString()
    '            Dim childTitle As String = item("Name").ToString()
    '            Dim childRow As DataRow() = Menus.[Select]("ParentId=" & childId)

    '            If childRow.Count() > 0 Then
    '                sb.Append("<li data-target='#" & childTitle & "' ><a href='/" & item("Url") & "'" + item("Name") & "><span class='pull-right-container'></span><i class='" & item("CssFont") & "'></i></a>")
    '                sb.Append("</li>")

    '            Else
    '                sb.Append("<li ><a href='/" & item("Url") & "' >" + item("Name") & "</a>")
    '                sb.Append("</li>")

    '            End If

    '            CreateChild(sb, childId, childTitle, childRow)
    '        Next

    '        sb.Append("</ul>")
    '        sb.Append("</li>")
    '    End If

    '    Return sb
    'End Function

    'Private Sub BindMenu()
    '    rptCategories.DataSource = Nothing

    '    Menus = Session("gmenuDTTB")
    '    Dim view As DataView = New DataView(Menus)
    '    view.RowFilter = "ParentId IS NULL "
    '    rptCategories.DataSource = view
    '    rptCategories.DataBind()
    'End Sub

    Protected Sub btn_Logout_Click(sender As Object, e As EventArgs) Handles btn_Logout.Click
        Response.Redirect("frm_ComplainLogin.aspx")
    End Sub

End Class

