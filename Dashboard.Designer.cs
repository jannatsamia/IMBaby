namespace IMBaby
{
    partial class Dashboard
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Top header panel
            var pnlHeader = new Panel();
            pnlHeader.BackColor = System.Drawing.Color.HotPink;
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 70;

            var lblLogo = new Label();
            lblLogo.Text = "🍼 IMBaby";
            lblLogo.Font = new System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold);
            lblLogo.ForeColor = System.Drawing.Color.White;
            lblLogo.Location = new System.Drawing.Point(16, 15);
            lblLogo.Size = new System.Drawing.Size(200, 40);
            pnlHeader.Controls.Add(lblLogo);

            var lblHeaderSub = new Label();
            lblHeaderSub.Text = "Baby Health Care Dashboard";
            lblHeaderSub.Font = new System.Drawing.Font("Arial", 10);
            lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(255, 200, 215);
            lblHeaderSub.Location = new System.Drawing.Point(220, 22);
            lblHeaderSub.Size = new System.Drawing.Size(300, 25);
            pnlHeader.Controls.Add(lblHeaderSub);

            // Logout button in header
            btnLogout = new Button();
            btnLogout.Text = "🚪 Logout";
            btnLogout.Location = new System.Drawing.Point(1320, 17);
            btnLogout.Size = new System.Drawing.Size(110, 36);
            btnLogout.BackColor = System.Drawing.Color.FromArgb(220, 60, 80);
            btnLogout.ForeColor = System.Drawing.Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Click += btnLogout_Click;
            pnlHeader.Controls.Add(btnLogout);

            // Alert panel
            pnlAlert = new Panel();
            pnlAlert.BackColor = System.Drawing.Color.FromArgb(255, 243, 205);
            pnlAlert.Dock = DockStyle.Top;
            pnlAlert.Height = 36;
            pnlAlert.Visible = false;

            lblAlertText = new Label();
            lblAlertText.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);
            lblAlertText.ForeColor = System.Drawing.Color.FromArgb(133, 77, 14);
            lblAlertText.Dock = DockStyle.Fill;
            lblAlertText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            pnlAlert.Controls.Add(lblAlertText);

            // Toolbar panel
            var pnlToolbar = new Panel();
            pnlToolbar.BackColor = System.Drawing.Color.White;
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Height = 55;
            pnlToolbar.Padding = new Padding(10, 8, 10, 8);

            btnadd = new Button();
            btnadd.Text = "➕ Add Child";
            btnadd.Location = new System.Drawing.Point(10, 10);
            btnadd.Size = new System.Drawing.Size(130, 35);
            btnadd.BackColor = System.Drawing.Color.HotPink;
            btnadd.ForeColor = System.Drawing.Color.White;
            btnadd.FlatStyle = FlatStyle.Flat;
            btnadd.FlatAppearance.BorderSize = 0;
            btnadd.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            btnadd.Click += btnadd_Click;
            pnlToolbar.Controls.Add(btnadd);

            btnedit = new Button();
            btnedit.Text = "✏ Edit";
            btnedit.Location = new System.Drawing.Point(150, 10);
            btnedit.Size = new System.Drawing.Size(100, 35);
            btnedit.BackColor = System.Drawing.Color.SteelBlue;
            btnedit.ForeColor = System.Drawing.Color.White;
            btnedit.FlatStyle = FlatStyle.Flat;
            btnedit.FlatAppearance.BorderSize = 0;
            btnedit.Font = new System.Drawing.Font("Arial", 10);
            btnedit.Click += btnedit_Click;
            pnlToolbar.Controls.Add(btnedit);

            btndelete = new Button();
            btndelete.Text = "🗑 Delete";
            btndelete.Location = new System.Drawing.Point(260, 10);
            btndelete.Size = new System.Drawing.Size(100, 35);
            btndelete.BackColor = System.Drawing.Color.FromArgb(220, 80, 80);
            btndelete.ForeColor = System.Drawing.Color.White;
            btndelete.FlatStyle = FlatStyle.Flat;
            btndelete.FlatAppearance.BorderSize = 0;
            btndelete.Font = new System.Drawing.Font("Arial", 10);
            btndelete.Click += btndelete_Click;
            pnlToolbar.Controls.Add(btndelete);

            btnRefresh = new Button();
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.Location = new System.Drawing.Point(370, 10);
            btnRefresh.Size = new System.Drawing.Size(100, 35);
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(100, 180, 100);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Font = new System.Drawing.Font("Arial", 10);
            btnRefresh.Click += btnRefresh_Click;
            pnlToolbar.Controls.Add(btnRefresh);

            // Search
            txtSearch = new TextBox();
            txtSearch.Location = new System.Drawing.Point(490, 13);
            txtSearch.Size = new System.Drawing.Size(220, 30);
            txtSearch.Font = new System.Drawing.Font("Arial", 10);
            txtSearch.PlaceholderText = "Search by name...";
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.KeyDown += txtSearch_KeyDown;
            pnlToolbar.Controls.Add(txtSearch);

            btnsearch = new Button();
            btnsearch.Text = "🔍";
            btnsearch.Location = new System.Drawing.Point(718, 12);
            btnsearch.Size = new System.Drawing.Size(40, 32);
            btnsearch.FlatStyle = FlatStyle.Flat;
            btnsearch.Click += btnsearch_Click;
            pnlToolbar.Controls.Add(btnsearch);

            // Status bar
            var pnlStatus = new Panel();
            pnlStatus.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            pnlStatus.Dock = DockStyle.Bottom;
            pnlStatus.Height = 30;

            lblCount = new Label();
            lblCount.Text = "Total Children: 0";
            lblCount.Font = new System.Drawing.Font("Arial", 9);
            lblCount.ForeColor = System.Drawing.Color.Gray;
            lblCount.Dock = DockStyle.Fill;
            lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblCount.Padding = new Padding(10, 0, 0, 0);
            pnlStatus.Controls.Add(lblCount);

            var lblHint = new Label();
            lblHint.Text = "💡 Double-click a row to view full details & vaccinations";
            lblHint.Font = new System.Drawing.Font("Arial", 8);
            lblHint.ForeColor = System.Drawing.Color.Gray;
            lblHint.Dock = DockStyle.Right;
            lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lblHint.Size = new System.Drawing.Size(380, 30);
            lblHint.Padding = new Padding(0, 0, 10, 0);
            pnlStatus.Controls.Add(lblHint);

            // DataGridView
            childstable = new DataGridView();
            childstable.Dock = DockStyle.Fill;
            childstable.BackgroundColor = System.Drawing.Color.White;
            childstable.BorderStyle = BorderStyle.None;
            childstable.RowHeadersVisible = false;
            childstable.AllowUserToAddRows = false;
            childstable.AllowUserToDeleteRows = false;
            childstable.ReadOnly = true;
            childstable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            childstable.MultiSelect = false;
            childstable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            childstable.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 182, 193);
            childstable.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(80, 20, 40);
            childstable.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            childstable.ColumnHeadersHeight = 38;
            childstable.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 10);
            childstable.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 220, 230);
            childstable.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            childstable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 253);
            childstable.GridColor = System.Drawing.Color.FromArgb(240, 230, 235);
            childstable.RowTemplate.Height = 36;
            childstable.CellDoubleClick += childstable_CellDoubleClick;

            // Columns
            childstable.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", Width = 40, FillWeight = 5 });
            childstable.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "👶 Name", FillWeight = 20 });
            childstable.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAge", HeaderText = "Age", FillWeight = 12 });
            childstable.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGender", HeaderText = "Gender", FillWeight = 10 });
            childstable.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBG", HeaderText = "Blood Group", FillWeight = 10 });
            childstable.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBMI", HeaderText = "💪 BMI", FillWeight = 18 });
            childstable.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAlert", HeaderText = "💉 Vaccine Status", FillWeight = 15 });
            childstable.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNotes", HeaderText = "Medical Notes", FillWeight = 20 });

            // Form
            this.Text = "IMBaby Dashboard";
            this.Size = new System.Drawing.Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White;
            this.Load += Dashboard_Load;

            // Add controls (reverse order for DockStyle)
            this.Controls.Add(childstable);
            this.Controls.Add(pnlStatus);
            this.Controls.Add(pnlToolbar);
            this.Controls.Add(pnlAlert);
            this.Controls.Add(pnlHeader);
        }

        private DataGridView childstable;
        private Button btnadd, btnedit, btndelete, btnsearch, btnLogout, btnRefresh;
        private TextBox txtSearch;
        private Label lblCount, lblAlertText;
        private Panel pnlAlert;
    }
}
