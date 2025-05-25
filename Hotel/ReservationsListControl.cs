using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    public partial class ReservationsListControl : UserControl
    {
        public ReservationsListControl()
        {
            InitializeComponent();
            this.Load += ReservationsListControl_Load;

            txtClientName.TextChanged += txtClientName_TextChanged;
            txtRoomNumber.TextChanged += txtRoomNumber_TextChanged;
            txtGuests.TextChanged += txtGuests_TextChanged;
            comboStatus.SelectedIndexChanged += comboStatus_SelectedIndexChanged;
            dtpCheckIn.ValueChanged += dtpCheckIn_ValueChanged;
            dtpCheckOut.ValueChanged += dtpCheckOut_ValueChanged;

            btnClear.Click += btnClear_Click;
            btnDelete.Click += btnDelete_Click;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;

            dataGridViewReservations.CellClick += dataGridViewReservations_CellContentClick;
        }

        private void ReservationsListControl_Load(object sender, EventArgs e)
        {
             LoadStatusToComboBox();
            LoadReservationsData();
            ClearInputs();
        }

        private void LoadStatusToComboBox()
        {
            comboStatus.Items.Clear();
            comboStatus.Items.AddRange(new string[] { "تم التأكيد", "قيد الانتظار", "أُلغيت" });
        }

        private void LoadReservationsData()
        {
            string query = @"
                SELECT reservation_id AS ID, client_name AS 'Client Name', room_number AS 'Room Number',
                       check_in AS 'Check-in Date', check_out AS 'Check-out Date', 
                       guests AS 'Guests', status AS 'Status'
                FROM reservations";

            DataTable dt = DatabaseHelper.ExecuteSelectCommand(query);
            dataGridViewReservations.DataSource = dt;

            if (dataGridViewReservations.Columns.Contains("ID"))
                dataGridViewReservations.Columns["ID"].Visible = false;
        }

        private void ClearInputs()
        {
            txtClientName.Clear();
            txtRoomNumber.Clear();
            txtGuests.Clear();
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(1);
            comboStatus.SelectedIndex = -1;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtClientName.Text) ||
                string.IsNullOrWhiteSpace(txtRoomNumber.Text) ||
                string.IsNullOrWhiteSpace(txtGuests.Text) ||
                comboStatus.SelectedIndex == -1)
            {
                MessageBox.Show("يرجى ملء جميع الحقول المطلوبة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtGuests.Text, out int guests) || guests <= 0)
            {
                MessageBox.Show("الرجاء إدخال عدد صحيح موجب للضيوف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpCheckOut.Value.Date <= dtpCheckIn.Value.Date)
            {
                MessageBox.Show("تاريخ المغادرة يجب أن يكون بعد تاريخ الوصول", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // ====================== Events ======================

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewReservations.CurrentRow == null)
            {
                MessageBox.Show("يرجى اختيار حجز لحذفه", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int reservationId = Convert.ToInt32(dataGridViewReservations.CurrentRow.Cells["ID"].Value);

            var confirm = MessageBox.Show("هل أنت متأكد من حذف هذا الحجز؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                string query = "DELETE FROM reservations WHERE reservation_id=@reservation_id";
                var parameters = new List<MySqlParameter>
                {
                    DatabaseHelper.CreateParameter("@reservation_id", reservationId)
                };

                int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("تم حذف الحجز بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadReservationsData();
                }
                else
                {
                    MessageBox.Show("حدث خطأ أثناء حذف الحجز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string checkQuery = @"
                SELECT COUNT(*) FROM reservations 
                WHERE room_number = @room_number 
                AND (
                    (@check_in BETWEEN check_in AND check_out)
                    OR (@check_out BETWEEN check_in AND check_out)
                    OR (check_in BETWEEN @check_in AND @check_out)
                )";

            var checkParams = new List<MySqlParameter>
            {
                DatabaseHelper.CreateParameter("@room_number", txtRoomNumber.Text),
                DatabaseHelper.CreateParameter("@check_in", dtpCheckIn.Value.Date),
                DatabaseHelper.CreateParameter("@check_out", dtpCheckOut.Value.Date)
            };

            int existingCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkQuery, checkParams));
            if (existingCount > 0)
            {
                MessageBox.Show("⚠️ الغرفة محجوزة بالفعل خلال الفترة المحددة. الرجاء اختيار غرفة أو تاريخ مختلف.",
                                "تعارض في الحجز", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string insertQuery = @"
                INSERT INTO reservations 
                (client_name, room_number, check_in, check_out, guests, status) 
                VALUES (@client_name, @room_number, @check_in, @check_out, @guests, @status)";

            var insertParams = new List<MySqlParameter>
            {
                DatabaseHelper.CreateParameter("@client_name", txtClientName.Text),
                DatabaseHelper.CreateParameter("@room_number", txtRoomNumber.Text),
                DatabaseHelper.CreateParameter("@check_in", dtpCheckIn.Value.Date),
                DatabaseHelper.CreateParameter("@check_out", dtpCheckOut.Value.Date),
                DatabaseHelper.CreateParameter("@guests", int.Parse(txtGuests.Text)),
                DatabaseHelper.CreateParameter("@status", comboStatus.SelectedItem.ToString())
            };

            int result = DatabaseHelper.ExecuteNonQuery(insertQuery, insertParams);
            if (result > 0)
            {
                MessageBox.Show("✅ تم إضافة الحجز بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadReservationsData();
            }
            else
            {
                MessageBox.Show("❌ حدث خطأ أثناء إضافة الحجز.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewReservations.CurrentRow == null)
            {
                MessageBox.Show("يرجى اختيار حجز لتحديثه", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            int reservationId = Convert.ToInt32(dataGridViewReservations.CurrentRow.Cells["ID"].Value);

            string query = @"
                UPDATE reservations 
                SET client_name=@client_name, room_number=@room_number, check_in=@check_in, 
                    check_out=@check_out, guests=@guests, status=@status 
                WHERE reservation_id=@reservation_id";

            var parameters = new List<MySqlParameter>
            {
                DatabaseHelper.CreateParameter("@client_name", txtClientName.Text),
                DatabaseHelper.CreateParameter("@room_number", txtRoomNumber.Text),
                DatabaseHelper.CreateParameter("@check_in", dtpCheckIn.Value.Date),
                DatabaseHelper.CreateParameter("@check_out", dtpCheckOut.Value.Date),
                DatabaseHelper.CreateParameter("@guests", int.Parse(txtGuests.Text)),
                DatabaseHelper.CreateParameter("@status", comboStatus.SelectedItem.ToString()),
                DatabaseHelper.CreateParameter("@reservation_id", reservationId)
            };

            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("تم تحديث الحجز بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadReservationsData();
            }
            else
            {
                MessageBox.Show("حدث خطأ أثناء تحديث الحجز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewReservations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewReservations.Rows[e.RowIndex];

            txtClientName.Text = row.Cells["Client Name"].Value.ToString();
            txtRoomNumber.Text = row.Cells["Room Number"].Value.ToString();
            dtpCheckIn.Value = Convert.ToDateTime(row.Cells["Check-in Date"].Value);
            dtpCheckOut.Value = Convert.ToDateTime(row.Cells["Check-out Date"].Value);
            txtGuests.Text = row.Cells["Guests"].Value.ToString();
            comboStatus.SelectedItem = row.Cells["Status"].Value.ToString();
        }

        // =========== Empty Event Handlers (Optional Enhancements) ============

        private void txtClientName_TextChanged(object sender, EventArgs e) { }
        private void txtRoomNumber_TextChanged(object sender, EventArgs e) { }
        private void txtGuests_TextChanged(object sender, EventArgs e) { }
        private void comboStatus_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dtpCheckIn_ValueChanged(object sender, EventArgs e) { }
        private void dtpCheckOut_ValueChanged(object sender, EventArgs e) { }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
