Imports Microsoft.VisualBasic

Public Class clsPara
    Public Enum ColType
        Varchar = 1
        NVarchar = 2
        Int = 3
        float = 4
        dbl = 5
        dt = 6
        bigINT = 7
        BIT = 8
    End Enum

    Private Name As String = String.Empty
    Private Type As String = String.Empty
    Private SIZE As String = String.Empty
    Private VAL As String = String.Empty

    Public mVarColType As ColType


    Public Property prpName() As String
        Get
            Return Name
        End Get
        Set(ByVal value As String)
            Name = value
        End Set
    End Property

    Public Property prpType() As String
        Get
            Return Type
        End Get
        Set(ByVal value As String)
            Type = value
        End Set
    End Property

    Public Property prpSIZE() As String
        Get
            Return SIZE
        End Get
        Set(ByVal value As String)
            SIZE = value
        End Set
    End Property

    Public Property prpVAL() As String
        Get
            Return VAL
        End Get
        Set(ByVal value As String)
            VAL = value
        End Set
    End Property

End Class
