using IMBaby.Models;
using IMBaby.Repositories;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IMBaby
{
    public class ChildDetailForm : Form
    {
        private readonly Child _child;
        private readonly ChildGrowthRepository _growthRepo = new();
        private readonly VaccinationRepository _vaccRepo = new();
        private TabControl tabControl;
        private DataGridView dgvGrowth, dgvVacc;

        public ChildDetailForm(Child child)
        {
            _child = child;
            BuildUI();
            LoadData();
        }

        private void BuildUI()
        {
            this.Text = $"Child Details - {_child.name}";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            // Header card
            var pnlInfo = new Panel();
            pnlInfo.BackColor = Color.FromArgb(255, 240, 245);
            pnlInfo.Dock = DockStyle.Top;
            pnlInfo.Height = 120;
            pnlInfo.Padding = new Padding(20, 10, 20, 10);

            var lblChildName = new Label();
            lblChildName.Text = $"👶 {_child.name}";
            lblChildName.Font = new Font("Arial", 18, FontStyle.Bold);
            lblChildName.ForeColor = Color.HotPink;
            lblChildName.Location = new Point(20, 10);
            lblChildName.Size = new Size(400, 40);
            pnlInfo.Controls.Add(lblChildName);

            var gr = _growthRepo.GetLatestGrowth(_child.id);
            string bmiText = gr != null ? $"{gr.BMI} ({gr.BMICategory})" : "No data";
            Color bmiColor = gr != null ? gr.BMIColor : Color.Gray;

            var infoText = new Label();
            infoText.Text =
                $"DOB: {_child.date_of_birth}   |   Age: {_child.AgeDisplay}   |   " +
                $"Gender: {_child.gender}   |   Blood: {_child.blood_group}";
            infoText.Location = new Point(20, 50);
            infoText.Size = new Size(700, 24);
            infoText.Font = new Font("Arial", 10);
            infoText.ForeColor = Color.FromArgb(80, 80, 100);
            pnlInfo.Controls.Add(infoText);

            var bmiLabel = new Label();
            bmiLabel.Text = $"Latest BMI: {bmiText}";
            bmiLabel.Location = new Point(20, 78);
            bmiLabel.Size = new Size(400, 24);
            bmiLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            bmiLabel.ForeColor = bmiColor;
            pnlInfo.Controls.Add(bmiLabel);

            if (!string.IsNullOrEmpty(_child.medical_notes))
            {
                var notesLbl = new Label();
                notesLbl.Text = $"📋 Notes: {_child.medical_notes}";
                notesLbl.Location = new Point(450, 78);
                notesLbl.Size = new Size(400, 24);
                notesLbl.Font = new Font("Arial", 9);
                notesLbl.ForeColor = Color.DimGray;
                pnlInfo.Controls.Add(notesLbl);
            }

            // Tabs
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Arial", 10);

            var tabGrowth = new TabPage("📊 Growth Records");
            var tabVacc = new TabPage("💉 Vaccinations");

            BuildGrowthTab(tabGrowth);
            BuildVaccTab(tabVacc);

            tabControl.TabPages.AddRange(new[] { tabGrowth, tabVacc });

            this.Controls.Add(tabControl);
            this.Controls.Add(pnlInfo);
        }

        private void BuildGrowthTab(TabPage tab)
        {
            var pnlBtn = new Panel();
            pnlBtn.Dock = DockStyle.Top;
            pnlBtn.Height = 48;
            pnlBtn.Padding = new Padding(8);

            var btnAddGrowth = new Button();
            btnAddGrowth.Text = "➕ Add Growth Record";
            btnAddGrowth.Size = new Size(180, 34);
            btnAddGrowth.Location = new Point(8, 7);
            btnAddGrowth.BackColor = Color.SteelBlue;
            btnAddGrowth.ForeColor = Color.White;
            btnAddGrowth.FlatStyle = FlatStyle.Flat;
            btnAddGrowth.FlatAppearance.BorderSize = 0;
            btnAddGrowth.Click += BtnAddGrowth_Click;
            pnlBtn.Controls.Add(btnAddGrowth);

            dgvGrowth = CreateDGV();
            dgvGrowth.Columns.Add(new DataGridViewTextBoxColumn { Name = "gId", HeaderText = "ID", Width = 45 });
            dgvGrowth.Columns.Add(new DataGridViewTextBoxColumn { Name = "gDate", HeaderText = "Measure Date", FillWeight = 20 });
            dgvGrowth.Columns.Add(new DataGridViewTextBoxColumn { Name = "gHeight", HeaderText = "Height (cm)", FillWeight = 15 });
            dgvGrowth.Columns.Add(new DataGridViewTextBoxColumn { Name = "gWeight", HeaderText = "Weight (kg)", FillWeight = 15 });
            dgvGrowth.Columns.Add(new DataGridViewTextBoxColumn { Name = "gHC", HeaderText = "Head Circum (cm)", FillWeight = 18 });
            dgvGrowth.Columns.Add(new DataGridViewTextBoxColumn { Name = "gBMI", HeaderText = "BMI", FillWeight = 12 });
            dgvGrowth.Columns.Add(new DataGridViewTextBoxColumn { Name = "gCat", HeaderText = "Category", FillWeight = 15 });
            dgvGrowth.Columns.Add(new DataGridViewTextBoxColumn { Name = "gBy", HeaderText = "Recorded By", FillWeight = 15 });
            dgvGrowth.Dock = DockStyle.Fill;

            tab.Controls.Add(dgvGrowth);
            tab.Controls.Add(pnlBtn);
        }

        private void BtnAddGrowth_Click(object? sender, EventArgs e)
        {
            var form = new AddGrowthForm(_child.id);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadGrowthData();
            }
        }

        private void BuildVaccTab(TabPage tab)
        {
            var pnlBtn = new Panel();
            pnlBtn.Dock = DockStyle.Top;
            pnlBtn.Height = 48;

            var btnMarkGiven = new Button();
            btnMarkGiven.Text = "✅ Mark as Given";
            btnMarkGiven.Size = new Size(160, 34);
            btnMarkGiven.Location = new Point(8, 7);
            btnMarkGiven.BackColor = Color.SeaGreen;
            btnMarkGiven.ForeColor = Color.White;
            btnMarkGiven.FlatStyle = FlatStyle.Flat;
            btnMarkGiven.FlatAppearance.BorderSize = 0;
            btnMarkGiven.Click += BtnMarkGiven_Click;
            pnlBtn.Controls.Add(btnMarkGiven);

            var lblLegend = new Label();
            lblLegend.Text = "🟢 Given  🟡 Due Soon (≤30 days)  🔴 Overdue  🔵 Upcoming";
            lblLegend.Location = new Point(180, 14);
            lblLegend.Size = new Size(500, 22);
            lblLegend.Font = new Font("Arial", 9);
            lblLegend.ForeColor = Color.DimGray;
            pnlBtn.Controls.Add(lblLegend);

            dgvVacc = CreateDGV();
            dgvVacc.Columns.Add(new DataGridViewTextBoxColumn { Name = "vId", HeaderText = "ID", Width = 45 });
            dgvVacc.Columns.Add(new DataGridViewTextBoxColumn { Name = "vName", HeaderText = "Vaccine", FillWeight = 30 });
            dgvVacc.Columns.Add(new DataGridViewTextBoxColumn { Name = "vAge", HeaderText = "Age (months)", FillWeight = 15 });
            dgvVacc.Columns.Add(new DataGridViewTextBoxColumn { Name = "vDue", HeaderText = "Due Date", FillWeight = 18 });
            dgvVacc.Columns.Add(new DataGridViewTextBoxColumn { Name = "vGiven", HeaderText = "Given Date", FillWeight = 18 });
            dgvVacc.Columns.Add(new DataGridViewTextBoxColumn { Name = "vStatus", HeaderText = "Status", FillWeight = 15 });
            dgvVacc.Columns.Add(new DataGridViewTextBoxColumn { Name = "vNotes", HeaderText = "Notes", FillWeight = 20 });
            dgvVacc.Dock = DockStyle.Fill;

            tab.Controls.Add(dgvVacc);
            tab.Controls.Add(pnlBtn);
        }

        private void BtnMarkGiven_Click(object? sender, EventArgs e)
        {
            if (dgvVacc.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a vaccine to mark as given.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int vacId = Convert.ToInt32(dgvVacc.SelectedRows[0].Cells["vId"].Value);
            string status = dgvVacc.SelectedRows[0].Cells["vStatus"].Value?.ToString() ?? "";
            if (status == "Given")
            {
                MessageBox.Show("This vaccine is already marked as given.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string notes = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter any notes (optional):", "Mark Vaccine Given", "");

            _vaccRepo.MarkGiven(vacId, DateTime.Today.ToString("yyyy-MM-dd"), notes);
            LoadVaccData();
            MessageBox.Show("✅ Vaccine marked as given!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadData()
        {
            LoadGrowthData();
            LoadVaccData();
        }

        private void LoadGrowthData()
        {
            dgvGrowth.Rows.Clear();
            var records = _growthRepo.GetGrowthRecords(_child.id);
            foreach (var g in records)
            {
                int ri = dgvGrowth.Rows.Add(g.id, g.measure_date,
                    g.height_cm, g.weight_kg, g.head_circum_cm,
                    g.BMI, g.BMICategory, g.recorded_by);

                dgvGrowth.Rows[ri].Cells["gCat"].Style.ForeColor = g.BMIColor;
                dgvGrowth.Rows[ri].Cells["gBMI"].Style.ForeColor = g.BMIColor;
            }
        }

        private void LoadVaccData()
        {
            dgvVacc.Rows.Clear();
            var vaccinations = _vaccRepo.GetVaccinations(_child.id);
            foreach (var v in vaccinations)
            {
                string statusDisplay = v.StatusDisplay;
                int ri = dgvVacc.Rows.Add(v.id, v.vaccine_name, v.due_age_months,
                    v.due_date, v.given_date, statusDisplay, v.notes);

                dgvVacc.Rows[ri].Cells["vStatus"].Style.BackColor = v.StatusColor;
                dgvVacc.Rows[ri].Cells["vStatus"].Style.ForeColor =
                    statusDisplay == "Overdue" ? Color.White : Color.Black;
            }
        }

        private static DataGridView CreateDGV()
        {
            var dgv = new DataGridView();
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 182, 193);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 34;
            dgv.DefaultCellStyle.Font = new Font("Arial", 9);
            dgv.RowTemplate.Height = 30;
            dgv.GridColor = Color.FromArgb(240, 230, 235);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 250, 253);
            return dgv;
        }
    }
}
