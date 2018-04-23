using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraEditors;

namespace WindowsApplication51 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            gridPanel.StartEdit(gridView1.Columns["ExpressionA"]);
        }

        private void simpleButton2_Click(object sender, EventArgs e) {
            gridPanel.StartEdit(gridView1.Columns["ExpressionB"]);

        }

        private void Form1_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'nwindDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.nwindDataSet.Products);

        }
    }
}