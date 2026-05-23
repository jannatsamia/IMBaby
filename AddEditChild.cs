using IMBaby.Helpers;
using IMBaby.Models;
using IMBaby.Repositories;
using System;
using System.Windows.Forms;

namespace IMBaby
{
    public class AddEditChild : Form
    {
        private readonly Child? _existing;
        private TextBox txtName, txtNotes, txtHeight, txtWeight, txtHeadCircum;
        private DateTimePicker dtpDOB;
        private RadioButton rdoMale, rdoFemale;
        private ComboBox cmbBloodGroup, cmbHeightUnit, cmbWeightUnit, cmbHCUnit;
        private Button btnSave, btnCancel;
        private Label lblBMIPreview;

        public AddEditChild(Child? child = null)
        {
            _existing = child;
            BuildUI();
            if (child != null) PopulateFields(child);
        }

        private void PopulateFields(Child c)
        {
            txtName.Text = c.name;
            if (DateTime.TryParse(c.date_of_birth, out var dob)) dtpDOB.Value = dob;
            rdoMale.Checked = c.gender == "Male";
            rdoFemale.Checked = c.gender == "Female";
            cmbBloodGroup.Text = c.blood_group;
            txtNotes.Text = c.medical_notes;
        }

        private void BuildUI()
        {
            this.Text = _existing == null ? "Add New Child" : $"Edit Child - {_existing.name}";
            this.Size = new System.Drawing.Size(860, 560);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.White;

            // Header
            var pnlHeader = new Panel();
            pnlHeader.BackColor = System.Drawing.Color.HotPink;
            pnlHeader.Size = new System.Drawing.Size(860, 55);
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            var lblTitle = new Label();
            lblTitle.Text = _existing == null ? "➕ Add New Child" : "✏ Edit Child Profile";
            lblTitle.Font = new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(20, 12);
            lblTitle.Size = new System.Drawing.Size(400, 32);
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            // --- Child Info Section ---
            AddSectionLabel("Child Information", 20, 70);

            AddFieldLabel("Full Name *", 30, 100);
            txtName = AddTxt(180, 100, 340);

            AddFieldLabel("Date of Birth *", 30, 140);
            dtpDOB = new DateTimePicker();
            dtpDOB.Location = new System.Drawing.Point(180, 140);
            dtpDOB.Size = new System.Drawing.Size(220, 28);
            dtpDOB.MaxDate = DateTime.Today;
            dtpDOB.Format = DateTimePickerFormat.Short;
            this.Controls.Add(dtpDOB);

            AddFieldLabel("Gender *", 30, 182);
            var grp = new GroupBox();
            grp.Location = new System.Drawing.Point(175, 175);
            grp.Size = new System.Drawing.Size(220, 36);
            grp.FlatStyle = FlatStyle.Flat;
            rdoMale = new RadioButton { Text = "Male", Location = new System.Drawing.Point(10, 10), AutoSize = true };
            rdoFemale = new RadioButton { Text = "Female", Location = new System.Drawing.Point(90, 10), AutoSize = true };
            grp.Controls.AddRange(new Control[] { rdoMale, rdoFemale });
            this.Controls.Add(grp);

            AddFieldLabel("Blood Group", 30, 225);
            cmbBloodGroup = new ComboBox();
            cmbBloodGroup.Location = new System.Drawing.Point(180, 222);
            cmbBloodGroup.Size = new System.Drawing.Size(120, 28);
            cmbBloodGroup.Items.AddRange(new object[] { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-", "Unknown" });
            cmbBloodGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBloodGroup.SelectedIndex = 8;
            this.Controls.Add(cmbBloodGroup);

            AddFieldLabel("Medical Notes", 30, 265);
            txtNotes = new TextBox();
            txtNotes.Location = new System.Drawing.Point(180, 262);
            txtNotes.Size = new System.Drawing.Size(340, 28);
            this.Controls.Add(txtNotes);

            // --- Growth Measurements Section (only shown for new child) ---
            if (_existing == null)
            {
                AddSectionLabel("Initial Growth Measurements", 20, 305);

                AddFieldLabel("Height", 30, 335);
                txtHeight = AddTxt(180, 335, 150);
                cmbHeightUnit = AddUnitCombo(340, 335, new[] { "cm", "inch", "feet" });

                AddFieldLabel("Weight", 30, 375);
                txtWeight = AddTxt(180, 375, 150);
                cmbWeightUnit = AddUnitCombo(340, 375, new[] { "kg", "lb" });

                AddFieldLabel("Head Circumference", 30, 415);
                txtHeadCircum = AddTxt(180, 415, 150);
                cmbHCUnit = AddUnitCombo(340, 415, new[] { "cm", "inch" });

                // BMI Preview
                lblBMIPreview = new Label();
                lblBMIPreview.Location = new System.Drawing.Point(420, 335);
                lblBMIPreview.Size = new System.Drawing.Size(250, 60);
                lblBMIPreview.Font = new System.Drawing.Font("Arial", 10);
                lblBMIPreview.ForeColor = System.Drawing.Color.DimGray;
                lblBMIPreview.Text = "BMI will be calculated\nafter saving.";
                this.Controls.Add(lblBMIPreview);

                txtHeight.TextChanged += UpdateBMIPreview;
                txtWeight.TextChanged += UpdateBMIPreview;
                cmbHeightUnit.SelectedIndexChanged += UpdateBMIPreview;
                cmbWeightUnit.SelectedIndexChanged += UpdateBMIPreview;
            }

            // Buttons
            int btnY = _existing == null ? 465 : 310;
            btnSave = new Button();
            btnSave.Text = "💾 Save";
            btnSave.Location = new System.Drawing.Point(200, btnY);
            btnSave.Size = new System.Drawing.Size(140, 40);
            btnSave.BackColor = System.Drawing.Color.HotPink;
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new System.Drawing.Point(360, btnY);
            btnCancel.Size = new System.Drawing.Size(120, 40);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Arial", 11);
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancel);

            this.Height = btnY + 100;
        }

        private void UpdateBMIPreview(object? sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtHeight.Text, out double h) ||
                    !double.TryParse(txtWeight.Text, out double w)) return;

