namespace Smart_Budget
{
    partial class MainMenu
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
            IconOfApplication = new PictureBox();
            ImageOfCat = new PictureBox();
            ButtonExit = new Button();
            ButtonSettings = new Button();
            ButtonFirstTime = new Button();
            ButtonContinueWork = new Button();
            LabelOfApp = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            btnStartNewWork = new Button();
            ((System.ComponentModel.ISupportInitialize)IconOfApplication).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ImageOfCat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // IconOfApplication
            // 
            IconOfApplication.Image = SmartBudget.Properties.Resources.pictureCatHelper_Photoroom;
            IconOfApplication.Location = new Point(0, -66);
            IconOfApplication.Name = "IconOfApplication";
            IconOfApplication.Size = new Size(536, 787);
            IconOfApplication.SizeMode = PictureBoxSizeMode.StretchImage;
            IconOfApplication.TabIndex = 28;
            IconOfApplication.TabStop = false;
            // 
            // ImageOfCat
            // 
            ImageOfCat.Image = SmartBudget.Properties.Resources.picturePlayButton;
            ImageOfCat.Location = new Point(637, 147);
            ImageOfCat.Name = "ImageOfCat";
            ImageOfCat.Size = new Size(51, 50);
            ImageOfCat.TabIndex = 29;
            ImageOfCat.TabStop = false;
            // 
            // ButtonExit
            // 
            ButtonExit.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ButtonExit.Font = new Font("Times New Roman", 22.2F);
            ButtonExit.ImeMode = ImeMode.NoControl;
            ButtonExit.Location = new Point(694, 578);
            ButtonExit.Name = "ButtonExit";
            ButtonExit.Size = new Size(357, 71);
            ButtonExit.TabIndex = 14;
            ButtonExit.Text = "Выход";
            ButtonExit.TextAlign = ContentAlignment.MiddleRight;
            ButtonExit.UseVisualStyleBackColor = true;
            ButtonExit.Click += ButtonExit_Click;
            // 
            // ButtonSettings
            // 
            ButtonSettings.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ButtonSettings.Font = new Font("Times New Roman", 22.2F);
            ButtonSettings.ImeMode = ImeMode.NoControl;
            ButtonSettings.Location = new Point(694, 464);
            ButtonSettings.Name = "ButtonSettings";
            ButtonSettings.Size = new Size(357, 71);
            ButtonSettings.TabIndex = 13;
            ButtonSettings.Text = "Настройки";
            ButtonSettings.TextAlign = ContentAlignment.MiddleRight;
            ButtonSettings.UseVisualStyleBackColor = true;
            ButtonSettings.Click += ButtonSettings_Click;
            // 
            // ButtonFirstTime
            // 
            ButtonFirstTime.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ButtonFirstTime.Font = new Font("Times New Roman", 22.2F);
            ButtonFirstTime.ImeMode = ImeMode.NoControl;
            ButtonFirstTime.Location = new Point(694, 350);
            ButtonFirstTime.Name = "ButtonFirstTime";
            ButtonFirstTime.Size = new Size(357, 71);
            ButtonFirstTime.TabIndex = 12;
            ButtonFirstTime.Text = "О приложении";
            ButtonFirstTime.TextAlign = ContentAlignment.MiddleRight;
            ButtonFirstTime.UseVisualStyleBackColor = true;
            ButtonFirstTime.Click += ButtonFirstTime_Click;
            // 
            // ButtonContinueWork
            // 
            ButtonContinueWork.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ButtonContinueWork.Font = new Font("Times New Roman", 22.2F);
            ButtonContinueWork.ImeMode = ImeMode.NoControl;
            ButtonContinueWork.Location = new Point(694, 138);
            ButtonContinueWork.Name = "ButtonContinueWork";
            ButtonContinueWork.Size = new Size(357, 71);
            ButtonContinueWork.TabIndex = 11;
            ButtonContinueWork.Text = "Продолжить работу";
            ButtonContinueWork.TextAlign = ContentAlignment.MiddleRight;
            ButtonContinueWork.UseVisualStyleBackColor = true;
            ButtonContinueWork.Click += ButtonContinueWork_Click;
            // 
            // LabelOfApp
            // 
            LabelOfApp.AutoSize = true;
            LabelOfApp.Font = new Font("Times New Roman", 48F, FontStyle.Bold);
            LabelOfApp.ImeMode = ImeMode.NoControl;
            LabelOfApp.Location = new Point(561, -8);
            LabelOfApp.Name = "LabelOfApp";
            LabelOfApp.Size = new Size(514, 90);
            LabelOfApp.TabIndex = 9;
            LabelOfApp.Text = "Smart Budget";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = SmartBudget.Properties.Resources.pictureCross;
            pictureBox1.Location = new Point(637, 588);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(51, 50);
            pictureBox1.TabIndex = 27;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = SmartBudget.Properties.Resources.pictureCog;
            pictureBox2.Location = new Point(637, 473);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(51, 50);
            pictureBox2.TabIndex = 26;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = SmartBudget.Properties.Resources.picturePursue;
            pictureBox3.Location = new Point(499, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(77, 75);
            pictureBox3.TabIndex = 25;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = SmartBudget.Properties.Resources.pictureQuestionMark;
            pictureBox4.Location = new Point(637, 360);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(51, 50);
            pictureBox4.TabIndex = 24;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = SmartBudget.Properties.Resources.picturePlus;
            pictureBox5.Location = new Point(637, 255);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(51, 50);
            pictureBox5.TabIndex = 23;
            pictureBox5.TabStop = false;
            // 
            // btnStartNewWork
            // 
            btnStartNewWork.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnStartNewWork.Font = new Font("Times New Roman", 22.2F);
            btnStartNewWork.ImeMode = ImeMode.NoControl;
            btnStartNewWork.Location = new Point(694, 246);
            btnStartNewWork.Name = "btnStartNewWork";
            btnStartNewWork.Size = new Size(357, 71);
            btnStartNewWork.TabIndex = 22;
            btnStartNewWork.Text = "Начать новую работу";
            btnStartNewWork.TextAlign = ContentAlignment.MiddleRight;
            btnStartNewWork.UseVisualStyleBackColor = true;
            btnStartNewWork.Click += btnStartNewWork_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnStartNewWork);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(IconOfApplication);
            Controls.Add(ImageOfCat);
            Controls.Add(ButtonExit);
            Controls.Add(ButtonSettings);
            Controls.Add(ButtonFirstTime);
            Controls.Add(ButtonContinueWork);
            Controls.Add(LabelOfApp);
            Name = "MainMenu";
            Size = new Size(1075, 712);
            ((System.ComponentModel.ISupportInitialize)IconOfApplication).EndInit();
            ((System.ComponentModel.ISupportInitialize)ImageOfCat).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox IconOfApplication;
        private PictureBox ImageOfCat;
        private Button ButtonExit;
        private Button ButtonSettings;
        private Button ButtonFirstTime;
        private Button ButtonContinueWork;
        private Label LabelOfApp;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private Button btnStartNewWork;
    }
}
