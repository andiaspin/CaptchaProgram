Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

Public Class Form1
    Dim DrawingFont As New Font("Arial", 25)
    Dim CaptchaImage As New Bitmap(140, 40)
    Dim CaptchaGraf As Graphics = Graphics.FromImage(CaptchaImage)
    Dim Alphabet As String = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz"
    Dim CaptchaString, TickRandom As String
    Dim ProcessNumber As Integer

    Private Sub GenerateCaptcha()
        ProcessNumber = My.Computer.Clock.LocalTime.Millisecond
        If ProcessNumber < 521 Then
            ProcessNumber = ProcessNumber \ 10
            CaptchaString = Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString = CStr(My.Computer.Clock.LocalTime.Second \ 6)
        End If
        ProcessNumber = My.Computer.Clock.LocalTime.Second
        If ProcessNumber < 30 Then
            ProcessNumber = Math.Abs(ProcessNumber - 8)
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(My.Computer.Clock.LocalTime.Minute \ 6)
        End If
        ProcessNumber = My.Computer.Clock.LocalTime.DayOfYear
        If ProcessNumber Mod 2 = 0 Then
            ProcessNumber = ProcessNumber \ 8
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(ProcessNumber \ 37)
        End If
        TickRandom = My.Computer.Clock.TickCount.ToString
        ProcessNumber = Val(TickRandom.Substring(TickRandom.Length - 1, 1))
        If ProcessNumber Mod 2 = 0 Then
            CaptchaString += CStr(ProcessNumber)
        Else
            ProcessNumber = Math.Abs(Int(Math.Cos(Val(TickRandom)) * 51))
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        End If
        ProcessNumber = My.Computer.Clock.LocalTime.Hour
        If ProcessNumber Mod 2 = 0 Then
            ProcessNumber = Math.Abs(Int(Math.Sin(Val(My.Computer.Clock.LocalTime.Year)) * 51))
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(ProcessNumber \ 3)
        End If
        ProcessNumber = My.Computer.Clock.LocalTime.Millisecond
        If ProcessNumber > 521 Then
            ProcessNumber = Math.Abs((ProcessNumber \ 10) - 52)
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(My.Computer.Clock.LocalTime.Second \ 6)
        End If
        CaptchaGraf.Clear(Color.White)

        For hasher As Integer = 0 To 5
            CaptchaGraf.DrawString(CaptchaString.Substring(hasher, 1), DrawingFont, Brushes.Black, hasher * 20 + hasher + ProcessNumber \ 200, (hasher Mod 3) * (ProcessNumber \ 200))

        Next
        PictureBox1.Image = CaptchaImage
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = CaptchaString Then
            MsgBox("Captcha Correct", MsgBoxStyle.Information)
            TextBox1.Clear()
            GenerateCaptcha()
        Else
            MsgBox("Captcha Incorrect", MsgBoxStyle.Exclamation)
            TextBox1.Clear()

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        GenerateCaptcha()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GenerateCaptcha()
    End Sub
End Class
