using IMBaby.Models;
using IMBaby.Repositories;
using System;
using System.Windows.Forms;

namespace IMBaby
{
    public class RegisterForm : Form
    {
        private TextBox txtFullName, txtUsername, txtPassword, txtConfirm, txtEmail;
        private Button btnRegister, btnCancel;

        public RegisterForm()
        {
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Register New Account";
            this.Size = new System.Drawing.Size(420, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(255, 245, 250);

            int y = 20;
            AddLabel("Full Name", 30, y); y += 25;
            txtFullName = AddTextBox(30, y); y += 50;

            AddLabel("Username", 30, y); y += 25;
            txtUsername = AddTextBox(30, y); y += 50;

            AddLabel("Email", 30, y); y += 25;
            txtEmail = AddTextBox(30, y); y += 50;

            AddLabel("Password", 30, y); y += 25;
            txtPassword = AddTextBox(30, y, true); y += 50;

            AddLabel("Confirm Password", 30, y); y += 25;
            txtConfirm = AddTextBox(30, y, true); y += 55;

            btnRegister = new Button
            {
                Text = "Register",
                Location = new System.Drawing.Point(30, y),
                Size = new System.Drawing.Size(140, 38),
                BackColor = System.Drawing.Color.HotPink,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold)
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(190, y),
                Size = new System.Drawing.Size(140, 38),
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { btnRegister, btnCancel });
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtConfirm.Text)
            {
                MessageBox.Show("Passwords do not match.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var repo = new UserRepository();
            if (repo.UsernameExists(txtUsername.Text.Trim()))
            {
                MessageBox.Show("Username already exists. Choose another.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = new User
            {
                full_name = txtFullName.Text.Trim(),
                username = txtUsername.Text.Trim(),
                password = txtPassword.Text,
                email = txtEmail.Text.Trim()
            };

            if (repo.Register(user))
            {
                MessageBox.Show("Account created successfully! You can now log in.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Label AddLabel(string text, int x, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(200, 22),
                Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(80, 80, 100)
            };
            this.Controls.Add(lbl);
            return lbl;
        }

        private TextBox AddTextBox(int x, int y, bool password = false)
        {
            var txt = new TextBox
            {
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(340, 28),
                Font = new System.Drawing.Font("Arial", 10),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = password
            };
            this.Controls.Add(txt);
            return txt;
        }
    }
}
