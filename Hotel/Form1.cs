using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Hotel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // دالة لتحميل أي UserControl داخل panelMain
        private void LoadUserControl(UserControl uc)
        {
            panelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }

        // عند تحميل الفورم لأول مرة، نعرض الـ Dashboard بشكل افتراضي
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadUserControl(new DashboardControl());
        }

        // أحداث أزرار التنقل
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LoadUserControl(new DashboardControl());
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            LoadUserControl(new RoomsControl());
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ClientsControl());
        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ReservationsListControl());
        }
        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void panelSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            LoadUserControl(new DashboardControl());
        }
        private UsersControl usersControl;
        private void btnUsers_Click(object sender, EventArgs e)
        {

            LoadUserControl(new UsersControl());
        }

        
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل تريد تسجيل الخروج؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // إظهار نافذة تسجيل الدخول
                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                // إغلاق النافذة الحالية
                this.Hide();  // أو this.Close(); إذا كنت لا تريد العودة لهذا الفورم لاحقًا
            }
        }

    }

}

