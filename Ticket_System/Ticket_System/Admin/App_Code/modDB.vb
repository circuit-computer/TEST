
Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Web

Imports Microsoft.VisualBasic



Public Class modDB




    Public Sub SAVE_CUST_MASTER_Fun(ByRef loComm As SqlCommand, ByVal iCustID As Long, _
                       ByVal iCCID As Integer, _
                            ByVal sCustID_GID_update As String, _
                       ByVal sCustName_Arb As String, _
                         ByVal sCustName_First As String, _
                       ByVal sCustName_Middle As String, _
                       ByVal sCustName_Last As String, _
                       ByVal iNat_CountryID As Integer, _
                       ByVal iCountryID As Integer, _
                       ByVal IsActive As Boolean, _
                       ByVal idtcd_Ref As Integer, _
                        ByVal isbcd_Ref As Integer, _
         ByVal sCustomerType As String, _
         ByVal str_user_id_gid As String, _
         ByVal isUpdate As Boolean, _
                                       ByRef str_CustID As String, _
                         ByRef str_CustID_GID As String, _
                       ByRef str_ErrorCode As String, _
                       Optional ByVal emi_ID As Integer = 0, _
                       Optional ByVal custTypeid As Integer = 0, _
                        Optional isFWD As Boolean = 0, _
                        Optional aux_b_1 As Boolean = 0, _
                        Optional aux_b_2 As Boolean = 0, _
                        Optional aux_b_3 As Boolean = 0, _
                        Optional aux_b_4 As Boolean = 0, _
                        Optional aux_1 As String = "", _
                        Optional aux_2 As String = "", _
                        Optional aux_3 As String = "", _
                        Optional aux_4 As String = "", _
                        Optional aux_5 As String = "", _
                        Optional EC_CODE As String = "", _
                        Optional EC_Account_Classification As String = "" _
                    )


        Dim m_iCustID As SqlInt64
        Dim m_iErrorCode As SqlInt32
        'sCustName_Arb = UCase(sCustName_Arb)
        sCustName_First = UCase(sCustName_First)
        sCustName_Middle = UCase(sCustName_Middle)
        sCustName_Last = UCase(sCustName_Last)
        sCustomerType = UCase(sCustomerType)
        loComm.Parameters.Clear()


        With loComm.Parameters

            loComm.CommandText = "dbo.[stp_tblCust_save_up]"
            loComm.CommandType = CommandType.StoredProcedure

            If isUpdate = False Then
                loComm.Parameters.Add(New SqlParameter("@CustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                loComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, iCCID))
                loComm.Parameters.Add(New SqlParameter("@CustID_GID_update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
            Else
                loComm.Parameters.Add(New SqlParameter("@CustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustID))
                loComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, iCCID))
                loComm.Parameters.Add(New SqlParameter("@CustID_GID_update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID_update))
            End If

            loComm.Parameters.Add(New SqlParameter("@CustName_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustName_Arb))

            loComm.Parameters.Add(New SqlParameter("@CustName_First", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustName_First))
            loComm.Parameters.Add(New SqlParameter("@CustName_Middle", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustName_Middle))
            loComm.Parameters.Add(New SqlParameter("@CustName_Last", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustName_Last))

            loComm.Parameters.Add(New SqlParameter("@Nat_CountryID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, iNat_CountryID))


            loComm.Parameters.Add(New SqlParameter("@CountryID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, System.DBNull.Value))
            loComm.Parameters.Add(New SqlParameter("@IsActive", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, IsActive))
            loComm.Parameters.Add(New SqlParameter("@dtcd_Ref", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, idtcd_Ref))
            loComm.Parameters.Add(New SqlParameter("@sbcd_Ref", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, isbcd_Ref))

            loComm.Parameters.Add(New SqlParameter("@CustomerType", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustomerType))


            loComm.Parameters.Add(New SqlParameter("@iCustID_Ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID))
            loComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_CustID_GID))

            loComm.Parameters.Add(New SqlParameter("@user_id_gid", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_user_id_gid))

            loComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iErrorCode))

            If isUpdate = False Then
                loComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            Else
                loComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            End If

            loComm.Parameters.Add(New SqlParameter("@emi_ID", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, emi_ID))


            loComm.Parameters.Add(New SqlParameter("@aux_1", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_1))
            loComm.Parameters.Add(New SqlParameter("@aux_2", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_2))
            loComm.Parameters.Add(New SqlParameter("@aux_3", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_3))
            loComm.Parameters.Add(New SqlParameter("@aux_4", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_4))
            loComm.Parameters.Add(New SqlParameter("@aux_5", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_5))


            loComm.Parameters.Add(New SqlParameter("@isFWD", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, isFWD))
            loComm.Parameters.Add(New SqlParameter("@aux_b_1", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, aux_b_1))
            loComm.Parameters.Add(New SqlParameter("@aux_b_2", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, aux_b_2))
            loComm.Parameters.Add(New SqlParameter("@aux_b_3", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, aux_b_3))
            loComm.Parameters.Add(New SqlParameter("@aux_b_4", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, aux_b_4))




            loComm.Parameters.Add(New SqlParameter("@EC_CODE", SqlDbType.NVarChar, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, EC_CODE))
            loComm.Parameters.Add(New SqlParameter("@EC_Account_Classification", SqlDbType.NVarChar, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, EC_Account_Classification))


            loComm.Parameters.Add(New SqlParameter("@custTypeid", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, custTypeid))




        End With
        loComm.ExecuteNonQuery()

        Try
            If isUpdate = False Then
                m_iCustID = (loComm.Parameters.Item("@iCustID_Ot").Value)
                str_CustID = CStr(m_iCustID.Value.ToString)
                str_CustID_GID = CStr(loComm.Parameters.Item("@CustID_GID").Value)
                'Else
                '    m_iCustID = New SqlInt64(CType(txtCustomerID.Text, SqlInt16))
                '    m_iCustID_STR = CStr(m_iCustID.Value.ToString)
                '    m_iCustID_STR = txtCustomerID.Text
            End If


            m_iErrorCode = New SqlInt32(CType(loComm.Parameters.Item("@iErrorCode").Value, SqlInt32))
            str_ErrorCode = CStr(m_iErrorCode.Value.ToString)

        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator...")

        End Try

    End Sub
    Public Sub mSave_Cust_detail(ByRef loComm As SqlCommand, ByVal iCustid_Details As Integer, _
                       ByVal sCustID_Details_GID_Update As String, _
                            ByVal iCustid As String, _
                       ByVal sCustID_GID As String, _
                         ByVal sPlaceOfIssue As String, _
                       ByVal iIDTypeID As Integer, _
                       ByVal sIDNumber As String, _
                       ByVal dDateOfIssue As Date, _
                       ByVal dExpiryDate As Date, _
                       ByVal sAddress1 As String, _
                       ByVal sAddress2 As String, _
                       ByVal sCity As String, _
                       ByVal sTel As String, _
                       ByVal sFax As String, _
                       ByVal sZIP As String, _
                       ByVal sEmail As String, _
                       ByVal sSponserName As String, _
                          ByVal sDetails As String, _
                       ByVal dmDate As Date, _
                       ByVal dDateof_Birth As Date, _
                        ByVal iUserID As Integer, _
                       ByVal BankAccNumber As String, _
           ByVal BankName As String, _
           ByVal BranchName As String, _
              ByVal isUpdate As Boolean, _
                        ByRef iCustid_Details_ot As String, _
                         ByRef sCustID_Details_GID As String, _
                       ByRef iErrorCode As String, _
                       ByRef USERID_GID As String)


        Dim m_iCustID_DET As SqlInt64
        sIDNumber = UCase(sIDNumber)
        sAddress1 = UCase(sAddress1)
        sAddress2 = UCase(sAddress2)
        sCity = UCase(sCity)
        sTel = UCase(sTel)
        sFax = UCase(sFax)
        sZIP = UCase(sZIP)
        sEmail = UCase(sEmail)
        sSponserName = UCase(sSponserName)
        BankAccNumber = UCase(BankAccNumber)
        BankName = UCase(BankName)
        BranchName = UCase(BranchName)
        sDetails = UCase(sDetails)
        sPlaceOfIssue = UCase(sPlaceOfIssue)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                loComm.CommandText = "dbo.[stp_tblCust_detail_InsertUpdate_GID_NW]"
                loComm.CommandType = CommandType.StoredProcedure
                If isUpdate = False Then


                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                Else

                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_Details_GID_Update))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))


                End If



                .Add(New SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sPlaceOfIssue & ""))


                .Add(New SqlParameter("@IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iIDTypeID))



                .Add(New SqlParameter("@IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sIDNumber))
                .Add(New SqlParameter("@DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateOfIssue))
                .Add(New SqlParameter("@ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dExpiryDate))

                .Add(New SqlParameter("@Address1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress1))
                .Add(New SqlParameter("@Address2", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress2))

                .Add(New SqlParameter("@City", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCity))
                .Add(New SqlParameter("@Tel", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sTel))
                .Add(New SqlParameter("@Fax", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sFax))
                .Add(New SqlParameter("@ZIP", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sZIP))
                .Add(New SqlParameter("@Email", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sEmail))

                .Add(New SqlParameter("@SponserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sSponserName))
                .Add(New SqlParameter("@Details", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sDetails))


                .Add(New SqlParameter("@mDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, dmDate))
                .Add(New SqlParameter("@Dateof_Birth", SqlDbType.DateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateof_Birth))

                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))


                .Add(New SqlParameter("@BankAccNumber", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankAccNumber))
                .Add(New SqlParameter("@BankName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankName))
                .Add(New SqlParameter("@BranchName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BranchName))

                .Add(New SqlParameter("@USERID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, USERID_GID))

                .Add(New SqlParameter("@iCustid_Details_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID_DET))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, iErrorCode))
                .Add(New SqlParameter("@CustID_Details_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, sCustID_Details_GID))
                'bIsUpdate = Convert.ToBoolean(btstatus.Text)


                If Not isUpdate Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If


            End With

            loComm.ExecuteNonQuery()

            If Not isUpdate Then
                m_iCustID_DET = (loComm.Parameters.Item("@iCustid_Details_ot").Value)
                iCustid_Details_ot = CStr(m_iCustID_DET.Value.ToString)
                sCustID_Details_GID = CStr(loComm.Parameters.Item("@CustID_Details_GID").Value)
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub SAVE_CUST_detail_REP_RISK_PROFILE_3(ByRef loComm As SqlCommand, ByVal sRep_ID_GID_UPDATE As String, _
                              ByVal lCustid As Long, _
                              ByVal sCustid_GID As String, _
                              ByVal sRepName As String, _
                              ByVal sRep_Name_Arb As String, _
                              ByVal iRep_IDTypeID As Integer, _
                              ByVal sRep_IDNumber As String, _
                              ByVal dRep_DateOfIssue As Date, _
                              ByVal dRep_ExpiryDate As Date, _
                              ByVal iUserID As Integer, _
                              ByVal IsUpdate As Boolean, _
                              ByRef str_Rep_ID_ot As String, _
                              ByRef str_Rep_ID_GID_ot As String, _
                              ByRef str_ErrorCode As String, _
                              ByRef str_Rep_CELL As String, _
                              ByRef str_Rep_IdPlaceOfIssue As String, _
                              ByRef str_Rep_Nationality As String, _
                              ByRef nDTCD As String, ByRef nSBCD As Integer, _
                              ByRef Rep_DOB As Date, _
                              ByRef Rep_ADD As String, _
                              ByRef Rep_Email As String, _
                               ByRef Rep_ID As String, _
                                ByRef Rep_ID_GID As String, _
                              ByRef Rep_Remarks As String, ByRef Remarks As String, ByRef userid_gid As String, ByVal risk_score As Double, _
                                   ByRef Risk_String As String, _
                ByRef bln_complinace_clear As Boolean, _
                ByRef str_complinace_clear_by As String, _
                ByRef str_complinacel_clear_details As String, ByVal risk_Name_1 As String,
                ByVal risk_Nationality_2 As String,
                ByVal risk_Address_3 As String,
                ByVal risk_POI_4 As String,
                ByVal risk_aux_5 As String,
                ByVal risk_aux_6 As String,
                ByVal risk_aux_7 As String,
                ByVal risk_aux_8 As String)
        Try




            sRepName = UCase(sRepName)
            'UCase(sRep_Name_Arb)
            sRep_IDNumber = UCase(sRep_IDNumber)


            loComm.Parameters.Clear()


            With loComm.Parameters

                loComm.CommandText = "dbo.[stp_tblCust_Rep_InsertUpdate_GID_new_3]"
                loComm.CommandType = CommandType.StoredProcedure


                If IsUpdate = False Then
                    .Add(New SqlParameter("@Rep_ID_GID_UPDATE", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                Else
                    .Add(New SqlParameter("@Rep_ID_GID_UPDATE", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_ID_GID_UPDATE))
                End If

                .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, lCustid))
                .Add(New SqlParameter("@Custid_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustid_GID))
                .Add(New SqlParameter("@RepName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRepName))
                .Add(New SqlParameter("@Rep_Name_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_Name_Arb))



                .Add(New SqlParameter("@Rep_IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iRep_IDTypeID))

                .Add(New SqlParameter("@Rep_IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_IDNumber))

                .Add(New SqlParameter("@Rep_DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dRep_DateOfIssue))
                .Add(New SqlParameter("@Rep_ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dRep_ExpiryDate))





                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))

                If IsUpdate = False Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
                    False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
                    False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If

                .Add(New SqlParameter("@Rep_ID_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_ID_ot))
                .Add(New SqlParameter("@Rep_ID_GID_ot", SqlDbType.NVarChar, 50, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_Rep_ID_GID_ot))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_ErrorCode))




                .Add(New SqlParameter("@Rep_CELL", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Rep_CELL))
                .Add(New SqlParameter("@Rep_IdPlaceOfIssue", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_IdPlaceOfIssue))
                .Add(New SqlParameter("@Rep_Nationality", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_Nationality))

                .Add(New SqlParameter("@DTCD", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, nDTCD))
                .Add(New SqlParameter("@SBCD", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, nSBCD))



                .Add(New SqlParameter("@Rep_DOB", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, Rep_DOB))
                .Add(New SqlParameter("@Rep_ADD", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_ADD))
                .Add(New SqlParameter("@Rep_Email", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Email))
                .Add(New SqlParameter("@Rep_Remarks", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Remarks))
                .Add(New SqlParameter("@Remarks", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Remarks))
                .Add(New SqlParameter("@user_id_gid", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, userid_gid))

                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 4000, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))



                .Add(New SqlParameter("@risk_Name_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Name_1))
                .Add(New SqlParameter("@risk_Nationality_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Nationality_2))
                .Add(New SqlParameter("@risk_Address_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Address_3))
                .Add(New SqlParameter("@risk_POI_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_POI_4))
                .Add(New SqlParameter("@risk_aux_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_5))
                .Add(New SqlParameter("@risk_aux_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_6))
                .Add(New SqlParameter("@risk_aux_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_7))
                .Add(New SqlParameter("@risk_aux_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_8))


            End With
            loComm.ExecuteNonQuery()
            Dim nrep_id As Integer = 0
            If IsUpdate = False Then

                Rep_ID = (loComm.Parameters.Item("@Rep_ID_ot").Value)
                Rep_ID_GID = CStr(loComm.Parameters.Item("@Rep_ID_GID_ot").Value)

            End If

        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator...")

        End Try


    End Sub

    Public Sub mSave_Cust_detail_new(ByRef loComm As SqlCommand, ByVal iCustid_Details As Integer, _
                     ByVal sCustID_Details_GID_Update As String, _
                          ByVal iCustid As String, _
                     ByVal sCustID_GID As String, _
                       ByVal sPlaceOfIssue As String, _
                     ByVal iIDTypeID As Integer, _
                     ByVal sIDNumber As String, _
                     ByVal dDateOfIssue As Date, _
                     ByVal dExpiryDate As Date, _
                     ByVal sAddress1 As String, _
                     ByVal sAddress2 As String, _
                     ByVal sCity As String, _
                     ByVal sTel As String, _
                     ByVal sFax As String, _
                     ByVal sZIP As String, _
                     ByVal sEmail As String, _
                     ByVal sSponserName As String, _
                        ByVal sDetails As String, _
                     ByVal dmDate As Date, _
                     ByVal dDateof_Birth As Date, _
                      ByVal iUserID As Integer, _
                     ByVal BankAccNumber As String, _
         ByVal BankName As String, _
         ByVal BranchName As String, _
          ByVal visa_id As String, _
            ByVal isUpdate As Boolean, _
                      ByRef iCustid_Details_ot As String, _
                       ByRef sCustID_Details_GID As String, _
                     ByRef iErrorCode As String, _
                     ByRef USERID_GID As String)


        Dim m_iCustID_DET As SqlInt64
        sIDNumber = UCase(sIDNumber)
        sAddress1 = UCase(sAddress1)
        sAddress2 = UCase(sAddress2)
        sCity = UCase(sCity)
        sTel = UCase(sTel)
        sFax = UCase(sFax)
        sZIP = UCase(sZIP)
        sEmail = UCase(sEmail)
        sSponserName = UCase(sSponserName)
        BankAccNumber = UCase(BankAccNumber)
        BankName = UCase(BankName)
        BranchName = UCase(BranchName)
        sDetails = UCase(sDetails)
        sPlaceOfIssue = UCase(sPlaceOfIssue)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident"
                loComm.CommandType = CommandType.StoredProcedure
                If isUpdate = False Then


                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                Else

                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_Details_GID_Update))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))


                End If



                .Add(New SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sPlaceOfIssue & ""))


                .Add(New SqlParameter("@IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iIDTypeID))



                .Add(New SqlParameter("@IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sIDNumber))
                .Add(New SqlParameter("@DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateOfIssue))
                .Add(New SqlParameter("@ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dExpiryDate))

                .Add(New SqlParameter("@Address1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress1))
                .Add(New SqlParameter("@Address2", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress2))

                .Add(New SqlParameter("@City", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCity))
                .Add(New SqlParameter("@Tel", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sTel))
                .Add(New SqlParameter("@Fax", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sFax))
                .Add(New SqlParameter("@ZIP", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sZIP))
                .Add(New SqlParameter("@Email", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sEmail))

                .Add(New SqlParameter("@SponserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sSponserName))
                .Add(New SqlParameter("@Details", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sDetails))


                .Add(New SqlParameter("@mDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, dmDate))
                .Add(New SqlParameter("@Dateof_Birth", SqlDbType.DateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateof_Birth))

                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))


                .Add(New SqlParameter("@BankAccNumber", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankAccNumber))
                .Add(New SqlParameter("@BankName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankName))
                .Add(New SqlParameter("@BranchName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BranchName))

                .Add(New SqlParameter("@USERID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, USERID_GID))
                .Add(New SqlParameter("@visa_id", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, visa_id))
                .Add(New SqlParameter("@iCustid_Details_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID_DET))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, iErrorCode))
                .Add(New SqlParameter("@CustID_Details_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, sCustID_Details_GID))
                'bIsUpdate = Convert.ToBoolean(btstatus.Text)


                If Not isUpdate Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If


            End With

            loComm.ExecuteNonQuery()

            If Not isUpdate Then
                m_iCustID_DET = (loComm.Parameters.Item("@iCustid_Details_ot").Value)
                iCustid_Details_ot = CStr(m_iCustID_DET.Value.ToString)
                sCustID_Details_GID = CStr(loComm.Parameters.Item("@CustID_Details_GID").Value)
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub mSave_Cust_detail_RiskProfile(ByRef loComm As SqlCommand, ByVal iCustid_Details As Integer, _
               ByVal sCustID_Details_GID_Update As String, _
                    ByVal iCustid As String, _
               ByVal sCustID_GID As String, _
                 ByVal sPlaceOfIssue As String, _
               ByVal iIDTypeID As Integer, _
               ByVal sIDNumber As String, _
               ByVal dDateOfIssue As Date, _
               ByVal dExpiryDate As Date, _
               ByVal sAddress1 As String, _
               ByVal sAddress2 As String, _
               ByVal sCity As String, _
               ByVal sTel As String, _
               ByVal sFax As String, _
               ByVal sZIP As String, _
               ByVal sEmail As String, _
               ByVal sSponserName As String, _
                  ByVal sDetails As String, _
               ByVal dmDate As Date, _
               ByVal dDateof_Birth As Date, _
                ByVal iUserID As Integer, _
               ByVal BankAccNumber As String, _
               ByVal BankName As String, _
               ByVal BranchName As String, _
               ByVal visa_id As String, _
               ByVal CustTypeID_Child As String, _
               ByVal CustTypeID_Child_SUB As String, _
               ByVal Rep_Type As String, _
               ByVal Volume_day_fx As Double, _
               ByVal Value_day_fx As Double, _
               ByVal Volume_Month_fx As Double, _
               ByVal Value_Month_fx As Double, _
               ByVal Volume_day_remittance As Double, _
               ByVal Value_day_remittance As Double, _
               ByVal Volume_Month_Remittance As Double, _
               ByVal Value_Month_Remittance As Double, _
               ByVal details_1 As String, _
               ByVal details_2 As String, _
               ByVal risk_score As Double, _
               ByVal isUpdate As Boolean, _
               ByRef iCustid_Details_ot As String, _
               ByRef sCustID_Details_GID As String, _
               ByRef iErrorCode As String, _
               ByRef USERID_GID As String, _
               ByRef Risk_String As String, _
               ByRef bln_complinace_clear As Boolean, _
               ByRef str_complinace_clear_by As String, _
               ByRef str_complinacel_clear_details As String)





        Dim m_iCustID_DET As SqlInt64
        sIDNumber = UCase(sIDNumber)
        sAddress1 = UCase(sAddress1)
        sAddress2 = UCase(sAddress2)
        sCity = UCase(sCity)
        sTel = UCase(sTel)
        sFax = UCase(sFax)
        sZIP = UCase(sZIP)
        sEmail = UCase(sEmail)
        sSponserName = UCase(sSponserName)
        BankAccNumber = UCase(BankAccNumber)
        BankName = UCase(BankName)
        BranchName = UCase(BranchName)
        sDetails = UCase(sDetails)
        sPlaceOfIssue = UCase(sPlaceOfIssue)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                ' loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident_risk_profile"

                loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk"
                loComm.CommandType = CommandType.StoredProcedure
                If isUpdate = False Then


                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                Else

                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_Details_GID_Update))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))


                End If



                .Add(New SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sPlaceOfIssue & ""))


                .Add(New SqlParameter("@IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iIDTypeID))



                .Add(New SqlParameter("@IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sIDNumber))
                .Add(New SqlParameter("@DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateOfIssue))
                .Add(New SqlParameter("@ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dExpiryDate))

                .Add(New SqlParameter("@Address1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress1))
                .Add(New SqlParameter("@Address2", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress2))

                .Add(New SqlParameter("@City", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCity))
                .Add(New SqlParameter("@Tel", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sTel))
                .Add(New SqlParameter("@Fax", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sFax))
                .Add(New SqlParameter("@ZIP", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sZIP))
                .Add(New SqlParameter("@Email", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sEmail))

                .Add(New SqlParameter("@SponserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sSponserName))
                .Add(New SqlParameter("@Details", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sDetails))


                .Add(New SqlParameter("@mDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, dmDate))
                .Add(New SqlParameter("@Dateof_Birth", SqlDbType.DateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateof_Birth))

                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))


                .Add(New SqlParameter("@BankAccNumber", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankAccNumber))
                .Add(New SqlParameter("@BankName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankName))
                .Add(New SqlParameter("@BranchName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BranchName))

                .Add(New SqlParameter("@USERID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, USERID_GID))
                .Add(New SqlParameter("@visa_id", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, visa_id))

                .Add(New SqlParameter("@CustTypeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child))
                .Add(New SqlParameter("@CustTypeID_Child_SUB", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child_SUB))
                .Add(New SqlParameter("@Rep_Type", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Type))
                .Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
                .Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
                .Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
                .Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

                .Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
                .Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
                .Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
                .Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))
                .Add(New SqlParameter("@details_1", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_1))
                .Add(New SqlParameter("@details_2", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_2))
                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@iCustid_Details_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID_DET))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, iErrorCode))
                .Add(New SqlParameter("@CustID_Details_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, sCustID_Details_GID))
                'bIsUpdate = Convert.ToBoolean(btstatus.Text)

                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))

                If Not isUpdate Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If


            End With

            loComm.ExecuteNonQuery()

            If Not isUpdate Then
                m_iCustID_DET = (loComm.Parameters.Item("@iCustid_Details_ot").Value)
                iCustid_Details_ot = CStr(m_iCustID_DET.Value.ToString)
                sCustID_Details_GID = CStr(loComm.Parameters.Item("@CustID_Details_GID").Value)
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Public Sub mSave_Cust_detail_RiskProfile_Profiling(ByRef loComm As SqlCommand, ByVal iCustid_Details As Integer, _
                  ByVal sCustID_Details_GID_Update As String, _
                       ByVal iCustid As String, _
                  ByVal sCustID_GID As String, _
                    ByVal sPlaceOfIssue As String, _
                  ByVal iIDTypeID As Integer, _
                  ByVal sIDNumber As String, _
                  ByVal dDateOfIssue As Date, _
                  ByVal dExpiryDate As Date, _
                  ByVal sAddress1 As String, _
                  ByVal sAddress2 As String, _
                  ByVal sCity As String, _
                  ByVal sTel As String, _
                  ByVal sFax As String, _
                  ByVal sZIP As String, _
                  ByVal sEmail As String, _
                  ByVal sSponserName As String, _
                     ByVal sDetails As String, _
                  ByVal dmDate As Date, _
                  ByVal dDateof_Birth As Date, _
                   ByVal iUserID As Integer, _
                  ByVal BankAccNumber As String, _
                  ByVal BankName As String, _
                  ByVal BranchName As String, _
                  ByVal visa_id As String, _
                  ByVal CustTypeID_Child As String, _
                  ByVal CustTypeID_Child_SUB As String, _
                  ByVal Rep_Type As String, _
                  ByVal Volume_day_fx As Double, _
                  ByVal Value_day_fx As Double, _
                  ByVal Volume_Month_fx As Double, _
                  ByVal Value_Month_fx As Double, _
                  ByVal Volume_day_remittance As Double, _
                  ByVal Value_day_remittance As Double, _
                  ByVal Volume_Month_Remittance As Double, _
                  ByVal Value_Month_Remittance As Double, _
                  ByVal details_1 As String, _
                  ByVal details_2 As String, _
                  ByVal risk_score As Double, _
                  ByVal isUpdate As Boolean, _
                  ByRef iCustid_Details_ot As String, _
                  ByRef sCustID_Details_GID As String, _
                  ByRef iErrorCode As String, _
                  ByRef USERID_GID As String, _
                  ByRef Risk_String As String, _
                  ByRef bln_complinace_clear As Boolean, _
                  ByRef str_complinace_clear_by As String, _
                  ByRef str_complinacel_clear_details As String, ByVal aux_1 As String, _
                       ByVal aux_2 As String, _
                       ByVal aux_3 As String, _
                       ByVal aux_4 As String, _
                       ByVal aux_5 As String, _
                       ByVal aux_6 As String, _
                       ByVal aux_7 As String, _
                       ByVal aux_8 As String, _
                        ByVal aux_9 As Date, _
                             ByVal aux_10 As Date, _
                                ByVal aux_11 As Date, _
                                   ByVal aux_12 As Date, _
                                    ByVal aux_13 As Date, _
                             ByVal aux_14 As Date, _
                                ByVal aux_15 As Date, _
                                   ByVal aux_16 As Date, _
                                     ByVal aux_17 As Integer, _
                             ByVal aux_18 As Integer, _
                                ByVal aux_19 As Integer, _
                                   ByVal aux_20 As Integer, ByVal blnISBlock_remittance As Boolean, ByVal blnISBlock_employee As Boolean, _
             ByVal Consignee_Name As String, ByVal Consignee_address As String, ByVal Consignee_port As String, _
             ByVal PurposeID_Master As Integer, ByVal PurposeID_Child As Integer, _
             ByVal risk_aux_1 As String, ByVal risk_aux_2 As String, ByVal risk_aux_3 As String, ByVal risk_aux_4 As String, _
             ByVal risk_aux_5 As String, ByVal risk_aux_6 As String, ByVal risk_aux_7 As String, ByVal risk_aux_8 As String, _
             ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String, _
             ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String, _
             ByVal risk_purpose As String, ByVal purpose As String, ByVal risk_source_of_income As String, ByVal source_of_income As String, _
             ByVal bln_RISK_PLUS As Boolean, ByVal RISK_PLUS_STRING As String)





        Dim m_iCustID_DET As SqlInt64
        sIDNumber = UCase(sIDNumber)
        sAddress1 = UCase(sAddress1)
        sAddress2 = UCase(sAddress2)
        sCity = UCase(sCity)
        sTel = UCase(sTel)
        sFax = UCase(sFax)
        sZIP = UCase(sZIP)
        sEmail = UCase(sEmail)
        sSponserName = UCase(sSponserName)
        BankAccNumber = UCase(BankAccNumber)
        BankName = UCase(BankName)
        BranchName = UCase(BranchName)
        sDetails = UCase(sDetails)
        sPlaceOfIssue = UCase(sPlaceOfIssue)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                ' loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident_risk_profile"
                loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk_Customer_Profile_risk_plus"

                'loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk"

                loComm.CommandType = CommandType.StoredProcedure
                If isUpdate = False Then


                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                Else

                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_Details_GID_Update))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))


                End If



                .Add(New SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sPlaceOfIssue & ""))


                .Add(New SqlParameter("@IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iIDTypeID))



                .Add(New SqlParameter("@IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sIDNumber))
                .Add(New SqlParameter("@DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateOfIssue))
                .Add(New SqlParameter("@ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dExpiryDate))

                .Add(New SqlParameter("@Address1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress1))
                .Add(New SqlParameter("@Address2", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress2))

                .Add(New SqlParameter("@City", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCity))
                .Add(New SqlParameter("@Tel", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sTel))
                .Add(New SqlParameter("@Fax", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sFax))
                .Add(New SqlParameter("@ZIP", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sZIP))
                .Add(New SqlParameter("@Email", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sEmail))

                .Add(New SqlParameter("@SponserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sSponserName))
                .Add(New SqlParameter("@Details", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sDetails))


                .Add(New SqlParameter("@mDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, dmDate))
                .Add(New SqlParameter("@Dateof_Birth", SqlDbType.DateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateof_Birth))

                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))


                .Add(New SqlParameter("@BankAccNumber", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankAccNumber))
                .Add(New SqlParameter("@BankName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankName))
                .Add(New SqlParameter("@BranchName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BranchName))

                .Add(New SqlParameter("@USERID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, USERID_GID))
                .Add(New SqlParameter("@visa_id", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, visa_id))

                .Add(New SqlParameter("@CustTypeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child))
                .Add(New SqlParameter("@CustTypeID_Child_SUB", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child_SUB))
                .Add(New SqlParameter("@Rep_Type", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Type))
                .Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
                .Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
                .Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
                .Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

                .Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
                .Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
                .Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
                .Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))
                .Add(New SqlParameter("@details_1", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_1))
                .Add(New SqlParameter("@details_2", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_2))
                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@iCustid_Details_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID_DET))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, iErrorCode))
                .Add(New SqlParameter("@CustID_Details_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, sCustID_Details_GID))
                'bIsUpdate = Convert.ToBoolean(btstatus.Text)

                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))


                .Add(New SqlParameter("@aux_1", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_1))
                .Add(New SqlParameter("@aux_2", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_2))
                .Add(New SqlParameter("@aux_3", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_3))
                .Add(New SqlParameter("@aux_4", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_4))

                .Add(New SqlParameter("@aux_5", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_5))
                .Add(New SqlParameter("@aux_6", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_6))
                .Add(New SqlParameter("@aux_7", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_7))
                .Add(New SqlParameter("@aux_8", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_8))




                .Add(New SqlParameter("@aux_9", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_9))

                .Add(New SqlParameter("@aux_10", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_10))

                .Add(New SqlParameter("@aux_11", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_11))

                .Add(New SqlParameter("@aux_12", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_12))


                .Add(New SqlParameter("@aux_13", _
         SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
         DataRowVersion.Proposed, aux_13))

                .Add(New SqlParameter("@aux_14", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_14))

                .Add(New SqlParameter("@aux_15", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_15))

                .Add(New SqlParameter("@aux_16", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_16))


                .Add(New SqlParameter("@aux_17", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_17))



                .Add(New SqlParameter("@aux_18", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_18))



                .Add(New SqlParameter("@aux_19", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_19))


                .Add(New SqlParameter("@aux_20", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_20))

                .Add(New SqlParameter("@is_remittance", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_remittance))
                .Add(New SqlParameter("@is_employee", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_employee))


                .Add(New SqlParameter("@Consignee_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_Name))
                .Add(New SqlParameter("@Consignee_address", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_address))
                .Add(New SqlParameter("@Consignee_port", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_port))


                .Add(New SqlParameter("@PurposeID_Master", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Master))
                .Add(New SqlParameter("@PurposeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Child))


                .Add(New SqlParameter("@risk_aux_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_1))
                .Add(New SqlParameter("@risk_aux_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_2))
                .Add(New SqlParameter("@risk_aux_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_3))
                .Add(New SqlParameter("@risk_aux_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_4))
                .Add(New SqlParameter("@risk_aux_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_5))
                .Add(New SqlParameter("@risk_aux_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_6))
                .Add(New SqlParameter("@risk_aux_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_7))
                .Add(New SqlParameter("@risk_aux_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_8))
                .Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
                .Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
                .Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
                .Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
                .Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
                .Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
                .Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
                .Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))

                .Add(New SqlParameter("@high_risk_plus", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_RISK_PLUS))
                .Add(New SqlParameter("@high_risk_plust_str", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, RISK_PLUS_STRING))
                .Add(New SqlParameter("@risk_purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_purpose))
                .Add(New SqlParameter("@risk_Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_source_of_income))
                .Add(New SqlParameter("@purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, purpose))
                .Add(New SqlParameter("@Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, source_of_income))



                If Not isUpdate Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If


            End With

            loComm.ExecuteNonQuery()

            If Not isUpdate Then
                m_iCustID_DET = (loComm.Parameters.Item("@iCustid_Details_ot").Value)
                iCustid_Details_ot = CStr(m_iCustID_DET.Value.ToString)
                sCustID_Details_GID = CStr(loComm.Parameters.Item("@CustID_Details_GID").Value)
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub



    Public Sub mSave_Cust_detail_RiskProfile_Profiling_CB(ByRef loComm As SqlCommand, ByVal iCustid_Details As Integer, _
                  ByVal sCustID_Details_GID_Update As String, _
                       ByVal iCustid As String, _
                  ByVal sCustID_GID As String, _
                    ByVal sPlaceOfIssue As String, _
                  ByVal iIDTypeID As Integer, _
                  ByVal sIDNumber As String, _
                  ByVal dDateOfIssue As Date, _
                  ByVal dExpiryDate As Date, _
                  ByVal sAddress1 As String, _
                  ByVal sAddress2 As String, _
                  ByVal sCity As String, _
                  ByVal sTel As String, _
                  ByVal sFax As String, _
                  ByVal sZIP As String, _
                  ByVal sEmail As String, _
                  ByVal sSponserName As String, _
                     ByVal sDetails As String, _
                  ByVal dmDate As Date, _
                  ByVal dDateof_Birth As Date, _
                   ByVal iUserID As Integer, _
                  ByVal BankAccNumber As String, _
                  ByVal BankName As String, _
                  ByVal BranchName As String, _
                  ByVal visa_id As String, _
                  ByVal CustTypeID_Child As String, _
                  ByVal CustTypeID_Child_SUB As String, _
                  ByVal Rep_Type As String, _
                  ByVal Volume_day_fx As Double, _
                  ByVal Value_day_fx As Double, _
                  ByVal Volume_Month_fx As Double, _
                  ByVal Value_Month_fx As Double, _
                  ByVal Volume_day_remittance As Double, _
                  ByVal Value_day_remittance As Double, _
                  ByVal Volume_Month_Remittance As Double, _
                  ByVal Value_Month_Remittance As Double, _
                  ByVal details_1 As String, _
                  ByVal details_2 As String, _
                  ByVal risk_score As Double, _
                  ByVal isUpdate As Boolean, _
                  ByRef iCustid_Details_ot As String, _
                  ByRef sCustID_Details_GID As String, _
                  ByRef iErrorCode As String, _
                  ByRef USERID_GID As String, _
                  ByRef Risk_String As String, _
                  ByRef bln_complinace_clear As Boolean, _
                  ByRef str_complinace_clear_by As String, _
                  ByRef str_complinacel_clear_details As String, ByVal aux_1 As String, _
                       ByVal aux_2 As String, _
                       ByVal aux_3 As String, _
                       ByVal aux_4 As String, _
                       ByVal aux_5 As String, _
                       ByVal aux_6 As String, _
                       ByVal aux_7 As String, _
                       ByVal aux_8 As String, _
                        ByVal aux_9 As Date, _
                             ByVal aux_10 As Date, _
                                ByVal aux_11 As Date, _
                                   ByVal aux_12 As Date, _
                                    ByVal aux_13 As Date, _
                             ByVal aux_14 As Date, _
                                ByVal aux_15 As Date, _
                                   ByVal aux_16 As Date, _
                                     ByVal aux_17 As Integer, _
                             ByVal aux_18 As Integer, _
                                ByVal aux_19 As Integer, _
                                   ByVal aux_20 As Integer, ByVal blnISBlock_remittance As Boolean, ByVal blnISBlock_employee As Boolean, _
             ByVal Consignee_Name As String, ByVal Consignee_address As String, ByVal Consignee_port As String, _
             ByVal PurposeID_Master As Integer, ByVal PurposeID_Child As Integer, _
             ByVal risk_aux_1 As String, ByVal risk_aux_2 As String, ByVal risk_aux_3 As String, ByVal risk_aux_4 As String, _
             ByVal risk_aux_5 As String, ByVal risk_aux_6 As String, ByVal risk_aux_7 As String, ByVal risk_aux_8 As String, _
             ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String, _
             ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String, _
             ByVal risk_purpose As String, ByVal purpose As String, ByVal risk_source_of_income As String, ByVal source_of_income As String, _
             ByVal bln_RISK_PLUS As Boolean, ByVal RISK_PLUS_STRING As String, _
             Optional CB_City As String = "", _
             Optional CB_Airport_Code As String = "", _
             Optional CB_CityID As Integer = 0, _
             Optional CB_CUST_PAYMODE As String = "", _
             Optional CB_CUST_PAYMODE_DESC As String = "")





        Dim m_iCustID_DET As SqlInt64
        sIDNumber = UCase(sIDNumber)
        sAddress1 = UCase(sAddress1)
        sAddress2 = UCase(sAddress2)
        sCity = UCase(sCity)
        sTel = UCase(sTel)
        sFax = UCase(sFax)
        sZIP = UCase(sZIP)
        sEmail = UCase(sEmail)
        sSponserName = UCase(sSponserName)
        BankAccNumber = UCase(BankAccNumber)
        BankName = UCase(BankName)
        BranchName = UCase(BranchName)
        sDetails = UCase(sDetails)
        sPlaceOfIssue = UCase(sPlaceOfIssue)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                ' loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident_risk_profile"
                loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk_Customer_Profile_risk_plus_cb"

                'loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk"

                loComm.CommandType = CommandType.StoredProcedure
                If isUpdate = False Then


                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                Else

                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_Details_GID_Update))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))


                End If



                .Add(New SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sPlaceOfIssue & ""))


                .Add(New SqlParameter("@IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iIDTypeID))



                .Add(New SqlParameter("@IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sIDNumber))
                .Add(New SqlParameter("@DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateOfIssue))
                .Add(New SqlParameter("@ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dExpiryDate))

                .Add(New SqlParameter("@Address1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress1))
                .Add(New SqlParameter("@Address2", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress2))

                .Add(New SqlParameter("@City", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCity))
                .Add(New SqlParameter("@Tel", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sTel))
                .Add(New SqlParameter("@Fax", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sFax))
                .Add(New SqlParameter("@ZIP", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sZIP))
                .Add(New SqlParameter("@Email", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sEmail))

                .Add(New SqlParameter("@SponserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sSponserName))
                .Add(New SqlParameter("@Details", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sDetails))


                .Add(New SqlParameter("@mDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, dmDate))
                .Add(New SqlParameter("@Dateof_Birth", SqlDbType.DateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateof_Birth))

                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))


                .Add(New SqlParameter("@BankAccNumber", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankAccNumber))
                .Add(New SqlParameter("@BankName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankName))
                .Add(New SqlParameter("@BranchName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BranchName))

                .Add(New SqlParameter("@USERID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, USERID_GID))
                .Add(New SqlParameter("@visa_id", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, visa_id))

                .Add(New SqlParameter("@CustTypeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child))
                .Add(New SqlParameter("@CustTypeID_Child_SUB", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child_SUB))
                .Add(New SqlParameter("@Rep_Type", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Type))
                .Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
                .Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
                .Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
                .Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

                .Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
                .Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
                .Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
                .Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))
                .Add(New SqlParameter("@details_1", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_1))
                .Add(New SqlParameter("@details_2", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_2))
                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@iCustid_Details_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID_DET))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, iErrorCode))
                .Add(New SqlParameter("@CustID_Details_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, sCustID_Details_GID))
                'bIsUpdate = Convert.ToBoolean(btstatus.Text)

                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))


                .Add(New SqlParameter("@aux_1", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_1))
                .Add(New SqlParameter("@aux_2", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_2))
                .Add(New SqlParameter("@aux_3", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_3))
                .Add(New SqlParameter("@aux_4", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_4))

                .Add(New SqlParameter("@aux_5", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_5))
                .Add(New SqlParameter("@aux_6", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_6))
                .Add(New SqlParameter("@aux_7", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_7))
                .Add(New SqlParameter("@aux_8", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_8))




                .Add(New SqlParameter("@aux_9", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_9))

                .Add(New SqlParameter("@aux_10", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_10))

                .Add(New SqlParameter("@aux_11", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_11))

                .Add(New SqlParameter("@aux_12", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_12))


                .Add(New SqlParameter("@aux_13", _
         SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
         DataRowVersion.Proposed, aux_13))

                .Add(New SqlParameter("@aux_14", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_14))

                .Add(New SqlParameter("@aux_15", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_15))

                .Add(New SqlParameter("@aux_16", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_16))


                .Add(New SqlParameter("@aux_17", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_17))



                .Add(New SqlParameter("@aux_18", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_18))



                .Add(New SqlParameter("@aux_19", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_19))


                .Add(New SqlParameter("@aux_20", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_20))

                .Add(New SqlParameter("@is_remittance", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_remittance))
                .Add(New SqlParameter("@is_employee", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_employee))


                .Add(New SqlParameter("@Consignee_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_Name))
                .Add(New SqlParameter("@Consignee_address", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_address))
                .Add(New SqlParameter("@Consignee_port", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_port))


                .Add(New SqlParameter("@PurposeID_Master", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Master))
                .Add(New SqlParameter("@PurposeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Child))


                .Add(New SqlParameter("@risk_aux_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_1))
                .Add(New SqlParameter("@risk_aux_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_2))
                .Add(New SqlParameter("@risk_aux_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_3))
                .Add(New SqlParameter("@risk_aux_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_4))
                .Add(New SqlParameter("@risk_aux_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_5))
                .Add(New SqlParameter("@risk_aux_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_6))
                .Add(New SqlParameter("@risk_aux_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_7))
                .Add(New SqlParameter("@risk_aux_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_8))
                .Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
                .Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
                .Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
                .Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
                .Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
                .Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
                .Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
                .Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))

                .Add(New SqlParameter("@high_risk_plus", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_RISK_PLUS))
                .Add(New SqlParameter("@high_risk_plust_str", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, RISK_PLUS_STRING))
                .Add(New SqlParameter("@risk_purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_purpose))
                .Add(New SqlParameter("@risk_Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_source_of_income))
                .Add(New SqlParameter("@purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, purpose))
                .Add(New SqlParameter("@Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, source_of_income))


                .Add(New SqlParameter("@CB_City", SqlDbType.NVarChar, 150, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_City))
                .Add(New SqlParameter("@CB_Airport_Code", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_Airport_Code))
                .Add(New SqlParameter("@CB_CityID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_CityID))
                .Add(New SqlParameter("@CB_CUST_PAYMODE", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_CUST_PAYMODE))
                .Add(New SqlParameter("@CB_CUST_PAYMODE_DESC", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_CUST_PAYMODE_DESC))


                If Not isUpdate Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If


            End With

            loComm.ExecuteNonQuery()

            If Not isUpdate Then
                m_iCustID_DET = (loComm.Parameters.Item("@iCustid_Details_ot").Value)
                iCustid_Details_ot = CStr(m_iCustID_DET.Value.ToString)
                sCustID_Details_GID = CStr(loComm.Parameters.Item("@CustID_Details_GID").Value)
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub



    Public Sub mSave_Cust_detail_RiskProfile_Profiling_rule_log(ByRef loComm As SqlCommand, ByVal iCustid_Details As Integer, _
                  ByVal sCustID_Details_GID_Update As String, _
                       ByVal iCustid As String, _
                  ByVal sCustID_GID As String, _
                    ByVal sPlaceOfIssue As String, _
                  ByVal iIDTypeID As Integer, _
                  ByVal sIDNumber As String, _
                  ByVal dDateOfIssue As Date, _
                  ByVal dExpiryDate As Date, _
                  ByVal sAddress1 As String, _
                  ByVal sAddress2 As String, _
                  ByVal sCity As String, _
                  ByVal sTel As String, _
                  ByVal sFax As String, _
                  ByVal sZIP As String, _
                  ByVal sEmail As String, _
                  ByVal sSponserName As String, _
                     ByVal sDetails As String, _
                  ByVal dmDate As Date, _
                  ByVal dDateof_Birth As Date, _
                   ByVal iUserID As Integer, _
                  ByVal BankAccNumber As String, _
                  ByVal BankName As String, _
                  ByVal BranchName As String, _
                  ByVal visa_id As String, _
                  ByVal CustTypeID_Child As String, _
                  ByVal CustTypeID_Child_SUB As String, _
                  ByVal Rep_Type As String, _
                  ByVal Volume_day_fx As Double, _
                  ByVal Value_day_fx As Double, _
                  ByVal Volume_Month_fx As Double, _
                  ByVal Value_Month_fx As Double, _
                  ByVal Volume_day_remittance As Double, _
                  ByVal Value_day_remittance As Double, _
                  ByVal Volume_Month_Remittance As Double, _
                  ByVal Value_Month_Remittance As Double, _
                  ByVal details_1 As String, _
                  ByVal details_2 As String, _
                  ByVal risk_score As Double, _
                  ByVal isUpdate As Boolean, _
                  ByRef iCustid_Details_ot As String, _
                  ByRef sCustID_Details_GID As String, _
                  ByRef iErrorCode As String, _
                  ByRef USERID_GID As String, _
                  ByRef Risk_String As String, _
                  ByRef bln_complinace_clear As Boolean, _
                  ByRef str_complinace_clear_by As String, _
                  ByRef str_complinacel_clear_details As String, ByVal aux_1 As String, _
                       ByVal aux_2 As String, _
                       ByVal aux_3 As String, _
                       ByVal aux_4 As String, _
                       ByVal aux_5 As String, _
                       ByVal aux_6 As String, _
                       ByVal aux_7 As String, _
                       ByVal aux_8 As String, _
                        ByVal aux_9 As Date, _
                             ByVal aux_10 As Date, _
                                ByVal aux_11 As Date, _
                                   ByVal aux_12 As Date, _
                                    ByVal aux_13 As Date, _
                             ByVal aux_14 As Date, _
                                ByVal aux_15 As Date, _
                                   ByVal aux_16 As Date, _
                                     ByVal aux_17 As Integer, _
                             ByVal aux_18 As Integer, _
                                ByVal aux_19 As Integer, _
                                   ByVal aux_20 As Integer, ByVal blnISBlock_remittance As Boolean, ByVal blnISBlock_employee As Boolean, _
             ByVal Consignee_Name As String, ByVal Consignee_address As String, ByVal Consignee_port As String, _
             ByVal PurposeID_Master As Integer, ByVal PurposeID_Child As Integer, _
             ByVal risk_aux_1 As String, ByVal risk_aux_2 As String, ByVal risk_aux_3 As String, ByVal risk_aux_4 As String, _
             ByVal risk_aux_5 As String, ByVal risk_aux_6 As String, ByVal risk_aux_7 As String, ByVal risk_aux_8 As String, _
             ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String, _
             ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String, _
             ByVal risk_purpose As String, ByVal purpose As String, ByVal risk_source_of_income As String, ByVal source_of_income As String, _
             ByVal nCompl_Fraud_Log_ID As Long, ByVal bln_RISK_PLUS As Boolean, ByVal RISK_PLUS_STRING As String)





        Dim m_iCustID_DET As SqlInt64
        sIDNumber = UCase(sIDNumber)
        sAddress1 = UCase(sAddress1)
        sAddress2 = UCase(sAddress2)
        sCity = UCase(sCity)
        sTel = UCase(sTel)
        sFax = UCase(sFax)
        sZIP = UCase(sZIP)
        sEmail = UCase(sEmail)
        sSponserName = UCase(sSponserName)
        BankAccNumber = UCase(BankAccNumber)
        BankName = UCase(BankName)
        BranchName = UCase(BranchName)
        sDetails = UCase(sDetails)
        sPlaceOfIssue = UCase(sPlaceOfIssue)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                ' loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident_risk_profile"
                loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk_Customer_Profile_rule_log_RISK_PLUS"

                'loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk"

                loComm.CommandType = CommandType.StoredProcedure
                If isUpdate = False Then


                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                Else

                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_Details_GID_Update))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))


                End If



                .Add(New SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sPlaceOfIssue & ""))


                .Add(New SqlParameter("@IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iIDTypeID))



                .Add(New SqlParameter("@IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sIDNumber))
                .Add(New SqlParameter("@DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateOfIssue))
                .Add(New SqlParameter("@ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dExpiryDate))

                .Add(New SqlParameter("@Address1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress1))
                .Add(New SqlParameter("@Address2", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress2))

                .Add(New SqlParameter("@City", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCity))
                .Add(New SqlParameter("@Tel", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sTel))
                .Add(New SqlParameter("@Fax", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sFax))
                .Add(New SqlParameter("@ZIP", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sZIP))
                .Add(New SqlParameter("@Email", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sEmail))

                .Add(New SqlParameter("@SponserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sSponserName))
                .Add(New SqlParameter("@Details", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sDetails))


                .Add(New SqlParameter("@mDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, dmDate))
                .Add(New SqlParameter("@Dateof_Birth", SqlDbType.DateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateof_Birth))

                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))


                .Add(New SqlParameter("@BankAccNumber", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankAccNumber))
                .Add(New SqlParameter("@BankName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankName))
                .Add(New SqlParameter("@BranchName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BranchName))

                .Add(New SqlParameter("@USERID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, USERID_GID))
                .Add(New SqlParameter("@visa_id", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, visa_id))

                .Add(New SqlParameter("@CustTypeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child))
                .Add(New SqlParameter("@CustTypeID_Child_SUB", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child_SUB))
                .Add(New SqlParameter("@Rep_Type", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Type))
                .Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
                .Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
                .Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
                .Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

                .Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
                .Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
                .Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
                .Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))
                .Add(New SqlParameter("@details_1", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_1))
                .Add(New SqlParameter("@details_2", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_2))
                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@iCustid_Details_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID_DET))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, iErrorCode))
                .Add(New SqlParameter("@CustID_Details_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, sCustID_Details_GID))
                'bIsUpdate = Convert.ToBoolean(btstatus.Text)

                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))


                .Add(New SqlParameter("@aux_1", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_1))
                .Add(New SqlParameter("@aux_2", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_2))
                .Add(New SqlParameter("@aux_3", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_3))
                .Add(New SqlParameter("@aux_4", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_4))

                .Add(New SqlParameter("@aux_5", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_5))
                .Add(New SqlParameter("@aux_6", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_6))
                .Add(New SqlParameter("@aux_7", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_7))
                .Add(New SqlParameter("@aux_8", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_8))




                .Add(New SqlParameter("@aux_9", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_9))

                .Add(New SqlParameter("@aux_10", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_10))

                .Add(New SqlParameter("@aux_11", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_11))

                .Add(New SqlParameter("@aux_12", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_12))


                .Add(New SqlParameter("@aux_13", _
         SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
         DataRowVersion.Proposed, aux_13))

                .Add(New SqlParameter("@aux_14", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_14))

                .Add(New SqlParameter("@aux_15", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_15))

                .Add(New SqlParameter("@aux_16", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_16))


                .Add(New SqlParameter("@aux_17", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_17))



                .Add(New SqlParameter("@aux_18", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_18))



                .Add(New SqlParameter("@aux_19", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_19))


                .Add(New SqlParameter("@aux_20", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_20))

                .Add(New SqlParameter("@is_remittance", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_remittance))
                .Add(New SqlParameter("@is_employee", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_employee))


                .Add(New SqlParameter("@Consignee_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_Name))
                .Add(New SqlParameter("@Consignee_address", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_address))
                .Add(New SqlParameter("@Consignee_port", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_port))


                .Add(New SqlParameter("@PurposeID_Master", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Master))
                .Add(New SqlParameter("@PurposeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Child))


                .Add(New SqlParameter("@risk_aux_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_1))
                .Add(New SqlParameter("@risk_aux_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_2))
                .Add(New SqlParameter("@risk_aux_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_3))
                .Add(New SqlParameter("@risk_aux_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_4))
                .Add(New SqlParameter("@risk_aux_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_5))
                .Add(New SqlParameter("@risk_aux_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_6))
                .Add(New SqlParameter("@risk_aux_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_7))
                .Add(New SqlParameter("@risk_aux_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_8))
                .Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
                .Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
                .Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
                .Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
                .Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
                .Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
                .Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
                .Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))


                .Add(New SqlParameter("@risk_purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_purpose))
                .Add(New SqlParameter("@risk_Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_source_of_income))
                .Add(New SqlParameter("@purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, purpose))
                .Add(New SqlParameter("@Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, source_of_income))


                .Add(New SqlParameter("@high_risk_plus", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_RISK_PLUS))
                .Add(New SqlParameter("@high_risk_plust_str", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, RISK_PLUS_STRING))
                .Add(New SqlParameter("@Rule_Log_ID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, nCompl_Fraud_Log_ID))

                If Not isUpdate Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If


            End With

            loComm.ExecuteNonQuery()

            If Not isUpdate Then
                m_iCustID_DET = (loComm.Parameters.Item("@iCustid_Details_ot").Value)
                iCustid_Details_ot = CStr(m_iCustID_DET.Value.ToString)
                sCustID_Details_GID = CStr(loComm.Parameters.Item("@CustID_Details_GID").Value)
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub



    Public Sub mSave_Cust_detail_RiskProfile_Profiling_rule_log_CB(ByRef loComm As SqlCommand, ByVal iCustid_Details As Integer, _
                  ByVal sCustID_Details_GID_Update As String, _
                       ByVal iCustid As String, _
                  ByVal sCustID_GID As String, _
                    ByVal sPlaceOfIssue As String, _
                  ByVal iIDTypeID As Integer, _
                  ByVal sIDNumber As String, _
                  ByVal dDateOfIssue As Date, _
                  ByVal dExpiryDate As Date, _
                  ByVal sAddress1 As String, _
                  ByVal sAddress2 As String, _
                  ByVal sCity As String, _
                  ByVal sTel As String, _
                  ByVal sFax As String, _
                  ByVal sZIP As String, _
                  ByVal sEmail As String, _
                  ByVal sSponserName As String, _
                     ByVal sDetails As String, _
                  ByVal dmDate As Date, _
                  ByVal dDateof_Birth As Date, _
                   ByVal iUserID As Integer, _
                  ByVal BankAccNumber As String, _
                  ByVal BankName As String, _
                  ByVal BranchName As String, _
                  ByVal visa_id As String, _
                  ByVal CustTypeID_Child As String, _
                  ByVal CustTypeID_Child_SUB As String, _
                  ByVal Rep_Type As String, _
                  ByVal Volume_day_fx As Double, _
                  ByVal Value_day_fx As Double, _
                  ByVal Volume_Month_fx As Double, _
                  ByVal Value_Month_fx As Double, _
                  ByVal Volume_day_remittance As Double, _
                  ByVal Value_day_remittance As Double, _
                  ByVal Volume_Month_Remittance As Double, _
                  ByVal Value_Month_Remittance As Double, _
                  ByVal details_1 As String, _
                  ByVal details_2 As String, _
                  ByVal risk_score As Double, _
                  ByVal isUpdate As Boolean, _
                  ByRef iCustid_Details_ot As String, _
                  ByRef sCustID_Details_GID As String, _
                  ByRef iErrorCode As String, _
                  ByRef USERID_GID As String, _
                  ByRef Risk_String As String, _
                  ByRef bln_complinace_clear As Boolean, _
                  ByRef str_complinace_clear_by As String, _
                  ByRef str_complinacel_clear_details As String, ByVal aux_1 As String, _
                       ByVal aux_2 As String, _
                       ByVal aux_3 As String, _
                       ByVal aux_4 As String, _
                       ByVal aux_5 As String, _
                       ByVal aux_6 As String, _
                       ByVal aux_7 As String, _
                       ByVal aux_8 As String, _
                        ByVal aux_9 As Date, _
                             ByVal aux_10 As Date, _
                                ByVal aux_11 As Date, _
                                   ByVal aux_12 As Date, _
                                    ByVal aux_13 As Date, _
                             ByVal aux_14 As Date, _
                                ByVal aux_15 As Date, _
                                   ByVal aux_16 As Date, _
                                     ByVal aux_17 As Integer, _
                             ByVal aux_18 As Integer, _
                                ByVal aux_19 As Integer, _
                                   ByVal aux_20 As Integer, ByVal blnISBlock_remittance As Boolean, ByVal blnISBlock_employee As Boolean, _
             ByVal Consignee_Name As String, ByVal Consignee_address As String, ByVal Consignee_port As String, _
             ByVal PurposeID_Master As Integer, ByVal PurposeID_Child As Integer, _
             ByVal risk_aux_1 As String, ByVal risk_aux_2 As String, ByVal risk_aux_3 As String, ByVal risk_aux_4 As String, _
             ByVal risk_aux_5 As String, ByVal risk_aux_6 As String, ByVal risk_aux_7 As String, ByVal risk_aux_8 As String, _
             ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String, _
             ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String, _
             ByVal risk_purpose As String, ByVal purpose As String, ByVal risk_source_of_income As String, ByVal source_of_income As String, _
             ByVal nCompl_Fraud_Log_ID As Long, ByVal bln_RISK_PLUS As Boolean, ByVal RISK_PLUS_STRING As String, _
             Optional CB_City As String = "", _
             Optional CB_Airport_Code As String = "", _
             Optional CB_CityID As Integer = 0, _
             Optional CB_CUST_PAYMODE As String = "", _
             Optional CB_CUST_PAYMODE_DESC As String = "")




        Dim m_iCustID_DET As SqlInt64
        sIDNumber = UCase(sIDNumber)
        sAddress1 = UCase(sAddress1)
        sAddress2 = UCase(sAddress2)
        sCity = UCase(sCity)
        sTel = UCase(sTel)
        sFax = UCase(sFax)
        sZIP = UCase(sZIP)
        sEmail = UCase(sEmail)
        sSponserName = UCase(sSponserName)
        BankAccNumber = UCase(BankAccNumber)
        BankName = UCase(BankName)
        BranchName = UCase(BranchName)
        sDetails = UCase(sDetails)
        sPlaceOfIssue = UCase(sPlaceOfIssue)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                ' loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident_risk_profile"
                loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk_Customer_Profile_rule_log_RISK_PLUS_CB"

                'loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk"

                loComm.CommandType = CommandType.StoredProcedure
                If isUpdate = False Then


                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                Else

                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_Details_GID_Update))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))


                End If



                .Add(New SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sPlaceOfIssue & ""))


                .Add(New SqlParameter("@IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iIDTypeID))



                .Add(New SqlParameter("@IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sIDNumber))
                .Add(New SqlParameter("@DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateOfIssue))
                .Add(New SqlParameter("@ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dExpiryDate))

                .Add(New SqlParameter("@Address1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress1))
                .Add(New SqlParameter("@Address2", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress2))

                .Add(New SqlParameter("@City", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCity))
                .Add(New SqlParameter("@Tel", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sTel))
                .Add(New SqlParameter("@Fax", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sFax))
                .Add(New SqlParameter("@ZIP", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sZIP))
                .Add(New SqlParameter("@Email", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sEmail))

                .Add(New SqlParameter("@SponserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sSponserName))
                .Add(New SqlParameter("@Details", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sDetails))


                .Add(New SqlParameter("@mDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, dmDate))
                .Add(New SqlParameter("@Dateof_Birth", SqlDbType.DateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateof_Birth))

                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))


                .Add(New SqlParameter("@BankAccNumber", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankAccNumber))
                .Add(New SqlParameter("@BankName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankName))
                .Add(New SqlParameter("@BranchName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BranchName))

                .Add(New SqlParameter("@USERID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, USERID_GID))
                .Add(New SqlParameter("@visa_id", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, visa_id))

                .Add(New SqlParameter("@CustTypeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child))
                .Add(New SqlParameter("@CustTypeID_Child_SUB", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child_SUB))
                .Add(New SqlParameter("@Rep_Type", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Type))
                .Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
                .Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
                .Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
                .Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

                .Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
                .Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
                .Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
                .Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))
                .Add(New SqlParameter("@details_1", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_1))
                .Add(New SqlParameter("@details_2", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_2))
                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@iCustid_Details_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID_DET))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, iErrorCode))
                .Add(New SqlParameter("@CustID_Details_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, sCustID_Details_GID))
                'bIsUpdate = Convert.ToBoolean(btstatus.Text)

                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))


                .Add(New SqlParameter("@aux_1", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_1))
                .Add(New SqlParameter("@aux_2", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_2))
                .Add(New SqlParameter("@aux_3", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_3))
                .Add(New SqlParameter("@aux_4", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_4))

                .Add(New SqlParameter("@aux_5", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_5))
                .Add(New SqlParameter("@aux_6", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_6))
                .Add(New SqlParameter("@aux_7", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_7))
                .Add(New SqlParameter("@aux_8", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_8))




                .Add(New SqlParameter("@aux_9", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_9))

                .Add(New SqlParameter("@aux_10", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_10))

                .Add(New SqlParameter("@aux_11", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_11))

                .Add(New SqlParameter("@aux_12", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_12))


                .Add(New SqlParameter("@aux_13", _
         SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
         DataRowVersion.Proposed, aux_13))

                .Add(New SqlParameter("@aux_14", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_14))

                .Add(New SqlParameter("@aux_15", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_15))

                .Add(New SqlParameter("@aux_16", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_16))


                .Add(New SqlParameter("@aux_17", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_17))



                .Add(New SqlParameter("@aux_18", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_18))



                .Add(New SqlParameter("@aux_19", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_19))


                .Add(New SqlParameter("@aux_20", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_20))

                .Add(New SqlParameter("@is_remittance", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_remittance))
                .Add(New SqlParameter("@is_employee", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_employee))


                .Add(New SqlParameter("@Consignee_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_Name))
                .Add(New SqlParameter("@Consignee_address", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_address))
                .Add(New SqlParameter("@Consignee_port", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_port))


                .Add(New SqlParameter("@PurposeID_Master", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Master))
                .Add(New SqlParameter("@PurposeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Child))


                .Add(New SqlParameter("@risk_aux_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_1))
                .Add(New SqlParameter("@risk_aux_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_2))
                .Add(New SqlParameter("@risk_aux_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_3))
                .Add(New SqlParameter("@risk_aux_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_4))
                .Add(New SqlParameter("@risk_aux_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_5))
                .Add(New SqlParameter("@risk_aux_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_6))
                .Add(New SqlParameter("@risk_aux_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_7))
                .Add(New SqlParameter("@risk_aux_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_8))
                .Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
                .Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
                .Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
                .Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
                .Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
                .Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
                .Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
                .Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))


                .Add(New SqlParameter("@risk_purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_purpose))
                .Add(New SqlParameter("@risk_Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_source_of_income))
                .Add(New SqlParameter("@purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, purpose))
                .Add(New SqlParameter("@Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, source_of_income))


                .Add(New SqlParameter("@high_risk_plus", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_RISK_PLUS))
                .Add(New SqlParameter("@high_risk_plust_str", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, RISK_PLUS_STRING))
                .Add(New SqlParameter("@Rule_Log_ID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, nCompl_Fraud_Log_ID))


                .Add(New SqlParameter("@CB_City", SqlDbType.NVarChar, 150, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_City))
                .Add(New SqlParameter("@CB_Airport_Code", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_Airport_Code))
                .Add(New SqlParameter("@CB_CityID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_CityID))
                .Add(New SqlParameter("@CB_CUST_PAYMODE", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_CUST_PAYMODE))
                .Add(New SqlParameter("@CB_CUST_PAYMODE_DESC", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_CUST_PAYMODE_DESC))

                If Not isUpdate Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If


            End With

            loComm.ExecuteNonQuery()

            If Not isUpdate Then
                m_iCustID_DET = (loComm.Parameters.Item("@iCustid_Details_ot").Value)
                iCustid_Details_ot = CStr(m_iCustID_DET.Value.ToString)
                sCustID_Details_GID = CStr(loComm.Parameters.Item("@CustID_Details_GID").Value)
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub





    Public Sub SAVE_CUST_detail_REP(ByRef loComm As SqlCommand, ByVal sRep_ID_GID_UPDATE As String, _
                                    ByVal lCustid As Long, _
                                    ByVal sCustid_GID As String, _
                                    ByVal sRepName As String, _
                                    ByVal sRep_Name_Arb As String, _
                                    ByVal iRep_IDTypeID As Integer, _
                                    ByVal sRep_IDNumber As String, _
                                    ByVal dRep_DateOfIssue As Date, _
                                    ByVal dRep_ExpiryDate As Date, _
                                    ByVal iUserID As Integer, _
                                    ByVal IsUpdate As Boolean, _
                                    ByRef str_Rep_ID_ot As String, _
                                    ByRef str_Rep_ID_GID_ot As String, _
                                    ByRef str_ErrorCode As String, _
                                    ByRef str_Rep_CELL As String, _
                                    ByRef str_Rep_IdPlaceOfIssue As String, _
                                    ByRef str_Rep_Nationality As String, _
                                    ByRef nDTCD As String, ByRef nSBCD As Integer, _
                                    ByRef Rep_DOB As Date, _
                                    ByRef Rep_ADD As String, _
                                    ByRef Rep_Email As String, _
                                     ByRef Rep_ID As String, _
                                      ByRef Rep_ID_GID As String, _
                                    ByRef Rep_Remarks As String, ByRef Remarks As String, ByRef userid_gid As String)

        Try




            sRepName = UCase(sRepName)
            'UCase(sRep_Name_Arb)
            sRep_IDNumber = UCase(sRep_IDNumber)


            loComm.Parameters.Clear()


            With loComm.Parameters

                loComm.CommandText = "dbo.[stp_tblCust_Rep_InsertUpdate_GID_new]"
                loComm.CommandType = CommandType.StoredProcedure


                If IsUpdate = False Then
                    .Add(New SqlParameter("@Rep_ID_GID_UPDATE", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                Else
                    .Add(New SqlParameter("@Rep_ID_GID_UPDATE", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_ID_GID_UPDATE))
                End If

                .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, lCustid))
                .Add(New SqlParameter("@Custid_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustid_GID))
                .Add(New SqlParameter("@RepName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRepName))
                .Add(New SqlParameter("@Rep_Name_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_Name_Arb))



                .Add(New SqlParameter("@Rep_IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iRep_IDTypeID))

                .Add(New SqlParameter("@Rep_IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_IDNumber))

                .Add(New SqlParameter("@Rep_DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dRep_DateOfIssue))
                .Add(New SqlParameter("@Rep_ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dRep_ExpiryDate))





                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))

                If IsUpdate = False Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
                    False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
                    False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If

                .Add(New SqlParameter("@Rep_ID_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_ID_ot))
                .Add(New SqlParameter("@Rep_ID_GID_ot", SqlDbType.NVarChar, 50, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_Rep_ID_GID_ot))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_ErrorCode))




                .Add(New SqlParameter("@Rep_CELL", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Rep_CELL))
                .Add(New SqlParameter("@Rep_IdPlaceOfIssue", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_IdPlaceOfIssue))
                .Add(New SqlParameter("@Rep_Nationality", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_Nationality))

                .Add(New SqlParameter("@DTCD", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, nDTCD))
                .Add(New SqlParameter("@SBCD", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, nSBCD))



                .Add(New SqlParameter("@Rep_DOB", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, Rep_DOB))
                .Add(New SqlParameter("@Rep_ADD", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_ADD))
                .Add(New SqlParameter("@Rep_Email", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Email))
                .Add(New SqlParameter("@Rep_Remarks", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Remarks))
                .Add(New SqlParameter("@Remarks", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Remarks))
                .Add(New SqlParameter("@user_id_gid", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, userid_gid))



            End With
            loComm.ExecuteNonQuery()
            Dim nrep_id As Integer = 0
            If IsUpdate = False Then

                Rep_ID = (loComm.Parameters.Item("@Rep_ID_ot").Value)
                Rep_ID_GID = CStr(loComm.Parameters.Item("@Rep_ID_GID_ot").Value)

            End If

        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator...")

        End Try


    End Sub


    Public Sub SAVE_CUST_detail_REP_RISK_PROFILE(ByRef loComm As SqlCommand, ByVal sRep_ID_GID_UPDATE As String, _
                                  ByVal lCustid As Long, _
                                  ByVal sCustid_GID As String, _
                                  ByVal sRepName As String, _
                                  ByVal sRep_Name_Arb As String, _
                                  ByVal iRep_IDTypeID As Integer, _
                                  ByVal sRep_IDNumber As String, _
                                  ByVal dRep_DateOfIssue As Date, _
                                  ByVal dRep_ExpiryDate As Date, _
                                  ByVal iUserID As Integer, _
                                  ByVal IsUpdate As Boolean, _
                                  ByRef str_Rep_ID_ot As String, _
                                  ByRef str_Rep_ID_GID_ot As String, _
                                  ByRef str_ErrorCode As String, _
                                  ByRef str_Rep_CELL As String, _
                                  ByRef str_Rep_IdPlaceOfIssue As String, _
                                  ByRef str_Rep_Nationality As String, _
                                  ByRef nDTCD As String, ByRef nSBCD As Integer, _
                                  ByRef Rep_DOB As Date, _
                                  ByRef Rep_ADD As String, _
                                  ByRef Rep_Email As String, _
                                   ByRef Rep_ID As String, _
                                    ByRef Rep_ID_GID As String, _
                                  ByRef Rep_Remarks As String, ByRef Remarks As String, ByRef userid_gid As String, ByVal risk_score As Double, _
                                       ByRef Risk_String As String, _
                    ByRef bln_complinace_clear As Boolean, _
                    ByRef str_complinace_clear_by As String, _
                    ByRef str_complinacel_clear_details As String, ByVal risk_Name_1 As String,
                    ByVal risk_Nationality_2 As String,
                    ByVal risk_Address_3 As String,
                    ByVal risk_POI_4 As String,
                    ByVal risk_aux_5 As String,
                    ByVal risk_aux_6 As String,
                    ByVal risk_aux_7 As String,
                    ByVal risk_aux_8 As String)
        Try




            sRepName = UCase(sRepName)
            'UCase(sRep_Name_Arb)
            sRep_IDNumber = UCase(sRep_IDNumber)


            loComm.Parameters.Clear()


            With loComm.Parameters

                loComm.CommandText = "dbo.[stp_tblCust_Rep_InsertUpdate_GID_new_2]"
                loComm.CommandType = CommandType.StoredProcedure


                If IsUpdate = False Then
                    .Add(New SqlParameter("@Rep_ID_GID_UPDATE", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                Else
                    .Add(New SqlParameter("@Rep_ID_GID_UPDATE", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_ID_GID_UPDATE))
                End If

                .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, lCustid))
                .Add(New SqlParameter("@Custid_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustid_GID))
                .Add(New SqlParameter("@RepName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRepName))
                .Add(New SqlParameter("@Rep_Name_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_Name_Arb))



                .Add(New SqlParameter("@Rep_IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iRep_IDTypeID))

                .Add(New SqlParameter("@Rep_IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_IDNumber))

                .Add(New SqlParameter("@Rep_DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dRep_DateOfIssue))
                .Add(New SqlParameter("@Rep_ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dRep_ExpiryDate))





                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))

                If IsUpdate = False Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
                    False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
                    False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If

                .Add(New SqlParameter("@Rep_ID_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_ID_ot))
                .Add(New SqlParameter("@Rep_ID_GID_ot", SqlDbType.NVarChar, 50, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_Rep_ID_GID_ot))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_ErrorCode))




                .Add(New SqlParameter("@Rep_CELL", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Rep_CELL))
                .Add(New SqlParameter("@Rep_IdPlaceOfIssue", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_IdPlaceOfIssue))
                .Add(New SqlParameter("@Rep_Nationality", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_Nationality))

                .Add(New SqlParameter("@DTCD", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, nDTCD))
                .Add(New SqlParameter("@SBCD", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, nSBCD))



                .Add(New SqlParameter("@Rep_DOB", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, Rep_DOB))
                .Add(New SqlParameter("@Rep_ADD", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_ADD))
                .Add(New SqlParameter("@Rep_Email", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Email))
                .Add(New SqlParameter("@Rep_Remarks", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Remarks))
                .Add(New SqlParameter("@Remarks", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Remarks))
                .Add(New SqlParameter("@user_id_gid", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, userid_gid))

                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 4000, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))



                .Add(New SqlParameter("@risk_Name_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Name_1))
                .Add(New SqlParameter("@risk_Nationality_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Nationality_2))
                .Add(New SqlParameter("@risk_Address_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Address_3))
                .Add(New SqlParameter("@risk_POI_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_POI_4))
                .Add(New SqlParameter("@risk_aux_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_5))
                .Add(New SqlParameter("@risk_aux_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_6))
                .Add(New SqlParameter("@risk_aux_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_7))
                .Add(New SqlParameter("@risk_aux_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_8))


            End With
            loComm.ExecuteNonQuery()
            Dim nrep_id As Integer = 0
            If IsUpdate = False Then

                Rep_ID = (loComm.Parameters.Item("@Rep_ID_ot").Value)
                Rep_ID_GID = CStr(loComm.Parameters.Item("@Rep_ID_GID_ot").Value)

            End If

        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator...")

        End Try


    End Sub



    Public Sub mSave_Beneficiary(ByRef objComm As SqlClient.SqlCommand, ByRef objMN As modMain, _
                                        ByVal dcBeneID As Long, _
                                        ByVal str_BeneID_GID As String, _
                                        ByVal str_CustID_GID As String, _
                                        ByVal str_Custid_Details_GID As String, _
                                        ByVal iCustID As Long, _
                                        ByVal iCustid_Details As Long, _
                                        ByVal str_BenName_Arb As String, _
                                        ByVal str_BenName_First As String, _
                                        ByVal str_BenName_Last As String, _
                                        ByVal str_BenName_Middle As String, _
                                        ByVal str_BankIdentifier As String, _
                                        ByVal str_Bank_1_Identifier As String, _
                                        ByVal str_Bank As String, _
                                        ByVal str_Bank_1 As String, _
                                        ByVal str_Bank_2 As String, _
                                        ByVal str_BankAcc As String, _
                                        ByVal str_BankAcc_1 As String, _
                                        ByVal str_BankAcc_2 As String, _
                                        ByVal str_BankBaranch As String, _
                                        ByVal str_BankBaranch_1 As String, _
                                        ByVal str_BankBaranch_2 As String, _
                                        ByVal str_Address As String, _
                                        ByVal str_Address_1 As String, _
                                        ByVal str_WorkPhoneNo As String, _
                                        ByVal str_HomePhoneNo As String, _
                                        ByVal str_CellPhone As String, _
                                        ByVal CountryID_Nationality_ben As Integer, _
                                        ByVal CountryID_ben As Integer, _
                                        ByVal City_ben As String, _
                                        ByVal str_zip_ben As String, _
                                        ByVal str_email_ben As String, _
                                        ByVal str_reference_ben As String, _
                                        ByVal CCID As Integer, _
                                        ByVal str_USER_GID As String, _
                                        ByRef str_Beneid_ot As String, _
                                        ByRef str_BeneID_GID_ot As String, _
                                        ByVal IsUpdate As Boolean)






        Dim m_Bene_ID As SqlInt64
        Dim m_iErrorCode As SqlInt32


        'UCase(str_BenName_Arb)
        str_BenName_First = UCase(str_BenName_First)
        str_BenName_Last = UCase(str_BenName_Last)
        str_BenName_Middle = UCase(str_BenName_Middle)
        str_BankIdentifier = UCase(str_BankIdentifier)
        str_Bank_1_Identifier = UCase(str_Bank_1_Identifier)
        str_Bank = UCase(str_Bank)
        str_Bank_1 = UCase(str_Bank_1)
        str_Bank_2 = UCase(str_Bank_2)
        str_BankAcc = UCase(str_BankAcc)
        str_BankAcc_1 = UCase(str_BankAcc_1)
        str_BankAcc_2 = UCase(str_BankAcc_2)
        str_Address = UCase(str_Address)
        str_BankBaranch = UCase(str_BankBaranch)
        str_BankBaranch_1 = UCase(str_BankBaranch_1)
        str_BankBaranch_2 = UCase(str_BankBaranch_2)
        str_Address_1 = UCase(str_Address_1)
        str_WorkPhoneNo = UCase(str_WorkPhoneNo)
        str_HomePhoneNo = UCase(str_HomePhoneNo)
        str_CellPhone = UCase(str_CellPhone)
        City_ben = UCase(City_ben)
        str_zip_ben = UCase(str_zip_ben)
        str_email_ben = UCase(str_email_ben)
        str_reference_ben = UCase(str_reference_ben)


        'UCase(City_ben)
        'UCase(str_zip_ben)
        'UCase(str_email_ben)
        'UCase(str_reference_ben)

        Try

            objComm.Parameters.Clear()
            objComm.CommandText = "stp_tblBen_InsertUpdate_GID"
            objComm.CommandType = CommandType.StoredProcedure
            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, 0))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CustID_GID))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Custid_Details_GID))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustID))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))


            Else
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, dcBeneID))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))


            End If
            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@BenName_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Arb))
            objComm.Parameters.Add(New SqlParameter("@BenName_First", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_First))
            objComm.Parameters.Add(New SqlParameter("@BenName_Last", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Last))
            objComm.Parameters.Add(New SqlParameter("@BenName_Middle", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Middle))

            objComm.Parameters.Add(New SqlParameter("@BankIdentifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankIdentifier))
            objComm.Parameters.Add(New SqlParameter("@Bank_1_Identifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))

            'objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))
            'objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            'objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))


            objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1))
            objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))



            objComm.Parameters.Add(New SqlParameter("@BankAcc", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_1))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_2))

            objComm.Parameters.Add(New SqlParameter("@BankBaranch", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_1))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_2))
            objComm.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address))
            objComm.Parameters.Add(New SqlParameter("@Address_1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address_1))
            objComm.Parameters.Add(New SqlParameter("@HomePhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_HomePhoneNo))
            objComm.Parameters.Add(New SqlParameter("@WorkPhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_WorkPhoneNo))
            objComm.Parameters.Add(New SqlParameter("@CellPhone", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CellPhone))
            objComm.Parameters.Add(New SqlParameter("@CountryID_Nationality_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_Nationality_ben))
            objComm.Parameters.Add(New SqlParameter("@CountryID_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_ben))
            objComm.Parameters.Add(New SqlParameter("@City_ben", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, City_ben))
            objComm.Parameters.Add(New SqlParameter("@zip_ben", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_zip_ben))
            objComm.Parameters.Add(New SqlParameter("@email_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_email_ben))
            objComm.Parameters.Add(New SqlParameter("@reference_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_reference_ben))
            objComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CCID))

            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@iBeneid_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_Bene_ID))
            objComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iErrorCode))
            objComm.Parameters.Add(New SqlParameter("@BeneID_GID_ot", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID_ot))


            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            Else
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            End If

            objComm.ExecuteNonQuery()
            If Not IsUpdate Then
                m_Bene_ID = (objComm.Parameters.Item("@iBeneid_ot").Value)
                str_Beneid_ot = CStr(m_Bene_ID.Value.ToString)
                str_BeneID_GID_ot = objComm.Parameters.Item("@BeneID_GID_ot").Value
                m_iErrorCode = New SqlInt32(CType(objComm.Parameters.Item("@iErrorCode").Value, SqlInt32))
            End If
            '**************************************************'**************************************************

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Public Sub mSave_Beneficiary_ALFA_new_2(ByRef objComm As SqlClient.SqlCommand, ByRef objMN As modMain, _
                                    ByVal dcBeneID As Long, _
                                    ByVal str_BeneID_GID As String, _
                                    ByVal str_CustID_GID As String, _
                                    ByVal str_Custid_Details_GID As String, _
                                    ByVal iCustID As Long, _
                                    ByVal iCustid_Details As Long, _
                                    ByVal str_BenName_Arb As String, _
                                    ByVal str_BenName_First As String, _
                                    ByVal str_BenName_Last As String, _
                                    ByVal str_BenName_Middle As String, _
                                    ByVal str_BankIdentifier As String, _
                                    ByVal str_Bank_1_Identifier As String, _
                                    ByVal str_Bank As String, _
                                    ByVal str_Bank_1 As String, _
                                    ByVal str_Bank_2 As String, _
                                    ByVal str_BankAcc As String, _
                                    ByVal str_BankAcc_1 As String, _
                                    ByVal str_BankAcc_2 As String, _
                                    ByVal str_BankBaranch As String, _
                                    ByVal str_BankBaranch_1 As String, _
                                    ByVal str_BankBaranch_2 As String, _
                                    ByVal str_Address As String, _
                                    ByVal str_Address_1 As String, _
                                    ByVal str_WorkPhoneNo As String, _
                                    ByVal str_HomePhoneNo As String, _
                                    ByVal str_CellPhone As String, _
                                    ByVal CountryID_Nationality_ben As Integer, _
                                    ByVal CountryID_ben As Integer, _
                                    ByVal City_ben As String, _
                                    ByVal str_zip_ben As String, _
                                    ByVal str_email_ben As String, _
                                    ByVal str_reference_ben As String, _
                                    ByVal CCID As Integer, _
                                    ByVal str_USER_GID As String, _
                                    ByRef str_Beneid_ot As String, _
                                    ByRef str_BeneID_GID_ot As String, _
                                    ByVal IsUpdate As Boolean, _
 ByVal str_Bene_In_bnk_a As String, _
   ByVal str_Bene_In_bnk_b As String, _
  ByVal str_Bene_In_bnk_c As String, _
  ByVal str_Bene_In_bnk_d As String, _
   ByVal str_Bene_In_bnk_e As String, _
   ByVal str_Bene_In_bnk_f As String, _
  ByVal str_Purpose_a As String, _
   ByVal str_Purpose_b As String, _
   ByVal str_Purpose_c As String, _
   ByVal str_Purpose_d As String, _
   ByVal str_Source_Funds As String, _
   ByVal str_DTCD As String, _
   ByVal str_SBCD As String, _
   ByVal risk_score As Double, _
   ByVal Risk_String As String, _
ByVal EDD As String, _
ByVal RESEARCH As String, _
ByVal THIRD_PARTY As String, _
ByVal Is_EDD As Boolean, _
ByVal Is_RESEARCH As Boolean, _
ByVal Is_THIRD_PARTY As Boolean, _
ByVal purpose_comp As Integer, ByVal Volume_day_fx As Double, _
      ByVal Value_day_fx As Double, _
      ByVal Volume_Month_fx As Double, _
      ByVal Value_Month_fx As Double, _
      ByVal Volume_day_remittance As Double, _
      ByVal Value_day_remittance As Double, _
      ByVal Volume_Month_Remittance As Double, _
      ByVal Value_Month_Remittance As Double, _
        ByVal risk_Name As String,
        ByVal risk_Nationality As String,
        ByVal risk_Country As String,
        ByVal risk_Address As String,
        ByVal risk_Bank_ACC As String,
        ByVal risk_Bank_Identifier As String,
        ByVal risk_Bank_Name As String,
        ByVal risk_Bank_Address As String,
        ByVal risk_Bank_Identifier_INT As String,
        ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String,
        ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String, ByVal check_clear As String)







        Dim m_Bene_ID As SqlInt64
        Dim m_iErrorCode As SqlInt32


        'UCase(str_BenName_Arb)
        str_BenName_First = UCase(str_BenName_First)
        str_BenName_Last = UCase(str_BenName_Last)
        str_BenName_Middle = UCase(str_BenName_Middle)
        str_BankIdentifier = UCase(str_BankIdentifier)
        str_Bank_1_Identifier = UCase(str_Bank_1_Identifier)
        str_Bank = UCase(str_Bank)
        str_Bank_1 = UCase(str_Bank_1)
        str_Bank_2 = UCase(str_Bank_2)
        str_BankAcc = UCase(str_BankAcc)
        str_BankAcc_1 = UCase(str_BankAcc_1)
        str_BankAcc_2 = UCase(str_BankAcc_2)
        str_Address = UCase(str_Address)
        str_BankBaranch = UCase(str_BankBaranch)
        str_BankBaranch_1 = UCase(str_BankBaranch_1)
        str_BankBaranch_2 = UCase(str_BankBaranch_2)
        str_Address_1 = UCase(str_Address_1)
        str_WorkPhoneNo = UCase(str_WorkPhoneNo)
        str_HomePhoneNo = UCase(str_HomePhoneNo)
        str_CellPhone = UCase(str_CellPhone)
        City_ben = UCase(City_ben)
        str_zip_ben = UCase(str_zip_ben)
        str_email_ben = UCase(str_email_ben)
        str_reference_ben = UCase(str_reference_ben)

        str_Bene_In_bnk_a = UCase(str_Bene_In_bnk_a)
        str_Bene_In_bnk_b = UCase(str_Bene_In_bnk_b)
        str_Bene_In_bnk_c = UCase(str_Bene_In_bnk_c)
        str_Bene_In_bnk_d = UCase(str_Bene_In_bnk_d)
        str_Bene_In_bnk_e = UCase(str_Bene_In_bnk_e)
        str_Bene_In_bnk_f = UCase(str_Bene_In_bnk_f)
        str_Purpose_a = UCase(str_Purpose_a)
        str_Purpose_b = UCase(str_Purpose_b)
        str_Purpose_c = UCase(str_Purpose_c)
        str_Purpose_d = UCase(str_Purpose_d)
        str_Source_Funds = UCase(str_Source_Funds)

        Try

            objComm.Parameters.Clear()
            ' objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA"
            objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA_RiskBased_new_2"

            objComm.CommandType = CommandType.StoredProcedure
            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, 0))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CustID_GID))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Custid_Details_GID))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustID))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))


            Else
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, dcBeneID))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))


            End If
            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@BenName_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Arb))
            objComm.Parameters.Add(New SqlParameter("@BenName_First", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_First))
            objComm.Parameters.Add(New SqlParameter("@BenName_Last", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Last))
            objComm.Parameters.Add(New SqlParameter("@BenName_Middle", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Middle))

            objComm.Parameters.Add(New SqlParameter("@BankIdentifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankIdentifier))
            objComm.Parameters.Add(New SqlParameter("@Bank_1_Identifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))

            'objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))
            'objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            'objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))


            objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1))
            objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))



            objComm.Parameters.Add(New SqlParameter("@BankAcc", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_1))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_2))

            objComm.Parameters.Add(New SqlParameter("@BankBaranch", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_1))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_2))
            objComm.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address))
            objComm.Parameters.Add(New SqlParameter("@Address_1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address_1))
            objComm.Parameters.Add(New SqlParameter("@HomePhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_HomePhoneNo))
            objComm.Parameters.Add(New SqlParameter("@WorkPhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_WorkPhoneNo))
            objComm.Parameters.Add(New SqlParameter("@CellPhone", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CellPhone))
            objComm.Parameters.Add(New SqlParameter("@CountryID_Nationality_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_Nationality_ben))
            objComm.Parameters.Add(New SqlParameter("@CountryID_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_ben))
            objComm.Parameters.Add(New SqlParameter("@City_ben", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, City_ben))
            objComm.Parameters.Add(New SqlParameter("@zip_ben", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_zip_ben))
            objComm.Parameters.Add(New SqlParameter("@email_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_email_ben))
            objComm.Parameters.Add(New SqlParameter("@reference_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_reference_ben))
            objComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CCID))

            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@iBeneid_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_Bene_ID))
            objComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iErrorCode))
            objComm.Parameters.Add(New SqlParameter("@BeneID_GID_ot", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID_ot))

            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_a", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_a))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_b", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_b))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_c))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_d))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_e", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_e))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_f", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_f))
            objComm.Parameters.Add(New SqlParameter("@Purpose_a", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_a))
            objComm.Parameters.Add(New SqlParameter("@Purpose_b", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_b))
            objComm.Parameters.Add(New SqlParameter("@Purpose_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_c))
            objComm.Parameters.Add(New SqlParameter("@Purpose_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_d))

            objComm.Parameters.Add(New SqlParameter("@Source_Funds", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Source_Funds))

            objComm.Parameters.Add(New SqlParameter("@DTCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_DTCD))
            objComm.Parameters.Add(New SqlParameter("@SBCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_SBCD))


            objComm.Parameters.Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
            objComm.Parameters.Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))

            objComm.Parameters.Add(New SqlParameter("@EDD", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, EDD))

            objComm.Parameters.Add(New SqlParameter("@Research", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, RESEARCH))
            objComm.Parameters.Add(New SqlParameter("@Third_Party", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, THIRD_PARTY))
            objComm.Parameters.Add(New SqlParameter("@is_EDD", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, Is_EDD))
            objComm.Parameters.Add(New SqlParameter("@is_Research", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, Is_RESEARCH))
            objComm.Parameters.Add(New SqlParameter("@is_Third_Party", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, Is_THIRD_PARTY))
            objComm.Parameters.Add(New SqlParameter("@purpose_comp", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, purpose_comp))
            ' ''        ByVal risk_score As Double, _
            ' ''ByVal Risk_String As String)


            objComm.Parameters.Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
            objComm.Parameters.Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
            objComm.Parameters.Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
            objComm.Parameters.Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

            objComm.Parameters.Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
            objComm.Parameters.Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
            objComm.Parameters.Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
            objComm.Parameters.Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))

            objComm.Parameters.Add(New SqlParameter("@risk_Name_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Name))
            objComm.Parameters.Add(New SqlParameter("@risk_Nationality_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Nationality))
            objComm.Parameters.Add(New SqlParameter("@risk_Country_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Country))
            objComm.Parameters.Add(New SqlParameter("@risk_Address_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Address))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_ACC_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_ACC))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Identifier_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Identifier))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Name_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Name))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Address_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Address))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Identifier_INT_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Identifier_INT))

            objComm.Parameters.Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))
            objComm.Parameters.Add(New SqlParameter("@check_clear", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, check_clear))



            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            Else
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            End If

            objComm.ExecuteNonQuery()
            If Not IsUpdate Then
                m_Bene_ID = (objComm.Parameters.Item("@iBeneid_ot").Value)
                str_Beneid_ot = CStr(m_Bene_ID.Value.ToString)
                str_BeneID_GID_ot = objComm.Parameters.Item("@BeneID_GID_ot").Value
                m_iErrorCode = New SqlInt32(CType(objComm.Parameters.Item("@iErrorCode").Value, SqlInt32))
            End If
            '**************************************************'**************************************************
            '**************************************************'**************************************************

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub


    Public Sub mSave_Beneficiary_ALFA_new(ByRef objComm As SqlClient.SqlCommand, ByRef objMN As modMain, _
                                  ByVal dcBeneID As Long, _
                                  ByVal str_BeneID_GID As String, _
                                  ByVal str_CustID_GID As String, _
                                  ByVal str_Custid_Details_GID As String, _
                                  ByVal iCustID As Long, _
                                  ByVal iCustid_Details As Long, _
                                  ByVal str_BenName_Arb As String, _
                                  ByVal str_BenName_First As String, _
                                  ByVal str_BenName_Last As String, _
                                  ByVal str_BenName_Middle As String, _
                                  ByVal str_BankIdentifier As String, _
                                  ByVal str_Bank_1_Identifier As String, _
                                  ByVal str_Bank As String, _
                                  ByVal str_Bank_1 As String, _
                                  ByVal str_Bank_2 As String, _
                                  ByVal str_BankAcc As String, _
                                  ByVal str_BankAcc_1 As String, _
                                  ByVal str_BankAcc_2 As String, _
                                  ByVal str_BankBaranch As String, _
                                  ByVal str_BankBaranch_1 As String, _
                                  ByVal str_BankBaranch_2 As String, _
                                  ByVal str_Address As String, _
                                  ByVal str_Address_1 As String, _
                                  ByVal str_WorkPhoneNo As String, _
                                  ByVal str_HomePhoneNo As String, _
                                  ByVal str_CellPhone As String, _
                                  ByVal CountryID_Nationality_ben As Integer, _
                                  ByVal CountryID_ben As Integer, _
                                  ByVal City_ben As String, _
                                  ByVal str_zip_ben As String, _
                                  ByVal str_email_ben As String, _
                                  ByVal str_reference_ben As String, _
                                  ByVal CCID As Integer, _
                                  ByVal str_USER_GID As String, _
                                  ByRef str_Beneid_ot As String, _
                                  ByRef str_BeneID_GID_ot As String, _
                                  ByVal IsUpdate As Boolean, _
ByVal str_Bene_In_bnk_a As String, _
 ByVal str_Bene_In_bnk_b As String, _
ByVal str_Bene_In_bnk_c As String, _
ByVal str_Bene_In_bnk_d As String, _
 ByVal str_Bene_In_bnk_e As String, _
 ByVal str_Bene_In_bnk_f As String, _
ByVal str_Purpose_a As String, _
 ByVal str_Purpose_b As String, _
 ByVal str_Purpose_c As String, _
 ByVal str_Purpose_d As String, _
 ByVal str_Source_Funds As String, _
 ByVal str_DTCD As String, _
 ByVal str_SBCD As String, _
 ByVal risk_score As Double, _
 ByVal Risk_String As String, _
ByVal EDD As String, _
ByVal RESEARCH As String, _
ByVal THIRD_PARTY As String, _
ByVal Is_EDD As Boolean, _
ByVal Is_RESEARCH As Boolean, _
ByVal Is_THIRD_PARTY As Boolean, _
ByVal purpose_comp As Integer)








        Dim m_Bene_ID As SqlInt64
        Dim m_iErrorCode As SqlInt32


        'UCase(str_BenName_Arb)
        str_BenName_First = UCase(str_BenName_First)
        str_BenName_Last = UCase(str_BenName_Last)
        str_BenName_Middle = UCase(str_BenName_Middle)
        str_BankIdentifier = UCase(str_BankIdentifier)
        str_Bank_1_Identifier = UCase(str_Bank_1_Identifier)
        str_Bank = UCase(str_Bank)
        str_Bank_1 = UCase(str_Bank_1)
        str_Bank_2 = UCase(str_Bank_2)
        str_BankAcc = UCase(str_BankAcc)
        str_BankAcc_1 = UCase(str_BankAcc_1)
        str_BankAcc_2 = UCase(str_BankAcc_2)
        str_Address = UCase(str_Address)
        str_BankBaranch = UCase(str_BankBaranch)
        str_BankBaranch_1 = UCase(str_BankBaranch_1)
        str_BankBaranch_2 = UCase(str_BankBaranch_2)
        str_Address_1 = UCase(str_Address_1)
        str_WorkPhoneNo = UCase(str_WorkPhoneNo)
        str_HomePhoneNo = UCase(str_HomePhoneNo)
        str_CellPhone = UCase(str_CellPhone)
        City_ben = UCase(City_ben)
        str_zip_ben = UCase(str_zip_ben)
        str_email_ben = UCase(str_email_ben)
        str_reference_ben = UCase(str_reference_ben)

        str_Bene_In_bnk_a = UCase(str_Bene_In_bnk_a)
        str_Bene_In_bnk_b = UCase(str_Bene_In_bnk_b)
        str_Bene_In_bnk_c = UCase(str_Bene_In_bnk_c)
        str_Bene_In_bnk_d = UCase(str_Bene_In_bnk_d)
        str_Bene_In_bnk_e = UCase(str_Bene_In_bnk_e)
        str_Bene_In_bnk_f = UCase(str_Bene_In_bnk_f)
        str_Purpose_a = UCase(str_Purpose_a)
        str_Purpose_b = UCase(str_Purpose_b)
        str_Purpose_c = UCase(str_Purpose_c)
        str_Purpose_d = UCase(str_Purpose_d)
        str_Source_Funds = UCase(str_Source_Funds)

        Try

            objComm.Parameters.Clear()
            ' objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA"
            objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA_RiskBased_new"

            objComm.CommandType = CommandType.StoredProcedure
            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, 0))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CustID_GID))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Custid_Details_GID))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustID))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))


            Else
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, dcBeneID))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))


            End If
            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@BenName_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Arb))
            objComm.Parameters.Add(New SqlParameter("@BenName_First", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_First))
            objComm.Parameters.Add(New SqlParameter("@BenName_Last", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Last))
            objComm.Parameters.Add(New SqlParameter("@BenName_Middle", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Middle))

            objComm.Parameters.Add(New SqlParameter("@BankIdentifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankIdentifier))
            objComm.Parameters.Add(New SqlParameter("@Bank_1_Identifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))

            'objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))
            'objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            'objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))


            objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1))
            objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))



            objComm.Parameters.Add(New SqlParameter("@BankAcc", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_1))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_2))

            objComm.Parameters.Add(New SqlParameter("@BankBaranch", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_1))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_2))
            objComm.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address))
            objComm.Parameters.Add(New SqlParameter("@Address_1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address_1))
            objComm.Parameters.Add(New SqlParameter("@HomePhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_HomePhoneNo))
            objComm.Parameters.Add(New SqlParameter("@WorkPhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_WorkPhoneNo))
            objComm.Parameters.Add(New SqlParameter("@CellPhone", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CellPhone))
            objComm.Parameters.Add(New SqlParameter("@CountryID_Nationality_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_Nationality_ben))
            objComm.Parameters.Add(New SqlParameter("@CountryID_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_ben))
            objComm.Parameters.Add(New SqlParameter("@City_ben", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, City_ben))
            objComm.Parameters.Add(New SqlParameter("@zip_ben", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_zip_ben))
            objComm.Parameters.Add(New SqlParameter("@email_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_email_ben))
            objComm.Parameters.Add(New SqlParameter("@reference_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_reference_ben))
            objComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CCID))

            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@iBeneid_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_Bene_ID))
            objComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iErrorCode))
            objComm.Parameters.Add(New SqlParameter("@BeneID_GID_ot", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID_ot))

            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_a", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_a))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_b", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_b))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_c))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_d))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_e", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_e))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_f", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_f))
            objComm.Parameters.Add(New SqlParameter("@Purpose_a", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_a))
            objComm.Parameters.Add(New SqlParameter("@Purpose_b", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_b))
            objComm.Parameters.Add(New SqlParameter("@Purpose_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_c))
            objComm.Parameters.Add(New SqlParameter("@Purpose_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_d))

            objComm.Parameters.Add(New SqlParameter("@Source_Funds", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Source_Funds))

            objComm.Parameters.Add(New SqlParameter("@DTCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_DTCD))
            objComm.Parameters.Add(New SqlParameter("@SBCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_SBCD))


            objComm.Parameters.Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
            objComm.Parameters.Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))

            objComm.Parameters.Add(New SqlParameter("@EDD", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, EDD))

            objComm.Parameters.Add(New SqlParameter("@Research", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, RESEARCH))
            objComm.Parameters.Add(New SqlParameter("@Third_Party", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, THIRD_PARTY))
            objComm.Parameters.Add(New SqlParameter("@is_EDD", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, Is_EDD))
            objComm.Parameters.Add(New SqlParameter("@is_Research", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, Is_RESEARCH))
            objComm.Parameters.Add(New SqlParameter("@is_Third_Party", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, Is_THIRD_PARTY))
            objComm.Parameters.Add(New SqlParameter("@purpose_comp", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, purpose_comp))
            ' ''        ByVal risk_score As Double, _
            ' ''ByVal Risk_String As String)






            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            Else
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            End If

            objComm.ExecuteNonQuery()
            If Not IsUpdate Then
                m_Bene_ID = (objComm.Parameters.Item("@iBeneid_ot").Value)
                str_Beneid_ot = CStr(m_Bene_ID.Value.ToString)
                str_BeneID_GID_ot = objComm.Parameters.Item("@BeneID_GID_ot").Value
                m_iErrorCode = New SqlInt32(CType(objComm.Parameters.Item("@iErrorCode").Value, SqlInt32))
            End If
            '**************************************************'**************************************************
            '**************************************************'**************************************************

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub




    Public Sub mSave_Beneficiary_ALFA(ByRef objComm As SqlClient.SqlCommand, ByRef objMN As modMain, _
                                        ByVal dcBeneID As Long, _
                                        ByVal str_BeneID_GID As String, _
                                        ByVal str_CustID_GID As String, _
                                        ByVal str_Custid_Details_GID As String, _
                                        ByVal iCustID As Long, _
                                        ByVal iCustid_Details As Long, _
                                        ByVal str_BenName_Arb As String, _
                                        ByVal str_BenName_First As String, _
                                        ByVal str_BenName_Last As String, _
                                        ByVal str_BenName_Middle As String, _
                                        ByVal str_BankIdentifier As String, _
                                        ByVal str_Bank_1_Identifier As String, _
                                        ByVal str_Bank As String, _
                                        ByVal str_Bank_1 As String, _
                                        ByVal str_Bank_2 As String, _
                                        ByVal str_BankAcc As String, _
                                        ByVal str_BankAcc_1 As String, _
                                        ByVal str_BankAcc_2 As String, _
                                        ByVal str_BankBaranch As String, _
                                        ByVal str_BankBaranch_1 As String, _
                                        ByVal str_BankBaranch_2 As String, _
                                        ByVal str_Address As String, _
                                        ByVal str_Address_1 As String, _
                                        ByVal str_WorkPhoneNo As String, _
                                        ByVal str_HomePhoneNo As String, _
                                        ByVal str_CellPhone As String, _
                                        ByVal CountryID_Nationality_ben As Integer, _
                                        ByVal CountryID_ben As Integer, _
                                        ByVal City_ben As String, _
                                        ByVal str_zip_ben As String, _
                                        ByVal str_email_ben As String, _
                                        ByVal str_reference_ben As String, _
                                        ByVal CCID As Integer, _
                                        ByVal str_USER_GID As String, _
                                        ByRef str_Beneid_ot As String, _
                                        ByRef str_BeneID_GID_ot As String, _
                                        ByVal IsUpdate As Boolean, _
     ByVal str_Bene_In_bnk_a As String, _
       ByVal str_Bene_In_bnk_b As String, _
      ByVal str_Bene_In_bnk_c As String, _
      ByVal str_Bene_In_bnk_d As String, _
       ByVal str_Bene_In_bnk_e As String, _
       ByVal str_Bene_In_bnk_f As String, _
      ByVal str_Purpose_a As String, _
       ByVal str_Purpose_b As String, _
       ByVal str_Purpose_c As String, _
       ByVal str_Purpose_d As String, _
       ByVal str_Source_Funds As String, _
       ByVal str_DTCD As String, _
       ByVal str_SBCD As String)






        Dim m_Bene_ID As SqlInt64
        Dim m_iErrorCode As SqlInt32


        'UCase(str_BenName_Arb)
        str_BenName_First = UCase(str_BenName_First)
        str_BenName_Last = UCase(str_BenName_Last)
        str_BenName_Middle = UCase(str_BenName_Middle)
        str_BankIdentifier = UCase(str_BankIdentifier)
        str_Bank_1_Identifier = UCase(str_Bank_1_Identifier)
        str_Bank = UCase(str_Bank)
        str_Bank_1 = UCase(str_Bank_1)
        str_Bank_2 = UCase(str_Bank_2)
        str_BankAcc = UCase(str_BankAcc)
        str_BankAcc_1 = UCase(str_BankAcc_1)
        str_BankAcc_2 = UCase(str_BankAcc_2)
        str_Address = UCase(str_Address)
        str_BankBaranch = UCase(str_BankBaranch)
        str_BankBaranch_1 = UCase(str_BankBaranch_1)
        str_BankBaranch_2 = UCase(str_BankBaranch_2)
        str_Address_1 = UCase(str_Address_1)
        str_WorkPhoneNo = UCase(str_WorkPhoneNo)
        str_HomePhoneNo = UCase(str_HomePhoneNo)
        str_CellPhone = UCase(str_CellPhone)
        City_ben = UCase(City_ben)
        str_zip_ben = UCase(str_zip_ben)
        str_email_ben = UCase(str_email_ben)
        str_reference_ben = UCase(str_reference_ben)

        str_Bene_In_bnk_a = UCase(str_Bene_In_bnk_a)
        str_Bene_In_bnk_b = UCase(str_Bene_In_bnk_b)
        str_Bene_In_bnk_c = UCase(str_Bene_In_bnk_c)
        str_Bene_In_bnk_d = UCase(str_Bene_In_bnk_d)
        str_Bene_In_bnk_e = UCase(str_Bene_In_bnk_e)
        str_Bene_In_bnk_f = UCase(str_Bene_In_bnk_f)
        str_Purpose_a = UCase(str_Purpose_a)
        str_Purpose_b = UCase(str_Purpose_b)
        str_Purpose_c = UCase(str_Purpose_c)
        str_Purpose_d = UCase(str_Purpose_d)
        str_Source_Funds = UCase(str_Source_Funds)

        Try

            objComm.Parameters.Clear()
            objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA"
            objComm.CommandType = CommandType.StoredProcedure
            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, 0))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CustID_GID))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Custid_Details_GID))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustID))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))


            Else
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, dcBeneID))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))


            End If
            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@BenName_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Arb))
            objComm.Parameters.Add(New SqlParameter("@BenName_First", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_First))
            objComm.Parameters.Add(New SqlParameter("@BenName_Last", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Last))
            objComm.Parameters.Add(New SqlParameter("@BenName_Middle", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Middle))

            objComm.Parameters.Add(New SqlParameter("@BankIdentifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankIdentifier))
            objComm.Parameters.Add(New SqlParameter("@Bank_1_Identifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))

            'objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))
            'objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            'objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))


            objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1))
            objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))



            objComm.Parameters.Add(New SqlParameter("@BankAcc", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_1))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_2))

            objComm.Parameters.Add(New SqlParameter("@BankBaranch", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_1))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_2))
            objComm.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address))
            objComm.Parameters.Add(New SqlParameter("@Address_1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address_1))
            objComm.Parameters.Add(New SqlParameter("@HomePhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_HomePhoneNo))
            objComm.Parameters.Add(New SqlParameter("@WorkPhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_WorkPhoneNo))
            objComm.Parameters.Add(New SqlParameter("@CellPhone", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CellPhone))
            objComm.Parameters.Add(New SqlParameter("@CountryID_Nationality_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_Nationality_ben))
            objComm.Parameters.Add(New SqlParameter("@CountryID_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_ben))
            objComm.Parameters.Add(New SqlParameter("@City_ben", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, City_ben))
            objComm.Parameters.Add(New SqlParameter("@zip_ben", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_zip_ben))
            objComm.Parameters.Add(New SqlParameter("@email_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_email_ben))
            objComm.Parameters.Add(New SqlParameter("@reference_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_reference_ben))
            objComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CCID))

            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@iBeneid_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_Bene_ID))
            objComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iErrorCode))
            objComm.Parameters.Add(New SqlParameter("@BeneID_GID_ot", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID_ot))

            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_a", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_a))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_b", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_b))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_c))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_d))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_e", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_e))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_f", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_f))
            objComm.Parameters.Add(New SqlParameter("@Purpose_a", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_a))
            objComm.Parameters.Add(New SqlParameter("@Purpose_b", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_b))
            objComm.Parameters.Add(New SqlParameter("@Purpose_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_c))
            objComm.Parameters.Add(New SqlParameter("@Purpose_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_d))

            objComm.Parameters.Add(New SqlParameter("@Source_Funds", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Source_Funds))

            objComm.Parameters.Add(New SqlParameter("@DTCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_DTCD))
            objComm.Parameters.Add(New SqlParameter("@SBCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_SBCD))

            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            Else
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            End If

            objComm.ExecuteNonQuery()
            If Not IsUpdate Then
                m_Bene_ID = (objComm.Parameters.Item("@iBeneid_ot").Value)
                str_Beneid_ot = CStr(m_Bene_ID.Value.ToString)
                str_BeneID_GID_ot = objComm.Parameters.Item("@BeneID_GID_ot").Value
                m_iErrorCode = New SqlInt32(CType(objComm.Parameters.Item("@iErrorCode").Value, SqlInt32))
            End If
            '**************************************************'**************************************************
            '**************************************************'**************************************************

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Sub mSave_Beneficiary_ALFA_CB(ByRef objComm As SqlClient.SqlCommand, ByRef objMN As modMain, _
                                       ByVal dcBeneID As Long, _
                                       ByVal str_BeneID_GID As String, _
                                       ByVal str_CustID_GID As String, _
                                       ByVal str_Custid_Details_GID As String, _
                                       ByVal iCustID As Long, _
                                       ByVal iCustid_Details As Long, _
                                       ByVal str_BenName_Arb As String, _
                                       ByVal str_BenName_First As String, _
                                       ByVal str_BenName_Last As String, _
                                       ByVal str_BenName_Middle As String, _
                                       ByVal str_BankIdentifier As String, _
                                       ByVal str_Bank_1_Identifier As String, _
                                       ByVal str_Bank As String, _
                                       ByVal str_Bank_1 As String, _
                                       ByVal str_Bank_2 As String, _
                                       ByVal str_BankAcc As String, _
                                       ByVal str_BankAcc_1 As String, _
                                       ByVal str_BankAcc_2 As String, _
                                       ByVal str_BankBaranch As String, _
                                       ByVal str_BankBaranch_1 As String, _
                                       ByVal str_BankBaranch_2 As String, _
                                       ByVal str_Address As String, _
                                       ByVal str_Address_1 As String, _
                                       ByVal str_WorkPhoneNo As String, _
                                       ByVal str_HomePhoneNo As String, _
                                       ByVal str_CellPhone As String, _
                                       ByVal CountryID_Nationality_ben As Integer, _
                                       ByVal CountryID_ben As Integer, _
                                       ByVal City_ben As String, _
                                       ByVal str_zip_ben As String, _
                                       ByVal str_email_ben As String, _
                                       ByVal str_reference_ben As String, _
                                       ByVal CCID As Integer, _
                                       ByVal str_USER_GID As String, _
                                       ByRef str_Beneid_ot As String, _
                                       ByRef str_BeneID_GID_ot As String, _
                                       ByVal IsUpdate As Boolean, _
                                    ByVal str_Bene_In_bnk_a As String, _
                                      ByVal str_Bene_In_bnk_b As String, _
                                     ByVal str_Bene_In_bnk_c As String, _
                                     ByVal str_Bene_In_bnk_d As String, _
                                      ByVal str_Bene_In_bnk_e As String, _
                                      ByVal str_Bene_In_bnk_f As String, _
                                     ByVal str_Purpose_a As String, _
                                      ByVal str_Purpose_b As String, _
                                      ByVal str_Purpose_c As String, _
                                      ByVal str_Purpose_d As String, _
                                      ByVal str_Source_Funds As String, _
                                      ByVal str_DTCD As String, _
                                      ByVal str_SBCD As String, _
                                    ByVal BEN_ID_NO As String,
                                      ByVal BEN_ID_TYPE As Integer,
                                      ByVal BEN_ID_EXPIRY As Date,
                                      ByVal BEN_CITY As String,
                                      ByVal BEN_NEARAIRPORT As String,
                                      ByVal BEN_RELATION As Integer,
                                      ByVal CB_BEN_CityID As Integer,
                                      ByVal CB_BEN_PAYMODE As String,
                                      ByVal CB_BEN_PAYMODE_DESC As String)






        Dim m_Bene_ID As SqlInt64
        Dim m_iErrorCode As SqlInt32


        'UCase(str_BenName_Arb)
        str_BenName_First = UCase(str_BenName_First)
        str_BenName_Last = UCase(str_BenName_Last)
        str_BenName_Middle = UCase(str_BenName_Middle)
        str_BankIdentifier = UCase(str_BankIdentifier)
        str_Bank_1_Identifier = UCase(str_Bank_1_Identifier)
        str_Bank = UCase(str_Bank)
        str_Bank_1 = UCase(str_Bank_1)
        str_Bank_2 = UCase(str_Bank_2)
        str_BankAcc = UCase(str_BankAcc)
        str_BankAcc_1 = UCase(str_BankAcc_1)
        str_BankAcc_2 = UCase(str_BankAcc_2)
        str_Address = UCase(str_Address)
        str_BankBaranch = UCase(str_BankBaranch)
        str_BankBaranch_1 = UCase(str_BankBaranch_1)
        str_BankBaranch_2 = UCase(str_BankBaranch_2)
        str_Address_1 = UCase(str_Address_1)
        str_WorkPhoneNo = UCase(str_WorkPhoneNo)
        str_HomePhoneNo = UCase(str_HomePhoneNo)
        str_CellPhone = UCase(str_CellPhone)
        City_ben = UCase(City_ben)
        str_zip_ben = UCase(str_zip_ben)
        str_email_ben = UCase(str_email_ben)
        str_reference_ben = UCase(str_reference_ben)

        str_Bene_In_bnk_a = UCase(str_Bene_In_bnk_a)
        str_Bene_In_bnk_b = UCase(str_Bene_In_bnk_b)
        str_Bene_In_bnk_c = UCase(str_Bene_In_bnk_c)
        str_Bene_In_bnk_d = UCase(str_Bene_In_bnk_d)
        str_Bene_In_bnk_e = UCase(str_Bene_In_bnk_e)
        str_Bene_In_bnk_f = UCase(str_Bene_In_bnk_f)
        str_Purpose_a = UCase(str_Purpose_a)
        str_Purpose_b = UCase(str_Purpose_b)
        str_Purpose_c = UCase(str_Purpose_c)
        str_Purpose_d = UCase(str_Purpose_d)
        str_Source_Funds = UCase(str_Source_Funds)

        Try

            objComm.Parameters.Clear()
            objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA_CB"
            objComm.CommandType = CommandType.StoredProcedure
            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, 0))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CustID_GID))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Custid_Details_GID))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustID))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))


            Else
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, dcBeneID))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))


            End If
            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@BenName_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Arb))
            objComm.Parameters.Add(New SqlParameter("@BenName_First", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_First))
            objComm.Parameters.Add(New SqlParameter("@BenName_Last", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Last))
            objComm.Parameters.Add(New SqlParameter("@BenName_Middle", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Middle))

            objComm.Parameters.Add(New SqlParameter("@BankIdentifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankIdentifier))
            objComm.Parameters.Add(New SqlParameter("@Bank_1_Identifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))

            'objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))
            'objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            'objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))


            objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1))
            objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))



            objComm.Parameters.Add(New SqlParameter("@BankAcc", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_1))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_2))

            objComm.Parameters.Add(New SqlParameter("@BankBaranch", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_1))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_2))
            objComm.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address))
            objComm.Parameters.Add(New SqlParameter("@Address_1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address_1))
            objComm.Parameters.Add(New SqlParameter("@HomePhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_HomePhoneNo))
            objComm.Parameters.Add(New SqlParameter("@WorkPhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_WorkPhoneNo))
            objComm.Parameters.Add(New SqlParameter("@CellPhone", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CellPhone))
            objComm.Parameters.Add(New SqlParameter("@CountryID_Nationality_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_Nationality_ben))
            objComm.Parameters.Add(New SqlParameter("@CountryID_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_ben))
            objComm.Parameters.Add(New SqlParameter("@City_ben", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, City_ben))
            objComm.Parameters.Add(New SqlParameter("@zip_ben", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_zip_ben))
            objComm.Parameters.Add(New SqlParameter("@email_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_email_ben))
            objComm.Parameters.Add(New SqlParameter("@reference_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_reference_ben))
            objComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CCID))

            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@iBeneid_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_Bene_ID))
            objComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iErrorCode))
            objComm.Parameters.Add(New SqlParameter("@BeneID_GID_ot", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID_ot))

            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_a", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_a))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_b", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_b))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_c))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_d))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_e", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_e))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_f", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_f))
            objComm.Parameters.Add(New SqlParameter("@Purpose_a", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_a))
            objComm.Parameters.Add(New SqlParameter("@Purpose_b", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_b))
            objComm.Parameters.Add(New SqlParameter("@Purpose_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_c))
            objComm.Parameters.Add(New SqlParameter("@Purpose_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_d))

            objComm.Parameters.Add(New SqlParameter("@Source_Funds", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Source_Funds))

            objComm.Parameters.Add(New SqlParameter("@DTCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_DTCD))
            objComm.Parameters.Add(New SqlParameter("@SBCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_SBCD))



            ''''''-----------CB FEILDS

            objComm.Parameters.Add(New SqlParameter("@BEN_ID_NO", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_ID_NO))
            objComm.Parameters.Add(New SqlParameter("@BEN_ID_TYPE", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_ID_TYPE))
            objComm.Parameters.Add(New SqlParameter("@BEN_ID_EXPIRY", SqlDbType.Date, 12, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_ID_EXPIRY))
            objComm.Parameters.Add(New SqlParameter("@BEN_CITY", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_CITY))
            objComm.Parameters.Add(New SqlParameter("@BEN_NEARAIRPORT", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_NEARAIRPORT))
            objComm.Parameters.Add(New SqlParameter("@BEN_RELATION", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_RELATION))


            objComm.Parameters.Add(New SqlParameter("@CB_BEN_CityID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_BEN_CityID))
            objComm.Parameters.Add(New SqlParameter("@CB_BEN_PAYMODE", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_BEN_PAYMODE))
            objComm.Parameters.Add(New SqlParameter("@CB_BEN_PAYMODE_DESC", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_BEN_PAYMODE_DESC))

            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            Else
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            End If

            objComm.ExecuteNonQuery()
            If Not IsUpdate Then
                m_Bene_ID = (objComm.Parameters.Item("@iBeneid_ot").Value)
                str_Beneid_ot = CStr(m_Bene_ID.Value.ToString)
                str_BeneID_GID_ot = objComm.Parameters.Item("@BeneID_GID_ot").Value
                m_iErrorCode = New SqlInt32(CType(objComm.Parameters.Item("@iErrorCode").Value, SqlInt32))
            End If
            '**************************************************'**************************************************
            '**************************************************'**************************************************

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub




    Public Sub mSave_Beneficiary_DTCD(ByRef objComm As SqlClient.SqlCommand, ByRef objMN As modMain, _
                                        ByVal dcBeneID As Long, _
                                        ByVal str_BeneID_GID As String, _
                                        ByVal str_CustID_GID As String, _
                                        ByVal str_Custid_Details_GID As String, _
                                        ByVal iCustID As Long, _
                                        ByVal iCustid_Details As Long, _
                                        ByVal str_BenName_Arb As String, _
                                        ByVal str_BenName_First As String, _
                                        ByVal str_BenName_Last As String, _
                                        ByVal str_BenName_Middle As String, _
                                        ByVal str_BankIdentifier As String, _
                                        ByVal str_Bank_1_Identifier As String, _
                                        ByVal str_Bank As String, _
                                        ByVal str_Bank_1 As String, _
                                        ByVal str_Bank_2 As String, _
                                        ByVal str_BankAcc As String, _
                                        ByVal str_BankAcc_1 As String, _
                                        ByVal str_BankAcc_2 As String, _
                                        ByVal str_BankBaranch As String, _
                                        ByVal str_BankBaranch_1 As String, _
                                        ByVal str_BankBaranch_2 As String, _
                                        ByVal str_Address As String, _
                                        ByVal str_Address_1 As String, _
                                        ByVal str_WorkPhoneNo As String, _
                                        ByVal str_HomePhoneNo As String, _
                                        ByVal str_CellPhone As String, _
                                        ByVal CountryID_Nationality_ben As Integer, _
                                        ByVal CountryID_ben As Integer, _
                                        ByVal City_ben As String, _
                                        ByVal str_zip_ben As String, _
                                        ByVal str_email_ben As String, _
                                        ByVal str_reference_ben As String, _
                                        ByVal CCID As Integer, _
                                        ByVal DTCD As Integer, _
                                        ByVal SBCD As Integer, _
                                        ByVal str_USER_GID As String, _
                                        ByRef str_Beneid_ot As String, _
                                        ByRef str_BeneID_GID_ot As String, _
                                        ByVal IsUpdate As Boolean)






        Dim m_Bene_ID As SqlInt64
        Dim m_iErrorCode As SqlInt32


        'UCase(str_BenName_Arb)
        'UCase(str_BenName_First)
        'UCase(str_BenName_Last)
        'UCase(str_BenName_Middle)
        'UCase(str_BankIdentifier)
        'UCase(str_Bank_1_Identifier)
        'UCase(str_Bank)
        'UCase(str_Bank_1)
        'UCase(str_Bank_2)
        'UCase(str_BankAcc)
        'UCase(str_BankAcc_1)
        'UCase(str_BankAcc_2)
        'UCase(str_Address)
        'UCase(str_Address_1)
        'UCase(str_WorkPhoneNo)
        'UCase(str_HomePhoneNo)
        'UCase(str_CellPhone)
        'UCase(City_ben)
        'UCase(str_zip_ben)
        'UCase(str_email_ben)
        'UCase(str_reference_ben)
        str_BenName_First = UCase(str_BenName_First)
        str_BenName_Last = UCase(str_BenName_Last)
        str_BenName_Middle = UCase(str_BenName_Middle)
        str_BankIdentifier = UCase(str_BankIdentifier)
        str_Bank_1_Identifier = UCase(str_Bank_1_Identifier)
        str_Bank = UCase(str_Bank)
        str_Bank_1 = UCase(str_Bank_1)
        str_Bank_2 = UCase(str_Bank_2)
        str_BankAcc = UCase(str_BankAcc)
        str_BankAcc_1 = UCase(str_BankAcc_1)
        str_BankAcc_2 = UCase(str_BankAcc_2)
        str_Address = UCase(str_Address)
        str_Address_1 = UCase(str_Address_1)
        str_WorkPhoneNo = UCase(str_WorkPhoneNo)
        str_HomePhoneNo = UCase(str_HomePhoneNo)
        str_CellPhone = UCase(str_CellPhone)
        City_ben = UCase(City_ben)
        str_zip_ben = UCase(str_zip_ben)
        str_email_ben = UCase(str_email_ben)
        str_reference_ben = UCase(str_reference_ben)
        Try

            objComm.Parameters.Clear()
            objComm.CommandText = "stp_tblBen_InsertUpdate_DTCD_GID"
            objComm.CommandType = CommandType.StoredProcedure
            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, 0))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CustID_GID))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Custid_Details_GID))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustID))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))


            Else
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, dcBeneID))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))


            End If
            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@BenName_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Arb))
            objComm.Parameters.Add(New SqlParameter("@BenName_First", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_First))
            objComm.Parameters.Add(New SqlParameter("@BenName_Last", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Last))
            objComm.Parameters.Add(New SqlParameter("@BenName_Middle", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Middle))
            objComm.Parameters.Add(New SqlParameter("@BankIdentifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankIdentifier))
            objComm.Parameters.Add(New SqlParameter("@Bank_1_Identifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))
            objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1))
            objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))
            objComm.Parameters.Add(New SqlParameter("@BankAcc", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_1))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_2))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_1))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_2))
            objComm.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address))
            objComm.Parameters.Add(New SqlParameter("@Address_1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address_1))
            objComm.Parameters.Add(New SqlParameter("@HomePhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_HomePhoneNo))
            objComm.Parameters.Add(New SqlParameter("@WorkPhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_WorkPhoneNo))
            objComm.Parameters.Add(New SqlParameter("@CellPhone", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CellPhone))
            objComm.Parameters.Add(New SqlParameter("@CountryID_Nationality_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_Nationality_ben))
            objComm.Parameters.Add(New SqlParameter("@CountryID_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_ben))
            objComm.Parameters.Add(New SqlParameter("@City_ben", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, City_ben))
            objComm.Parameters.Add(New SqlParameter("@zip_ben", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_zip_ben))
            objComm.Parameters.Add(New SqlParameter("@email_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_email_ben))
            objComm.Parameters.Add(New SqlParameter("@reference_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_reference_ben))
            objComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CCID))


            objComm.Parameters.Add(New SqlParameter("@dtcd_ref", SqlDbType.SmallInt, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, DTCD))
            objComm.Parameters.Add(New SqlParameter("@sbcd_ref", SqlDbType.SmallInt, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, SBCD))




            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@iBeneid_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_Bene_ID))
            objComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iErrorCode))
            objComm.Parameters.Add(New SqlParameter("@BeneID_GID_ot", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID_ot))


            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            Else
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            End If

            objComm.ExecuteNonQuery()
            If Not IsUpdate Then
                m_Bene_ID = (objComm.Parameters.Item("@iBeneid_ot").Value)
                str_Beneid_ot = CStr(m_Bene_ID.Value.ToString)
                str_BeneID_GID_ot = objComm.Parameters.Item("@BeneID_GID_ot").Value
                m_iErrorCode = New SqlInt32(CType(objComm.Parameters.Item("@iErrorCode").Value, SqlInt32))
            End If
            '**************************************************'**************************************************

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub






    Public Sub mSave_Cust_detail_RiskProfile_2(ByRef loComm As SqlCommand, ByVal iCustid_Details As Long, _
                      ByVal sCustID_Details_GID_Update As String, _
                      ByVal iCustid As String, _
                      ByVal sCustID_GID As String, _
                      ByVal sPlaceOfIssue As String, _
                      ByVal iIDTypeID As Integer, _
                      ByVal sIDNumber As String, _
                      ByVal dDateOfIssue As Date, _
                      ByVal dExpiryDate As Date, _
                      ByVal sAddress1 As String, _
                      ByVal sAddress2 As String, _
                      ByVal sCity As String, _
                      ByVal sTel As String, _
                      ByVal sFax As String, _
                      ByVal sZIP As String, _
                      ByVal sEmail As String, _
                      ByVal sSponserName As String, _
                      ByVal sDetails As String, _
                      ByVal dmDate As Date, _
                      ByVal dDateof_Birth As Date, _
                      ByVal iUserID As Integer, _
                      ByVal BankAccNumber As String, _
                      ByVal BankName As String, _
                      ByVal BranchName As String, _
                      ByVal visa_id As String, _
                      ByVal CustTypeID_Child As String, _
                      ByVal CustTypeID_Child_SUB As String, _
                      ByVal Rep_Type As String, _
                      ByVal Volume_day_fx As Double, _
                      ByVal Value_day_fx As Double, _
                      ByVal Volume_Month_fx As Double, _
                      ByVal Value_Month_fx As Double, _
                      ByVal Volume_day_remittance As Double, _
                      ByVal Value_day_remittance As Double, _
                      ByVal Volume_Month_Remittance As Double, _
                      ByVal Value_Month_Remittance As Double, _
                      ByVal details_1 As String, _
                      ByVal details_2 As String, _
                      ByVal risk_score As Double, _
                      ByVal isUpdate As Boolean, _
                      ByRef iCustid_Details_ot As String, _
                      ByRef sCustID_Details_GID As String, _
                      ByRef iErrorCode As String, _
                      ByRef USERID_GID As String, _
                      ByRef Risk_String As String, _
                      ByRef bln_complinace_clear As Boolean, _
                      ByRef str_complinace_clear_by As String, _
                      ByRef str_complinacel_clear_details As String, _
                      ByVal aux_1 As String, _
                      ByVal aux_2 As String, _
                      ByVal aux_3 As String, _
                      ByVal aux_4 As String, _
                      ByVal aux_5 As String, _
                      ByVal aux_6 As String, _
                      ByVal aux_7 As String, _
                      ByVal aux_8 As String, _
                       ByVal aux_9 As Date, _
                            ByVal aux_10 As Date, _
                               ByVal aux_11 As Date, _
                                  ByVal aux_12 As Date, _
                                   ByVal aux_13 As Date, _
                            ByVal aux_14 As Date, _
                               ByVal aux_15 As Date, _
                                  ByVal aux_16 As Date, _
                                    ByVal aux_17 As Integer, _
                            ByVal aux_18 As Integer, _
                               ByVal aux_19 As Integer, _
                                  ByVal aux_20 As Integer, ByVal blnISBlock_remittance As Boolean, ByVal blnISBlock_employee As Boolean, _
            ByVal Consignee_Name As String, ByVal Consignee_address As String, ByVal Consignee_port As String, _
            ByVal PurposeID_Master As Integer, ByVal PurposeID_Child As Integer, _
            ByVal risk_aux_1 As String, ByVal risk_aux_2 As String, ByVal risk_aux_3 As String, ByVal risk_aux_4 As String, _
            ByVal risk_aux_5 As String, ByVal risk_aux_6 As String, ByVal risk_aux_7 As String, ByVal risk_aux_8 As String, _
            ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String, _
            ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String, _
            ByVal risk_purpose As String, ByVal purpose As String, ByVal risk_source_of_income As String, ByVal source_of_income As String)





        Dim m_iCustID_DET As SqlInt64
        sIDNumber = UCase(sIDNumber)
        sAddress1 = UCase(sAddress1)
        sAddress2 = UCase(sAddress2)
        sCity = UCase(sCity)
        sTel = UCase(sTel)
        sFax = UCase(sFax)
        sZIP = UCase(sZIP)
        sEmail = UCase(sEmail)
        sSponserName = UCase(sSponserName)
        BankAccNumber = UCase(BankAccNumber)
        BankName = UCase(BankName)
        BranchName = UCase(BranchName)
        sDetails = UCase(sDetails)
        sPlaceOfIssue = UCase(sPlaceOfIssue)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                ' loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident_risk_profile"

                loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk_new_02_with_branch_new"
                loComm.CommandType = CommandType.StoredProcedure
                If isUpdate = False Then


                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                Else

                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_Details_GID_Update))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))


                End If



                .Add(New SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sPlaceOfIssue & ""))


                .Add(New SqlParameter("@IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iIDTypeID))



                .Add(New SqlParameter("@IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sIDNumber))
                .Add(New SqlParameter("@DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateOfIssue))
                .Add(New SqlParameter("@ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dExpiryDate))

                .Add(New SqlParameter("@Address1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress1))
                .Add(New SqlParameter("@Address2", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress2))

                .Add(New SqlParameter("@City", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCity))
                .Add(New SqlParameter("@Tel", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sTel))
                .Add(New SqlParameter("@Fax", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sFax))
                .Add(New SqlParameter("@ZIP", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sZIP))
                .Add(New SqlParameter("@Email", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sEmail))

                .Add(New SqlParameter("@SponserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sSponserName))
                .Add(New SqlParameter("@Details", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sDetails))


                .Add(New SqlParameter("@mDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, dmDate))
                .Add(New SqlParameter("@Dateof_Birth", SqlDbType.DateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateof_Birth))

                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))


                .Add(New SqlParameter("@BankAccNumber", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankAccNumber))
                .Add(New SqlParameter("@BankName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankName))
                .Add(New SqlParameter("@BranchName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BranchName))

                .Add(New SqlParameter("@USERID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, USERID_GID))
                .Add(New SqlParameter("@visa_id", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, visa_id))

                .Add(New SqlParameter("@CustTypeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child))
                .Add(New SqlParameter("@CustTypeID_Child_SUB", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child_SUB))
                .Add(New SqlParameter("@Rep_Type", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Type))
                .Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
                .Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
                .Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
                .Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

                .Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
                .Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
                .Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
                .Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))
                .Add(New SqlParameter("@details_1", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_1))
                .Add(New SqlParameter("@details_2", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_2))
                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@iCustid_Details_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID_DET))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, iErrorCode))
                .Add(New SqlParameter("@CustID_Details_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, sCustID_Details_GID))
                'bIsUpdate = Convert.ToBoolean(btstatus.Text)

                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 2000, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))


                .Add(New SqlParameter("@aux_1", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_1))
                .Add(New SqlParameter("@aux_2", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_2))
                .Add(New SqlParameter("@aux_3", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_3))
                .Add(New SqlParameter("@aux_4", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_4))

                .Add(New SqlParameter("@aux_5", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_5))
                .Add(New SqlParameter("@aux_6", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_6))
                .Add(New SqlParameter("@aux_7", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_7))
                .Add(New SqlParameter("@aux_8", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_8))




                .Add(New SqlParameter("@aux_9", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_9))

                .Add(New SqlParameter("@aux_10", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_10))

                .Add(New SqlParameter("@aux_11", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_11))

                .Add(New SqlParameter("@aux_12", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_12))


                .Add(New SqlParameter("@aux_13", _
         SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
         DataRowVersion.Proposed, aux_13))

                .Add(New SqlParameter("@aux_14", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_14))

                .Add(New SqlParameter("@aux_15", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_15))

                .Add(New SqlParameter("@aux_16", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_16))


                .Add(New SqlParameter("@aux_17", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_17))



                .Add(New SqlParameter("@aux_18", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_18))



                .Add(New SqlParameter("@aux_19", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_19))


                .Add(New SqlParameter("@aux_20", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_20))

                .Add(New SqlParameter("@is_remittance", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_remittance))
                .Add(New SqlParameter("@is_employee", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_employee))


                .Add(New SqlParameter("@Consignee_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_Name))
                .Add(New SqlParameter("@Consignee_address", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_address))
                .Add(New SqlParameter("@Consignee_port", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_port))


                .Add(New SqlParameter("@PurposeID_Master", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Master))
                .Add(New SqlParameter("@PurposeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Child))


                .Add(New SqlParameter("@risk_aux_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_1))
                .Add(New SqlParameter("@risk_aux_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_2))
                .Add(New SqlParameter("@risk_aux_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_3))
                .Add(New SqlParameter("@risk_aux_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_4))
                .Add(New SqlParameter("@risk_aux_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_5))
                .Add(New SqlParameter("@risk_aux_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_6))
                .Add(New SqlParameter("@risk_aux_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_7))
                .Add(New SqlParameter("@risk_aux_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_8))
                .Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
                .Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
                .Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
                .Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
                .Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
                .Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
                .Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
                .Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))


                .Add(New SqlParameter("@risk_purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_purpose))
                .Add(New SqlParameter("@risk_Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_source_of_income))
                .Add(New SqlParameter("@purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, purpose))
                .Add(New SqlParameter("@Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, source_of_income))




                If Not isUpdate Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If


            End With

            loComm.ExecuteNonQuery()

            If Not isUpdate Then
                m_iCustID_DET = (loComm.Parameters.Item("@iCustid_Details_ot").Value)
                iCustid_Details_ot = CStr(m_iCustID_DET.Value.ToString)
                sCustID_Details_GID = CStr(loComm.Parameters.Item("@CustID_Details_GID").Value)
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Public Sub mSave_Cust_detail_RiskProfile_3(ByRef loComm As SqlCommand, ByVal iCustid_Details As Long, _
                   ByVal sCustID_Details_GID_Update As String, _
                   ByVal iCustid As String, _
                   ByVal sCustID_GID As String, _
                   ByVal sPlaceOfIssue As String, _
                   ByVal iIDTypeID As Integer, _
                   ByVal sIDNumber As String, _
                   ByVal dDateOfIssue As Date, _
                   ByVal dExpiryDate As Date, _
                   ByVal sAddress1 As String, _
                   ByVal sAddress2 As String, _
                   ByVal sCity As String, _
                   ByVal sTel As String, _
                   ByVal sFax As String, _
                   ByVal sZIP As String, _
                   ByVal sEmail As String, _
                   ByVal sSponserName As String, _
                   ByVal sDetails As String, _
                   ByVal dmDate As Date, _
                   ByVal dDateof_Birth As Date, _
                   ByVal iUserID As Integer, _
                   ByVal BankAccNumber As String, _
                   ByVal BankName As String, _
                   ByVal BranchName As String, _
                   ByVal visa_id As String, _
                   ByVal CustTypeID_Child As String, _
                   ByVal CustTypeID_Child_SUB As String, _
                   ByVal Rep_Type As String, _
                   ByVal Volume_day_fx As Double, _
                   ByVal Value_day_fx As Double, _
                   ByVal Volume_Month_fx As Double, _
                   ByVal Value_Month_fx As Double, _
                   ByVal Volume_day_remittance As Double, _
                   ByVal Value_day_remittance As Double, _
                   ByVal Volume_Month_Remittance As Double, _
                   ByVal Value_Month_Remittance As Double, _
                   ByVal details_1 As String, _
                   ByVal details_2 As String, _
                   ByVal risk_score As Double, _
                   ByVal isUpdate As Boolean, _
                   ByRef iCustid_Details_ot As String, _
                   ByRef sCustID_Details_GID As String, _
                   ByRef iErrorCode As String, _
                   ByRef USERID_GID As String, _
                   ByRef Risk_String As String, _
                   ByRef bln_complinace_clear As Boolean, _
                   ByRef str_complinace_clear_by As String, _
                   ByRef str_complinacel_clear_details As String, _
                   ByVal aux_1 As String, _
                   ByVal aux_2 As String, _
                   ByVal aux_3 As String, _
                   ByVal aux_4 As String, _
                   ByVal aux_5 As String, _
                   ByVal aux_6 As String, _
                   ByVal aux_7 As String, _
                   ByVal aux_8 As String, _
                    ByVal aux_9 As Date, _
                         ByVal aux_10 As Date, _
                            ByVal aux_11 As Date, _
                               ByVal aux_12 As Date, _
                                ByVal aux_13 As Date, _
                         ByVal aux_14 As Date, _
                            ByVal aux_15 As Date, _
                               ByVal aux_16 As Date, _
                                 ByVal aux_17 As Integer, _
                         ByVal aux_18 As Integer, _
                            ByVal aux_19 As Integer, _
                               ByVal aux_20 As Integer, ByVal blnISBlock_remittance As Boolean, ByVal blnISBlock_employee As Boolean, _
         ByVal Consignee_Name As String, ByVal Consignee_address As String, ByVal Consignee_port As String, _
         ByVal PurposeID_Master As Integer, ByVal PurposeID_Child As Integer, _
         ByVal risk_aux_1 As String, ByVal risk_aux_2 As String, ByVal risk_aux_3 As String, ByVal risk_aux_4 As String, _
         ByVal risk_aux_5 As String, ByVal risk_aux_6 As String, ByVal risk_aux_7 As String, ByVal risk_aux_8 As String, _
         ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String, _
         ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String, _
         ByVal risk_purpose As String, ByVal purpose As String, ByVal risk_source_of_income As String, ByVal source_of_income As String, _
         ByVal bln_RISK_PLUS As Boolean, ByVal RISK_PLUS_STRING As String, _
         Optional CB_City As String = "", _
         Optional CB_Airport_Code As String = "", _
         Optional CB_CityID As Integer = 0, _
         Optional CB_CUST_PAYMODE As String = "", _
         Optional CB_CUST_PAYMODE_DESC As String = "")




        Dim m_iCustID_DET As SqlInt64
        sIDNumber = UCase(sIDNumber)
        sAddress1 = UCase(sAddress1)
        sAddress2 = UCase(sAddress2)
        sCity = UCase(sCity)
        sTel = UCase(sTel)
        sFax = UCase(sFax)
        sZIP = UCase(sZIP)
        sEmail = UCase(sEmail)
        sSponserName = UCase(sSponserName)
        BankAccNumber = UCase(BankAccNumber)
        BankName = UCase(BankName)
        BranchName = UCase(BranchName)
        sDetails = UCase(sDetails)
        sPlaceOfIssue = UCase(sPlaceOfIssue)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                ' loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident_risk_profile"
                'loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk_new_03_with_branch_new_risk_plus"
                loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk_new_03_with_branch_new_risk_plus_CB"
                loComm.CommandType = CommandType.StoredProcedure
                If isUpdate = False Then


                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                Else

                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_Details_GID_Update))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))


                End If



                .Add(New SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sPlaceOfIssue & ""))


                .Add(New SqlParameter("@IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iIDTypeID))



                .Add(New SqlParameter("@IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sIDNumber))
                .Add(New SqlParameter("@DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateOfIssue))
                .Add(New SqlParameter("@ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dExpiryDate))

                .Add(New SqlParameter("@Address1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress1))
                .Add(New SqlParameter("@Address2", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress2))

                .Add(New SqlParameter("@City", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCity))
                .Add(New SqlParameter("@Tel", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sTel))
                .Add(New SqlParameter("@Fax", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sFax))
                .Add(New SqlParameter("@ZIP", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sZIP))
                .Add(New SqlParameter("@Email", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sEmail))

                .Add(New SqlParameter("@SponserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sSponserName))
                .Add(New SqlParameter("@Details", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sDetails))


                .Add(New SqlParameter("@mDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, dmDate))
                .Add(New SqlParameter("@Dateof_Birth", SqlDbType.DateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateof_Birth))

                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))


                .Add(New SqlParameter("@BankAccNumber", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankAccNumber))
                .Add(New SqlParameter("@BankName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankName))
                .Add(New SqlParameter("@BranchName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BranchName))

                .Add(New SqlParameter("@USERID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, USERID_GID))
                .Add(New SqlParameter("@visa_id", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, visa_id))

                .Add(New SqlParameter("@CustTypeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child))
                .Add(New SqlParameter("@CustTypeID_Child_SUB", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child_SUB))
                .Add(New SqlParameter("@Rep_Type", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Type))
                .Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
                .Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
                .Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
                .Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

                .Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
                .Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
                .Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
                .Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))
                .Add(New SqlParameter("@details_1", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_1))
                .Add(New SqlParameter("@details_2", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_2))
                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@iCustid_Details_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID_DET))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, iErrorCode))
                .Add(New SqlParameter("@CustID_Details_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, sCustID_Details_GID))
                'bIsUpdate = Convert.ToBoolean(btstatus.Text)

                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 2000, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))


                .Add(New SqlParameter("@aux_1", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_1))
                .Add(New SqlParameter("@aux_2", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_2))
                .Add(New SqlParameter("@aux_3", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_3))
                .Add(New SqlParameter("@aux_4", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_4))

                .Add(New SqlParameter("@aux_5", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_5))
                .Add(New SqlParameter("@aux_6", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_6))
                .Add(New SqlParameter("@aux_7", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_7))
                .Add(New SqlParameter("@aux_8", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_8))




                .Add(New SqlParameter("@aux_9", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_9))

                .Add(New SqlParameter("@aux_10", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_10))

                .Add(New SqlParameter("@aux_11", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_11))

                .Add(New SqlParameter("@aux_12", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_12))


                .Add(New SqlParameter("@aux_13", _
         SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
         DataRowVersion.Proposed, aux_13))

                .Add(New SqlParameter("@aux_14", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_14))

                .Add(New SqlParameter("@aux_15", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_15))

                .Add(New SqlParameter("@aux_16", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_16))


                .Add(New SqlParameter("@aux_17", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_17))



                .Add(New SqlParameter("@aux_18", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_18))



                .Add(New SqlParameter("@aux_19", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_19))


                .Add(New SqlParameter("@aux_20", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_20))

                .Add(New SqlParameter("@is_remittance", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_remittance))
                .Add(New SqlParameter("@is_employee", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_employee))


                .Add(New SqlParameter("@Consignee_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_Name))
                .Add(New SqlParameter("@Consignee_address", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_address))
                .Add(New SqlParameter("@Consignee_port", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Consignee_port))


                .Add(New SqlParameter("@PurposeID_Master", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Master))
                .Add(New SqlParameter("@PurposeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Child))


                .Add(New SqlParameter("@risk_aux_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_1))
                .Add(New SqlParameter("@risk_aux_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_2))
                .Add(New SqlParameter("@risk_aux_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_3))
                .Add(New SqlParameter("@risk_aux_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_4))
                .Add(New SqlParameter("@risk_aux_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_5))
                .Add(New SqlParameter("@risk_aux_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_6))
                .Add(New SqlParameter("@risk_aux_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_7))
                .Add(New SqlParameter("@risk_aux_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_8))
                .Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
                .Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
                .Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
                .Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
                .Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
                .Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
                .Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
                .Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))

                .Add(New SqlParameter("@high_risk_plus", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_RISK_PLUS))
                .Add(New SqlParameter("@high_risk_plust_str", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, RISK_PLUS_STRING))
                .Add(New SqlParameter("@risk_purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_purpose))
                .Add(New SqlParameter("@risk_Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_source_of_income))
                .Add(New SqlParameter("@purpose", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, purpose))
                .Add(New SqlParameter("@Source_of_income", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, source_of_income))

                .Add(New SqlParameter("@CB_City", SqlDbType.NVarChar, 150, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_City))
                .Add(New SqlParameter("@CB_Airport_Code", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_Airport_Code))
                .Add(New SqlParameter("@CB_CityID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_CityID))
                .Add(New SqlParameter("@CB_CUST_PAYMODE", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_CUST_PAYMODE))
                .Add(New SqlParameter("@CB_CUST_PAYMODE_DESC", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_CUST_PAYMODE_DESC))


                If Not isUpdate Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If


            End With

            loComm.ExecuteNonQuery()

            If Not isUpdate Then
                m_iCustID_DET = (loComm.Parameters.Item("@iCustid_Details_ot").Value)
                iCustid_Details_ot = CStr(m_iCustID_DET.Value.ToString)
                sCustID_Details_GID = CStr(loComm.Parameters.Item("@CustID_Details_GID").Value)
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub




    Public Sub mSave_Cust_Purpose_Detail(ByRef loComm As SqlCommand, ByVal sCustID_GID As String, _
          ByVal PurposeID_Detail As Integer)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                ' loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident_risk_profile"

                loComm.CommandText = "stp_tblCust_Purpose_Detail_Insert"
                loComm.CommandType = CommandType.StoredProcedure



                .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))

                .Add(New SqlParameter("@PurposeID_Detail", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Detail))


            End With

            loComm.ExecuteNonQuery()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Public Sub mSave_Cust_Purpose_Master(ByRef loComm As SqlCommand, ByVal sCustID_GID As String, _
         ByVal PurposeID_Master As Integer)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                ' loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident_risk_profile"

                loComm.CommandText = "stp_tblCust_Purpose_Master_Insert"
                loComm.CommandType = CommandType.StoredProcedure



                .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                .Add(New SqlParameter("@PurposeID_Master", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, PurposeID_Master))


            End With

            loComm.ExecuteNonQuery()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub mSave_Beneficiary_Risk_2(ByRef objComm As SqlClient.SqlCommand, ByRef objMN As modMain, _
                                        ByVal dcBeneID As Long, _
                                        ByVal str_BeneID_GID As String, _
                                        ByVal str_CustID_GID As String, _
                                        ByVal str_Custid_Details_GID As String, _
                                        ByVal iCustID As Long, _
                                        ByVal iCustid_Details As Long, _
                                        ByVal str_BenName_Arb As String, _
                                        ByVal str_BenName_First As String, _
                                        ByVal str_BenName_Last As String, _
                                        ByVal str_BenName_Middle As String, _
                                        ByVal str_BankIdentifier As String, _
                                        ByVal str_Bank_1_Identifier As String, _
                                        ByVal str_Bank As String, _
                                        ByVal str_Bank_1 As String, _
                                        ByVal str_Bank_2 As String, _
                                        ByVal str_BankAcc As String, _
                                        ByVal str_BankAcc_1 As String, _
                                        ByVal str_BankAcc_2 As String, _
                                        ByVal str_BankBaranch As String, _
                                        ByVal str_BankBaranch_1 As String, _
                                        ByVal str_BankBaranch_2 As String, _
                                        ByVal str_Address As String, _
                                        ByVal str_Address_1 As String, _
                                        ByVal str_WorkPhoneNo As String, _
                                        ByVal str_HomePhoneNo As String, _
                                        ByVal str_CellPhone As String, _
                                        ByVal CountryID_Nationality_ben As Integer, _
                                        ByVal CountryID_ben As Integer, _
                                        ByVal City_ben As String, _
                                        ByVal str_zip_ben As String, _
                                        ByVal str_email_ben As String, _
                                        ByVal str_reference_ben As String, _
                                        ByVal CCID As Integer, _
                                        ByVal str_USER_GID As String, _
                                        ByRef str_Beneid_ot As String, _
                                        ByRef str_BeneID_GID_ot As String, _
                                        ByVal IsUpdate As Boolean, _
     ByVal str_Bene_In_bnk_a As String, _
       ByVal str_Bene_In_bnk_b As String, _
      ByVal str_Bene_In_bnk_c As String, _
      ByVal str_Bene_In_bnk_d As String, _
       ByVal str_Bene_In_bnk_e As String, _
       ByVal str_Bene_In_bnk_f As String, _
      ByVal str_Purpose_a As String, _
       ByVal str_Purpose_b As String, _
       ByVal str_Purpose_c As String, _
       ByVal str_Purpose_d As String, _
       ByVal str_Source_Funds As String, _
       ByVal str_DTCD As String, _
       ByVal str_SBCD As String, _
       ByVal risk_score As Double, _
       ByVal Risk_String As String, _
      ByVal Volume_day_fx As Double, _
      ByVal Value_day_fx As Double, _
      ByVal Volume_Month_fx As Double, _
      ByVal Value_Month_fx As Double, _
      ByVal Volume_day_remittance As Double, _
      ByVal Value_day_remittance As Double, _
      ByVal Volume_Month_Remittance As Double, _
      ByVal Value_Month_Remittance As Double, _
        ByVal risk_Name As String,
        ByVal risk_Nationality As String,
        ByVal risk_Country As String,
        ByVal risk_Address As String,
        ByVal risk_Bank_ACC As String,
        ByVal risk_Bank_Identifier As String,
        ByVal risk_Bank_Name As String,
        ByVal risk_Bank_Address As String,
        ByVal risk_Bank_Identifier_INT As String,
        ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String,
        ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String)








        Dim m_Bene_ID As SqlInt64
        Dim m_iErrorCode As SqlInt32


        'UCase(str_BenName_Arb)
        str_BenName_First = UCase(str_BenName_First)
        str_BenName_Last = UCase(str_BenName_Last)
        str_BenName_Middle = UCase(str_BenName_Middle)
        str_BankIdentifier = UCase(str_BankIdentifier)
        str_Bank_1_Identifier = UCase(str_Bank_1_Identifier)
        str_Bank = UCase(str_Bank)
        str_Bank_1 = UCase(str_Bank_1)
        str_Bank_2 = UCase(str_Bank_2)
        str_BankAcc = UCase(str_BankAcc)
        str_BankAcc_1 = UCase(str_BankAcc_1)
        str_BankAcc_2 = UCase(str_BankAcc_2)
        str_Address = UCase(str_Address)
        str_BankBaranch = UCase(str_BankBaranch)
        str_BankBaranch_1 = UCase(str_BankBaranch_1)
        str_BankBaranch_2 = UCase(str_BankBaranch_2)
        str_Address_1 = UCase(str_Address_1)
        str_WorkPhoneNo = UCase(str_WorkPhoneNo)
        str_HomePhoneNo = UCase(str_HomePhoneNo)
        str_CellPhone = UCase(str_CellPhone)
        City_ben = UCase(City_ben)
        str_zip_ben = UCase(str_zip_ben)
        str_email_ben = UCase(str_email_ben)
        str_reference_ben = UCase(str_reference_ben)

        str_Bene_In_bnk_a = UCase(str_Bene_In_bnk_a)
        str_Bene_In_bnk_b = UCase(str_Bene_In_bnk_b)
        str_Bene_In_bnk_c = UCase(str_Bene_In_bnk_c)
        str_Bene_In_bnk_d = UCase(str_Bene_In_bnk_d)
        str_Bene_In_bnk_e = UCase(str_Bene_In_bnk_e)
        str_Bene_In_bnk_f = UCase(str_Bene_In_bnk_f)
        str_Purpose_a = UCase(str_Purpose_a)
        str_Purpose_b = UCase(str_Purpose_b)
        str_Purpose_c = UCase(str_Purpose_c)
        str_Purpose_d = UCase(str_Purpose_d)
        str_Source_Funds = UCase(str_Source_Funds)

        Try

            objComm.Parameters.Clear()
            ' objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA"
            objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA_RiskBased_3_with_branch"

            objComm.CommandType = CommandType.StoredProcedure
            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, 0))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CustID_GID))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Custid_Details_GID))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustID))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))


            Else
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, dcBeneID))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))


            End If
            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@BenName_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Arb))
            objComm.Parameters.Add(New SqlParameter("@BenName_First", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_First))
            objComm.Parameters.Add(New SqlParameter("@BenName_Last", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Last))
            objComm.Parameters.Add(New SqlParameter("@BenName_Middle", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Middle))

            objComm.Parameters.Add(New SqlParameter("@BankIdentifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankIdentifier))
            objComm.Parameters.Add(New SqlParameter("@Bank_1_Identifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))

            'objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))
            'objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            'objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))


            objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1))
            objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))



            objComm.Parameters.Add(New SqlParameter("@BankAcc", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_1))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_2))

            objComm.Parameters.Add(New SqlParameter("@BankBaranch", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_1))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_2))
            objComm.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address))
            objComm.Parameters.Add(New SqlParameter("@Address_1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address_1))
            objComm.Parameters.Add(New SqlParameter("@HomePhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_HomePhoneNo))
            objComm.Parameters.Add(New SqlParameter("@WorkPhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_WorkPhoneNo))
            objComm.Parameters.Add(New SqlParameter("@CellPhone", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CellPhone))
            objComm.Parameters.Add(New SqlParameter("@CountryID_Nationality_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_Nationality_ben))
            objComm.Parameters.Add(New SqlParameter("@CountryID_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_ben))
            objComm.Parameters.Add(New SqlParameter("@City_ben", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, City_ben))
            objComm.Parameters.Add(New SqlParameter("@zip_ben", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_zip_ben))
            objComm.Parameters.Add(New SqlParameter("@email_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_email_ben))
            objComm.Parameters.Add(New SqlParameter("@reference_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_reference_ben))
            objComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CCID))

            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@iBeneid_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_Bene_ID))
            objComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iErrorCode))
            objComm.Parameters.Add(New SqlParameter("@BeneID_GID_ot", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID_ot))

            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_a", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_a))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_b", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_b))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_c))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_d))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_e", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_e))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_f", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_f))
            objComm.Parameters.Add(New SqlParameter("@Purpose_a", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_a))
            objComm.Parameters.Add(New SqlParameter("@Purpose_b", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_b))
            objComm.Parameters.Add(New SqlParameter("@Purpose_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_c))
            objComm.Parameters.Add(New SqlParameter("@Purpose_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_d))

            objComm.Parameters.Add(New SqlParameter("@Source_Funds", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Source_Funds))

            objComm.Parameters.Add(New SqlParameter("@DTCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_DTCD))
            objComm.Parameters.Add(New SqlParameter("@SBCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_SBCD))


            objComm.Parameters.Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
            objComm.Parameters.Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
            objComm.Parameters.Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
            objComm.Parameters.Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

            objComm.Parameters.Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
            objComm.Parameters.Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
            objComm.Parameters.Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
            objComm.Parameters.Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))


            objComm.Parameters.Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
            objComm.Parameters.Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


            objComm.Parameters.Add(New SqlParameter("@risk_Name_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Name))
            objComm.Parameters.Add(New SqlParameter("@risk_Nationality_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Nationality))
            objComm.Parameters.Add(New SqlParameter("@risk_Country_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Country))
            objComm.Parameters.Add(New SqlParameter("@risk_Address_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Address))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_ACC_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_ACC))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Identifier_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Identifier))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Name_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Name))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Address_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Address))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Identifier_INT_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Identifier_INT))

            objComm.Parameters.Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))

            ' ''        ByVal risk_score As Double, _
            ' ''ByVal Risk_String As String)






            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            Else
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            End If

            objComm.ExecuteNonQuery()
            If Not IsUpdate Then
                m_Bene_ID = (objComm.Parameters.Item("@iBeneid_ot").Value)
                str_Beneid_ot = CStr(m_Bene_ID.Value.ToString)
                str_BeneID_GID_ot = objComm.Parameters.Item("@BeneID_GID_ot").Value
                m_iErrorCode = New SqlInt32(CType(objComm.Parameters.Item("@iErrorCode").Value, SqlInt32))
            End If
            '**************************************************'**************************************************
            '**************************************************'**************************************************

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Sub mSave_Beneficiary_Risk_3(ByRef objComm As SqlClient.SqlCommand, ByRef objMN As modMain, _
                                       ByVal dcBeneID As Long, _
                                       ByVal str_BeneID_GID As String, _
                                       ByVal str_CustID_GID As String, _
                                       ByVal str_Custid_Details_GID As String, _
                                       ByVal iCustID As Long, _
                                       ByVal iCustid_Details As Long, _
                                       ByVal str_BenName_Arb As String, _
                                       ByVal str_BenName_First As String, _
                                       ByVal str_BenName_Last As String, _
                                       ByVal str_BenName_Middle As String, _
                                       ByVal str_BankIdentifier As String, _
                                       ByVal str_Bank_1_Identifier As String, _
                                       ByVal str_Bank As String, _
                                       ByVal str_Bank_1 As String, _
                                       ByVal str_Bank_2 As String, _
                                       ByVal str_BankAcc As String, _
                                       ByVal str_BankAcc_1 As String, _
                                       ByVal str_BankAcc_2 As String, _
                                       ByVal str_BankBaranch As String, _
                                       ByVal str_BankBaranch_1 As String, _
                                       ByVal str_BankBaranch_2 As String, _
                                       ByVal str_Address As String, _
                                       ByVal str_Address_1 As String, _
                                       ByVal str_WorkPhoneNo As String, _
                                       ByVal str_HomePhoneNo As String, _
                                       ByVal str_CellPhone As String, _
                                       ByVal CountryID_Nationality_ben As Integer, _
                                       ByVal CountryID_ben As Integer, _
                                       ByVal City_ben As String, _
                                       ByVal str_zip_ben As String, _
                                       ByVal str_email_ben As String, _
                                       ByVal str_reference_ben As String, _
                                       ByVal CCID As Integer, _
                                       ByVal str_USER_GID As String, _
                                       ByRef str_Beneid_ot As String, _
                                       ByRef str_BeneID_GID_ot As String, _
                                       ByVal IsUpdate As Boolean, _
    ByVal str_Bene_In_bnk_a As String, _
      ByVal str_Bene_In_bnk_b As String, _
     ByVal str_Bene_In_bnk_c As String, _
     ByVal str_Bene_In_bnk_d As String, _
      ByVal str_Bene_In_bnk_e As String, _
      ByVal str_Bene_In_bnk_f As String, _
     ByVal str_Purpose_a As String, _
      ByVal str_Purpose_b As String, _
      ByVal str_Purpose_c As String, _
      ByVal str_Purpose_d As String, _
      ByVal str_Source_Funds As String, _
      ByVal str_DTCD As String, _
      ByVal str_SBCD As String, _
      ByVal risk_score As Double, _
      ByVal Risk_String As String, _
     ByVal Volume_day_fx As Double, _
     ByVal Value_day_fx As Double, _
     ByVal Volume_Month_fx As Double, _
     ByVal Value_Month_fx As Double, _
     ByVal Volume_day_remittance As Double, _
     ByVal Value_day_remittance As Double, _
     ByVal Volume_Month_Remittance As Double, _
     ByVal Value_Month_Remittance As Double, _
       ByVal risk_Name As String,
       ByVal risk_Nationality As String,
       ByVal risk_Country As String,
       ByVal risk_Address As String,
       ByVal risk_Bank_ACC As String,
       ByVal risk_Bank_Identifier As String,
       ByVal risk_Bank_Name As String,
       ByVal risk_Bank_Address As String,
       ByVal risk_Bank_Identifier_INT As String,
       ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String,
       ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String, ByVal purpose_code As String)








        Dim m_Bene_ID As SqlInt64
        Dim m_iErrorCode As SqlInt32


        'UCase(str_BenName_Arb)
        str_BenName_First = UCase(str_BenName_First)
        str_BenName_Last = UCase(str_BenName_Last)
        str_BenName_Middle = UCase(str_BenName_Middle)
        str_BankIdentifier = UCase(str_BankIdentifier)
        str_Bank_1_Identifier = UCase(str_Bank_1_Identifier)
        str_Bank = UCase(str_Bank)
        str_Bank_1 = UCase(str_Bank_1)
        str_Bank_2 = UCase(str_Bank_2)
        str_BankAcc = UCase(str_BankAcc)
        str_BankAcc_1 = UCase(str_BankAcc_1)
        str_BankAcc_2 = UCase(str_BankAcc_2)
        str_Address = UCase(str_Address)
        str_BankBaranch = UCase(str_BankBaranch)
        str_BankBaranch_1 = UCase(str_BankBaranch_1)
        str_BankBaranch_2 = UCase(str_BankBaranch_2)
        str_Address_1 = UCase(str_Address_1)
        str_WorkPhoneNo = UCase(str_WorkPhoneNo)
        str_HomePhoneNo = UCase(str_HomePhoneNo)
        str_CellPhone = UCase(str_CellPhone)
        City_ben = UCase(City_ben)
        str_zip_ben = UCase(str_zip_ben)
        str_email_ben = UCase(str_email_ben)
        str_reference_ben = UCase(str_reference_ben)

        str_Bene_In_bnk_a = UCase(str_Bene_In_bnk_a)
        str_Bene_In_bnk_b = UCase(str_Bene_In_bnk_b)
        str_Bene_In_bnk_c = UCase(str_Bene_In_bnk_c)
        str_Bene_In_bnk_d = UCase(str_Bene_In_bnk_d)
        str_Bene_In_bnk_e = UCase(str_Bene_In_bnk_e)
        str_Bene_In_bnk_f = UCase(str_Bene_In_bnk_f)
        str_Purpose_a = UCase(str_Purpose_a)
        str_Purpose_b = UCase(str_Purpose_b)
        str_Purpose_c = UCase(str_Purpose_c)
        str_Purpose_d = UCase(str_Purpose_d)
        str_Source_Funds = UCase(str_Source_Funds)
        purpose_code = UCase(purpose_code)
        Try

            objComm.Parameters.Clear()
            ' objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA"
            objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA_RiskBased_3_with_branch_new"

            objComm.CommandType = CommandType.StoredProcedure
            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, 0))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CustID_GID))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Custid_Details_GID))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustID))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))


            Else
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, dcBeneID))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))


            End If
            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@BenName_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Arb))
            objComm.Parameters.Add(New SqlParameter("@BenName_First", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_First))
            objComm.Parameters.Add(New SqlParameter("@BenName_Last", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Last))
            objComm.Parameters.Add(New SqlParameter("@BenName_Middle", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Middle))

            objComm.Parameters.Add(New SqlParameter("@BankIdentifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankIdentifier))
            objComm.Parameters.Add(New SqlParameter("@Bank_1_Identifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))

            'objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))
            'objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            'objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))


            objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1))
            objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))



            objComm.Parameters.Add(New SqlParameter("@BankAcc", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_1))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_2))

            objComm.Parameters.Add(New SqlParameter("@BankBaranch", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_1))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_2))
            objComm.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address))
            objComm.Parameters.Add(New SqlParameter("@Address_1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address_1))
            objComm.Parameters.Add(New SqlParameter("@HomePhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_HomePhoneNo))
            objComm.Parameters.Add(New SqlParameter("@WorkPhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_WorkPhoneNo))
            objComm.Parameters.Add(New SqlParameter("@CellPhone", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CellPhone))
            objComm.Parameters.Add(New SqlParameter("@CountryID_Nationality_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_Nationality_ben))
            objComm.Parameters.Add(New SqlParameter("@CountryID_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_ben))
            objComm.Parameters.Add(New SqlParameter("@City_ben", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, City_ben))
            objComm.Parameters.Add(New SqlParameter("@zip_ben", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_zip_ben))
            objComm.Parameters.Add(New SqlParameter("@email_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_email_ben))
            objComm.Parameters.Add(New SqlParameter("@reference_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_reference_ben))
            objComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CCID))

            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@iBeneid_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_Bene_ID))
            objComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iErrorCode))
            objComm.Parameters.Add(New SqlParameter("@BeneID_GID_ot", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID_ot))

            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_a", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_a))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_b", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_b))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_c))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_d))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_e", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_e))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_f", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_f))
            objComm.Parameters.Add(New SqlParameter("@Purpose_a", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_a))
            objComm.Parameters.Add(New SqlParameter("@Purpose_b", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_b))
            objComm.Parameters.Add(New SqlParameter("@Purpose_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_c))
            objComm.Parameters.Add(New SqlParameter("@Purpose_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_d))

            objComm.Parameters.Add(New SqlParameter("@Source_Funds", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Source_Funds))

            objComm.Parameters.Add(New SqlParameter("@DTCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_DTCD))
            objComm.Parameters.Add(New SqlParameter("@SBCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_SBCD))


            objComm.Parameters.Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
            objComm.Parameters.Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
            objComm.Parameters.Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
            objComm.Parameters.Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

            objComm.Parameters.Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
            objComm.Parameters.Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
            objComm.Parameters.Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
            objComm.Parameters.Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))


            objComm.Parameters.Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
            objComm.Parameters.Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


            objComm.Parameters.Add(New SqlParameter("@risk_Name_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Name))
            objComm.Parameters.Add(New SqlParameter("@risk_Nationality_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Nationality))
            objComm.Parameters.Add(New SqlParameter("@risk_Country_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Country))
            objComm.Parameters.Add(New SqlParameter("@risk_Address_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Address))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_ACC_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_ACC))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Identifier_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Identifier))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Name_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Name))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Address_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Address))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Identifier_INT_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Identifier_INT))

            objComm.Parameters.Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))
            objComm.Parameters.Add(New SqlParameter("@purpose_code", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, purpose_code))
            ' ''        ByVal risk_score As Double, _
            ' ''ByVal Risk_String As String)






            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            Else
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            End If

            objComm.ExecuteNonQuery()
            If Not IsUpdate Then
                m_Bene_ID = (objComm.Parameters.Item("@iBeneid_ot").Value)
                str_Beneid_ot = CStr(m_Bene_ID.Value.ToString)
                str_BeneID_GID_ot = objComm.Parameters.Item("@BeneID_GID_ot").Value
                m_iErrorCode = New SqlInt32(CType(objComm.Parameters.Item("@iErrorCode").Value, SqlInt32))
            End If
            '**************************************************'**************************************************
            '**************************************************'**************************************************

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Sub mSave_Beneficiary_Risk_3_CB(ByRef objComm As SqlClient.SqlCommand, ByRef objMN As modMain, _
                                     ByVal dcBeneID As Long, _
                                     ByVal str_BeneID_GID As String, _
                                     ByVal str_CustID_GID As String, _
                                     ByVal str_Custid_Details_GID As String, _
                                     ByVal iCustID As Long, _
                                     ByVal iCustid_Details As Long, _
                                     ByVal str_BenName_Arb As String, _
                                     ByVal str_BenName_First As String, _
                                     ByVal str_BenName_Last As String, _
                                     ByVal str_BenName_Middle As String, _
                                     ByVal str_BankIdentifier As String, _
                                     ByVal str_Bank_1_Identifier As String, _
                                     ByVal str_Bank As String, _
                                     ByVal str_Bank_1 As String, _
                                     ByVal str_Bank_2 As String, _
                                     ByVal str_BankAcc As String, _
                                     ByVal str_BankAcc_1 As String, _
                                     ByVal str_BankAcc_2 As String, _
                                     ByVal str_BankBaranch As String, _
                                     ByVal str_BankBaranch_1 As String, _
                                     ByVal str_BankBaranch_2 As String, _
                                     ByVal str_Address As String, _
                                     ByVal str_Address_1 As String, _
                                     ByVal str_WorkPhoneNo As String, _
                                     ByVal str_HomePhoneNo As String, _
                                     ByVal str_CellPhone As String, _
                                     ByVal CountryID_Nationality_ben As Integer, _
                                     ByVal CountryID_ben As Integer, _
                                     ByVal City_ben As String, _
                                     ByVal str_zip_ben As String, _
                                     ByVal str_email_ben As String, _
                                     ByVal str_reference_ben As String, _
                                     ByVal CCID As Integer, _
                                     ByVal str_USER_GID As String, _
                                     ByRef str_Beneid_ot As String, _
                                     ByRef str_BeneID_GID_ot As String, _
                                     ByVal IsUpdate As Boolean, _
  ByVal str_Bene_In_bnk_a As String, _
    ByVal str_Bene_In_bnk_b As String, _
   ByVal str_Bene_In_bnk_c As String, _
   ByVal str_Bene_In_bnk_d As String, _
    ByVal str_Bene_In_bnk_e As String, _
    ByVal str_Bene_In_bnk_f As String, _
   ByVal str_Purpose_a As String, _
    ByVal str_Purpose_b As String, _
    ByVal str_Purpose_c As String, _
    ByVal str_Purpose_d As String, _
    ByVal str_Source_Funds As String, _
    ByVal str_DTCD As String, _
    ByVal str_SBCD As String, _
    ByVal risk_score As Double, _
    ByVal Risk_String As String, _
   ByVal Volume_day_fx As Double, _
   ByVal Value_day_fx As Double, _
   ByVal Volume_Month_fx As Double, _
   ByVal Value_Month_fx As Double, _
   ByVal Volume_day_remittance As Double, _
   ByVal Value_day_remittance As Double, _
   ByVal Volume_Month_Remittance As Double, _
   ByVal Value_Month_Remittance As Double, _
     ByVal risk_Name As String,
     ByVal risk_Nationality As String,
     ByVal risk_Country As String,
     ByVal risk_Address As String,
     ByVal risk_Bank_ACC As String,
     ByVal risk_Bank_Identifier As String,
     ByVal risk_Bank_Name As String,
     ByVal risk_Bank_Address As String,
     ByVal risk_Bank_Identifier_INT As String,
     ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String,
     ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String, ByVal purpose_code As String,
  ByVal BEN_ID_NO As String, ByVal BEN_ID_TYPE As Integer, ByVal BEN_ID_EXPIRY As Date, ByVal BEN_CITY As String, ByVal BEN_NEARAIRPORT As String,
     ByVal BEN_RELATION As Integer, ByVal CB_BEN_CityID As Integer, ByVal CB_BEN_PAYMODE As String, ByVal CB_BEN_PAYMODE_DESC As String)







        Dim m_Bene_ID As SqlInt64
        Dim m_iErrorCode As SqlInt32


        'UCase(str_BenName_Arb)
        str_BenName_First = UCase(str_BenName_First)
        str_BenName_Last = UCase(str_BenName_Last)
        str_BenName_Middle = UCase(str_BenName_Middle)
        str_BankIdentifier = UCase(str_BankIdentifier)
        str_Bank_1_Identifier = UCase(str_Bank_1_Identifier)
        str_Bank = UCase(str_Bank)
        str_Bank_1 = UCase(str_Bank_1)
        str_Bank_2 = UCase(str_Bank_2)
        str_BankAcc = UCase(str_BankAcc)
        str_BankAcc_1 = UCase(str_BankAcc_1)
        str_BankAcc_2 = UCase(str_BankAcc_2)
        str_Address = UCase(str_Address)
        str_BankBaranch = UCase(str_BankBaranch)
        str_BankBaranch_1 = UCase(str_BankBaranch_1)
        str_BankBaranch_2 = UCase(str_BankBaranch_2)
        str_Address_1 = UCase(str_Address_1)
        str_WorkPhoneNo = UCase(str_WorkPhoneNo)
        str_HomePhoneNo = UCase(str_HomePhoneNo)
        str_CellPhone = UCase(str_CellPhone)
        City_ben = UCase(City_ben)
        str_zip_ben = UCase(str_zip_ben)
        str_email_ben = UCase(str_email_ben)
        str_reference_ben = UCase(str_reference_ben)

        str_Bene_In_bnk_a = UCase(str_Bene_In_bnk_a)
        str_Bene_In_bnk_b = UCase(str_Bene_In_bnk_b)
        str_Bene_In_bnk_c = UCase(str_Bene_In_bnk_c)
        str_Bene_In_bnk_d = UCase(str_Bene_In_bnk_d)
        str_Bene_In_bnk_e = UCase(str_Bene_In_bnk_e)
        str_Bene_In_bnk_f = UCase(str_Bene_In_bnk_f)
        str_Purpose_a = UCase(str_Purpose_a)
        str_Purpose_b = UCase(str_Purpose_b)
        str_Purpose_c = UCase(str_Purpose_c)
        str_Purpose_d = UCase(str_Purpose_d)
        str_Source_Funds = UCase(str_Source_Funds)
        purpose_code = UCase(purpose_code)
        Try

            objComm.Parameters.Clear()
            ' objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA"
            ''objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA_RiskBased_3_with_branch_new"
            objComm.CommandText = "stp_tblBen_InsertUpdate_GID_ALFA_RiskBased_3_with_branch_new_CB"
            objComm.CommandType = CommandType.StoredProcedure
            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, 0))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CustID_GID))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Custid_Details_GID))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustID))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))


            Else
                objComm.Parameters.Add(New SqlParameter("@dcBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, dcBeneID))
                objComm.Parameters.Add(New SqlParameter("@BeneID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID))

                objComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@Custid_Details_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                objComm.Parameters.Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                objComm.Parameters.Add(New SqlParameter("@iCustid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))


            End If
            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@BenName_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Arb))
            objComm.Parameters.Add(New SqlParameter("@BenName_First", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_First))
            objComm.Parameters.Add(New SqlParameter("@BenName_Last", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Last))
            objComm.Parameters.Add(New SqlParameter("@BenName_Middle", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BenName_Middle))

            objComm.Parameters.Add(New SqlParameter("@BankIdentifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankIdentifier))
            objComm.Parameters.Add(New SqlParameter("@Bank_1_Identifier", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))

            'objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1_Identifier))
            'objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            'objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))


            objComm.Parameters.Add(New SqlParameter("@Bank", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank))
            objComm.Parameters.Add(New SqlParameter("@Bank_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_1))
            objComm.Parameters.Add(New SqlParameter("@Bank_2", SqlDbType.NVarChar, 128, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bank_2))



            objComm.Parameters.Add(New SqlParameter("@BankAcc", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_1))
            objComm.Parameters.Add(New SqlParameter("@BankAcc_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankAcc_2))

            objComm.Parameters.Add(New SqlParameter("@BankBaranch", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_1", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_1))
            objComm.Parameters.Add(New SqlParameter("@BankBaranch_2", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_BankBaranch_2))
            objComm.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address))
            objComm.Parameters.Add(New SqlParameter("@Address_1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Address_1))
            objComm.Parameters.Add(New SqlParameter("@HomePhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_HomePhoneNo))
            objComm.Parameters.Add(New SqlParameter("@WorkPhoneNo", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_WorkPhoneNo))
            objComm.Parameters.Add(New SqlParameter("@CellPhone", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_CellPhone))
            objComm.Parameters.Add(New SqlParameter("@CountryID_Nationality_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_Nationality_ben))
            objComm.Parameters.Add(New SqlParameter("@CountryID_ben", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CountryID_ben))
            objComm.Parameters.Add(New SqlParameter("@City_ben", SqlDbType.NVarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, City_ben))
            objComm.Parameters.Add(New SqlParameter("@zip_ben", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_zip_ben))
            objComm.Parameters.Add(New SqlParameter("@email_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_email_ben))
            objComm.Parameters.Add(New SqlParameter("@reference_ben", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_reference_ben))
            objComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, CCID))

            '**************************************************'**************************************************
            objComm.Parameters.Add(New SqlParameter("@iBeneid_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_Bene_ID))
            objComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iErrorCode))
            objComm.Parameters.Add(New SqlParameter("@BeneID_GID_ot", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_BeneID_GID_ot))

            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_a", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_a))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_b", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_b))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_c))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_d))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_e", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_e))
            objComm.Parameters.Add(New SqlParameter("@Bene_In_bnk_f", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Bene_In_bnk_f))
            objComm.Parameters.Add(New SqlParameter("@Purpose_a", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_a))
            objComm.Parameters.Add(New SqlParameter("@Purpose_b", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_b))
            objComm.Parameters.Add(New SqlParameter("@Purpose_c", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_c))
            objComm.Parameters.Add(New SqlParameter("@Purpose_d", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Purpose_d))

            objComm.Parameters.Add(New SqlParameter("@Source_Funds", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Source_Funds))

            objComm.Parameters.Add(New SqlParameter("@DTCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_DTCD))
            objComm.Parameters.Add(New SqlParameter("@SBCD_REF", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_SBCD))


            objComm.Parameters.Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
            objComm.Parameters.Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
            objComm.Parameters.Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
            objComm.Parameters.Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

            objComm.Parameters.Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
            objComm.Parameters.Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
            objComm.Parameters.Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
            objComm.Parameters.Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))


            objComm.Parameters.Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
            objComm.Parameters.Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 500, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


            objComm.Parameters.Add(New SqlParameter("@risk_Name_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Name))
            objComm.Parameters.Add(New SqlParameter("@risk_Nationality_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Nationality))
            objComm.Parameters.Add(New SqlParameter("@risk_Country_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Country))
            objComm.Parameters.Add(New SqlParameter("@risk_Address_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Address))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_ACC_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_ACC))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Identifier_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Identifier))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Name_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Name))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Address_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Address))
            objComm.Parameters.Add(New SqlParameter("@risk_Bank_Identifier_INT_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Bank_Identifier_INT))

            objComm.Parameters.Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
            objComm.Parameters.Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))
            objComm.Parameters.Add(New SqlParameter("@purpose_code", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, purpose_code))
            ' ''        ByVal risk_score As Double, _
            ' ''ByVal Risk_String As String)


            ''''''-----------CB FEILDS

            objComm.Parameters.Add(New SqlParameter("@BEN_ID_NO", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_ID_NO))
            objComm.Parameters.Add(New SqlParameter("@BEN_ID_TYPE", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_ID_TYPE))
            objComm.Parameters.Add(New SqlParameter("@BEN_ID_EXPIRY", SqlDbType.Date, 12, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_ID_EXPIRY))
            objComm.Parameters.Add(New SqlParameter("@BEN_CITY", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_CITY))
            objComm.Parameters.Add(New SqlParameter("@BEN_NEARAIRPORT", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_NEARAIRPORT))
            objComm.Parameters.Add(New SqlParameter("@BEN_RELATION", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BEN_RELATION))


            objComm.Parameters.Add(New SqlParameter("@CB_BEN_CityID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_BEN_CityID))
            objComm.Parameters.Add(New SqlParameter("@CB_BEN_PAYMODE", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_BEN_PAYMODE))
            objComm.Parameters.Add(New SqlParameter("@CB_BEN_PAYMODE_DESC", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, CB_BEN_PAYMODE_DESC))



            If Not IsUpdate Then
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            Else
                objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            End If

            objComm.ExecuteNonQuery()
            If Not IsUpdate Then
                m_Bene_ID = (objComm.Parameters.Item("@iBeneid_ot").Value)
                str_Beneid_ot = CStr(m_Bene_ID.Value.ToString)
                str_BeneID_GID_ot = objComm.Parameters.Item("@BeneID_GID_ot").Value
                m_iErrorCode = New SqlInt32(CType(objComm.Parameters.Item("@iErrorCode").Value, SqlInt32))
            End If
            '**************************************************'**************************************************
            '**************************************************'**************************************************

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub





    Public Sub mSave_Cust_detail_RiskProfile_kycc_2(ByRef loComm As SqlCommand, ByVal iCustid_Details As Long, _
                 ByVal sCustID_Details_GID_Update As String, _
                      ByVal iCustid As String, _
                 ByVal sCustID_GID As String, _
                   ByVal sPlaceOfIssue As String, _
                 ByVal iIDTypeID As Integer, _
                 ByVal sIDNumber As String, _
                 ByVal dDateOfIssue As Date, _
                 ByVal dExpiryDate As Date, _
                 ByVal sAddress1 As String, _
                 ByVal sAddress2 As String, _
                 ByVal sCity As String, _
                 ByVal sTel As String, _
                 ByVal sFax As String, _
                 ByVal sZIP As String, _
                 ByVal sEmail As String, _
                 ByVal sSponserName As String, _
                    ByVal sDetails As String, _
                 ByVal dmDate As Date, _
                 ByVal dDateof_Birth As Date, _
                  ByVal iUserID As Integer, _
                 ByVal BankAccNumber As String, _
                 ByVal BankName As String, _
                 ByVal BranchName As String, _
                 ByVal visa_id As String, _
                 ByVal CustTypeID_Child As String, _
                 ByVal CustTypeID_Child_SUB As String, _
                 ByVal Rep_Type As String, _
                 ByVal Volume_day_fx As Double, _
                 ByVal Value_day_fx As Double, _
                 ByVal Volume_Month_fx As Double, _
                 ByVal Value_Month_fx As Double, _
                 ByVal Volume_day_remittance As Double, _
                 ByVal Value_day_remittance As Double, _
                 ByVal Volume_Month_Remittance As Double, _
                 ByVal Value_Month_Remittance As Double, _
                 ByVal details_1 As String, _
                 ByVal details_2 As String, _
                 ByVal risk_score As Double, _
                 ByVal isUpdate As Boolean, _
                 ByRef iCustid_Details_ot As String, _
                 ByRef sCustID_Details_GID As String, _
                 ByRef iErrorCode As String, _
                 ByRef USERID_GID As String, _
                 ByRef Risk_String As String, _
                 ByRef bln_complinace_clear As Boolean, _
                 ByRef str_complinace_clear_by As String, _
                 ByRef str_complinacel_clear_details As String, _
       ByVal aux_1 As String, _
                 ByVal aux_2 As String, _
                   ByVal aux_3 As String, _
                 ByVal aux_4 As String, _
                   ByVal aux_5 As String, _
                 ByVal aux_6 As String, _
                   ByVal aux_7 As String, _
                 ByVal aux_8 As String, _
                    ByVal aux_9 As Date, _
                       ByVal aux_10 As Date, _
                          ByVal aux_11 As Date, _
                             ByVal aux_12 As Date, _
                              ByVal aux_13 As Date, _
                       ByVal aux_14 As Date, _
                          ByVal aux_15 As Date, _
                             ByVal aux_16 As Date, _
                               ByVal aux_17 As Integer, _
                       ByVal aux_18 As Integer, _
                          ByVal aux_19 As Integer, _
                             ByVal aux_20 As Integer, ByVal blnISBlock_remittance As Boolean, _
           ByVal risk_aux_1 As String, ByVal risk_aux_2 As String, ByVal risk_aux_3 As String, ByVal risk_aux_4 As String, _
           ByVal risk_aux_5 As String, ByVal risk_aux_6 As String, ByVal risk_aux_7 As String, ByVal risk_aux_8 As String, _
           ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String, _
           ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String)





        Dim m_iCustID_DET As SqlInt64
        sIDNumber = UCase(sIDNumber)
        sAddress1 = UCase(sAddress1)
        sAddress2 = UCase(sAddress2)
        sCity = UCase(sCity)
        sTel = UCase(sTel)
        sFax = UCase(sFax)
        sZIP = UCase(sZIP)
        sEmail = UCase(sEmail)
        sSponserName = UCase(sSponserName)
        BankAccNumber = UCase(BankAccNumber)
        BankName = UCase(BankName)
        BranchName = UCase(BranchName)
        sDetails = UCase(sDetails)
        sPlaceOfIssue = UCase(sPlaceOfIssue)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                ' loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident_risk_profile"

                loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk_new_01_kycc_WITH_BRANCH"
                loComm.CommandType = CommandType.StoredProcedure
                If isUpdate = False Then


                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                Else

                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_Details_GID_Update))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))


                End If



                .Add(New SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sPlaceOfIssue & ""))


                .Add(New SqlParameter("@IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iIDTypeID))



                .Add(New SqlParameter("@IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sIDNumber))
                .Add(New SqlParameter("@DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateOfIssue))
                .Add(New SqlParameter("@ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dExpiryDate))

                .Add(New SqlParameter("@Address1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress1))
                .Add(New SqlParameter("@Address2", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress2))

                .Add(New SqlParameter("@City", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCity))
                .Add(New SqlParameter("@Tel", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sTel))
                .Add(New SqlParameter("@Fax", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sFax))
                .Add(New SqlParameter("@ZIP", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sZIP))
                .Add(New SqlParameter("@Email", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sEmail))

                .Add(New SqlParameter("@SponserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sSponserName))
                .Add(New SqlParameter("@Details", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sDetails))


                .Add(New SqlParameter("@mDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, dmDate))
                .Add(New SqlParameter("@Dateof_Birth", SqlDbType.DateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateof_Birth))

                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))


                .Add(New SqlParameter("@BankAccNumber", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankAccNumber))
                .Add(New SqlParameter("@BankName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankName))
                .Add(New SqlParameter("@BranchName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BranchName))

                .Add(New SqlParameter("@USERID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, USERID_GID))
                .Add(New SqlParameter("@visa_id", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, visa_id))

                .Add(New SqlParameter("@CustTypeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child))
                .Add(New SqlParameter("@CustTypeID_Child_SUB", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child_SUB))
                .Add(New SqlParameter("@Rep_Type", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Type))
                .Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
                .Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
                .Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
                .Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

                .Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
                .Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
                .Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
                .Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))
                .Add(New SqlParameter("@details_1", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_1))
                .Add(New SqlParameter("@details_2", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_2))
                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@iCustid_Details_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID_DET))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, iErrorCode))
                .Add(New SqlParameter("@CustID_Details_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, sCustID_Details_GID))
                'bIsUpdate = Convert.ToBoolean(btstatus.Text)

                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 2000, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))


                .Add(New SqlParameter("@aux_1", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_1))
                .Add(New SqlParameter("@aux_2", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_2))
                .Add(New SqlParameter("@aux_3", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_3))
                .Add(New SqlParameter("@aux_4", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_4))

                .Add(New SqlParameter("@aux_5", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_5))
                .Add(New SqlParameter("@aux_6", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_6))
                .Add(New SqlParameter("@aux_7", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_7))
                .Add(New SqlParameter("@aux_8", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_8))

                .Add(New SqlParameter("@aux_9", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_9))

                .Add(New SqlParameter("@aux_10", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_10))

                .Add(New SqlParameter("@aux_11", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_11))

                .Add(New SqlParameter("@aux_12", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_12))


                .Add(New SqlParameter("@aux_13", _
         SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
         DataRowVersion.Proposed, aux_13))

                .Add(New SqlParameter("@aux_14", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_14))

                .Add(New SqlParameter("@aux_15", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_15))

                .Add(New SqlParameter("@aux_16", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_16))


                .Add(New SqlParameter("@aux_17", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_17))



                .Add(New SqlParameter("@aux_18", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_18))



                .Add(New SqlParameter("@aux_19", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_19))


                .Add(New SqlParameter("@aux_20", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_20))

                .Add(New SqlParameter("@is_remittance", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_remittance))


                .Add(New SqlParameter("@risk_aux_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_1))
                .Add(New SqlParameter("@risk_aux_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_2))
                .Add(New SqlParameter("@risk_aux_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_3))
                .Add(New SqlParameter("@risk_aux_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_4))
                .Add(New SqlParameter("@risk_aux_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_5))
                .Add(New SqlParameter("@risk_aux_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_6))
                .Add(New SqlParameter("@risk_aux_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_7))
                .Add(New SqlParameter("@risk_aux_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_8))
                .Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
                .Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
                .Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
                .Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
                .Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
                .Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
                .Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
                .Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))



                If Not isUpdate Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If


            End With

            loComm.ExecuteNonQuery()

            If Not isUpdate Then
                m_iCustID_DET = (loComm.Parameters.Item("@iCustid_Details_ot").Value)
                iCustid_Details_ot = CStr(m_iCustID_DET.Value.ToString)
                sCustID_Details_GID = CStr(loComm.Parameters.Item("@CustID_Details_GID").Value)
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Public Sub SAVE_CUST_MASTER_Fun_kycc(ByRef loComm As SqlCommand, ByVal iCustID As Long, _
                       ByVal iCCID As Integer, _
                            ByVal sCustID_GID_update As String, _
                       ByVal sCustName_Arb As String, _
                         ByVal sCustName_First As String, _
                       ByVal sCustName_Middle As String, _
                       ByVal sCustName_Last As String, _
                       ByVal iNat_CountryID As Integer, _
                       ByVal iCountryID As Integer, _
                       ByVal IsActive As Boolean, _
                       ByVal idtcd_Ref As Integer, _
                        ByVal isbcd_Ref As Integer, _
         ByVal sCustomerType As String, _
         ByVal str_user_id_gid As String, _
         ByVal isUpdate As Boolean, _
                                       ByRef str_CustID As String, _
                         ByRef str_CustID_GID As String, _
                          ByRef CUSTID_GID_OLD As String, _
                       ByRef str_ErrorCode As String, _
                       Optional ByVal emi_ID As Integer = 0, _
                       Optional ByVal custTypeid As Integer = 0)



        Dim m_iCustID As SqlInt64
        Dim m_iErrorCode As SqlInt32
        'sCustName_Arb = UCase(sCustName_Arb)
        sCustName_First = UCase(sCustName_First)
        sCustName_Middle = UCase(sCustName_Middle)
        sCustName_Last = UCase(sCustName_Last)
        sCustomerType = UCase(sCustomerType)
        loComm.Parameters.Clear()


        With loComm.Parameters
            loComm.CommandText = "dbo.[stp_tblCust_Insert_tblCustomer_kyc]"
            'loComm.CommandText = "dbo.[stp_tblCust_InsertUpdate_GID_NW_55_kycc_WITH_BRANCH]"
            loComm.CommandType = CommandType.StoredProcedure

            'If isUpdate = False Then
            loComm.Parameters.Add(New SqlParameter("@CustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
            loComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, iCCID))
            loComm.Parameters.Add(New SqlParameter("@CustID_GID_update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
            'Else
            'loComm.Parameters.Add(New SqlParameter("@CustID", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustID))
            'loComm.Parameters.Add(New SqlParameter("@CCID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, iCCID))
            'loComm.Parameters.Add(New SqlParameter("@CustID_GID_update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID_update))
            'End If

            loComm.Parameters.Add(New SqlParameter("@CustName_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustName_Arb))

            loComm.Parameters.Add(New SqlParameter("@CustName_First", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustName_First))
            loComm.Parameters.Add(New SqlParameter("@CustName_Middle", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustName_Middle))
            loComm.Parameters.Add(New SqlParameter("@CustName_Last", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustName_Last))

            loComm.Parameters.Add(New SqlParameter("@Nat_CountryID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, iNat_CountryID))


            loComm.Parameters.Add(New SqlParameter("@CountryID", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, System.DBNull.Value))
            loComm.Parameters.Add(New SqlParameter("@IsActive", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, IsActive))
            loComm.Parameters.Add(New SqlParameter("@dtcd_Ref", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, idtcd_Ref))
            loComm.Parameters.Add(New SqlParameter("@sbcd_Ref", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, isbcd_Ref))

            loComm.Parameters.Add(New SqlParameter("@CustomerType", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustomerType))


            loComm.Parameters.Add(New SqlParameter("@iCustID_Ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID))
            loComm.Parameters.Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_CustID_GID))

            loComm.Parameters.Add(New SqlParameter("@user_id_gid", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_user_id_gid))

            loComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iErrorCode))

            If isUpdate = False Then
                loComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            Else
                loComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            End If

            loComm.Parameters.Add(New SqlParameter("@emi_ID", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, emi_ID))
            loComm.Parameters.Add(New SqlParameter("@custTypeid", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, custTypeid))
            loComm.Parameters.Add(New SqlParameter("@CUSTID_GID_OLD", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CUSTID_GID_OLD))




        End With
        loComm.ExecuteNonQuery()

        Try
            If isUpdate = False Then
                m_iCustID = (loComm.Parameters.Item("@iCustID_Ot").Value)
                str_CustID = CStr(m_iCustID.Value.ToString)
                str_CustID_GID = CStr(loComm.Parameters.Item("@CustID_GID").Value)
                'Else
                '    m_iCustID = New SqlInt64(CType(txtCustomerID.Text, SqlInt16))
                '    m_iCustID_STR = CStr(m_iCustID.Value.ToString)
                '    m_iCustID_STR = txtCustomerID.Text
            End If


            m_iErrorCode = New SqlInt32(CType(loComm.Parameters.Item("@iErrorCode").Value, SqlInt32))
            str_ErrorCode = CStr(m_iErrorCode.Value.ToString)

        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator...")

        End Try

    End Sub


    Public Sub mSave_Cust_detail_RiskProfile_kycc_3(ByRef loComm As SqlCommand, ByVal iCustid_Details As Long, _
               ByVal sCustID_Details_GID_Update As String, _
                    ByVal iCustid As String, _
               ByVal sCustID_GID As String, _
                 ByVal sPlaceOfIssue As String, _
               ByVal iIDTypeID As Integer, _
               ByVal sIDNumber As String, _
               ByVal dDateOfIssue As Date, _
               ByVal dExpiryDate As Date, _
               ByVal sAddress1 As String, _
               ByVal sAddress2 As String, _
               ByVal sCity As String, _
               ByVal sTel As String, _
               ByVal sFax As String, _
               ByVal sZIP As String, _
               ByVal sEmail As String, _
               ByVal sSponserName As String, _
                  ByVal sDetails As String, _
               ByVal dmDate As Date, _
               ByVal dDateof_Birth As Date, _
                ByVal iUserID As Integer, _
               ByVal BankAccNumber As String, _
               ByVal BankName As String, _
               ByVal BranchName As String, _
               ByVal visa_id As String, _
               ByVal CustTypeID_Child As String, _
               ByVal CustTypeID_Child_SUB As String, _
               ByVal Rep_Type As String, _
               ByVal Volume_day_fx As Double, _
               ByVal Value_day_fx As Double, _
               ByVal Volume_Month_fx As Double, _
               ByVal Value_Month_fx As Double, _
               ByVal Volume_day_remittance As Double, _
               ByVal Value_day_remittance As Double, _
               ByVal Volume_Month_Remittance As Double, _
               ByVal Value_Month_Remittance As Double, _
               ByVal details_1 As String, _
               ByVal details_2 As String, _
               ByVal risk_score As Double, _
               ByVal isUpdate As Boolean, _
               ByRef iCustid_Details_ot As String, _
               ByRef sCustID_Details_GID As String, _
               ByRef iErrorCode As String, _
               ByRef USERID_GID As String, _
               ByRef Risk_String As String, _
               ByRef bln_complinace_clear As Boolean, _
               ByRef str_complinace_clear_by As String, _
               ByRef str_complinacel_clear_details As String, _
     ByVal aux_1 As String, _
               ByVal aux_2 As String, _
                 ByVal aux_3 As String, _
               ByVal aux_4 As String, _
                 ByVal aux_5 As String, _
               ByVal aux_6 As String, _
                 ByVal aux_7 As String, _
               ByVal aux_8 As String, _
                  ByVal aux_9 As Date, _
                     ByVal aux_10 As Date, _
                        ByVal aux_11 As Date, _
                           ByVal aux_12 As Date, _
                            ByVal aux_13 As Date, _
                     ByVal aux_14 As Date, _
                        ByVal aux_15 As Date, _
                           ByVal aux_16 As Date, _
                             ByVal aux_17 As Integer, _
                     ByVal aux_18 As Integer, _
                        ByVal aux_19 As Integer, _
                           ByVal aux_20 As Integer, ByVal blnISBlock_remittance As Boolean, _
         ByVal risk_aux_1 As String, ByVal risk_aux_2 As String, ByVal risk_aux_3 As String, ByVal risk_aux_4 As String, _
         ByVal risk_aux_5 As String, ByVal risk_aux_6 As String, ByVal risk_aux_7 As String, ByVal risk_aux_8 As String, _
         ByVal risk_aux_9 As String, ByVal risk_aux_10 As String, ByVal risk_aux_11 As String, ByVal risk_aux_12 As String, _
         ByVal risk_aux_13 As String, ByVal risk_aux_14 As String, ByVal risk_aux_15 As String, ByVal risk_aux_16 As String)





        Dim m_iCustID_DET As SqlInt64
        sIDNumber = UCase(sIDNumber)
        sAddress1 = UCase(sAddress1)
        sAddress2 = UCase(sAddress2)
        sCity = UCase(sCity)
        sTel = UCase(sTel)
        sFax = UCase(sFax)
        sZIP = UCase(sZIP)
        sEmail = UCase(sEmail)
        sSponserName = UCase(sSponserName)
        BankAccNumber = UCase(BankAccNumber)
        BankName = UCase(BankName)
        BranchName = UCase(BranchName)
        sDetails = UCase(sDetails)
        sPlaceOfIssue = UCase(sPlaceOfIssue)


        Try


            loComm.Parameters.Clear()

            With loComm.Parameters

                ' loComm.CommandText = "dbo.stp_tblCust_detail_InsertUpdate_GID_NW_Resident_risk_profile"

                loComm.CommandText = "stp_tblCust_detail_InsertUpdate_withRisk_new_02_kycc_WITH_BRANCH"
                loComm.CommandType = CommandType.StoredProcedure
                If isUpdate = False Then


                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, 0))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_GID))


                Else

                    .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, _
                    ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid_Details))
                    .Add(New SqlParameter("@CustID_Details_GID_Update", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, sCustID_Details_GID_Update))
                    .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCustid))
                    .Add(New SqlParameter("@CustID_GID", SqlDbType.NVarChar, 48, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, ""))


                End If



                .Add(New SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sPlaceOfIssue & ""))


                .Add(New SqlParameter("@IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iIDTypeID))



                .Add(New SqlParameter("@IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sIDNumber))
                .Add(New SqlParameter("@DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateOfIssue))
                .Add(New SqlParameter("@ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dExpiryDate))

                .Add(New SqlParameter("@Address1", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress1))
                .Add(New SqlParameter("@Address2", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sAddress2))

                .Add(New SqlParameter("@City", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCity))
                .Add(New SqlParameter("@Tel", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sTel))
                .Add(New SqlParameter("@Fax", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sFax))
                .Add(New SqlParameter("@ZIP", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sZIP))
                .Add(New SqlParameter("@Email", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sEmail))

                .Add(New SqlParameter("@SponserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sSponserName))
                .Add(New SqlParameter("@Details", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sDetails))


                .Add(New SqlParameter("@mDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, dmDate))
                .Add(New SqlParameter("@Dateof_Birth", SqlDbType.DateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dDateof_Birth))

                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))


                .Add(New SqlParameter("@BankAccNumber", SqlDbType.NVarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankAccNumber))
                .Add(New SqlParameter("@BankName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BankName))
                .Add(New SqlParameter("@BranchName", SqlDbType.NVarChar, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, BranchName))

                .Add(New SqlParameter("@USERID_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, USERID_GID))
                .Add(New SqlParameter("@visa_id", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, visa_id))

                .Add(New SqlParameter("@CustTypeID_Child", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child))
                .Add(New SqlParameter("@CustTypeID_Child_SUB", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, CustTypeID_Child_SUB))
                .Add(New SqlParameter("@Rep_Type", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Type))
                .Add(New SqlParameter("@Volume_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_fx))
                .Add(New SqlParameter("@Value_day_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_fx))
                .Add(New SqlParameter("@Volume_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_fx))
                .Add(New SqlParameter("@Value_Month_fx", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_fx))

                .Add(New SqlParameter("@Volume_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_day_remittance))
                .Add(New SqlParameter("@Value_day_remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_day_remittance))
                .Add(New SqlParameter("@Volume_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Volume_Month_Remittance))
                .Add(New SqlParameter("@Value_Month_Remittance", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Value_Month_Remittance))
                .Add(New SqlParameter("@details_1", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_1))
                .Add(New SqlParameter("@details_2", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, details_2))
                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@iCustid_Details_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, m_iCustID_DET))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, iErrorCode))
                .Add(New SqlParameter("@CustID_Details_GID", SqlDbType.NVarChar, 48, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, sCustID_Details_GID))
                'bIsUpdate = Convert.ToBoolean(btstatus.Text)

                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 2000, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))


                .Add(New SqlParameter("@aux_1", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_1))
                .Add(New SqlParameter("@aux_2", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_2))
                .Add(New SqlParameter("@aux_3", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_3))
                .Add(New SqlParameter("@aux_4", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_4))

                .Add(New SqlParameter("@aux_5", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_5))
                .Add(New SqlParameter("@aux_6", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_6))
                .Add(New SqlParameter("@aux_7", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_7))
                .Add(New SqlParameter("@aux_8", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, aux_8))

                .Add(New SqlParameter("@aux_9", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_9))

                .Add(New SqlParameter("@aux_10", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_10))

                .Add(New SqlParameter("@aux_11", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_11))

                .Add(New SqlParameter("@aux_12", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_12))


                .Add(New SqlParameter("@aux_13", _
         SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
         DataRowVersion.Proposed, aux_13))

                .Add(New SqlParameter("@aux_14", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_14))

                .Add(New SqlParameter("@aux_15", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_15))

                .Add(New SqlParameter("@aux_16", _
            SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", _
            DataRowVersion.Proposed, aux_16))


                .Add(New SqlParameter("@aux_17", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_17))



                .Add(New SqlParameter("@aux_18", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_18))



                .Add(New SqlParameter("@aux_19", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_19))


                .Add(New SqlParameter("@aux_20", SqlDbType.SmallInt, 2, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, aux_20))

                .Add(New SqlParameter("@is_remittance", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, blnISBlock_remittance))


                .Add(New SqlParameter("@risk_aux_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_1))
                .Add(New SqlParameter("@risk_aux_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_2))
                .Add(New SqlParameter("@risk_aux_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_3))
                .Add(New SqlParameter("@risk_aux_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_4))
                .Add(New SqlParameter("@risk_aux_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_5))
                .Add(New SqlParameter("@risk_aux_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_6))
                .Add(New SqlParameter("@risk_aux_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_7))
                .Add(New SqlParameter("@risk_aux_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_8))
                .Add(New SqlParameter("@risk_aux_9", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_9))
                .Add(New SqlParameter("@risk_aux_10", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_10))
                .Add(New SqlParameter("@risk_aux_11", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_11))
                .Add(New SqlParameter("@risk_aux_12", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_12))
                .Add(New SqlParameter("@risk_aux_13", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_13))
                .Add(New SqlParameter("@risk_aux_14", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_14))
                .Add(New SqlParameter("@risk_aux_15", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_15))
                .Add(New SqlParameter("@risk_aux_16", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_16))



                If Not isUpdate Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If


            End With

            loComm.ExecuteNonQuery()

            If Not isUpdate Then
                m_iCustID_DET = (loComm.Parameters.Item("@iCustid_Details_ot").Value)
                iCustid_Details_ot = CStr(m_iCustID_DET.Value.ToString)
                sCustID_Details_GID = CStr(loComm.Parameters.Item("@CustID_Details_GID").Value)
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub SAVE_CUST_detail_REP_RISK_PROFILE_2(ByRef loComm As SqlCommand, ByVal sRep_ID_GID_UPDATE As String, _
                                 ByVal lCustid As Long, _
                                 ByVal sCustid_GID As String, _
                                 ByVal sRepName As String, _
                                 ByVal sRep_Name_Arb As String, _
                                 ByVal iRep_IDTypeID As Integer, _
                                 ByVal sRep_IDNumber As String, _
                                 ByVal dRep_DateOfIssue As Date, _
                                 ByVal dRep_ExpiryDate As Date, _
                                 ByVal iUserID As Integer, _
                                 ByVal IsUpdate As Boolean, _
                                 ByRef str_Rep_ID_ot As String, _
                                 ByRef str_Rep_ID_GID_ot As String, _
                                 ByRef str_ErrorCode As String, _
                                 ByRef str_Rep_CELL As String, _
                                 ByRef str_Rep_IdPlaceOfIssue As String, _
                                 ByRef str_Rep_Nationality As String, _
                                 ByRef nDTCD As String, ByRef nSBCD As Integer, _
                                 ByRef Rep_DOB As Date, _
                                 ByRef Rep_ADD As String, _
                                 ByRef Rep_Email As String, _
                                  ByRef Rep_ID As String, _
                                   ByRef Rep_ID_GID As String, _
                                 ByRef Rep_Remarks As String, ByRef Remarks As String, ByRef userid_gid As String, _
                                   ByVal risk_score As Double, _
                                     ByRef Risk_String As String, _
                  ByRef bln_complinace_clear As Boolean, _
                  ByRef str_complinace_clear_by As String, _
                  ByRef str_complinacel_clear_details As String,
                  ByVal risk_Name_1 As String,
                  ByVal risk_Nationality_2 As String,
                  ByVal risk_Address_3 As String,
                  ByVal risk_POI_4 As String,
                  ByVal risk_aux_5 As String,
                  ByVal risk_aux_6 As String,
                  ByVal risk_aux_7 As String,
                  ByVal risk_aux_8 As String)

        Try




            sRepName = UCase(sRepName)
            'UCase(sRep_Name_Arb)
            sRep_IDNumber = UCase(sRep_IDNumber)


            loComm.Parameters.Clear()


            With loComm.Parameters

                loComm.CommandText = "dbo.[stp_tblCust_Rep_InsertUpdate_GID_new_RISK_PROFILE_with_branch_1]"
                loComm.CommandType = CommandType.StoredProcedure


                If IsUpdate = False Then
                    .Add(New SqlParameter("@Rep_ID_GID_UPDATE", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, ""))
                Else
                    .Add(New SqlParameter("@Rep_ID_GID_UPDATE", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_ID_GID_UPDATE))
                End If

                .Add(New SqlParameter("@Custid", SqlDbType.BigInt, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, lCustid))
                .Add(New SqlParameter("@Custid_GID", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sCustid_GID))
                .Add(New SqlParameter("@RepName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRepName))
                .Add(New SqlParameter("@Rep_Name_Arb", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_Name_Arb))



                .Add(New SqlParameter("@Rep_IDTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iRep_IDTypeID))

                .Add(New SqlParameter("@Rep_IDNumber", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sRep_IDNumber))

                .Add(New SqlParameter("@Rep_DateOfIssue", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dRep_DateOfIssue))
                .Add(New SqlParameter("@Rep_ExpiryDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, dRep_ExpiryDate))





                .Add(New SqlParameter("@UserID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                                       False, 23, 3, "", DataRowVersion.Proposed, iUserID))

                If IsUpdate = False Then
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
                    False, 1, 0, "", DataRowVersion.Proposed, 0))
                Else
                    .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
                    False, 1, 0, "", DataRowVersion.Proposed, 1))
                End If

                .Add(New SqlParameter("@Rep_ID_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_ID_ot))
                .Add(New SqlParameter("@Rep_ID_GID_ot", SqlDbType.NVarChar, 50, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Proposed, str_Rep_ID_GID_ot))
                .Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_ErrorCode))




                .Add(New SqlParameter("@Rep_CELL", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_Rep_CELL))
                .Add(New SqlParameter("@Rep_IdPlaceOfIssue", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_IdPlaceOfIssue))
                .Add(New SqlParameter("@Rep_Nationality", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, str_Rep_Nationality))

                .Add(New SqlParameter("@DTCD", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, nDTCD))
                .Add(New SqlParameter("@SBCD", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, nSBCD))



                .Add(New SqlParameter("@Rep_DOB", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, False, 16, 0, "", DataRowVersion.Proposed, Rep_DOB))
                .Add(New SqlParameter("@Rep_ADD", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_ADD))
                .Add(New SqlParameter("@Rep_Email", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Email))
                .Add(New SqlParameter("@Rep_Remarks", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Rep_Remarks))
                .Add(New SqlParameter("@Remarks", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Remarks))
                .Add(New SqlParameter("@user_id_gid", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, userid_gid))
                .Add(New SqlParameter("@risk_score", SqlDbType.Float, 8, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, risk_score))
                .Add(New SqlParameter("@Risk_String", SqlDbType.NVarChar, 1000, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Risk_String))


                .Add(New SqlParameter("@complinace_clear", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, bln_complinace_clear))
                .Add(New SqlParameter("@complinace_clear_by", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinace_clear_by))
                .Add(New SqlParameter("@complinacel_clear_details", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_complinacel_clear_details))

                .Add(New SqlParameter("@risk_Name_1", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Name_1))
                .Add(New SqlParameter("@risk_Nationality_2", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Nationality_2))
                .Add(New SqlParameter("@risk_Address_3", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_Address_3))
                .Add(New SqlParameter("@risk_POI_4", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_POI_4))
                .Add(New SqlParameter("@risk_aux_5", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_5))
                .Add(New SqlParameter("@risk_aux_6", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_6))
                .Add(New SqlParameter("@risk_aux_7", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_7))
                .Add(New SqlParameter("@risk_aux_8", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, risk_aux_8))


            End With
            loComm.ExecuteNonQuery()
            Dim nrep_id As Integer = 0
            If IsUpdate = False Then

                Rep_ID = (loComm.Parameters.Item("@Rep_ID_ot").Value)
                Rep_ID_GID = CStr(loComm.Parameters.Item("@Rep_ID_GID_ot").Value)

            End If

        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator...")

        End Try


    End Sub
End Class
