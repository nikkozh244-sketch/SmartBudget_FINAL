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
            awmpStudyingVideo = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)PictureCat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IconOpenMenu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)awmpStudyingVideo).BeginInit();
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
            LabelAboutApp.Text = "Мур-р-р! Добро пожаловать в Smart Budget - приложение, которое поможет вам с работой с личными финансами! Для ознакомления с работой просмотрите видео или прочтите справочник пользователя ";
            LabelAboutApp.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PictureCat
            // 
            PictureCat.BackColor = SystemColors.Window;
            PictureCat.BorderStyle = BorderStyle.FixedSingle;
            PictureCat.Image = SmartBudget.Properties.Resources.pictureCatHelperSmall;
            PictureCat.Location = new Point(-1, 0);
            PictureCat.Name = "PictureCat";
            PictureCat.Size = new Size(109, 111);
            PictureCat.TabIndex = 8;
            PictureCat.TabStop = false;
            // 
            // IconOpenMenu
            // 
            IconOpenMenu.Image = SmartBudget.Properties.Resources.pictureMenu;
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
            // awmpStudyingVideo
            // 
            awmpStudyingVideo.Enabled = true;
            awmpStudyingVideo.Location = new Point(90, 134);
            awmpStudyingVideo.Name = "awmpStudyingVideo";
            awmpStudyingVideo.OcxState = (AxHost.State)resources.GetObject("awmpStudyingVideo.OcxState");
            awmpStudyingVideo.Size = new Size(885, 457);
            awmpStudyingVideo.TabIndex = 13;
            // 
            // AboutApplication
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(awmpStudyingVideo);
            Controls.Add(btnOpenChmFile);
            Controls.Add(IconOpenMenu);
            Controls.Add(LabelAboutApp);
            Controls.Add(PictureCat);
            Name = "AboutApplication";
            Size = new Size(1062, 712);
            ((System.ComponentModel.ISupportInitialize)PictureCat).EndInit();
            ((System.ComponentModel.ISupportInitialize)IconOpenMenu).EndInit();
            ((System.ComponentModel.ISupportInitialize)awmpStudyingVideo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label LabelAboutApp;
        private PictureBox PictureCat;
        private PictureBox IconOpenMenu;
        private Button btnOpenChmFile;
        private AxWMPLib.AxWindowsMediaPlayer awmpStudyingVideo;
    }
}
