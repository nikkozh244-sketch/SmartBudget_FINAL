namespace Smart_Budget
{
    partial class StartNewWork
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblMessage = new Label();
            dgvOperations = new DataGridView();
            pnlInput = new Panel();
            btnDelete = new Button();
            btnDone = new Button();
            btnChange = new Button();
            btnAdd = new Button();
            dtpDate = new DateTimePicker();
            lblDate = new Label();
            cboCurrency = new ComboBox();
            lblCurrency = new Label();
            cboCategory = new ComboBox();
            lblCategory = new Label();
            cboType = new ComboBox();
            lblType = new Label();
            lblAmount = new Label();
            numAmount = new NumericUpDown();
            PictureCat = new PictureBox();
            IconOpenMenu = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvOperations).BeginInit();
            pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureCat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IconOpenMenu).BeginInit();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.BackColor = SystemColors.Window;
            lblMessage.BorderStyle = BorderStyle.FixedSingle;
            lblMessage.FlatStyle = FlatStyle.Flat;
            lblMessage.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblMessage.ImageAlign = ContentAlignment.MiddleRight;
            lblMessage.Location = new Point(106, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(812, 114);
            lblMessage.TabIndex = 15;
            lblMessage.Text = "Мяу! Для начала работы введите данные об операциях, и они будут записаны в таблицу!";
            lblMessage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvOperations
            // 
            dgvOperations.AllowUserToOrderColumns = true;
            dgvOperations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOperations.Location = new Point(12, 131);
            dgvOperations.Name = "dgvOperations";
            dgvOperations.RowHeadersWidth = 51;
            dgvOperations.Size = new Size(678, 566);
            dgvOperations.TabIndex = 17;
            // 
            // pnlInput
            // 
            pnlInput.AutoScroll = true;
            pnlInput.BackColor = Color.Transparent;
            pnlInput.Controls.Add(btnDelete);
            pnlInput.Controls.Add(btnDone);
            pnlInput.Controls.Add(btnChange);
            pnlInput.Controls.Add(btnAdd);
            pnlInput.Controls.Add(dtpDate);
            pnlInput.Controls.Add(lblDate);
            pnlInput.Controls.Add(cboCurrency);
            pnlInput.Controls.Add(lblCurrency);
            pnlInput.Controls.Add(cboCategory);
            pnlInput.Controls.Add(lblCategory);
            pnlInput.Controls.Add(cboType);
            pnlInput.Controls.Add(lblType);
            pnlInput.Controls.Add(lblAmount);
            pnlInput.Controls.Add(numAmount);
            pnlInput.Location = new Point(711, 114);
            pnlInput.Name = "pnlInput";
            pnlInput.Size = new Size(345, 598);
            pnlInput.TabIndex = 18;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnDelete.Location = new Point(193, 518);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(144, 65);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnDone
            // 
            btnDone.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnDone.Location = new Point(17, 518);
            btnDone.Name = "btnDone";
            btnDone.Size = new Size(144, 65);
            btnDone.TabIndex = 13;
            btnDone.Text = "Готово";
            btnDone.UseVisualStyleBackColor = true;
            btnDone.Click += btnDone_Click;
            // 
            // btnChange
            // 
            btnChange.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnChange.Location = new Point(193, 447);
            btnChange.Name = "btnChange";
            btnChange.Size = new Size(144, 65);
            btnChange.TabIndex = 12;
            btnChange.Text = "Изменить";
            btnChange.UseVisualStyleBackColor = true;
            btnChange.Click += btnChange_Click;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnAdd.Location = new Point(17, 447);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(144, 65);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // dtpDate
            // 
            dtpDate.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            dtpDate.Location = new Point(17, 396);
            dtpDate.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(320, 34);
            dtpDate.TabIndex = 10;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblDate.Location = new Point(88, 353);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(201, 34);
            lblDate.TabIndex = 9;
            lblDate.Text = "Дата операции";
            // 
            // cboCurrency
            // 
            cboCurrency.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCurrency.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cboCurrency.FormattingEnabled = true;
            cboCurrency.Items.AddRange(new object[] { "Рубли", "Доллары" });
            cboCurrency.Location = new Point(16, 302);
            cboCurrency.Name = "cboCurrency";
            cboCurrency.Size = new Size(320, 34);
            cboCurrency.TabIndex = 8;
            // 
            // lblCurrency
            // 
            lblCurrency.AutoSize = true;
            lblCurrency.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblCurrency.Location = new Point(129, 265);
            lblCurrency.Name = "lblCurrency";
            lblCurrency.Size = new Size(111, 34);
            lblCurrency.TabIndex = 7;
            lblCurrency.Text = "Валюта";
            // 
            // cboCategory
            // 
            cboCategory.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cboCategory.FormattingEnabled = true;
            cboCategory.Items.AddRange(new object[] { "Продукты", "Кафе", "Транспорт", "Доставка", "Одежда", "Электротехника" });
            cboCategory.Location = new Point(17, 214);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(320, 34);
            cboCategory.TabIndex = 6;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.CausesValidation = false;
            lblCategory.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblCategory.Location = new Point(46, 177);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(272, 34);
            lblCategory.TabIndex = 5;
            lblCategory.Text = "Категория операции";
            // 
            // cboType
            // 
            cboType.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cboType.FormattingEnabled = true;
            cboType.Items.AddRange(new object[] { "Зачисление", "Перевод", "Снятие", "Списание" });
            cboType.Location = new Point(17, 126);
            cboType.Name = "cboType";
            cboType.Size = new Size(320, 34);
            cboType.TabIndex = 4;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblType.Location = new Point(88, 89);
            lblType.Name = "lblType";
            lblType.Size = new Size(193, 34);
            lblType.TabIndex = 3;
            lblType.Text = "Тип операции";
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.BackColor = Color.Transparent;
            lblAmount.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblAmount.Location = new Point(64, 0);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(232, 34);
            lblAmount.TabIndex = 1;
            lblAmount.Text = "Размер операции";
            // 
            // numAmount
            // 
            numAmount.DecimalPlaces = 2;
            numAmount.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numAmount.Location = new Point(85, 37);
            numAmount.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numAmount.Minimum = new decimal(new int[] { 1000000000, 0, 0, int.MinValue });
            numAmount.Name = "numAmount";
            numAmount.Size = new Size(196, 34);
            numAmount.TabIndex = 0;
            numAmount.Value = new decimal(new int[] { 100010, 0, 0, 131072 });
            // 
            // PictureCat
            // 
            PictureCat.BackColor = SystemColors.Window;
            PictureCat.BorderStyle = BorderStyle.FixedSingle;
            PictureCat.Image = SmartBudget.Properties.Resources.pictureCatHelperSmall_Photoroom;
            PictureCat.Location = new Point(0, -8);
            PictureCat.Name = "PictureCat";
            PictureCat.Size = new Size(112, 122);
            PictureCat.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureCat.TabIndex = 17;
            PictureCat.TabStop = false;
            // 
            // IconOpenMenu
            // 
            IconOpenMenu.Image = SmartBudget.Properties.Resources.pictureMenu;
            IconOpenMenu.Location = new Point(934, 3);
            IconOpenMenu.Name = "IconOpenMenu";
            IconOpenMenu.Size = new Size(98, 94);
            IconOpenMenu.TabIndex = 16;
            IconOpenMenu.TabStop = false;
            IconOpenMenu.Click += IconOpenMenu_Click;
            // 
            // StartNewWork
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(PictureCat);
            Controls.Add(IconOpenMenu);
            Controls.Add(pnlInput);
            Controls.Add(dgvOperations);
            Controls.Add(lblMessage);
            Name = "StartNewWork";
            Size = new Size(1075, 712);
            ((System.ComponentModel.ISupportInitialize)dgvOperations).EndInit();
            pnlInput.ResumeLayout(false);
            pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureCat).EndInit();
            ((System.ComponentModel.ISupportInitialize)IconOpenMenu).EndInit();
            ResumeLayout(false);
        }
        private Label lblMessage;
        private DataGridView dgvOperations;
        private Panel pnlInput;
        private Button btnDelete;
        private Button btnDone;
        private Button btnChange;
        private Button btnAdd;
        private DateTimePicker dtpDate;
        private Label lblDate;
        private ComboBox cboCurrency;
        private Label lblCurrency;
        private ComboBox cboCategory;
        private Label lblCategory;
        private ComboBox cboType;
        private Label lblType;
        private Label lblAmount;
        private NumericUpDown numAmount;
        private PictureBox PictureCat;
        private PictureBox IconOpenMenu;
    }
}