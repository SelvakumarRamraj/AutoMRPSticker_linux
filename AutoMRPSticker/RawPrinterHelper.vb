Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing

Public Class RawPrinterHelper
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
    Public Structure DOCINFOA
        <MarshalAs(UnmanagedType.LPStr)> Public pDocName As String
        <MarshalAs(UnmanagedType.LPStr)> Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPStr)> Public pDataType As String
    End Structure

    <DllImport("winspool.Drv", EntryPoint:="OpenPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True)>
    Public Shared Function OpenPrinter(szPrinter As String, ByRef hPrinter As IntPtr, pd As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", SetLastError:=True)>
    Public Shared Function ClosePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True)>
    Public Shared Function StartDocPrinter(hPrinter As IntPtr, level As Integer, ByRef di As DOCINFOA) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", SetLastError:=True)>
    Public Shared Function EndDocPrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", SetLastError:=True)>
    Public Shared Function StartPagePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", SetLastError:=True)>
    Public Shared Function EndPagePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="WritePrinter", SetLastError:=True)>
    Public Shared Function WritePrinter(hPrinter As IntPtr, pBytes As IntPtr, dwCount As Integer, ByRef dwWritten As Integer) As Boolean
    End Function

    Public Shared Function SendStringToPrinter(printerName As String, zplCommand As String) As Boolean
        Dim hPrinter As IntPtr = IntPtr.Zero
        Dim di As New DOCINFOA()
        Dim pBytes As IntPtr
        Dim dwCount As Integer = zplCommand.Length
        Dim dwWritten As Integer = 0
        di.pDocName = "Raw ZPL Command"
        di.pDataType = "RAW"

        Dim success As Boolean = False
        If OpenPrinter(printerName, hPrinter, IntPtr.Zero) Then
            If StartDocPrinter(hPrinter, 1, di) Then
                If StartPagePrinter(hPrinter) Then
                    pBytes = Marshal.StringToCoTaskMemAnsi(zplCommand)
                    success = WritePrinter(hPrinter, pBytes, dwCount, dwWritten)
                    Marshal.FreeCoTaskMem(pBytes)
                    EndPagePrinter(hPrinter)
                End If
                EndDocPrinter(hPrinter)
            End If
            ClosePrinter(hPrinter)
        End If
        Return success
    End Function

    Public Shared Function SendStringToPrinter2(printerName As String, data As String) As Boolean
        Dim hPrinter As IntPtr
        Dim di As New DOCINFOA With {.pDocName = "Raw Print", .pDataType = "RAW"}

        If Not OpenPrinter(printerName, hPrinter, IntPtr.Zero) Then Return False
        If Not StartDocPrinter(hPrinter, 1, di) Then Return False
        StartPagePrinter(hPrinter)

        Dim bytes() As Byte = System.Text.Encoding.ASCII.GetBytes(data)
        Dim ptr As IntPtr = Marshal.AllocHGlobal(bytes.Length)
        Marshal.Copy(bytes, 0, ptr, bytes.Length)

        Dim written As Integer
        WritePrinter(hPrinter, ptr, bytes.Length, written)

        Marshal.FreeHGlobal(ptr)
        EndPagePrinter(hPrinter)
        EndDocPrinter(hPrinter)
        ClosePrinter(hPrinter)

        Return True
    End Function



    Public Shared Function SendStringToPrinter3(printerName As String, data As String) As Boolean
        Dim rawData = System.Text.Encoding.ASCII.GetBytes(data)
        Dim ptr As IntPtr = Marshal.AllocHGlobal(rawData.Length)
        Marshal.Copy(rawData, 0, ptr, rawData.Length)

        Dim success = SendBytesToPrinter(printerName, ptr, rawData.Length)
        Marshal.FreeHGlobal(ptr)
        Return success
    End Function

    Public Shared Function SendBytesToPrinter(printerName As String, pBytes As IntPtr, count As Integer) As Boolean
        Dim hPrinter As IntPtr = IntPtr.Zero
        Dim di As New DOCINFOA
        Dim dwWritten As Integer = 0
        Dim success As Boolean = False

        di.pDocName = "Raw TSPL Label"
        di.pDatatype = "RAW"

        If OpenPrinter(printerName, hPrinter, IntPtr.Zero) Then
            If StartDocPrinter(hPrinter, 1, di) Then
                If StartPagePrinter(hPrinter) Then
                    success = WritePrinter(hPrinter, pBytes, count, dwWritten)
                    EndPagePrinter(hPrinter)
                End If
                EndDocPrinter(hPrinter)
            End If
            ClosePrinter(hPrinter)
        End If
        Return success
    End Function


    '    Dim tspl As String =
    '"SIZE 60 mm,40 mm" & vbCrLf &
    '"GAP 3 mm,0" & vbCrLf &
    '"CLS" & vbCrLf &
    '"TEXT 20,20,""0"",0,12,12,""USB Test""" & vbCrLf &
    '"BARCODE 20,70,""128"",70,1,0,2,2,""123456789012""" & vbCrLf &
    '"PRINT 1" & vbCrLf

    '    Dim ok As Boolean = RawPrinterHelper.SendStringToPrinter("Citizen CL-E321", tspl)

    '    If ok Then
    '    MessageBox.Show("Printed successfully!")
    'Else
    '    MessageBox.Show("Print failed.")
    'End If
End Class
