using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class HomeUserControl : UserControl
    {
        public HomeUserControl()
        {
            InitializeComponent();
        }

        private void pictureLogo_Click(object sender, EventArgs e)
        {
            // يمكنك إضافة حدث عند الضغط على شعار الفندق إذا أردت
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {
            // لا حاجة لشيء حالياً عند الضغط على الترحيب
        }

        private void btnBookNow_Click(object sender, EventArgs e)
        {
            // عند الضغط على زر "احجز الآن" افتح نموذج الحجز
          //  BookingForm bookingForm = new BookingForm();
            //bookingForm.ShowDialog();
        }

        private void lblDescription_Click(object sender, EventArgs e)
        {
            // لا حاجة لشيء حالياً عند الضغط على الوصف
        }
    }
}
