using EasyBookPrinter.Core;
using System.Diagnostics;

namespace EasyBookPrinter
{
    public partial class MainForm : Form
    {
        private readonly IBookPrintManager _bookPrintManager;

        public MainForm()
        {
            InitializeComponent();

            _bookPrintManager = new BookPrintManager();
            _bookPrintManager.WorkStatusChanged += BookPrintManager_InfoStatusChanged;
        }

        private void BookPrintManager_InfoStatusChanged(object? sender, string message)
        {
            infoTxtBx.Text += message;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new(Color.FromArgb(102, 102, 102), 1);

            int y1 = orderPagesBtn.Location.Y - 6,
                y2 = orderPagesBtn.Location.Y + orderPagesBtn.Height + 6,
                y3 = infoTxtBx.Location.Y + infoTxtBx.Height + 8;

            for (int i = 12; i <= ClientSize.Width - 12; i += 3)
            {
                e.Graphics.DrawLine(pen, i, y1, i + 3, y1);
                e.Graphics.DrawLine(pen, i, y2, i + 3, y2);
                e.Graphics.DrawLine(pen, i, y3, i + 3, y3);

                i += 3;
            }
        }

        private void OrderPagesBtn_Click(object sender, EventArgs e)
        {
            infoTxtBx.Clear();

            string bookLoction = bookLocationTxtBx.Text;

            if (string.IsNullOrEmpty(bookLoction) || !File.Exists(bookLoction))
            {
                MessageBox.Show("Set book location.", "Something going wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _bookPrintManager.OpenBook(bookLoction);
                _bookPrintManager.SortPages((int)paperSheetCountNumUpDown.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            printBtn.Enabled = true;
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (!_bookPrintManager.IsPagesSorted)
            {
                MessageBox.Show("Book is not sorted yet.", "Something going wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _bookPrintManager.PrintBook(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectBookBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "(*.pdf)|*.pdf" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bookLocationTxtBx.Text = openFileDialog.FileName;
                }
            }
        }

        private void MainForm_HelpRequested(object sender, EventArgs hlpevent)
        {
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = "https://github.com/4emploxmouhik/Easy-Book-Printer/blob/master/README.md";
            myProcess.Start();
        }
    }
}
