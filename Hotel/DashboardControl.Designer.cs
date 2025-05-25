namespace Hotel
{
    partial class DashboardControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.lblRoomsTitle = new System.Windows.Forms.Label();
            this.lblReservationsTitle = new System.Windows.Forms.Label();
            this.lblClientsTitle = new System.Windows.Forms.Label();
            this.lblUsersTitle = new System.Windows.Forms.Label();
            this.lblTotalRooms = new System.Windows.Forms.Label();
            this.lblTotalClients = new System.Windows.Forms.Label();
            this.lblTotalReservations = new System.Windows.Forms.Label();
            this.lblTotalUsers = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(307, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(468, 78);
            this.label2.TabIndex = 1;
            this.label2.Text = "أهلًا بك في لوحة التحكم";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblRoomsTitle
            // 
            this.lblRoomsTitle.AutoSize = true;
            this.lblRoomsTitle.BackColor = System.Drawing.Color.PowderBlue;
            this.lblRoomsTitle.Location = new System.Drawing.Point(52, 16);
            this.lblRoomsTitle.Name = "lblRoomsTitle";
            this.lblRoomsTitle.Size = new System.Drawing.Size(121, 40);
            this.lblRoomsTitle.TabIndex = 2;
            this.lblRoomsTitle.Text = "عدد الغرف";
            this.lblRoomsTitle.Click += new System.EventHandler(this.lblRoomsTitle_Click);
            // 
            // lblReservationsTitle
            // 
            this.lblReservationsTitle.AutoSize = true;
            this.lblReservationsTitle.Location = new System.Drawing.Point(34, 11);
            this.lblReservationsTitle.Name = "lblReservationsTitle";
            this.lblReservationsTitle.Size = new System.Drawing.Size(152, 40);
            this.lblReservationsTitle.TabIndex = 3;
            this.lblReservationsTitle.Text = "عدد الحجوزات";
            this.lblReservationsTitle.Click += new System.EventHandler(this.lblReservationsTitle_Click);
            // 
            // lblClientsTitle
            // 
            this.lblClientsTitle.AutoSize = true;
            this.lblClientsTitle.Font = new System.Drawing.Font("TheSans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientsTitle.Location = new System.Drawing.Point(29, 3);
            this.lblClientsTitle.Name = "lblClientsTitle";
            this.lblClientsTitle.Size = new System.Drawing.Size(140, 40);
            this.lblClientsTitle.TabIndex = 4;
            this.lblClientsTitle.Text = " عدد العملاء";
            this.lblClientsTitle.Click += new System.EventHandler(this.lblClientsTitle_Click);
            // 
            // lblUsersTitle
            // 
            this.lblUsersTitle.AutoSize = true;
            this.lblUsersTitle.Location = new System.Drawing.Point(6, 11);
            this.lblUsersTitle.Name = "lblUsersTitle";
            this.lblUsersTitle.Size = new System.Drawing.Size(191, 40);
            this.lblUsersTitle.TabIndex = 5;
            this.lblUsersTitle.Text = "عدد المستخدمين";
            this.lblUsersTitle.Click += new System.EventHandler(this.lblUsersTitle_Click);
            // 
            // lblTotalRooms
            // 
            this.lblTotalRooms.AutoSize = true;
            this.lblTotalRooms.Location = new System.Drawing.Point(70, 56);
            this.lblTotalRooms.Name = "lblTotalRooms";
            this.lblTotalRooms.Size = new System.Drawing.Size(76, 40);
            this.lblTotalRooms.TabIndex = 6;
            this.lblTotalRooms.Text = "label1";
            this.lblTotalRooms.Click += new System.EventHandler(this.lblTotalRooms_Click);
            // 
            // lblTotalClients
            // 
            this.lblTotalClients.AutoSize = true;
            this.lblTotalClients.Font = new System.Drawing.Font("TheSans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalClients.Location = new System.Drawing.Point(71, 56);
            this.lblTotalClients.Name = "lblTotalClients";
            this.lblTotalClients.Size = new System.Drawing.Size(78, 40);
            this.lblTotalClients.TabIndex = 7;
            this.lblTotalClients.Text = "label3";
            this.lblTotalClients.Click += new System.EventHandler(this.lblTotalClients_Click);
            // 
            // lblTotalReservations
            // 
            this.lblTotalReservations.AutoSize = true;
            this.lblTotalReservations.Location = new System.Drawing.Point(70, 59);
            this.lblTotalReservations.Name = "lblTotalReservations";
            this.lblTotalReservations.Size = new System.Drawing.Size(80, 40);
            this.lblTotalReservations.TabIndex = 8;
            this.lblTotalReservations.Text = "label4";
            this.lblTotalReservations.Click += new System.EventHandler(this.lblTotalReservations_Click);
            // 
            // lblTotalUsers
            // 
            this.lblTotalUsers.AutoSize = true;
            this.lblTotalUsers.Location = new System.Drawing.Point(72, 59);
            this.lblTotalUsers.Name = "lblTotalUsers";
            this.lblTotalUsers.Size = new System.Drawing.Size(78, 40);
            this.lblTotalUsers.TabIndex = 9;
            this.lblTotalUsers.Text = "label5";
            this.lblTotalUsers.Click += new System.EventHandler(this.lblTotalUsers_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.panel1.Controls.Add(this.lblRoomsTitle);
            this.panel1.Controls.Add(this.lblTotalRooms);
            this.panel1.Font = new System.Drawing.Font("TheSans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(307, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 10;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PowderBlue;
            this.panel2.Controls.Add(this.lblClientsTitle);
            this.panel2.Controls.Add(this.lblTotalClients);
            this.panel2.Font = new System.Drawing.Font("TheSans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(575, 133);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 11;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.PowderBlue;
            this.panel3.Controls.Add(this.lblReservationsTitle);
            this.panel3.Controls.Add(this.lblTotalReservations);
            this.panel3.Font = new System.Drawing.Font("TheSans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(307, 259);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 100);
            this.panel3.TabIndex = 12;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.PowderBlue;
            this.panel4.Controls.Add(this.lblUsersTitle);
            this.panel4.Controls.Add(this.lblTotalUsers);
            this.panel4.Font = new System.Drawing.Font("TheSans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(575, 259);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 100);
            this.panel4.TabIndex = 13;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(550, 478);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(118, 46);
            this.btnExportPDF.TabIndex = 14;
            this.btnExportPDF.Text = "تقرير pdf";
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // DashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(1170, 649);
            this.Load += new System.EventHandler(this.DashboardControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRoomsTitle;
        private System.Windows.Forms.Label lblReservationsTitle;
        private System.Windows.Forms.Label lblClientsTitle;
        private System.Windows.Forms.Label lblUsersTitle;
        private System.Windows.Forms.Label lblTotalRooms;
        private System.Windows.Forms.Label lblTotalClients;
        private System.Windows.Forms.Label lblTotalReservations;
        private System.Windows.Forms.Label lblTotalUsers;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnExportPDF;
    }
}
