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
    public partial class UsersControl : UserControl
    {
        int selectedUserId = -1;

        public UsersControl()
        {
            InitializeComponent();
        }

        private void UsersControl_Load(object sender, EventArgs e)
        {
            LoadUsers();
            cmbRole.Items.Clear();
            cmbRole.Items.AddRange(new string[] { "admin", "receptionist" });
            cmbRole.SelectedIndex = -1;
        }

        private void LoadUsers()
        {
            string query = "SELECT * FROM users";
            dataGridViewUsers.DataSource = DatabaseHelper.ExecuteSelectCommand(query);
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1;
            selectedUserId = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO users (username, email, password, role) VALUES (@username, @email, @password, @role)";
            var parameters = new List<MySqlParameter>
            {
                DatabaseHelper.CreateParameter("@username", txtUsername.Text),
                DatabaseHelper.CreateParameter("@email", txtEmail.Text),
                DatabaseHelper.CreateParameter("@password", txtPassword.Text),
                DatabaseHelper.CreateParameter("@role", cmbRole.Text)
            };

            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("تمت إضافة المستخدم بنجاح");
                LoadUsers();
                ClearFields();
            }
        }

        private void button3_Click(object sender, EventArgs e) // افترض هذا هو زر التحديث
        {
            if (selectedUserId == -1)
            {
                MessageBox.Show("يرجى تحديد مستخدم للتعديل");
                return;
            }

            string query = "UPDATE users SET username=@username, email=@email, password=@password, role=@role WHERE id=@id";
            var parameters = new List<MySqlParameter>
            {
                DatabaseHelper.CreateParameter("@username", txtUsername.Text),
                DatabaseHelper.CreateParameter("@email", txtEmail.Text),
                DatabaseHelper.CreateParameter("@password", txtPassword.Text),
                DatabaseHelper.CreateParameter("@role", cmbRole.Text),
                DatabaseHelper.CreateParameter("@id", selectedUserId)
            };

            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("تم تحديث المستخدم بنجاح");
                LoadUsers();
                ClearFields();
            }
        }

        private void button4_Click(object sender, EventArgs e) // افترض هذا هو زر الحذف
        {
            if (selectedUserId == -1)
            {
                MessageBox.Show("يرجى تحديد مستخدم للحذف");
                return;
            }

            string query = "DELETE FROM users WHERE id=@id";
            var parameters = new List<MySqlParameter>
            {
                DatabaseHelper.CreateParameter("@id", selectedUserId)
            };

            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("تم حذف المستخدم بنجاح");
                LoadUsers();
                ClearFields();
            }
        }

        private void button2_Click(object sender, EventArgs e) // افترض هذا هو زر مسح الحقول
        {
            ClearFields();
        }

        private void dataGridViewUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedUserId = Convert.ToInt32(dataGridViewUsers.Rows[e.RowIndex].Cells["id"].Value);
                txtUsername.Text = dataGridViewUsers.Rows[e.RowIndex].Cells["username"].Value.ToString();
                txtEmail.Text = dataGridViewUsers.Rows[e.RowIndex].Cells["email"].Value.ToString();
                txtPassword.Text = dataGridViewUsers.Rows[e.RowIndex].Cells["password"].Value.ToString();
                cmbRole.Text = dataGridViewUsers.Rows[e.RowIndex].Cells["role"].Value.ToString();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            string query = "SELECT * FROM users WHERE username LIKE @search OR email LIKE @search";
            var parameters = new List<MySqlParameter>
            {
                DatabaseHelper.CreateParameter("@search", "%" + keyword + "%")
            };

            dataGridViewUsers.DataSource = DatabaseHelper.ExecuteSelectCommand(query, parameters);
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
