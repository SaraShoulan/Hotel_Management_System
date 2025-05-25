using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Windows.Forms;

namespace Hotel
{
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();
            this.Load += DashboardControl_Load;
        }

        private void DashboardControl_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                string queryRooms = "SELECT COUNT(*) FROM rooms";
                string queryClients = "SELECT COUNT(*) FROM clients";
                string queryReservations = "SELECT COUNT(*) FROM reservations";
                string queryUsers = "SELECT COUNT(*) FROM users";

                object roomsCount = DatabaseHelper.ExecuteScalar(queryRooms, null);
                object clientsCount = DatabaseHelper.ExecuteScalar(queryClients, null);
                object reservationsCount = DatabaseHelper.ExecuteScalar(queryReservations, null);
                object usersCount = DatabaseHelper.ExecuteScalar(queryUsers, null);

                lblTotalRooms.Text = roomsCount != null ? roomsCount.ToString() : "0";
                lblTotalClients.Text = clientsCount != null ? clientsCount.ToString() : "0";
                lblTotalReservations.Text = reservationsCount != null ? reservationsCount.ToString() : "0";
                lblTotalUsers.Text = usersCount != null ? usersCount.ToString() : "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل بيانات لوحة التحكم: " + ex.Message);
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportDashboardReportToPDF();
        }

        private void ExportDashboardReportToPDF()
        {
            try
            {
                Document doc = new Document(PageSize.A4);
                string filePath = Path.Combine(Application.StartupPath, "DashboardReport.pdf");
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // إنشاء الخط مع ترميز Cp1256 الصحيح
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, "Cp1256", BaseFont.NOT_EMBEDDED);

                // استخدام iTextSharp.text.Font مع تحديد الحجم ونمط الخط
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(bf, 18f, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 14f, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font bodyFont = new iTextSharp.text.Font(bf, 12f, iTextSharp.text.Font.NORMAL);

                Paragraph title = new Paragraph("تقرير لوحة التحكم", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                doc.Add(title);

                PdfPTable table = new PdfPTable(2)
                {
                    RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                    WidthPercentage = 100
                };

                table.AddCell(new Phrase("العنصر", headerFont));
                table.AddCell(new Phrase("العدد", headerFont));

                table.AddCell(new Phrase("عدد الغرف", bodyFont));
                table.AddCell(new Phrase(lblTotalRooms.Text, bodyFont));

                table.AddCell(new Phrase("عدد العملاء", bodyFont));
                table.AddCell(new Phrase(lblTotalClients.Text, bodyFont));

                table.AddCell(new Phrase("عدد الحجوزات", bodyFont));
                table.AddCell(new Phrase(lblTotalReservations.Text, bodyFont));

                table.AddCell(new Phrase("عدد المستخدمين", bodyFont));
                table.AddCell(new Phrase(lblTotalUsers.Text, bodyFont));

                doc.Add(table);
                doc.Close();

                MessageBox.Show("تم إنشاء التقرير بنجاح:\n" + filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message);
            }
        }

        // الأحداث الفارغة مع الحفاظ على أسماء المعالجات كما هي (يمكن حذفها إذا لم تستخدم)
        private void label2_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void lblRoomsTitle_Click(object sender, EventArgs e) { }
        private void lblTotalRooms_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void lblClientsTitle_Click(object sender, EventArgs e) { }
        private void lblTotalClients_Click(object sender, EventArgs e) { }
        private void panel3_Paint(object sender, PaintEventArgs e) { }
        private void lblReservationsTitle_Click(object sender, EventArgs e) { }
        private void lblTotalReservations_Click(object sender, EventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }
        private void lblUsersTitle_Click(object sender, EventArgs e) { }
        private void lblTotalUsers_Click(object sender, EventArgs e) { }
    }
}
