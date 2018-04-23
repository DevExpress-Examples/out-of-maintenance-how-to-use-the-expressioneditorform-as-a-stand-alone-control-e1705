Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Design
Imports System
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms

Namespace DXSample
	Public MustInherit Class ExpressionPanel
		Inherits PanelControl
		Public Sub New()
			BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		End Sub

		Private arguments_Renamed() As Object
		Protected ReadOnly Property Arguments() As Object()
			Get
				Return arguments_Renamed
			End Get
		End Property

		Private form_Renamed As ExpressionEditorForm = Nothing
		Protected ReadOnly Property Form() As ExpressionEditorForm
			Get
				Return form_Renamed
			End Get
		End Property

		Public Overridable Sub StartEdit(ParamArray ByVal arguments() As Object)
			DestroyForm()
			Me.arguments_Renamed = arguments
			Me.form_Renamed = CreateForm(arguments)
			If form_Renamed Is Nothing Then
				Return
			End If
			form_Renamed.Dock = DockStyle.Fill
			form_Renamed.TopLevel = False
			form_Renamed.FormBorderStyle = FormBorderStyle.None
			AddHandler form_Renamed.Closing, AddressOf form_Closing
			AddHandler form_Renamed.buttonCancel.Click, AddressOf buttonCancel_Click
			form_Renamed.buttonOK.Text = "Apply"
			Controls.Add(form_Renamed)
			form_Renamed.Visible = True
		End Sub

		Private Sub buttonCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
			If form_Renamed IsNot Nothing Then
				form_Renamed.Close()
			End If
		End Sub

		Private Sub form_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
			e.Cancel = True
			If Me.arguments_Renamed Is Nothing OrElse Me.form_Renamed Is Nothing Then
				Return
			End If
			If form_Renamed.DialogResult = DialogResult.OK Then
				ApplyExpression(form_Renamed.Expression)
			Else
				form_Renamed.ResetMemoText()
			End If
		End Sub

		Public Sub DestroyForm()
			If form_Renamed IsNot Nothing Then
				form_Renamed.Dispose()
			End If
			form_Renamed = Nothing
		End Sub

		Protected MustOverride Sub ApplyExpression(ByVal expression As String)

		Protected MustOverride Function CreateForm(ParamArray ByVal arguments_Renamed() As Object) As ExpressionEditorForm
	End Class
	Public Class UnboundExpressionPanel
		Inherits ExpressionPanel
		Public Sub New()
			MyBase.New()
		StartEdit(New GridColumn())
		End Sub

		Private fUnboundColumn As GridColumn
		Public Property UnboundColumn() As GridColumn
			Get
				Return fUnboundColumn
			End Get
			Set(ByVal value As GridColumn)
				If fUnboundColumn Is value Then
					Return
				End If
				StartEdit(value)
			End Set
		End Property

		Protected Overrides Function CreateForm(ParamArray ByVal arguments() As Object) As ExpressionEditorForm
			Return New UnboundColumnExpressionEditorForm(arguments(0), Nothing)
		End Function

		Protected Overrides Sub ApplyExpression(ByVal expression As String)
			If Arguments Is Nothing Then
				Return
			End If
			CType(Arguments(0), GridColumn).UnboundExpression = expression
		End Sub

		Public Overrides Sub StartEdit(ParamArray ByVal arguments() As Object)
			If arguments.Length < 1 Then
				Return
			End If
			Dim unboundColumn As GridColumn = TryCast(arguments(0), GridColumn)
			If unboundColumn Is Nothing Then
				Return
			End If
			fUnboundColumn = unboundColumn
			MyBase.StartEdit(unboundColumn)
		End Sub
	End Class
End Namespace