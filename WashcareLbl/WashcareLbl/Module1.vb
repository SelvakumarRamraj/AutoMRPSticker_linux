Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
'Imports CarlosAg.ExcelXmlWriter
Imports System.IO.MemoryStream
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.ReportSource
'Imports CrystalDecisions.CrystalReports.Engine.Section
'Imports CrystalDecisions.CrystalReports.Engine.Sections

Imports System.Drawing.Drawing2D
Imports System.Collections.Specialized
Imports System.Security
Imports System.Text
Imports System.Net.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail.MailMessage
Imports System.Net.Mail.Attachment
Imports System.Net
Imports Microsoft.VisualBasic

Module Module1
    Public mdbserver As String
    Public mdbname As String
    Public mdbuserid As String
    Public mdbpwd As String
    Dim tmpp As String



    Public Function decodefile(ByVal srcfile As String) As String

        Dim decodedBytes As Byte()
        decodedBytes = Convert.FromBase64String(Decode(srcfile))

        Dim decodedText As String
        decodedText = Encoding.UTF8.GetString(decodedBytes)
        decodefile = decodedText
    End Function
    Public Function decodefilesql(ByVal srcfile As String) As String

        Dim decodedBytes As Byte()
        decodedBytes = Convert.FromBase64String(Decode(srcfile))

        Dim decodedText As String
        decodedText = Encoding.UTF8.GetString(decodedBytes)
        decodefilesql = decodedText
    End Function

    'Sub EncodeFile(ByVal srcFile As String, ByVal destfile As String)
    Public Function encodefile(ByVal srcfile As String) As String

        Dim bytesToEncode As Byte()
        bytesToEncode = Encoding.UTF8.GetBytes(srcfile)

        Dim encodedText As String
        encodedText = Convert.ToBase64String(bytesToEncode)
        encodefile = Encript(encodedText)
    End Function
    Public Function encodefilesql(ByVal srcfile As String) As String

        Dim bytesToEncode As Byte()
        bytesToEncode = Encoding.UTF8.GetBytes(srcfile)

        Dim encodedText As String
        encodedText = Convert.ToBase64String(bytesToEncode)
        encodefilesql = encodedText
    End Function
    Public Function Decode(ByVal Password As String) As String
        'Dim I As Integer
        Dim TMP As Long
        tmpp = ""
        For i = 1 To Len(Password)
            TMP = Asc(Mid(Password, i, 1))
            TMP = TMP - i
            tmpp = Trim(tmpp) & Chr(TMP)
            'Decode = Decode & Chr(TMP)
        Next i
        Decode = tmpp
        Return Decode
    End Function
    Public Function Encript(ByVal Password As String) As String
        ' Dim I As Integer
        'Dim tmpp As String
        Dim TMP As Long
        tmpp = ""
        For i = 1 To Len(Password)
            TMP = Asc(Mid(Password, i, 1))
            TMP = TMP + i
            tmpp = Trim(tmpp) + Chr(TMP)
            'Encript = Encript & Chr(TMP)
        Next i
        Encript = tmpp
        Return Encript
    End Function
End Module
