namespace Smart_Budget
{
    partial class AboutApplication
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutApplication));
            LabelAboutApp = new Label();
            PictureCat = new PictureBox();
            IconOpenMenu = new PictureBox();
            btnOpenChmFile = new Button();
            ((System.ComponentModel.ISupportInitialize)PictureCat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IconOpenMenu).BeginInit();
            SuspendLayout();
            // 
            // LabelAboutApp
            // 
            LabelAboutApp.BackColor = SystemColors.Window;
            LabelAboutApp.BorderStyle = BorderStyle.FixedSingle;
            LabelAboutApp.FlatStyle = FlatStyle.Flat;
            LabelAboutApp.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LabelAboutApp.ImageAlign = ContentAlignment.MiddleRight;
            LabelAboutApp.Location = new Point(105, 0);
            LabelAboutApp.Name = "LabelAboutApp";
            LabelAboutApp.Size = new Size(812, 111);
            LabelAboutApp.TabIndex = 9;
            LabelAboutApp.Text = resources.GetString("LabelAboutApp.Text");
            LabelAboutApp.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureCat
            // 
            PictureCat.BackColor = SystemColors.Window;
            PictureCat.BorderStyle = BorderStyle.FixedSingle;
            PictureCat.Image = (Image)resources.GetObject("PictureCat.Image");
            PictureCat.Location = new Point(-1, 0);
            PictureCat.Name = "PictureCat";
            PictureCat.Size = new Size(109, 111);
            PictureCat.TabIndex = 8;
            PictureCat.TabStop = false;
            // 
            // IconOpenMenu
            // 
            IconOpenMenu.Image = (Image)resources.GetObject("IconOpenMenu.Image");
            IconOpenMenu.Location = new Point(934, 3);
            IconOpenMenu.Name = "IconOpenMenu";
            IconOpenMenu.Size = new Size(98, 94);
            IconOpenMenu.TabIndex = 10;
            IconOpenMenu.TabStop = false;
            IconOpenMenu.Click += OpenMenuIcon_Click_1;
            // 
            // btnOpenChmFile
            // 
            btnOpenChmFile.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnOpenChmFile.Location = new Point(347, 597);
            btnOpenChmFile.Name = "btnOpenChmFile";
            btnOpenChmFile.Size = new Size(406, 95);
            btnOpenChmFile.TabIndex = 12;
            btnOpenChmFile.Text = "Открыть справочник пользователя";
            btnOpenChmFile.UseVisualStyleBackColor = true;
            btnOpenChmFile.Click += btnOpenChmFile_Click;
            // 
            // AboutApplication
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnOpenChmFile);
            Controls.Add(IconOpenMenu);
            Controls.Add(LabelAboutApp);
            Controls.Add(PictureCat);
            Name = "AboutApplication";
            Size = new Size(1062, 712);
            ((System.ComponentModel.ISupportInitialize)PictureCat).EndInit();
            ((System.ComponentModel.ISupportInitialize)IconOpenMenu).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label LabelAboutApp;
        private PictureBox PictureCat;
        private PictureBox IconOpenMenu;
        private Button btnOpenChmFile;
    }
}
