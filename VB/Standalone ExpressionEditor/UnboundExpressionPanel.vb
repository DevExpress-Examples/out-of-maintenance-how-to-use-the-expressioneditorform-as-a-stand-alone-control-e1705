Imports DevExpress.Data
Imports DevExpress.Data.ExpressionEditor
Imports DevExpress.DataAccess.UI.ExpressionEditor
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.ExpressionEditor
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Internal
Imports DevExpress.XtraTreeList.Columns
Imports System
Imports System.ComponentModel
Imports System.Linq

Namespace Standalone_ExpressionEditor
	<ToolboxItem(True), DesignerCategory("")>
	Public Class UnboundExpressionPanel
		Inherits PanelControl

		Private view As ExpressionEditorView
		Private column As Object
		Private columnContext As IDataColumnInfo
		Public Sub New()
			MyBase.New()
			BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		End Sub
		Private Sub DestroyExpressionControls()
			If view IsNot Nothing Then
				Controls.Remove(view)
				view.Dispose()
			End If
		End Sub
		Private Function CreateExpressionControl() As ExpressionEditorView
			Dim control As New ExpressionEditorControl()
			If TypeOf column Is GridColumn Then
				columnContext = New GridColumnIDataColumnInfoWrapper(TryCast(column, GridColumn), GridColumnIDataColumnInfoWrapperEnum.ExpressionEditor)
			End If
			If TypeOf column Is TreeListColumn Then
				columnContext = TryCast(column, IDataColumnInfo)
			End If
			control.Context = ExpressionEditorHelper.GetExpressionEditorContext(LookAndFeel, columnContext)
			Dim expressionView As New ExpressionEditorView(control.LookAndFeel, control)
			expressionView.Dock = System.Windows.Forms.DockStyle.Fill
			expressionView.TopLevel = False
			expressionView.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
			AddHandler expressionView.Ok, AddressOf ExpressionView_Ok
			AddHandler expressionView.Cancel, AddressOf ExpressionView_Cancel
			expressionView.Visible = True
			Return expressionView
		End Function
		Public Sub StartEdit(ByVal columnObject As Object)
			SuspendLayout()
			column = columnObject
			DestroyExpressionControls()
			view = CreateExpressionControl()
			Controls.Add(view)
			Dim expression As String = String.Empty
			If TypeOf column Is GridColumn Then
				expression = (TryCast(column, GridColumn)).UnboundExpression
			End If
			If TypeOf column Is TreeListColumn Then
				expression = (TryCast(column, TreeListColumn)).UnboundExpression
			End If
			view.ExpressionString = UnboundExpressionConvertHelper.ConvertToCaption(columnContext, expression)
			ResumeLayout()
		End Sub

		Private Sub ExpressionView_Ok(ByVal sender As Object, ByVal e As EventArgs)
			Dim expression As String = UnboundExpressionConvertHelper.ConvertToFields(columnContext, view.ExpressionString)
			If TypeOf column Is GridColumn Then
				TryCast(column, GridColumn).UnboundExpression = expression
			End If
			If TypeOf column Is TreeListColumn Then
				TryCast(column, TreeListColumn).UnboundExpression = expression
			End If
		End Sub
		Private Sub ExpressionView_Cancel(ByVal sender As Object, ByVal e As EventArgs)
			' TO DO
		End Sub
	End Class
End Namespace
