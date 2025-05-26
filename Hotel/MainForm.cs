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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private UserControl currentControl;

        // Constructor إضافي لقبول UserControl
        public MainForm(UserControl initialControl)
        {
            InitializeComponent();
            LoadUserControl(initialControl);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // لا شيء هنا حالياً
        }

        // دالة لعرض أي UserControl في mainPanel
        private void LoadUserControl(UserControl uc)
        {
            if (currentControl != null)
            {
                mainPanel.Controls.Remove(currentControl);
                currentControl.Dispose();
            }

            currentControl = uc;
            uc.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(uc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            // مثال: تحميل واجهة الحجز مثلاً
            //  var reservations = new ReservationsListControl();
            //  LoadUserControl(reservations);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            // مثال: تحميل الواجهة الرئيسية
           // var dashboard = new DashboardControl();
        //    LoadUserControl(dashboard);
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
