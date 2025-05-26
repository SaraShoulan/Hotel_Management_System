using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Hotel
{
    public partial class RoomsControl : UserControl
    {
        public RoomsControl()
        {
            InitializeComponent();
            this.Load += RoomsControl_Load;
            LoadRoomsData();
        }

        private void RoomsControl_Load(object sender, EventArgs e)
        {
            comboroomType.DropDownStyle = ComboBoxStyle.DropDownList;

            comboroomType.Items.Clear();

            comboroomType.Items.Add("مفردة");
            comboroomType.Items.Add("مزدوجة");
            comboroomType.Items.Add("جناح");

            comboStatus.Items.Add("محجوزة");
            comboStatus.Items.Add("متاح");

        }

        private void LoadRoomsData()
        {
            string query = "SELECT room_id AS 'Room ID', room_number AS 'Room Number', room_type AS 'Type', beds AS 'Beds', price AS 'Price', status AS 'Status' FROM rooms";
            DataTable dt = DatabaseHelper.ExecuteSelectCommand(query);
            dataGridViewRooms.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text) || comboroomType.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtBeds.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) || comboStatus.SelectedIndex == -1)
            {
                MessageBox.Show("يرجى ملء جميع الحقول", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO rooms (room_number, room_type, beds, price, status) VALUES (@room_number, @room_type, @beds, @price, @status)";
            var parameters = new List<MySql.Data.MySqlClient.MySqlParameter>()
            {
                DatabaseHelper.CreateParameter("@room_number", txtRoomNumber.Text),
                DatabaseHelper.CreateParameter("@room_type", comboroomType.SelectedItem.ToString()),
                DatabaseHelper.CreateParameter("@beds", int.Parse(txtBeds.Text)),
                DatabaseHelper.CreateParameter("@price", decimal.Parse(txtPrice.Text)),
                DatabaseHelper.CreateParameter("@status", comboStatus.SelectedItem.ToString())
            };

            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("تم إضافة الغرفة بنجاح");
                ClearInputs();
                LoadRoomsData();
            }
            else
            {
                MessageBox.Show("حدث خطأ أثناء الإضافة");
            }
        }

        private void dgvRooms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewRooms.ScrollBars = ScrollBars.Both;
            dataGridViewRooms.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewRooms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewRooms.Rows[e.RowIndex];
                txtRoomNumber.Text = row.Cells["Room Number"].Value.ToString();
                comboroomType.SelectedItem = row.Cells["Type"].Value.ToString();
                txtBeds.Text = row.Cells["Beds"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
                comboStatus.SelectedItem = row.Cells["Status"].Value.ToString();
            }
        }

        private void txtRoomNumber_TextChanged(object sender, EventArgs e) { }
        private void txtRoomType_TextChanged(object sender, EventArgs e) { }
        private void txtBeds_TextChanged(object sender, EventArgs e) { }
        private void txtPrice_TextChanged(object sender, EventArgs e) { }
        private void comboStatus_SelectedIndexChanged(object sender, EventArgs e) { }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewRooms.CurrentRow == null)
            {
                MessageBox.Show("يرجى اختيار غرفة لحذفها");
                return;
            }

            int roomId = Convert.ToInt32(dataGridViewRooms.CurrentRow.Cells["Room ID"].Value);
            var confirmResult = MessageBox.Show("هل أنت متأكد من حذف هذه الغرفة؟", "تأكيد الحذف", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                string query = "DELETE FROM rooms WHERE room_id=@room_id";
                var parameters = new List<MySql.Data.MySqlClient.MySqlParameter>()
                {
                    DatabaseHelper.CreateParameter("@room_id", roomId)
                };

                int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("تم حذف الغرفة");
                    ClearInputs();
                    LoadRoomsData();
                }
                else
                {
                    MessageBox.Show("حدث خطأ أثناء الحذف");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewRooms.CurrentRow == null)
            {
                MessageBox.Show("يرجى اختيار غرفة لتحديثها");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text) || comboroomType.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtBeds.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) || comboStatus.SelectedIndex == -1)
            {
                MessageBox.Show("يرجى ملء جميع الحقول", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roomId = Convert.ToInt32(dataGridViewRooms.CurrentRow.Cells["Room ID"].Value);
            string query = "UPDATE rooms SET room_number=@room_number, room_type=@room_type, beds=@beds, price=@price, status=@status WHERE room_id=@room_id";
            var parameters = new List<MySql.Data.MySqlClient.MySqlParameter>()
            {
                DatabaseHelper.CreateParameter("@room_number", txtRoomNumber.Text),
                DatabaseHelper.CreateParameter("@room_type", comboroomType.SelectedItem.ToString()),
                DatabaseHelper.CreateParameter("@beds", int.Parse(txtBeds.Text)),
                DatabaseHelper.CreateParameter("@price", decimal.Parse(txtPrice.Text)),
                DatabaseHelper.CreateParameter("@status", comboStatus.SelectedItem.ToString()),
                DatabaseHelper.CreateParameter("@room_id", roomId)
            };

            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("تم تحديث بيانات الغرفة");
                ClearInputs();
                LoadRoomsData();
            }
            else
            {
                MessageBox.Show("حدث خطأ أثناء التحديث");
            }
        }

        private void ClearInputs()
        {
            txtRoomNumber.Clear();
            comboroomType.SelectedIndex = -1;
            txtBeds.Clear();
            txtPrice.Clear();
            comboStatus.SelectedIndex = -1;
        }

        private void label5_Click(object sender, EventArgs e) { }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (dataGridViewRooms.Rows.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات لتصديرها", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "ملفات PDF|*.pdf", FileName = "RoomsReport.pdf" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
                        PdfWriter.GetInstance(pdfDoc, new FileStream(sfd.FileName, FileMode.Create));
                        pdfDoc.Open();

                        Paragraph title = new Paragraph("تقرير الغرف", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD));
                        title.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(title);
                        pdfDoc.Add(new Chunk("\n"));

                        PdfPTable pdfTable = new PdfPTable(dataGridViewRooms.Columns.Count);

                        foreach (DataGridViewColumn column in dataGridViewRooms.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                            pdfTable.AddCell(cell);
                        }

                        foreach (DataGridViewRow row in dataGridViewRooms.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value?.ToString() ?? "");
                                }
                            }
                        }

                        pdfDoc.Add(pdfTable);
                        pdfDoc.Close();

                        MessageBox.Show("تم تصدير التقرير بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء تصدير التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboroomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // فارغ حسب طلبك
        }

        private void comboroomType_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
