Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Web

Imports Microsoft.VisualBasic
Imports System.Security.Cryptography
Imports System.IO
Imports System.Xml
Imports System.Net

Public Class modMain
    Public gUserID, gUserLogin, gUserNAME, gUserPWD, gUserPWD_Rev As String
    Public gUSD_Rate As Double
    Public gUSD_Rate_TT As Double
    Public nID As String
    Public i, j, errorCode As Integer
    Public gFlag, isError As Boolean
    Public strSql As String
    Public gobjListItem As New ListItem
    Public gERRmsg As String
    Public arrUserRightsName(300) As String
    Public gCon As New SqlClient.SqlConnection
    Public gCon_IMAGE As New SqlClient.SqlConnection
    Public gCon_WCHK As New SqlClient.SqlConnection
    Public gCon_WPS As New SqlClient.SqlConnection

    Public objDS As New DataSet()
    Public objComm As New SqlCommand
    Public objTrans As SqlTransaction
    Public gobjUserDataTable As DataTable
    Public gobjUserRightsTable As DataTable
    Protected objReader As SqlDataReader
    Public gVoucherLength As Integer = 6
    Public gServerDATE As DateTime
    Public gServerDate_Str As String

    Public Const gVchNew As String = " -- NEW -- "


    Public Const gSTR_BLOCK As String = "BLOCK"
    Public Const gSTR_ACTIVE As String = "ACTIVE"

    Public Const gAPP_MSG As String = "The approval changes, financial positions as of transaction date"

    Public Const gAPP_MSG_TT_STS As String = "The approval changes, Remittance to NEXT level of process"

    Public Const gFormatRate As String = "###,##0.000000000"
    Public Const gFormatRate_SML As String = "###,##0.00000"
    Public Const gFormatRate_SML_1 As String = "###,##0.000000"
    Public Const gFormatAmount As String = "###,##0.00"
    Public Const gFormat_DT As String = "dd-MMM-yyyy"
    Public Const gFormat_DT_Short As String = "DD/MM/YYYY"
    Public Const gFormatRisk As String = "###,##0"
    Public Const gFormatNO As String = "#####0"


    Public Const gStrSelectEvent As String = "SELECT_EVENT"
    Public Const gStrSelectEvent_2 As String = "SELECT_EVENT_2_Cols"
    Public Const gStrSelectEvent_3 As String = "SELECT_EVENT_3_Cols"
    Public Const gStrSelectEvent_4 As String = "SELECT_EVENT_4_Cols"
    Public Const gStrSelectEvent_6 As String = "SELECT_EVENT_6_Cols"
    Public Const gStr_Empty As String = "--EMPTY--"
    Public Const gN_htmlSPACE As Integer = 6
    Public Const gN_BaseCCID As Integer = 1
    Public Const isAUTO_CLEAR As Boolean = False
   

    Public Const gDBNAME As String = "Ticket_System"
    Public Const gSTRVER As String = "  V-02/04-FEB-2019 " & gDBNAME

    Public Const gStrCnString As String = "Data Source=10.10.0.51; user id = sa; password=111; initial catalog='" & gDBNAME & "' ; Connection Lifetime=5;Max Pool Size=30;Pooling='true';Connect Timeout=20;"


    Public Const gDBNAME_IMAGE As String = "IMAGE_2018"
    Public Const gStrCnString_IMAGE As String = "Data Source=ADMIN-PC; user id = sa; password=111; initial catalog='" & gDBNAME_IMAGE & "'; Connection Lifetime=5;Max Pool Size=30;Pooling='true';Connect Timeout=20;"


    Public Shared Function Encrypt(ByVal PublicKey As String, ByVal data As String) As String
        Using rsa = New RSACryptoServiceProvider()
            rsa.FromXmlString(PublicKey)
            Dim bytesToEncrypt = Encoding.UTF8.GetBytes(data)
            Dim encryptedBytes = rsa.Encrypt(bytesToEncrypt, False)
            Return Convert.ToBase64String(encryptedBytes)
        End Using
    End Function


    Public Enum DTTB_FILL_CALLS
        MainAccountcode = 1
        Maincode = 2

        Detailcode = 3
        subDetailcode = 4

        subcode = 5
        INCOMEACCOUNT = 6

        rptJV = 7
        rptCP_CR = 8
        getccy = 9
        getccyRate = 10

        API_BOC = 11
        API_SR = 12
        API_SR_detail = 13
    End Enum


    Public Enum enm_RPTS
        P_L = 4033
        TRIAL_BALANCE = 4031
        TRIAL_BALANCE_DateRange_FINAL_BALANCE = 4032


        TB_DATE_RANGE = 6002


        TRIAL_BALANCE_FCY_WITH_SUP_STS = 6003
        TRIAL_BALANCE_FCY_COORPARATE = 4589
        TRIAL_BALANCE_ACCOUNT_TYPE = 20463

        BALANCE_SHEET = 4034
        BALANCE_SHEET_DETAILED = 4035
        FCY_LEDGERS = 4027
        PARTY_ACCOUNTS_FCY = 4028
        PARTY_ACCOUNTS_FCY_II = 4029
        PARTY_ACCOUNTS_FCY_III = 4030
        CASH_INHAND_STATEMENT = 4050
        CASH_INHAND_STATEMENT_DATERANGE = 20991
        REMITTANCE_OUTWARD_ANALYSIS = 4051
        REMITTANCE_OUTWARD_ANALYSIS_FORMAT_II = 4052
        REMITTANCE_INWARD_ANALYSIS = 4053
        REMITTANCE_INWARD_ANALYSIS_FORMAT_II = 4054
        RO_RI_ANALYSIS = 4055
        EXPENSES_SHEET_ANALYSIS = 4056
        CASH_PAYMENTS_DETAILS = 4057
        P_L_ANALYSIS = 4058
        P_L_ANALYSIS_FORMAT_II = 4059
        BUSINESS_SUMMARY_REPORT = 4060
        SALE_PURCHASE_ANALYSIS = 4042
        SALE_PURCHASE_ANALYSIS_AGENT = 4142
        SALE_PURCHASE_ANALYSIS_FORMAT_II = 4043
        CUSTOMER_LISTING_DETAIL = 4019
        CUSTOMER_DEFINITION = 4015
        CURRENCY_STOCK = 4046
        CURRENCY_STOCK_FORMAT_II = 4047

        ITEM_INV_PRICE = 5021
        REVALUATION_SHEET = 6001
        CENTRAL_BANK_REMITTANCE_REPORT = 4100
        CENTRAL_BANK_FORM_3 = 4101
        CENTRAL_BANK_FORM_3_FC_LC = 4102
        CENTRAL_BANK_Remittance_Report_CountryWise = 4103
        TELLER_CASH = 20245
        TELLER_CASH_REPORT = 4104
        CENTRAL_BANK_INWARD_REMITTANCE_REPORT = 4105
        CENTRAL_BANK_OUTWARD_REMITTANCE_REPORT = 4106
        SALE_PURCHASE_ANALYSIS_FORMAT_III = 4108
        SALE_PURCHASE_ANALYSIS_FORMAT_IIII = 4109
        SALE_PURCHASE_CASH_FOLLOW = 4110

        CASHIER_ACCOUNTS_FOR_FRONT_DESK_branch = 3072
        CHART_OF_ACCOUNTS_BRANCH = 4123

        CHART_OF_ACCOUNTS_CR_LIMIT = 6014


        CURRENCY_EXPOSURE = 4127
        CURRENCY_EXPOSURE_ANX_I = 4128
        CURRENCY_EXPOSURE_2 = 4134
        CURRENCY_EXPOSURE_RPT = 20950
        CURRENCY_EXPOSURE_SUMMARY = 20951

        COMPLIANCE_RISK_PROFILE_BY_COUNTRY = 3076
        COMPLIANCE_Risk_By_Sender_Value = 3077
        COMPLIANCE_Risk_By_Sender_Volume = 3078
        COMPLIANCE_Risk_By_Receiver_Value = 3079
        COMPLIANCE_Risk_By_Receiver_Volume = 3080
        COMPLIANCE_Risk_By_Receiver_Purpose = 3081
        COMPLIANCE_SCORE_SETTING = 3082
        COMPLIANCE_Exceptional = 3085

        Teller_Currency_Stock = 20248

        REVALUATION_SHEET_FORMAT_II = 6015
        REVALUATION_SHEET_BY_CURRENCY = 6016
        COMPLIANCE_WORLD_CHECK_EXCEPTIONAL_WORDS_REPORT = 6035
        CENTRAL_BANK_INWARD_REMITTANCE_REPORT_QUARTYLY = 6041
        CENTRAL_BANK_OUTWARD_REMITTANCE_REPORT_QUARTYLY = 6040

        CURRENCY_STOCK_COST_RATE = 20392
        CURRENCY_STOCK_COST_RATE_FIFO = 20465
        CURRENCY_STOCK_COST_RATE_CONSOLIDATED = 6071

        CUSTOMER_CAD_FILE_PENDING = 6045
        WPS_PENDINGRECEIPTS = 4189

        BENIFICIARY_LISTING = 4209
        REPRESENTATIVE_LISTING = 4210
        TRANSCATION_LOG = 7014
        FCY_TRANSCATION_LOG = 7029
        CURRENCY_SALE_STATMENT = 20393
        FRGS_REPORT = 7030
        FCY_COMPLIANCE_DECLINED_LOG = 20467
        FCY_HIT_LOG = 20500
        CASH_FLOW_DAY_END = 20468

        Multiple_beneficiaries_one_to_many = 20601
        Multiple_remitters_many_to_one = 20602
        High_value_single_transaction_Individual = 20603
        High_value_single_transaction_Corporate = 20604
        High_value_transactions_cumulative_Individual = 20605
        High_value_transactions_cumulative_Corporate = 20606
        Transaction_count_Individual = 20607
        Transaction_count_Corporate = 20608
        Send_to_high_risk_country = 20609
        Receive_from_high_risk_country = 20610
        High_risk_currency = 20611
        Nationality = 20612
        Transaction_count_pattern = 20613
        Multiple_branch_transactions = 20614
        High_risk_customers_manual_risk = 20615
        High_risk_customers_AML_risk = 20616
        Splitting_of_transactions = 20617
        In_and_out_transactions = 20618
        Customer_mobile_number_combination = 20619
        TC_encashment_Individual = 20620
        Currency_exchange = 20621
        Cash_payments_by_Corporate = 20622
        Multiple_beneficiaries_receive_transactions = 20623
        Nationality_country_combination_Send_Transaction = 20624
        Nationality_country_combination_Receive_Transaction = 20625
        Nationality_nationality_combination_Send_Transaction = 20626
        Nationality_nationality_combination_Receive_Transaction = 20627
        Multiple_country_Send = 20628
        Multiple_nationality_Send = 20629
        Multiple_country_Receive = 20630
        Multiple_nationality_Receive = 20631
        Beneficiary_name_suggests = 20632
        Watch_List_customer = 20633
        Employee_threshold = 20634
        Indian_bank_remittance_more_than_threshold = 20635


    End Enum





    Public Enum enm_RPTS_SMS
        DAY_END_ALERT = 6006
        CASH_POSITION_ALERT = 6007
        BANK_POSITION_ALERT = 6008
        EXPENSE_SUMMARY_ALERT = 6009
        PROFIT_AND_LOSS_ALERT = 6010
        REMITTANCE_SUMMARY = 6019
    End Enum





    Public Enum enm_RPTS_OLAP
        SALE_PURCHASE_ANALYSIS = 4117
        TELLER_STOCK_REPORT = 4118
        CCY_ASSOCIATION = 4119
        DAY_END_ANALYSIS = 4120
        CORRESPONDENT_CURRENCY_RATE_SETTING = 4121
        CORRESPONDENT_BALANCE = 4122
        PROFIT_LOSS_STATEMENT = 4131
        REMITTANCE_OUTWARD = 4132
        FWD_BALANCE_ANALYSIS = 4133
    End Enum


    Public Enum enm_Acc_Type
        ASSETS = 1
        LIABILITIES = 2
        INCOME = 3
        EXPENSE = 4
        Asset_Liability = 5
        EQUITY = 6
    End Enum

    Public Enum eLG_Prt
        NORMAL = 1
        MIDIUM = 2
        HIGH = 3

    End Enum

    Public Enum eLG_TYP
        SAVE_TRANSACTION = 1
        CHANGE_DATE = 2
        CANCEL_TRANSACTION = 3
        REPORT_GENERATE = 4
        CHANGE_STATUS = 5
        FWD_ADJUSTMENT = 6

        CUSTOMER_LOG = 7
        REP_LOG = 8
        CUST_NAME_UPDATE = 9
        CUST_Image_Update = 10
    End Enum




    Public Enum enm_CST_Type
        Individual = 5
        Corporate = 6
        Financial_Institution = 7
    End Enum

    Public Enum enm_CST_RISK
        high = 3
       
    End Enum

    Public Sub getCCY_FWD_ROTT(ByVal strCCY As String, _
                              ByVal dtTB_CCY As DataTable, _
                              ByRef nHoCost As Double, _
                              ByRef nBRCost As Double, _
                              ByRef isFWD_CCY As Boolean)

        Try
            Dim mRWs As DataRow() = dtTB_CCY.Select(strCCY)
            For Each mRW As DataRow In mRWs

                nHoCost = mRW("HoCost")
                nBRCost = mRW("BRCost")
                isFWD_CCY = mRW("isFWD_CCY")

                Exit For
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTB_CCY.Dispose()
            dtTB_CCY = Nothing
        End Try
    End Sub


    Public Sub getCCY_Details(ByVal strCCY As String, _
                              ByVal dtTB_CCY As DataTable, _
                              ByRef nSBCD_CCY As Integer, _
                              ByRef nCCYID As Integer)

        Try


            Dim mRWs As DataRow() = dtTB_CCY.Select(strCCY)
            For Each mRW As DataRow In mRWs
                nSBCD_CCY = mRW("SubCodeID")
                nCCYID = mRW("currencyID")
                Exit For
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTB_CCY.Dispose()
            dtTB_CCY = Nothing
        End Try
    End Sub

    Public Sub getCCY_ID(ByVal strCCY As String, _
                             ByVal dtTB_CCY As DataTable, _
                             ByRef nCCYID As Integer)

        Try


            Dim mRWs As DataRow() = dtTB_CCY.Select(strCCY)
            For Each mRW As DataRow In mRWs
                nCCYID = mRW("currencyID")
                Exit For
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTB_CCY.Dispose()
            dtTB_CCY = Nothing
        End Try
    End Sub
    Public Sub getUser_Rights_sup(ByVal strRight As String, _
                            ByRef dtTB As DataTable, _
                            ByRef isAllowed As Boolean, ByRef strURL As String)

        Try
            Dim mRWs As DataRow() = dtTB.Select(strRight)
            For Each mRW As DataRow In mRWs
                isAllowed = True
                strURL = mRW("URL")
                Exit For
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Public Sub getUser_Rights(ByVal nPageID As Integer, _
                           ByRef dtTB As DataTable, _
                           ByRef isAllowed As Boolean)

        Try
            Dim strRight As String = " MenuID =  " & nPageID & "  "
            Dim mRWs As DataRow() = dtTB.Select(strRight)
            For Each mRW As DataRow In mRWs
                isAllowed = True
                Exit For
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub




    Public Sub getACC_DTCD(ByVal strCODE As String, _
                          ByRef nDTCD As Integer, _
                        ByRef nSBCD As Integer, _
                        ByRef strACC As String)
        Dim nTemp As String = ""
        Try
            nDTCD = Mid(strCODE, 1, InStr(strCODE, "-") - 1)
            nTemp = Mid(strCODE, InStr(strCODE, "-") + 1, InStr(strCODE, ",") - 1)
            nSBCD = Mid(nTemp, 1, InStr(nTemp, ",") - 1)
            strACC = Mid(strCODE, InStr(strCODE, ",") + 1)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Sub





    Public Function GetDPassword(ByVal strPassword As String) As String
        GetDPassword = strPassword
        Return GetDPassword

        'Dim intChar As Integer
        'GetDPassword = String.Empty
        'a = 5
        'b = 2

        'For intChar = 1 To Len(Trim(strPassword))
        '    a = a + b
        '    GetDPassword = GetDPassword + Chr(Asc(Mid(strPassword, intChar, 1)) - a)
        'Next intChar
        'Return GetDPassword
    End Function
    Public Enum enmTR_Type
        Sale = 1
        Purchase = 2
        Sale_Purchase = 3
    End Enum


    Public Enum enmUserRights_Name
        JV_SUPERVISSION = 801
        JV_SUPERVISSION_L2 = 802
        '---------------------------------------
        CP_POSTING = 805
        CP_SUPERVISSION = 806
        CR_POSTING = 807
        CR_SUPERVISSION = 808
        '--------------------------------------
        'pagename (all spacec "_") = MenuID

        FINANCIAL_ACCOUNTS = 1000
        CHART_OF_ACCOUNTS = 1001
        JOURNAL_VOUCHER = 1002
        J_V_Supervission = 1016

        CURRENCY_REVALUATION_RATE_POSTING = 3044
        BANK_TRANSACTION = 1003
        BANK_PAYMENT = 1004
        CASH_TRANSACTION = 1005
        CASH_PAYMENT_ACC = 1006
        FD_CASH_PAYMENT = 2004
        BANK_PAYMENT_POSTING = 1007
        BANK_RECEIPT = 1008
        BANK_RECEIPT_POSTING = 1009

        CASH_PAYMENT_POSTING_ACC = 1010
        CASH_PAYMENT_SUPERVISSION_ACC = 1011

        DAY_END_ACC_SUPERVISION = 1053
        DAY_END_ACC_SUPERVISION_2 = 1057
        DAY_END_ACC_SUPERVISION_3_TBAL = 1058

        CASH_PAYMENT_POSTING_ACC_ByBR = 1049
        CASH_RECEIPT_ACC = 1012
        FD_CASH_RECEIPT = 2005
        CASH_RECEIPT_POSTING_ACC = 1013
        CASH_RECEIPT_POSTING_ACC_ByBR = 1051
        CASH_RECEIPT_SUPERVISSION_ACC = 1014
        JV_POSTING = 1015
        JV_SUPERVISSION_From = 1016
        JV_TRANSACTION_Form = 1017
        FRONT_DESK = 2000
        FCY_SALE_PURCHASE = 2001
        FCY__SALE_PUR_ON_ACCOUNT = 2002
        CASH_TRANSACTION_Form = 2003

        CASH_PAYMENT = 2004
        CASH_PAYMENT__POSTING = 2005
        CASH_PAYMENT__SUPERVISSION = 2006

        CASH_RECEIPT = 2008
        CASH_RECEIPT_POSTING = 2009
        CASH_RECEIPT_SUPERVISSION = 2010

        PDC_PAYMENT_ENTRY = 1028
        PDC_PAYMENT_POSTING = 1029
        PDC_PAYMENT_CLEARANCE = 1030
        PDC_RECEIPT_ENTRY = 1032
        PDC_RECEIPT_POSTING = 1033
        PDC_RECEIPT_CLEARANCE = 1034

        '------------------------------------
        '------------------------------------
        FWD_ALLOCATION_CANCEL_PURCHASE = 5015
        FWD_ALLOCATION_CANCEL_sale = 5013

        FORWARD_DEALS_REPORTS = 4086
        FORWARD_DEALS_REPORTS_FCY = 6072
        PARCEL_SHIPMENT_REPORTS = 4087
        REMITTANCE_OUTWARD = 2011
        TT_HOME_REMITTANCE = 2012
        TT_HMR_CASH_RECEIVED = 2013
        TT_HMR_SUPERVISSION = 2014
        TT_HRM_CANCELLATION = 2015
        TT_HRM_REFUND_APPROVAL = 2016
        TT_HRM_REFUND = 2017
        TT_HRM_COMPLIANCE = 2018
        TT_HRM_CANCELLATION_APPROVAL = 20117
        TT_HMR_CASH_RECEIVED_BOC = 20251
        TT_HMR_CASH_RECEIVED_sr = 20253

        TT_OUTWARD_REMITTANCE = 20230
        TT_OUTWARD_CASH_RECEIVED = 20231
        TT_OUTWARD_CHEQUE_PAYMENT = 20238
        TT_OUTWARD_SUPERVISSION = 20232
        TT_OUTWARD_CANCELLATION = 20233
        TT_OUTWARD_REFUND = 20235
        CADD_FILE_GENERATION = 20298
        TT_OUTWARD_CANCELLATION_APPROVAL = 20239
        TT_OUTWARD_REFUND_APPROVAL = 20234
        TT_OUTWARD_COMPLIANCE = 20236


        TT_DD_STATUS = 2027
        TT_OUTWARD_ON_ACCOUNT = 2086
        TT_OUTWARD_ON_ACCOUNT_SUPERVISSION = 2091
        TT_OUTWARD_ON_ACCOUNT_CANCELLATION = 2092
        TT_OUTWARD_ON_ACCOUNT_REFUND_APPROVAL = 2093
        TT_OUTWARD_ON_ACCOUNT_REFUND = 2094
        TT_OUTWARD_ON_ACCOUNT_COMPLIANCE = 2095



        CCY_TRANSFER = 2040
        TRANSFER_BTWEEN_TELLER = 2041
        ACCEPT_TRANSFER = 2042

        TRANSFER_BTWEEN_BR_FCY = 2044
        TRANSFER_BTWEEN_BR_FCY_SUPERVISSION = 2045

        TRANSFER_BTWEEN_BR_L_C = 2047
        TRANSFER_BTWEEN_BR_L_C_SUPERVISSION = 2048
        PARCEL_SHIPMENT = 2051
        PARCEL_PAYMENT = 2052
        PARCEL_PAYMENT_POSTING = 2053
        PARCEL_PAYMENT_SUPERVISSION = 2054
        PARCEL_RECEIPT = 2056
        PARCEL_RECEIPT_POSTING = 2057
        PARCEL_RECEIPT_SUPERVISSION = 2058
        REMITTANCE_INWARD_REPORT = 4039
        REMITTANCE_OUTWARD_REPORT = 4040
        REMITTANCE_INWARD = 2082
        REMITTANCE_INWARD_CASH_PAYMENT = 2083
        REMITTANCE_INWARD_SUPERVISSION = 2084
        REMITTANCE_INWARD_CANCELATION = 2085


        WEB_FROM_AGENT_APROVAL = 20104

        TT_HMR_CHECK_AMOUNT_CLEARNESS = 2100
        TT_OUTWARD_REMITTANCE_CHECK_AMOUNT_CLEARNESS = 2101


        REMITTANCE_APPLICATION = 20267
        REMITTANCE_APPLICATION_COMMERCIAL = 20305
        RO_Payment_LVL_1 = 20268
        RO_SEND_TO_HO_LVL_3 = 20269
        'RO_supervise_LVL_3 = 20270
        RO_Compliannce_LVL_2 = 20271
        RO_Modificztion_LVL_4 = 20272
        RO_Send_TO_Bank_L5 = 20273
        RO_COMPLIANCE_HO_ACCELERATE = 7099
        '----------------------------------------------

        MANAGMENT = 3000
        USER_CREATION_MENU = 3001
        USER_CREATION = 3002
        USER__RIGHTS_SETTING = 3003
        CUSTOMER__DEFINITION_MENU = 3004

        CUSTOMER__DEFINITION = 3005

        CUSTOMER__DEFINITION_REP = 3061

        BLOCK_CUSTOMER__BY_C_BANK = 3006
        CURRENCY_DEFINITION = 3007
        COMPUTER_CONFIGURATION_MENU = 3008
        BANK_DEFINITION = 3009
        BENEFICIARY_DEFINITION = 3010
        BANK_TRANS_ACC_TYPE = 3011
        CASHIER_ACC_MAPING = 3012
        PARTY_CURRENCY_SETTING = 3013
        PARTY_CURRENCY_RATE__SETTING = 3014
        REMITTANCE_ACCOUNT_MAPPING = 3015
        BANK_ACCOUNT_CODES = 3016
        PROMO_MESSAGE = 3017
        SALES_TAX_DEFINITION = 3036
        INC_TAX_DEFINITION = 3037
        PARTY_CURRENCY_SETTING_REPORT_BYPARTY = 4088
        PARTY_CURRENCY_RATE_SETTING_REPORT_BYPARTY = 4089
        BATCH_INV = 3047
        Item_Group = 3040
        ITEM_INV = 3039
        INV_QTY = 3041
        WEB_LOADER_FILE = 20110
        WebUser = 3043
        web_Payout = 3045
        TT_COMMISSION_ADJUSTMENT = 3018
        _CURRENCY_RATE = 3019
        MAC_ID = 3002
        MAC_IDs_AGENT = 3021
        LOG_OUT = 3022
        User_GROUP_CREATION = 3023
        PASSWORD_POLICY_LIMIT = 3024
        CHANGE_PASSWORD = 3025
        RESET_PASSWORD = 3026
        INQUIRY_PAGE = 3027
        SYSTEM_REPORTING = 4000
        FINANCIAL_REPORTING = 4001
        BACK_OFFICE_REPORTING = 4002
        MANAGEMENT_SUMMARY_REPORTING = 4003
        SYSTEM_DEFINITION_REPORTING = 4004
        SYSTEM_USER_LISTINGS = 4005
        PARTY_CURRENCY_SETTING__REPORT = 4006


        PARTY_CURRENCY_RATE__SETTING__REPORT = 4007
        ACCOUNT_CODE_REPORT = 4008
        BANK_TRANS_ACC_TYPE_REPORT = 4009
        CASHIER_ACC_MAPING_REPORT = 4010
        REMITTANCE__ACCOUNT_MAPPING_REPORT = 4011
        TT__COMMISSION_ADJUSTMENT_REPORT = 3018
        PROMO_MESSAGE_REPORT = 4013
        USER_GROUP_RIGHTS__LISTINGS = 4014
        CUSTOMER_DEFINITION = 4015
        SYSTEM_USER_LISTING_DETAIL = 4016
        USER_PASSWORD_LOG = 4017
        ALL_USER_GROUP_RIGHTS_LISTINGS = 4018
        CUSTOMER_LISTING_DETAIL = 4019
        FRONT_DESK_REPORTING = 4020
        CURRENCY_REPORTING = 4021
        CENTRAL_BANK_REPORTING = 4022
        APPLICATION_LOG_REPORTING = 4023
        DUPLICATE__COPY = 4024
        DUPLICATE__COPY_RO_WITH_RISK = 4024
        VOUCHER_REPORT = 4025

        Inventory_Listing_Report = 4094
        FINANCIAL_STATEMENTS = 4026
        FCY_LEDGERS = 4027
        PARTY_ACCOUNTS_FCY = 4028
        PARTY_ACCOUNTS__FCY_II = 4029
        PARTY_ACCOUNTS_FCY_III = 4030
        TRIAL_BALANCE = 4031
        TRIAL_BALANCE__Date_Range = 4032
        TRIAL_BALANCE_FCY = 6002
        TRIAL_BALANCE_FCY_COORPARATE = 4589
        TRIAL_BALANCE_ACCOUNT_TYPE = 20463

        P_L_STATEMENT = 4033
        BALANCE_SHEET = 4034
        BALANCE_SHEET_DETAILED = 4035
        CHART_OF_ACCOUNTS_REPORT = 4036
        CASHIER_ACCOUNTS_FOR_FRONT_DESK = 4037
        FCY_EXCHANGE_OPERATIONS = 4038
        REMITTANCE_INWARD_RPT = 4039
        REMITTANCE_OUTWARD_RPT = 4040
        TALLER_TRANSFER_STOCK = 4041

        SALE_PURCHASE_ANALYSIS = 4042
        SALE_PURCHASE_ANALYSIS_AGENT = 4142

        SALE_PURCHASE_ANALYSIS_FORMAT_II = 4043
        BANK_S_FORMAT_INTEGRATION = 4044
        CURRENCY_LISTINGS = 4045
        CURRENCY_STOCK = 4046
        CURRENCY_STOCK_FORMAT_II = 4047


        RATE_CARD = 4048
        TELLER_S_CASH_SHEET = 4049
        TELLER_S_CASH_SHEET_grouped = 4555

        CASH_INHAND_STATEMENT = 4050
        CASH_INHAND_STATEMENT_Report_DATERANGE = 20991
        REMITTANCE_OUTWARD_ANALYSIS = 4051
        REMITTANCE_OUTWARD_ANALYSIS_FORMAT_II = 4052
        REMITTANCE_INWARD_ANALYSIS = 4053
        REMITTANCE_INWARD_ANALYSIS_FORMAT_II = 4054
        RO_RI_ANALYSIS = 4055
        EXPENSES_SHEET_ANALYSIS = 4056
        CASH_PAYMENTS_DETAILS = 4057
        P_L_ANALYSIS = 4058
        P_L_ANALYSIS_FORMAT_II = 4059

        SALE_PURCHASE_REPORTS = 4061
        REMITTANCE_REPORTS_YEARLY = 4062
        CUSTOMER_INQUIRY_REPORTS = 4063

        CENTRAL_BANK_Remittance_Report_CountryWise = 4103

        SYSTEM_REMARKS_REPORT = 4125

        FORWARD_DEALS_FIX_UNFIX = 5000
        DEAL_ALLOCATION_SALE = 5001
        DEAL_ALLOCATION_PURCHASE = 5002
        FWD_SALE = 5003
        FWD__PURCAHSE = 5004
        FWD_SETTLEMENT = 5017
        FWD_SALEPUR = 5020

        FWD__SALE__POSTING = 5005
        FWD__PURCHASE__POSTING = 5006

        FWD__TT_SALE_POSTING = 5007
        FWD__TT_PURCHASE_POSTING = 5008
        FWD__TT_PURCHASE = 5009
        FWD__TT_SALE = 5010
        SYSTEM_REMARKS = 20265

        FORWARD_DEALS = 5012
        MAIN_ACCOUNTS = 1019
        DETAIL_ACCOUNTS = 1020
        SUB_ACCOUNTS = 1021
        D_R_NOTES = 1022
        RO_INQUIRY = 2060
        RO_CANCEL_APPROVE = 2061
        RO_REFUND_APPROVAL = 2062
        RO_STOP_PAYMENT_APPROVAL = 2063
        SEND_TO_COMPLIANCE = 2064
        BLOCK_BENEFICIARY = 3029
        BLOCK_CUSTOMER = 3027
        FAX_INWARD = 1026
        BRANCH_DEFINITION_MENU = 3030
        CLIENT_LOCAL_BANK = 3032
        RECEIVE_TRANSFER = 2088
        CURRENCY_TRANSFER_REQUEST = 1023
        CASH_INHAND_STATEMENT_Report = 4050
        CASH_INHAND_STATEMENT_DATERANGE = 20991
        REMITTANCE_OUTWARD_ANALYSIS_report = 4051
        REMITTANCE_OUTWARD_ANALYSIS_FORMAT_II_report = 4052
        REMITTANCE_INWARD_ANALYSIS_report = 4053

        REMITTANCE_INWARD_ANALYSIS_FORMAT_II_Report = 4054
        RO_RI_ANALYSIS_Report = 4055
        EXPENSES_SHEET_ANALYSIS_Report = 4056
        CASH_PAYMENTS_DETAILS_Report = 4057

        P_L_ANALYSIS_Report = 4058
        P_L_ANALYSIS_FORMAT_II_Report = 4059
        BUSINESS_SUMMARY_REPORT = 4060
        ITEM_INV_PRICE = 3042
        INVENTORY_FROM = 3046
        SALE = 5018
        TRANSFER_RECEIVE_POSTING = 2089
        REQUEST_TRANSFER_CLEARANCE = 2090
        CURRENCY_REVALUATION_RATE_ADJUSTMENT = 3034

        RO_ON_ACCOUNT_INQUIRY = 2096
        RO_ON_ACCOUNT_CANCEL_APPROVE = 2097
        RO_ON_ACCOUNT_STOP_PAYMENT_APPROVAL = 2098
        RO_ON_ACCOUNT_SEND_TO_COMPLIANCE = 2099
        WEB_APROVAL_AGENT = 20107
        WEB_POSTING_AGENT = 20108
        WEB_COMPLIANCE_AGENT = 20109
        GRG_CAR_MAKE = 3053
        GRG_JOB_Type = 3054
        GRG_JOB_COST = 3055
        GRG_INSURANCE_COMP = 3056
        GRG_MASTER_DETAIL_FORM = 3057
        AGENT_WEB_REPORTING = 4092
        BANK_BRANCH_DEFINITION = 3031
        WPS_AUTHORIZATION = 20222


        WPS_AUTHORIZATION_LIMIT_ACCESSED = 20223
        WPS_PAYMENT = 20225
        WPS_INQUIRY = 20226

        Teller_Cash_Position = 20228
        REVALUEATION_SHEET = 6001
        CENTRAL_BANK_REMITTANCE_REPORT = 4100
        CENTRAL_BANK_FORM_3 = 4101
        CENTRAL_BANK_FORM_3_FC_LC = 4102

        JV_INQUIRY = 1035
        TELLER_CASH = 20247
        TELLER_CASH_NW = 20248
        GLOBAL_CASH_POSITION = 20249
        TELLER_CASH_REPORT = 4104
        PURPOSE_DEFINITION = 3059
        CENTRAL_BANK_INWARD_REMITTANCE_REPORT = 4105
        CENTRAL_BANK_OUTWARD_REMITTANCE_REPORT = 4106
        SALE_PURCHASE_ANALYSIS_FORMAT_III = 4108
        SALE_PURCHASE_ANALYSIS_FORMAT_IIII = 4109
        SALE_PURCHASE_CASH_FOLLOW = 4110
        REMITTANCE_OUTWARD_WEB = 4112
        API_DETAILS = 3060
        TT_APPLICATION_INPUT = 4124
        '***************************
        '---SMS---------------------
        DAY_END_ALERT = 6006
        CASH_POSITION_ALERT = 6007
        BANK_POSITION_ALERT = 6008
        EXPENSE_SUMMARY_ALERT = 6009
        PROFIT_AND_LOSS_ALERT = 6010

        '***************************
        '---OLAP---------------------
        TELLER_STOCK_REPORT_olap = 4117
        PARTY_LEGDER_FCY_SUMMARY_olap = 4118
        PROFIT_AND_LOSS_olap = 4119
        SALE_PURCHASE_ANALYSIS_olap = 4120
        CCY_ASSOCIATION_olap = 4121
        PARTY_CURRENCY_RATE_SETTING_olap = 4122


        TR_Threshold_Amount = 3064
        TR_Threshold_Volume = 3065
        SYSTEM_ALERT_Management = 3068
        SYSTEM_ALERT_Assignment = 3069

        '***************************
        PAYMENT_BYBR = 1048
        PAYMENT_POSTING_BYBR = 1049
        RECEIPT_BYBR = 1050
        RECEIPT_BYBR_POSTING = 1051
        '***************************
        CASHIER_ACCOUNTS_FOR_FRONT_DESK_branch = 3072
        CHART_OF_ACCOUNTS_BRANCH = 4123


        CURRENCY_EXPOSURE = 4127
        CURRENCY_EXPOSURE_ANX_I = 4128
        CURRENCY_EXPOSURE_RPT = 20950
        CURRENCY_EXPOSURE_SUMMARY = 20951
        DAY_END_ANALYSIS = 6013

        COMPLIANCE_RISK_PROFILE_BY_COUNTRY = 3076
        COMPLIANCE_Risk_By_Sender_Value = 3077
        COMPLIANCE_Risk_By_Sender_Volume = 3078
        COMPLIANCE_Risk_By_Receiver_Value = 3079
        COMPLIANCE_Risk_By_Receiver_Volume = 3080
        COMPLIANCE_Risk_By_Receiver_Purpose = 3081
        COMPLIANCE_SCORE_SETTING = 3082
        COMPLIANCE_Exceptional = 3085


        eis_SALE_PURCHASE_ANALYSIS = 4117
        eis_TELLER_STOCK_REPORT = 4118
        eis_CCY_ASSOCIATION = 4119
        eis_DAY_END_ANALYSIS = 4120
        eis_CORRESPONDENT_CURRENCY_RATE_SETTING = 4121
        eis_CORRESPONDENT_BALANCE = 4122
        eis_PROFIT_LOSS_STATEMENT = 4131
        eis_REMITTANCE_OUTWARD = 4132
        eis_FWD_BALANCE_ANALYSIS = 4133


        USER_LOG_STATUS = 4107
        TT_APPLICATION_STATUS = 6004
        TT_NO = 6005
        CUSTOMER_DEFINITION_UPDATE = 3084

        CHART_OF_ACCOUNTS_CR_LIMIT = 6014

        REVALUATION_SHEET_FORMAT_II = 6015
        REVALUATION_SHEET_BY_CURRENCY = 6016
        RO_Payment_LVL_cancel = 20291
        REMITTANCE_OUTWARD_RPT_AML = 6030
        REVELATION_ANYLYSIS = 6033
        COMPLIANCE_Exceptional_whck = 3085
        RO_COMPLIANCE_HO = 20292
        COMPLIANCE_WORLD_CHECK_EXCEPTIONAL_WORDS_REPORT = 6035
        RO_Send_TO_Bank_L5_home = 20293
        COMPLIANCE_STATUS_CLEAR = 3086
        REMITTANCE_INWARD_SANCTION_SCREENING = 20294

        rate_card_new = 3086
        FCY_CANCELATION = 20294
        CENTRAL_BANK_OUTWARD_REMITTANCE_REPORT_QUTARLY = 6040
        CENTRAL_BANK_INWARD_REMITTANCE_REPORT_QUTARLY = 6041
        CURRENCY_STOCK_COST_RATE = 20392
        CURRENCY_STOCK_COST_RATE_FIFO = 20465
        CURRENCY_STOCK_COST_RATE_CONSOLIDATED = 6071

        WPS_PENDINGRECEIPTS = 4189
        CUSTOMER_CAD_FILE_PENDING = 6045
        CUSTOMER_ACCOUNT_MAPPING = 3090
        RO_Payment_LVL_1_Mapped = 20211

        RO_TOKEN = 20304
        FCY_BR_TRANSFER = 20509

        BENIFICIARY_LISTING = 4209
        REPRESENTATIVE_LISTING = 4210
        TRANSCATION_LOG = 7014
        FCY_TRANSCATION_LOG = 7029
        FRGS_REPORT = 7030
        FCY_COMPLIANCE_DECLINED_LOG = 20467
        FCY_HIT_LOG = 20500
        CASH_FLOW_DAY_END = 20468
        RULE_LOG_COMPLIANCE_CUST = 7015
        RULE_LOG_COMPLIANCE_RO = 7016


        DOCUMENT_VIEW = 3285
        DOCUMENT_TYPE = 3284
        DOCUMENT_UPLOAD = 3282

        Customer_Doc_Listing_Report = 4155

        AML_FRAUD_RULES = 3085
        AML_FRAUD_RULES_RO = 3108
        AML_FRAUD_RULES_FCY = 3107
        AML_FRAUD_RULES_RI = 3106


        RO_FRAUD_RULES_REPORT = 4192
        RI_FRAUD_RULES_REPORT = 4189
        FCY_FRAUD_RULES_REPORT = 4190
        CP_FRAUD_RULES_REPORT = 4191

        FCY_AML_PROFILE = 7021
        CURRENCY_SALE_STATMENT = 20393

        TB_ON_SCREEN = 20458
        PL_STATEMENT_ON_SCREEN = 20459
        FS_ON_SCREEN = 20457

        Multiple_beneficiaries_one_to_many = 20601
        Multiple_remitters_many_to_one = 20602
        High_value_single_transaction_Individual = 20603
        High_value_single_transaction_Corporate = 20604
        High_value_transactions_cumulative_Individual = 20605
        High_value_transactions_cumulative_Corporate = 20606
        Transaction_count_Individual = 20607
        Transaction_count_Corporate = 20608
        Send_to_high_risk_country = 20609
        Receive_from_high_risk_country = 20610
        High_risk_currency = 20611
        Nationality = 20612
        Transaction_count_pattern = 20613
        Multiple_branch_transactions = 20614
        High_risk_customers_manual_risk = 20615
        High_risk_customers_AML_risk = 20616
        Splitting_of_transactions = 20617
        In_and_out_transactions = 20618
        Customer_mobile_number_combination = 20619
        TC_encashment_Individual = 20620
        Currency_exchange = 20621
        Cash_payments_by_Corporate = 20622
        Multiple_beneficiaries_receive_transactions = 20623
        Nationality_country_combination_Send_Transaction = 20624
        Nationality_country_combination_Receive_Transaction = 20625
        Nationality_nationality_combination_Send_Transaction = 20626
        Nationality_nationality_combination_Receive_Transaction = 20627
        Multiple_country_Send = 20628
        Multiple_nationality_Send = 20629
        Multiple_country_Receive = 20630
        Multiple_nationality_Receive = 20631
        Beneficiary_name_suggests = 20632
        Watch_List_customer = 20633
        Employee_threshold = 20634
        Indian_bank_remittance_more_than_threshold = 20635


        AIRPORT_DEFINITION = 20501
        CITY_DEFINITION = 20500
    End Enum







    Public Enum enmCOA_ALFA

        '------ALFA - COA ----------------
        dtcd_Rounding_Acc = 1905
        sbcd_Rounding_ACC = 3
        dtcd_UNPAID_CASH = 1905
        sbcd_UNPAID_CASH = 1

        dtcd_CASH_IN_TRANSIT = 1905
        sbcd_CASH_IN_TRANSIT = 2

        dtcd_RO_COLLECTION = 1904
        sbcd_RO_COLLECTION = 1
        dtcd_WEB_TT_COLLECTION = 1904
        sbcd_WEB_TT_COLLECTION = 2

        dtcd_CHQ_PAYMENT = 1903
        sbcd_CHQ_PAYMENT = 2
        dtcd_CHQ_RECEIPT = 1903
        sbcd_CHQ_RECEIPT = 1

        dtcd_Customer_Acc = 6100
        sbcd_Customer_Acc_9999 = 9999


        dtcd_CASH = 1100

        mn_CASH = 1000
        mn_BANKS_INSIDE_UAE = 1200
        mn_BANKS_OUTSIDE_UAE = 1300
        mn_EXCHANGE_HOUSE_INSIDE_UAE = 1400
        mn_INSTANT_MONEY_TRANSFER_IN_UAE = 1500
        mn_EXCHANGE_HOUSE_OUTSIDE_UAE = 1600
        mn_AGENTS_OUTSIDE_UAE_FINANCE_INSTITUTION = 1700
        mn_branches = 22000
        dtcd_CTRL_ACC_BR = 22200
        sbcd_CTRL_HO = 1
        sbcd_CTRL_MN_BR = 2

        dtcd_TT_EXCHANGE_GAIN_LOSS_FWD = 4005
        sbcd_TT_EXCHANGE_GAIN_LOSS_FWD = 1

        dtcd_TT_EXCHANGE_GAIN_LOSS_REV = 4005
        sbcd_TT_EXCHANGE_GAIN_LOSS_REV = 2

        '04005 - 0002 TT - EXCHANGE GAIN/LOSS
        dtcd_TT_Commission_Acc = 4002
        sbcd_TT_Commission_Acc = 1


        dtcd_FX_Transaction_FEE = 4004
        sbcd_FX_Transaction_FEE = 3

        dtcd_Rem_Payable = 6300
        sbcd_Rem_Payable = 1
        dtcd_Rem_OPR = 6300
        sbcd_Rem_OPR = 2

        dtcd_Rem_Payable_new = 6000
        DTCD_REBATES_BANK_REMIT = 4003
        SBCD_REBATES_BANK_REMIT = 1




        DTCD_FWD_CONTRA_INCOME = 4005
        SBCD_FWD_CONTRA_INCOME = 100

        mn_IG_MARKET = 1700
        DTCD_IG_MARKET_DIFFERENCE = 1701
        SBCD_IG_MARKET_DIFFERENCE = 100


        mn_income = 4000
        mn_expense = 5000

        dtcd_WU_USD = 1501
        sbcd_WU_USD = 2

        dtcd_WU_AED = 1501
        sbcd_WU_AED = 1

        dtcd_HBL_DUBAI_USD_AC = 1202
        sbcd_HBL_DUBAI_USD_AC = 2
        dtcd_Remittance_Acc = 6300
        sbcd_Remittance_Acc = 1
        dtcd_vat_account = 29100
        sbcd_vat_account = 1


        dtcd_Transfast_account = 1405
        sbcd_Transfast_account = 1

        dtcd_TT_Commission_Acc_rev = 4006
        sbcd_TT_Commission_Acc_rev = 1

    End Enum

    Public Enum enm_CCIDs
        ' ''HeadOffice = 1
        ' ''Main_Branh = 12

        CORPORATE_OFFICE_CO = 1
        AWAIS_TOWER_DUBAI_BRANCH_DR = 2
        J_B_ROUNDABOUT_SHARJAH_BRANCH_SH = 3
        MUSSAFA_ABU_DHABI_BRANCH_AD = 4

    End Enum


    Public Enum enm_Bank_Identifier
        SWIFT = 1
        Clearing_Code = 2
        OTHER = 3
        IFSC = 4
        PAK_BANKS = 5

    End Enum
    Public Enum enmUserRights_Cols

        UserGroupID = 0
        PageID = 1
        vbComponentName = 2
        bSave = 3
        bUpdate = 4
        bFind = 5
        bView = 6
        bPrint = 7

        bPendingForCSH = 8
        bPendingForSUP = 9
        bPendingForACC = 10
    End Enum
    Public Sub Get_Custid_iamge(ByRef n_Cust_imageID As String, ByVal nCust_ID As String)
        Dim dtTb As New DataTable
        n_Cust_imageID = 0
        Try
            setCon_IMAGE(True)
            strSql = "SELECT top 1  id_GUID  AS id_GUID   from tbl_cust_img  where Cust_id_GUID = '" & nCust_ID & "' order by mdate desc"
            dtTb = DTTB_Fill(strSql, gCon_IMAGE)
            Dim I As Integer = 0
            For I = 0 To dtTb.Rows.Count - 1
                n_Cust_imageID = dtTb.Rows(I).Item("id_GUID").ToString
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing
        End Try
    End Sub
    Public Sub Get_Custid_iamge_Mast(ByRef n_Cust_imageID As String, ByVal nCust_ID As String)
        Dim dtTb As New DataTable
        n_Cust_imageID = 0
        Try
            setCon_IMAGE(True)
            strSql = "SELECT top 1  id_GUID  AS id_GUID   from tbl_cust_img  where Cust_id_GUID_Master = '" & nCust_ID & "' order by mdate desc"
            dtTb = DTTB_Fill(strSql, gCon_IMAGE)
            Dim I As Integer = 0
            For I = 0 To dtTb.Rows.Count - 1
                n_Cust_imageID = dtTb.Rows(I).Item("id_GUID").ToString
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing
        End Try
    End Sub





    Public Function getUserRight(ByVal objUserRights_Cols As enmUserRights_Cols, _
                           ByVal objUserRights_Name As enmUserRights_Name, _
                            Optional ByVal recID As String = "") As Boolean
        Dim recCount As Int16
        Dim strPriority, strRecordType As String
        Dim blnFound_Right As Boolean
        Dim objSql As New clsSQL
        strRecordType = String.Empty
        strPriority = String.Empty
        'Dim moComm As New SqlClient.SqlCommand

        Try
            'setCon(True)

            'moComm.Connection = gobjCon
            Select Case objUserRights_Cols
                Case enmUserRights_Cols.bFind
                    strPriority = "2"
                    strRecordType = "Find"
                Case enmUserRights_Cols.bPrint
                    strPriority = "1"
                    strRecordType = "Print"
                Case enmUserRights_Cols.bSave
                    strPriority = "2"
                    strRecordType = "Save"
                Case enmUserRights_Cols.bView
                    strPriority = "4"
                    strRecordType = "Access"
                Case enmUserRights_Cols.bPendingForSUP
                    strPriority = "1"
                    strRecordType = "Supervise"
                Case enmUserRights_Cols.bPendingForACC
                    strPriority = "1"
                    strRecordType = "Pend ACC"
                Case enmUserRights_Cols.bPendingForCSH
                    strPriority = "1"
                    strRecordType = "Pend CSH"
                Case enmUserRights_Cols.bUpdate
                    strPriority = "1"
                    strRecordType = "Update"
            End Select

            For recCount = 0 To gobjUserRightsTable.Rows.Count - 1
                If Trim(gobjUserRightsTable.Rows(recCount)(enmUserRights_Cols.PageID)) _
                          = CDbl(objUserRights_Name) Then
                    blnFound_Right = True


                    If Not gobjUserRightsTable.Rows(recCount)(objUserRights_Cols) Then
                        MsgBox("System received and Unauthorized attempt, Please contact to system administrator, illegal log generated ....", MsgBoxStyle.Information)
                        getUserRight = False
                        InsertLOG(strRecordType, recID, gobjUserRightsTable.Rows(recCount)(enmUserRights_Cols.vbComponentName), strPriority, False)
                        Exit For
                    Else
                        'If objUserRights_Cols = enmUserRights_Cols.bView Then
                        InsertLOG(strRecordType, recID, gobjUserRightsTable.Rows(recCount)(enmUserRights_Cols.vbComponentName), strPriority, True)
                        'End If
                        getUserRight = True
                        Exit For
                    End If
                End If
            Next
            If Not blnFound_Right Then
                MsgBox("System received and Unauthorized attempt, Please contact to system administrator, illegal log generated ....", MsgBoxStyle.Information)
                getUserRight = False
                InsertLOG(strRecordType, "", arrUserRightsName(objUserRights_Name), strPriority, False)
            End If
        Catch ex As Exception

        End Try
        ''getUserRight = True

    End Function


    Public Sub InsertLOG(ByVal strRecordType As String, _
              ByVal strRecordID As String, _
              ByVal strComponentName As String, _
              ByVal strLogPriority As String, ByVal isLOG As Boolean)
        Dim loComm As New SqlClient.SqlCommand
        loComm.Connection = gCon
        setServerDate()
        Try
            setCon(True)
            loComm.CommandType = CommandType.StoredProcedure
            loComm.CommandText = "stp_tblLog_Insert"

            With loComm.Parameters
                '.Add(New SqlParameter("@dalogDate", SqlDbType.DateTime, 8, ParameterDirection.Input, False, 23, 3, "", DataRowVersion.Proposed, gServerDATE))   'DateSerial_SFTX(gServerDATE.Year, gServerDATE.Month, gServerDATE.Day)))

                .Add(New SqlParameter("@dalogDate", SqlDbType.DateTime, 8, ParameterDirection.Input, False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(gServerDATE.Year, gServerDATE.Month, gServerDATE.Day)))
                .Add(New SqlParameter("@sRecordType", SqlDbType.VarChar, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, strRecordType))

                If Len(Trim(strRecordID)) = 0 Then
                    .Add(New SqlParameter("@sRecordID", SqlDbType.VarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, System.DBNull.Value))
                Else
                    .Add(New SqlParameter("@sRecordID", SqlDbType.VarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, strRecordID))
                End If
                strComponentName = "qamar"
                gUserNAME = 1
                .Add(New SqlParameter("@sComponentName", SqlDbType.VarChar, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, strComponentName))
                .Add(New SqlParameter("@sLogPriority", SqlDbType.VarChar, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, strLogPriority))
                .Add(New SqlParameter("@sUserID", SqlDbType.VarChar, 15, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, gUserNAME))
                .Add(New SqlParameter("@bisLOG", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, isLOG))
            End With

            loComm.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            loComm.Dispose()
            loComm = Nothing
            setCon(False)
        End Try
    End Sub

    Public Sub mSave_LOG(ByVal str_RecordID As String, _
                            ByVal obj_eLG_TYP As eLG_TYP, _
                            ByVal str_ComponentName As String, _
                            ByVal obj_eLG_Prt As eLG_Prt, _
                            ByVal str_UserID_Gid As String, _
                            ByVal nCCID As Integer, _
                            Optional ByVal isLOG As Boolean = False)

        Dim loComm As New SqlClient.SqlCommand
        loComm.Connection = gCon

        Try
            setCon(True)
            loComm.CommandType = CommandType.StoredProcedure
            loComm.CommandText = "TBL_LOG_Insert"
            With loComm.Parameters

                .Add(New SqlParameter("@RecordID", SqlDbType.VarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_RecordID))

                .Add(New SqlParameter("@REC_TYPE_ID", SqlDbType.Int, 8, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, obj_eLG_TYP))
                .Add(New SqlParameter("@ComponentName", SqlDbType.VarChar, 200, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_ComponentName))
                .Add(New SqlParameter("@LOG_PRIORITY_ID", SqlDbType.Int, 8, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, obj_eLG_Prt))
                .Add(New SqlParameter("@UserID_Gid", SqlDbType.VarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, str_UserID_Gid))
                .Add(New SqlParameter("@isLOG", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, isLOG))
                .Add(New SqlParameter("@CCID", SqlDbType.Int, 8, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, nCCID))
            End With

            loComm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            loComm.Dispose()
            loComm = Nothing
            setCon(False)
        End Try
    End Sub




    Public Sub setServerDate()
        Dim loComm As New SqlClient.SqlCommand
        Dim loDataReader As SqlClient.SqlDataReader = Nothing

        Try
            setCon(True)
            loComm.Connection = gCon
            loComm.CommandType = CommandType.Text
            loComm.CommandText = "SELECT GetDate()"
            loDataReader = loComm.ExecuteReader
            If loDataReader.Read Then
                gServerDATE = loDataReader(0)
                gServerDate_Str = CStr(gServerDATE.Year) & "/" & CStr(gServerDATE.Month) & "/" & CStr(gServerDATE.Day)
            End If
        Catch ex As Exception

        Finally
            loDataReader.Close()
            loDataReader = Nothing
            loComm.Dispose()
            loComm = Nothing
        End Try


    End Sub

    Function GridDS(ByVal strSql As String) As DataSet
        Dim ds As New DataSet()
        Try
            Dim moComm As New SqlClient.SqlCommand
            Dim Con As New SqlConnection(gStrCnString)

            moComm.Connection = Con
            moComm.CommandText = strSql
            Dim dt As DataTable = New DataTable("dt")
            Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
            sdaAdapter.Fill(ds)

        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

        Return ds
    End Function



    Public Function DTTB_Fill(ByVal sTxt As String, Optional ByVal isSP As Boolean = False) As DataTable

        Dim loComm As New SqlClient.SqlCommand
        Dim sdaAdapter As SqlDataAdapter
        Dim dtTb As DataTable = New DataTable("tb")
        Try
            setCon(True)
            loComm.Connection = gCon
            sdaAdapter = New SqlDataAdapter(loComm)
            'If Not isSP Then
            '    loComm.CommandType = CommandType.Text
            'Else
            '    loComm.CommandType = CommandType.StoredProcedure
            'End If
            loComm.CommandType = CommandType.Text
            loComm.CommandText = sTxt
            sdaAdapter.Fill(dtTb)
            DTTB_Fill = dtTb
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            setCon(False)
            dtTb = Nothing
        End Try

    End Function


    Public Function DTTB_Fill_BySP(ByVal objDTTB_FILL_CALLS As DTTB_FILL_CALLS, _
                        Optional ByVal Para1 As Object = "", _
                        Optional ByVal Para2 As Object = "", _
                        Optional ByVal Para3 As Object = "", _
                        Optional ByVal Para4 As Object = "", _
                        Optional ByVal Para5 As Object = "", _
                        Optional ByVal Para6 As Object = "", _
                        Optional ByVal Para7 As Object = "", _
                         Optional ByVal Para8 As Object = "", _
                         Optional ByVal Para9 As Object = "", _
                         Optional ByVal Para10 As Object = "") As DataTable


        Dim loComm As New SqlClient.SqlCommand
        Dim sdaAdapter As SqlDataAdapter
        Dim dtTb As DataTable = New DataTable("tb")
        Select Case objDTTB_FILL_CALLS
            Case DTTB_FILL_CALLS.MainAccountcode
                Try

                    setCon(True)
                    loComm.Connection = gCon
                    sdaAdapter = New SqlDataAdapter(loComm)
                    loComm.CommandText = "stp_tblAccMain_SELECT"
                    loComm.CommandType = CommandType.StoredProcedure
                    loComm.Parameters.Add(New SqlParameter("@iMainCodeID", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Para1))

                    sdaAdapter.Fill(dtTb)
                    DTTB_Fill_BySP = dtTb
                    Exit Function


                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    setCon(False)

                End Try
            Case DTTB_FILL_CALLS.Detailcode
                Try

                    setCon(True)
                    loComm.Connection = gCon
                    sdaAdapter = New SqlDataAdapter(loComm)
                    loComm.CommandText = "stp_tblAccDetail_SELECT"
                    loComm.CommandType = CommandType.StoredProcedure
                    loComm.Parameters.Add(New SqlParameter("@iDetailCodeID", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Para1))

                    sdaAdapter.Fill(dtTb)
                    DTTB_Fill_BySP = dtTb
                    Exit Function


                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    setCon(False)

                End Try


            Case DTTB_FILL_CALLS.getccy
                Try

                    setCon(True)
                    loComm.Connection = gCon
                    sdaAdapter = New SqlDataAdapter(loComm)
                    loComm.CommandText = "[stp_get_PartyCCYASSO_2]"
                    loComm.CommandType = CommandType.StoredProcedure
                    loComm.Parameters.Add(New SqlParameter("@currencyID", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Para1))

                    sdaAdapter.Fill(dtTb)
                    DTTB_Fill_BySP = dtTb
                    Exit Function


                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    setCon(False)
                End Try
            Case DTTB_FILL_CALLS.getccyRate
                Try

                    setCon(True)
                    loComm.Connection = gCon
                    sdaAdapter = New SqlDataAdapter(loComm)
                    loComm.CommandText = "[stp_RPT_PartyCCY]"
                    loComm.CommandType = CommandType.StoredProcedure
                    loComm.Parameters.Add(New SqlParameter("@currencyID", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Para1))

                    sdaAdapter.Fill(dtTb)
                    DTTB_Fill_BySP = dtTb
                    Exit Function


                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    setCon(False)
                End Try

            Case DTTB_FILL_CALLS.subcode
                Try

                    setCon(True)
                    loComm.Connection = gCon
                    sdaAdapter = New SqlDataAdapter(loComm)
                    loComm.CommandText = "stp_tblAccSubCode_SELECT"
                    loComm.CommandType = CommandType.StoredProcedure
                    loComm.Parameters.Add(New SqlParameter("@iSubCodeID", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Para1))
                    loComm.Parameters.Add(New SqlParameter("@iDetailCodeID", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, Para2))

                    sdaAdapter.Fill(dtTb)
                    DTTB_Fill_BySP = dtTb
                    Exit Function


                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    setCon(False)

                End Try

                '-----------------------------------------------------------------  
                '-----------------------------------------------------------------
                '-----------------------------------------------------------------
            Case DTTB_FILL_CALLS.rptJV
                Try
                    setCon(True)
                    loComm.Connection = gCon
                    sdaAdapter = New SqlDataAdapter(loComm)
                    loComm.CommandText = "stp_Rpt_GLVch_JV"
                    loComm.CommandType = CommandType.StoredProcedure
                    loComm.Parameters.Add(New SqlParameter("@svrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Para1))
                    sdaAdapter.Fill(dtTb)
                    DTTB_Fill_BySP = dtTb
                    Exit Function
                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    setCon(False)

                End Try

                '-----------------------------------------------------------------  
                '-----------------------------------------------------------------
                '-----------------------------------------------------------------
            Case DTTB_FILL_CALLS.rptCP_CR
                Try
                    setCon(True)
                    loComm.Connection = gCon
                    sdaAdapter = New SqlDataAdapter(loComm)
                    loComm.CommandText = "stp_Rpt_GLVch_ds"
                    loComm.CommandType = CommandType.StoredProcedure
                    loComm.Parameters.Add(New SqlParameter("@svrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Para1))
                    sdaAdapter.Fill(dtTb)
                    DTTB_Fill_BySP = dtTb
                    Exit Function
                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    setCon(False)

                End Try
                '-----------------------------------------------------------------  
                '-----------------------------------------------------------------
                '-----------------------------------------------------------------

            Case DTTB_FILL_CALLS.API_BOC
                Try

                    setCon(True)
                    loComm.Connection = gCon
                    sdaAdapter = New SqlDataAdapter(loComm)
                    loComm.CommandText = "stp_API_BOC"
                    loComm.CommandType = CommandType.StoredProcedure
                    loComm.Parameters.Add(New SqlParameter("@sVrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Para1))

                    sdaAdapter.Fill(dtTb)
                    DTTB_Fill_BySP = dtTb
                    Exit Function


                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    setCon(False)

                End Try

            Case DTTB_FILL_CALLS.API_SR
                Try

                    setCon(True)
                    loComm.Connection = gCon
                    sdaAdapter = New SqlDataAdapter(loComm)
                    loComm.CommandText = "stp_API_SR"
                    loComm.CommandType = CommandType.StoredProcedure
                    loComm.Parameters.Add(New SqlParameter("@sVrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Para1))

                    sdaAdapter.Fill(dtTb)
                    DTTB_Fill_BySP = dtTb
                    Exit Function


                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    setCon(False)

                End Try


                '-----------------------------------------------------------------  

            Case DTTB_FILL_CALLS.API_SR_detail
                Try

                    setCon(True)
                    loComm.Connection = gCon
                    sdaAdapter = New SqlDataAdapter(loComm)
                    loComm.CommandText = "stp_API_SR_detail"
                    loComm.CommandType = CommandType.StoredProcedure
                    loComm.Parameters.Add(New SqlParameter("@API_DETAIL_BRANCH", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Para1))
                    loComm.Parameters.Add(New SqlParameter("@API_DETAIL_API_ID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Para2))

                    sdaAdapter.Fill(dtTb)
                    DTTB_Fill_BySP = dtTb
                    Exit Function


                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    setCon(False)

                End Try


                '----------------------------------------------------------------- 
                '-----------------------------------------------------------------
                '-----------------------------------------------------------------
        End Select
        DTTB_Fill_BySP = dtTb


    End Function
   
    Public Function DTTB_Fill_Generic(ByVal strSPName As String, _
                                        ByVal ParamArray PARA() As clsPara) As DataTable
        Dim i As Integer = 0

        Dim loComm As New SqlClient.SqlCommand
        Dim sdaAdapter As SqlDataAdapter
        Dim dtTb As DataTable = New DataTable("tb")

        Try
            setCon(True)
            loComm.Connection = gCon
            sdaAdapter = New SqlDataAdapter(loComm)
            loComm.CommandText = strSPName
            loComm.CommandType = CommandType.StoredProcedure
            For i = 0 To PARA.Length - 1
                loComm.Parameters.Add(fun_GetPara(PARA(i)))
            Next
            sdaAdapter.Fill(dtTb)
            DTTB_Fill_Generic = dtTb
            Exit Function

        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            setCon(False)
            sdaAdapter = Nothing
            loComm = Nothing
            dtTb = Nothing
        End Try


        DTTB_Fill_Generic = dtTb


    End Function





    Public Sub Str_Remove_Special_Char(ByRef str_DATA As String)
        Dim mARR() As String
        Dim s_ARR_2 As String = ""
        Try
            str_DATA = UCase(str_DATA)

            str_DATA = Replace(str_DATA, ".", " ")
            str_DATA = Replace(str_DATA, ",", " ")

            str_DATA = Regex.Replace(str_DATA, "0", "*")
            str_DATA = Regex.Replace(str_DATA, "1", "*")
            str_DATA = Regex.Replace(str_DATA, "2", "*")
            str_DATA = Regex.Replace(str_DATA, "3", "*")
            str_DATA = Regex.Replace(str_DATA, "4", "*")
            str_DATA = Regex.Replace(str_DATA, "5", "*")
            str_DATA = Regex.Replace(str_DATA, "6", "*")
            str_DATA = Regex.Replace(str_DATA, "7", "*")
            str_DATA = Regex.Replace(str_DATA, "8", "*")
            str_DATA = Regex.Replace(str_DATA, "9", "*")

            mARR = Split(str_DATA, " ")
            s_ARR_2 = ""
            For Each s_ARR As String In mARR
                If InStr(s_ARR, "*") = 0 Then
                    s_ARR_2 = s_ARR_2 + " " + s_ARR
                End If
            Next


            '---- NEW CODE *********************
            str_DATA = s_ARR_2
            str_DATA = Regex.Replace(str_DATA, "  ", " ")
            str_DATA = Regex.Replace(str_DATA, "   ", "")
            str_DATA = Regex.Replace(str_DATA, "    ", "")
            str_DATA = Regex.Replace(str_DATA, "     ", "")
            str_DATA = RTrim(LTrim(str_DATA))

            mARR = Split(str_DATA, " ")
            s_ARR_2 = ""
            For Each s_ARR As String In mARR

                If s_ARR = "AND" Then GoTo Skip_L_2
                If s_ARR = "ARE" Then GoTo Skip_L_2
                If s_ARR = "ALL" Then GoTo Skip_L_2
                If s_ARR = "NO" Then GoTo Skip_L_2
                If s_ARR = "DT" Then GoTo Skip_L_2
                If s_ARR = "DTD" Then GoTo Skip_L_2
                If s_ARR = "AGST" Then GoTo Skip_L_2
                If s_ARR = "AGNST" Then GoTo Skip_L_2
                If s_ARR = "DATE" Then GoTo Skip_L_2
                If s_ARR = "OF" Then GoTo Skip_L_2
                If s_ARR = "AN" Then GoTo Skip_L_2
                If s_ARR = "AS" Then GoTo Skip_L_2
                If s_ARR = "INV" Then GoTo Skip_L_2
                If s_ARR = "FOR" Then GoTo Skip_L_2
                If s_ARR = "IMP" Then GoTo Skip_L_2
                If s_ARR = "REF" Then GoTo Skip_L_2
                If s_ARR = "THE" Then GoTo Skip_L_2
                If s_ARR = "ON" Then GoTo Skip_L_2
                If s_ARR = "THAN" Then GoTo Skip_L_2
                If s_ARR = "PART" Then GoTo Skip_L_2
                If s_ARR = "IN" Then GoTo Skip_L_2
                If s_ARR = "IS" Then GoTo Skip_L_2
                If s_ARR = "BE" Then GoTo Skip_L_2
                If s_ARR = "TO" Then GoTo Skip_L_2
                If s_ARR = "BY" Then GoTo Skip_L_2
                If s_ARR = "IT" Then GoTo Skip_L_2
                If s_ARR = "AT" Then GoTo Skip_L_2
                If s_ARR = "ITEM" Then GoTo Skip_L_2

                s_ARR_2 = s_ARR_2 + " " + s_ARR
Skip_L_2:

            Next


            '-----------------------------------
            '-----------------------------------


            str_DATA = s_ARR_2

            str_DATA = Regex.Replace(str_DATA, "[^A-Za-z0-9\-/]", " ")

            'This statement will replace any character that is not a word, \ or -. For e.g. aa-b@c will become aa-bc.
            str_DATA = Regex.Replace(str_DATA, "[^\w\\-]", " ")

            'This statement will replace any numric


            str_DATA = Regex.Replace(str_DATA, " ONE ", "")
            str_DATA = Regex.Replace(str_DATA, " ONE", "")
            str_DATA = Regex.Replace(str_DATA, "ONE ", "")

            str_DATA = Regex.Replace(str_DATA, " TWO ", "")
            str_DATA = Regex.Replace(str_DATA, " TWO", "")
            str_DATA = Regex.Replace(str_DATA, "TWO ", "")

            str_DATA = Regex.Replace(str_DATA, " THREE ", "")
            str_DATA = Regex.Replace(str_DATA, " THREE", "")
            str_DATA = Regex.Replace(str_DATA, "THREE ", "")

            str_DATA = Regex.Replace(str_DATA, " FOUR ", "")
            str_DATA = Regex.Replace(str_DATA, " FOUR", "")
            str_DATA = Regex.Replace(str_DATA, "FOUR ", "")

            str_DATA = Regex.Replace(str_DATA, " FIVE ", "")
            str_DATA = Regex.Replace(str_DATA, " FIVE", "")
            str_DATA = Regex.Replace(str_DATA, "FIVE ", "")

            str_DATA = Regex.Replace(str_DATA, " SIX ", "")
            str_DATA = Regex.Replace(str_DATA, " SIX", "")
            str_DATA = Regex.Replace(str_DATA, "SIX ", "")

            str_DATA = Regex.Replace(str_DATA, " SEVEN ", "")
            str_DATA = Regex.Replace(str_DATA, " SEVEN", "")
            str_DATA = Regex.Replace(str_DATA, "SEVEN ", "")
            str_DATA = Regex.Replace(str_DATA, " EIGHT ", "")
            str_DATA = Regex.Replace(str_DATA, " EIGHT", "")
            str_DATA = Regex.Replace(str_DATA, "EIGHT ", "")

            str_DATA = Regex.Replace(str_DATA, " NINE ", "")
            str_DATA = Regex.Replace(str_DATA, " NINE", "")
            str_DATA = Regex.Replace(str_DATA, "NINE ", "")

            str_DATA = Regex.Replace(str_DATA, " TEN ", "")
            str_DATA = Regex.Replace(str_DATA, " TEN", "")
            str_DATA = Regex.Replace(str_DATA, "TEN ", "")

            '----------------------------------------------------------

            str_DATA = Regex.Replace(str_DATA, "  ", " ")
            str_DATA = Regex.Replace(str_DATA, "   ", "")
            str_DATA = Regex.Replace(str_DATA, "    ", "")
            str_DATA = Regex.Replace(str_DATA, "     ", "")
            str_DATA = RTrim(LTrim(str_DATA))


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub



    'Public Sub mCalculate_Risk_GEN(ByVal obj_Risk_Cal As enm_Risk_Cal, ByVal risk_parameter As String, ByVal Risk_dttb As DataTable, ByRef risk As Double, ByRef risk_string As String, ByVal risk_parameter_2 As String, _
    '                             ByRef str_Result As String, Optional ByVal aml_product As Integer = enm_RISK_PRODUCTS._4_Customer_Profiling)



    '    Dim strTmp As String = ""
    '    Dim strTMP_2 As String = ""
    '    Dim n_Risk_Score As Double = 0

    '    Dim Dow_Jon_Detail As String = ""
    '    Try

    '        Select Case obj_Risk_Cal
    '            Case enm_Risk_Cal._1_NAME_CHECK


    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)
    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '            Case enm_Risk_Cal._3_NATIONALITY


    '                strTmp = "CountryID = " & risk_parameter & " AND AML_PRODUCT_ID=  " & aml_product & " "
    '                get_AML_RISK_SCROE_BY_TEXT_GENERAL(strTmp, Risk_dttb, n_Risk_Score)
    '                risk = n_Risk_Score
    '                risk_string = risk & "," & risk_parameter_2



    '            Case enm_Risk_Cal._4_ADDRESS

    '                Dim nTmp As Integer = 0
    '                ' If Not Len(Trim(risk_parameter)) = 0 Then
    '                strTMP_2 = UCase(risk_parameter)
    '                strTMP_2 = RTrim(LTrim(strTMP_2))
    '                Str_Remove_Special_Char(strTMP_2)
    '                strTmp = "ADD_WORD= '" & strTMP_2 & "'  AND isActive = 'TRUE'"
    '                get_AML_RISK_SCORE_BY_TEXT_GENERAL_FROM_DB_PARSE_TEXT(strTmp, n_Risk_Score, Risk_dttb)
    '                risk = n_Risk_Score

    '                risk_string = risk & "," & risk_parameter

    '                ' End If



    '            Case enm_Risk_Cal._5_Place_OF_Birth


    '                strTMP_2 = UCase(risk_parameter)
    '                strTMP_2 = RTrim(LTrim(strTMP_2))
    '                Str_Remove_Special_Char(strTMP_2)
    '                strTmp = "ADD_WORD= '" & strTMP_2 & "'  AND isActive = 'TRUE'"
    '                get_AML_RISK_SCORE_BY_TEXT_GENERAL_FROM_DB(strTmp, n_Risk_Score, Risk_dttb)
    '                risk = n_Risk_Score
    '                'risk_string = "Address:Details:" & risk_parameter
    '                risk_string = risk & "," & risk_parameter

    '            Case enm_Risk_Cal._6_Place_OF_issue
    '                'txt_6_Place_OF_issue.Text = 0
    '                strTmp = "CountryID = " & risk_parameter & " AND AML_PRODUCT_ID=  " & aml_product & " "
    '                get_AML_RISK_SCROE_BY_TEXT_GENERAL(strTmp, Risk_dttb, n_Risk_Score)
    '                risk = n_Risk_Score
    '                risk_string = risk & "," & risk_parameter_2

    '            Case enm_Risk_Cal._7_CUST_TYPE_CHILD

    '                strTmp = UCase(risk_parameter)
    '                strTmp = RTrim(LTrim(strTmp))
    '                Str_Remove_Special_Char(strTmp)

    '                strTMP_2 = "CustTypeName_Child = '" & strTmp & "'  "
    '                get_AML_RISK_SCORE_BY_TEXT_GENERAL_FROM_DB_customer(strTMP_2, n_Risk_Score, Risk_dttb)
    '                risk = n_Risk_Score
    '                risk_string = risk & "," & risk_parameter

    '            Case enm_Risk_Cal._8_CUST_TYPE_CHILD_SUB

    '                If Len(Trim(risk_parameter)) = 0 Then Exit Select
    '                strTmp = UCase(risk_parameter)
    '                strTmp = RTrim(LTrim(strTmp))
    '                Str_Remove_Special_Char(strTmp)
    '                strTMP_2 = "CustTypeName_Child_SUB =  '" & strTmp & "'  "
    '                get_AML_RISK_SCORE_BY_TEXT_GENERAL_FROM_DB_customer(strTMP_2, n_Risk_Score, Risk_dttb)
    '                risk = n_Risk_Score
    '                risk_string = risk & "," & risk_parameter

    '            Case enm_Risk_Cal._9_partner_name_1

    '                If Len(Trim(risk_parameter)) = 0 Then Exit Select
    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)



    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    '  Dow_Jon_Detail = Trim(Mid(Dow_Jon_Detail1(0), 49, 18))
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '            Case enm_Risk_Cal._10_partner_name_2
    '                If Len(Trim(risk_parameter)) = 0 Then Exit Select
    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)
    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '            Case enm_Risk_Cal._11_partner_name_3
    '                If Len(Trim(risk_parameter)) = 0 Then Exit Select
    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)
    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '            Case enm_Risk_Cal._12_partner_name_4
    '                If Len(Trim(risk_parameter)) = 0 Then Exit Select
    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)
    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '            Case enm_Risk_Cal._13_partner_nationality_1

    '                strTmp = "CountryID = " & risk_parameter & " AND AML_PRODUCT_ID=  " & aml_product & " "
    '                get_AML_RISK_SCROE_BY_TEXT_GENERAL(strTmp, Risk_dttb, n_Risk_Score)
    '                risk = n_Risk_Score
    '                risk_string = risk & "," & risk_parameter_2

    '            Case enm_Risk_Cal._14_partner_nationality_2

    '                strTmp = "CountryID = " & risk_parameter & " AND AML_PRODUCT_ID=  " & aml_product & " "
    '                get_AML_RISK_SCROE_BY_TEXT_GENERAL(strTmp, Risk_dttb, n_Risk_Score)
    '                risk = n_Risk_Score
    '                risk_string = risk & "," & risk_parameter_2

    '            Case enm_Risk_Cal._15_partner_nationality_3

    '                strTmp = "CountryID = " & risk_parameter & " AND AML_PRODUCT_ID=  " & aml_product & " "
    '                get_AML_RISK_SCROE_BY_TEXT_GENERAL(strTmp, Risk_dttb, n_Risk_Score)
    '                risk = n_Risk_Score
    '                risk_string = risk & "," & risk_parameter_2


    '            Case enm_Risk_Cal._16_partner_nationality_4

    '                strTmp = "CountryID = " & risk_parameter & " AND AML_PRODUCT_ID=  " & aml_product & " "
    '                get_AML_RISK_SCROE_BY_TEXT_GENERAL(strTmp, Risk_dttb, n_Risk_Score)
    '                risk = n_Risk_Score


    '                risk_string = risk & "," & risk_parameter_2


    '            Case enm_Risk_Cal._17_Purpose

    '                Dim nTmp As Integer = 0
    '                If Not Len(Trim(risk_parameter)) = 0 Then
    '                    strTMP_2 = UCase(risk_parameter)
    '                    strTMP_2 = RTrim(LTrim(strTMP_2))
    '                    'Str_Remove_Special_Char(strTMP_2)
    '                    strTmp = "PURPOSE_WORD= '" & strTMP_2 & "'  AND isActive = 'TRUE'"
    '                    get_AML_RISK_SCORE_BY_TEXT_GENERAL_FROM_DB(strTmp, n_Risk_Score, Risk_dttb)
    '                    risk = n_Risk_Score

    '                    risk_string = risk & "," & risk_parameter

    '                End If
    '                If Not Len(Trim(risk_parameter)) = 0 Then
    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If


    '            Case enm_Risk_Cal._18_Source_OF_Income

    '                Dim nTmp As Integer = 0
    '                If Not Len(Trim(risk_parameter)) = 0 Then
    '                    strTMP_2 = UCase(risk_parameter)
    '                    strTMP_2 = RTrim(LTrim(strTMP_2))
    '                    'Str_Remove_Special_Char(strTMP_2)
    '                    strTmp = "ADD_WORD= '" & strTMP_2 & "'  AND isActive = 'TRUE'"
    '                    get_AML_RISK_SCORE_BY_TEXT_GENERAL_FROM_DB_PARSE_TEXT(strTmp, n_Risk_Score, Risk_dttb)
    '                    risk = n_Risk_Score

    '                    risk_string = risk & "," & risk_parameter

    '                End If
    '                If Not Len(Trim(risk_parameter)) = 0 Then
    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '            Case enm_Risk_Cal._19_BANK_ACCOUNT_ben

    '                If Len(Trim(risk_parameter)) = 0 Then Exit Select
    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)
    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '            Case enm_Risk_Cal._20_BANK_IDENTIFIER_ben
    '                If Len(Trim(risk_parameter)) = 0 Then Exit Select

    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)
    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If


    '            Case enm_Risk_Cal._21_BANK_NAME_ben

    '                If Len(Trim(risk_parameter)) = 0 Then Exit Select
    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)
    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '            Case enm_Risk_Cal._22_BANK_ADDRESS_ben

    '                If Len(Trim(risk_parameter)) = 0 Then Exit Select
    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)
    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '            Case enm_Risk_Cal._23_BANK_IDENTIFIER_intermediate_ben

    '                If Len(Trim(risk_parameter)) = 0 Then Exit Select
    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)
    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '            Case enm_Risk_Cal._3_COUNTRY_BEN


    '                strTmp = "CountryID = " & risk_parameter & " AND AML_PRODUCT_ID=  " & aml_product & " "
    '                get_AML_RISK_SCROE_BY_TEXT_GENERAL(strTmp, Risk_dttb, n_Risk_Score)
    '                risk = n_Risk_Score
    '                risk_string = risk & "," & risk_parameter_2

    '            Case enm_Risk_Cal._1_TrRisk_Consignee_Name


    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)
    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '            Case enm_Risk_Cal._2_TrRisk_Address


    '                Dim nTmp As Integer = 0

    '                If Not Len(Trim(risk_parameter)) = 0 Then
    '                    strTMP_2 = UCase(risk_parameter)
    '                    strTMP_2 = RTrim(LTrim(strTMP_2))
    '                    '  Str_Remove_Special_Char(strTMP_2)
    '                    strTmp = "ADD_WORD= '" & strTMP_2 & "'  AND isActive = 'TRUE'"
    '                    get_AML_RISK_SCORE_BY_TEXT_GENERAL_FROM_DB_PARSE_TEXT(strTmp, n_Risk_Score, Risk_dttb)
    '                    risk = n_Risk_Score

    '                    risk_string = risk & "," & risk_parameter

    '                End If

    '                If Len(Trim(risk_parameter)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If


    '            Case enm_Risk_Cal._3_TrRisk_Shipment_Vessel


    '                strTmp = risk_parameter
    '                World_Check_Data_Compare(strTmp, str_Result)
    '                If Not Len(Trim(str_Result)) = 0 Then

    '                    Dim Dow_Jon_Detail1 = str_Result.Split("/")
    '                    Dow_Jon_Detail = Dow_Jon_Detail1(0).Replace("** DOW JONES DETAILS", "")
    '                    Dow_Jon_Detail = "DJ:" & Dow_Jon_Detail.Replace("##################", "").Trim
    '                    Dow_Jon_Detail = Dow_Jon_Detail & " / " & Dow_Jon_Detail1(1)
    '                    risk = 4
    '                    risk_string = risk & "," & risk_parameter & " | " & Trim(Dow_Jon_Detail)
    '                End If

    '                If Len(Trim(str_Result)) = 0 Then

    '                    risk = 0
    '                    risk_string = risk & "," & risk_parameter
    '                End If

    '        End Select




    '    Catch ex As Exception
    '        'ERR_MSG(ex.Message & "   " & obj_Risk_Cal)
    '    Finally
    '        'objMN = Nothing
    '    End Try

    'End Sub

   
    Public Sub GET_Customer(ByVal CustID_GID As String, ByVal CustID_Det_GID As String, _
                         ByRef CustID As String, ByRef CustID_Detail As String, _
                         ByRef FirstName As String, ByRef MiddleName As String, _
                         ByRef LastName As String, ByRef FirstName_ARB As String, _
                         ByRef dtmDate As DateTime, ByRef ExpiryDate As DateTime, _
                         ByRef DateofIssue As DateTime, ByRef DateofBirth As DateTime, _
                         ByRef Address1 As String, ByRef Address2 As String, _
                         ByRef City As String, ByRef Zip As String, _
                         ByRef Phone As String, ByRef Fax As String, _
                         ByRef Email As String, ByRef Details As String, _
                         ByRef sponsor As String, ByRef IDNo As String, _
                         ByRef BankAccNo As String, ByRef BankName As String, _
                         ByRef BankBranch As String, ByRef CustomerType As Integer, _
                         ByRef IdentityType_Cust As Integer, ByRef Nationality As Integer, _
                         ByRef Emirates As Integer, ByRef IssuePlace As Integer, _
                         ByRef visa_Status As Integer, ByRef Cust_imageID As String, ByRef btn_Block As String, _
                                 ByRef isFWD As Boolean, ByRef EC_CODE As String, ByRef EC_IBAN_CODE As String, _
                               ByRef EC_Account_Classification As String)

        Dim dtTb As New DataTable
        Dim objPara(1) As clsPara

        Try
            '' ''setCon(True)
            '' ''strSql = "SELECT   isnull(a.EC_IBAN_CODE,0) as EC_IBAN_CODE , isnull(a.isFWD,0) as isFWD, isnull(a.EC_CODE,0) as EC_CODE,isnull(a.EC_Account_Classification,0) as EC_Account_Classification, A.custid_display as custid,b.custid_details,  A.CustName_Arb, A.CustName_First,A.CustName_Middle,A.CustName_Last, A.Nat_CountryID, " _
            '' ''& " A.CustomerType, A.emi_ID,A.custTypeid,A.isActive, B.PlaceOfIssue,B.IDTypeID, B.IDNumber, B.DateOfIssue, B.ExpiryDate, B.Address1, B.Address2, " _
            '' ''& " b.City, b.Tel, b.Fax, b.Email, b.SponserName,b.visa_status_id, b.Details, A.mDate,b.Dateof_Birth, b.ZIP,BankAccNumber,BankName,BranchName " _
            '' ''& " FROM tblCustomer AS A INNER JOIN " _
            '' ''& " tblCustomer_Details AS B ON A.CustID_GID = B.Custid_GID " _
            '' ''& " WHERE      (A.CustID_GID = '" & CustID_GID & "' ) AND (B.Custid_Details_GID = '" & CustID_Det_GID & "' ) "
            ''  stp_GET_CUSTOMER_GID()
            ''@_CustID_GID NVARCHAR(50),
            ''@_Custid_Details_GID NVARCHAR(50)



            setPara(objPara(0), "_CustID_GID", CustID_GID, "50", clsPara.ColType.Varchar)
            setPara(objPara(1), "_Custid_Details_GID", CustID_Det_GID, "50", clsPara.ColType.Varchar)

            dtTb = DTTB_Fill_Generic("stp_GET_CUSTOMER_GID", objPara)


            Dim I As Integer = 0
            For I = 0 To dtTb.Rows.Count - 1


                CustID = dtTb.Rows(I).Item("custid").ToString
                CustID_Detail = dtTb.Rows(I).Item("custid_details").ToString

                FirstName = dtTb.Rows(I).Item("CustName_First").ToString
                MiddleName = dtTb.Rows(I).Item("CustName_Middle").ToString
                LastName = dtTb.Rows(I).Item("CustName_Last").ToString
                FirstName_ARB = dtTb.Rows(I).Item("CustName_Arb").ToString

                dtmDate = dtTb.Rows(I).Item("mDate")
                ExpiryDate = dtTb.Rows(I).Item("ExpiryDate")
                DateofIssue = dtTb.Rows(I).Item("DateOfIssue")
                DateofBirth = dtTb.Rows(I).Item("Dateof_Birth")
                If Not dtTb.Rows(I).Item("Address1") Is System.DBNull.Value Then
                    Address1 = dtTb.Rows(I).Item("Address1")
                End If
                If Not dtTb.Rows(I).Item("Address2") Is System.DBNull.Value Then
                    Address2 = dtTb.Rows(I).Item("Address2")
                End If
                If Not dtTb.Rows(I).Item("city") Is System.DBNull.Value Then
                    City = dtTb.Rows(I).Item("city").ToString
                End If
                If Not dtTb.Rows(I).Item("zip") Is System.DBNull.Value Then
                    Zip = dtTb.Rows(I).Item("zip").ToString
                End If
                If Not dtTb.Rows(I).Item("tel") Is System.DBNull.Value Then
                    Phone = dtTb.Rows(I).Item("tel").ToString
                End If
                If Not dtTb.Rows(I).Item("Fax") Is System.DBNull.Value Then
                    Fax = dtTb.Rows(I).Item("Fax").ToString
                End If

                If Not dtTb.Rows(I).Item("email") Is System.DBNull.Value Then
                    Email = dtTb.Rows(I).Item("email").ToString
                End If
                If Not dtTb.Rows(I).Item("Details") Is System.DBNull.Value Then
                    Details = dtTb.Rows(I).Item("Details").ToString
                End If

                If Not dtTb.Rows(I).Item("SponserName") Is System.DBNull.Value Then
                    sponsor = dtTb.Rows(I).Item("SponserName").ToString
                End If


                If Not dtTb.Rows(I).Item("IDNumber") Is System.DBNull.Value Then
                    IDNo = dtTb.Rows(I).Item("IDNumber").ToString
                End If


                If Not dtTb.Rows(I).Item("BankAccNumber") Is System.DBNull.Value Then
                    BankAccNo = dtTb.Rows(I).Item("BankAccNumber")
                End If
                If Not dtTb.Rows(I).Item("BankName") Is System.DBNull.Value Then
                    BankName = dtTb.Rows(I).Item("BankName")
                End If
                If Not dtTb.Rows(I).Item("BranchName") Is System.DBNull.Value Then
                    BankBranch = dtTb.Rows(I).Item("BranchName")
                End If

                If Not (dtTb.Rows(I).Item("isActive")) Then
                    btn_Block = modMain.gSTR_ACTIVE.ToString
                End If
                If dtTb.Rows(I).Item("isActive") Then
                    btn_Block = modMain.gSTR_BLOCK.ToString
                End If
                If Not dtTb.Rows(0).Item("CustomerType") Is System.DBNull.Value Then
                    CustomerType = CInt(dtTb.Rows(0).Item("CustomerType").ToString)
                End If

                If Not dtTb.Rows(0).Item("IDTypeID") Is System.DBNull.Value Then
                    IdentityType_Cust = CInt(dtTb.Rows(0).Item("IDTypeID").ToString)
                End If

                If Not dtTb.Rows(0).Item("Nat_CountryID") Is System.DBNull.Value Then
                    Nationality = CInt(dtTb.Rows(0).Item("Nat_CountryID").ToString)
                End If
                If Not dtTb.Rows(0).Item("emi_ID") Is System.DBNull.Value Then
                    Emirates = CInt(dtTb.Rows(0).Item("emi_ID").ToString)
                End If
                If Not dtTb.Rows(0).Item("PlaceOfIssue") Is System.DBNull.Value Then
                    IssuePlace = CInt(dtTb.Rows(0).Item("PlaceOfIssue").ToString)
                End If

                If Not dtTb.Rows(0).Item("visa_status_id") Is System.DBNull.Value Then
                    visa_Status = CInt(dtTb.Rows(0).Item("visa_status_id").ToString)

                End If


                If Not dtTb.Rows(0).Item("isFWD") Is System.DBNull.Value Then
                    isFWD = (dtTb.Rows(0).Item("isFWD").ToString)

                End If

                If Not dtTb.Rows(0).Item("EC_CODE") Is System.DBNull.Value Then
                    EC_CODE = (dtTb.Rows(0).Item("EC_CODE").ToString)

                End If
                If Not dtTb.Rows(0).Item("EC_IBAN_CODE") Is System.DBNull.Value Then
                    EC_IBAN_CODE = (dtTb.Rows(0).Item("EC_IBAN_CODE").ToString)

                End If

                If Not dtTb.Rows(0).Item("EC_Account_Classification") Is System.DBNull.Value Then
                    EC_Account_Classification = (dtTb.Rows(0).Item("EC_Account_Classification").ToString)

                End If

            Next
            If Len(Trim(CustID_GID)) <> 0 Then

                Get_Custid_iamge_Mast(Cust_imageID, CustID_GID)
                If (Trim(Cust_imageID)) <> "0" Then
                    Cust_imageID = Cust_imageID
                End If


            End If
            '********************************

            ' ''Dim o_dtIDExpiryDate As DATETI
            ' ''Dim o_Now As Date = Now.Date
            ' ''getMMDDYYYY(ExpiryDate, o_dtIDExpiryDate)
            ' ''If isDATEValid_NOTGREATER(o_dtIDExpiryDate, o_Now) Then
            ' ''    init_CustomerPane()
            ' ''    init_CustomerPane_Detail()
            ' ''    Throw New Exception(txtFirstName.Text & "  " & txtMiddleName.Text & " ,  Customer ID Expired")
            ' ''End If


        Catch ex As Exception
            Throw New Exception("NO CUSTOMER DATA FOUND...")
        Finally

            dtTb.Dispose()
            dtTb = Nothing
            objPara = Nothing

        End Try


    End Sub
   



    Public Function DTTB_Fill_GenericRpt(ByVal strSPName As String, ByVal strdttbName As String, ByVal objDataset As Object, _
                                      ByVal ParamArray PARA() As clsPara) As DataSet
        Dim i As Integer = 0

        Dim loComm As New SqlClient.SqlCommand
        Dim MyDA As New SqlClient.SqlDataAdapter()
        Dim sdaAdapter As SqlDataAdapter
        'Dim dtTb As DataTable = New DataTable("tb")

        Try
            setCon(True)
            loComm.Connection = gCon
            sdaAdapter = New SqlDataAdapter(loComm)
            loComm.CommandText = strSPName
            loComm.CommandType = CommandType.StoredProcedure
            loComm.CommandTimeout = 2200
            For i = 0 To PARA.Length - 1
                loComm.Parameters.Add(fun_GetPara(PARA(i)))
            Next
            MyDA.SelectCommand = loComm

            'This is our DataSet created at Design Time  

            MyDA.Fill(objDataset, strdttbName)

            'sdaAdapter.Fill(dtTb)

            DTTB_Fill_GenericRpt = objDataset
            Exit Function

        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            setCon(False)

        End Try


        DTTB_Fill_GenericRpt = objDataset


    End Function


    Public Function DTTB_Fill_GenericRpt_img(ByVal strSPName As String, ByVal strdttbName As String, ByVal objDataset As Object, _
                                         ByVal ParamArray PARA() As clsPara) As DataSet
        Dim i As Integer = 0

        Dim loComm As New SqlClient.SqlCommand
        Dim MyDA As New SqlClient.SqlDataAdapter()
        Dim sdaAdapter As SqlDataAdapter
        Dim dtTb As DataTable = New DataTable("tb")

        Try
            setCon_IMAGE(True)
            loComm.Connection = gCon_IMAGE
            sdaAdapter = New SqlDataAdapter(loComm)
            loComm.CommandText = strSPName
            loComm.CommandType = CommandType.StoredProcedure
            For i = 0 To PARA.Length - 1
                loComm.Parameters.Add(fun_GetPara(PARA(i)))
            Next
            MyDA.SelectCommand = loComm

            'This is our DataSet created at Design Time      
            MyDA.Fill(objDataset, strdttbName)

            'sdaAdapter.Fill(dtTb)

            DTTB_Fill_GenericRpt_img = objDataset
            Exit Function

        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            '  setCon_WPS(False)

        End Try


        DTTB_Fill_GenericRpt_img = objDataset


    End Function


    Public Sub getUSER_INFO(ByVal dtTb_USR As DataTable, _
                             ByRef nUserID As Integer, _
                             ByRef nCCID As Integer)

        Try
            nUserID = dtTb_USR.Rows(0)("userid")
            nCCID = dtTb_USR.Rows(0)("locid")
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub

    Public Sub getCompany_GID(ByVal dtTb_Comp As DataTable, ByRef comp_GID As String)
        Try
            comp_GID = dtTb_Comp.Rows(0)("Company_GID")
        Catch ex As Exception
            dtTb_Comp.Dispose()
            dtTb_Comp = Nothing
        End Try
    End Sub

    Public Sub getCompany_Representative_Name(ByVal dtTb_Comp As DataTable, ByRef Comp_Representative_Name As String)
        Try
            Comp_Representative_Name = dtTb_Comp.Rows(0)("Representative_Name")
        Catch ex As Exception
            dtTb_Comp.Dispose()
            dtTb_Comp = Nothing
        End Try
    End Sub


    Public Sub getUSER_INFO_Name_GID_new(ByVal dtTb_USR As DataTable, _
                             ByRef nUserID As Integer, _
                             ByRef nCCID As Integer, ByRef strUSER As String, ByRef strCCName As String, _
                              ByRef strUSERID_GID As String)

        Try
            nUserID = dtTb_USR.Rows(0)("userid")
            nCCID = dtTb_USR.Rows(0)("locid")
            strUSER = dtTb_USR.Rows(0)("NAME")
            strCCName = dtTb_USR.Rows(0)("CCNAME")
            strUSERID_GID = dtTb_USR.Rows(0)("userid_GID")
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub

    Public Sub getUSER_INFO_GID(ByVal dtTb_USR As DataTable, _
                             ByRef nUserID As Integer, _
                             ByRef nCCID As Integer, _
                             ByRef strUSERID_GID As String)

        Try
            nUserID = dtTb_USR.Rows(0)("userid")
            nCCID = dtTb_USR.Rows(0)("locid")
            strUSERID_GID = dtTb_USR.Rows(0)("userid_GID")
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub

    Public Sub getUSER_INFO_user_group_gid(ByVal dtTb_USR As DataTable, _
                            ByRef nUserID As Integer, _
                            ByRef nCCID As Integer, _
                            ByRef strUSERgroup_GID As String)

        Try
            nUserID = dtTb_USR.Rows(0)("userid")
            nCCID = dtTb_USR.Rows(0)("locid")
            strUSERgroup_GID = dtTb_USR.Rows(0)("UserGroupID_Gid")
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub


    Public Sub getUSER_INFO_GID_WITHOUT_ID(ByVal dtTb_USR As DataTable, _
                            ByRef nCCID As Integer, _
                            ByRef strUSERID_GID As String)

        Try
            nCCID = dtTb_USR.Rows(0)("locid")
            strUSERID_GID = dtTb_USR.Rows(0)("userid_GID")
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub





    Public Sub getUSER_INFO_GID_isMultiBR(ByVal dtTb_USR As DataTable, _
                            ByRef nUserID As Integer, _
                            ByRef nCCID As Integer, _
                            ByRef strUSERID_GID As String, _
                            ByRef isMutliBR As Boolean, _
                            ByRef isSupervisior As Boolean)

        Try
            nUserID = dtTb_USR.Rows(0)("userid")
            nCCID = dtTb_USR.Rows(0)("locid")
            strUSERID_GID = dtTb_USR.Rows(0)("userid_GID")
            isMutliBR = dtTb_USR.Rows(0)("isBRCOMBO")
            isSupervisior = dtTb_USR.Rows(0)("isPOPUP")
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub




    Public Sub getUSER_INFO_GID_isMultiBR_WITH_NAME(ByVal dtTb_USR As DataTable, _
                            ByRef nUserID As Integer, _
                            ByRef nCCID As Integer, _
                            ByRef strUSERID_GID As String, _
                            ByRef isMutliBR As Boolean, _
                            ByRef isSupervisior As Boolean, _
                            ByRef strNAme As String)

        Try
            nUserID = dtTb_USR.Rows(0)("userid")
            nCCID = dtTb_USR.Rows(0)("locid")
            strUSERID_GID = dtTb_USR.Rows(0)("userid_GID")
            isMutliBR = dtTb_USR.Rows(0)("isBRCOMBO")
            isSupervisior = dtTb_USR.Rows(0)("isPOPUP")
            strNAme = dtTb_USR.Rows(0)("NAME")
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub
    Public Sub getUSER_Name(ByVal dtTb_USR As DataTable, _
                              ByRef strUSER As String)

        Try

            strUSER = dtTb_USR.Rows(0)("NAME")

        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub





    Public Sub getUSER_INFO_SBCD(ByVal dtTb_USR As DataTable, _
                            ByRef nUsr_sbcd As Integer)

        Try
            nUsr_sbcd = dtTb_USR.Rows(0)("subcodeid")
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub



    Public Sub getUSER_INFO_Name(ByVal dtTb_USR As DataTable, _
                             ByRef nUserID As Integer, _
                             ByRef nCCID As Integer, ByRef strUSER As String, ByRef strCCName As String)

        Try
            nUserID = dtTb_USR.Rows(0)("userid")

            nCCID = dtTb_USR.Rows(0)("locid")
            strUSER = dtTb_USR.Rows(0)("NAME")
            strCCName = dtTb_USR.Rows(0)("CCNAME")

        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub

    Public Sub getUSER_INFO_Name_Gid(ByVal dtTb_USR As DataTable, _
                            ByRef nUserID As Integer, _
                            ByRef nCCID As Integer, ByRef strUSER As String, ByRef strCCName As String, ByRef strUserID_gid As String)

        Try
            nUserID = dtTb_USR.Rows(0)("userid")

            nCCID = dtTb_USR.Rows(0)("locid")
            strUSER = dtTb_USR.Rows(0)("NAME")
            strCCName = dtTb_USR.Rows(0)("CCNAME")
            strUserID_gid = dtTb_USR.Rows(0)("userid_gid")
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub
    Public Sub getUSER_INFO_CCShort_Name(ByVal dtTb_USR As DataTable, _
                             ByRef nUserID As Integer, _
                             ByRef nCCID As Integer, ByRef strUSER As String, ByRef strCCShortName As String)

        Try
            nUserID = dtTb_USR.Rows(0)("userid")
            nCCID = dtTb_USR.Rows(0)("locid")
            strUSER = dtTb_USR.Rows(0)("NAME")
            strCCShortName = dtTb_USR.Rows(0)("compnameshort")
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub



    Public Sub getUSER_INFO_CCShort_Name_GID(ByVal dtTb_USR As DataTable, _
                             ByRef nUserID As Integer, _
                             ByRef nCCID As Integer, ByRef strUSER As String, _
                             ByRef strCCShortName As String, _
                             ByRef sUSERID_GID As String)

        Try
            nUserID = dtTb_USR.Rows(0)("userid")
            nCCID = dtTb_USR.Rows(0)("locid")
            strUSER = dtTb_USR.Rows(0)("NAME")
            strCCShortName = dtTb_USR.Rows(0)("compnameshort")
            sUSERID_GID = dtTb_USR.Rows(0)("userid_GID")
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            dtTb_USR.Dispose()
            dtTb_USR = Nothing
        End Try

        '-----------------------------------
    End Sub


    Private Function fun_GetPara(ByVal Para As clsPara) As SqlParameter
        Dim sqlPM As New SqlParameter()
        Dim lclPR As New clsPara
        Try


            lclPR.mVarColType = Para.prpType

            sqlPM.ParameterName = "@" + Para.prpName
            Select Case lclPR.mVarColType

                '---------  INT ----------------
                Case clsPara.ColType.Int
                    sqlPM.DbType = SqlDbType.Int
                    sqlPM.Size = 4
                    sqlPM.Precision = 10
                    sqlPM.Scale = 0
                    sqlPM.Value = CInt(Para.prpVAL)
                    '---------  VARCHAR ----------------
                Case clsPara.ColType.Varchar
                    sqlPM.DbType = SqlDbType.VarChar
                    sqlPM.Size = Para.prpSIZE
                    sqlPM.Precision = 0
                    sqlPM.Scale = 0
                    sqlPM.Value = (Para.prpVAL)
                    '---------  N-VARCHAR ----------------
                Case clsPara.ColType.NVarchar
                    sqlPM.DbType = SqlDbType.NVarChar
                    sqlPM.Size = Para.prpSIZE
                    sqlPM.Precision = 0
                    sqlPM.Scale = 0
                    sqlPM.Value = (Para.prpVAL)
                    '---------  DATE ----------------
                Case clsPara.ColType.dt
                    sqlPM.DbType = SqlDbType.DateTime
                    sqlPM.Size = 8
                    sqlPM.Precision = 23
                    sqlPM.Scale = 3
                    sqlPM.Value = CDate(Para.prpVAL)
                    sqlPM.SqlDbType = SqlDbType.DateTime
                    '---------  BIG-INT ----------------
                Case clsPara.ColType.bigINT
                    sqlPM.DbType = SqlDbType.Int
                    sqlPM.Size = 8
                    sqlPM.Precision = 19
                    sqlPM.Scale = 0
                    '---------  FLOAT ----------------
                Case clsPara.ColType.float
                    sqlPM.DbType = SqlDbType.Float
                    sqlPM.Size = 8
                    sqlPM.Precision = 38
                    sqlPM.Scale = 0
                    '---------  BOOLEAN ----------------
                Case clsPara.ColType.BIT
                    sqlPM.DbType = SqlDbType.Bit
                    sqlPM.Size = 1
                    sqlPM.Precision = 1
                    sqlPM.Scale = 0

            End Select
            sqlPM.Direction = ParameterDirection.Input
            sqlPM.IsNullable = False

            sqlPM.SourceColumn = ""
            sqlPM.SourceVersion = DataRowVersion.Proposed

            fun_GetPara = sqlPM
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function



    'Public Function SelectQueryDS(ByVal strTable As String, _
    ' ByVal strDisplayField As String, _
    ' ByVal strDataField As String, _
    ' ByVal strWhereCriteria As String, _
    ' ByRef objCn As SqlConnection) As DataSet
    '    Dim moComm As New SqlClient.SqlCommand


    '    Dim Con As New SqlConnection(gStrCnString)

    '    Dim ds As New DataSet()
    '    moComm.Connection = Con
    '    strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria

    '    moComm.CommandText = strSql
    '    Try

    '        Dim dt As DataTable = New DataTable("dt")
    '        Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
    '        sdaAdapter.Fill(ds)


    '    Catch ex As Exception

    '    End Try
    '    Return ds
    'End Function

    Public Function split_ID(ByVal txtID As String) As String
        txtID = txtID.PadLeft(gVoucherLength, "0")
        Return Mid(txtID, Len(txtID) - 5)
    End Function

    'Public Function getTextFld(ByVal tblName As String, ByVal fldName As String, ByVal strWhere As String, ByRef objCn As SqlConnection) As String

    '    Dim loComm As New SqlClient.SqlCommand
    '    Dim strResult As String
    '    loComm.Connection = objCn

    '    Try
    '        strSql = "Select (" & fldName & ") as fldVal From " & tblName & " "
    '        If Len(Trim(strWhere)) <> 0 Then
    '            strWhere = " WHERE " & strWhere
    '            strSql = strSql & strWhere
    '        End If
    '        loComm.CommandText = strSql
    '        strResult = loComm.ExecuteScalar
    '        If strResult.Length = 0 Then
    '            getTextFld = ""
    '        Else
    '            getTextFld = strResult
    '        End If

    '    Catch ex As Exception
    '        'MsgBox("Err in Field retrieval .... Please contact to system administrator ")
    '        getTextFld = ""
    '    Finally

    '        loComm.Dispose()
    '        loComm = Nothing
    '    End Try

    'End Function

    'Public Function getTextFld_float(ByVal tblName As String, ByVal fldName As String, ByVal strWhere As String, ByRef objCn As SqlConnection) As Double

    '    Dim loComm As New SqlClient.SqlCommand
    '    Dim fltValue As Double
    '    loComm.Connection = objCn

    '    Try
    '        strSql = "Select (" & fldName & ") as fldVal From " & tblName & " "
    '        If Len(Trim(strWhere)) <> 0 Then
    '            strWhere = " WHERE " & strWhere
    '            strSql = strSql & strWhere
    '        End If

    '        loComm.CommandType = CommandType.Text
    '        loComm.CommandText = strSql
    '        fltValue = loComm.ExecuteScalar()
    '        getTextFld_float = fltValue
    '    Catch ex As Exception
    '        'MsgBox("Err in Field retrieval .... Please contact to system administrator ")
    '        getTextFld_float = 1
    '    Finally
    '        loComm.Dispose()
    '        loComm = Nothing
    '    End Try

    'End Function
    Public Sub setServerDate_WB(ByVal gobjCon As SqlConnection)
        Dim loComm As New SqlClient.SqlCommand
        Dim loDataReader As SqlClient.SqlDataReader = Nothing

        Try

            loComm.Connection = gobjCon
            loComm.CommandType = CommandType.Text
            loComm.CommandText = "SELECT GetDate()"
            loDataReader = loComm.ExecuteReader
            If loDataReader.Read Then
                gServerDATE = loDataReader(0)
            End If
        Catch ex As Exception

        Finally
            loDataReader.Close()
            loDataReader = Nothing
            loComm.Dispose()
            loComm = Nothing
            'gobjCon.Close()
        End Try


    End Sub







    Public Function getMaxValue(ByVal fldName As String, ByVal tblName As String, ByVal strWhere As String, ByVal gobjCon As SqlClient.SqlConnection) As Long
        Dim loComm As New SqlClient.SqlCommand
        Dim lngMaxValue As Long

        Try
            loComm.Connection = gobjCon
            strSql = "Select IsNull(Max(" & fldName & "),0) as ID From " & tblName & " "
            If Len(Trim(strWhere)) <> 0 Then
                strSql = strSql & " WHERE " & strWhere
            End If
            loComm.CommandText = strSql
            lngMaxValue = loComm.ExecuteScalar

            lngMaxValue = lngMaxValue + 1

        Catch ex As Exception
            MsgBox("Err in ID Generation .... Please contact to system administrator " & strSql)
        Finally

            loComm.Dispose()
            loComm = Nothing
        End Try


    End Function

    Public Sub NAME_API_CALL_fcy(ByVal str_full_name As String, ByRef Return_string As String, ByRef hit As Boolean, ByRef COUNT As Integer, ByRef str_Head As String, ByRef DJ_URL_REQ As String, ByRef DJ_URL_RES As String)

        'Dim LOGIN As String = "18/ShaheenApi"
        'Dim PASSWORD As String = "ShaheenApi"

        'Dim LOGIN As String = "18/MesrkanAPI"
        'Dim PASSWORD As String = "MesrkanAPI"


        'str_full_name = RTrim(LTrim(str_full_name))
        'Dim STR_NAME As String = str_full_name
        'Dim STR_PEP_Categories As String = 0
        'Dim STR_SIC_Categories As String = 0
        'Dim STR_AMC_Categories As String = 0
        'Dim str_record_type As String = ""  'P/E
        'Dim str_search_type As String = ""  'precise/near/broad

        'Dim str_PEP As String = "false"

        ''Dim URL As String = "https://djrc.api.dowjones.com/v1/search/name?name=" & STR_NAME & "&record-type=P,E&search-type=broad&exclude-deceased=true&hits-from=1&hits-to=50"


        'Try
        '    '1)--- Record Type ----------------------
        '    'If chkREC_Type_Person.Checked Then
        '    '    str_record_type = "P"
        '    'End If
        '    'If chkREC_Type_Entity.Checked Then
        '    '    If Len(Trim(str_record_type)) <> 0 Then
        '    '        str_record_type = str_record_type + "," + "E"
        '    '    Else
        '    '        str_record_type = "E"
        '    '    End If
        '    'End If
        '    '---------------------------------------
        '    '---------------------------------------

        '    '2)--- Search Type ----------------------
        '    'If RD_precise.Checked Then str_search_type = "precise"
        '    'If RD_near.Checked Then str_search_type = "near"
        '    'If RD_broad.Checked Then str_search_type = "broad"
        '    str_search_type = "precise"
        '    '---------------------------------------
        '    '---------------------------------------

        '    '3)--- Search Type PEP ----------------------
        '    'If chk_PEP.Checked Then
        '    '    str_PEP = "true"
        '    'End If
        '    '4)--- Search Type PEP-CATGORY ----------------------

        '    'If rbt_selected_worldcheck.Checked Then
        '    '    For j = 0 To chk_list.CheckedItems.Count - 1
        '    '        'MsgBox(chk_list.CheckedItems(j).ToString)
        '    '        'If UCase(chk_list.CheckedItems(j).ToString) Then
        '    '        STR_PEP_Categories = (chk_list.CheckedIndices(j).ToString)
        '    '        'MsgBox(j)
        '    '        'Exit For
        '    '        'End If
        '    '    Next

        '    'End If
        '    '---------------------------------------

        '    '5)--- Search Type sic ----------------------

        '    'If rbt_selected_SIC.Checked Then
        '    '    For j = 0 To chk_list_SIC.CheckedItems.Count - 1
        '    '        'MsgBox(chk_list.CheckedItems(j).ToString)
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Corruption") Then
        '    '            STR_SIC_Categories = 2
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Enhanced Country Risk") Then
        '    '            STR_SIC_Categories = 4
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Financial Crime") Then
        '    '            STR_SIC_Categories = 6
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Organised Crime") Then
        '    '            STR_SIC_Categories = 8
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Organised Crime Japan") Then
        '    '            STR_SIC_Categories = 9
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Tax Crime") Then
        '    '            STR_SIC_Categories = 12
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Terror") Then
        '    '            STR_SIC_Categories = 13
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Trafficking") Then
        '    '            STR_SIC_Categories = 14
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("War Crimes") Then
        '    '            STR_SIC_Categories = 15
        '    '        End If
        '    '        'MsgBox(j)
        '    '        'Exit For
        '    '        'End If
        '    '    Next

        '    'End If
        '    '---------------------------------------
        '    'djrcfeed.dowjones.com()

        '    '---------------------------------------
        '    Dim URL As String = "https://djrc.api.dowjones.com/v1/search/name?name=" & STR_NAME
        '    URL = URL + "&record-type=" & str_record_type
        '    URL = URL + "&search-type=" & str_search_type
        '    URL = URL + "&filter-pep-operator=" & "and"
        '    URL = URL + "&filter-pep-exclude-adsr=" & str_PEP
        '    'If rbt_selected_worldcheck.Checked And STR_PEP_Categories <> 0 Then
        '    '    URL = URL + "&filter-pep=" & STR_PEP_Categories
        '    'End If
        '    'If rbt_selected_SIC.Checked And STR_SIC_Categories <> 0 Then
        '    '    URL = URL + "&filter-sic=" & STR_SIC_Categories
        '    '    URL = URL + "&filter-sic-operator=" & "AND"
        '    'End If


        '    'https://djrc.api.dowjones.com/v1/search/name?name=EMILIE KONIG&record-type=&search-type=precise&filter-pep-operator=and&filter-pep-exclude-adsr=false&exclude-deceased=true&filter-sic-operator=AND&hits-to=50

        '    URL = URL + "&exclude-deceased=true&filter-sic-operator=AND&hits-to=10"
        '    DJ_URL_REQ = URL


        '    System.Net.ServicePointManager.Expect100Continue = False
        '    ServicePointManager.Expect100Continue = True
        '    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls






        '    Dim req As Net.HttpWebRequest = Net.WebRequest.Create(URL)
        '    If Not LOGIN = Nothing AndAlso Not PASSWORD = Nothing Then
        '        Dim myCache As New System.Net.CredentialCache()
        '        myCache.Add(New Uri(URL), "Basic", New System.Net.NetworkCredential(LOGIN, PASSWORD))
        '        req.Credentials = myCache
        '        req.Headers("Authorization") = "Basic"
        '        req.UnsafeAuthenticatedConnectionSharing = True
        '        req.Method = "GET"
        '        req.ContentType = "application/plain"
        '        req.KeepAlive = True
        '    End If
        '    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
        '    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        '    Dim response As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
        '    Dim sr As New StreamReader(response.GetResponseStream())

        '    Dim ss As String = sr.ReadToEnd
        '    DJ_URL_RES = ss
        '    'Save it as excel & close stream
        '    xml_read(ss, str_full_name, Return_string, hit, COUNT, str_Head)
        '    'txt_WCHK_AREA.Text = tagless
        '    sr.Close()

        'Catch ex As Exception
        '    'Throw New Exception(ex.Message, ex)
        '    Exit Sub
        'End Try

    End Sub


    Public Sub NAME_API_CALL(ByVal str_full_name As String, ByRef Return_string As String, ByRef hit As Boolean, ByRef COUNT As Integer, ByRef str_Head As String)
        ''Dim LOGIN As String = "18/ShaheenApi"
        ''Dim PASSWORD As String = "ShaheenApi"

        ''Dim LOGIN As String = "18/imranfeed1"
        ''Dim PASSWORD As String = "imranfeed1"

        'Dim LOGIN As String = "18/MesrkanAPI"
        'Dim PASSWORD As String = "MesrkanAPI"



        'Dim STR_NAME As String = RTrim(LTrim(str_full_name))
        'Dim STR_PEP_Categories As String = 0
        'Dim STR_SIC_Categories As String = 0
        'Dim STR_AMC_Categories As String = 0
        'Dim str_record_type As String = ""  'P/E
        'Dim str_search_type As String = ""  'precise/near/broad

        'Dim str_PEP As String = "false"

        ''Dim URL As String = "https://djrc.api.dowjones.com/v1/search/name?name=" & STR_NAME & "&record-type=P,E&search-type=broad&exclude-deceased=true&hits-from=1&hits-to=50"


        'Try
        '    '1)--- Record Type ----------------------
        '    'If chkREC_Type_Person.Checked Then
        '    '    str_record_type = "P"
        '    'End If
        '    'If chkREC_Type_Entity.Checked Then
        '    '    If Len(Trim(str_record_type)) <> 0 Then
        '    '        str_record_type = str_record_type + "," + "E"
        '    '    Else
        '    '        str_record_type = "E"
        '    '    End If
        '    'End If
        '    '---------------------------------------
        '    '---------------------------------------

        '    '2)--- Search Type ----------------------
        '    'If RD_precise.Checked Then str_search_type = "precise"
        '    'If RD_near.Checked Then str_search_type = "near"
        '    'If RD_broad.Checked Then str_search_type = "broad"
        '    str_search_type = "precise"
        '    '---------------------------------------
        '    '---------------------------------------

        '    '3)--- Search Type PEP ----------------------
        '    'If chk_PEP.Checked Then
        '    '    str_PEP = "true"
        '    'End If
        '    '4)--- Search Type PEP-CATGORY ----------------------

        '    'If rbt_selected_worldcheck.Checked Then
        '    '    For j = 0 To chk_list.CheckedItems.Count - 1
        '    '        'MsgBox(chk_list.CheckedItems(j).ToString)
        '    '        'If UCase(chk_list.CheckedItems(j).ToString) Then
        '    '        STR_PEP_Categories = (chk_list.CheckedIndices(j).ToString)
        '    '        'MsgBox(j)
        '    '        'Exit For
        '    '        'End If
        '    '    Next

        '    'End If
        '    '---------------------------------------

        '    '5)--- Search Type sic ----------------------

        '    'If rbt_selected_SIC.Checked Then
        '    '    For j = 0 To chk_list_SIC.CheckedItems.Count - 1
        '    '        'MsgBox(chk_list.CheckedItems(j).ToString)
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Corruption") Then
        '    '            STR_SIC_Categories = 2
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Enhanced Country Risk") Then
        '    '            STR_SIC_Categories = 4
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Financial Crime") Then
        '    '            STR_SIC_Categories = 6
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Organised Crime") Then
        '    '            STR_SIC_Categories = 8
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Organised Crime Japan") Then
        '    '            STR_SIC_Categories = 9
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Tax Crime") Then
        '    '            STR_SIC_Categories = 12
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Terror") Then
        '    '            STR_SIC_Categories = 13
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("Trafficking") Then
        '    '            STR_SIC_Categories = 14
        '    '        End If
        '    '        If UCase(chk_list_SIC.CheckedItems(j).ToString) = UCase("War Crimes") Then
        '    '            STR_SIC_Categories = 15
        '    '        End If
        '    '        'MsgBox(j)
        '    '        'Exit For
        '    '        'End If
        '    '    Next

        '    'End If
        '    '---------------------------------------
        '    'djrcfeed.dowjones.com()

        '    '---------------------------------------
        '    Dim URL As String = "https://djrc.api.dowjones.com/v1/search/name?name=" & STR_NAME
        '    URL = URL + "&record-type=" & str_record_type
        '    URL = URL + "&search-type=" & str_search_type
        '    URL = URL + "&filter-pep-operator=" & "and"
        '    URL = URL + "&filter-pep-exclude-adsr=" & str_PEP
        '    'If rbt_selected_worldcheck.Checked And STR_PEP_Categories <> 0 Then
        '    '    URL = URL + "&filter-pep=" & STR_PEP_Categories
        '    'End If
        '    'If rbt_selected_SIC.Checked And STR_SIC_Categories <> 0 Then
        '    '    URL = URL + "&filter-sic=" & STR_SIC_Categories
        '    '    URL = URL + "&filter-sic-operator=" & "AND"
        '    'End If


        '    'https://djrc.api.dowjones.com/v1/search/name?name=EMILIE KONIG&record-type=&search-type=precise&filter-pep-operator=and&filter-pep-exclude-adsr=false&exclude-deceased=true&filter-sic-operator=AND&hits-to=50

        '    URL = URL + "&exclude-deceased=true&filter-sic-operator=AND&hits-to=10"
        '    System.Net.ServicePointManager.Expect100Continue = False
        '    ServicePointManager.Expect100Continue = True
        '    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls

        '    Dim req As Net.HttpWebRequest = CType(WebRequest.Create(URL), HttpWebRequest)

        '    req.KeepAlive = False
        '    req.ProtocolVersion = HttpVersion.Version10
        '    req.ServicePoint.ConnectionLimit = 1

        '    If Not LOGIN = Nothing AndAlso Not PASSWORD = Nothing Then
        '        Dim myCache As New System.Net.CredentialCache()
        '        myCache.Add(New Uri(URL), "Basic", New System.Net.NetworkCredential(LOGIN, PASSWORD))
        '        req.Credentials = myCache
        '        req.Headers("Authorization") = "Basic"
        '        req.UnsafeAuthenticatedConnectionSharing = True
        '        req.Method = "GET"
        '        req.ContentType = "application/plain"
        '        req.KeepAlive = True
        '    End If


        '    Dim response As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
        '    Dim sr As StreamReader = New StreamReader(response.GetResponseStream())






        '    Dim ss As String = sr.ReadToEnd
        '    'Save it as excel & close stream
        '    xml_read(ss, str_full_name, Return_string, hit, COUNT, str_Head)
        '    'txt_WCHK_AREA.Text = tagless
        '    sr.Close()

        'Catch ex As Exception
        '    'Throw New Exception(ex.Message)
        '    Exit Sub
        'End Try

    End Sub
  


    Public Sub xml_read(ByVal html As String, ByVal str_full_name As String, ByRef Return_string As String, ByRef bln_WCHIT As Boolean, ByRef Count As Integer, ByRef str_Head As String)
        Dim total_hits As Integer = 0
        Dim strALL, strRow As String
        Dim sender_details As String = ""
        Dim str_name As String
        Dim Catgory As String
        Dim country As String
        Dim gender As String
        ' Create the XML document
        Dim doc As New XmlDocument()
        ' Load the document from the string data
        doc.LoadXml(html)
        ' Get a list of all the Dispatch nodes

        Dim Match_NODE_head As XmlNodeList = doc.GetElementsByTagName("head")
        Dim Match_NODE As XmlNodeList = doc.GetElementsByTagName("match")

        'Dim str_Head As String = ""

        'TXT_TOTAL_HITS.Text = 0
        'txt_compliance_details.Text = ""

        Dim str_Compliance_details As String = ""

        For Each ND_Match_NODE_head As XmlNode In Match_NODE_head
            'If Len(txtBeneName.Text.Trim) <> 0 Then
            '    sender_details = " ** SENDER DETAILS  ####################################################################################" & vbNewLine & vbNewLine
            'End If

            str_Head = "API VERSION = " & ND_Match_NODE_head.SelectSingleNode("descendant::api-version").InnerText & " , " & ND_Match_NODE_head.SelectSingleNode("descendant::backend-version").InnerText
            str_Head = str_Head + vbNewLine

            str_Head = str_Head + "RESULT ID = " & ND_Match_NODE_head.SelectSingleNode("descendant::cached-results-id").InnerText
            str_Head = str_Head + vbNewLine
            If ND_Match_NODE_head.SelectSingleNode("descendant::total-hits").InnerText <> 0 Then

            End If
            'str_Head = str_Head + "TOTAL HITS = " & ND_Match_NODE_head.SelectSingleNode("descendant::total-hits").InnerText

            str_Compliance_details = "Customer : " & str_full_name & " , RESULT ID : " & ND_Match_NODE_head.SelectSingleNode("descendant::cached-results-id").InnerText


        Next

        'lblCount.Text = str_Head


        Try


            Dim nCount As Integer = 0
            Dim J As Integer = 0
            Dim str_MN_TAG_INSIDE As Integer
            Dim n_FOR_MN_INNER As Integer = 0
            If Len(str_full_name) <> 0 Then
                sender_details = " ** DOW JONES DETAILS   ##################" & vbNewLine & vbNewLine
                strALL = sender_details
            End If
            For Each MN As XmlNode In Match_NODE
                total_hits = total_hits + 1
                strALL = strALL + vbNewLine

                '// HEADER LINE FROM "match" TAG OF RECEIVED XML.
                For n_FOR_MN_INNER = 0 To MN.Attributes.Count - 1
                    If n_FOR_MN_INNER <> 0 Then
                        strALL = strALL + " / "
                    End If
                    strALL = strALL + UCase(MN.Attributes.Item(n_FOR_MN_INNER).Name.ToString)
                    strALL = strALL + " = " + UCase(MN.Attributes.Item(n_FOR_MN_INNER).Value.ToString)
                Next
                '// ALL CHILD NODES OF "match" TAG OF RECEIVED XML.
                For Each CN As XmlNode In MN
                    For i = 0 To CN.ChildNodes.Count - 1
                        If UCase(CN.ChildNodes(i).Name.ToString) <> "#TEXT" Then
                            strALL = strALL + vbNewLine + UCase(CN.ChildNodes(i).Name.ToString)
                            strALL = strALL + "  = " + UCase(CN.ChildNodes(i).InnerText.ToString)
                            bln_WCHIT = True
                        End If
                    Next
                Next
                J = +1
                strALL = strALL + vbNewLine + "#############"
                strALL = strALL + vbNewLine


            Next


            'str_Head = str_Head + "TOTAL HITS = " & total_hits
            'lblCount.Text = str_Head
            'txt_WCHK_AREA.Text = strALL
            'TXT_TOTAL_HITS.Text = total_hits

            'str_Compliance_details = str_Compliance_details & ", Total Hits : " & total_hits
            'txt_compliance_details.Text = str_Compliance_details

            'If CDbl(total_hits) = 0 Then
            '    txt_compliance_details.Text = "DOWJONES CLEARED"
            'End If

            Count = total_hits
            str_Head = str_Head + "TOTAL HITS = " & Count

            Return_string = strALL
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub xml_read_NEW(ByVal html As String, ByVal str_full_name As String, ByRef Return_string As String, ByRef bln_WCHIT As Boolean, ByRef Count As Integer)
        Dim total_hits As Integer = 0
        Dim strALL, strRow As String
        Dim sender_details As String = ""
        Dim str_name As String
        Dim Catgory As String
        Dim country As String
        Dim gender As String
        ' Create the XML document
        Dim doc As New XmlDocument()
        ' Load the document from the string data
        doc.LoadXml(html)
        ' Get a list of all the Dispatch nodes

        Dim Match_NODE_head As XmlNodeList = doc.GetElementsByTagName("head")
        Dim Match_NODE As XmlNodeList = doc.GetElementsByTagName("match")

        Dim str_Head As String = ""

        'TXT_TOTAL_HITS.Text = 0
        'txt_compliance_details.Text = ""

        Dim str_Compliance_details As String = ""

        For Each ND_Match_NODE_head As XmlNode In Match_NODE_head
            'If Len(txtBeneName.Text.Trim) <> 0 Then
            '    sender_details = " ** SENDER DETAILS  ####################################################################################" & vbNewLine & vbNewLine
            'End If

            str_Head = "API VERSION = " & ND_Match_NODE_head.SelectSingleNode("descendant::api-version").InnerText & " , " & ND_Match_NODE_head.SelectSingleNode("descendant::backend-version").InnerText
            str_Head = str_Head + vbNewLine

            str_Head = str_Head + "RESULT ID = " & ND_Match_NODE_head.SelectSingleNode("descendant::cached-results-id").InnerText
            str_Head = str_Head + vbNewLine
            If ND_Match_NODE_head.SelectSingleNode("descendant::total-hits").InnerText <> 0 Then

            End If
            'str_Head = str_Head + "TOTAL HITS = " & ND_Match_NODE_head.SelectSingleNode("descendant::total-hits").InnerText

            str_Compliance_details = "Customer : " & str_full_name & " , RESULT ID : " & ND_Match_NODE_head.SelectSingleNode("descendant::cached-results-id").InnerText


        Next

        'lblCount.Text = str_Head


        Try


            Dim nCount As Integer = 0
            Dim J As Integer = 0
            Dim str_MN_TAG_INSIDE As Integer
            Dim n_FOR_MN_INNER As Integer = 0
            If Len(str_full_name) <> 0 Then
                sender_details = " ** DOW JONES DETAILS   ##################" & vbNewLine & vbNewLine
                strALL = sender_details
            End If
            For Each MN As XmlNode In Match_NODE
                total_hits = total_hits + 1
                strALL = strALL + vbNewLine

                '// HEADER LINE FROM "match" TAG OF RECEIVED XML.
                For n_FOR_MN_INNER = 0 To MN.Attributes.Count - 1
                    If n_FOR_MN_INNER <> 0 Then
                        strALL = strALL + " / "
                    End If
                    strALL = strALL + UCase(MN.Attributes.Item(n_FOR_MN_INNER).Name.ToString)
                    strALL = strALL + " = " + UCase(MN.Attributes.Item(n_FOR_MN_INNER).Value.ToString)
                Next
                '// ALL CHILD NODES OF "match" TAG OF RECEIVED XML.
                For Each CN As XmlNode In MN
                    For i = 0 To CN.ChildNodes.Count - 1
                        If UCase(CN.ChildNodes(i).Name.ToString) <> "#TEXT" Then
                            strALL = strALL + vbNewLine + UCase(CN.ChildNodes(i).Name.ToString)
                            strALL = strALL + "  = " + UCase(CN.ChildNodes(i).InnerText.ToString)
                            bln_WCHIT = True
                        End If
                    Next
                Next
                J = +1
                strALL = strALL + vbNewLine + "#############"
                strALL = strALL + vbNewLine


            Next


            'str_Head = str_Head + "TOTAL HITS = " & total_hits
            'lblCount.Text = str_Head
            'txt_WCHK_AREA.Text = strALL
            'TXT_TOTAL_HITS.Text = total_hits

            'str_Compliance_details = str_Compliance_details & ", Total Hits : " & total_hits
            'txt_compliance_details.Text = str_Compliance_details

            'If CDbl(total_hits) = 0 Then
            '    txt_compliance_details.Text = "DOWJONES CLEARED"
            'End If


            str_Head = str_Head + "TOTAL HITS = " & total_hits
            Count = total_hits
            If bln_WCHIT = True Then
                Return_string = strALL
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Function getGUID() As String
        getGUID = System.Guid.NewGuid.ToString
    End Function
    Public Function ZeroPading(ByVal strPading As String, _
      ByVal padLength As Int32, ByVal vrTyp As String, ByVal strCCShortNAme As String) As String

        Dim dt As Date
        dt = Now.Date
        Dim FY As String = Mid(CStr(dt.Year), 3)


        Return strCCShortNAme & "." & vrTyp & "." & FY & "." & (strPading.PadLeft(padLength, "0"))


    End Function

    Public Function ZeroPading_Simple(ByVal strPading As String, _
       ByVal padLength As Int32) As String

        Return (strPading.PadLeft(padLength, "0"))


    End Function



    Public Function ZeroPading_TT(ByVal strPading As String, _
     ByVal padLength As Int32, ByVal vrTyp As String, ByVal strCCShortNAme As String) As String
        Dim dt As Date
        dt = Now.Date
        Dim FY As String = Mid(CStr(dt.Year), 3)
        Return strCCShortNAme & "." & vrTyp & "." & FY & ".000001"
    End Function

    Public Function ZeroPading_TT_17(ByVal strPading As String, _
   ByVal padLength As Int32, ByVal vrTyp As String, ByVal strCCShortNAme As String) As String
        Dim dt As Date
        dt = Now.Date
        'Dim FY As String = Mid(CStr(17), 3)
        Return strCCShortNAme & "." & vrTyp & "." & 17 & ".000001"
    End Function


    Public Function ZeroPading_NONvch(ByVal strPading As String, _
     ByVal padLength As Int32) As String

        Return (strPading.PadLeft(padLength, "0"))

    End Function


    Public Function isDuplicate(ByVal fldName As String, ByVal tblName As String, ByVal strWhere As String, ByVal gobjCon As SqlClient.SqlConnection) As Boolean
        Dim loComm As New SqlClient.SqlCommand
        Dim loReader As SqlClient.SqlDataReader = Nothing

        Try
            loComm.Connection = gobjCon
            strSql = "Select (" & fldName & ") as ID From " & tblName & " "
            If Len(Trim(strWhere)) <> 0 Then
                strSql = strSql & " WHERE " & strWhere
            End If
            loComm.CommandText = strSql
            loReader = loComm.ExecuteReader
            If loReader.Read Then
                If Not loReader("ID") Is System.DBNull.Value Then
                    isDuplicate = True
                Else
                    isDuplicate = False
                End If
            End If

        Catch ex As Exception
            gERRmsg = ex.Message
        Finally
            loReader.Close()
            loReader = Nothing
            loComm.Dispose()
            loComm = Nothing
        End Try


    End Function
    'Public Sub initCmb(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
    '      ByVal strTable As String, _
    '  ByVal strDisplayField As String, _
    '  ByVal strDataField As String, _
    '  ByVal strWhereCriteria As String, _
    '  ByRef objCn As SqlConnection)


    '    Dim moComm As New SqlClient.SqlCommand

    '    moComm.Connection = objCn
    '    strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria

    '    moComm.CommandText = strSql
    '    Try
    '        Dim dt As DataTable = New DataTable("dt")
    '        Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
    '        sdaAdapter.Fill(dt)
    '        Dim recCount As Int16
    '        cmbBox.Items.Clear()
    '        cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(""))
    '        For recCount = 0 To dt.Rows.Count - 1
    '            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
    '        Next
    '        cmbBox.SelectedIndex = 0
    '        moComm.Dispose()
    '        sdaAdapter.Dispose()
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Public Sub initCmb(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
         ByVal strTable As String, _
     ByVal strDisplayField As String, _
     ByVal strDataField As String, _
     ByVal strWhereCriteria As String)


        Dim moComm As New SqlClient.SqlCommand
        setCon(True)
        moComm.Connection = gCon
        strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria
        '  strSql = strSql + "  order by 1 "

        cmbBox.Items.Clear()
        cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
        moComm.CommandText = strSql
        Try
            Dim dt As DataTable = New DataTable("dt")
            Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
            sdaAdapter.Fill(dt)
            Dim recCount As Int16
            'cmbBox.Items.Clear()
            'cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, -1))
            For recCount = 0 To dt.Rows.Count - 1
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(Left(dt.Rows(recCount)(0), 90), dt.Rows(recCount)(1)))
            Next
            cmbBox.SelectedIndex = 0
            moComm.Dispose()
            sdaAdapter.Dispose()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
        End Try

    End Sub

    

    Public Sub initCmb_auto_ORDER(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
         ByVal strTable As String, _
     ByVal strDisplayField As String, _
     ByVal strDataField As String, _
     ByVal strWhereCriteria As String)


        Dim moComm As New SqlClient.SqlCommand
        setCon(True)
        moComm.Connection = gCon
        strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria
        strSql = strSql + "  order by 1 "

        cmbBox.Items.Clear()
        cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
        moComm.CommandText = strSql
        Try
            Dim dt As DataTable = New DataTable("dt")
            Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
            sdaAdapter.Fill(dt)
            Dim recCount As Int16
            'cmbBox.Items.Clear()
            'cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, -1))
            For recCount = 0 To dt.Rows.Count - 1
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
            Next
            cmbBox.SelectedIndex = 0
            moComm.Dispose()
            sdaAdapter.Dispose()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
        End Try

    End Sub

    Public Sub initCmb_Empty(ByRef cmbBox As System.Web.UI.WebControls.DropDownList)


        Try
            cmbBox.Items.Clear()
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
            cmbBox.SelectedIndex = 0
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Sub




    Public Sub initCmb_For_4_Boxes(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
                                   ByRef cmbBox1 As System.Web.UI.WebControls.DropDownList, _
                                   ByRef cmbBox2 As System.Web.UI.WebControls.DropDownList, _
                                   ByRef cmbBox3 As System.Web.UI.WebControls.DropDownList, _
                                    ByVal strTable As String, _
                                    ByVal strDisplayField As String, _
                                    ByVal strDataField As String, _
                                    ByVal strWhereCriteria As String)


        Dim moComm As New SqlClient.SqlCommand
        setCon(True)
        moComm.Connection = gCon
        strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria

        moComm.CommandText = strSql
        Try
            Dim dt As DataTable = New DataTable("dt")
            Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
            sdaAdapter.Fill(dt)
            Dim recCount As Int16
            cmbBox.Items.Clear()
            cmbBox1.Items.Clear()
            cmbBox2.Items.Clear()
            cmbBox3.Items.Clear()




            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem("--EMPTY--", -1))
            cmbBox1.Items.Add(New System.Web.UI.WebControls.ListItem("--EMPTY--", -1))
            cmbBox2.Items.Add(New System.Web.UI.WebControls.ListItem("--EMPTY--", -1))
            cmbBox3.Items.Add(New System.Web.UI.WebControls.ListItem("--EMPTY--", -1))

            For recCount = 0 To dt.Rows.Count - 1
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
                cmbBox1.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
                cmbBox2.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
                cmbBox3.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
            Next

            cmbBox.SelectedIndex = 0
            cmbBox1.SelectedIndex = 0
            cmbBox2.SelectedIndex = 0
            cmbBox3.SelectedIndex = 0

            moComm.Dispose()
            sdaAdapter.Dispose()
        Catch ex As Exception
        Finally
            setCon(False)
        End Try

    End Sub




    Public Sub initCmb_For_2_Boxes_gstrEMPTY(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
                                 ByRef cmbBox1 As System.Web.UI.WebControls.DropDownList, _
                                  ByVal strTable As String, _
                                  ByVal strDisplayField As String, _
                                  ByVal strDataField As String, _
                                  ByVal strWhereCriteria As String)


        Dim moComm As New SqlClient.SqlCommand
        setCon(True)
        moComm.Connection = gCon
        strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria

        moComm.CommandText = strSql
        Try
            Dim dt As DataTable = New DataTable("dt")
            Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
            sdaAdapter.Fill(dt)
            Dim recCount As Int16
            cmbBox.Items.Clear()
            cmbBox1.Items.Clear()


            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, -1))
            cmbBox1.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, -1))

            For recCount = 0 To dt.Rows.Count - 1
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
                cmbBox1.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))

            Next

            cmbBox.SelectedIndex = 0
            cmbBox1.SelectedIndex = 0


            moComm.Dispose()
            sdaAdapter.Dispose()
        Catch ex As Exception
        Finally
            setCon(False)
        End Try

    End Sub





    Public Sub initCmb_For_2_Boxes(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
                                 ByRef cmbBox1 As System.Web.UI.WebControls.DropDownList, _
                                  ByVal strTable As String, _
                                  ByVal strDisplayField As String, _
                                  ByVal strDataField As String, _
                                  ByVal strWhereCriteria As String)


        Dim moComm As New SqlClient.SqlCommand
        setCon(True)
        moComm.Connection = gCon
        strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria

        moComm.CommandText = strSql
        Try
            Dim dt As DataTable = New DataTable("dt")
            Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
            sdaAdapter.Fill(dt)
            Dim recCount As Int16
            cmbBox.Items.Clear()
            cmbBox1.Items.Clear()


            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem("-- Empty --", -1))
            cmbBox1.Items.Add(New System.Web.UI.WebControls.ListItem("-- Empty --", -1))

            For recCount = 0 To dt.Rows.Count - 1
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
                cmbBox1.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))

            Next

            cmbBox.SelectedIndex = 0
            cmbBox1.SelectedIndex = 0


            moComm.Dispose()
            sdaAdapter.Dispose()
        Catch ex As Exception
        Finally
            setCon(False)
        End Try

    End Sub




    'Public Sub initCmb_For_2_Boxes_gstrEMPTY(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
    '                             ByRef cmbBox1 As System.Web.UI.WebControls.DropDownList, _
    '                              ByVal strTable As String, _
    '                              ByVal strDisplayField As String, _
    '                              ByVal strDataField As String, _
    '                              ByVal strWhereCriteria As String)


    '    Dim moComm As New SqlClient.SqlCommand
    '    setCon(True)
    '    moComm.Connection = gCon
    '    strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria

    '    moComm.CommandText = strSql
    '    Try
    '        Dim dt As DataTable = New DataTable("dt")
    '        Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
    '        sdaAdapter.Fill(dt)
    '        Dim recCount As Int16
    '        cmbBox.Items.Clear()
    '        cmbBox1.Items.Clear()


    '        cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, -1))
    '        cmbBox1.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, -1))

    '        For recCount = 0 To dt.Rows.Count - 1
    '            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
    '            cmbBox1.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))

    '        Next

    '        cmbBox.SelectedIndex = 0
    '        cmbBox1.SelectedIndex = 0


    '        moComm.Dispose()
    '        sdaAdapter.Dispose()
    '    Catch ex As Exception
    '    Finally
    '        setCon(False)
    '    End Try

    'End Sub






    Public Sub initCmb_For_3_Boxes(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
                                 ByRef cmbBox1 As System.Web.UI.WebControls.DropDownList, _
                                   ByRef cmbBox2 As System.Web.UI.WebControls.DropDownList, _
                                  ByVal strTable As String, _
                                  ByVal strDisplayField As String, _
                                  ByVal strDataField As String, _
                                  ByVal strWhereCriteria As String)


        Dim moComm As New SqlClient.SqlCommand
        setCon(True)
        moComm.Connection = gCon
        strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria

        moComm.CommandText = strSql
        Try
            Dim dt As DataTable = New DataTable("dt")
            Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
            sdaAdapter.Fill(dt)
            Dim recCount As Int16
            cmbBox.Items.Clear()
            cmbBox1.Items.Clear()


            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem("--EMPTY--", -1))
            cmbBox1.Items.Add(New System.Web.UI.WebControls.ListItem("--EMPTY--", -1))

            For recCount = 0 To dt.Rows.Count - 1
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
                cmbBox1.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
                cmbBox2.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
            Next

            cmbBox.SelectedIndex = 0
            cmbBox1.SelectedIndex = 0


            moComm.Dispose()
            sdaAdapter.Dispose()
        Catch ex As Exception
        Finally
            setCon(False)
        End Try

    End Sub
    Public Sub initCMB_setText_TEXT_BASED(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
                                                ByVal strTEXT As String)

        Dim str_ACC_EXP As String = ""
        Dim str_X As String = ""
        Dim nDTCD_CMB As Integer = 0
        Dim nSBCD_CMB As Integer = 0
        Dim j As Integer = 0
        Try
            For j = 0 To cmbBox.Items.Count - 1
                If UCase(cmbBox.Items(j).Text) <> modMain.gStr_Empty Then
                    If InStr(cmbBox.Items(j).Text, strTEXT) <> 0 Then
                        cmbBox.SelectedIndex = j
                        Exit For
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Sub initCmb_COA(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
                 ByVal strWhr As String, ByVal dtTb As DataTable)

        Dim i As Integer = 0
        Try

            cmbBox.Items.Clear()

            strSql = strWhr

            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem("--EMPTY--", "9999"))
            For Each tbRow As DataRow In trows
                i += 1
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(tbRow("ACC"), i))
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'dtTb.Dispose()
            'dtTb = Nothing
        End Try

    End Sub


    Public Sub get_SQL_Find_CALL_VRTYPEID_SYS(ByVal STR_enm_vrTypeID_SYS As String, nCCID As Integer, _
                                 ByRef strSql As String, ByRef strSql_Field As String)
        Try

            strSql = "SELECT * FROM ( SELECT        VM.vrNoDisplay_s as TRNO, VM.vrDate as _Date, STS.statusDescription as _Status,  " _
                    & " USR.NAME AS ENTERY_bY,ISNULL(USR_SUP.Name,'') AS SUP_BY,VM.vrno " _
                    & " FROM            tblVchMaster AS VM INNER JOIN " _
                    & " tluTR_VRStatus AS STS ON VM.statusID = STS.statusID LEFT OUTER JOIN " _
                    & " tblUser AS USR_SUP ON VM.SupByID = USR_SUP.UserID_Gid LEFT OUTER JOIN " _
                    & " tblUser AS USR ON VM.UserID_Gid = USR.UserID_Gid " _
                    & " WHERE ( VRTYPEID_SYS IN (" & STR_enm_vrTypeID_SYS & ") AND CCID = " & nCCID & "   ) " _
                    & " )MN"

            strSql_Field = "TRNO,ENTERY_bY , _Date,_Status ,"


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub










    Public Sub get_SQL_Find_CALL_VRTYPEID_SYS_SEARCH(ByVal STR_enm_vrTypeID_SYS As String, nCCID As Integer, _
                                 ByRef strSql As String, ByRef strSql_Field As String, ByRef strSearchBy As String)
        Try

            strSql = "SELECT * FROM ( SELECT        VM.vrNoDisplay_s as TRNO, VM.vrDate as _Date, STS.statusDescription as _Status,  " _
                    & " USR.NAME AS ENTERY_bY,ISNULL(USR_SUP.Name,'') AS SUP_BY,VM.vrno " _
                    & " FROM            tblVchMaster AS VM INNER JOIN " _
                    & " tluTR_VRStatus AS STS ON VM.statusID = STS.statusID LEFT OUTER JOIN " _
                    & " tblUser AS USR_SUP ON VM.SupByID = USR_SUP.UserID_Gid LEFT OUTER JOIN " _
                    & " tblUser AS USR ON VM.UserID_Gid = USR.UserID_Gid " _
                    & " WHERE ( VRTYPEID_SYS IN (" & STR_enm_vrTypeID_SYS & ") AND CCID = " & nCCID & "   ) " _
                       & " AND (  vrNoDisplay_s like '%" & strSearchBy & "%'  OR   statusDescription LIKE '%" & strSearchBy & "%'  OR  USR.NAME LIKE '%" & strSearchBy & "%'    )   " _
                    & " )MN"


            strSql_Field = "TRNO,ENTERY_bY , _Date,_Status ,"


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub




    Public Sub initCmb_COA_DTCDSBCD_CCY_BASE(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
            ByVal strWhr As String, ByVal dtTb As DataTable)

        Dim i As Integer = 0
        Try

            cmbBox.Items.Clear()

            strSql = strWhr
            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem("--EMPTY--", "9999"))
            For Each tbRow As DataRow In trows
                i += 1
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(tbRow("ACC"), i))
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'dtTb.Dispose()
            'dtTb = Nothing
        End Try

    End Sub









    Public Sub initCmb_COA_DTCDSBCD_VAL(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
            ByVal strWhr As String, ByVal dtTb As DataTable)

        Dim i As Integer = 0
        Dim strACCVAL As String = ""
        Try

            cmbBox.Items.Clear()

            strSql = strWhr
            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem("--EMPTY--", "9999"))
            For Each tbRow As DataRow In trows
                i += 1
                strACCVAL = tbRow("dtcd") + "_" + tbRow("sbcd")
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(tbRow("ACC"), strACCVAL))
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'dtTb.Dispose()
            'dtTb = Nothing
        End Try

    End Sub

    Public Sub initCmb_CCID(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
           ByVal strWhr As String, ByRef dtTb As DataTable)


        Dim i As Integer = 0
        cmbBox.Items.Clear()

        strSql = strWhr
        Dim trows As DataRow() = dtTb.Select(strSql)
        cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem("--EMPTY--", "9999"))
        For Each tbRow As DataRow In trows
            i += 1
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(tbRow("CompName"), tbRow("CCID")))
        Next
    End Sub

    Public Sub get_CCID_Codes(ByVal strWhr As String, ByVal dtTb As DataTable, ByRef dtcd_CCID As Integer, ByRef sbCd_CCID As Integer, _
                        ByRef CC_ShortName As String)

        Try


            Dim i As Integer = 0
            strSql = strWhr
            Dim trows As DataRow() = dtTb.Select(strSql)
            For Each tbRow As DataRow In trows
                i += 1
                dtcd_CCID = tbRow("dtcd")
                sbCd_CCID = tbRow("sbcd")
                CC_ShortName = tbRow("companyshortname")
            Next
        Catch ex As Exception
        Finally
            'dtTb.Dispose()
            'dtTb = Nothing
        End Try
    End Sub

    Public Sub get_CCID_Codes_FullName(ByVal strWhr As String, ByVal dtTb As DataTable, ByRef dtcd_CCID As Integer, ByRef sbCd_CCID As Integer, _
                      ByRef CC_ShortName As String, ByRef CC_FullName As String)

        Try


            Dim i As Integer = 0
            strSql = strWhr
            Dim trows As DataRow() = dtTb.Select(strSql)
            For Each tbRow As DataRow In trows
                i += 1
                dtcd_CCID = tbRow("dtcd")
                sbCd_CCID = tbRow("sbcd")
                CC_ShortName = tbRow("companyshortname")
                CC_FullName = tbRow("CompName")
            Next
        Catch ex As Exception
        Finally
            'dtTb.Dispose()
            'dtTb = Nothing
        End Try
    End Sub

    Public Function Execute_Web_TT_Log_WTH_COMM(ByVal moComm As SqlCommand, ByVal nidAUTO As Integer, ByVal strWbTTId As String, ByVal nuserid As Integer, ByVal nstatusid As Integer, ByRef strERR As String) As Boolean

        Try
            setCon(True)
            moComm.Connection = gCon

            moComm.CommandType = CommandType.StoredProcedure
            moComm.CommandText = "dbo.[stp_tbl_WEB_TT_LOG_Insert]"
            moComm.Parameters.Add(New SqlParameter("@idAUTO", SqlDbType.SmallInt, 4, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, nidAUTO))
            moComm.Parameters.Add(New SqlParameter("@sWbTTId", SqlDbType.VarChar, 12, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, strWbTTId))
            moComm.Parameters.Add(New SqlParameter("@iuserid", SqlDbType.SmallInt, 4, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, nuserid))
            moComm.Parameters.Add(New SqlParameter("@istatusid", SqlDbType.SmallInt, 4, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, nstatusid))

            moComm.ExecuteNonQuery()
            Execute_Web_TT_Log_WTH_COMM = True
        Catch ex As Exception
            Execute_Web_TT_Log_WTH_COMM = False
            strERR = ex.Message
        Finally
            'setCon(False)
            moComm.Dispose()
        End Try

    End Function


    Public Function Execute_Web_TT_Log(ByVal nidAUTO As Integer, ByVal strWbTTId As String, ByVal nuserid As Integer, ByVal nstatusid As Integer, ByRef strERR As String) As Boolean
        Dim moComm As New SqlClient.SqlCommand
        Try
            setCon(True)
            moComm.Connection = gCon

            moComm.CommandType = CommandType.StoredProcedure
            moComm.CommandText = "dbo.[stp_tbl_WEB_TT_LOG_Insert]"
            moComm.Parameters.Add(New SqlParameter("@idAUTO", SqlDbType.SmallInt, 4, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, nidAUTO))
            moComm.Parameters.Add(New SqlParameter("@sWbTTId", SqlDbType.VarChar, 12, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, strWbTTId))
            moComm.Parameters.Add(New SqlParameter("@iuserid", SqlDbType.SmallInt, 4, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, nuserid))
            moComm.Parameters.Add(New SqlParameter("@istatusid", SqlDbType.SmallInt, 4, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, nstatusid))

            moComm.ExecuteNonQuery()
            Execute_Web_TT_Log = True
        Catch ex As Exception
            Execute_Web_TT_Log = False
            strERR = ex.Message
        Finally
            'setCon(False)
            moComm.Dispose()
        End Try

    End Function
    Public Sub get_CCID_ShortName(ByVal strWhr As String, ByVal dtTb As DataTable, ByRef CC_ShortName As String)

        Try


            Dim i As Integer = 0
            strSql = strWhr
            Dim trows As DataRow() = dtTb.Select(strSql)
            For Each tbRow As DataRow In trows
                i += 1
                CC_ShortName = tbRow("companyshortname")
            Next
        Catch ex As Exception
        Finally
            'dtTb.Dispose()
            'dtTb = Nothing
        End Try

    End Sub


    Public Sub get_CCID_Report_Values(ByVal strWhr As String, ByRef dtTb As DataTable, _
                                      ByRef str_fmComp As String, ByRef str_fmCompPh As String, _
                                      ByRef str_fmCompAdd As String, ByRef str_fmCompemail As String)

        Dim i As Integer = 0
        strSql = strWhr
        Dim trows As DataRow() = dtTb.Select(strSql)
        For Each tbRow As DataRow In trows

            str_fmComp = tbRow("rptCompTitle")
            str_fmCompPh = tbRow("rptCompPO_Ph_Fax")
            str_fmCompAdd = tbRow("CompAdd")
            str_fmCompemail = tbRow("CompEmail")
        Next
    End Sub





    Public Sub initCmb_CCY_SELECT_BY(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
        ByVal dtTb As DataTable, Optional ByVal strSEL As String = "")
        Dim strTitle As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()


            'strSql = " isBaseCurrency = 'FALSE' AND isActive = 'TRUE'  "
            strSql = strSEL ' " isBaseCurrency = 'FALSE' AND isActive = 'TRUE'  "

            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
            For Each tbRow As DataRow In trows
                ' If tbRow("isactive") Then
                strTitle = tbRow("CurrencyShortName") & "   - " & tbRow("CurrencyName")
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(strTitle, tbRow("CurrencyID")))
                ' End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing

        End Try

    End Sub



    Public Sub initCmb_CCY_JV(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
        ByVal dtTb As DataTable, Optional ByVal isBaseCurrency_needed As Boolean = False)
        Dim strTitle As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()
            strSql = " CurrencyShortName in ('MYR','USD','GBP','EUR','CYN')"


            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
            For Each tbRow As DataRow In trows
                ' If tbRow("isactive") Then
                strTitle = tbRow("CurrencyShortName") & "   - " & tbRow("CurrencyName")
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(strTitle, tbRow("CurrencyID")))
                ' End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing

        End Try

    End Sub


    Public Sub initCmb_CCY(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
        ByVal dtTb As DataTable, Optional ByVal isBaseCurrency_needed As Boolean = False)
        Dim strTitle As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()
            strSql = " isActive = 'TRUE' "
            If Not isBaseCurrency_needed Then
                strSql = " isBaseCurrency = 'FALSE' AND isActive = 'TRUE'  "
            End If

            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
            For Each tbRow As DataRow In trows
                ' If tbRow("isactive") Then
                strTitle = tbRow("CurrencyShortName") & "   - " & tbRow("CurrencyName")
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(strTitle, tbRow("CurrencyID")))
                ' End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing

        End Try

    End Sub


    Public Sub initCmb_CCY_FWD_CCY(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
        ByVal dtTb As DataTable, Optional ByVal isBaseCurrency_needed As Boolean = False)
        Dim strTitle As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()
            strSql = " isFWD_CCY='TRUE'  AND  isActive = 'TRUE' "
            If Not isBaseCurrency_needed Then
                strSql = strSql & "  AND  isBaseCurrency = 'FALSE'  "
            End If

            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
            For Each tbRow As DataRow In trows
                ' If tbRow("isactive") Then
                strTitle = tbRow("CurrencyShortName") & "   - " & tbRow("CurrencyName")
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(strTitle, tbRow("CurrencyID")))
                ' End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing

        End Try

    End Sub


    Public Sub initCmb_CCY_PAYORDER_CCY(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
        ByVal dtTb As DataTable, Optional ByVal isBaseCurrency_needed As Boolean = False)
        Dim strTitle As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()
            strSql = " CURRENCYID IN (5,21,55,66,43,42,63,64)  AND  isActive = 'TRUE' "
            If Not isBaseCurrency_needed Then
                strSql = strSql
            End If

            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
            For Each tbRow As DataRow In trows
                ' If tbRow("isactive") Then
                strTitle = tbRow("CurrencyShortName") & "   - " & tbRow("CurrencyName")
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(strTitle, tbRow("CurrencyID")))
                ' End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing

        End Try

    End Sub
    Public Sub initCmb_CCY_BASE_ONLY(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
        ByVal dtTb As DataTable)
        Dim strTitle As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()
            strSql = " isBaseCurrency = 'TRUE' "


            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
            For Each tbRow As DataRow In trows
                ' If tbRow("isactive") Then
                strTitle = tbRow("CurrencyShortName") & "   - " & tbRow("CurrencyName")
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(strTitle, tbRow("CurrencyID")))
                ' End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing

        End Try

    End Sub





    Public Sub initCmb_CCY_BASE_USD_ONLY(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
        ByVal dtTb As DataTable)
        Dim strTitle As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()
            strSql = " isBaseCurrency = 'TRUE' or currencyid = 64"


            Dim trows As DataRow() = dtTb.Select(strSql)
            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
            For Each tbRow As DataRow In trows
                ' If tbRow("isactive") Then
                strTitle = tbRow("CurrencyShortName") & "   - " & tbRow("CurrencyName")
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(strTitle, tbRow("CurrencyID")))
                ' End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing

        End Try

    End Sub


    Public Sub initCmb_CCY_PARTY(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
        ByVal dtTb As DataTable, ByVal isRo As Boolean)

        Dim strCCY As String = ""
        Dim StrCCY_EX As String = ""
        Try
            Dim i As Integer = 0
            cmbBox.Items.Clear()


            Dim strSql As String = ""

            If isRo Then
                strSql = "isACTIVE = 'TRUE'  AND isRO = 'TRUE'  "
            End If
            If Not isRo Then
                strSql = "isACTIVE = 'TRUE'  AND isRi = 'TRUE'  "
            End If

            Dim trows As DataRow() = dtTb.Select(strSql, "CCY ASC")

            cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
            For Each tbRow As DataRow In trows
                strCCY = tbRow("CCY")
                If strCCY <> StrCCY_EX Then

                    cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(strCCY, tbRow("CCYID")))
                    StrCCY_EX = strCCY
                End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
 

    Public Function getStauts_VCH_BYREFNO(ByVal sVrno As String) As Integer
        Dim moComm As New SqlClient.SqlCommand
        Dim nStatusID As Integer = 0
        Try
            setCon(True)
            moComm.Connection = gCon
            moComm.CommandType = CommandType.Text
            moComm.CommandText = "Select StatusID from tblvchmaster where refvrno = " & sVrno & " "
            nStatusID = moComm.ExecuteScalar()

        Catch ex As Exception
            nStatusID = 0
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
            moComm.Dispose()
        End Try
        getStauts_VCH_BYREFNO = nStatusID
    End Function


    Public Function getStauts_VCH(ByVal sVrno As String) As Integer
        Dim moComm As New SqlClient.SqlCommand
        Dim nStatusID As Integer = 0
        Try
            setCon(True)
            moComm.Connection = gCon
            moComm.CommandType = CommandType.Text
            moComm.CommandText = "Select StatusID from tblvchmaster where vrno = '" & sVrno & "' "
            nStatusID = moComm.ExecuteScalar()

        Catch ex As Exception
            nStatusID = 0
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
            moComm.Dispose()
        End Try
        getStauts_VCH = nStatusID
    End Function


    Public Function getStauts_VCH_WEB(ByVal sVrno As String) As Integer
        Dim moComm As New SqlClient.SqlCommand
        Dim nStatusID As Integer = 0
        Try
            setCon(True)
            moComm.Connection = gCon
            moComm.CommandType = CommandType.Text
            moComm.CommandText = "Select StatusID from tbl_TT_App_input_WEB where WbTTId_gid = '" & sVrno & "' "
            nStatusID = moComm.ExecuteScalar()

        Catch ex As Exception
            nStatusID = 0
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
            moComm.Dispose()
        End Try
        getStauts_VCH_WEB = nStatusID
    End Function


    Public Sub getACC_BAL_LC(ByVal nDTCD As Integer, ByVal nSBCD As Integer, ByVal nAmt_TO_DR As Double)

        'Dim dtTb As New DataTable
        'Dim nAMT_DB As Double = 0
        'Try

        '    Dim objPara(2) As clsPara
        '    setPara(objPara(0), "endDate", DateSerial_SFTX(Now.Year, Now.Month, Now.Day), "", clsPara.ColType.dt)
        '    setPara(objPara(1), "DTCD", nDTCD, "", clsPara.ColType.Int)
        '    setPara(objPara(2), "SBCD", nSBCD, "", clsPara.ColType.Int)


        '    dtTb = DTTB_Fill_Generic("stp_TLParty_DS_LC_CR_LIMIT", objPara)
        '    If dtTb.Rows.Count <> 0 Then
        '        nAMT_DB = (dtTb.Rows(i).Item("AMT"))
        '    End If
        '    If nAMT_DB >= 0 Then
        '        Throw New Exception("")
        '    End If

        '    If (nAMT_DB + nAmt_TO_DR) > 0 Then
        '        Throw New Exception("")
        '    End If

        'Catch ex As Exception
        '    Throw New Exception("The " & nAmt_TO_DR & "/LC ,  Amount exceeded then the Limit")
        'Finally
        '    dtTb = Nothing
        'End Try

    End Sub

    Public Sub GET_ACC_BALANCE_CCY_BASED(ByVal strACC As String, _
                                       ByVal nCCYID As Integer, _
                                       ByRef nAMT_BAL As Double, ByRef nRate As Double)

        Dim dtTb_data As New DataTable
        Dim objPara(3) As clsPara
        Dim o_dtDateFrom As Date

        Dim nDTCD As Integer = 0
        Dim nSBCD As Integer = 0

        Dim objFN As New modFunctions
        Dim str_ACC_Name As String = ""

        Try
            getACC_DTCD(strACC, nDTCD, nSBCD, str_ACC_Name)

            getMMDDYYYY(Now.Date, o_dtDateFrom)
            Dim strDTFrom = o_dtDateFrom

            setPara(objPara(0), "endDate", strDTFrom, "", clsPara.ColType.dt)
            setPara(objPara(1), "CCYID", nCCYID, "", clsPara.ColType.Int)
            setPara(objPara(2), "DTCD", nDTCD, "", clsPara.ColType.Int)
            setPara(objPara(3), "SBCD", nSBCD, "", clsPara.ColType.Int)

            dtTb_data = DTTB_Fill_Generic("stp_BANK_CCY_POSITION", objPara)

            Dim I As Integer = 0
            If dtTb_data.Rows.Count <> 0 Then
                nAMT_BAL = objFN.isValid_NBR_NEGATIVE(dtTb_data.Rows(I).Item("CLS_FC"))
                nRate = objFN.isValid_NBR(dtTb_data.Rows(I).Item("CLS_RATE"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            dtTb_data = Nothing
            objFN = Nothing
        End Try
    End Sub

    Public Sub getACC_BAL_LC_BALANCE(ByVal nDTCD As Integer, ByVal nSBCD As Integer, ByRef nAMT_DB As Double)

        Dim dtTb As New DataTable

        Try
            nAMT_DB = 0
            Dim objPara(2) As clsPara
            setPara(objPara(0), "endDate", DateSerial_SFTX(Now.Year, Now.Month, Now.Day), "", clsPara.ColType.dt)
            setPara(objPara(1), "DTCD", nDTCD, "", clsPara.ColType.Int)
            setPara(objPara(2), "SBCD", nSBCD, "", clsPara.ColType.Int)


            dtTb = DTTB_Fill_Generic("stp_TLParty_DS_LC_CR_LIMIT", objPara)
            If dtTb.Rows.Count <> 0 Then
                nAMT_DB = (dtTb.Rows(i).Item("AMT"))
            End If


        Catch ex As Exception
            Throw New Exception("Please contact to system admin")
        Finally
            dtTb = Nothing
        End Try

    End Sub







    Public Function getStauts_VCH_tt_app(ByVal sVrno As String) As Integer
        Dim moComm As New SqlClient.SqlCommand
        Dim nStatusID As Integer = 0
        Try
            setCon(True)
            moComm.Connection = gCon
            moComm.CommandType = CommandType.Text
            moComm.CommandText = "Select StatusID from tbl_TT_App_input where TT_App_input_gid = '" & sVrno & "' "
            nStatusID = moComm.ExecuteScalar()

        Catch ex As Exception
            nStatusID = 0
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
            moComm.Dispose()
        End Try
        getStauts_VCH_tt_app = nStatusID
    End Function



    Public Function getStauts_VCH_tt_app_RI(ByVal sVrno As String) As Integer
        Dim moComm As New SqlClient.SqlCommand
        Dim nStatusID As Integer = 0
        Try
            setCon(True)
            moComm.Connection = gCon
            moComm.CommandType = CommandType.Text
            moComm.CommandText = "Select StatusID from tbl_TT_App_input_RI where TT_App_input_gid_RI = '" & sVrno & "' "
            nStatusID = moComm.ExecuteScalar()

        Catch ex As Exception
            nStatusID = 0
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
            moComm.Dispose()
        End Try
        getStauts_VCH_tt_app_RI = nStatusID

    End Function
    Public Sub getCCY_Stock_TELLER(ByVal nCCID As String, _
                            ByVal strUSERID_GID As String, _
                            ByVal strCCY As String, _
                            ByVal nStock_APPliend As Double, _
                            ByRef isValid As Boolean)


        Try
            '**************************************************  

            '-----------------------------------------------
            Dim o_dtDateFrom As Date
            Dim o_dtDateTO As Date

            Dim strDT As String = Format(DateSerial(Date.Now.Year, 1, 1), "dd/MM/yyyy")
            Dim endDT As String = Format(Now.Date, "dd/MM/yyyy")
            getMMDDYYYY(strDT, o_dtDateFrom)
            getMMDDYYYY(endDT, o_dtDateTO)



            '**************************************************
            Dim strDTFrom = o_dtDateFrom
            Dim strDTEnd = o_dtDateTO
            '**************************************************
            '**************************************************
            Dim dtTB As New DataTable
            Dim objPara(3) As clsPara
            setPara(objPara(0), "endDate", o_dtDateTO, "", clsPara.ColType.dt)
            setPara(objPara(1), "UserID_GID", strUSERID_GID, "50", clsPara.ColType.Varchar)
            setPara(objPara(2), "CCID", nCCID, "", clsPara.ColType.Int)
            setPara(objPara(3), "CCYID", 0, "", clsPara.ColType.Int)


            dtTB = DTTB_Fill_Generic("stp_rptStkCurr_Teller", objPara)

            isValid = False
            Dim nSTK_DB As Double = 0
            Dim j As Integer = 0
            For j = 0 To dtTB.Rows.Count - 1
                If dtTB.Rows(j).Item("CurrencyShortName") = strCCY Then
                    nSTK_DB = (CDbl(dtTB.Rows(j).Item("DR")) - CDbl(dtTB.Rows(j).Item("CR")))
                    If nSTK_DB < nStock_APPliend Then
                        isValid = False
                    Else
                        isValid = True
                    End If
                End If
            Next



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try



    End Sub



    Public Sub getCCY_Stock_TELLER_NW(ByVal nCCID As String, _
                            ByVal strUSERID_GID As String, _
                            ByVal strCCY As String, _
                            ByVal nStock_APPliend As Double, _
                            ByRef isValid As Boolean)


        Try
            '**************************************************  
            '**************************************************
            Dim dtTB As New DataTable
            Dim objPara(2) As clsPara

            setPara(objPara(0), "UserID_GID", strUSERID_GID, "50", clsPara.ColType.Varchar)
            setPara(objPara(1), "CCID", nCCID, "", clsPara.ColType.Int)
            setPara(objPara(2), "CCYID", 0, "", clsPara.ColType.Int)


            dtTB = DTTB_Fill_Generic("stp_rptStkCurr_Teller_dtNw", objPara)
            Dim nBAL As Double = 0
            isValid = False
            Dim nSTK_DB As Double = 0
            Dim j As Integer = 0
            For j = 0 To dtTB.Rows.Count - 1
                If UCase(Trim(dtTB.Rows(j).Item("CurrencyShortName"))) = UCase(Trim(strCCY)) Then
                    nSTK_DB = (CDbl(dtTB.Rows(j).Item("DR")) - CDbl(dtTB.Rows(j).Item("CR")))

                    'If nSTK_DB < nStock_APPliend Then
                    '    isValid = False
                    'Else
                    '    isValid = True
                    'End If

                    nBAL = nSTK_DB - nStock_APPliend

                    If nBAL < -0.5 Then
                        isValid = False
                    Else
                        isValid = True
                    End If
                End If
            Next



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try



    End Sub


    Public Sub get_TAX_DETAILS(ByVal DTTB_TAX As DataTable, ByVal nTAX_ID As Integer, _
                            ByRef SAL_TAX_CODE As Double, _
                            ByRef SAL_TAX_NUMBER As String, _
                           ByRef SAL_tAX_AUX_1 As String, _
                           ByRef dtcd As Integer, _
                           ByRef sbcd As Integer)



        Try


            For Each mRW As DataRow In DTTB_TAX.Rows
                If nTAX_ID = CDbl(mRW("SAL_TAX_ID").ToString) Then
                    SAL_TAX_CODE = mRW("SAL_TAX_CODE")
                    SAL_TAX_NUMBER = mRW("SAL_TAX_NUMBER")
                    SAL_tAX_AUX_1 = mRW("SAL_tAX_AUX_1")
                    dtcd = mRW("dtcd")
                    sbcd = mRW("sbcd")
                End If

            Next


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            DTTB_TAX = Nothing
        End Try
    End Sub
    Public Sub getCCY_Stock_TELLER_BALANCE(ByVal nCCID As String, _
                          ByVal strUSERID_GID As String, _
                          ByVal strCCY As String, _
                          ByRef nStock As Double)


        Try
            '**************************************************  
            '**************************************************
            Dim dtTB As New DataTable
            Dim objPara(2) As clsPara

            setPara(objPara(0), "UserID_GID", strUSERID_GID, "50", clsPara.ColType.Varchar)
            setPara(objPara(1), "CCID", nCCID, "", clsPara.ColType.Int)
            setPara(objPara(2), "CCYID", 0, "", clsPara.ColType.Int)


            dtTB = DTTB_Fill_Generic("stp_rptStkCurr_Teller_dtNw", objPara)


            nStock = 0
            Dim j As Integer = 0
            For j = 0 To dtTB.Rows.Count - 1
                If UCase(Trim(dtTB.Rows(j).Item("CurrencyShortName"))) = UCase(Trim(strCCY)) Then
                    nStock = (CDbl(dtTB.Rows(j).Item("DR")) - CDbl(dtTB.Rows(j).Item("CR")))
                End If
            Next


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try



    End Sub

    Public Function getStauts_2_VCH(ByVal sVrno As String) As Integer
        Dim moComm As New SqlClient.SqlCommand
        Dim nStatusID As Integer = 0
        Try
            setCon(True)
            moComm.Connection = gCon
            moComm.CommandType = CommandType.Text
            moComm.CommandText = "Select StatusID_2 from tblvchmaster where vrno = '" & sVrno & "' "
            nStatusID = moComm.ExecuteScalar()

        Catch ex As Exception
            nStatusID = 0
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
            moComm.Dispose()
            getStauts_2_VCH = nStatusID
        End Try

    End Function


    Public Function getRule_Log_RO_STS(ByVal sVrno As String) As Integer
        Dim moComm As New SqlClient.SqlCommand
        Dim nStatusID As Integer = 0
        Try
            setCon(True)
            moComm.Connection = gCon
            moComm.CommandType = CommandType.Text
            moComm.CommandText = "Select Rule_Log_clear from tbl_TT_App_input where TT_App_input_gid = '" & sVrno & "' "
            nStatusID = moComm.ExecuteScalar()

        Catch ex As Exception
            nStatusID = 0
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
            moComm.Dispose()
            getRule_Log_RO_STS = nStatusID
        End Try

    End Function




    Public Sub getVrtypeID(ByVal sVrno As String, _
                                     ByRef nVrTypeid As Integer)

        Dim dttb As New DataTable
        Try

            dttb = DTTB_Fill("Select vrtypeid from tblvchmaster where vrno = '" & sVrno & "' ")
            nVrTypeid = dttb(0)("vrtypeid")

        Catch ex As Exception

            Throw New Exception(ex.Message)
        Finally
            dttb.Dispose()
            dttb = Nothing

        End Try

    End Sub


    Public Sub initChkList_With_DataTable(ByRef CheckBoxList As System.Web.UI.WebControls.CheckBoxList, _
      ByVal strTable As String, _
  ByVal strDisplayField As String, _
  ByVal strDataField As String, _
  ByVal strWhereCriteria As String, _
  ByRef dtData As DataTable)


        Dim moComm As New SqlClient.SqlCommand
        setCon(True)
        moComm.Connection = gCon
        strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria
        '  strSql = strSql + "  order by 1 "

        CheckBoxList.Items.Clear()
        moComm.CommandText = strSql
        Try
            Dim dt As DataTable = New DataTable("dt")
            Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
            sdaAdapter.Fill(dt)
            Dim recCount As Int16
            'cmbBox.Items.Clear()
            'cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, -1))
            For recCount = 0 To dt.Rows.Count - 1
                CheckBoxList.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
            Next
            dtData = dt
            moComm.Dispose()
            sdaAdapter.Dispose()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
        End Try

    End Sub

    Public Sub initChkList(ByRef CheckBoxList As System.Web.UI.WebControls.CheckBoxList, _
       ByVal strTable As String, _
   ByVal strDisplayField As String, _
   ByVal strDataField As String, _
   ByVal strWhereCriteria As String)


        Dim moComm As New SqlClient.SqlCommand
        setCon(True)
        moComm.Connection = gCon
        strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria
        '  strSql = strSql + "  order by 1 "

        CheckBoxList.Items.Clear()
        moComm.CommandText = strSql
        Try
            Dim dt As DataTable = New DataTable("dt")
            Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
            sdaAdapter.Fill(dt)
            Dim recCount As Int16
            'cmbBox.Items.Clear()
            'cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, -1))
            For recCount = 0 To dt.Rows.Count - 1
                CheckBoxList.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
            Next

            moComm.Dispose()
            sdaAdapter.Dispose()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
        End Try

    End Sub

    


    Public Function DTTB_Fill(ByVal sTxt As String, ByRef gCon As SqlConnection) As DataTable

        Dim loComm As New SqlClient.SqlCommand
        Dim sdaAdapter As SqlDataAdapter
        Dim dtTb As DataTable = New DataTable("tb")
        Try

            loComm.Connection = gCon
            sdaAdapter = New SqlDataAdapter(loComm)
            loComm.CommandType = CommandType.Text
            loComm.CommandText = sTxt
            sdaAdapter.Fill(dtTb)
            DTTB_Fill = dtTb
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally


            loComm.Dispose()
            sdaAdapter = Nothing

        End Try

    End Function
    Public Sub saveVchMaster_Fun_vat(ByRef loComm As SqlCommand, ByVal svrNo As String, _
                               ByVal ivrNoDisplay As String, _
                               ByVal svrNoDisplay_s As String, _
                               ByVal sinvoiceNo As String, _
                               ByVal davrDate As Date, _
                               ByVal byvrTypeID As Int16, _
                               ByVal iStatusID As Integer, _
                               ByVal siPrincipleCurrID As Int32, _
                               ByVal sMasterNarration As String, _
                               ByVal iDetailCodeID As Integer, _
                               ByVal iSubCodeID As Integer, _
                               ByVal fPrincipleRate As Double, _
                               ByVal sCheque_DD_TT As String, _
                               ByVal daChequeDate As Date, _
                               ByVal Bvch_PayReceive As String, _
                               ByVal Bvch_Reference As String, _
                               ByVal RevREFNo As Int32, _
                               ByVal iBeneID As Long, _
                               ByVal iCustID As Long, _
                               ByVal isChkCanceled As Boolean, _
                               ByVal isDenomination As Boolean, _
                               ByVal sREFvrNo As String, _
                               ByVal byvrTypeID_sys As Int16, _
                               ByVal sPOSTitle As String, _
                               ByVal sPOSCardID As String, _
                               ByVal fPOSBankComm As Double, _
                               ByVal fPOSExchComm As Double, _
                               ByVal itrnstRef_DtCd As Integer, _
                               ByVal itrnstRef_SbCd As Integer, _
                               ByVal ttValueType As String, _
                               ByVal iBranchID As Integer, _
                               ByVal isPayByAcc As Boolean, _
                               ByVal str_CshByID As String, _
                               ByVal IsUpdate As Boolean, _
                               ByVal ttComm As Double, _
                               ByVal ttCCY As Integer, _
                               ByVal ttRate As Double, _
                               ByVal ttFC As Double, _
                               ByVal ttLC As Double, _
                               ByVal Custid_Details As Long, _
                               ByVal Rep_ID As Long, _
                               ByVal nUSERID As Integer, _
                               ByVal nCCID As Integer, _
                               ByVal sUSERID_GID As String, _
                               Optional ByVal isRevEntry As Boolean = 0, _
                               Optional ByVal sREF_TrNo As String = "0", _
                               Optional ByVal bisDASP_BalanceAmount As Boolean = 0, _
                               Optional ByVal iCCID_Posting As Integer = 0, _
                               Optional ByVal Aux_1 As String = "", _
                               Optional ByVal Aux_2 As String = "", _
                               Optional ByVal Aux_3 As String = "", _
                               Optional ByVal Aux_4 As String = "", _
                               Optional ByVal Aux_5 As String = "", _
                               Optional ByVal Aux_6 As String = "", _
                               Optional ByVal Aux_7 As String = "", _
                               Optional ByVal Aux_8 As String = "", _
                               Optional ByVal Aux_9 As String = "", _
                               Optional ByVal Aux_10 As String = "", _
                               Optional ByVal Aux_i_1 As Integer = 0, _
                               Optional ByVal Aux_i_2 As Integer = 0, _
                               Optional ByVal Aux_i_3 As Integer = 0, _
                               Optional ByVal Aux_i_4 As Integer = 0, _
                               Optional ByVal Aux_f_1 As Double = 0, _
                               Optional ByVal Aux_f_2 As Double = 0, _
                               Optional ByVal Aux_f_3 As Double = 0, _
                               Optional ByVal Aux_f_4 As Double = 0, _
                               Optional ByVal Aux_b_1 As Boolean = 0, _
                               Optional ByVal Aux_b_2 As Boolean = 0, _
                               Optional ByVal Aux_b_3 As Boolean = 0, _
                               Optional ByVal Aux_b_4 As Boolean = 0, _
                               Optional ByVal Cust_ID_GID As String = "", _
                               Optional ByVal custid_detail_GID As String = "", _
                               Optional ByVal cust_CCID As Integer = 1, _
                               Optional ByVal statusid_2 As Integer = 1, _
                               Optional ByRef str_svrNoDisplay_s_ot As String = "", _
                               Optional ByVal Aux_1_big As String = "", _
                               Optional ByVal Aux_2_big As String = "", _
                               Optional ByVal Aux_3_big As String = "", _
                               Optional ByVal Aux_4_big As String = "", _
                               Optional ByVal Aux_5_big As String = "", _
                               Optional ByVal Aux_6_big As String = "")



        svrNo = UCase(svrNo)
        ivrNoDisplay = UCase(ivrNoDisplay)
        svrNoDisplay_s = UCase(svrNoDisplay_s)
        sinvoiceNo = UCase(sinvoiceNo)
        sMasterNarration = UCase(sMasterNarration)
        'sMasterNarration = Replace(sMasterNarration, "&nbsp;", ".")


        sCheque_DD_TT = UCase(sCheque_DD_TT)
        Bvch_PayReceive = UCase(Bvch_PayReceive)
        'Bvch_PayReceive = Replace(Bvch_PayReceive, "&nbsp;", ".")

        Bvch_Reference = UCase(Bvch_Reference)
        'Bvch_Reference = Replace(Bvch_Reference, "&nbsp;", ".")

        sREFvrNo = UCase(sREFvrNo)
        sPOSTitle = UCase(sPOSTitle)
        sPOSCardID = UCase(sPOSCardID)
        ttValueType = UCase(ttValueType)
        str_CshByID = UCase(str_CshByID)
        sPOSTitle = UCase(sPOSTitle)
        sPOSCardID = UCase(sPOSCardID)
        ttValueType = UCase(ttValueType)

        If Len(sMasterNarration) > 100 Then
            sMasterNarration = Mid(sMasterNarration, 1, 99)
        End If
        'Dim nAccountType As Integer
        'Dim loReader As SqlClient.SqlDataReader
        'Dim strTrID As String


        '---------------------------------------------------
        '***************************************************
        'If iCCID_Posting <> goCCID_USER Then
        '    MsgBox("USER CAN DO ENTRY ONLY IN THE DEFAULT BRANCH ")
        '    Throw New Exception("USER CAN DO ENTRY ONLY IN THE DEFAULT BRANCH ......")
        '    Exit Sub
        'End If
        'If DateSerial_SFTX(gREV_DATE_MAX.Year, gREV_DATE_MAX.Month, gREV_DATE_MAX.Day) >= DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day) Then
        '    MsgBox("DATA IS LOCKED --- CAN NOT SAVE/UPDATE ....")
        '    Throw New Exception("System is unable to save. please contact to system administrator...")
        '    Exit Sub
        'End If
        '***************************************************
        '---------------------------------------------------

        'If DateSerial_SFTX(gREV_DATE_MAX.Year, gREV_DATE_MAX.Month, gREV_DATE_MAX.Day) >= DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day) Then
        '    MsgBox("DATA IS LOCKED --- CAN NOT SAVE/UPDATE ....")
        '    Throw New Exception("System is unable to save. please contact to system administrator...")
        '    Exit Sub
        'End If


        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        loComm.CommandText = "stp_tblVchMaster_GID_vat"


        With loComm.Parameters

            .Add(New SqlParameter("@svrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, svrNo))

            .Add(New SqlParameter("@ivrNoDisplay", SqlDbType.Int, 4, ParameterDirection.Input, _
             False, 10, 0, "", DataRowVersion.Proposed, CDbl(split_ID(ivrNoDisplay))))

            .Add(New SqlParameter("@svrNoDisplay_s", SqlDbType.VarChar, 20, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, svrNoDisplay_s))

            .Add(New SqlParameter("@sinvoiceNo", SqlDbType.VarChar, 30, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sinvoiceNo & ""))

            .Add(New SqlParameter("@davrDate", SqlDbType.DateTime, 8, ParameterDirection.Input, _
            False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day)))

            .Add(New SqlParameter("@byvrTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, _
            False, 3, 0, "", DataRowVersion.Proposed, byvrTypeID))

            .Add(New SqlParameter("@istatusID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iStatusID))

            .Add(New SqlParameter("@siPrincipleCurrID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
            False, 5, 0, "", DataRowVersion.Proposed, IIf(siPrincipleCurrID = 0, SqlTypes.SqlInt16.Null, siPrincipleCurrID)))

            .Add(New SqlParameter("@sMasterNarration", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sMasterNarration))

            .Add(New SqlParameter("@iDetailCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iDetailCodeID))

            .Add(New SqlParameter("@iSubCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iSubCodeID))

            .Add(New SqlParameter("@fPrincipleRate", SqlDbType.Float, 8, ParameterDirection.Input, _
            False, 38, 0, "", DataRowVersion.Proposed, fPrincipleRate))

            .Add(New SqlParameter("@sCheque_DD_TT", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sCheque_DD_TT & ""))

            .Add(New SqlParameter("@daChequeDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
            False, 16, 0, "", DataRowVersion.Proposed, DateSerial_SFTX(daChequeDate.Year, daChequeDate.Month, daChequeDate.Day)))

            .Add(New SqlParameter("@Bvch_PayReceive", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, Bvch_PayReceive & ""))

            .Add(New SqlParameter("@Bvch_Reference", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, Bvch_Reference & ""))

            .Add(New SqlParameter("@RevREFNo", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, IIf(RevREFNo = 0, System.DBNull.Value, RevREFNo)))

            .Add(New SqlParameter("@iBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(iBeneID = 0, System.DBNull.Value, iBeneID)))

            .Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(iCustID = 0, System.DBNull.Value, iCustID)))

            .Add(New SqlParameter("@isChkCanceled", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, isChkCanceled))
            .Add(New SqlParameter("@isDenomination", SqlDbType.Bit, 1, ParameterDirection.Input, _
                          False, 1, 0, "", DataRowVersion.Proposed, isDenomination))


            .Add(New SqlParameter("@sREFvrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, _
           False, 0, 0, "", DataRowVersion.Proposed, IIf(Len(Trim(sREFvrNo)) = 0, System.DBNull.Value, sREFvrNo)))

            .Add(New SqlParameter("@byvrTypeID_sys", SqlDbType.TinyInt, 1, ParameterDirection.Input, _
                        False, 3, 0, "", DataRowVersion.Proposed, IIf(byvrTypeID_sys = 0, System.DBNull.Value, byvrTypeID_sys)))

            .Add(New SqlParameter("@sPOSTitle", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sPOSTitle))
            .Add(New SqlParameter("@sPOSCardID", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, sPOSCardID))


            .Add(New SqlParameter("@fPOSBankComm", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, fPOSBankComm))
            .Add(New SqlParameter("@fPOSExchComm", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, fPOSExchComm))


            .Add(New SqlParameter("@itrnstRef_DtCd", SqlDbType.Int, 4, ParameterDirection.Input, _
             False, 10, 0, "", DataRowVersion.Proposed, IIf(itrnstRef_DtCd = 0, System.DBNull.Value, itrnstRef_DtCd)))
            .Add(New SqlParameter("@itrnstRef_SbCd", SqlDbType.Int, 4, ParameterDirection.Input, _
                        False, 10, 0, "", DataRowVersion.Proposed, IIf(itrnstRef_SbCd = 0, System.DBNull.Value, itrnstRef_SbCd)))


            .Add(New SqlParameter("@sttValueType", SqlDbType.VarChar, 30, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, ttValueType))


            .Add(New SqlParameter("@iUserID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, nUSERID))


            .Add(New SqlParameter("@iCCID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, nCCID))


            .Add(New SqlParameter("@isPayByAcc", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, isPayByAcc))

            .Add(New SqlParameter("@isRevEntry", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, isRevEntry))


            .Add(New SqlParameter("@sREF_TrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sREF_TrNo))

            .Add(New SqlParameter("@isDASP_BalanceAmount", SqlDbType.Bit, 1, ParameterDirection.Input, _
                     False, 1, 0, "", DataRowVersion.Proposed, bisDASP_BalanceAmount))

            '.Add(New SqlParameter("@iCshByID", SqlDbType.Int, 4, ParameterDirection.Input, _
            '           False, 10, 0, "", DataRowVersion.Proposed, iCshByID))


            .Add(New SqlParameter("@iCshByID", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, str_CshByID))


            .Add(New SqlParameter("@iCCID_Posting", SqlDbType.Int, 4, ParameterDirection.Input, _
                                   False, 10, 0, "", DataRowVersion.Proposed, iCCID_Posting))



            .Add(New SqlParameter("@ttComm", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttComm))

            .Add(New SqlParameter("@ttCCY", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
            False, 5, 0, "", DataRowVersion.Proposed, ttCCY))

            .Add(New SqlParameter("@ttRate", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttRate))
            .Add(New SqlParameter("@ttFC", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttFC))
            .Add(New SqlParameter("@ttLC", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttLC))


            .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(Custid_Details = 0, System.DBNull.Value, Custid_Details)))


            .Add(New SqlParameter("@Rep_ID", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(Rep_ID = 0, System.DBNull.Value, Rep_ID)))





            .Add(New SqlParameter("@BlockID", SqlDbType.Int, 2, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, 1))

            '--************************************************************************
            '-- AUX Adjustment

            .Add(New SqlParameter("@Aux_1", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, Aux_1))
            .Add(New SqlParameter("@Aux_2", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_2))
            .Add(New SqlParameter("@Aux_3", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_3))
            .Add(New SqlParameter("@Aux_4", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_4))
            .Add(New SqlParameter("@Aux_5", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_5))
            .Add(New SqlParameter("@Aux_6", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_6))
            .Add(New SqlParameter("@Aux_7", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_7))
            .Add(New SqlParameter("@Aux_8", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_8))
            .Add(New SqlParameter("@Aux_9", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_9))
            .Add(New SqlParameter("@Aux_10", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_10))


            .Add(New SqlParameter("@Aux_i_1", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_1))

            .Add(New SqlParameter("@Aux_i_2", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_2))

            .Add(New SqlParameter("@Aux_i_3", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_3))

            .Add(New SqlParameter("@Aux_i_4", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_4))



            .Add(New SqlParameter("@aux_f_1", SqlDbType.Float, 8, ParameterDirection.Input, _
                  False, 38, 0, "", DataRowVersion.Proposed, Aux_f_1))
            .Add(New SqlParameter("@aux_f_2", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_2))
            .Add(New SqlParameter("@aux_f_3", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_3))
            .Add(New SqlParameter("@aux_f_4", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_4))


            .Add(New SqlParameter("@Aux_b_1", SqlDbType.Bit, 1, ParameterDirection.Input, _
                      False, 1, 0, "", DataRowVersion.Proposed, Aux_b_1))
            .Add(New SqlParameter("@Aux_b_2", SqlDbType.Bit, 1, ParameterDirection.Input, _
               False, 1, 0, "", DataRowVersion.Proposed, Aux_b_2))
            .Add(New SqlParameter("@Aux_b_3", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                 False, 1, 0, "", DataRowVersion.Proposed, Aux_b_3))
            .Add(New SqlParameter("@Aux_b_4", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                 False, 1, 0, "", DataRowVersion.Proposed, Aux_b_4))

            '--************************************************************************

            .Add(New SqlParameter("@Cust_ID_GID", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Cust_ID_GID))
            .Add(New SqlParameter("@custid_detail_GID", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, custid_detail_GID))
            ' .Add(New SqlParameter("@cust_CCID", SqlDbType.TinyInt, 2, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, cust_CCID))
            .Add(New SqlParameter("@userID_GID", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sUSERID_GID))


            '--************************************************************************
            .Add(New SqlParameter("@statusid_2", SqlDbType.TinyInt, 2, ParameterDirection.Input, _
                                  False, 1, 0, "", DataRowVersion.Proposed, statusid_2))

            .Add(New SqlParameter("@isUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                False, 10, 0, "", DataRowVersion.Proposed, IsUpdate))

            .Add(New SqlParameter("@Aux_1_big", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                                 False, 0, 0, "", DataRowVersion.Proposed, Aux_1_big))
            .Add(New SqlParameter("@Aux_2_big", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                                 False, 0, 0, "", DataRowVersion.Proposed, Aux_2_big))
            .Add(New SqlParameter("@Aux_3_big", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                                 False, 0, 0, "", DataRowVersion.Proposed, Aux_3_big))
            .Add(New SqlParameter("@Aux_4_big", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                                 False, 0, 0, "", DataRowVersion.Proposed, Aux_4_big))
            .Add(New SqlParameter("@Aux_5_big", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                                 False, 0, 0, "", DataRowVersion.Proposed, Aux_5_big))
            .Add(New SqlParameter("@Aux_6_big", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                                 False, 0, 0, "", DataRowVersion.Proposed, Aux_6_big))

            .Add(New SqlParameter("@svrNoDisplay_s_ot", SqlDbType.NVarChar, 20, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_svrNoDisplay_s_ot))



        End With


        Try
            loComm.ExecuteNonQuery()
            str_svrNoDisplay_s_ot = CStr(loComm.Parameters.Item("@svrNoDisplay_s_ot").Value)
        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator...")

        End Try

    End Sub
    Public Sub get_TAX_DETAILS(ByVal DTTB_TAX As DataTable, ByVal nTAX_ID As Integer, _
                           ByRef SAL_TAX_CODE As Double, _
                           ByRef SAL_TAX_NUMBER As String, _
                          ByRef SAL_tAX_AUX_1 As String, _
                          ByRef dtcd As Integer, _
                          ByRef sbcd As Integer, _
                          ByRef Type As Integer)



        Try


            For Each mRW As DataRow In DTTB_TAX.Rows
                If nTAX_ID = CDbl(mRW("SAL_TAX_ID").ToString) Then
                    SAL_TAX_CODE = mRW("SAL_TAX_CODE")
                    SAL_TAX_NUMBER = mRW("SAL_TAX_NUMBER")
                    SAL_tAX_AUX_1 = mRW("SAL_tAX_AUX_1")
                    dtcd = mRW("dtcd")
                    sbcd = mRW("sbcd")
                    Type = mRW("TYP_To_Display")
                End If

            Next


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            DTTB_TAX = Nothing
        End Try
    End Sub
    Public Sub get_TAX_DETAILS_DTCD_SBCD(ByVal DTTB_TAX As DataTable, ByVal nTAX_ID As Integer, _
                                                      ByRef dtcd As Integer, _
                                                        ByRef sbcd As Integer)



        Try


            For Each mRW As DataRow In DTTB_TAX.Rows
                If nTAX_ID = CDbl(mRW("SAL_TAX_ID").ToString) Then
                    dtcd = mRW("dtcd")
                    sbcd = mRW("sbcd")
                End If

            Next


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            DTTB_TAX = Nothing
        End Try
    End Sub
    Public Function saveDetail_Fun_loComm_vat(ByRef loComm As SqlCommand, ByVal lsvrNo As String, _
                              ByVal lisqNo As Integer, _
                              ByVal lDetailCodeID As Integer, ByVal lSubCodeID As Integer, _
                              ByVal fAmount As Double, ByVal fBCAmount As Double, _
                              ByVal sDescription As String, _
                              ByVal lDetailCodeID_1 As Integer, ByVal lSubCodeID_1 As Integer, _
                              ByVal ldetail_CurrencyID As Double, ByVal fdetail_Rate As Double, _
                              ByVal fdealRate As Double, _
                              ByVal isBALEntry As Boolean, _
                              ByVal stkID As Integer, _
                              ByVal lisDRCol As Boolean, Optional ByVal ttVrNo_ToFund As String = "", _
                              Optional ByVal prcelNo_Allocation As String = "", _
                              Optional ByVal FWDealNo_Allocation As String = "", _
                               Optional ByVal dtcd_REF As Integer = 0, _
                                Optional ByVal sbcd_REF As Integer = 0, _
                                 Optional ByVal sDescription_REF As String = "", _
                            Optional ByVal ITEM_ID As Integer = 0, _
                           Optional ByVal vrDetailID_INV_GUID_REF As String = "", _
                           Optional ByVal vrno_INV_REF As String = "", _
                           Optional ByVal TAX_ID As Integer = 0, _
                           Optional ByVal TAX_PERCENTAGE As Double = 0, _
                           Optional ByVal TAX_AMOUNT As Double = 0, _
                           Optional ByVal AMOUNT_WITH_VAT As Double = 0, _
                           Optional ByVal TYP_ANA_CODE_ID As Integer = 0, _
                           Optional ByVal ANA_CODE_DTCD As Integer = 0, _
                            Optional ByVal ANA_CODE_SBCD As Integer = 0, _
                           Optional ByVal ANA_CODE_STR As String = "", _
                        Optional ByVal aux_vd_1 As String = "", _
                        Optional ByVal aux_vd_2 As String = "", _
                        Optional ByVal aux_vd_3 As String = "", _
                        Optional ByVal aux_vd_4 As String = "", _
                        Optional ByVal aux_vd_5 As String = "", _
                        Optional ByVal aux_vd_6 As String = "", _
                        Optional ByVal aux_vd_7 As String = "", _
                        Optional ByVal aux_vd_8 As String = "", _
                        Optional ByVal AUX_1_BIG As String = "", _
                        Optional ByVal AUX_2_BIG As String = "", _
                        Optional ByVal AUX_3_BIG As String = "", _
                        Optional ByVal AUX_4_BIG As String = "", _
                        Optional ByVal AUX_5_BIG As String = "", _
                        Optional ByVal AUX_6_BIG As String = "", _
                        Optional ByVal Supplier_Customer_Name As String = "", _
                        Optional ByVal Supplier_Customer_Permit_NO As String = "", _
                        Optional ByVal Supplier_Cusomeer_Product_Discription As String = "", _
                        Optional ByVal Supplier_Customer_Country_CODE As String = "", _
                        Optional ByVal Supplier_Customer_FCY_CODE As String = "", _
                        Optional ByVal Supplier_Customer_Source_Type As String = "", _
                        Optional ByVal Emirates As String = "", _
                        Optional ByVal FAF_FCY_AMT As String = "", _
                        Optional ByVal IS_EXCISE As Boolean = False, _
                        Optional ByVal TRANSACTION_DESC As String = "" _
                        ) As Long

        sDescription = UCase(sDescription)

        sDescription = Replace(sDescription, "&nbsp;", ".")

        aux_vd_1 = Trim(Replace(aux_vd_1, "&nbsp;", ""))
        aux_vd_2 = Trim(Replace(aux_vd_2, "&nbsp;", ""))
        aux_vd_3 = Trim(Replace(aux_vd_3, "&nbsp;", ""))
        aux_vd_4 = Trim(Replace(aux_vd_4, "&nbsp;", ""))
        aux_vd_5 = Trim(Replace(aux_vd_5, "&nbsp;", ""))
        aux_vd_6 = Trim(Replace(aux_vd_6, "&nbsp;", ""))
        aux_vd_7 = Trim(Replace(aux_vd_7, "&nbsp;", ""))
        aux_vd_8 = Trim(Replace(aux_vd_8, "&nbsp;", ""))

        Supplier_Customer_Name = Trim(Replace(Supplier_Customer_Name, "&nbsp;", ""))
        Supplier_Customer_Permit_NO = Trim(Replace(Supplier_Customer_Permit_NO, "&nbsp;", ""))
        Supplier_Cusomeer_Product_Discription = Trim(Replace(Supplier_Cusomeer_Product_Discription, "&nbsp;", ""))
        Supplier_Customer_Country_CODE = Trim(Replace(Supplier_Customer_Country_CODE, "&nbsp;", ""))
        Supplier_Customer_FCY_CODE = Trim(Replace(Supplier_Customer_FCY_CODE, "&nbsp;", ""))
        Supplier_Customer_Source_Type = Trim(Replace(Supplier_Customer_Source_Type, "&nbsp;", ""))
        Emirates = Trim(Replace(Emirates, "&nbsp;", ""))
        FAF_FCY_AMT = Trim(Replace(FAF_FCY_AMT, "&nbsp;", ""))
        TRANSACTION_DESC = Trim(Replace(TRANSACTION_DESC, "&nbsp;", ""))

        Dim m_lVrDetailID As Long = 0
        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        loComm.CommandText = "stp_tblVchDetail_Insert_GID_vat"

        With loComm.Parameters
            '--**********************************************
            '-- Case DB Entry = NoChange
            '--**********************************************
            '-- ******************************************
            If Len(sDescription) > 100 Then
                sDescription = Mid(sDescription, 1, 100)
            End If

            '-- ******************************************
            .Add(New SqlParameter("@svrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, _
                False, 0, 0, "", DataRowVersion.Proposed, lsvrNo))
            .Add(New SqlParameter("@sisqNo", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                False, 5, 0, "", DataRowVersion.Proposed, lisqNo))
            .Add(New SqlParameter("@iDetailCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
               False, 10, 0, "", DataRowVersion.Proposed, lDetailCodeID))

            .Add(New SqlParameter("@iSubCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
                lSubCodeID))

            .Add(New SqlParameter("@fAmount", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, _
                 Math.Abs(fAmount)))

            .Add(New SqlParameter("@fBCAmount", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(fBCAmount)))

            .Add(New SqlParameter("@sDescription", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
                False, 0, 0, "", DataRowVersion.Proposed, sDescription))

            .Add(New SqlParameter("@iDetailCodeID_1", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
                lDetailCodeID_1))

            .Add(New SqlParameter("@iSubCodeID_1", SqlDbType.Int, 4, ParameterDirection.Input, _
              False, 10, 0, "", DataRowVersion.Proposed, _
               lSubCodeID_1))

            If ldetail_CurrencyID = 0 Then
                .Add(New SqlParameter("@sidetail_CurrencyID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                False, 5, 0, "", DataRowVersion.Proposed, System.DBNull.Value))

            Else
                .Add(New SqlParameter("@sidetail_CurrencyID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                False, 5, 0, "", DataRowVersion.Proposed, ldetail_CurrencyID))
            End If
            'fdetail_Rate
            .Add(New SqlParameter("@fdetail_Rate", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, fdetail_Rate))


            .Add(New SqlParameter("@bisFrmDrColumn", SqlDbType.Bit, 1, ParameterDirection.Input, _
                False, 1, 0, "", DataRowVersion.Proposed, _
                lisDRCol))
            .Add(New SqlParameter("@bApprovedByTeller", SqlDbType.Bit, 1, ParameterDirection.Input, _
                False, 1, 0, "", DataRowVersion.Proposed, 0))
            .Add(New SqlParameter("@lEntryNo", SqlDbType.BigInt, 8, ParameterDirection.Input, _
                False, 19, 0, "", DataRowVersion.Proposed, SqlTypes.SqlInt64.Null))

            .Add(New SqlParameter("@fdealRate", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, fdealRate))

            '.Add(New SqlParameter("@bisCCYStkEntry", SqlDbType.Bit, 1, ParameterDirection.Input, _
            '    False, 1, 0, "", DataRowVersion.Proposed, _
            '    isCCYStkEntry))

            .Add(New SqlParameter("@bisBALEntry", SqlDbType.Bit, 1, ParameterDirection.Input, _
              False, 1, 0, "", DataRowVersion.Proposed, _
              isBALEntry))

            .Add(New SqlParameter("@sistkID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                            False, 5, 0, "", DataRowVersion.Proposed, stkID))

            .Add(New SqlParameter("@sttVrNo_ToFund", SqlDbType.NVarChar, 64, ParameterDirection.Input, _
               False, 0, 0, "", DataRowVersion.Proposed, ttVrNo_ToFund))

            .Add(New SqlParameter("@sprcelNo_Allocation", SqlDbType.NVarChar, 64, ParameterDirection.Input, _
                           False, 0, 0, "", DataRowVersion.Proposed, prcelNo_Allocation))

            .Add(New SqlParameter("@sFWDealNo_Allocation", SqlDbType.NVarChar, 64, ParameterDirection.Input, _
                                    False, 0, 0, "", DataRowVersion.Proposed, FWDealNo_Allocation))


            .Add(New SqlParameter("@dtcd_Ref", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, dtcd_REF))

            .Add(New SqlParameter("@sbcd_Ref", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
                sbcd_REF))

            .Add(New SqlParameter("@sDescription_ref", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
           False, 0, 0, "", DataRowVersion.Proposed, sDescription_REF))

            '--**********************************************
            '-- Case CR Entry = NoChange
            '--**********************************************



            .Add(New SqlParameter("@ITEM_ID", SqlDbType.Int, 4, ParameterDirection.Input, _
                          False, 10, 0, "", DataRowVersion.Proposed, ITEM_ID))
            .Add(New SqlParameter("@vrDetailID_INV_GUID_REF", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                False, 0, 0, "", DataRowVersion.Proposed, vrDetailID_INV_GUID_REF))
            .Add(New SqlParameter("@vrno_INV_REF", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                          False, 0, 0, "", DataRowVersion.Proposed, vrno_INV_REF))
            .Add(New SqlParameter("@TAX_ID", SqlDbType.Int, 4, ParameterDirection.Input, _
                                     False, 10, 0, "", DataRowVersion.Proposed, TAX_ID))

            .Add(New SqlParameter("@TAX_PERCENTAGE", SqlDbType.Float, 8, ParameterDirection.Input, _
              False, 38, 0, "", DataRowVersion.Proposed, TAX_PERCENTAGE))
            .Add(New SqlParameter("@TAX_AMOUNT", SqlDbType.Float, 8, ParameterDirection.Input, _
                          False, 38, 0, "", DataRowVersion.Proposed, TAX_AMOUNT))
            .Add(New SqlParameter("@AMOUNT_WITH_VAT", SqlDbType.Float, 8, ParameterDirection.Input, _
                          False, 38, 0, "", DataRowVersion.Proposed, AMOUNT_WITH_VAT))



            .Add(New SqlParameter("@TYP_ANA_CODE_ID", SqlDbType.Int, 4, ParameterDirection.Input, _
             False, 10, 0, "", DataRowVersion.Proposed, TYP_ANA_CODE_ID))
            .Add(New SqlParameter("@ANA_CODE_DTCD", SqlDbType.Int, 4, ParameterDirection.Input, _
                        False, 10, 0, "", DataRowVersion.Proposed, ANA_CODE_DTCD))
            .Add(New SqlParameter("@ANA_CODE_SBCD", SqlDbType.Int, 4, ParameterDirection.Input, _
                        False, 10, 0, "", DataRowVersion.Proposed, ANA_CODE_SBCD))

            .Add(New SqlParameter("@ANA_CODE_STR", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                                 False, 0, 0, "", DataRowVersion.Proposed, ANA_CODE_STR))




            .Add(New SqlParameter("@aux_vd_1", SqlDbType.NVarChar, 199, ParameterDirection.Input, _
                                 False, 0, 0, "", DataRowVersion.Proposed, aux_vd_1))
            .Add(New SqlParameter("@aux_vd_2", SqlDbType.NVarChar, 199, ParameterDirection.Input, _
                                             False, 0, 0, "", DataRowVersion.Proposed, aux_vd_2))
            .Add(New SqlParameter("@aux_vd_3", SqlDbType.NVarChar, 199, ParameterDirection.Input, _
                              False, 0, 0, "", DataRowVersion.Proposed, aux_vd_3))
            .Add(New SqlParameter("@aux_vd_4", SqlDbType.NVarChar, 199, ParameterDirection.Input, _
                                             False, 0, 0, "", DataRowVersion.Proposed, aux_vd_4))
            .Add(New SqlParameter("@aux_vd_5", SqlDbType.NVarChar, 199, ParameterDirection.Input, _
                                           False, 0, 0, "", DataRowVersion.Proposed, aux_vd_5))
            .Add(New SqlParameter("@aux_vd_6", SqlDbType.NVarChar, 199, ParameterDirection.Input, _
                                             False, 0, 0, "", DataRowVersion.Proposed, aux_vd_6))
            .Add(New SqlParameter("@aux_vd_7", SqlDbType.NVarChar, 199, ParameterDirection.Input, _
                                           False, 0, 0, "", DataRowVersion.Proposed, aux_vd_7))
            .Add(New SqlParameter("@aux_vd_8", SqlDbType.NVarChar, 199, ParameterDirection.Input, _
                                             False, 0, 0, "", DataRowVersion.Proposed, aux_vd_8))



            .Add(New SqlParameter("@Aux_1_BIG", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, AUX_1_BIG))
            .Add(New SqlParameter("@Aux_2_BIG", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, AUX_2_BIG))
            .Add(New SqlParameter("@Aux_3_BIG", SqlDbType.NVarChar, 400, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, AUX_3_BIG))
            .Add(New SqlParameter("@Aux_4_BIG", SqlDbType.NVarChar, 400, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, AUX_4_BIG))
            .Add(New SqlParameter("@Aux_5_BIG", SqlDbType.NVarChar, 400, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, AUX_5_BIG))
            .Add(New SqlParameter("@Aux_6_BIG", SqlDbType.NVarChar, 400, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, AUX_6_BIG))


            .Add(New SqlParameter("@Supplier_Customer_Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, Supplier_Customer_Name))
            .Add(New SqlParameter("@Supplier_Customer_Permit_NO", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, Supplier_Customer_Permit_NO))
            .Add(New SqlParameter("@Supplier_Cusomeer_Product_Discription", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, Supplier_Cusomeer_Product_Discription))
            .Add(New SqlParameter("@Supplier_Customer_Country_CODE", SqlDbType.NVarChar, 10, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, Supplier_Customer_Country_CODE))
            .Add(New SqlParameter("@Supplier_Customer_FCY_CODE", SqlDbType.NVarChar, 10, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, Supplier_Customer_FCY_CODE))
            .Add(New SqlParameter("@Supplier_Customer_Source_Type", SqlDbType.NVarChar, 10, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, Supplier_Customer_Source_Type))


            .Add(New SqlParameter("@Emirates", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                                 False, 0, 0, "", DataRowVersion.Proposed, Emirates))


            .Add(New SqlParameter("@FAF_FCY_AMT", SqlDbType.NVarChar, 20, ParameterDirection.Input, _
                                 False, 0, 0, "", DataRowVersion.Proposed, FAF_FCY_AMT))

            .Add(New SqlParameter("@IS_EXCISE", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                    False, 1, 0, "", DataRowVersion.Proposed, IS_EXCISE))

            .Add(New SqlParameter("@TRANSACTION_DESC", SqlDbType.NVarChar, 250, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, TRANSACTION_DESC))


        End With

        'loComm.ExecuteNonQuery()

        Try

            If Math.Abs(fAmount) <> 0 Then
                loComm.ExecuteNonQuery()
            End If


        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator..." & ex.Message)
        End Try



    End Function
    Public Sub initCmb_BY_QRY(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
       ByVal strSql_QRY As String)


        Dim moComm As New SqlClient.SqlCommand
        setCon(True)
        moComm.Connection = gCon

        strSql = strSql_QRY
        cmbBox.Items.Clear()
        cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
        moComm.CommandText = strSql
        Try
            Dim dt As DataTable = New DataTable("dt")
            Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
            sdaAdapter.Fill(dt)
            Dim recCount As Int16
            'cmbBox.Items.Clear()
            'cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, -1))
            For recCount = 0 To dt.Rows.Count - 1
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0), dt.Rows(recCount)(1)))
            Next
            cmbBox.SelectedIndex = 0
            moComm.Dispose()
            sdaAdapter.Dispose()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
        End Try

    End Sub

    ' ''Public Sub getStauts_WPS_PIF_INFO(ByVal sPIFID As String, _
    ' ''                                 ByRef isPAY As Boolean, _
    ' ''                                 ByRef is_LATE_10_DAYS_Auth As Boolean, _
    ' ''                                 ByRef TR_Expiry_Date As Date)

    ' ''    Dim dttb As New DataTable
    ' ''    Try

    ' ''        dttb = DTTB_Fill_WPS_DB("Select  aux_b_1,isnull(is_LATE_10_DAYS_Auth,'False') as is_LATE_10_DAYS_Auth ,TR_Expiry_Date  from tblpif where PIFID = '" & sPIFID & "' ")

    ' ''        isPAY = dttb(0)("aux_b_1")
    ' ''        is_LATE_10_DAYS_Auth = dttb(0)("is_LATE_10_DAYS_Auth")
    ' ''        TR_Expiry_Date = dttb(0)("TR_Expiry_Date")

    ' ''    Catch ex As Exception
    ' ''        setCon_WPS(True)
    ' ''        Throw New Exception(ex.Message)
    ' ''    Finally
    ' ''        dttb.Dispose()
    ' ''        dttb = Nothing

    ' ''    End Try

    ' ''End Sub



   
    Public Function Execute_nonSQL(ByVal strSQL As String, ByRef strERR As String) As Boolean
        Dim moComm As New SqlClient.SqlCommand
        Try
            setCon(True)
            moComm.Connection = gCon
            moComm.CommandType = CommandType.Text
            moComm.CommandText = strSQL
            moComm.ExecuteNonQuery()
            Execute_nonSQL = True
        Catch ex As Exception
            Execute_nonSQL = False
            strERR = ex.Message
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
            moComm.Dispose()
        End Try

    End Function

    Public Sub Execute_nonSQL_EXP_BASE(ByVal strSQL As String)
        Dim moComm As New SqlClient.SqlCommand
        Try
            setCon(True)
            moComm.Connection = gCon
            moComm.CommandType = CommandType.Text
            moComm.CommandText = strSQL
            moComm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
            moComm.Dispose()
        End Try
    End Sub






    Public Sub Execute_nonSQL_WithEXCP(ByVal strSQL As String)
        Dim moComm As New SqlClient.SqlCommand
        Try
            setCon(True)
            moComm.Connection = gCon
            moComm.CommandType = CommandType.Text
            moComm.CommandText = strSQL
            moComm.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
            moComm.Dispose()
        End Try

    End Sub








    Public Function Execute_nonSQL_objTrans(ByRef moComm As SqlClient.SqlCommand, _
                                            ByVal strSQL As String) As Boolean

        Try
            moComm.Parameters.Clear()
            moComm.CommandType = CommandType.Text
            moComm.CommandText = strSQL
            moComm.ExecuteNonQuery()
            Execute_nonSQL_objTrans = True
        Catch ex As Exception
            Execute_nonSQL_objTrans = False
            Throw New Exception(ex.Message)
        Finally

        End Try

    End Function


    Public Sub setCon(ByVal isOpen As Boolean)
        Try
            If isOpen Then
                If gCon.State = ConnectionState.Closed Then
                    gCon.ConnectionString = gStrCnString
                    gCon.Open()
                End If
            Else
                If gCon.State = ConnectionState.Open Then
                    gCon.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox("System is unable to contact with Server. Please check internet connection / contact to server")
        End Try
    End Sub




    Public Sub setCon_IMAGE(ByVal isOpen As Boolean)
        Try

            If isOpen Then
                If gCon_IMAGE.State = ConnectionState.Closed Then
                    gCon_IMAGE.ConnectionString = gStrCnString_IMAGE
                    gCon_IMAGE.Open()
                End If
            Else
                If gCon_IMAGE.State = ConnectionState.Open Then
                    gCon_IMAGE.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox("System is unable to contact with Server. Please check internet connection / contact to server")
        End Try
    End Sub






    Public Function DateSerial_SFTX(ByVal yr As Integer, ByVal Mn As Integer, ByVal dy As Integer) As Date
        Dim mDT As Date

        Try
            mDT = New Date(yr, Mn, dy)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return mDT

    End Function

    Public Function DateSerial_SFTX_STR(ByVal yr As Integer, ByVal Mn As Integer, ByVal dy As Integer) As String

        Dim dt As String = ""
        Try

            dt = CStr(yr) + "/" + CStr(Mn) + "/" + CStr(dy)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return dt

    End Function

    'Public Sub getMMDDYYYY(ByVal strDate As String, ByRef DT As DateTime)
    '    Try


    '        Dim str As String = strDate

    '        str = Replace(str, "-", "|")
    '        str = Replace(str, "/", "|")
    '        Dim DD As String = Mid(str, 1, (InStr(str, "|") - 1))
    '        str = Mid(str, (InStr(str, "|") + 1))
    '        Dim MM As String = Mid(str, 1, (InStr(str, "|") - 1))
    '        str = Mid(str, (InStr(str, "|") + 1))
    '        Dim YY As String = str
    '        str = MM + "/" + DD + "/" + YY
    '        DT = CDate(str)
    '    Catch ex As Exception

    '    End Try


    'End Sub

    Public Sub getMMDDYYYY(ByVal strDate As String, ByRef DT As DateTime)
        Try


            Dim str As String = strDate

            str = Replace(str, "-", "|")
            str = Replace(str, "/", "|")





            Dim DD As String = Mid(str, 1, (InStr(str, "|") - 1))
            str = Mid(str, (InStr(str, "|") + 1))
            Dim MM As String = Mid(str, 1, (InStr(str, "|") - 1))
            str = Mid(str, (InStr(str, "|") + 1))
            Dim YY As String = str
            str = MM + "/" + DD + "/" + YY
            DT = DateSerial(CInt(YY), CInt(MM), CInt(DD))
            ' DT = CDate(str)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Sub


    Public Function isDATEValid_GREATER(ByVal BigDate As Date, ByVal SmallDate As Date) As Boolean

        Try
            If DateSerial(BigDate.Year, BigDate.Month, BigDate.Day) < DateSerial(SmallDate.Year, SmallDate.Month, SmallDate.Day) Then
                Return True
                Exit Function
            End If
            ' Return False

        Catch ex As Exception

        End Try

    End Function
    Public Function isDATEValid_NOTGREATER(ByVal BigDate As Date, ByVal SmallDate As Date) As Boolean

        Try
            If DateSerial(BigDate.Year, BigDate.Month, BigDate.Day) < DateSerial(SmallDate.Year, SmallDate.Month, SmallDate.Day) Then
                Return True
                Exit Function
            End If
            Return False

        Catch ex As Exception

        End Try

    End Function

    Public Function isDATEValid_equalto(ByVal BigDate As Date, ByVal SmallDate As Date) As Boolean

        Try
            If DateSerial(BigDate.Year, BigDate.Month, BigDate.Day) = DateSerial(SmallDate.Year, SmallDate.Month, SmallDate.Day) Then
                Return False
                Exit Function
            End If
            Return False

        Catch ex As Exception

        End Try

    End Function

    Public Function isDATEValid_NOT_EQUAL(ByVal BigDate As Date, ByVal SmallDate As Date) As Boolean

        Try
            If DateSerial(BigDate.Year, BigDate.Month, BigDate.Day) <> DateSerial(SmallDate.Year, SmallDate.Month, SmallDate.Day) Then
                Return True
                Exit Function
            End If
            Return False

        Catch ex As Exception

        End Try

    End Function




    Public Function getMaxValue_LoComm(ByRef loComm As SqlCommand, ByVal fldName As String, ByVal tblName As String, ByVal strWhere As String) As Long

        setCon(True)
        loComm.Connection = gCon

        Try
            loComm.Parameters.Clear()
            loComm.CommandType = CommandType.Text
            strSql = "Select IsNull(Max(" & fldName & "),0) as ID From " & tblName & " "
            If Len(Trim(strWhere)) <> 0 Then
                strSql = strSql & " WHERE " & strWhere
            End If
            loComm.CommandText = strSql
            getMaxValue_LoComm = loComm.ExecuteScalar + 1
        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator..." & ex.Message)


        Finally


        End Try


    End Function




    Public Function GetEPassword(ByVal strPassword As String) As String
        GetEPassword = strPassword
        Return GetEPassword
        'Dim intChar As Integer
        'GetEPassword = String.Empty
        'a = 5
        'b = 2
        ''strPassword = "ne^*)%"
        'For intChar = 1 To Len(Trim(strPassword))
        '    a = a + b
        '    GetEPassword = GetEPassword + Chr(Asc(Mid(strPassword, intChar, 1)) + a)
        'Next intChar
        'Return GetEPassword
    End Function


    Public Sub saveVchMaster_Fun(ByRef loComm As SqlCommand, ByVal svrNo As String, _
                                ByVal ivrNoDisplay As String, _
                                ByVal svrNoDisplay_s As String, _
                                ByVal sinvoiceNo As String, _
                                ByVal davrDate As Date, _
                                ByVal byvrTypeID As Int16, _
                                ByVal iStatusID As Integer, _
                                ByVal siPrincipleCurrID As Int32, _
                                ByVal sMasterNarration As String, _
                                ByVal iDetailCodeID As Integer, _
                                ByVal iSubCodeID As Integer, _
                                ByVal fPrincipleRate As Double, _
                                ByVal sCheque_DD_TT As String, _
                                ByVal daChequeDate As Date, _
                                ByVal Bvch_PayReceive As String, _
                                ByVal Bvch_Reference As String, _
                                ByVal RevREFNo As Int32, _
                                ByVal iBeneID As Long, _
                                ByVal iCustID As Long, _
                                ByVal isChkCanceled As Boolean, _
                                ByVal isDenomination As Boolean, _
                                ByVal sREFvrNo As String, _
                                ByVal byvrTypeID_sys As Int16, _
                                ByVal sPOSTitle As String, _
                                ByVal sPOSCardID As String, _
                                ByVal fPOSBankComm As Double, _
                                ByVal fPOSExchComm As Double, _
                                ByVal itrnstRef_DtCd As Integer, _
                                ByVal itrnstRef_SbCd As Integer, _
                                ByVal ttValueType As String, _
                                ByVal iBranchID As Integer, _
                                ByVal isPayByAcc As Boolean, _
                                ByVal str_CshByID As String, _
                                ByVal IsUpdate As Boolean, _
                                ByVal ttComm As Double, _
                                ByVal ttCCY As Integer, _
                                ByVal ttRate As Double, _
                                ByVal ttFC As Double, _
                                ByVal ttLC As Double, _
                                ByVal Custid_Details As Long, _
                                ByVal Rep_ID As Long, _
                                ByVal nUSERID As Integer, _
                                ByVal nCCID As Integer, _
                                ByVal sUSERID_GID As String, _
                                Optional ByVal isRevEntry As Boolean = 0, _
                                Optional ByVal sREF_TrNo As String = "0", _
                                Optional ByVal bisDASP_BalanceAmount As Boolean = 0, _
                                Optional ByVal iCCID_Posting As Integer = 0, _
                                Optional ByVal Aux_1 As String = "", _
                                Optional ByVal Aux_2 As String = "", _
                                Optional ByVal Aux_3 As String = "", _
                                Optional ByVal Aux_4 As String = "", _
                                Optional ByVal Aux_5 As String = "", _
                                Optional ByVal Aux_6 As String = "", _
                                Optional ByVal Aux_7 As String = "", _
                                Optional ByVal Aux_8 As String = "", _
                                Optional ByVal Aux_9 As String = "", _
                                Optional ByVal Aux_10 As String = "", _
                                Optional ByVal Aux_i_1 As Integer = 0, _
                                Optional ByVal Aux_i_2 As Integer = 0, _
                                Optional ByVal Aux_i_3 As Integer = 0, _
                                Optional ByVal Aux_i_4 As Integer = 0, _
                                Optional ByVal Aux_f_1 As Double = 0, _
                                Optional ByVal Aux_f_2 As Double = 0, _
                                Optional ByVal Aux_f_3 As Double = 0, _
                                Optional ByVal Aux_f_4 As Double = 0, _
                                Optional ByVal Aux_b_1 As Boolean = 0, _
                                Optional ByVal Aux_b_2 As Boolean = 0, _
                                Optional ByVal Aux_b_3 As Boolean = 0, _
                                Optional ByVal Aux_b_4 As Boolean = 0, _
                                Optional ByVal Cust_ID_GID As String = "", _
                                Optional ByVal custid_detail_GID As String = "", _
                                Optional ByVal cust_CCID As Integer = 1, _
                                Optional ByVal statusid_2 As Integer = 1, _
                                Optional ByRef str_svrNoDisplay_s_ot As String = "")



        svrNo = UCase(svrNo)
        ivrNoDisplay = UCase(ivrNoDisplay)
        svrNoDisplay_s = UCase(svrNoDisplay_s)
        sinvoiceNo = UCase(sinvoiceNo)
        sMasterNarration = UCase(sMasterNarration)
        'sMasterNarration = Replace(sMasterNarration, "&nbsp;", ".")


        sCheque_DD_TT = UCase(sCheque_DD_TT)
        Bvch_PayReceive = UCase(Bvch_PayReceive)
        'Bvch_PayReceive = Replace(Bvch_PayReceive, "&nbsp;", ".")

        Bvch_Reference = UCase(Bvch_Reference)
        'Bvch_Reference = Replace(Bvch_Reference, "&nbsp;", ".")

        sREFvrNo = UCase(sREFvrNo)
        sPOSTitle = UCase(sPOSTitle)
        sPOSCardID = UCase(sPOSCardID)
        ttValueType = UCase(ttValueType)
        str_CshByID = UCase(str_CshByID)
        sPOSTitle = UCase(sPOSTitle)
        sPOSCardID = UCase(sPOSCardID)
        ttValueType = UCase(ttValueType)

        If Len(sMasterNarration) > 100 Then
            sMasterNarration = Mid(sMasterNarration, 1, 99)
        End If
        'Dim nAccountType As Integer
        'Dim loReader As SqlClient.SqlDataReader
        'Dim strTrID As String


        '---------------------------------------------------
        '***************************************************
        'If iCCID_Posting <> goCCID_USER Then
        '    MsgBox("USER CAN DO ENTRY ONLY IN THE DEFAULT BRANCH ")
        '    Throw New Exception("USER CAN DO ENTRY ONLY IN THE DEFAULT BRANCH ......")
        '    Exit Sub
        'End If
        'If DateSerial_SFTX(gREV_DATE_MAX.Year, gREV_DATE_MAX.Month, gREV_DATE_MAX.Day) >= DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day) Then
        '    MsgBox("DATA IS LOCKED --- CAN NOT SAVE/UPDATE ....")
        '    Throw New Exception("System is unable to save. please contact to system administrator...")
        '    Exit Sub
        'End If
        '***************************************************
        '---------------------------------------------------

        'If DateSerial_SFTX(gREV_DATE_MAX.Year, gREV_DATE_MAX.Month, gREV_DATE_MAX.Day) >= DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day) Then
        '    MsgBox("DATA IS LOCKED --- CAN NOT SAVE/UPDATE ....")
        '    Throw New Exception("System is unable to save. please contact to system administrator...")
        '    Exit Sub
        'End If


        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        loComm.CommandText = "stp_tblVchMaster_GID"


        With loComm.Parameters

            .Add(New SqlParameter("@svrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, svrNo))

            .Add(New SqlParameter("@ivrNoDisplay", SqlDbType.Int, 4, ParameterDirection.Input, _
             False, 10, 0, "", DataRowVersion.Proposed, CDbl(split_ID(ivrNoDisplay))))

            .Add(New SqlParameter("@svrNoDisplay_s", SqlDbType.VarChar, 20, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, svrNoDisplay_s))

            .Add(New SqlParameter("@sinvoiceNo", SqlDbType.VarChar, 30, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sinvoiceNo & ""))

            .Add(New SqlParameter("@davrDate", SqlDbType.DateTime, 8, ParameterDirection.Input, _
            False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day)))

            .Add(New SqlParameter("@byvrTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, _
            False, 3, 0, "", DataRowVersion.Proposed, byvrTypeID))

            .Add(New SqlParameter("@istatusID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iStatusID))

            .Add(New SqlParameter("@siPrincipleCurrID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
            False, 5, 0, "", DataRowVersion.Proposed, IIf(siPrincipleCurrID = 0, SqlTypes.SqlInt16.Null, siPrincipleCurrID)))

            .Add(New SqlParameter("@sMasterNarration", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sMasterNarration))

            .Add(New SqlParameter("@iDetailCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iDetailCodeID))

            .Add(New SqlParameter("@iSubCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iSubCodeID))

            .Add(New SqlParameter("@fPrincipleRate", SqlDbType.Float, 8, ParameterDirection.Input, _
            False, 38, 0, "", DataRowVersion.Proposed, fPrincipleRate))

            .Add(New SqlParameter("@sCheque_DD_TT", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sCheque_DD_TT & ""))

            .Add(New SqlParameter("@daChequeDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
            False, 16, 0, "", DataRowVersion.Proposed, DateSerial_SFTX(daChequeDate.Year, daChequeDate.Month, daChequeDate.Day)))

            .Add(New SqlParameter("@Bvch_PayReceive", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, Bvch_PayReceive & ""))

            .Add(New SqlParameter("@Bvch_Reference", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, Bvch_Reference & ""))

            .Add(New SqlParameter("@RevREFNo", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, IIf(RevREFNo = 0, System.DBNull.Value, RevREFNo)))

            .Add(New SqlParameter("@iBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(iBeneID = 0, System.DBNull.Value, iBeneID)))

            .Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(iCustID = 0, System.DBNull.Value, iCustID)))

            .Add(New SqlParameter("@isChkCanceled", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, isChkCanceled))
            .Add(New SqlParameter("@isDenomination", SqlDbType.Bit, 1, ParameterDirection.Input, _
                          False, 1, 0, "", DataRowVersion.Proposed, isDenomination))


            .Add(New SqlParameter("@sREFvrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, _
           False, 0, 0, "", DataRowVersion.Proposed, IIf(Len(Trim(sREFvrNo)) = 0, System.DBNull.Value, sREFvrNo)))

            .Add(New SqlParameter("@byvrTypeID_sys", SqlDbType.TinyInt, 1, ParameterDirection.Input, _
                        False, 3, 0, "", DataRowVersion.Proposed, IIf(byvrTypeID_sys = 0, System.DBNull.Value, byvrTypeID_sys)))

            .Add(New SqlParameter("@sPOSTitle", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sPOSTitle))
            .Add(New SqlParameter("@sPOSCardID", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, sPOSCardID))


            .Add(New SqlParameter("@fPOSBankComm", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, fPOSBankComm))
            .Add(New SqlParameter("@fPOSExchComm", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, fPOSExchComm))


            .Add(New SqlParameter("@itrnstRef_DtCd", SqlDbType.Int, 4, ParameterDirection.Input, _
             False, 10, 0, "", DataRowVersion.Proposed, IIf(itrnstRef_DtCd = 0, System.DBNull.Value, itrnstRef_DtCd)))
            .Add(New SqlParameter("@itrnstRef_SbCd", SqlDbType.Int, 4, ParameterDirection.Input, _
                        False, 10, 0, "", DataRowVersion.Proposed, IIf(itrnstRef_SbCd = 0, System.DBNull.Value, itrnstRef_SbCd)))


            .Add(New SqlParameter("@sttValueType", SqlDbType.VarChar, 30, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, ttValueType))


            .Add(New SqlParameter("@iUserID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, nUSERID))


            .Add(New SqlParameter("@iCCID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, nCCID))


            .Add(New SqlParameter("@isPayByAcc", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, isPayByAcc))

            .Add(New SqlParameter("@isRevEntry", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, isRevEntry))


            .Add(New SqlParameter("@sREF_TrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sREF_TrNo))

            .Add(New SqlParameter("@isDASP_BalanceAmount", SqlDbType.Bit, 1, ParameterDirection.Input, _
                     False, 1, 0, "", DataRowVersion.Proposed, bisDASP_BalanceAmount))

            '.Add(New SqlParameter("@iCshByID", SqlDbType.Int, 4, ParameterDirection.Input, _
            '           False, 10, 0, "", DataRowVersion.Proposed, iCshByID))


            .Add(New SqlParameter("@iCshByID", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, str_CshByID))


            .Add(New SqlParameter("@iCCID_Posting", SqlDbType.Int, 4, ParameterDirection.Input, _
                                   False, 10, 0, "", DataRowVersion.Proposed, iCCID_Posting))



            .Add(New SqlParameter("@ttComm", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttComm))

            .Add(New SqlParameter("@ttCCY", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
            False, 5, 0, "", DataRowVersion.Proposed, ttCCY))

            .Add(New SqlParameter("@ttRate", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttRate))
            .Add(New SqlParameter("@ttFC", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttFC))
            .Add(New SqlParameter("@ttLC", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttLC))


            .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(Custid_Details = 0, System.DBNull.Value, Custid_Details)))


            .Add(New SqlParameter("@Rep_ID", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(Rep_ID = 0, System.DBNull.Value, Rep_ID)))





            .Add(New SqlParameter("@BlockID", SqlDbType.Int, 2, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, 1))

            '--************************************************************************
            '-- AUX Adjustment

            .Add(New SqlParameter("@Aux_1", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, Aux_1))
            .Add(New SqlParameter("@Aux_2", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_2))
            .Add(New SqlParameter("@Aux_3", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_3))
            .Add(New SqlParameter("@Aux_4", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_4))
            .Add(New SqlParameter("@Aux_5", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_5))
            .Add(New SqlParameter("@Aux_6", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_6))
            .Add(New SqlParameter("@Aux_7", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_7))
            .Add(New SqlParameter("@Aux_8", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_8))
            .Add(New SqlParameter("@Aux_9", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_9))
            .Add(New SqlParameter("@Aux_10", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_10))


            .Add(New SqlParameter("@Aux_i_1", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_1))

            .Add(New SqlParameter("@Aux_i_2", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_2))

            .Add(New SqlParameter("@Aux_i_3", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_3))

            .Add(New SqlParameter("@Aux_i_4", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_4))



            .Add(New SqlParameter("@aux_f_1", SqlDbType.Float, 8, ParameterDirection.Input, _
                  False, 38, 0, "", DataRowVersion.Proposed, Aux_f_1))
            .Add(New SqlParameter("@aux_f_2", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_2))
            .Add(New SqlParameter("@aux_f_3", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_3))
            .Add(New SqlParameter("@aux_f_4", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_4))


            .Add(New SqlParameter("@Aux_b_1", SqlDbType.Bit, 1, ParameterDirection.Input, _
                      False, 1, 0, "", DataRowVersion.Proposed, Aux_b_1))
            .Add(New SqlParameter("@Aux_b_2", SqlDbType.Bit, 1, ParameterDirection.Input, _
               False, 1, 0, "", DataRowVersion.Proposed, Aux_b_2))
            .Add(New SqlParameter("@Aux_b_3", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                 False, 1, 0, "", DataRowVersion.Proposed, Aux_b_3))
            .Add(New SqlParameter("@Aux_b_4", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                 False, 1, 0, "", DataRowVersion.Proposed, Aux_b_4))

            '--************************************************************************

            .Add(New SqlParameter("@Cust_ID_GID", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, Cust_ID_GID))
            .Add(New SqlParameter("@custid_detail_GID", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, custid_detail_GID))
            ' .Add(New SqlParameter("@cust_CCID", SqlDbType.TinyInt, 2, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, cust_CCID))
            .Add(New SqlParameter("@userID_GID", SqlDbType.VarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, sUSERID_GID))


            '--************************************************************************
            .Add(New SqlParameter("@statusid_2", SqlDbType.TinyInt, 2, ParameterDirection.Input, _
                                  False, 1, 0, "", DataRowVersion.Proposed, statusid_2))

            .Add(New SqlParameter("@isUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
       False, 10, 0, "", DataRowVersion.Proposed, IsUpdate))



            .Add(New SqlParameter("@svrNoDisplay_s_ot", SqlDbType.NVarChar, 20, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, str_svrNoDisplay_s_ot))



        End With


        Try
            loComm.ExecuteNonQuery()
            If IsUpdate = False Then
                str_svrNoDisplay_s_ot = CStr(loComm.Parameters.Item("@svrNoDisplay_s_ot").Value)
            End If
        Catch ex As Exception


            Throw New Exception(ex.Message + "  system is unable to process...")

        End Try

    End Sub


    Public Sub saveVchMaster_Fun_Grg(ByRef loComm As SqlCommand, ByVal iID_WORK As String, _
                        ByVal sID_INS As String, _
                             ByVal daVistDate As Date, _
                        ByVal daApproveDate As Date, _
                          ByVal dastartDate As Date, _
                        ByVal sCust_id As String, _
                        ByVal sCarReg As String, _
                        ByVal sDirverLicNo As String, _
                        ByVal sDriverName As String, _
                        ByVal daAccident_Date As Date, _
                        ByVal sCAR_MAKE As String, _
                                ByVal sCAR_STATUS As String, _
                          ByVal sCAR_MODEL As String, _
                        ByVal sremarks As String, ByVal isUpdate As Boolean, ByRef sID_INS_OUT As String)





        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        loComm.CommandText = "stp_tbl_GRG_WORK_MASTER_InsertUpdate"


        With loComm.Parameters

            .Add(New SqlParameter("@iID_WORK", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, iID_WORK))

            .Add(New SqlParameter("@iID_INS", SqlDbType.Int, 4, ParameterDirection.Input, _
             False, 10, 0, "", DataRowVersion.Proposed, sID_INS))

            .Add(New SqlParameter("@dVistDate", SqlDbType.DateTime, 8, ParameterDirection.Input, _
               False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(daVistDate.Year, daVistDate.Month, daVistDate.Day)))

            .Add(New SqlParameter("@dApproveDate", SqlDbType.DateTime, 8, ParameterDirection.Input, _
            False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(daApproveDate.Year, daApproveDate.Month, daApproveDate.Day)))

            .Add(New SqlParameter("@dstartDate", SqlDbType.DateTime, 8, ParameterDirection.Input, _
            False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(dastartDate.Year, dastartDate.Month, dastartDate.Day)))

            .Add(New SqlParameter("@iCust_Id", SqlDbType.BigInt, 8, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sCust_id))

            .Add(New SqlParameter("@sCarReg", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 23, 3, "", DataRowVersion.Proposed, sCarReg))

            .Add(New SqlParameter("@sDirverLicNo", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 3, 0, "", DataRowVersion.Proposed, sDirverLicNo))

            .Add(New SqlParameter("@sDriverName", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, sDriverName))

            .Add(New SqlParameter("@dAccident_Date", SqlDbType.DateTime, 8, ParameterDirection.Input, _
            False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(daAccident_Date.Year, daAccident_Date.Month, daAccident_Date.Day)))

            .Add(New SqlParameter("@sCAR_MAKE", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sCAR_MAKE))

            .Add(New SqlParameter("@sCAR_STATUS", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, sCAR_STATUS))

            .Add(New SqlParameter("@sCAR_MODEL", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, sCAR_MODEL))

            .Add(New SqlParameter("@sremarks", SqlDbType.NVarChar, 250, ParameterDirection.Input, _
            False, 38, 0, "", DataRowVersion.Proposed, sremarks))
            objComm.Parameters.Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, isUpdate))
            objComm.Parameters.Add(New SqlParameter("@idAUTO_ot", SqlDbType.BigInt, 8, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, sID_INS))
            objComm.Parameters.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, sID_INS))





            '--************************************************************************
            '--************************************************************************




        End With


        Try
            loComm.ExecuteNonQuery()
            If isUpdate = True Then
            Else
                sID_INS_OUT = objComm.Parameters.Item("@idAUTO_ot").Value
            End If


        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator...")

        End Try

    End Sub



    Public Sub saveVchMaster_Fun_inv(ByRef loComm As SqlCommand, ByVal svrNo_inv As String, _
                                    ByVal ivrNoDisplay As String, _
                                    ByVal svrNoDisplay_s As String, _
                                    ByVal sinvoiceNo As String, _
                                    ByVal davrDate As Date, _
                                    ByVal byvrTypeID As Int16, _
                                    ByVal iStatusID As Integer, _
                                    ByVal siPrincipleCurrID As Int32, _
                                    ByVal sMasterNarration As String, _
                                    ByVal iDetailCodeID As Integer, _
                                    ByVal iSubCodeID As Integer, _
                                    ByVal fPrincipleRate As Double, _
                                    ByVal sCheque_DD_TT As String, _
                                    ByVal daChequeDate As Date, _
                                    ByVal Bvch_PayReceive As String, _
                                    ByVal Bvch_Reference As String, _
                                    ByVal RevREFNo As Int32, _
                                    ByVal iBeneID As Long, _
                                    ByVal iCustID As Long, _
                                    ByVal isChkCanceled As Boolean, _
                                    ByVal isDenomination As Boolean, _
                                    ByVal sREFvrNo As String, _
                                    ByVal byvrTypeID_sys As Int16, _
                                    ByVal sPOSTitle As String, _
                                    ByVal sPOSCardID As String, _
                                    ByVal fPOSBankComm As Double, _
                                    ByVal fPOSExchComm As Double, _
                                    ByVal itrnstRef_DtCd As Integer, _
                                    ByVal itrnstRef_SbCd As Integer, _
                                    ByVal ttValueType As String, _
                                    ByVal iBranchID As Integer, _
                                    ByVal isPayByAcc As Boolean, _
                                    ByVal iCshByID As Integer, _
                                    ByVal IsUpdate As Boolean, _
                                    ByVal ttComm As Double, _
                                    ByVal ttCCY As Integer, _
                                    ByVal ttRate As Double, _
                                    ByVal ttFC As Double, _
                                    ByVal ttLC As Double, _
                                    ByVal Custid_Details As Long, _
                                    ByVal Rep_ID As Long, _
                                    ByVal nUSERID As Integer, _
                                    ByVal nCCID As Integer, _
                                    Optional ByVal isRevEntry As Boolean = 0, _
                                    Optional ByVal sREF_TrNo As String = "0", _
                                    Optional ByVal bisDASP_BalanceAmount As Boolean = 0, _
                                    Optional ByVal iCCID_Posting As Integer = 0, _
                                    Optional ByVal Aux_1 As String = "", _
                                    Optional ByVal Aux_2 As String = "", _
                                    Optional ByVal Aux_3 As String = "", _
                                    Optional ByVal Aux_4 As String = "", _
                                    Optional ByVal Aux_5 As String = "", _
                                    Optional ByVal Aux_6 As String = "", _
                                    Optional ByVal Aux_7 As String = "", _
                                    Optional ByVal Aux_8 As String = "", _
                                    Optional ByVal Aux_9 As String = "", _
                                    Optional ByVal Aux_10 As String = "", _
                                    Optional ByVal Aux_i_1 As Integer = 0, _
                                    Optional ByVal Aux_i_2 As Integer = 0, _
                                    Optional ByVal Aux_i_3 As Integer = 0, _
                                    Optional ByVal Aux_i_4 As Integer = 0, _
                                    Optional ByVal Aux_f_1 As Double = 0, _
                                    Optional ByVal Aux_f_2 As Double = 0, _
                                    Optional ByVal Aux_f_3 As Double = 0, _
                                    Optional ByVal Aux_f_4 As Double = 0, _
                                    Optional ByVal Aux_b_1 As Boolean = 0, _
                                    Optional ByVal Aux_b_2 As Boolean = 0, _
                                    Optional ByVal Aux_b_3 As Boolean = 0, _
                                    Optional ByVal Aux_b_4 As Boolean = 0, _
                                    Optional ByRef svrNoDisplay_s_2 As String = "")

        'Dim nAccountType As Integer
        'Dim loReader As SqlClient.SqlDataReader
        'Dim strTrID As String



        '---------------------------------------------------
        '***************************************************
        'If iCCID_Posting <> goCCID_USER Then
        '    MsgBox("USER CAN DO ENTRY ONLY IN THE DEFAULT BRANCH ")
        '    Throw New Exception("USER CAN DO ENTRY ONLY IN THE DEFAULT BRANCH ......")
        '    Exit Sub
        'End If
        'If DateSerial_SFTX(gREV_DATE_MAX.Year, gREV_DATE_MAX.Month, gREV_DATE_MAX.Day) >= DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day) Then
        '    MsgBox("DATA IS LOCKED --- CAN NOT SAVE/UPDATE ....")
        '    Throw New Exception("System is unable to save. please contact to system administrator...")
        '    Exit Sub
        'End If
        '***************************************************
        '---------------------------------------------------

        'If DateSerial_SFTX(gREV_DATE_MAX.Year, gREV_DATE_MAX.Month, gREV_DATE_MAX.Day) >= DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day) Then
        '    MsgBox("DATA IS LOCKED --- CAN NOT SAVE/UPDATE ....")
        '    Throw New Exception("System is unable to save. please contact to system administrator...")
        '    Exit Sub
        'End If


        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        loComm.CommandText = "stp_tblVchMaster_inv_2"


        With loComm.Parameters

            .Add(New SqlParameter("@svrNo_inv", SqlDbType.VarChar, 64, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, svrNo_inv))

            .Add(New SqlParameter("@ivrNoDisplay", SqlDbType.Int, 4, ParameterDirection.Input, _
             False, 10, 0, "", DataRowVersion.Proposed, CDbl(split_ID(ivrNoDisplay))))

            .Add(New SqlParameter("@svrNoDisplay_s", SqlDbType.VarChar, 20, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, svrNoDisplay_s))

            .Add(New SqlParameter("@sinvoiceNo", SqlDbType.VarChar, 30, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sinvoiceNo & ""))

            .Add(New SqlParameter("@davrDate", SqlDbType.DateTime, 8, ParameterDirection.Input, _
            False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day)))

            .Add(New SqlParameter("@byvrTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, _
            False, 3, 0, "", DataRowVersion.Proposed, byvrTypeID))

            .Add(New SqlParameter("@istatusID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iStatusID))

            .Add(New SqlParameter("@siPrincipleCurrID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
            False, 5, 0, "", DataRowVersion.Proposed, IIf(siPrincipleCurrID = 0, SqlTypes.SqlInt16.Null, siPrincipleCurrID)))

            .Add(New SqlParameter("@sMasterNarration", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sMasterNarration))

            .Add(New SqlParameter("@iDetailCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iDetailCodeID))

            .Add(New SqlParameter("@iSubCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iSubCodeID))

            .Add(New SqlParameter("@fPrincipleRate", SqlDbType.Float, 8, ParameterDirection.Input, _
            False, 38, 0, "", DataRowVersion.Proposed, fPrincipleRate))

            .Add(New SqlParameter("@sCheque_DD_TT", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sCheque_DD_TT & ""))

            .Add(New SqlParameter("@daChequeDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
            False, 16, 0, "", DataRowVersion.Proposed, DateSerial_SFTX(daChequeDate.Year, daChequeDate.Month, daChequeDate.Day)))

            .Add(New SqlParameter("@Bvch_PayReceive", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, Bvch_PayReceive & ""))

            .Add(New SqlParameter("@Bvch_Reference", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, Bvch_Reference & ""))

            .Add(New SqlParameter("@RevREFNo", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, IIf(RevREFNo = 0, System.DBNull.Value, RevREFNo)))

            .Add(New SqlParameter("@iBeneID", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(iBeneID = 0, System.DBNull.Value, iBeneID)))

            .Add(New SqlParameter("@iCustID", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(iCustID = 0, System.DBNull.Value, iCustID)))

            .Add(New SqlParameter("@isChkCanceled", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, isChkCanceled))
            .Add(New SqlParameter("@isDenomination", SqlDbType.Bit, 1, ParameterDirection.Input, _
                          False, 1, 0, "", DataRowVersion.Proposed, isDenomination))


            .Add(New SqlParameter("@sREFvrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, _
           False, 0, 0, "", DataRowVersion.Proposed, IIf(Len(Trim(sREFvrNo)) = 0, System.DBNull.Value, sREFvrNo)))

            .Add(New SqlParameter("@byvrTypeID_sys", SqlDbType.TinyInt, 1, ParameterDirection.Input, _
                        False, 3, 0, "", DataRowVersion.Proposed, IIf(byvrTypeID_sys = 0, System.DBNull.Value, byvrTypeID_sys)))

            .Add(New SqlParameter("@sPOSTitle", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sPOSTitle))
            .Add(New SqlParameter("@sPOSCardID", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, sPOSCardID))


            .Add(New SqlParameter("@fPOSBankComm", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, fPOSBankComm))
            .Add(New SqlParameter("@fPOSExchComm", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, fPOSExchComm))


            .Add(New SqlParameter("@itrnstRef_DtCd", SqlDbType.Int, 4, ParameterDirection.Input, _
             False, 10, 0, "", DataRowVersion.Proposed, IIf(itrnstRef_DtCd = 0, System.DBNull.Value, itrnstRef_DtCd)))
            .Add(New SqlParameter("@itrnstRef_SbCd", SqlDbType.Int, 4, ParameterDirection.Input, _
                        False, 10, 0, "", DataRowVersion.Proposed, IIf(itrnstRef_SbCd = 0, System.DBNull.Value, itrnstRef_SbCd)))


            .Add(New SqlParameter("@sttValueType", SqlDbType.VarChar, 30, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, ttValueType))


            .Add(New SqlParameter("@iUserID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, nUSERID))


            .Add(New SqlParameter("@iCCID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, nCCID))


            .Add(New SqlParameter("@isPayByAcc", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, isPayByAcc))

            .Add(New SqlParameter("@isRevEntry", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, isRevEntry))


            .Add(New SqlParameter("@sREF_TrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sREF_TrNo))

            .Add(New SqlParameter("@isDASP_BalanceAmount", SqlDbType.Bit, 1, ParameterDirection.Input, _
                     False, 1, 0, "", DataRowVersion.Proposed, bisDASP_BalanceAmount))

            .Add(New SqlParameter("@iCshByID", SqlDbType.Int, 4, ParameterDirection.Input, _
                       False, 10, 0, "", DataRowVersion.Proposed, iCshByID))
            .Add(New SqlParameter("@iCCID_Posting", SqlDbType.Int, 4, ParameterDirection.Input, _
                                   False, 10, 0, "", DataRowVersion.Proposed, iCCID_Posting))



            .Add(New SqlParameter("@ttComm", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttComm))

            .Add(New SqlParameter("@ttCCY", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
            False, 5, 0, "", DataRowVersion.Proposed, ttCCY))

            .Add(New SqlParameter("@ttRate", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttRate))
            .Add(New SqlParameter("@ttFC", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttFC))
            .Add(New SqlParameter("@ttLC", SqlDbType.Float, 8, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, ttLC))


            .Add(New SqlParameter("@Custid_Details", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(Custid_Details = 0, System.DBNull.Value, Custid_Details)))


            .Add(New SqlParameter("@Rep_ID", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, IIf(Rep_ID = 0, System.DBNull.Value, Rep_ID)))





            .Add(New SqlParameter("@BlockID", SqlDbType.Int, 2, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, 1))

            '--************************************************************************
            '-- AUX Adjustment

            .Add(New SqlParameter("@Aux_1", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, Aux_1))
            .Add(New SqlParameter("@Aux_2", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_2))
            .Add(New SqlParameter("@Aux_3", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_3))
            .Add(New SqlParameter("@Aux_4", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_4))
            .Add(New SqlParameter("@Aux_5", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_5))
            .Add(New SqlParameter("@Aux_6", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_6))
            .Add(New SqlParameter("@Aux_7", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_7))
            .Add(New SqlParameter("@Aux_8", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_8))
            .Add(New SqlParameter("@Aux_9", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_9))
            .Add(New SqlParameter("@Aux_10", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_10))


            .Add(New SqlParameter("@Aux_i_1", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_1))

            .Add(New SqlParameter("@Aux_i_2", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_2))

            .Add(New SqlParameter("@Aux_i_3", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_3))

            .Add(New SqlParameter("@Aux_i_4", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_4))



            .Add(New SqlParameter("@aux_f_1", SqlDbType.Float, 8, ParameterDirection.Input, _
                  False, 38, 0, "", DataRowVersion.Proposed, Aux_f_1))
            .Add(New SqlParameter("@aux_f_2", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_2))
            .Add(New SqlParameter("@aux_f_3", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_3))
            .Add(New SqlParameter("@aux_f_4", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_4))


            .Add(New SqlParameter("@Aux_b_1", SqlDbType.Bit, 1, ParameterDirection.Input, _
                      False, 1, 0, "", DataRowVersion.Proposed, Aux_b_1))
            .Add(New SqlParameter("@Aux_b_2", SqlDbType.Bit, 1, ParameterDirection.Input, _
               False, 1, 0, "", DataRowVersion.Proposed, Aux_b_2))
            .Add(New SqlParameter("@Aux_b_3", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                 False, 1, 0, "", DataRowVersion.Proposed, Aux_b_3))
            .Add(New SqlParameter("@Aux_b_4", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                 False, 1, 0, "", DataRowVersion.Proposed, Aux_b_4))

            '--************************************************************************
            '--************************************************************************

            .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
                      False, 1, 0, "", DataRowVersion.Proposed, IsUpdate))


            .Add(New SqlParameter("@svrNoDisplay_s_2", SqlDbType.VarChar, 20, ParameterDirection.Output, _
            False, 0, 0, "", DataRowVersion.Proposed, svrNoDisplay_s_2))


        End With


        Try
            loComm.ExecuteNonQuery()
            svrNoDisplay_s_2 = objComm.Parameters.Item("@svrNoDisplay_s_2").Value

        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator...")

        End Try

    End Sub



    Public Sub get_FWD_Searach_Qry(ByVal nVRTYPE_ID As Integer, _
                                   ByVal nCCYID As Integer, _
                                   ByVal CustID_GID As String, _
                                   ByRef str_SQL As String)
        Try


            '--------------------------------------------------------


            str_SQL = "SELECT   top 200  A.vrNoDisplay_s AS [VR No], A.CurrencyShortName AS CCY, " _
                            & " (isnull(A.Amount,0) - ( isnull( B.set_Amount,0) +   ISNULL(TT_INPUT.set_Amount,0) + isnull( TCD.AMT_TCD,0) )) as Amount, A.detail_rate as Rate,A.vrdate,A.vrno     " _
                            & " FROM (SELECT  TVm.vrno,  TVm.vrNoDisplay_s, TCCY.CurrencyShortName, TVD.Amount,TVD.detail_rate,TVm.vrDate " _
                            & " FROM   tblVchDetail AS TVD INNER JOIN tblVchMaster AS TVm " _
                            & " ON TVD.vrNo = TVm.vrNo INNER JOIN  dbo.tblCurrency AS TCCY ON " _
                            & " TVD.detail_CurrencyID = TCCY.CurrencyID WHERE " _
                            & " ((TVm.vrTypeID = " & nVRTYPE_ID & ")  " _
                            & " AND (TVm.cust_ID_GID = '" & CustID_GID & "')  " _
                            & " AND (TVD.sqNo <> 0)   " _
                            & "   " _
                            & " AND (TVD.detail_CurrencyID = " & nCCYID & " ))  ) A " _
                            & " LEFT OUTER JOIN (SELECT  sum(TVD.Amount) AS set_Amount, TVD.FWDealNo_Allocation FROM  " _
                            & " tblVchDetail AS TVD INNER JOIN tblVchMaster AS TVm ON TVD.vrNo = TVm.vrNo " _
                            & " WHERE    " _
                            & "   (TVD.sqNo <> 0) Group by TVD.FWDealNo_Allocation ) B on  " _
                            & " b.FWDealNo_Allocation = a.VRNO " _
                            & "LEFT OUTER JOIN " _
                            & "( " _
                            & "SELECT     sum(TC.Amount_Allocation) AS AMT_TCD,FWDealNo_Allocation  FROM  " _
                            & "       tblTransaction AS TR INNER JOIN " _
                            & "tblTrChild AS TC ON TR.TrNo = TC.TrNo  " _
                            & "                   where  " _
                            & "  LEN(FWDealNo_Allocation)>2 " _
                            & "group by FWDealNo_Allocation " _
                            & " )TCD " _
                            & "on A.vrNo = TCD.FWDealNo_Allocation " _
                            & "    LEFT OUTER JOIN  " _
                            & " (SELECT  sum(Amount_FC ) AS set_Amount, FWDealNo_Allocation FROM tbl_TT_App_input  " _
                            & " WHERE cust_ID_GID = '" & CustID_GID & "' group by FWDealNo_Allocation   ) TT_INPUT  " _
                            & " on   TT_INPUT.FWDealNo_Allocation = a.VRNO  " _
                            & " WHERE   (isnull(A.Amount,0) - ( isnull( B.set_Amount,0) +   ISNULL(TT_INPUT.set_Amount,0) + isnull( TCD.AMT_TCD,0) )) > 0  "

        Catch ex As Exception
            Throw New Exception
        End Try
    End Sub


    Public Sub saveDetail_Fun_loComm_Grg(ByRef loComm As SqlCommand, ByVal ID_WRK_DET As Integer, _
                            ByVal ID_WRK As Integer, _
                            ByVal ID_JOB As Integer, _
         ByVal startDate As Date, _
         ByVal esstimatedEndDate As Date, _
        ByVal endDate As Date, _
        ByVal Details As String, _
        ByVal Remarksa As String, _
        ByVal inTime As Boolean, _
        ByVal Cost As Double, _
        ByVal NEWCOST As Double, _
        ByVal ResonOF As String, ByVal Isupdate As Boolean)




        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        loComm.CommandText = "stp_tbl_GRG_WORK_Details_InsertUpdate"

        With loComm.Parameters
            '--**********************************************
            '-- Case DB Entry = NoChange
            '--**********************************************









            .Add(New SqlParameter("@iID_WRK_DET", SqlDbType.Int, 8, ParameterDirection.Input, _
                False, 0, 0, "", DataRowVersion.Proposed, ID_WRK_DET))
            .Add(New SqlParameter("@iID_WRK", SqlDbType.Int, 8, ParameterDirection.Input, _
                False, 5, 0, "", DataRowVersion.Proposed, ID_WRK))

            .Add(New SqlParameter("@iID_JOB", SqlDbType.Int, 8, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
                ID_JOB))


            .Add(New SqlParameter("@dstartDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
              DateSerial_SFTX(startDate.Year, startDate.Month, startDate.Day)))

            .Add(New SqlParameter("@desstimatedEndDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
             False, 10, 0, "", DataRowVersion.Proposed, _
           DateSerial_SFTX(esstimatedEndDate.Year, esstimatedEndDate.Month, esstimatedEndDate.Day)))

            .Add(New SqlParameter("@dendDate", SqlDbType.SmallDateTime, 4, ParameterDirection.Input, _
             False, 10, 0, "", DataRowVersion.Proposed, _
           DateSerial_SFTX(endDate.Year, endDate.Month, endDate.Day)))



            .Add(New SqlParameter("@sDetails", SqlDbType.NVarChar, 250, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, _
               Details))

            .Add(New SqlParameter("@sRemarksa", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
               False, 0, 0, "", DataRowVersion.Proposed, Remarksa))


            .Add(New SqlParameter("@binTime", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, inTime))
            .Add(New SqlParameter("@fCost", SqlDbType.Float, 8, ParameterDirection.Input, _
                    False, 38, 0, "", DataRowVersion.Proposed, Cost))

            .Add(New SqlParameter("@fNEWCOST", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, NEWCOST))

            .Add(New SqlParameter("@sResonOF", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
            False, 19, 0, "", DataRowVersion.Proposed, ResonOF))

            .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, Isupdate))


            '--**********************************************
            '-- Case CR Entry = NoChange
            '--**********************************************
        End With

        Try
            loComm.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator..." & ex.Message)
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
    Public Sub saveDetail_Fun_loComm_INV(ByRef loComm As SqlCommand, ByVal lsvrNo_INV As String, _
                            ByVal lisqNo As Integer, _
                            ByVal type_id As Integer, _
         ByVal SubCodeID As Integer, _
         ByVal Rate As Double, _
        ByVal Description As String, _
        ByVal IsRC As Boolean, _
        ByVal MnRC As Boolean, _
        ByVal RCID As Long, _
        ByVal RCID2 As Long, _
        ByVal ITEM_ID As Integer, _
        ByVal GROUP_ID As Integer, _
        ByVal QTY As Double, _
        ByVal Amount As Double, _
        ByVal NETAmount As Double, _
        ByVal Discount As Double, _
        ByVal aux_1 As Integer, _
        ByVal aux_2 As Integer, _
        ByVal aux_3 As Integer, _
        ByVal aux_f_1 As Double, _
        ByVal aux_f_2 As Double, _
        ByVal aux_f_3 As Double, _
        ByVal aux_b_1 As Boolean, _
        ByVal aux_b_2 As Boolean, _
        ByVal aux_b_3 As Boolean)





        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        loComm.CommandText = "stp_tblVchDetail_Insert_INV"

        With loComm.Parameters
            '--**********************************************
            '-- Case DB Entry = NoChange
            '--**********************************************


            .Add(New SqlParameter("@vrNo_INV", SqlDbType.VarChar, 64, ParameterDirection.Input, _
                False, 0, 0, "", DataRowVersion.Proposed, lsvrNo_INV))
            .Add(New SqlParameter("@sqNo", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                False, 5, 0, "", DataRowVersion.Proposed, lisqNo))

            .Add(New SqlParameter("@type_id", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
                type_id))


            .Add(New SqlParameter("@SubCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
               SubCodeID))



            .Add(New SqlParameter("@Rate", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, _
                 Math.Abs(Rate)))

            .Add(New SqlParameter("@Description", SqlDbType.VarChar, 50, ParameterDirection.Input, _
               False, 0, 0, "", DataRowVersion.Proposed, Description))


            .Add(New SqlParameter("@IsRC", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, IsRC))
            .Add(New SqlParameter("@MnRC", SqlDbType.Bit, 1, ParameterDirection.Input, _
             False, 1, 0, "", DataRowVersion.Proposed, MnRC))

            .Add(New SqlParameter("@RCID", SqlDbType.BigInt, 8, ParameterDirection.Input, _
            False, 19, 0, "", DataRowVersion.Proposed, RCID))
            .Add(New SqlParameter("@RCID2", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, RCID2))


            .Add(New SqlParameter("@ITEM_ID", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, ITEM_ID))
            .Add(New SqlParameter("@GROUP_ID", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, GROUP_ID))


            .Add(New SqlParameter("@QTY", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(QTY)))
            .Add(New SqlParameter("@Amount", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(Amount)))
            .Add(New SqlParameter("@NETAmount", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(NETAmount)))
            .Add(New SqlParameter("@Discount", SqlDbType.Float, 8, ParameterDirection.Input, _
                           False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(Discount)))


            .Add(New SqlParameter("@aux_1", SqlDbType.Int, 4, ParameterDirection.Input, _
               False, 10, 0, "", DataRowVersion.Proposed, aux_1))
            .Add(New SqlParameter("@aux_2", SqlDbType.Int, 4, ParameterDirection.Input, _
                           False, 10, 0, "", DataRowVersion.Proposed, aux_2))
            .Add(New SqlParameter("@aux_3", SqlDbType.Int, 4, ParameterDirection.Input, _
                                       False, 10, 0, "", DataRowVersion.Proposed, aux_3))


            .Add(New SqlParameter("@aux_f_1", SqlDbType.Float, 8, ParameterDirection.Input, _
               False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(aux_f_1)))
            .Add(New SqlParameter("@aux_f_2", SqlDbType.Float, 8, ParameterDirection.Input, _
                          False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(aux_f_2)))
            .Add(New SqlParameter("@aux_f_3", SqlDbType.Float, 8, ParameterDirection.Input, _
                          False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(aux_f_3)))


            .Add(New SqlParameter("@aux_b_1", SqlDbType.Bit, 1, ParameterDirection.Input, _
           False, 1, 0, "", DataRowVersion.Proposed, aux_b_1))
            .Add(New SqlParameter("@aux_b_2", SqlDbType.Bit, 1, ParameterDirection.Input, _
           False, 1, 0, "", DataRowVersion.Proposed, aux_b_2))
            .Add(New SqlParameter("@aux_b_3", SqlDbType.Bit, 1, ParameterDirection.Input, _
           False, 1, 0, "", DataRowVersion.Proposed, aux_b_3))


            '.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, nERRCODE))



            '--**********************************************
            '-- Case CR Entry = NoChange
            '--**********************************************
        End With
        'Dim sID_ERR_OUT As String
        Try
            loComm.ExecuteNonQuery()
            'sID_ERR_OUT = loComm.Parameters.Item("@iErrorCode").Value

        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator..." & ex.Message)
        End Try



    End Sub















    Public Function saveDetail_Fun_loComm(ByRef loComm As SqlCommand, ByVal lsvrNo As String, _
                               ByVal lisqNo As Integer, _
                               ByVal lDetailCodeID As Integer, ByVal lSubCodeID As Integer, _
                               ByVal fAmount As Double, ByVal fBCAmount As Double, _
                               ByVal sDescription As String, _
                               ByVal lDetailCodeID_1 As Integer, ByVal lSubCodeID_1 As Integer, _
                               ByVal ldetail_CurrencyID As Double, ByVal fdetail_Rate As Double, _
                               ByVal fdealRate As Double, _
                               ByVal isBALEntry As Boolean, _
                               ByVal stkID As Integer, _
                               ByVal lisDRCol As Boolean, Optional ByVal ttVrNo_ToFund As String = "", _
                               Optional ByVal prcelNo_Allocation As String = "", _
                               Optional ByVal FWDealNo_Allocation As String = "", _
                                Optional ByVal dtcd_REF As Integer = 0, _
                                 Optional ByVal sbcd_REF As Integer = 0, _
                                  Optional ByVal sDescription_REF As String = "") As Long

        sDescription = UCase(sDescription)

        'sDescription = Replace(sDescription, "&nbsp;", ".")


        Dim m_lVrDetailID As Long = 0
        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        loComm.CommandText = "stp_tblVchDetail_Insert_GID"

        With loComm.Parameters
            '--**********************************************
            '-- Case DB Entry = NoChange
            '--**********************************************
            '-- ******************************************
            If Len(sDescription) > 100 Then
                sDescription = Mid(sDescription, 1, 100)
            End If

            '-- ******************************************
            .Add(New SqlParameter("@svrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, _
                False, 0, 0, "", DataRowVersion.Proposed, lsvrNo))
            .Add(New SqlParameter("@sisqNo", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                False, 5, 0, "", DataRowVersion.Proposed, lisqNo))
            .Add(New SqlParameter("@iDetailCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
               False, 10, 0, "", DataRowVersion.Proposed, lDetailCodeID))

            .Add(New SqlParameter("@iSubCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
                lSubCodeID))

            .Add(New SqlParameter("@fAmount", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, _
                 Math.Abs(fAmount)))

            .Add(New SqlParameter("@fBCAmount", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(fBCAmount)))

            .Add(New SqlParameter("@sDescription", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
                False, 0, 0, "", DataRowVersion.Proposed, sDescription))

            .Add(New SqlParameter("@iDetailCodeID_1", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
                lDetailCodeID_1))

            .Add(New SqlParameter("@iSubCodeID_1", SqlDbType.Int, 4, ParameterDirection.Input, _
              False, 10, 0, "", DataRowVersion.Proposed, _
               lSubCodeID_1))

            If ldetail_CurrencyID = 0 Then
                .Add(New SqlParameter("@sidetail_CurrencyID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                False, 5, 0, "", DataRowVersion.Proposed, System.DBNull.Value))

            Else
                .Add(New SqlParameter("@sidetail_CurrencyID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                False, 5, 0, "", DataRowVersion.Proposed, ldetail_CurrencyID))
            End If
            'fdetail_Rate
            .Add(New SqlParameter("@fdetail_Rate", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, fdetail_Rate))


            .Add(New SqlParameter("@bisFrmDrColumn", SqlDbType.Bit, 1, ParameterDirection.Input, _
                False, 1, 0, "", DataRowVersion.Proposed, _
                lisDRCol))
            .Add(New SqlParameter("@bApprovedByTeller", SqlDbType.Bit, 1, ParameterDirection.Input, _
                False, 1, 0, "", DataRowVersion.Proposed, 0))
            .Add(New SqlParameter("@lEntryNo", SqlDbType.BigInt, 8, ParameterDirection.Input, _
                False, 19, 0, "", DataRowVersion.Proposed, SqlTypes.SqlInt64.Null))

            .Add(New SqlParameter("@fdealRate", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, fdealRate))

            '.Add(New SqlParameter("@bisCCYStkEntry", SqlDbType.Bit, 1, ParameterDirection.Input, _
            '    False, 1, 0, "", DataRowVersion.Proposed, _
            '    isCCYStkEntry))

            .Add(New SqlParameter("@bisBALEntry", SqlDbType.Bit, 1, ParameterDirection.Input, _
              False, 1, 0, "", DataRowVersion.Proposed, _
              isBALEntry))

            .Add(New SqlParameter("@sistkID", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                            False, 5, 0, "", DataRowVersion.Proposed, stkID))

            .Add(New SqlParameter("@sttVrNo_ToFund", SqlDbType.NVarChar, 64, ParameterDirection.Input, _
               False, 0, 0, "", DataRowVersion.Proposed, ttVrNo_ToFund))

            .Add(New SqlParameter("@sprcelNo_Allocation", SqlDbType.NVarChar, 64, ParameterDirection.Input, _
                           False, 0, 0, "", DataRowVersion.Proposed, prcelNo_Allocation))

            .Add(New SqlParameter("@sFWDealNo_Allocation", SqlDbType.NVarChar, 64, ParameterDirection.Input, _
                                    False, 0, 0, "", DataRowVersion.Proposed, FWDealNo_Allocation))


            .Add(New SqlParameter("@dtcd_Ref", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, dtcd_REF))

            .Add(New SqlParameter("@sbcd_Ref", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
                sbcd_REF))

            .Add(New SqlParameter("@sDescription_ref", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
           False, 0, 0, "", DataRowVersion.Proposed, sDescription_REF))

            '--**********************************************
            '-- Case CR Entry = NoChange
            '--**********************************************
        End With

        'loComm.ExecuteNonQuery()

        Try

            If Math.Abs(fAmount) <> 0 Then
                loComm.ExecuteNonQuery()
            End If


        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator..." & ex.Message)
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
            Next

        Catch ex As Exception
            strERR = (ex.Message)
        Finally
            dtTb.Dispose()
            dtTb = Nothing
        End Try


    End Sub




    Public Sub get_Mirror_FWD(str_FWD_vrNo As String, _
                                ByRef str_DealID_VRNO_Mirror As String, _
                                ByRef str_AMT_FWD_Mirror As String, _
                                ByRef str_DealID_Mirror As String, _
                                Optional str_cust_ID_GID_Mirror As String = "")

        Dim dtTb_data As New DataTable
        Dim i As Integer
        Try

            Dim objPara(0) As clsPara

            setPara(objPara(0), "FWD_VRNO", str_FWD_vrNo, "50", clsPara.ColType.Varchar)
            dtTb_data = DTTB_Fill_Generic("stp_get_FWD_mirror", objPara)

            For i = 0 To dtTb_data.Rows.Count - 1
                str_DealID_VRNO_Mirror = dtTb_data.Rows(i).Item("vrno")
                str_AMT_FWD_Mirror = dtTb_data.Rows(i).Item("Amount")
                str_DealID_Mirror = dtTb_data.Rows(i).Item("vrNoDisplay_s")
                str_cust_ID_GID_Mirror = dtTb_data.Rows(i).Item("cust_ID_GID")
            Next


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dtTb_data = Nothing
        End Try
    End Sub




    Public Sub setPara(ByRef Para As clsPara, ByVal sName As String, ByVal sVal As String, ByVal sSize As String, ByVal sType As String)
        Try
            Para = New clsPara()
            Para.prpName = sName
            Para.prpVAL = sVal
            Para.prpSIZE = sSize
            Para.prpType = sType

        Catch ex As Exception
            Throw New Exception("System is unable to execute. please contact to system administrator..." & ex.Message)

        End Try
    End Sub



    '*************************************************************************************************
    '*************************************************************************************************



    '*************************************************************************************************
    '*************************************************************************************************


    Public Sub RndVAL_NAFEX(ByRef nVl As Double, ByRef nFRC As Double, _
                   ByRef isD As Boolean, _
                  ByRef isR As Boolean)

        Dim Val1, Val2, Val3 As Double
        'If nVl > 1 Then
        Val1 = Math.Floor(nVl)
        Val2 = Math.Round((nVl - Val1), 3)
        Val3 = nVl


        isR = True

        Dim nFig, nFig_RND As Double
        If Val2 = 0 _
        Or (Math.Round(Val2, 3) Mod 0.005 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.01 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.015 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.02 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.025 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.03 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.035 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.04 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.045 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.05 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.055 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.06 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.065 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.07 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.075 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.08 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.085 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.09 = 0) _
        Or (Math.Round(Val2, 3) Mod 0.095 = 0) Then
            isR = False

            Exit Sub
        End If

        If Val2 <= 0.002 Then
            nFRC = Val2
            nVl = Math.Floor(nVl)
            isD = False
            Exit Sub
        End If

        nFig = 0.008
        nFig_RND = 0.005
        'For i = 1 To 100

        While nFig_RND <= 1
            'If nFig_RND >= 1 Then Exit Sub
            If Val2 < nFig Then
                If Val2 < nFig_RND Then
                    isD = True
                Else
                    isD = False
                End If
                nFRC = Math.Round(Math.Abs(nFig_RND - Val2), 3)
                nVl = Math.Floor(nVl) + nFig_RND
                nVl = Math.Round(nVl, 3)
                Exit Sub
            End If
            nFig = Math.Round((nFig + 0.005), 3)
            nFig_RND = Math.Round((nFig_RND + 0.005), 3)
            'Next
        End While

    End Sub




    Public Sub RndVAL_25(ByRef nVl As Double, ByRef nFRC As Double)

        Dim Val1, Val2, Val3 As Double
        'If nVl > 1 Then
        Val1 = Math.Floor(nVl)
        Val2 = Math.Round((nVl - Val1), 2)
        Val3 = nVl



        Dim nFig As Double
        If Val2 = 0 _
        Or (Math.Round(Val2, 2) Mod 0.25 = 0) _
        Or (Math.Round(Val2, 2) Mod 0.5 = 0) _
        Or (Math.Round(Val2, 2) Mod 0.75 = 0) Then

            Exit Sub
        End If




        '-----------------------------------------------------------------
        '*****************************************************************
        nFig = 0.01
        Dim nLVL As Double = 0.25
        Dim nCompVal As Double = 0.0
        Dim isGET As Boolean = False
        While nFig <= 1
            'If nFig_RND >= 1 Then Exit Sub
            If Val2 <= nFig Then
                Dim nRnd As Double
                '-----------------------------
                If nFig <= 0.12 Then
                    nRnd = nFig * (-1)
                    nFRC = nRnd
                    Exit Sub
                End If
                '-----------------------------
                If nFig <= 0.37 Then
                    nCompVal = 0.25
                    isGET = True
                ElseIf nFig <= 0.62 Then
                    nCompVal = 0.5
                    isGET = True
                ElseIf nFig <= 0.87 Then
                    nCompVal = 0.75
                    isGET = True
                Else
                    nCompVal = 1
                    isGET = True
                End If


                If isGET Then
                    'If nCompVal >= nFig Then
                    nRnd = (nCompVal - nFig)
                    'End If
                    'If nCompVal < nFig Then
                    '    nRnd = (nFig - nCompVal)
                    'End If
                    nFRC = nRnd
                    Exit While
                End If







            End If
            nFig = Math.Round((nFig + 0.01), 2)

        End While

    End Sub

    Public Sub RndVAL_25_WITH_AMT(ByRef nVl As Double, ByRef nFRC As Double, ByRef nAMT_NET As Double)

        Dim Val1, Val2, Val3 As Double
        'If nVl > 1 Then
        Val1 = Math.Floor(nVl)
        Val2 = Math.Round((nVl - Val1), 2)
        Val3 = nVl



        Dim nFig As Double
        If Val2 = 0 _
        Or (Math.Round(Val2, 2) Mod 0.25 = 0) _
        Or (Math.Round(Val2, 2) Mod 0.5 = 0) _
        Or (Math.Round(Val2, 2) Mod 0.75 = 0) Then
            nFRC = 0
            nAMT_NET = nAMT_NET
            Exit Sub
        End If




        '-----------------------------------------------------------------
        '*****************************************************************
        nFig = 0.01
        Dim nLVL As Double = 0.25
        Dim nCompVal As Double = 0.0
        Dim isGET As Boolean = False

        While nFig <= 1
            'If nFig_RND >= 1 Then Exit Sub
            If Val2 <= nFig Then
                Dim nRnd As Double
                '-----------------------------
                If nFig <= 0.12 Then
                    nRnd = nFig * (-1)
                    nFRC = nRnd
                    nAMT_NET = nAMT_NET + nRnd
                    Exit Sub
                End If
                '-----------------------------
                If nFig <= 0.37 Then
                    nCompVal = 0.25
                    isGET = True
                ElseIf nFig <= 0.62 Then
                    nCompVal = 0.5
                    isGET = True
                ElseIf nFig <= 0.87 Then
                    nCompVal = 0.75
                    isGET = True
                Else
                    nCompVal = 1
                    isGET = True
                End If


                If isGET Then
                    'If nCompVal >= nFig Then
                    nRnd = (nCompVal - nFig)
                    'End If
                    'If nCompVal < nFig Then
                    '    nRnd = (nFig - nCompVal)
                    'End If
                    nFRC = nRnd
                    nAMT_NET = nAMT_NET + nRnd
                    Exit While
                End If







            End If
            nFig = Math.Round((nFig + 0.01), 2)

        End While

    End Sub





    Public Sub get_MasterRefNo(ByRef str_Master_Ref_No As String)


        Try
            setCon(True)
            strSql = "select (FORMAT(GETDATE(),'yyMMddHHmmsss') + format(datepart(ms,getdate())  ,'000')) MasterRefNo"

            objComm.Connection = gCon
            objComm.CommandText = strSql
            str_Master_Ref_No = objComm.ExecuteScalar()
        Catch ex As Exception
            str_Master_Ref_No = ""
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
        End Try

    End Sub




    Public Sub saveVchMaster_Fun_budget_INV_GEN(ByRef loComm As SqlCommand, ByVal svrNo_inv As String, _
                                 ByVal ivrNoDisplay As String, _
                                 ByVal svrNoDisplay_s As String, _
                                 ByVal sinvoiceNo As String, _
                                 ByVal davrDate As Date, _
                                 ByVal byvrTypeID As Int16, _
                                 ByVal iStatusID As Integer, _
                                 ByVal sMasterNarration As String, _
                                 ByVal iDetailCodeID As Integer, _
                                 ByVal iSubCodeID As Integer, _
                                 ByVal REFvrNo As String, _
                                 ByVal AMC_YEAR_ID As Int32, _
                                 ByVal AMC_TYPE_ID As Int32, _
                                 ByVal AMC_START_DATE As Date, _
                                 ByVal AMC_END_DATE As Date, _
                                 ByVal CCID As Int32, _
                                 ByVal ccid_GID As String, _
                                 ByVal ccid_GID_Master As String, _
                                 ByVal IsUpdate As Boolean, _
                                 ByVal svrNoDisplay_s_2 As String, _
                                 ByVal Aux_dt_1 As Date, _
                                  ByVal Aux_dt_2 As Date, _
                                  ByVal Aux_dt_3 As Date, _
                                  ByVal Aux_dt_4 As Date, _
                                  ByVal Aux_dt_5 As Date, _
                                  ByVal Aux_dt_6 As Date, _
                                  ByVal Aux_dt_7 As Date, _
                                 Optional ByVal Aux_1 As String = "", _
                                 Optional ByVal Aux_2 As String = "", _
                                 Optional ByVal Aux_3 As String = "", _
                                 Optional ByVal Aux_4 As String = "", _
                                 Optional ByVal Aux_5 As String = "", _
                                 Optional ByVal Aux_6 As String = "", _
                                 Optional ByVal Aux_7 As String = "", _
                                 Optional ByVal Aux_8 As String = "", _
                                 Optional ByVal Aux_9 As String = "", _
                                 Optional ByVal Aux_10 As String = "", _
                                 Optional ByVal Aux_i_1 As Integer = 0, _
                                 Optional ByVal Aux_i_2 As Integer = 0, _
                                 Optional ByVal Aux_i_3 As Integer = 0, _
                                 Optional ByVal Aux_i_4 As Integer = 0, _
                                 Optional ByVal Aux_f_1 As Double = 0, _
                                 Optional ByVal Aux_f_2 As Double = 0, _
                                 Optional ByVal Aux_f_3 As Double = 0, _
                                 Optional ByVal Aux_f_4 As Double = 0, _
                                 Optional ByVal Aux_b_1 As Boolean = 0, _
                                 Optional ByVal Aux_b_2 As Boolean = 0, _
                                 Optional ByVal Aux_b_3 As Boolean = 0, _
                                 Optional ByVal Aux_b_4 As Boolean = 0, _
                                 Optional ByVal Mark_To As String = "", _
                                 Optional ByVal Payment_Terms As String = "", _
                                 Optional ByVal QUT_Note As String = "", _
                                 Optional ByVal DONOTE_ADD As String = "", _
                                 Optional ByVal Customer_ADD As String = "", _
                                 Optional ByVal Aux_1_big As String = "", _
                                 Optional ByVal Aux_2_big As String = "", _
                                 Optional ByRef Aux_3_big As String = "", _
                                 Optional ByVal Aux_4_big As String = "", _
                                 Optional ByVal Aux_5_big As String = "", _
                                 Optional ByVal Aux_b_5 As Boolean = False, _
                                 Optional ByVal Aux_b_6 As Boolean = False, _
                                 Optional ByVal Aux_b_7 As Boolean = False, _
                                 Optional ByVal Aux_b_8 As Boolean = False, _
                                 Optional ByVal Aux_b_9 As Boolean = False, _
                                 Optional ByVal Aux_b_10 As Boolean = False, _
                                 Optional ByVal vrTypeID_sys As Integer = 0)






        'Dim nAccountType As Integer
        'Dim loReader As SqlClient.SqlDataReader
        'Dim strTrID As String


        '---------------------------------------------------
        '***************************************************
        'If iCCID_Posting <> goCCID_USER Then
        '    MsgBox("USER CAN DO ENTRY ONLY IN THE DEFAULT BRANCH ")
        '    Throw New Exception("USER CAN DO ENTRY ONLY IN THE DEFAULT BRANCH ......")
        '    Exit Sub
        'End If
        'If DateSerial_SFTX(gREV_DATE_MAX.Year, gREV_DATE_MAX.Month, gREV_DATE_MAX.Day) >= DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day) Then
        '    MsgBox("DATA IS LOCKED --- CAN NOT SAVE/UPDATE ....")
        '    Throw New Exception("System is unable to save. please contact to system administrator...")
        '    Exit Sub
        'End If
        '***************************************************
        '---------------------------------------------------

        'If DateSerial_SFTX(gREV_DATE_MAX.Year, gREV_DATE_MAX.Month, gREV_DATE_MAX.Day) >= DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day) Then
        '    MsgBox("DATA IS LOCKED --- CAN NOT SAVE/UPDATE ....")
        '    Throw New Exception("System is unable to save. please contact to system administrator...")
        '    Exit Sub
        'End If


        '------------------------------------------------------
        Aux_1 = Trim(Replace(Aux_1, "&nbsp;", ""))
        Aux_2 = Trim(Replace(Aux_2, "&nbsp;", ""))
        Aux_3 = Trim(Replace(Aux_3, "&nbsp;", ""))
        Aux_4 = Trim(Replace(Aux_4, "&nbsp;", ""))
        Aux_5 = Trim(Replace(Aux_5, "&nbsp;", ""))
        Aux_6 = Trim(Replace(Aux_6, "&nbsp;", ""))
        Aux_7 = Trim(Replace(Aux_7, "&nbsp;", ""))
        Aux_8 = Trim(Replace(Aux_8, "&nbsp;", ""))
        Aux_9 = Trim(Replace(Aux_9, "&nbsp;", ""))
        Aux_10 = Trim(Replace(Aux_10, "&nbsp;", ""))


        Mark_To = Trim(Replace(Mark_To, "&nbsp;", ""))
        Payment_Terms = Trim(Replace(Payment_Terms, "&nbsp;", ""))
        QUT_Note = Trim(Replace(QUT_Note, "&nbsp;", ""))
        DONOTE_ADD = Trim(Replace(DONOTE_ADD, "&nbsp;", ""))
        Customer_ADD = Trim(Replace(Customer_ADD, "&nbsp;", ""))
        '------------------------------------------------------



        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        '   loComm.CommandText = "stp_tblVchMaster_inv_2"
        'loComm.CommandText = "stp_tblVchMaster_inv_3"
        loComm.CommandText = "stp_tblVchMaster_budget_INV_GEN"


        With loComm.Parameters

            .Add(New SqlParameter("@svrNo_inv", SqlDbType.VarChar, 64, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, svrNo_inv))

            .Add(New SqlParameter("@ivrNoDisplay", SqlDbType.Int, 4, ParameterDirection.Input, _
             False, 10, 0, "", DataRowVersion.Proposed, CDbl(split_ID(ivrNoDisplay))))

            .Add(New SqlParameter("@svrNoDisplay_s", SqlDbType.VarChar, 20, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, svrNoDisplay_s))

            .Add(New SqlParameter("@sinvoiceNo", SqlDbType.VarChar, 30, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sinvoiceNo & ""))

            .Add(New SqlParameter("@davrDate", SqlDbType.DateTime, 8, ParameterDirection.Input, _
            False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(davrDate.Year, davrDate.Month, davrDate.Day)))

            .Add(New SqlParameter("@byvrTypeID", SqlDbType.TinyInt, 1, ParameterDirection.Input, _
            False, 3, 0, "", DataRowVersion.Proposed, byvrTypeID))

            .Add(New SqlParameter("@istatusID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iStatusID))

            .Add(New SqlParameter("@sREFvrNo", SqlDbType.VarChar, 64, ParameterDirection.Input, _
         False, 0, 0, "", DataRowVersion.Proposed, REFvrNo))



            .Add(New SqlParameter("@sMasterNarration", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
            False, 0, 0, "", DataRowVersion.Proposed, sMasterNarration))

            .Add(New SqlParameter("@iDetailCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iDetailCodeID))

            .Add(New SqlParameter("@iSubCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
            False, 10, 0, "", DataRowVersion.Proposed, iSubCodeID))







            '--************************************************************************
            '-- AUX Adjustment

            .Add(New SqlParameter("@Aux_1", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
             False, 0, 0, "", DataRowVersion.Proposed, Aux_1))
            .Add(New SqlParameter("@Aux_2", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_2))
            .Add(New SqlParameter("@Aux_3", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_3))
            .Add(New SqlParameter("@Aux_4", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_4))
            .Add(New SqlParameter("@Aux_5", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_5))
            .Add(New SqlParameter("@Aux_6", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_6))
            .Add(New SqlParameter("@Aux_7", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_7))
            .Add(New SqlParameter("@Aux_8", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_8))
            .Add(New SqlParameter("@Aux_9", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_9))
            .Add(New SqlParameter("@Aux_10", SqlDbType.NVarChar, 50, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_10))


            .Add(New SqlParameter("@Aux_i_1", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_1))

            .Add(New SqlParameter("@Aux_i_2", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_2))

            .Add(New SqlParameter("@Aux_i_3", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_3))

            .Add(New SqlParameter("@Aux_i_4", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, Aux_i_4))



            .Add(New SqlParameter("@aux_f_1", SqlDbType.Float, 8, ParameterDirection.Input, _
                  False, 38, 0, "", DataRowVersion.Proposed, Aux_f_1))
            .Add(New SqlParameter("@aux_f_2", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_2))
            .Add(New SqlParameter("@aux_f_3", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_3))
            .Add(New SqlParameter("@aux_f_4", SqlDbType.Float, 8, ParameterDirection.Input, _
                             False, 38, 0, "", DataRowVersion.Proposed, Aux_f_4))


            .Add(New SqlParameter("@Aux_b_1", SqlDbType.Bit, 1, ParameterDirection.Input, _
                      False, 1, 0, "", DataRowVersion.Proposed, Aux_b_1))
            .Add(New SqlParameter("@Aux_b_2", SqlDbType.Bit, 1, ParameterDirection.Input, _
               False, 1, 0, "", DataRowVersion.Proposed, Aux_b_2))
            .Add(New SqlParameter("@Aux_b_3", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                 False, 1, 0, "", DataRowVersion.Proposed, Aux_b_3))
            .Add(New SqlParameter("@Aux_b_4", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                 False, 1, 0, "", DataRowVersion.Proposed, Aux_b_4))

            '--************************************************************************
            '--************************************************************************

            .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, _
                      False, 1, 0, "", DataRowVersion.Proposed, IsUpdate))


            .Add(New SqlParameter("@svrNoDisplay_s_2", SqlDbType.VarChar, 20, ParameterDirection.Output, _
            False, 0, 0, "", DataRowVersion.Proposed, svrNoDisplay_s_2))







            .Add(New SqlParameter("@Mark_To", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                       False, 0, 0, "", DataRowVersion.Proposed, Mark_To))
            .Add(New SqlParameter("@Payment_Terms", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
                       False, 0, 0, "", DataRowVersion.Proposed, Payment_Terms))
            .Add(New SqlParameter("@QUT_Note", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                       False, 0, 0, "", DataRowVersion.Proposed, QUT_Note))
            .Add(New SqlParameter("@DONOTE_ADD", SqlDbType.NVarChar, 400, ParameterDirection.Input, _
                                  False, 0, 0, "", DataRowVersion.Proposed, DONOTE_ADD))
            .Add(New SqlParameter("@Customer_ADD", SqlDbType.NVarChar, 400, ParameterDirection.Input, _
                                            False, 0, 0, "", DataRowVersion.Proposed, Customer_ADD))


            .Add(New SqlParameter("@AMC_YEAR_ID", SqlDbType.Int, 4, ParameterDirection.Input, _
               False, 10, 0, "", DataRowVersion.Proposed, AMC_YEAR_ID))

            .Add(New SqlParameter("@AMC_TYPE_ID", SqlDbType.Int, 4, ParameterDirection.Input, _
               False, 10, 0, "", DataRowVersion.Proposed, AMC_TYPE_ID))
            .Add(New SqlParameter("@AMC_START_DATE", SqlDbType.DateTime, 8, ParameterDirection.Input, _
           False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(AMC_START_DATE.Year, AMC_START_DATE.Month, AMC_START_DATE.Day)))
            .Add(New SqlParameter("@AMC_END_DATE", SqlDbType.DateTime, 8, ParameterDirection.Input, _
           False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(AMC_END_DATE.Year, AMC_END_DATE.Month, AMC_END_DATE.Day)))
            .Add(New SqlParameter("@ICCID", SqlDbType.Int, 4, ParameterDirection.Input, _
               False, 10, 0, "", DataRowVersion.Proposed, CCID))
            .Add(New SqlParameter("@ccid_GID", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
         False, 0, 0, "", DataRowVersion.Proposed, ccid_GID))
            .Add(New SqlParameter("@ccid_GID_Master", SqlDbType.NVarChar, 100, ParameterDirection.Input, _
         False, 0, 0, "", DataRowVersion.Proposed, ccid_GID_Master))

            '--------------------------------------------------------------------------

            .Add(New SqlParameter("@Aux_1_big", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
          False, 0, 0, "", DataRowVersion.Proposed, Aux_1_big))
            .Add(New SqlParameter("@Aux_2_big", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                 False, 0, 0, "", DataRowVersion.Proposed, Aux_2_big))
            .Add(New SqlParameter("@Aux_3_big", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                 False, 0, 0, "", DataRowVersion.Proposed, Aux_3_big))
            .Add(New SqlParameter("@Aux_4_big", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                 False, 0, 0, "", DataRowVersion.Proposed, Aux_4_big))
            .Add(New SqlParameter("@Aux_5_big", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                        False, 0, 0, "", DataRowVersion.Proposed, Aux_5_big))
            '--------------------------------------------------------------------------
            .Add(New SqlParameter("@Aux_b_5", SqlDbType.Bit, 1, ParameterDirection.Input, _
                   False, 1, 0, "", DataRowVersion.Proposed, Aux_b_5))
            .Add(New SqlParameter("@Aux_b_6", SqlDbType.Bit, 1, ParameterDirection.Input, _
                              False, 1, 0, "", DataRowVersion.Proposed, Aux_b_6))
            .Add(New SqlParameter("@Aux_b_7", SqlDbType.Bit, 1, ParameterDirection.Input, _
                       False, 1, 0, "", DataRowVersion.Proposed, Aux_b_7))
            .Add(New SqlParameter("@Aux_b_8", SqlDbType.Bit, 1, ParameterDirection.Input, _
                      False, 1, 0, "", DataRowVersion.Proposed, Aux_b_8))
            .Add(New SqlParameter("@Aux_b_9", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                False, 1, 0, "", DataRowVersion.Proposed, Aux_b_9))
            .Add(New SqlParameter("@Aux_b_10", SqlDbType.Bit, 1, ParameterDirection.Input, _
                                           False, 1, 0, "", DataRowVersion.Proposed, Aux_b_10))

            '--------------------------------------------------------------------------

            .Add(New SqlParameter("@Aux_dt_1", SqlDbType.DateTime, 8, ParameterDirection.Input, _
     False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(Aux_dt_1.Year, Aux_dt_1.Month, Aux_dt_1.Day)))
            .Add(New SqlParameter("@Aux_dt_2", SqlDbType.DateTime, 8, ParameterDirection.Input, _
                False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(Aux_dt_2.Year, Aux_dt_2.Month, Aux_dt_2.Day)))
            .Add(New SqlParameter("@Aux_dt_3", SqlDbType.DateTime, 8, ParameterDirection.Input, _
                False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(Aux_dt_3.Year, Aux_dt_3.Month, Aux_dt_3.Day)))
            .Add(New SqlParameter("@Aux_dt_4", SqlDbType.DateTime, 8, ParameterDirection.Input, _
                False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(Aux_dt_4.Year, Aux_dt_4.Month, Aux_dt_4.Day)))
            .Add(New SqlParameter("@Aux_dt_5", SqlDbType.DateTime, 8, ParameterDirection.Input, _
                False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(Aux_dt_5.Year, Aux_dt_5.Month, Aux_dt_5.Day)))
            .Add(New SqlParameter("@Aux_dt_6", SqlDbType.DateTime, 8, ParameterDirection.Input, _
                False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(Aux_dt_6.Year, Aux_dt_6.Month, Aux_dt_6.Day)))
            .Add(New SqlParameter("@Aux_dt_7", SqlDbType.DateTime, 8, ParameterDirection.Input, _
                False, 23, 3, "", DataRowVersion.Proposed, DateSerial_SFTX(Aux_dt_7.Year, Aux_dt_7.Month, Aux_dt_7.Day)))
            '--------------------------------------------------------------------------

            .Add(New SqlParameter("@Master_Ref_No", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
              False, 0, 0, "", DataRowVersion.Proposed, Aux_1))


            .Add(New SqlParameter("@vrTypeID_sys", SqlDbType.Int, 4, ParameterDirection.Input, _
           False, 0, 0, "", DataRowVersion.Proposed, vrTypeID_sys))



        End With


        Try
            loComm.ExecuteNonQuery()
            If Not IsUpdate Then
                If Not IsDBNull(objComm.Parameters.Item("@svrNoDisplay_s_2").Value) Then
                    svrNoDisplay_s_2 = objComm.Parameters.Item("@svrNoDisplay_s_2").Value
                End If

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message & " System is unable to save. please contact to system administrator...")

        End Try

    End Sub


    Public Sub saveDetail_Fun_loComm_BUDGET(ByRef loComm As SqlCommand, ByVal lsvrNo_INV As String, _
                         ByVal lisqNo As Integer, _
                         ByVal type_id As Integer, _
      ByVal SubCodeID As Integer, _
      ByVal Rate As Double, _
     ByVal Description As String, _
     ByVal IsRC As Boolean, _
     ByVal MnRC As Boolean, _
     ByVal RCID As Long, _
     ByVal RCID2 As Long, _
     ByVal ITEM_ID As Integer, _
     ByVal GROUP_ID As Integer, _
     ByVal QTY As Double, _
     ByVal Amount As Double, _
     ByVal NETAmount As Double, _
     ByVal Discount As Double, _
     ByVal aux_1 As Integer, _
     ByVal aux_2 As Integer, _
     ByVal aux_3 As Integer, _
     ByVal aux_f_1 As Double, _
     ByVal aux_f_2 As Double, _
     ByVal aux_f_3 As Double, _
     ByVal aux_b_1 As Boolean, _
     ByVal aux_b_2 As Boolean, _
     ByVal aux_b_3 As Boolean, ByVal aux_7 As String)




        '-------------------------------------------------
        Description = Trim(Replace(Description, "&nbsp;", ""))
        '-------------------------------------------













        Dim str_vrDetailID_INV_GUID As String = getGUID()
        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        loComm.CommandText = "stp_tblVchDetail_Insert_budget"

        With loComm.Parameters
            '--**********************************************
            '-- Case DB Entry = NoChange
            '--**********************************************

            .Add(New SqlParameter("@vrDetailID_INV", SqlDbType.Int, 4, ParameterDirection.Input, _
               False, 10, 0, "", DataRowVersion.Proposed, _
               0))

            .Add(New SqlParameter("@vrNo_INV", SqlDbType.VarChar, 64, ParameterDirection.Input, _
                False, 0, 0, "", DataRowVersion.Proposed, lsvrNo_INV))

            .Add(New SqlParameter("@vrDetailID_INV_GUID", SqlDbType.VarChar, 64, ParameterDirection.Input, _
                           False, 0, 0, "", DataRowVersion.Proposed, str_vrDetailID_INV_GUID))

            .Add(New SqlParameter("@sqNo", SqlDbType.SmallInt, 2, ParameterDirection.Input, _
                False, 5, 0, "", DataRowVersion.Proposed, lisqNo))

            .Add(New SqlParameter("@type_id", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
                type_id))


            .Add(New SqlParameter("@SubCodeID", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, _
               SubCodeID))



            .Add(New SqlParameter("@Rate", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, _
                 Math.Abs(Rate)))

            .Add(New SqlParameter("@Description", SqlDbType.VarChar, 400, ParameterDirection.Input, _
               False, 0, 0, "", DataRowVersion.Proposed, Description))


            .Add(New SqlParameter("@IsRC", SqlDbType.Bit, 1, ParameterDirection.Input, _
            False, 1, 0, "", DataRowVersion.Proposed, IsRC))
            .Add(New SqlParameter("@MnRC", SqlDbType.Bit, 1, ParameterDirection.Input, _
             False, 1, 0, "", DataRowVersion.Proposed, MnRC))

            .Add(New SqlParameter("@RCID", SqlDbType.BigInt, 8, ParameterDirection.Input, _
            False, 19, 0, "", DataRowVersion.Proposed, RCID))
            .Add(New SqlParameter("@RCID2", SqlDbType.BigInt, 8, ParameterDirection.Input, _
             False, 19, 0, "", DataRowVersion.Proposed, RCID2))


            .Add(New SqlParameter("@ITEM_ID", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, ITEM_ID))
            .Add(New SqlParameter("@GROUP_ID", SqlDbType.Int, 4, ParameterDirection.Input, _
                False, 10, 0, "", DataRowVersion.Proposed, GROUP_ID))


            .Add(New SqlParameter("@QTY", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(QTY)))
            .Add(New SqlParameter("@Amount", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(Amount)))
            .Add(New SqlParameter("@NETAmount", SqlDbType.Float, 8, ParameterDirection.Input, _
                False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(NETAmount)))
            .Add(New SqlParameter("@Discount", SqlDbType.Float, 8, ParameterDirection.Input, _
                           False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(Discount)))


            .Add(New SqlParameter("@aux_1", SqlDbType.Int, 4, ParameterDirection.Input, _
               False, 10, 0, "", DataRowVersion.Proposed, aux_1))
            .Add(New SqlParameter("@aux_2", SqlDbType.Int, 4, ParameterDirection.Input, _
                           False, 10, 0, "", DataRowVersion.Proposed, aux_2))
            .Add(New SqlParameter("@aux_3", SqlDbType.Int, 4, ParameterDirection.Input, _
                                       False, 10, 0, "", DataRowVersion.Proposed, aux_3))


            .Add(New SqlParameter("@aux_f_1", SqlDbType.Float, 8, ParameterDirection.Input, _
               False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(aux_f_1)))
            .Add(New SqlParameter("@aux_f_2", SqlDbType.Float, 8, ParameterDirection.Input, _
                          False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(aux_f_2)))
            .Add(New SqlParameter("@aux_f_3", SqlDbType.Float, 8, ParameterDirection.Input, _
                          False, 38, 0, "", DataRowVersion.Proposed, Math.Abs(aux_f_3)))


            .Add(New SqlParameter("@aux_b_1", SqlDbType.Bit, 1, ParameterDirection.Input, _
           False, 1, 0, "", DataRowVersion.Proposed, aux_b_1))
            .Add(New SqlParameter("@aux_b_2", SqlDbType.Bit, 1, ParameterDirection.Input, _
           False, 1, 0, "", DataRowVersion.Proposed, aux_b_2))
            .Add(New SqlParameter("@aux_b_3", SqlDbType.Bit, 1, ParameterDirection.Input, _
           False, 1, 0, "", DataRowVersion.Proposed, aux_b_3))
            .Add(New SqlParameter("@aux_7", SqlDbType.NVarChar, 200, ParameterDirection.Input, _
                       False, 38, 0, "", DataRowVersion.Proposed, aux_7))

            '.Add(New SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, False, 10, 0, "", DataRowVersion.Proposed, nERRCODE))



            '--**********************************************
            '-- Case CR Entry = NoChange
            '--**********************************************
        End With
        'Dim sID_ERR_OUT As String
        Try
            loComm.ExecuteNonQuery()
            'sID_ERR_OUT = loComm.Parameters.Item("@iErrorCode").Value

        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator..." & ex.Message)
        End Try



    End Sub
    Public Sub set_CMB_TITLE_BY_VALUEID_GEN(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
                               ByRef nID As String)
        Dim str_ACC As String = ""
        Dim n_CMB_ID_VALUE As String = ""
        Dim i As Integer = 0
        Try
            For i = 0 To cmbBox.Items.Count - 1
                str_ACC = cmbBox.Items(i).Text
                n_CMB_ID_VALUE = cmbBox.Items(i).Value
                If Not str_ACC = gStr_Empty Then
                    If nID = n_CMB_ID_VALUE Then
                        cmbBox.SelectedIndex = i
                        Exit For
                    End If
                End If
            Next

        Catch ex As Exception
            Throw New Exception("Please contact to system administrator ......")
        End Try
    End Sub

    Public Sub set_CMB_TITLE_BY_VALUEID(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
                               ByRef nID As Integer)
        Dim str_ACC As String = ""
        Dim n_CMB_ID_VALUE As Integer = 0
        Dim i As Integer = 0
        Try
            For i = 0 To cmbBox.Items.Count - 1
                str_ACC = cmbBox.Items(i).Text
                n_CMB_ID_VALUE = cmbBox.Items(i).Value
                If Not str_ACC = gStr_Empty Then
                    If nID = n_CMB_ID_VALUE Then
                        cmbBox.SelectedIndex = i
                        Exit For
                    End If
                End If
            Next

        Catch ex As Exception
            Throw New Exception("Please contact to system administrator ......")
        End Try
    End Sub


    Public Sub getMM_MONTH(ByVal strDate As String, ByRef MM As Integer)
        Try


            Dim str As String = strDate

            str = Replace(str, "-", "|")
            str = Replace(str, "/", "|")

            Dim DD As String = Mid(str, 1, (InStr(str, "|") - 1))
            str = Mid(str, (InStr(str, "|") + 1))
            Dim MM_STR As String = Mid(str, 1, (InStr(str, "|") - 1))
            str = Mid(str, (InStr(str, "|") + 1))
            MM = CInt(MM_STR)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Sub

    Public Sub initCmb_Text_Code(ByRef cmbBox As System.Web.UI.WebControls.DropDownList, _
    ByVal strTable As String, _
    ByVal strDisplayField As String, _
    ByVal strDataField As String, _
    ByVal strWhereCriteria As String)


        Dim moComm As New SqlClient.SqlCommand
        setCon(True)
        moComm.Connection = gCon
        strSql = "SELECT " & strDisplayField & " , " & strDataField & "  FROM " & strTable & " " & strWhereCriteria
        '  strSql = strSql + "  order by 1 "

        cmbBox.Items.Clear()
        cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, "9999"))
        moComm.CommandText = strSql
        Try
            Dim dt As DataTable = New DataTable("dt")
            Dim sdaAdapter As SqlDataAdapter = New SqlDataAdapter(moComm)
            sdaAdapter.Fill(dt)
            Dim recCount As Int16
            'cmbBox.Items.Clear()
            'cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(gStr_Empty, -1))
            For recCount = 0 To dt.Rows.Count - 1
                cmbBox.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(recCount)(0) + " - " + dt.Rows(recCount)(1), dt.Rows(recCount)(1)))
            Next
            cmbBox.SelectedIndex = 0
            moComm.Dispose()
            sdaAdapter.Dispose()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            setCon(False)
        End Try

    End Sub
    Sub saveCity(ByRef loComm As SqlCommand, iCityid As Integer, strCityname As String, iStateid As Integer, strCountrid As String, dlat As Double, dlong As Double, bIsUpdate As Boolean)
        Dim iErrorcode As Integer
        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        loComm.CommandText = "stp_tblCities_InsertUpdate"

        With loComm.Parameters

            .Add(New SqlParameter("@cityID", SqlDbType.Int, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iCityid))
            .Add(New SqlParameter("@CityName", SqlDbType.VarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, strCityname))
            .Add(New SqlParameter("@stateID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, iStateid))
            .Add(New SqlParameter("@CountryID", SqlDbType.VarChar, 3, ParameterDirection.Input, False, 5, 0, "", DataRowVersion.Proposed, strCountrid))
            .Add(New SqlParameter("@latitude", SqlDbType.Float, 8, ParameterDirection.Input, False, 38, 0, "", DataRowVersion.Proposed, dlat))
            .Add(New SqlParameter("@longitude", SqlDbType.Float, 8, ParameterDirection.Input, False, 38, 0, "", DataRowVersion.Proposed, dlong))
            If bIsUpdate Then
                .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            Else
                .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            End If
            .Add(New SqlParameter("@iErrorcode", SqlDbType.Float, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iErrorcode))

        End With

        Try
            loComm.ExecuteNonQuery()


        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator..." & ex.Message)
        End Try
    End Sub

    Sub saveAirport(ByRef loComm As SqlCommand, strcityname As String, strAirportCode As String, strAirportName As String, strCountryName As String, strCountryCode As String, iCountryId As Int16, bisUpdate As Boolean)
        Dim iErrorcode As Integer
        loComm.Parameters.Clear()
        loComm.CommandType = CommandType.StoredProcedure
        loComm.CommandText = "stp_tblAirports_InsertUpdate"

        With loComm.Parameters

            .Add(New SqlParameter("@CityName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, strcityname))
            .Add(New SqlParameter("@airportcode", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, strAirportCode))
            .Add(New SqlParameter("@AirportName", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, strAirportName))
            .Add(New SqlParameter("@CountryName", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, strCountryName))
            .Add(New SqlParameter("@CountryCode", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Proposed, strCountryCode))
            .Add(New SqlParameter("@WorldAreaCode", SqlDbType.Float, 8, ParameterDirection.Input, False, 38, 0, "", DataRowVersion.Proposed, CType(iCountryId, Single)))

            If bIsUpdate Then
                .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 1))
            Else
                .Add(New SqlParameter("@IsUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, False, 1, 0, "", DataRowVersion.Proposed, 0))
            End If
            .Add(New SqlParameter("@iErrorcode", SqlDbType.Float, 4, ParameterDirection.Input, False, 10, 0, "", DataRowVersion.Proposed, iErrorcode))

        End With

        Try
            loComm.ExecuteNonQuery()


        Catch ex As Exception
            Throw New Exception("System is unable to save. please contact to system administrator..." & ex.Message)
        End Try
    End Sub

















End Class
