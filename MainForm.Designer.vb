﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnViewSubmissions = New System.Windows.Forms.Button()
        Me.btnCreateSubmission = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnViewSubmissions
        '
        Me.btnViewSubmissions.Location = New System.Drawing.Point(132, 176)
        Me.btnViewSubmissions.Name = "btnViewSubmissions"
        Me.btnViewSubmissions.Size = New System.Drawing.Size(259, 67)
        Me.btnViewSubmissions.TabIndex = 0
        Me.btnViewSubmissions.Text = "View Submissions (CTRL + V)"
        Me.btnViewSubmissions.UseVisualStyleBackColor = True
        '
        'btnCreateSubmission
        '
        Me.btnCreateSubmission.Location = New System.Drawing.Point(413, 174)
        Me.btnCreateSubmission.Name = "btnCreateSubmission"
        Me.btnCreateSubmission.Size = New System.Drawing.Size(272, 69)
        Me.btnCreateSubmission.TabIndex = 1
        Me.btnCreateSubmission.Text = "Create New Submission (CTRL + C)"
        Me.btnCreateSubmission.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(226, 110)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(336, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Satyendu Mohapatra, Slidely Task 2 - Slidely Form App"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCreateSubmission)
        Me.Controls.Add(Me.btnViewSubmissions)
        Me.Name = "MainForm"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnViewSubmissions As Button
    Friend WithEvents btnCreateSubmission As Button
    Friend WithEvents Label1 As Label
End Class
