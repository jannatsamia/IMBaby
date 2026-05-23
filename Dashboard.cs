using IMBaby.Helpers;
using IMBaby.Models;
using IMBaby.Repositories;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IMBaby
{
    public partial class Dashboard : Form
    {
        private readonly ChildRepository _childRepo = new();
        private readonly VaccinationRepository _vaccRepo = new();

        public Dashboard()
        {
            InitializeComponent();
            this.Text = $"IMBaby Dashboard - {Session.CurrentUsername}";
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadChildren();
            LoadAlertSummary();
        }

        private void LoadChildren(string search = "")
        {
            var children = _childRepo.GetChildren(Session.CurrentUserId, search);
            childstable.Rows.Clear();

            foreach (var c in children)
            {
                var gr = new ChildGrowthRepository().GetLatestGrowth(c.id);
                int alerts = _vaccRepo.GetAlertCount(c.id);

                string bmi = gr != null ? $"{gr.BMI} ({gr.BMICategory})" : "No record";
                string alertStr = alerts > 0 ? $"⚠ {alerts} alert(s)" : "✓ OK";

                int rowIdx = childstable.Rows.Add(
                    c.id, c.name, c.AgeDisplay, c.gender,
                    c.blood_group, bmi, alertStr, c.medical_notes
                );

                // Color the alert cell
                if (alerts > 0)
                    childstable.Rows[rowIdx].Cells[6].Style.BackColor = Color.MistyRose;
                else
                    childstable.Rows[rowIdx].Cells[6].Style.BackColor = Color.Honeydew;

                // Color BMI cell
                if (gr != null)
                    childstable.Rows[rowIdx].Cells[5].Style.ForeColor = gr.BMIColor;
            }

            lblCount.Text = $"Total Children: {children.Count}";
        }

        private void LoadAlertSummary()
        {
            var children = _childRepo.GetChildren(Session.CurrentUserId);
            int totalAlerts = 0;
            foreach (var c in children)
                totalAlerts += _vaccRepo.GetAlertCount(c.id);

            if (totalAlerts > 0)
            {
                pnlAlert.Visible = true;
                lblAlertText.Text = $"⚠  {totalAlerts} vaccination alert(s) require attention across your children's profiles!";
            }
            else
            {
                pnlAlert.Visible = false;
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            var form = new AddEditChild();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadChildren();
                LoadAlertSummary();
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (childstable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a child to edit.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int childId = Convert.ToInt32(childstable.SelectedRows[0].Cells["colId"].Value);
            var child = _childRepo.GetChild(childId);
            if (child == null) return;

            var form = new AddEditChild(child);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadChildren(txtSearch.Text);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (childstable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a child to delete.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = childstable.SelectedRows[0].Cells["colName"].Value?.ToString() ?? "";
            if (MessageBox.Show($"Are you sure you want to delete '{name}' and all their records?\nThis cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int childId = Convert.ToInt32(childstable.SelectedRows[0].Cells["colId"].Value);
                _childRepo.DeleteChild(childId);
                LoadChildren(txtSearch.Text);
                LoadAlertSummary();
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            LoadChildren(txtSearch.Text.Trim());
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadChildren(txtSearch.Text.Trim());
        }

        private void childstable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int childId = Convert.ToInt32(childstable.Rows[e.RowIndex].Cells["colId"].Value);
            var child = _childRepo.GetChild(childId);
            if (child == null) return;
            var detail = new ChildDetailForm(child);
            detail.ShowDialog();
            LoadChildren(txtSearch.Text);
            LoadAlertSummary();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Session.CurrentUser = null;
            var login = new Form1();
            login.Show();
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadChildren(txtSearch.Text);
            LoadAlertSummary();
        }
    }
}
