namespace EasyBookPrinter
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PictureBox pictureBox;
            Label label2;
            Label label3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            titleLbl = new Label();
            bookLocationTxtBx = new TextBox();
            selectBookBtn = new Button();
            paperSheetCountNumUpDown = new NumericUpDown();
            orderPagesBtn = new Button();
            infoTxtBx = new TextBox();
            printBtn = new Button();
            pictureBox = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)paperSheetCountNumUpDown).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Image = Properties.Resources.book;
            pictureBox.Location = new Point(9, 10);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(26, 26);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 52);
            label2.Name = "label2";
            label2.Size = new Size(114, 15);
            label2.TabIndex = 2;
            label2.Text = "Select book to print:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 106);
            label3.Name = "label3";
            label3.Size = new Size(236, 15);
            label3.TabIndex = 5;
            label3.Text = "Set count of paper sheet for the one block:";
            // 
            // titleLbl
            // 
            titleLbl.AutoSize = true;
            titleLbl.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold | FontStyle.Italic);
            titleLbl.Location = new Point(32, 10);
            titleLbl.Name = "titleLbl";
            titleLbl.Size = new Size(233, 27);
            titleLbl.TabIndex = 1;
            titleLbl.Text = "Easy Book Printer v.1.0";
            // 
            // bookLocationTxtBx
            // 
            bookLocationTxtBx.Location = new Point(12, 70);
            bookLocationTxtBx.Name = "bookLocationTxtBx";
            bookLocationTxtBx.Size = new Size(216, 22);
            bookLocationTxtBx.TabIndex = 1;
            // 
            // selectBookBtn
            // 
            selectBookBtn.AutoSize = true;
            selectBookBtn.Location = new Point(234, 68);
            selectBookBtn.Name = "selectBookBtn";
            selectBookBtn.Size = new Size(68, 25);
            selectBookBtn.TabIndex = 2;
            selectBookBtn.Text = "Browse";
            selectBookBtn.UseVisualStyleBackColor = true;
            selectBookBtn.Click += SelectBookBtn_Click;
            // 
            // paperSheetCountNumUpDown
            // 
            paperSheetCountNumUpDown.Location = new Point(254, 104);
            paperSheetCountNumUpDown.Name = "paperSheetCountNumUpDown";
            paperSheetCountNumUpDown.Size = new Size(48, 22);
            paperSheetCountNumUpDown.TabIndex = 3;
            // 
            // orderPagesBtn
            // 
            orderPagesBtn.AutoSize = true;
            orderPagesBtn.Location = new Point(12, 145);
            orderPagesBtn.Name = "orderPagesBtn";
            orderPagesBtn.Size = new Size(290, 25);
            orderPagesBtn.TabIndex = 4;
            orderPagesBtn.Text = "Order pages";
            orderPagesBtn.UseVisualStyleBackColor = true;
            orderPagesBtn.Click += OrderPagesBtn_Click;
            // 
            // infoTxtBx
            // 
            infoTxtBx.Location = new Point(12, 185);
            infoTxtBx.Multiline = true;
            infoTxtBx.Name = "infoTxtBx";
            infoTxtBx.ReadOnly = true;
            infoTxtBx.ScrollBars = ScrollBars.Both;
            infoTxtBx.Size = new Size(290, 170);
            infoTxtBx.TabIndex = 8;
            infoTxtBx.TabStop = false;
            infoTxtBx.Text = "Hello, user! Let's get started.\r\n";
            infoTxtBx.WordWrap = false;
            // 
            // printBtn
            // 
            printBtn.AutoSize = true;
            printBtn.Enabled = false;
            printBtn.Location = new Point(12, 370);
            printBtn.Name = "printBtn";
            printBtn.Size = new Size(290, 25);
            printBtn.TabIndex = 7;
            printBtn.Text = "Print / Get printed version of book";
            printBtn.UseVisualStyleBackColor = true;
            printBtn.Click += PrintBtn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(314, 407);
            Controls.Add(printBtn);
            Controls.Add(infoTxtBx);
            Controls.Add(orderPagesBtn);
            Controls.Add(paperSheetCountNumUpDown);
            Controls.Add(label3);
            Controls.Add(selectBookBtn);
            Controls.Add(bookLocationTxtBx);
            Controls.Add(label2);
            Controls.Add(pictureBox);
            Controls.Add(titleLbl);
            Font = new Font("Helvetica", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            RightToLeft = RightToLeft.No;
            Text = "Easy Book Printer";
            HelpButtonClicked += MainForm_HelpRequested;
            HelpRequested += MainForm_HelpRequested;
            Paint += MainForm_Paint;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)paperSheetCountNumUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox bookLocationTxtBx;
        private Button selectBookBtn;
        private NumericUpDown paperSheetCountNumUpDown;
        private Button orderPagesBtn;
        private TextBox infoTxtBx;
        private Button printBtn;
        private Label titleLbl;
    }
}
