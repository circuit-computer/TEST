Imports System
Imports System.Data
Imports System.IO
Partial Class frmlogin
    Inherits System.Web.UI.Page

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

    


    Protected Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit.Click
        'sms_for_outward("12312", "500", "usd", "00971557457134")
        Dim str_date As Date
        Dim objMod As New modMain
        'Dim DTTB34 As New dtst_ACC.dttb_COADataTable

        dvERR.Visible = False
        If Len(Trim(txtuserid.Text)) = 0 Then
            dvERR.Visible = True
            lblERR.Text = "Please enter valid login id"
            ERR_MSG("Please enter valid login id", True)
            Exit Sub
        End If
        If Len(Trim(txtpwd.Text)) = 0 Then
            dvERR.Visible = True
            lblERR.Text = "Please enter password"
            ERR_MSG("Please Enter Password", True)
            Exit Sub
        End If

        Session.RemoveAll()

        Dim dtTB As New DataTable()
        Dim nUSERGroupID As Integer = 0
        Dim str_USERGroupID As String = ""
        Dim str_USERNAME As String = ""
        Dim nCCID As Integer = 0
        Dim strDT As String = ""
        Try

            objMod.setCon(True)

            strDT = objMod.DateSerial_SFTX_STR(Date.Now.Year, Date.Now.Month, Date.Now.Day)

            objMod.strSql = "SELECT * ,dbo.fun_GetCCID_Name(locid)  as CCNAME, dbo.fun_GetCCID_Short_Name(locid) as compnameshort, getdate() as dt From tblUser where ( userlogin='" & Trim(txtuserid.Text) & "' AND userPwd='" & Trim(txtpwd.Text) & "' AND isActive=1 AND passwordDate >'" & strDT & "' )  "

            'objMod.strSql = "SELECT *,dbo.fun_GetCCID_Name(locid) as CCNAME,getdate() as dt  From tblUser where ( userlogin='" & Trim(txtuserid.Text) & "'  )  "

            dtTB = objMod.DTTB_Fill(objMod.strSql)
            If dtTB.Rows.Count = 0 Then
                dvERR.Visible = True
                lblERR.Text = "Invalid Login / Password"
                ' ERR_MSG("Invalid Login / Password", True)
                createNewSessionID()
                Exit Sub
            End If

            Session("gobjUSER") = dtTB
            'Session.Insert("gobjUSER", dtTB, Nothing, System.DateTime.Now.AddMinutes(50), System.Web.Caching.Session.NoSlidingExpiration)

            nCCID = CInt(dtTB.Rows(0)("locid"))
            'nUSERGroupID = CInt(dtTB.Rows(0)("UserGroupID"))
            str_USERGroupID = dtTB.Rows(0)("UserGroupID_GID")
            str_USERNAME = dtTB.Rows(0)("NAME")
            str_date = dtTB.Rows(0)("passwordDate")

            '---- SET MENU
            Session.Remove("gmenuDTTB")
            dtTB.Dispose()
            dtTB = Nothing
            dtTB = New DataTable
            objMod.strSql = getMenuText(str_USERGroupID, str_USERNAME)
            'dtTB = objMod.DTTB_Fill(objMod.strSql)
            Session("gmenuDTTB") = dtTB
            'Session.Insert("gmenuDTTB", dtTB, Nothing, System.DateTime.Now.AddMinutes(50), System.Web.Caching.Session.NoSlidingExpiration)

            '---- SET CC INFORMATION
            dtTB.Dispose()
            dtTB = Nothing
            dtTB = New DataTable
            objMod.strSql = " select * from tluCompBranchInfo"
            dtTB = objMod.DTTB_Fill(objMod.strSql)
            Session("gobjCCID") = dtTB
            'Session.Insert("gobjCCID", dtTB, Nothing, System.DateTime.Now.AddMinutes(50), System.Web.Caching.Session.NoSlidingExpiration)
            '-------------------------------




            '***************************RISK*******************************
            Session("IsLogin") = 1
            'Dim myCookie As HttpCookie = New HttpCookie("UserSettings")
            'myCookie("Font") = "Arial"
            'myCookie("Color") = "Blue"
            'myCookie.Expires = Now.AddDays(1)
            'Response.Cookies.Add(myCookie)
            'Server.Transfer("frm_mng\frm_sys_customer.aspx")

            '----------------------------------------------------------
            '**********************************************************
            Try
                Dim str_UserID_gID As String = ""
                'Dim nCCID As Integer = 0
                objMod.getUSER_INFO_GID_WITHOUT_ID(Session("gobjUSER"), nCCID, str_UserID_gID)

                objMod.mSave_LOG(txtuserid.Text, modMain.eLG_TYP.SAVE_TRANSACTION, _
                                " User LogIn", _
                                 modMain.eLG_Prt.NORMAL, _
                                 str_UserID_gID, nCCID)
            Catch ex As Exception
                'ERR_MSG(ex.Message)
            End Try
            '**********************************************************

            Dim dt As Date = str_date
            dt = DateAdd(DateInterval.Day, -5, dt)

            Dim strDT_1 As String = objMod.DateSerial_SFTX_STR(dt.Year, dt.Month, dt.Day)

            Dim o_dtIDDateOfIssue As Date
            'Dim o_dtIDExpiryDate As Date
            objMod.getMMDDYYYY(strDT_1, o_dtIDDateOfIssue)
            'objMod.getMMDDYYYY(dtExpiryDate.Text, o_dtIDExpiryDate)

            Dim o_Now As Date = Now.Date

       
            '-----------------------------------------------------------
            Server.Transfer("frmIndex.aspx")
            'Response.Redirect("../index.html")

        Catch ex As Exception
            dvERR.Visible = True
            lblERR.Text = ex.Message
        Finally
            objMod.setCon(False)
            objMod = Nothing
            ' dtTB=Nothing 
        End Try
    End Sub

    Private Sub ERR_MSG(ByVal strErr As String, Optional ByVal isError As Boolean = True)
        Try

            Dim mpLabel As Label
            mpLabel = CType(Master.FindControl("lblERR"), Label)
            If Len(Trim(strErr)) = 0 Then
                mpLabel.Visible = False
                Exit Sub
            End If

            'mpLabel.ForeColor = Drawing.Color.Pink
            If Not isError Then
                mpLabel.ForeColor = Drawing.Color.LightGreen
            End If

            mpLabel.Text = strErr
            mpLabel.Visible = True

        Catch ex As Exception
            Dim mpLabel As Label
            mpLabel = CType(Master.FindControl("lblERR"), Label)
            mpLabel.Text = ex.Message
            mpLabel.Visible = True
        End Try
    End Sub


    Public Sub Login_Direct()
        Dim objMod As New modMain
        'Dim DTTB34 As New dtst_ACC.dttb_COADataTable
        lblERR.Visible = False
        dvERR.Visible = False



        Session.RemoveAll()
        Dim dtTB As New DataTable()
        Dim nUSERGroupID As Integer = 0
        Dim str_USERGroupID As String = ""
        Dim str_USERNAME As String = ""
        Dim nCCID As Integer = 0
        Dim strDT As String = ""

        Dim str_Login As String = "mn_ADMIN"
        Dim str_Pwd As String = "111"
        Try

            objMod.setCon(True)

            strDT = objMod.DateSerial_SFTX_STR(Date.Now.Year, Date.Now.Month, Date.Now.Day)

            objMod.strSql = "SELECT * ,dbo.fun_GetCCID_Name(locid)  as CCNAME, dbo.fun_GetCCID_Short_Name(locid) as compnameshort, getdate() as dt From tblUser where ( userlogin='" & str_Login & "'    )  "

            dtTB = objMod.DTTB_Fill(objMod.strSql)
            If dtTB.Rows.Count = 0 Then

                dvERR.Visible = True
                lblERR.Text = "Invalid Login / Password"
                Exit Sub
            End If

            Session("gobjUSER") = dtTB
            'Session.Insert("gobjUSER", dtTB, Nothing, System.DateTime.Now.AddMinutes(50), System.Web.Caching.Session.NoSlidingExpiration)

            nCCID = CInt(dtTB.Rows(0)("locid"))
            'nUSERGroupID = CInt(dtTB.Rows(0)("UserGroupID"))
            str_USERGroupID = dtTB.Rows(0)("UserGroupID_GID")
            str_USERNAME = dtTB.Rows(0)("NAME")

            '---- SET MENU
            Session.Remove("gmenuDTTB")
            dtTB.Dispose()
            dtTB = Nothing
            dtTB = New DataTable
            objMod.strSql = getMenuText(str_USERGroupID, str_USERNAME)
            dtTB = objMod.DTTB_Fill(objMod.strSql)
            Session("gmenuDTTB") = dtTB
            'Session.Insert("gmenuDTTB", dtTB, Nothing, System.DateTime.Now.AddMinutes(50), System.Web.Caching.Session.NoSlidingExpiration)


            '---- SET CC INFORMATION
            dtTB.Dispose()
            dtTB = Nothing
            dtTB = New DataTable
            objMod.strSql = " select * from tluCompBranchInfo"
            dtTB = objMod.DTTB_Fill(objMod.strSql)
            Session("gobjCCID") = dtTB
            'Session.Insert("gobjCCID", dtTB, Nothing, System.DateTime.Now.AddMinutes(50), System.Web.Caching.Session.NoSlidingExpiration)
            '-------------------------------



            Session("IsLogin") = 1


            'Server.Transfer("frmIndex.aspx")
            Server.Transfer("frm_fd/frm_fd_ro_swift.aspx?id=202767")
        Catch ex As Exception
            dvERR.Visible = True

            lblERR.Text = ex.Message
        Finally
            objMod.setCon(False)
        End Try

    End Sub


    Private Function getMenuText(ByVal str_USRGroupid As String, ByVal strUSERNAME As String) As String
        Dim strMnu As String = ""
        Dim str_Admin_Grp As String = "" 'modMain.gStr_admin_Group


        'If UCase(strUSERNAME) = "ADMIN" Then
        '    strMnu = "SELECT     PG.MenuID,  PG.PageName  as Name, PG.URL as Url, PG.ParentID,indx   FROM  RIT_tluPages_WEB AS PG where isactive='TRUE' order by indx,MenuID "
        '    getMenuText = strMnu
        '    Exit Function
        'End If

        If UCase(str_USRGroupid) = str_Admin_Grp Then
            strMnu = "SELECT     PG.MenuID,  PG.PageName  as Name, CONCAT(PG.Solution_Name, '', PG.URL) as Url, PG.ParentID,PG.Pagelevel,indx, CssFont  FROM  RIT_tluPages_WEB AS PG where isactive='TRUE' order by indx,MenuID "
            getMenuText = strMnu
            Exit Function
        End If


        strMnu = " select * from ( " _
                              & "  SELECT MNU_MN.menuid,MNU_MN.Name,MNU_MN.Url,MNU_MN.ParentID,MNU_MN.indx, MNU_MN.Pagelevel, MNU_MN.CssFont from ( " _
                      & "  SELECT     PG.MenuID,  PG.PageName  as Name, CONCAT(PG.Solution_Name, '',PG.URL as Url, PG.ParentID,indx, PG.Pagelevel, PG.CssFont    " _
                      & "  FROM  RIT_tluPages_WEB AS PG RIGHT OUTER JOIN  RIT_tblGroupRights_WEB AS RGT ON PG.MenuID = RGT.MenuID  " _
                      & "  WHERE     (RGT.UserGroupID_GID = '" & str_USRGroupid & "' ) and isnull(isActive,'FALSE') = 'TRUE'   " _
                      & "  )MNU_MN " _
                      & "          UNION ( " _
                      & "    select PRNT.menuid,PRNT.Name,PRNT.Url,PRNT.ParentID,PRNT.indx, PRNT.Pagelevel, PRNT.CssFont from  " _
                      & "    (SELECT     PG.MenuID,  PG.PageName  as Name, CONCAT(PG.Solution_Name, '',PG.URL as Url, PG.ParentID,indx, PG.Pagelevel, PG.CssFont   " _
                      & "    FROM  RIT_tluPages_WEB AS PG RIGHT OUTER JOIN  RIT_tblGroupRights_WEB AS RGT ON PG.MenuID = RGT.MenuID  " _
                      & "    WHERE     (RGT.UserGroupID_GID = '" & str_USRGroupid & "' ) and isnull(isActive,'FALSE') = 'TRUE'   " _
                      & "    )MNU " _
                      & "    left outer join " _
                      & "    ( " _
                      & "    SELECT PG.MenuID,  PG.PageName  as Name, CONCAT(PG.Solution_Name, '',PG.URL as Url, PG.ParentID,indx,PG.Pagelevel, PG.CssFont   " _
                      & "    FROM  RIT_tluPages_WEB AS PG WHERE ISNULL(ISACTIVE,'FALSE') = 'TRUE' AND isnull(ParentID  ,0) <> 0 " _
                      & "    )PRNT on MNU.ParentID  = PRNT.MenuID  " _
                      & "    UNION " _
                      & "    SELECT PG.MenuID,  PG.PageName  as Name, CONCAT(PG.Solution_Name, '',PG.URL as Url, PG.ParentID,indx,PG.Pagelevel, PG.CssFont   " _
                      & "    FROM  RIT_tluPages_WEB AS PG WHERE ISNULL(ISACTIVE,'FALSE') = 'TRUE' AND isnull(ParentID  ,0) = 0 " _
                      & "    )" _
                              & " UNION " _
                        & " SELECT PG.MenuID,  " _
                        & " PG.PageName  as Name, CONCAT(PG.Solution_Name, '',PG.URL as Url, PG.ParentID,indx,PG.Pagelevel, PG.CssFont      " _
                        & " FROM  RIT_tluPages_WEB AS PG WHERE ISNULL(isfixed,'FALSE') = 'TRUE'  " _
                        & " ) ALL_MN " _
                      & "    where isnull(menuid,0) <> 0  " _
                      & "    order by indx, MenuID "





        getMenuText = strMnu

    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objMod As New modMain
            Dim str_USERNAME As String = ""
            Dim nCCID As Integer = 0
            Dim strDT As String = ""
            lblVER.Text = (modMain.gSTRVER + " " + modMain.gDBNAME)
            Dim STR As String = (Request.QueryString("id").ToString())
            If STR = "Log" Then
                '----------------------------------------------------------
                '**********************************************************
                Try
                    Dim str_UserID_gID As String = ""
                    'Dim nCCID As Integer = 0
                    objMod.getUSER_INFO_GID_WITHOUT_ID(Session("gobjUSER"), nCCID, str_UserID_gID)

                    objMod.mSave_LOG(txtuserid.Text, modMain.eLG_TYP.SAVE_TRANSACTION, _
                                    " User LogOut", _
                                     modMain.eLG_Prt.NORMAL, _
                                     str_UserID_gID, nCCID)
                Catch ex As Exception
                    'ERR_MSG(ex.Message)
                End Try
                '**********************************************************

            End If
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


    Protected Sub NewWindow(ByVal url As String)

        Response.Write(String.Format("<script>window.open('" & url & "','_blank','top=10,left=200,height=800px,width=1200,addressbar=yes,toolbar=no,directories=no,status=yes,scrollbars=yes,menubar=no,resizable=yes'); </script>"))

    End Sub




End Class
