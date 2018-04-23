Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Design
Imports DevExpress.XtraEditors

Namespace WindowsApplication51
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			gridPanel.StartEdit(gridView1.Columns("ExpressionA"))
		End Sub

		Private Sub simpleButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton2.Click
			gridPanel.StartEdit(gridView1.Columns("ExpressionB"))

		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' TODO: This line of code loads data into the 'nwindDataSet.Products' table. You can move, or remove it, as needed.
			Me.productsTableAdapter.Fill(Me.nwindDataSet.Products)

		End Sub
	End Class
End Namespace