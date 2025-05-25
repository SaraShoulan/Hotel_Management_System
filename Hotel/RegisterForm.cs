using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            // تعيين القيم الافتراضية للدور
            cmbRole.Items.Add("admin");
            cmbRole.Items.Add("receptionist");
            cmbRole.SelectedIndex = 1; // الافتراضي "receptionist"
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // لا شيء ضروري هنا حاليًا
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            // لا شيء ضروري هنا حاليًا
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // لا شيء ضروري هنا حاليًا
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            // لا شيء ضروري هنا حاليًا
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            // لا شيء ضروري هنا حاليًا
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // جلب القيم من الحقول
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string role = cmbRole.SelectedItem?.ToString();

            // التحقق من صحة البيانات
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) ||
                string.IsNullOrEmpty(role))
            {
                MessageBox.Show("يرجى ملء جميع الحقول.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // تحقق من تطابق كلمة المرور
            if (password != confirmPassword)
            {
                MessageBox.Show("كلمة المرور وتأكيدها غير متطابقين.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // تحقق من صحة البريد الإلكتروني (بسيط)
            if (!IsValidEmail(email))
            {
                MessageBox.Show("يرجى إدخال بريد إلكتروني صحيح.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // تحقق أن اسم المستخدم أو البريد الإلكتروني غير موجودين مسبقاً
                string checkQuery = "SELECT COUNT(*) FROM users WHERE username=@username OR email=@email";
                var checkParams = new List<MySqlParameter>
                {
                    DatabaseHelper.CreateParameter("@username", username),
                    DatabaseHelper.CreateParameter("@email", email)
                };
                object exists = DatabaseHelper.ExecuteScalar(checkQuery, checkParams);
                if (Convert.ToInt32(exists) > 0)
                {
                    MessageBox.Show("اسم المستخدم أو البريد الإلكتروني مستخدمان مسبقاً.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // إدخال المستخدم الجديد في قاعدة البيانات
                string insertQuery = "INSERT INTO users (username, email, password, role) VALUES (@username, @email, @password, @role)";
                var insertParams = new List<MySqlParameter>
                {
                    DatabaseHelper.CreateParameter("@username", username),
                    DatabaseHelper.CreateParameter("@email", email),
                    DatabaseHelper.CreateParameter("@password", password),
                    DatabaseHelper.CreateParameter("@role", role)
                };

                int rowsAffected = DatabaseHelper.ExecuteNonQuery(insertQuery, insertParams);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("تم إنشاء الحساب بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // إغلاق نافذة التسجيل والعودة لواجهة تسجيل الدخول
                }
                else
                {
                    MessageBox.Show("حدث خطأ أثناء إنشاء الحساب.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // اغلاق نموذج التسجيل والعودة لنموذج تسجيل الدخول
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // دالة بسيطة للتحقق من صحة البريد الإلكتروني
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
