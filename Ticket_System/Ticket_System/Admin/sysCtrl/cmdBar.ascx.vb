
Partial Class cmdBar1
    Inherits System.Web.UI.UserControl
    Public Event mSaveEvent()
    Public Event mPrintEvent()
    Public Event mNewEvent()
    Public Event mHomeEvent()
    Public Event mLogotEvent()
    Public Event mAuth_L1_Event()
    Public Event mAuth_L2_Event()

    Public Event mPDF_Event()
    Public Event mEXCEL_Event()
    Public Event mCANCEL_Event()


    Public isSave As Boolean = True
    Public isPrint As Boolean = True
    Public isNew As Boolean = True
    Public isHome As Boolean = True
    Public isAuth_L1 As Boolean = False
    Public isAuth_L2 As Boolean = False
    Public isPDF As Boolean = False
    Public isEXCEL As Boolean = False
    Public isCancel As Boolean = False



    Protected Sub btnNEW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNEW.Click
        RaiseEvent mNewEvent()
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        RaiseEvent mPrintEvent()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'System.Threading.Thread.Sleep(2000)
        RaiseEvent mSaveEvent()
    End Sub

   
    Protected Sub btnLevel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLevel1.Click
        RaiseEvent mAuth_L1_Event()
    End Sub

    Protected Sub btnLevel2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLevel2.Click
        RaiseEvent mAuth_L2_Event()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            btnSave.Visible = False
            btnPrint.Visible = False
            btnNEW.Visible = False
            btnLevel1.Visible = False
            btnLevel2.Visible = False
            btnPDF.Visible = False
            btnExcel.Visible = False
            btnCancel.Visible = False

            If isSave Then
                btnSave.Visible = True
            End If
            If isPrint Then
                btnPrint.Visible = True
            End If
            If isNew Then
                btnNEW.Visible = True
            End If
            If isAuth_L1 Then
                btnLevel1.Visible = True
            End If
            If isAuth_L2 Then
                btnLevel2.Visible = True
            End If
            If isPDF Then
                btnPDF.Visible = True
            End If
            If isEXCEL Then
                btnExcel.Visible = True
            End If
            If isCancel Then
                btnCancel.Visible = True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Protected Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        RaiseEvent mPDF_Event()
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        RaiseEvent mEXCEL_Event()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        RaiseEvent mCANCEL_Event()
    End Sub

End Class