                double hcm = cmbHeightUnit.Text == "inch" ? h * 2.54 :
                             cmbHeightUnit.Text == "feet" ? h * 30.48 : h;
                double wkg = cmbWeightUnit.Text == "lb" ? w * 0.453592 : w;

                if (hcm <= 0 || wkg <= 0) return;
                double hm = hcm / 100.0;
                double bmi = Math.Round(wkg / (hm * hm), 1);
                string cat = bmi < 18.5 ? "Underweight" : bmi < 25 ? "Normal" : bmi < 30 ? "Overweight" : "Obese";

                lblBMIPreview.Text = $"BMI Preview:\n{bmi}  →  {cat}";
                lblBMIPreview.ForeColor = cat == "Normal" ? System.Drawing.Color.Green :
                                          cat == "Underweight" ? System.Drawing.Color.Orange :
                                          System.Drawing.Color.OrangeRed;
            }
            catch { }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!rdoMale.Checked && !rdoFemale.Checked)
            {
                MessageBox.Show("Please select gender.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var child = new Child
            {
                id = _existing?.id ?? 0,
                user_id = Session.CurrentUserId,
                name = txtName.Text.Trim(),
                date_of_birth = dtpDOB.Value.ToString("yyyy-MM-dd"),
                gender = rdoMale.Checked ? "Male" : "Female",
                blood_group = cmbBloodGroup.Text,
                medical_notes = txtNotes.Text.Trim()
            };

            var repo = new ChildRepository();

            if (_existing == null)
            {
                // Validate growth fields
                if (!double.TryParse(txtHeight.Text, out double height) ||
                    !double.TryParse(txtWeight.Text, out double weight) ||
                    !double.TryParse(txtHeadCircum.Text, out double hc))
                {
                    MessageBox.Show("Please enter valid numbers for height, weight, and head circumference.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int childId = repo.CreateChild(child);
                if (childId <= 0)
                {
                    MessageBox.Show("Failed to save child. Check database connection.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Save growth
                double heightCm = cmbHeightUnit.Text == "inch" ? height * 2.54 :
                                  cmbHeightUnit.Text == "feet" ? height * 30.48 : height;
                double weightKg = cmbWeightUnit.Text == "lb" ? weight * 0.453592 : weight;
                double hcCm = cmbHCUnit.Text == "inch" ? hc * 2.54 : hc;

                var growth = new ChildGrowth
                {
                    child_id = childId,
                    measure_date = DateTime.Today.ToString("yyyy-MM-dd"),
                    height_cm = Math.Round(heightCm, 2),
                    weight_kg = Math.Round(weightKg, 2),
                    head_circum_cm = Math.Round(hcCm, 2),
                    recorded_by = Session.CurrentUsername
                };
                new ChildGrowthRepository().CreateChildGrowth(growth);

                // Auto-generate vaccination schedule
                var schedule = VaccinationSchedule.GenerateSchedule(childId, dtpDOB.Value);
                new VaccinationRepository().InsertSchedule(schedule);

                MessageBox.Show($"✅ Child '{child.name}' added successfully!\nVaccination schedule has been auto-generated.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                repo.UpdateChild(child);
                MessageBox.Show($"✅ Child '{child.name}' updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private Label AddFieldLabel(string text, int x, int y)
        {
            var lbl = new Label { Text = text, Location = new System.Drawing.Point(x, y + 3),
                Size = new System.Drawing.Size(145, 22),
                Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(80, 80, 100) };
            this.Controls.Add(lbl);
            return lbl;
        }

        private void AddSectionLabel(string text, int x, int y)
        {
            var lbl = new Label { Text = text, Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(400, 24),
                Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.HotPink };
            var sep = new Label { Location = new System.Drawing.Point(x, y + 22),
                Size = new System.Drawing.Size(800, 2),
                BackColor = System.Drawing.Color.FromArgb(255, 182, 193) };
            this.Controls.Add(lbl);
            this.Controls.Add(sep);
        }

        private TextBox AddTxt(int x, int y, int w)
        {
            var t = new TextBox { Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(w, 28),
                Font = new System.Drawing.Font("Arial", 10),
                BorderStyle = BorderStyle.FixedSingle };
            this.Controls.Add(t);
            return t;
        }

        private ComboBox AddUnitCombo(int x, int y, string[] items)
        {
            var c = new ComboBox { Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(70, 28),
                DropDownStyle = ComboBoxStyle.DropDownList };
            c.Items.AddRange(items);
            c.SelectedIndex = 0;
            this.Controls.Add(c);
            return c;
        }
    }
}
