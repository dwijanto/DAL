<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.vendorcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vendorname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.shortname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(103, 40)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Get List of Vendor"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.ColumnHeadersHeight = 35
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.vendorcode, Me.vendorname, Me.shortname})
        Me.DataGridView1.Location = New System.Drawing.Point(127, 12)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(609, 214)
        Me.DataGridView1.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 58)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(103, 40)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Get Dataset"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'vendorcode
        '
        Me.vendorcode.DataPropertyName = "vendorcode"
        Me.vendorcode.HeaderText = "Vendor Code"
        Me.vendorcode.Name = "vendorcode"
        Me.vendorcode.ReadOnly = True
        Me.vendorcode.Width = 120
        '
        'vendorname
        '
        Me.vendorname.DataPropertyName = "vendorname"
        Me.vendorname.HeaderText = "Vendor Name"
        Me.vendorname.Name = "vendorname"
        Me.vendorname.ReadOnly = True
        Me.vendorname.Width = 300
        '
        'shortname
        '
        Me.shortname.DataPropertyName = "shortname"
        Me.shortname.HeaderText = "Short Name"
        Me.shortname.Name = "shortname"
        Me.shortname.ReadOnly = True
        Me.shortname.Width = 120
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 262)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents vendorcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vendorname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents shortname As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
