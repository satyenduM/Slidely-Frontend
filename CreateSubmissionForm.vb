Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Windows.Forms
Imports System.Diagnostics

Public Class CreateSubmissionForm
    Public Submission As SubmissionDetails
    Private stopwatch As Stopwatch
    Private stopwatchRunning As Boolean
    Private updateTimer As Timer

    Public Sub New(Optional submissionToEdit As SubmissionDetails = Nothing)
        InitializeComponent()
        stopwatch = New Stopwatch()
        updateTimer = New Timer()
        AddHandler updateTimer.Tick, AddressOf UpdateStopwatchTime
        updateTimer.Interval = 1000 ' Update every second

        Me.Submission = submissionToEdit
        If submissionToEdit IsNot Nothing Then
            txtName.Text = submissionToEdit.Name
            txtEmail.Text = submissionToEdit.Email
            txtPhone.Text = submissionToEdit.Phone
            txtGithubLink.Text = submissionToEdit.GitHubLink
            txtStopwatchTime.Text = submissionToEdit.StopwatchTime
        End If
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim newSubmission As New SubmissionDetails With {
            .Name = txtName.Text,
            .Email = txtEmail.Text,
            .Phone = txtPhone.Text,
            .GitHubLink = txtGithubLink.Text,
            .StopwatchTime = txtStopwatchTime.Text
        }

        If Submission IsNot Nothing Then
            ' Edit submission
            Await UpdateSubmission(newSubmission)
        Else
            ' Create new submission
            Await SubmitNewSubmission(newSubmission)
        End If

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Async Function SubmitNewSubmission(submission As SubmissionDetails) As Task
        Using client As New HttpClient()
            Dim json As String = JsonSerializer.Serialize(submission)
            Dim content As New StringContent(json, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:3000/submit", content)
            response.EnsureSuccessStatusCode()
        End Using
    End Function

    Private Async Function UpdateSubmission(submission As SubmissionDetails) As Task
        Using client As New HttpClient()
            Dim json As String = JsonSerializer.Serialize(submission)
            Dim content As New StringContent(json, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:3000/update", content)
            response.EnsureSuccessStatusCode()
        End Using
    End Function

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If stopwatchRunning Then
            stopwatch.Stop()
            updateTimer.Stop()
            stopwatchRunning = False
        Else
            stopwatch.Start()
            updateTimer.Start()
            stopwatchRunning = True
        End If
        txtStopwatchTime.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub UpdateStopwatchTime(sender As Object, e As EventArgs)
        If stopwatchRunning Then
            txtStopwatchTime.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
        End If
    End Sub
End Class
