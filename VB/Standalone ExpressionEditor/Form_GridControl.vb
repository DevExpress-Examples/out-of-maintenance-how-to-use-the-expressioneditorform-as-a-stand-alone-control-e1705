Imports DevExpress.XtraGrid.Columns
Imports System
Imports System.Data
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq

Namespace Standalone_ExpressionEditor
	Partial Public Class Form_GridControl
		Inherits DevExpress.XtraEditors.XtraForm

		Public Sub New()
			InitializeComponent()
			gridControl1.DataSource = GetData()
			AddUnboundColumns()
			PopulateComboWithUnboundColumns()
			comboBoxEdit1.EditValue = "Expression A"
		End Sub
		Private Sub comboBoxEdit1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comboBoxEdit1.SelectedIndexChanged
			Dim unboundColumn As GridColumn = gridView1.Columns(comboBoxEdit1.EditValue.ToString())
			unboundExpressionPanel1.StartEdit(unboundColumn)
		End Sub


		Private Sub AddUnboundColumns()
			Dim col1 As GridColumn = gridView1.Columns.AddVisible("Expression A")
			col1.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
			col1.UnboundExpression = "[UnitPrice] * [UnitsInStock]"

			Dim col2 As GridColumn = gridView1.Columns.AddVisible("Expression B")
			col2.UnboundType = DevExpress.Data.UnboundColumnType.Object
		End Sub
		Private Sub PopulateComboWithUnboundColumns()
			For Each col As GridColumn In gridView1.Columns
				If col.UnboundType <> DevExpress.Data.UnboundColumnType.Bound Then
					comboBoxEdit1.Properties.Items.Add(col.FieldName)
				End If
			Next col
		End Sub
		Private Function GetData() As DataTable
			If File.Exists("northwind_data.xml") Then
				Dim ds As New DataSet()
				ds.ReadXml("northwind_data.xml", XmlReadMode.ReadSchema)
				Return ds.Tables(0)
			Else
				Dim dt As New DataTable()
				dt.Columns.Add("ID", GetType(Integer))
				dt.Columns.Add("Info", GetType(String))
				dt.Columns.Add("UnitPrice", GetType(Decimal))
				dt.Columns.Add("UnitsInStock", GetType(Integer))
				For i As Integer = 0 To 5
					dt.Rows.Add(i, "Info" & i, 3.37 * (i + 1), i + 3)
				Next i
				Return dt
			End If
		End Function
	End Class
End Namespace
