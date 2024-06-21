Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text
Imports System.Windows.Forms

Public Class ViewSubmissionsForm
    Private currentIndex As Integer = 0
    Private submissions As List(Of SubmissionDetails)

    Private Async Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
        Await LoadSubmissions()
        DisplaySubmission(currentIndex)
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Async Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim editForm As New CreateSubmissionForm(submissions(currentIndex))
        If editForm.ShowDialog() = DialogResult.OK Then
            Await LoadSubmissions()
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim submission = submissions(currentIndex)
        Dim result = MessageBox.Show($"Are you sure you want to delete the submission from {submission.Name}?", "Confirm Delete", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Await DeleteSubmission(submission)
            Await LoadSubmissions()
            If currentIndex >= submissions.Count Then
                currentIndex = Math.Max(submissions.Count - 1, 0)
            End If
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub ViewSubmissionsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            btnEdit.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            btnDelete.PerformClick()
        End If
    End Sub

    Private Async Function LoadSubmissions() As Task
        Try
            Dim client As New HttpClient()
            Dim response = Await client.GetStringAsync("http://localhost:3000/submissions")
            submissions = JsonConvert.DeserializeObject(Of List(Of SubmissionDetails))(response)
            ' Ensure UI updates are performed on the main thread
            If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
                Me.Invoke(Sub() DisplaySubmission(currentIndex))
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading submissions: " & ex.Message)
        End Try
    End Function

    Private Sub DisplaySubmission(index As Integer)
        If submissions IsNot Nothing AndAlso submissions.Count > 0 AndAlso index >= 0 AndAlso index < submissions.Count Then
            Dim submission = submissions(index)
            txtName.Text = submission.Name
            txtEmail.Text = submission.Email
            txtPhone.Text = submission.Phone
            txtGithubLink.Text = submission.GitHubLink
            txtStopwatchTime.Text = submission.StopwatchTime
        Else
            ' Clear the fields if no submissions are available
            txtName.Clear()
            txtEmail.Clear()
            txtPhone.Clear()
            txtGithubLink.Clear()
            txtStopwatchTime.Clear()
        End If
    End Sub

    Private Async Function DeleteSubmission(submission As SubmissionDetails) As Task
        Try
            Debug.WriteLine($"Attempting to delete submission: {submission.Name}")
            Dim client As New HttpClient()
            Dim content = New StringContent(JsonConvert.SerializeObject(New With {Key .Name = submission.Name}), Encoding.UTF8, "application/json")
            Dim response = Await client.PostAsync("http://localhost:3000/delete", content)
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission deleted successfully.")
            Else
                MessageBox.Show("Error deleting submission: " & Await response.Content.ReadAsStringAsync())
            End If
        Catch ex As Exception
            MessageBox.Show("Error deleting submission: " & ex.Message)
        End Try
    End Function

End Class
