using DevExpress.XtraGrid.Columns;
using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Standalone_ExpressionEditor
{
    public partial class Form_GridControl : DevExpress.XtraEditors.XtraForm
    {
        public Form_GridControl() {
            InitializeComponent();
            gridControl1.DataSource = GetData();
            AddUnboundColumns();
            PopulateComboWithUnboundColumns();
            comboBoxEdit1.EditValue = "Expression A";
        }
        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e) {
            GridColumn unboundColumn = gridView1.Columns[comboBoxEdit1.EditValue.ToString()];
            unboundExpressionPanel1.StartEdit(unboundColumn);
        }


        void AddUnboundColumns() {
            GridColumn col1 = gridView1.Columns.AddVisible("Expression A");
            col1.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            col1.UnboundExpression = "[UnitPrice] * [UnitsInStock]";

            GridColumn col2 = gridView1.Columns.AddVisible("Expression B");
            col2.UnboundType = DevExpress.Data.UnboundColumnType.Object;
        }
        void PopulateComboWithUnboundColumns() {
            foreach (GridColumn col in gridView1.Columns) {
                if (col.UnboundType != DevExpress.Data.UnboundColumnType.Bound)
                    comboBoxEdit1.Properties.Items.Add(col.FieldName);
            }
        }
        DataTable GetData() {
            if (File.Exists("northwind_data.xml")) {
                DataSet ds = new DataSet();
                ds.ReadXml("northwind_data.xml", XmlReadMode.ReadSchema);
                return ds.Tables[0];
            } else {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Info", typeof(string));
                dt.Columns.Add("UnitPrice", typeof(decimal));
                dt.Columns.Add("UnitsInStock", typeof(int));
                for (int i = 0; i < 6; i++) {
                    dt.Rows.Add(i, "Info" + i, 3.37 * (i + 1), i + 3);
                }
                return dt;
            }
        }
    }
}
