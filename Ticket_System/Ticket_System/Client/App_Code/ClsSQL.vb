Imports Microsoft.VisualBasic

Public Class clsSQL

    Private msTableName As String
    Private msql_InsFld As String
    Private msql_UpdVal As String
    Private mlInsCont As Long
    Private msCondition As String
    Private mlCndCont As Long
    Private mlUpdCont As Long
    Private msVals As String
    Private msUpdateFld As String
    Public gUserID, gUserLogin, gUserNAME, gUserPWD, gUserPWD_Rev As String
    Public Enum azDataType
        azTyp_Auto = 0
        azTyp_String = 1
        azTyp_Number = 2
        azTyp_Date = 3
        azTyp_DateTime = 4
        azTyp_boolean = 5
        azTyp_Field = 6
    End Enum

    Public Property TableName() As String
        Get
            Return msTableName
        End Get
        Set(ByVal Value As String)
            msTableName = Value
        End Set
    End Property



    Public Sub adCnd(ByVal pCondition As String)
        mlCndCont = mlCndCont + 1

        If mlCndCont = 1 Then
            msCondition = "(" & pCondition & ")"
        Else
            msCondition = msCondition & " And (" & pCondition & ")"
        End If

    End Sub

    Public Sub AddField(ByVal psField As String, _
                        ByVal psValue As Object, _
                        ByVal pTyp As azDataType, _
                        ByVal pIsPK As Boolean)
        Dim mValStr As String

        Dim lbFld As Boolean
        mValStr = String.Empty
        lbFld = False

        mlInsCont = mlInsCont + 1

        If pTyp = azDataType.azTyp_boolean Then
            mValStr = IIf(psValue = True, "-1", "0")
        ElseIf pTyp = azDataType.azTyp_Date Then

            If Len(psValue) = 0 Then
                mValStr = "NULL"
            Else

                If VarType(psValue) = vbString Then
                    If Trim$(psValue) = "" Then
                        mValStr = "NULL"
                    Else
                        mValStr = "'" & CDate(psValue) & "'"

                    End If

                Else
                    mValStr = "'" & CDate(psValue) & "'"
                End If
            End If

        ElseIf pTyp = azDataType.azTyp_DateTime Then

            If Len(psValue) = 0 Then
                mValStr = "NULL"
            Else

                mValStr = "#" & CDate(psValue) & "#"
            End If

        ElseIf pTyp = azDataType.azTyp_Number Then

            If VarType(psValue) = vbString Then
                mValStr = "" & Val(psValue)
            ElseIf VarType(psValue) = vbBoolean Then
                mValStr = "" & IIf(psValue = True, -1, 0)
            Else
                mValStr = "" & Val(IIf(Len(psValue) = 0, 0, psValue))
            End If

            '*********************
        ElseIf pTyp = azDataType.azTyp_String Then

            If Len(psValue & "") = 0 Then
                mValStr = "NULL"

            Else
                mValStr = "'" & Replace(psValue, "'", "''") & "'"
            End If

        ElseIf pTyp = azDataType.azTyp_Field Then
            lbFld = True
            mValStr = psValue
        End If

        If Not lbFld Then
            If mlInsCont = 1 Then
                msql_InsFld = "(" & psField
                msVals = "(" & mValStr
            Else
                msql_InsFld = msql_InsFld & ", " & psField
                msVals = msVals & ", " & mValStr
            End If
        End If

        If Not pIsPK Then
            mlUpdCont = mlUpdCont + 1

            If mlUpdCont = 1 Then
                msUpdateFld = "SET " & psField & " = " & mValStr
            Else
                msUpdateFld = msUpdateFld & ", " & psField & " = " & mValStr
            End If
        End If

    End Sub

    Public Function sql_Insert() As String

        Dim mSQL As String

        If Len(Trim(msql_InsFld)) = 0 Then
            mSQL = ""
        Else
            mSQL = "INSERT INTO " & msTableName & " " & msql_InsFld & ") VALUES " & msVals & ")"
        End If

        sql_Insert = mSQL

    End Function

    Public Function sql_Update() As String
        Dim mSQL As String

        If Len(Trim(msUpdateFld)) = 0 Then
            mSQL = ""
        Else
            mSQL = "UPDATE " & msTableName & " " & msUpdateFld

            If mlCndCont > 0 Then
                mSQL = mSQL & " WHERE (" & msCondition & ")"
            End If
        End If

        sql_Update = mSQL

    End Function

    Public Function resetIT() As String
        msql_InsFld = ""
        msVals = ""
        msUpdateFld = ""
        msql_UpdVal = ""

        msCondition = ""
        mlCndCont = 0

        mlInsCont = 0
        mlUpdCont = 0
        Return ""
    End Function






End Class
