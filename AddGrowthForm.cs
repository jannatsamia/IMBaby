using IMBaby.Helpers;
using IMBaby.Models;
using IMBaby.Repositories;
using System;
using System.Windows.Forms;

namespace IMBaby
{
    public class AddGrowthForm : Form
    {
        private readonly int _childId;
        private TextBox txtHeight, txtWeight, txtHeadCircum;
        private ComboBox cmbHUnit, cmbWUnit, cmbHCUnit;
        private DateTimePicker dtpDate;
        private Label lblBMI;

        public AddGrowthForm(int childId)
        {
            _childId = childId;
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Add Growth Record";
            this.Size = new System.Drawing.Size(480, 380);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.White;

            int y = 20;
            AddLbl("Measurement Date", 20, y); y += 24;
            dtpDate = new DateTimePicker { Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(200, 28), MaxDate = DateTime.Today };
            this.Controls.Add(dtpDate); y += 45;

            AddLbl("Height", 20, y); y += 24;
            txtHeight = AddTxt(20, y, 160);
            cmbHUnit = AddCmb(190, y, new[] { "cm", "inch", "feet" }); y += 45;

            AddLbl("Weight", 20, y); y += 24;
            txtWeight = AddTxt(20, y, 160);
            cmbWUnit = AddCmb(190, y, new[] { "kg", "lb" }); y += 45;

            AddLbl("Head Circumference", 20, y); y += 24;
            txtHeadCircum = AddTxt(20, y, 160);
            cmbHCUnit = AddCmb(190, y, new[] { "cm", "inch" }); y += 50;

            lblBMI = new Label { Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(400, 30),
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.DimGray };
            this.Controls.Add(lblBMI); y += 40;

            txtHeight.TextChanged += UpdateBMI;
            txtWeight.TextChanged += UpdateBMI;
            cmbHUnit.SelectedIndexChanged += UpdateBMI;
            cmbWUnit.SelectedIndexChanged += UpdateBMI;

            var btnSave = new Button { Text = "💾 Save", Location = new System.Drawing.Point(60, y),
                Size = new System.Drawing.Size(130, 38),
                BackColor = System.Drawing.Color.HotPink, ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat, Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold) };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            var btnCancel = new Button { Text = "Cancel", Location = new System.Drawing.Point(210, y),
                Size = new System.Drawing.Size(100, 38), FlatStyle = FlatStyle.Flat };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancel);

            this.Height = y + 100;
        }

        private void UpdateBMI(object? sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtHeight.Text, out double h) ||
                    !double.TryParse(txtWeight.Text, out double w)) return;
                double hcm = cmbHUnit.Text == "inch" ? h * 2.54 : cmbHUnit.Text == "feet" ? h * 30.48 : h;
                double wkg = cmbWUnit.Text == "lb" ? w * 0.453592 : w;
                if (hcm <= 0 || wkg <= 0) return;
                double bmi = Math.Round(wkg / Math.Pow(hcm / 100.0, 2), 1);
                string cat = bmi < 18.5 ? "Underweight" : bmi < 25 ? "Normal" : bmi < 30 ? "Overweight" : "Obese";
                lblBMI.Text = $"BMI: {bmi}  →  {cat}";
                lblBMI.ForeColor = cat == "Normal" ? System.Drawing.Color.Green :
                                   cat == "Underweight" ? System.Drawing.Color.Orange :
                                   System.Drawing.Color.OrangeRed;
            }
            catch { }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (!double.TryParse(txtHeight.Text, out double h) ||
                !double.TryParse(txtWeight.Text, out double w) ||
                !double.TryParse(txtHeadCircum.Text, out double hc))
            {
                MessageBox.Show("Please enter valid numbers.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double hcm = cmbHUnit.Text == "inch" ? h * 2.54 : cmbHUnit.Text == "feet" ? h * 30.48 : h;
            double wkg = cmbWUnit.Text == "lb" ? w * 0.453592 : w;
            double hccm = cmbHCUnit.Text == "inch" ? hc * 2.54 : hc;

            var g = new ChildGrowth
            {
                child_id = _childId,
                measure_date = dtpDate.Value.ToString("yyyy-MM-dd"),
                height_cm = Math.Round(hcm, 2),
                weight_kg = Math.Round(wkg, 2),
                head_circum_cm = Math.Round(hccm, 2),
                recorded_by = Session.CurrentUsername
            };

            new ChildGrowthRepository().CreateChildGrowth(g);
            MessageBox.Show("✅ Growth record saved!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddLbl(string text, int x, int y)
        {
            var l = new Label { Text = text, Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(250, 20),
                Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(80, 80, 100) };
            this.Controls.Add(l);
        }

        private TextBox AddTxt(int x, int y, int w)
        {
            var t = new TextBox { Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(w, 28),
                Font = new System.Drawing.Font("Arial", 10), BorderStyle = BorderStyle.FixedSingle };
            this.Controls.Add(t);
            return t;
        }

        private ComboBox AddCmb(int x, int y, string[] items)
        {
            var c = new ComboBox { Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(70, 28),
                DropDownStyle = ComboBoxStyle.DropDownList };
            c.Items.AddRange(items);
            c.SelectedIndex = 0;
            this.Controls.Add(c);
            return c;
        }
    }
}
