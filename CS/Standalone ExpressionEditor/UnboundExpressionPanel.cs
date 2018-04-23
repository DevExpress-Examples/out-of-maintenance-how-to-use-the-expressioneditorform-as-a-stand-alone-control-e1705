using DevExpress.Data;
using DevExpress.Data.ExpressionEditor;
using DevExpress.DataAccess.UI.ExpressionEditor;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ExpressionEditor;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Internal;
using DevExpress.XtraTreeList.Columns;
using System;
using System.ComponentModel;
using System.Linq;

namespace Standalone_ExpressionEditor
{
    [ToolboxItem(true)]
    [DesignerCategory("")]
    public class UnboundExpressionPanel : PanelControl
    {
        ExpressionEditorView view;
        object column;
        IDataColumnInfo columnContext;
        public UnboundExpressionPanel() : base() {
            BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        }
        void DestroyExpressionControls() {
            if (view != null) {
                Controls.Remove(view);
                view.Dispose();
            }
        }
        ExpressionEditorView CreateExpressionControl() {
            ExpressionEditorControl control = new ExpressionEditorControl();
            if (column is GridColumn)
                columnContext = new GridColumnIDataColumnInfoWrapper(column as GridColumn, GridColumnIDataColumnInfoWrapperEnum.ExpressionEditor);
            if (column is TreeListColumn)
                columnContext = column as IDataColumnInfo;
            control.Context = ExpressionEditorHelper.GetExpressionEditorContext(LookAndFeel, columnContext);
            ExpressionEditorView expressionView = new ExpressionEditorView(control.LookAndFeel, control);
            expressionView.Dock = System.Windows.Forms.DockStyle.Fill;
            expressionView.TopLevel = false;
            expressionView.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            expressionView.Ok += ExpressionView_Ok;
            expressionView.Cancel += ExpressionView_Cancel;
            expressionView.Visible = true;
            return expressionView;
        }
        public void StartEdit(object columnObject) {
            SuspendLayout();
            column = columnObject;
            DestroyExpressionControls();
            view = CreateExpressionControl();
            Controls.Add(view);
            string expression = string.Empty;
            if (column is GridColumn)
                expression = (column as GridColumn).UnboundExpression;
            if (column is TreeListColumn)
                expression = (column as TreeListColumn).UnboundExpression;
            view.ExpressionString = UnboundExpressionConvertHelper.ConvertToCaption(columnContext, expression);
            ResumeLayout();
        }

        private void ExpressionView_Ok(object sender, EventArgs e) {
            string expression = UnboundExpressionConvertHelper.ConvertToFields(columnContext, view.ExpressionString);
            if (column is GridColumn)
                (column as GridColumn).UnboundExpression = expression;
            if (column is TreeListColumn)
                (column as TreeListColumn).UnboundExpression = expression;
        }
        private void ExpressionView_Cancel(object sender, EventArgs e) {
            // TO DO
        }
    }
}
