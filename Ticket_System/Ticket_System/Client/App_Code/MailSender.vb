Imports System.Web.Mail
Public Class MailSender
    Public Shared Function SendEmail(ByVal pGmailEmail As String, ByVal pGmailPassword As String, ByVal pTo As String, ByVal pSubject As String, ByVal pBody As String, ByVal pFormat As System.Web.Mail.MailFormat, _
     ByVal pAttachmentPath As String) As Boolean
        Try
            Dim myMail As New System.Web.Mail.MailMessage()
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
            'sendusing: cdoSendUsingPort, value 2, for sending the message using 
            'the network.

            'smtpauthenticate: Specifies the mechanism used when authenticating 
            'to an SMTP 
            'service over the network. Possible values are:
            '- cdoAnonymous, value 0. Do not authenticate.
            '- cdoBasic, value 1. Use basic clear-text authentication. 
            'When using this option you have to provide the user name and password 
            'through the sendusername and sendpassword fields.
            '- cdoNTLM, value 2. The current process security context is used to 
            ' authenticate with the service.
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
            'Use 0 for anonymous
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", pGmailEmail)
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", pGmailPassword)
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true")
            myMail.From = pGmailEmail
            myMail.[To] = pTo
            myMail.Subject = pSubject
            myMail.BodyFormat = pFormat
            myMail.Body = pBody
            If pAttachmentPath.Trim() <> "" Then
                Dim MyAttachment As New MailAttachment(pAttachmentPath)
                myMail.Attachments.Add(MyAttachment)
                myMail.Priority = System.Web.Mail.MailPriority.High
            End If

            System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com:465"
            System.Web.Mail.SmtpMail.Send(myMail)
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class