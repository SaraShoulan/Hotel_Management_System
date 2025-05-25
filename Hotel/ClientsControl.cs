using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    public partial class ClientsControl : UserControl
    {
        public ClientsControl()
        {
            InitializeComponent();
            this.Load += ClientsControl_Load;

            txtSearch.TextChanged += txtSearch_TextChanged;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;
            dataGridViewClients.CellClick += dataGridViewClients_CellContentClick;
        }

        private void ClientsControl_Load(object sender, EventArgs e)
        {
            LoadClients();
            ClearInputs();
        }

        // تحميل العملاء (مع فلترة البحث)
        private void LoadClients(string filter = "")
        {
            string query = @"SELECT client_id AS ID, name AS 'Name', phone AS 'Phone', email AS 'Email' FROM clients";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query += " WHERE name LIKE @filter OR phone LIKE @filter OR email LIKE @filter";
                parameters.Add(DatabaseHelper.CreateParameter("@filter", "%" + filter + "%"));
            }

            DataTable dt = DatabaseHelper.ExecuteSelectCommand(query, parameters);
            dataGridViewClients.DataSource = dt;

            if (dataGridViewClients.Columns.Contains("ID"))
                dataGridViewClients.Columns["ID"].Visible = false;
        }

        // تحقق من صحة الإدخالات
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("يرجى إدخال اسم العميل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("يرجى إدخال رقم الهاتف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // يمكن إضافة تحقق للبريد الإلكتروني إذا أردت

            return true;
        }

        // مسح الحقول
        private void ClearInputs()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtSearch.Clear();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            // يمكن إضافة وظيفة إضافية إن أردت
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            // يمكن إضافة وظيفة إضافية إن أردت
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            // يمكن إضافة وظيفة إضافية إن أردت
        }

        // البحث أثناء الكتابة
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadClients(txtSearch.Text.Trim());
        }

        // اختيار صف من الجدول لعرض البيانات في الحقول
        private void dataGridViewClients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewClients.Rows[e.RowIndex];

            txtName.Text = row.Cells["Name"].Value.ToString();
            txtPhone.Text = row.Cells["Phone"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
        }

        // زر المسح
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        // زر الحذف
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.CurrentRow == null)
            {
                MessageBox.Show("يرجى اختيار عميل للحذف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int clientId = Convert.ToInt32(dataGridViewClients.CurrentRow.Cells["ID"].Value);

            var confirm = MessageBox.Show("هل أنت متأكد من حذف هذا العميل؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                string query = "DELETE FROM clients WHERE client_id=@client_id";
                var parameters = new List<MySqlParameter>()
                {
                    DatabaseHelper.CreateParameter("@client_id", clientId)
                };

                int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("تم حذف العميل بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadClients();
                }
                else
                {
                    MessageBox.Show("حدث خطأ أثناء حذف العميل.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // زر التحديث
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.CurrentRow == null)
            {
                MessageBox.Show("يرجى اختيار عميل لتحديثه.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            int clientId = Convert.ToInt32(dataGridViewClients.CurrentRow.Cells["ID"].Value);

            string query = @"UPDATE clients SET name=@name, phone=@phone, email=@email WHERE client_id=@client_id";

            var parameters = new List<MySqlParameter>()
            {
                DatabaseHelper.CreateParameter("@name", txtName.Text.Trim()),
                DatabaseHelper.CreateParameter("@phone", txtPhone.Text.Trim()),
                DatabaseHelper.CreateParameter("@email", txtEmail.Text.Trim()),
                DatabaseHelper.CreateParameter("@client_id", clientId)
            };

            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("تم تحديث بيانات العميل بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadClients();
            }
            else
            {
                MessageBox.Show("حدث خطأ أثناء تحديث بيانات العميل.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // زر الإضافة
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string query = @"INSERT INTO clients (name, phone, email) VALUES (@name, @phone, @email)";

            var parameters = new List<MySqlParameter>()
            {
                DatabaseHelper.CreateParameter("@name", txtName.Text.Trim()),
                DatabaseHelper.CreateParameter("@phone", txtPhone.Text.Trim()),
                DatabaseHelper.CreateParameter("@email", txtEmail.Text.Trim())
            };

            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("تم إضافة عميل جديد بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadClients();
            }
            else
            {
                MessageBox.Show("حدث خطأ أثناء إضافة العميل.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClientsControl_Load_1(object sender, EventArgs e)
        {

        }
    }
}
