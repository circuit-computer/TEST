Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Web
 

Imports Microsoft.VisualBasic


Public Class modFunctions
    Public Function isValid_NBR_RATE_SMALL(ByVal x As String, Optional ByVal isRate As Boolean = False) As String

        Try
            If Not IsNumeric(x) Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate_SML, modMain.gFormatAmount))
            End If

            If CDbl(x) < 0 Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate_SML, modMain.gFormatAmount))
            Else
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate_SML, modMain.gFormatAmount))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return x


    End Function

    Public Function isValid_NBR(ByVal x As String, Optional ByVal isRate As Boolean = False) As String

        Try
            If Not IsNumeric(x) Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatAmount))
            End If

            If CDbl(x) < 0 Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatAmount))
            Else
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatAmount))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return x


    End Function


    Public Function isValid_NBR_NO(ByVal x As String, Optional ByVal isRate As Boolean = False) As String

        Try
            If Not IsNumeric(x) Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatNO, modMain.gFormatNO))
            End If

            If CDbl(x) < 0 Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatNO, modMain.gFormatNO))
            Else
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatNO, modMain.gFormatNO))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return x


    End Function

    Public Function isValid_NBR_NEW(ByVal x As String, Optional ByVal isRate As Boolean = False) As String

        Try
            If Not IsNumeric(x) Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatRate_SML))
            End If

            If CDbl(x) < 0 Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatRate_SML))
            Else
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatRate_SML))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return x


    End Function
    Public Function isValid_NBR_BIGRATE_FMT(ByVal x As String, Optional ByVal isRate As Boolean = False) As String

        Try
            If Not IsNumeric(x) Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatAmount))
            End If

            If CDbl(x) < 0 Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatAmount))
            Else
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatAmount))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return x


    End Function
    Public Function isValid_NBR_Risk(ByVal x As String, Optional ByVal isRate As Boolean = False) As String

        Try
            If Not IsNumeric(x) Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRisk, modMain.gFormatRisk))
            End If

            If CDbl(x) < 0 Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRisk, modMain.gFormatRisk))
            Else
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRisk, modMain.gFormatRisk))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return x


    End Function
    Public Sub str_Replace_unwanted(ByRef str_X As String)
        Try
            str_X = Replace(str_X, "#", "")
            str_X = Replace(str_X, "%", "")
            str_X = Replace(str_X, ";", "")
            str_X = Replace(str_X, "'", "")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function isValid_NBR_ROUND(ByVal x As String) As String
        Try
            If Not IsNumeric(x) Then
                x = "0"
            End If
            If CDbl(x) < 0 Then
                x = "0"
            Else
                x = Format(CDbl(x), "######")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return x
    End Function

    Public Function isValid_TEXT_A_Z_0_9(ByVal x As String) As String
        Try
            If Len(Trim(x)) = 0 Then
                x = ""
            End If

            If Regex.IsMatch(x, "^[a-zA-Z0-9\s.\?\,\'\;\:\-]+$") Then
                'ERR_MSG("Ok Fine!")
            Else
                'ERR_MSG("Special Characters are not allowed")
                x = ""
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
        Return x
    End Function

    Public Function isValid_TEXT_A_Z_0_9_EID(ByVal x As String) As String
        Try
            If Len(Trim(x)) = 0 Then
                x = ""
            End If

            If Regex.IsMatch(x, "^[a-zA-Z0-9\s.\?\,\'\;\:]+$") Then
                'ERR_MSG("Ok Fine!")
            Else
                'ERR_MSG("Special Characters are not allowed")
                x = ""
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
        Return x
    End Function

    Public Function isValid_TEXT_0_9(ByVal x As String) As String
        Try
            If Len(Trim(x)) = 0 Then
                x = ""
            End If

            If Regex.IsMatch(x, "^[0-9\s.\?\,\'\;\:\!\-]+$") Then
                'ERR_MSG("Ok Fine!")
            Else
                'ERR_MSG("Special Characters are not allowed")
                x = ""
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
        Return x
    End Function

     

    Public Function isValid_NBR_PRCENTAGE(ByVal x As String, Optional ByVal isRate As Boolean = False, Optional ByRef lblERR As String = "") As String
        Try
            If Not IsNumeric(x) Then
                x = "0.00"
            End If
            If x > 100 Then
                x = "0.00"
            End If
            x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatAmount))
        Catch ex As Exception

            lblERR = ex.Message
        End Try
        Return x


    End Function




    Public Function isValid_NBR_PRCENTAGE_BIGRATE(ByVal x As String, Optional ByVal isRate As Boolean = False, Optional ByRef lblERR As String = "") As String
        Try
            If Not IsNumeric(x) Then
                x = "0.00"
            End If
            If x > 100 Then
                x = "0.00"
            End If
            x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatAmount))
        Catch ex As Exception

            lblERR = ex.Message
        End Try
        Return x


    End Function



    Public Function isValid_RateRange(ByVal strRateRange As String, ByVal nRate As Double, ByRef lblERR As String) As Boolean

        Dim objFun As New modFunctions
        Try
            Dim nMinRate As Double = CDbl(Mid(strRateRange, 1, (InStr(strRateRange, "-") - 1)))
            Dim nMaxRate As Double = CDbl(Mid(strRateRange, (InStr(strRateRange, "-") + 1)))
            lblERR = ""
            If nRate < nMinRate Then
                lblERR = "Invalid Rate " & Format(nRate, modMain.gFormatRate) & ".   IT  Should Be Within Defined Rate Range ..... " & "        سعر العملة ليست في نطاق سعر محدد "
            End If
            If nRate > nMaxRate Then
                lblERR = "Invalid Rate " & Format(nRate, modMain.gFormatRate) & ".   IT Should Be Within Defined Rate Range ..... " & "        سعر العملة ليست في نطاق سعر محدد "
            End If

            If Len(Trim(lblERR)) = 0 Then
                Return True
            End If
            If Len(Trim(lblERR)) <> 0 Then
                Return False
            End If

        Catch ex As Exception
            lblERR = ex.Message
        End Try



    End Function


    Public Sub get_CCY_RATE_WEB_TT_COMM(ByVal strSql As String, ByVal dtTb As DataTable, _
                            ByRef nRate As Double, ByRef strRate As String, ByRef strERR As String)



        Try
            Dim i As Integer = 0
            Dim trows As DataRow() = dtTb.Select(strSql)
            For Each tbRow As DataRow In trows
                strRate = Format(tbRow("SALE_MIN"), modMain.gFormatRate) + "-" + Format(tbRow("SALE_MAX"), modMain.gFormatRate)
                nRate = tbRow("SALE_MAX")
                Exit For
            Next

        Catch ex As Exception
            strERR = (ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing
        End Try


    End Sub

    Public Function isValid_NBR_NEGATIVE(ByVal x As String, Optional ByVal isRate As Boolean = False) As String

        Try
            If Not IsNumeric(x) Then
                x = "0"
                x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatAmount))
                Return x
                Exit Function
            End If
            x = Format(CDbl(x), IIf(isRate, modMain.gFormatRate, modMain.gFormatAmount))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return x


    End Function

    'Public Sub getRATE_CCY_PARTY(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
    '        ByRef dtTb As DataTable)
    '    Dim strCCY As String = ""
    '    Dim StrCCY_EX As String = ""
    '    Try
    '        Dim i As Integer = 0
    '        cmbBox.Items.Clear()
    '        If Not isBaseCurrency_needed Then
    '            strSql = " isBaseCurrency = 'FALSE' "
    '        End If

    '        Dim trows As DataRow() = dtTb.Select(strSql)
    '        cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
    '        For Each tbRow As DataRow In trows
    '            strTitle = tbRow("CurrencyShortName") & "   - " & tbRow("CurrencyName")
    '            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(strTitle, tbRow("CurrencyID")))
    '        Next

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try

    'End Sub

    Public Sub get_COA_CCY_PARTY_RORI(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
              ByVal dtTb As DataTable, ByRef strCCY As String, Optional ByVal isRO As Boolean = True)


        Dim StrCCY_EX As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()

            Dim strSql As String = ""

            If isRO Then
                strSql = " CCY = '" & strCCY & "' AND isRO = 'TRUE' AND isACTIVE = 'TRUE'   "
            End If

            If Not isRO Then
                strSql = " CCY = '" & strCCY & "' AND isRI = 'TRUE' AND isACTIVE = 'TRUE'  "
            End If



            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(modMain.gStr_Empty, "9999"))
            For Each tbRow As DataRow In trows
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(tbRow("ACC"), i))
                i += 1
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing
        End Try


    End Sub

    Public Sub get_COA_CCY_PARTY_RO_NEW(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
          ByVal dtTb As DataTable, ByRef strCCY As String, Optional ByVal isRO As Boolean = True)


        Dim StrCCY_EX As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()

            Dim strSql As String = ""

            If isRO Then
                strSql = " CCY = '" & strCCY & "' AND isACTIVE = 'TRUE'   "
            End If

            If Not isRO Then
                strSql = " CCY = '" & strCCY & "' AND isACTIVE = 'TRUE'  "
            End If



            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(modMain.gStr_Empty, "9999"))
            For Each tbRow As DataRow In trows
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(tbRow("ACC"), i))
                i += 1
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing
        End Try


    End Sub
    Public Sub get_COA_CCY_PARTY_WEB_COMMER(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
            ByVal dtTb As DataTable, ByRef strCCY As String, Optional ByVal isRO As Boolean = True)


        Dim StrCCY_EX As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()

            Dim strSql As String = ""

            If isRO Then
                strSql = " CCY = '" & strCCY & "' AND dtcd=26600  "
            End If

            If Not isRO Then
                strSql = " CCY = '" & strCCY & "' AND dtcd=26600 "
            End If



            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(modMain.gStr_Empty, "9999"))
            For Each tbRow As DataRow In trows
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(tbRow("ACC"), i))
                i += 1
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing
        End Try


    End Sub
    Public Sub get_COA_CCY_PARTY_CMB_RORI(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
                ByVal dtTb As DataTable)


        Dim StrCCY_EX As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()

            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(modMain.gStr_Empty, "9999"))

            Dim trows As DataRow() = dtTb.Select(" CCY <> 'ABC' ", "ACC ASC")

            '---------------------------------------------------
            Dim strX As String = ""

            For Each tbRow As DataRow In trows
                If strX <> tbRow("ACC") Then
                    cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(tbRow("ACC"), i))
                    strX = tbRow("ACC")
                    i += 1
                End If
            Next
            '---------------------------------------------------

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing
        End Try

    End Sub

    Public Sub get_COA_CCY_PARTY_RATE_SALE(ByVal strSql As String, ByVal dtTb As DataTable, _
                                 ByRef nRate As Double, ByRef strRate As String, ByRef strERR As String)



        Try
            Dim i As Integer = 0
            Dim trows As DataRow() = dtTb.Select(strSql)
            For Each tbRow As DataRow In trows
                strRate = Format(tbRow("SALE_MIN"), modMain.gFormatRate) + "-" + Format(tbRow("SALE_MAX"), modMain.gFormatRate)
                nRate = tbRow("SALE_MAX")
            Next

        Catch ex As Exception
            strERR = (ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing
        End Try


    End Sub

    Public Sub get_COA_CCY_PARTY_RATE_ROTT(ByVal strSql As String, ByVal dtTb As DataTable, _
                                 ByRef nRate As Double, ByRef strRate As String, _
                                ByRef strDisplayRate As String, _
                                ByRef isMultiply As Boolean)



        Try
            Dim i As Integer = 0
            Dim trows As DataRow() = dtTb.Select(strSql)
            For Each tbRow As DataRow In trows
                strRate = Format(tbRow("SALE_MIN"), modMain.gFormatRate) + "-" + Format(tbRow("SALE_MAX"), modMain.gFormatRate)

                nRate = tbRow("SALE_MIN")
                If tbRow("ISMULTIPLY") Then
                    strDisplayRate = 1 / nRate
                Else
                    strDisplayRate = nRate
                End If
                isMultiply = tbRow("ISMULTIPLY")

            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing
        End Try


    End Sub
    Public Sub get_CCY_RATE_DrNOTE(ByVal strSql As String, ByVal dtTb As DataTable, _
                               ByRef nRate As Double, ByRef strRate As String, ByRef strERR As String)



        Try
            Dim i As Integer = 0
            Dim trows As DataRow() = dtTb.Select(strSql)
            For Each tbRow As DataRow In trows
                strRate = Format(tbRow("SALE_MIN"), modMain.gFormatRate) + "-" + Format(tbRow("SALE_MAX"), modMain.gFormatRate)
                nRate = tbRow("SALE_MAX")
            Next

        Catch ex As Exception
            strERR = (ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing
        End Try


    End Sub

End Class

