namespace IMBaby
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMain = new Panel();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.lblUser = new Label();
            this.txtuser = new TextBox();
            this.lblPass = new Label();
            this.txtpass = new TextBox();
            this.btnLogin = new Button();
            this.btnExit = new Button();
            this.lnkRegister = new LinkLabel();
            this.lblWelcome = new Label();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();

            // Form
            this.Text = "IMBaby - Child Health Management";
            this.Size = new System.Drawing.Size(560, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(255, 240, 245);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // pnlMain - card
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderStyle = BorderStyle.FixedSingle;
            this.pnlMain.Location = new System.Drawing.Point(80, 80);
            this.pnlMain.Size = new System.Drawing.Size(400, 460);
            this.pnlMain.Controls.AddRange(new Control[] {
                lblTitle, lblSubtitle, lblWelcome,
                lblUser, txtuser, lblPass, txtpass,
                btnLogin, btnExit, lnkRegister });

            // lblTitle
            lblTitle.Text = "🍼 IMBaby";
            lblTitle.Font = new System.Drawing.Font("Arial", 26, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.HotPink;
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblTitle.Size = new System.Drawing.Size(380, 50);
            lblTitle.Location = new System.Drawing.Point(10, 20);

            // lblSubtitle
            lblSubtitle.Text = "Child Health Management System";
            lblSubtitle.Font = new System.Drawing.Font("Arial", 10);
            lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblSubtitle.Size = new System.Drawing.Size(380, 25);
            lblSubtitle.Location = new System.Drawing.Point(10, 68);

            // lblWelcome
            lblWelcome.Text = "Welcome Back!";
            lblWelcome.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            lblWelcome.ForeColor = System.Drawing.Color.FromArgb(60, 60, 80);
            lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblWelcome.Size = new System.Drawing.Size(380, 35);
            lblWelcome.Location = new System.Drawing.Point(10, 110);

            // lblUser
            lblUser.Text = "Username";
            lblUser.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lblUser.ForeColor = System.Drawing.Color.FromArgb(80, 80, 100);
            lblUser.Location = new System.Drawing.Point(50, 165);
            lblUser.Size = new System.Drawing.Size(100, 22);

            // txtuser
            txtuser.Location = new System.Drawing.Point(50, 190);
            txtuser.Size = new System.Drawing.Size(300, 30);
            txtuser.Font = new System.Drawing.Font("Arial", 11);
            txtuser.BorderStyle = BorderStyle.FixedSingle;

            // lblPass
            lblPass.Text = "Password";
            lblPass.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lblPass.ForeColor = System.Drawing.Color.FromArgb(80, 80, 100);
            lblPass.Location = new System.Drawing.Point(50, 235);
            lblPass.Size = new System.Drawing.Size(100, 22);

            // txtpass
            txtpass.Location = new System.Drawing.Point(50, 260);
            txtpass.Size = new System.Drawing.Size(300, 30);
            txtpass.Font = new System.Drawing.Font("Arial", 11);
            txtpass.UseSystemPasswordChar = true;
            txtpass.BorderStyle = BorderStyle.FixedSingle;
            txtpass.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) btnLogin_Click(s, e); };

            // btnLogin
            btnLogin.Text = "Log In";
            btnLogin.Location = new System.Drawing.Point(50, 320);
            btnLogin.Size = new System.Drawing.Size(140, 40);
            btnLogin.BackColor = System.Drawing.Color.HotPink;
            btnLogin.ForeColor = System.Drawing.Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += btnLogin_Click;

            // btnExit
            btnExit.Text = "Exit";
            btnExit.Location = new System.Drawing.Point(210, 320);
            btnExit.Size = new System.Drawing.Size(140, 40);
            btnExit.BackColor = System.Drawing.Color.FromArgb(200, 200, 210);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new System.Drawing.Font("Arial", 12);
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.Click += btnExit_Click;

            // lnkRegister
            lnkRegister.Text = "New user? Register here";
            lnkRegister.Location = new System.Drawing.Point(110, 380);
            lnkRegister.Size = new System.Drawing.Size(200, 22);
            lnkRegister.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lnkRegister.LinkClicked += lnkRegister_LinkClicked;

            this.Controls.Add(this.pnlMain);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Panel pnlMain;
        private Label lblTitle, lblSubtitle, lblWelcome, lblUser, lblPass;
        private TextBox txtuser, txtpass;
        private Button btnLogin, btnExit;
        private LinkLabel lnkRegister;
    }
}
