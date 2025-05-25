using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // لا شيء ضروري هنا حاليًا
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // لا شيء ضروري هنا حاليًا
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // لا شيء ضروري هنا حاليًا
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // لا شيء ضروري هنا حاليًا
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // إغلاق البرنامج
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("يرجى إدخال اسم المستخدم أو البريد الإلكتروني وكلمة المرور.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // التحقق من بيانات الأدمن الثابتة
            if ((usernameOrEmail == "admin" || usernameOrEmail == "admin@gmail.com") && password == "admin")
            {
                this.Hide();
                Form1 main = new Form1();
                main.Show();
                return;
            }

            // التحقق من قاعدة البيانات
            string query = "SELECT * FROM users WHERE (username = @user OR email = @user) AND password = @pass";
            var parameters = new List<MySqlParameter>
            {
                DatabaseHelper.CreateParameter("@user", usernameOrEmail),
                DatabaseHelper.CreateParameter("@pass", password)
            };

            DataTable result = DatabaseHelper.ExecuteSelectCommand(query, parameters);

            if (result.Rows.Count > 0)
            {
                this.Hide();
                Form1 main = new Form1();
                main.Show();
            }
            else
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // لا شيء ضروري هنا حاليًا
        }

        private void btnreg_Click(object sender, EventArgs e)
        {
            // فتح نافذة إنشاء حساب جديدة
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }
    }
}
