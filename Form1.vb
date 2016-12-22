Imports System.IO

Public Class Form1
    Dim Employees As New Dictionary(Of Integer, Person)
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Dictionary

        Employees.Add(1, New Person)
        With Employees(1)
            .FirstName = "Eater"
            .LastName = "Bulaga"
            MessageBox.Show("Hello " & .FirstName & " " & .LastName & ".")
        End With

        If Employees.ContainsKey(1) Then
            MessageBox.Show("Already contains ID 1. ")
        End If

        For Each k In Employees.Keys
            Debug.Print(k.ToString)
        Next
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'Linq To Object

        Debug.Print("This will print all Employees with the LastName of Bulaga")

        Dim search_result = From p As Person In Employees.Values
                            Where p.LastName = "Bulaga"
                            Select p.FirstName

        For Each p In search_result
            Debug.Print(p)
        Next
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'File Read/Write

        Dim sample_file As String = "sample.txt"

        'Write to file
        Using sw As New StreamWriter(sample_file)
            With sw
                .WriteLine("This is Eater's note.")
                .WriteLine("Please take care of it.")
            End With
        End Using

        'Read from file
        Using sr As New StreamReader(sample_file)
            With sr
                Debug.Print(.ReadToEnd)
            End With
        End Using

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        'Host Form in a Container

        If Panel1.Controls.Count <= 0 Then
            Dim frm As New Dialog1
            With frm
                .TopLevel = False
                .Visible = True
                .Dock = DockStyle.Fill
                .FormBorderStyle = Windows.Forms.FormBorderStyle.None
            End With
            Panel1.Controls.Add(frm)
        End If
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        'Create a control dynamically

        Dim btn1 As New Button
        btn1.Text = "New Button 1"
        btn1.Location = New Point(100, 100)
        btn1.Size = New Point(100, 20)
        Me.Controls.Add(btn1)
        AddHandler btn1.Click, AddressOf btn_click

        'Create a control dynamically
        Dim btn2 As New Button
        btn2.Text = "New Button 2"
        btn2.Location = New Point(100, 140)
        btn2.Size = New Point(100, 20)
        Me.Controls.Add(btn2)
        AddHandler btn2.Click, AddressOf btn_click
    End Sub

    Private Sub btn_click(sender As Object, e As EventArgs)
        MessageBox.Show("You Pressed Me, I'm " & sender.text & ".")
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        'PictureBox to MemoryStream to Database
        Dim cn As New SqlClient.SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=Database1;Integrated Security=True;")
        Dim cmd As New SqlClient.SqlCommand("UPDATE [dbo].[Table1] SET [Field5] = @Field5 WHERE [Field1] = 1", cn)
        Dim ms As New MemoryStream
        PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        cmd.Parameters.AddWithValue("@Field5", ms.GetBuffer)
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cn.Close()
        End Try
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        'Database to MemoryStream to PictureBox
        Dim cn As New SqlClient.SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=Database1;Integrated Security=True;")
        Dim cmd As New SqlClient.SqlCommand("SELECT [Field5] FROM [dbo].[Table1] WHERE [Field1] = 1", cn)
        Try
            cn.Open()
            Dim dr = cmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Dim img As Byte() = DirectCast(dr("Field5"), Byte())
                Dim ms As New MemoryStream(img, 0, img.Length)
                With PictureBox1
                    .Image = Image.FromStream(ms)
                    .SizeMode = PictureBoxSizeMode.StretchImage
                End With
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cn.Close()
        End Try
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        'PictureBox to File
        Dim sample_file As String = "sample.jpg"
        PictureBox1.Image.Save(sample_file, System.Drawing.Imaging.ImageFormat.Jpeg)
        MessageBox.Show("Image File Saved")
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        'Image From File to PictureBox
        With PictureBox1
            .Image = Image.FromFile("koala.jpg")
            .SizeMode = PictureBoxSizeMode.StretchImage
        End With
    End Sub
End Class

Public Class Person
    Public FirstName As String
    Public LastName As String
    Public BirthDate As Date
    Public Age As Integer
    Public Address As String
End Class