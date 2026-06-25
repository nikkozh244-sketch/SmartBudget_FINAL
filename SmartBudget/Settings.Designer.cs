namespace SmartBudget
{
    partial class Settings
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
            pnlMenu = new Panel();
            ButtonReturnToHome = new Button();
            ButtonSettings = new Button();
            ButtonFirstTime = new Button();
            ButtonContinueWork = new Button();
            ButtonStartNewWork = new Button();
            label6 = new Label();
            ButtonResetSettings = new Button();
            ButtonApplySettings = new Button();
            LabelSettings = new Label();
            panel1 = new Panel();
            CheckDogMode = new CheckBox();
            LabelDogModeDescription = new Label();
            LabelDogMode = new Label();
            IconDog = new PictureBox();
            CheckLeftHanded = new CheckBox();
            LabelLeftHandedDescription = new Label();
            LabelLeftHandedMode = new Label();
            IconMouse = new PictureBox();
            CheckDarkTheme = new CheckBox();
            LabelDarkOnOrOff = new Label();
            LabelDarkTheme = new Label();
            IconDarkTheme = new PictureBox();
            ComboBoxChooseLanguage = new ComboBox();
            LabelLanguageDescription = new Label();
            LabelChangeLanguage = new Label();
            IconTranslate = new PictureBox();
            NumericDollarChoose = new NumericUpDown();
            LabelDollarDescriprtion = new Label();
            LabelChangeDollar = new Label();
            IconDollar = new PictureBox();
            IconOpenMenu = new PictureBox();
            PictureCat = new PictureBox();
            pnlMenu.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)IconDog).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IconMouse).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IconDarkTheme).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IconTranslate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumericDollarChoose).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IconDollar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IconOpenMenu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureCat).BeginInit();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = SystemColors.Control;
            pnlMenu.BorderStyle = BorderStyle.FixedSingle;
            pnlMenu.Controls.Add(ButtonReturnToHome);
            pnlMenu.Controls.Add(ButtonSettings);
            pnlMenu.Controls.Add(ButtonFirstTime);
            pnlMenu.Controls.Add(ButtonContinueWork);
            pnlMenu.Controls.Add(ButtonStartNewWork);
            pnlMenu.Controls.Add(label6);
            pnlMenu.ForeColor = SystemColors.ControlDarkDark;
            pnlMenu.Location = new Point(668, 44);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(401, 583);
            pnlMenu.TabIndex = 0;
            pnlMenu.Visible = false;
            // 
            // ButtonReturnToHome
            // 
            ButtonReturnToHome.Anchor = AnchorStyles.None;
            ButtonReturnToHome.Font = new Font("Times New Roman", 18F);
            ButtonReturnToHome.ForeColor = SystemColors.ActiveCaptionText;
            ButtonReturnToHome.ImeMode = ImeMode.NoControl;
            ButtonReturnToHome.Location = new Point(18, 388);
            ButtonReturnToHome.Name = "ButtonReturnToHome";
            ButtonReturnToHome.Size = new Size(375, 43);
            ButtonReturnToHome.TabIndex = 18;
            ButtonReturnToHome.Text = "Вернуться в главное меню";
            ButtonReturnToHome.UseVisualStyleBackColor = true;
            // 
            // ButtonSettings
            // 
            ButtonSettings.Anchor = AnchorStyles.None;
            ButtonSettings.BackColor = SystemColors.AppWorkspace;
            ButtonSettings.Enabled = false;
            ButtonSettings.Font = new Font("Times New Roman", 18F);
            ButtonSettings.ForeColor = Color.DimGray;
            ButtonSettings.ImeMode = ImeMode.NoControl;
            ButtonSettings.Location = new Point(18, 315);
            ButtonSettings.Name = "ButtonSettings";
            ButtonSettings.Size = new Size(375, 43);
            ButtonSettings.TabIndex = 17;
            ButtonSettings.Text = "Настройки";
            ButtonSettings.UseVisualStyleBackColor = false;
            // 
            // ButtonFirstTime
            // 
            ButtonFirstTime.Anchor = AnchorStyles.None;
            ButtonFirstTime.Font = new Font("Times New Roman", 18F);
            ButtonFirstTime.ForeColor = SystemColors.ActiveCaptionText;
            ButtonFirstTime.ImeMode = ImeMode.NoControl;
            ButtonFirstTime.Location = new Point(18, 233);
            ButtonFirstTime.Name = "ButtonFirstTime";
            ButtonFirstTime.Size = new Size(375, 43);
            ButtonFirstTime.TabIndex = 16;
            ButtonFirstTime.Text = "О приложении";
            ButtonFirstTime.UseVisualStyleBackColor = true;
            // 
            // ButtonContinueWork
            // 
            ButtonContinueWork.Anchor = AnchorStyles.None;
            ButtonContinueWork.Font = new Font("Times New Roman", 18F);
            ButtonContinueWork.ForeColor = SystemColors.ActiveCaptionText;
            ButtonContinueWork.ImeMode = ImeMode.NoControl;
            ButtonContinueWork.Location = new Point(21, 88);
            ButtonContinueWork.Name = "ButtonContinueWork";
            ButtonContinueWork.Size = new Size(375, 43);
            ButtonContinueWork.TabIndex = 15;
            ButtonContinueWork.Text = "Продолжить работу";
            ButtonContinueWork.UseVisualStyleBackColor = true;
            // 
            // ButtonStartNewWork
            // 
            ButtonStartNewWork.Anchor = AnchorStyles.None;
            ButtonStartNewWork.Font = new Font("Times New Roman", 18F);
            ButtonStartNewWork.ForeColor = SystemColors.ActiveCaptionText;
            ButtonStartNewWork.ImageAlign = ContentAlignment.MiddleRight;
            ButtonStartNewWork.ImeMode = ImeMode.NoControl;
            ButtonStartNewWork.Location = new Point(18, 164);
            ButtonStartNewWork.Name = "ButtonStartNewWork";
            ButtonStartNewWork.Size = new Size(375, 43);
            ButtonStartNewWork.TabIndex = 14;
            ButtonStartNewWork.Text = "Начать новую работу";
            ButtonStartNewWork.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.Location = new Point(124, 11);
            label6.Name = "label6";
            label6.Size = new Size(119, 42);
            label6.TabIndex = 0;
            label6.Text = "Меню";
            // 
            // ButtonResetSettings
            // 
            ButtonResetSettings.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ButtonResetSettings.Location = new Point(623, 456);
            ButtonResetSettings.Name = "ButtonResetSettings";
            ButtonResetSettings.Size = new Size(350, 91);
            ButtonResetSettings.TabIndex = 10;
            ButtonResetSettings.Text = "Сбросить настройки";
            ButtonResetSettings.UseVisualStyleBackColor = true;
            ButtonResetSettings.Click += ButtonResetSettings_Click;
            // 
            // ButtonApplySettings
            // 
            ButtonApplySettings.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ButtonApplySettings.ForeColor = Color.Black;
            ButtonApplySettings.Location = new Point(106, 456);
            ButtonApplySettings.Name = "ButtonApplySettings";
            ButtonApplySettings.Size = new Size(350, 91);
            ButtonApplySettings.TabIndex = 9;
            ButtonApplySettings.Text = "Применить настройки";
            ButtonApplySettings.UseVisualStyleBackColor = true;
            ButtonApplySettings.Click += ButtonApplySettings_Click;
            // 
            // LabelSettings
            // 
            LabelSettings.BackColor = SystemColors.Window;
            LabelSettings.BorderStyle = BorderStyle.FixedSingle;
            LabelSettings.FlatStyle = FlatStyle.Flat;
            LabelSettings.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LabelSettings.ImageAlign = ContentAlignment.MiddleRight;
            LabelSettings.Location = new Point(106, 0);
            LabelSettings.Name = "LabelSettings";
            LabelSettings.Size = new Size(812, 114);
            LabelSettings.TabIndex = 7;
            LabelSettings.Text = "Добро пожаловать в меню настроек, мяу! Здесь вы можете настроить приложение специально под себя!\r\n\r\n";
            LabelSettings.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.Controls.Add(CheckDogMode);
            panel1.Controls.Add(LabelDogModeDescription);
            panel1.Controls.Add(LabelDogMode);
            panel1.Controls.Add(IconDog);
            panel1.Controls.Add(CheckLeftHanded);
            panel1.Controls.Add(LabelLeftHandedDescription);
            panel1.Controls.Add(LabelLeftHandedMode);
            panel1.Controls.Add(IconMouse);
            panel1.Controls.Add(CheckDarkTheme);
            panel1.Controls.Add(LabelDarkOnOrOff);
            panel1.Controls.Add(LabelDarkTheme);
            panel1.Controls.Add(IconDarkTheme);
            panel1.Controls.Add(ComboBoxChooseLanguage);
            panel1.Controls.Add(LabelLanguageDescription);
            panel1.Controls.Add(LabelChangeLanguage);
            panel1.Controls.Add(IconTranslate);
            panel1.Controls.Add(NumericDollarChoose);
            panel1.Controls.Add(LabelDollarDescriprtion);
            panel1.Controls.Add(LabelChangeDollar);
            panel1.Controls.Add(IconDollar);
            panel1.Controls.Add(ButtonResetSettings);
            panel1.Controls.Add(ButtonApplySettings);
            panel1.Location = new Point(0, 130);
            panel1.Name = "panel1";
            panel1.Size = new Size(1075, 582);
            panel1.TabIndex = 11;
            // 
            // CheckDogMode
            // 
            CheckDogMode.AutoSize = true;
            CheckDogMode.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            CheckDogMode.Location = new Point(935, 354);
            CheckDogMode.Name = "CheckDogMode";
            CheckDogMode.Size = new Size(127, 30);
            CheckDogMode.TabIndex = 30;
            CheckDogMode.Text = "Вкл/Выкл";
            CheckDogMode.UseVisualStyleBackColor = true;
            // 
            // LabelDogModeDescription
            // 
            LabelDogModeDescription.AutoSize = true;
            LabelDogModeDescription.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LabelDogModeDescription.Location = new Point(623, 353);
            LabelDogModeDescription.Name = "LabelDogModeDescription";
            LabelDogModeDescription.Size = new Size(311, 26);
            LabelDogModeDescription.TabIndex = 29;
            LabelDogModeDescription.Text = "Меняет помощника на собаку:";
            // 
            // LabelDogMode
            // 
            LabelDogMode.AutoSize = true;
            LabelDogMode.Font = new Font("Times New Roman", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            LabelDogMode.Location = new Point(623, 309);
            LabelDogMode.Name = "LabelDogMode";
            LabelDogMode.Size = new Size(353, 42);
            LabelDogMode.TabIndex = 28;
            LabelDogMode.Text = "\"Режим собачника\"";
            // 
            // IconDog
            // 
            IconDog.Image = SmartBudget.Properties.Resources.picturePaw;
            IconDog.Location = new Point(547, 309);
            IconDog.Name = "IconDog";
            IconDog.Size = new Size(70, 70);
            IconDog.TabIndex = 27;
            IconDog.TabStop = false;
            // 
            // CheckLeftHanded
            // 
            CheckLeftHanded.AutoSize = true;
            CheckLeftHanded.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            CheckLeftHanded.Location = new Point(899, 214);
            CheckLeftHanded.Name = "CheckLeftHanded";
            CheckLeftHanded.Size = new Size(127, 30);
            CheckLeftHanded.TabIndex = 26;
            CheckLeftHanded.Text = "Вкл/Выкл";
            CheckLeftHanded.UseVisualStyleBackColor = true;
            // 
            // LabelLeftHandedDescription
            // 
            LabelLeftHandedDescription.AutoSize = true;
            LabelLeftHandedDescription.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LabelLeftHandedDescription.Location = new Point(623, 213);
            LabelLeftHandedDescription.Name = "LabelLeftHandedDescription";
            LabelLeftHandedDescription.Size = new Size(270, 26);
            LabelLeftHandedDescription.TabIndex = 25;
            LabelLeftHandedDescription.Text = "Отзеркаливает интерфейс:";
            // 
            // LabelLeftHandedMode
            // 
            LabelLeftHandedMode.AutoSize = true;
            LabelLeftHandedMode.Font = new Font("Times New Roman", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            LabelLeftHandedMode.Location = new Point(623, 169);
            LabelLeftHandedMode.Name = "LabelLeftHandedMode";
            LabelLeftHandedMode.Size = new Size(290, 42);
            LabelLeftHandedMode.TabIndex = 24;
            LabelLeftHandedMode.Text = "\"Режим левши\"";
            // 
            // IconMouse
            // 
            IconMouse.Image = SmartBudget.Properties.Resources.pictureLefryMouse;
            IconMouse.Location = new Point(547, 169);
            IconMouse.Name = "IconMouse";
            IconMouse.Size = new Size(70, 70);
            IconMouse.TabIndex = 23;
            IconMouse.TabStop = false;
            // 
            // CheckDarkTheme
            // 
            CheckDarkTheme.AutoSize = true;
            CheckDarkTheme.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            CheckDarkTheme.Location = new Point(384, 352);
            CheckDarkTheme.Name = "CheckDarkTheme";
            CheckDarkTheme.Size = new Size(127, 30);
            CheckDarkTheme.TabIndex = 22;
            CheckDarkTheme.Text = "Вкл/Выкл";
            CheckDarkTheme.UseVisualStyleBackColor = true;
            // 
            // LabelDarkOnOrOff
            // 
            LabelDarkOnOrOff.AutoSize = true;
            LabelDarkOnOrOff.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LabelDarkOnOrOff.Location = new Point(90, 353);
            LabelDarkOnOrOff.Name = "LabelDarkOnOrOff";
            LabelDarkOnOrOff.Size = new Size(287, 26);
            LabelDarkOnOrOff.TabIndex = 21;
            LabelDarkOnOrOff.Text = "Тёмный фон, светлый текст:";
            // 
            // LabelDarkTheme
            // 
            LabelDarkTheme.AutoSize = true;
            LabelDarkTheme.Font = new Font("Times New Roman", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            LabelDarkTheme.Location = new Point(90, 309);
            LabelDarkTheme.Name = "LabelDarkTheme";
            LabelDarkTheme.Size = new Size(232, 42);
            LabelDarkTheme.TabIndex = 20;
            LabelDarkTheme.Text = "Тёмная тема";
            // 
            // IconDarkTheme
            // 
            IconDarkTheme.Image = SmartBudget.Properties.Resources.pictureDarkMode;
            IconDarkTheme.Location = new Point(14, 309);
            IconDarkTheme.Name = "IconDarkTheme";
            IconDarkTheme.Size = new Size(70, 70);
            IconDarkTheme.TabIndex = 19;
            IconDarkTheme.TabStop = false;
            // 
            // ComboBoxChooseLanguage
            // 
            ComboBoxChooseLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxChooseLanguage.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ComboBoxChooseLanguage.FormattingEnabled = true;
            ComboBoxChooseLanguage.Items.AddRange(new object[] { "Русский", "English" });
            ComboBoxChooseLanguage.Location = new Point(384, 210);
            ComboBoxChooseLanguage.Name = "ComboBoxChooseLanguage";
            ComboBoxChooseLanguage.Size = new Size(120, 34);
            ComboBoxChooseLanguage.TabIndex = 18;
            // 
            // LabelLanguageDescription
            // 
            LabelLanguageDescription.AutoSize = true;
            LabelLanguageDescription.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LabelLanguageDescription.Location = new Point(90, 213);
            LabelLanguageDescription.Name = "LabelLanguageDescription";
            LabelLanguageDescription.Size = new Size(288, 26);
            LabelLanguageDescription.TabIndex = 17;
            LabelLanguageDescription.Text = "Выберете язык приложения:";
            // 
            // LabelChangeLanguage
            // 
            LabelChangeLanguage.AutoSize = true;
            LabelChangeLanguage.Font = new Font("Times New Roman", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            LabelChangeLanguage.Location = new Point(90, 171);
            LabelChangeLanguage.Name = "LabelChangeLanguage";
            LabelChangeLanguage.Size = new Size(238, 42);
            LabelChangeLanguage.TabIndex = 16;
            LabelChangeLanguage.Text = "Смена языка";
            // 
            // IconTranslate
            // 
            IconTranslate.Image = SmartBudget.Properties.Resources.pictureTranslation;
            IconTranslate.Location = new Point(14, 169);
            IconTranslate.Name = "IconTranslate";
            IconTranslate.Size = new Size(70, 70);
            IconTranslate.TabIndex = 15;
            IconTranslate.TabStop = false;
            // 
            // NumericDollarChoose
            // 
            NumericDollarChoose.BackColor = Color.White;
            NumericDollarChoose.BorderStyle = BorderStyle.FixedSingle;
            NumericDollarChoose.DecimalPlaces = 2;
            NumericDollarChoose.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            NumericDollarChoose.ForeColor = SystemColors.InfoText;
            NumericDollarChoose.Location = new Point(728, 81);
            NumericDollarChoose.Maximum = new decimal(new int[] { 121, 0, 0, 0 });
            NumericDollarChoose.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            NumericDollarChoose.Name = "NumericDollarChoose";
            NumericDollarChoose.Size = new Size(75, 34);
            NumericDollarChoose.TabIndex = 14;
            NumericDollarChoose.TextAlign = HorizontalAlignment.Center;
            NumericDollarChoose.Value = new decimal(new int[] { 80, 0, 0, 0 });
            // 
            // LabelDollarDescriprtion
            // 
            LabelDollarDescriprtion.AutoSize = true;
            LabelDollarDescriprtion.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LabelDollarDescriprtion.Location = new Point(399, 83);
            LabelDollarDescriprtion.Name = "LabelDollarDescriprtion";
            LabelDollarDescriprtion.Size = new Size(323, 26);
            LabelDollarDescriprtion.TabIndex = 13;
            LabelDollarDescriprtion.Text = "Введите текущий курс доллара:";
            // 
            // LabelChangeDollar
            // 
            LabelChangeDollar.AutoSize = true;
            LabelChangeDollar.Font = new Font("Times New Roman", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            LabelChangeDollar.Location = new Point(399, 36);
            LabelChangeDollar.Name = "LabelChangeDollar";
            LabelChangeDollar.Size = new Size(376, 42);
            LabelChangeDollar.TabIndex = 12;
            LabelChangeDollar.Text = "Смена курса доллара";
            // 
            // IconDollar
            // 
            IconDollar.Image = SmartBudget.Properties.Resources.puctureDollar;
            IconDollar.Location = new Point(333, 39);
            IconDollar.Name = "IconDollar";
            IconDollar.Size = new Size(70, 70);
            IconDollar.TabIndex = 11;
            IconDollar.TabStop = false;
            // 
            // IconOpenMenu
            // 
            IconOpenMenu.Image = SmartBudget.Properties.Resources.pictureMenu;
            IconOpenMenu.Location = new Point(934, 3);
            IconOpenMenu.Name = "IconOpenMenu";
            IconOpenMenu.Size = new Size(98, 94);
            IconOpenMenu.TabIndex = 14;
            IconOpenMenu.TabStop = false;
            IconOpenMenu.Click += IconOpenMenu_Click;
            // 
            // PictureCat
            // 
            PictureCat.BackColor = SystemColors.Window;
            PictureCat.BorderStyle = BorderStyle.FixedSingle;
            PictureCat.Image = SmartBudget.Properties.Resources.pictureCatHelperSmaller;
            PictureCat.Location = new Point(0, 0);
            PictureCat.Name = "PictureCat";
            PictureCat.Size = new Size(108, 114);
            PictureCat.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureCat.TabIndex = 19;
            PictureCat.TabStop = false;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(PictureCat);
            Controls.Add(IconOpenMenu);
            Controls.Add(panel1);
            Controls.Add(LabelSettings);
            Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Name = "Settings";
            Size = new Size(1075, 712);
            pnlMenu.ResumeLayout(false);
            pnlMenu.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)IconDog).EndInit();
            ((System.ComponentModel.ISupportInitialize)IconMouse).EndInit();
            ((System.ComponentModel.ISupportInitialize)IconDarkTheme).EndInit();
            ((System.ComponentModel.ISupportInitialize)IconTranslate).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumericDollarChoose).EndInit();
            ((System.ComponentModel.ISupportInitialize)IconDollar).EndInit();
            ((System.ComponentModel.ISupportInitialize)IconOpenMenu).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureCat).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlMenu;
        private Button ButtonResetSettings;
        private Button ButtonApplySettings;
        private Label LabelSettings;
        private Panel panel1;
        private Label LabelChangeDollar;
        private PictureBox IconDollar;
        private Label LabelDollarDescriprtion;
        private NumericUpDown NumericDollarChoose;
        private PictureBox IconTranslate;
        private Label LabelChangeLanguage;
        private ComboBox ComboBoxChooseLanguage;
        private Label LabelLanguageDescription;
        private CheckBox CheckDarkTheme;
        private Label LabelDarkOnOrOff;
        private Label LabelDarkTheme;
        private PictureBox IconDarkTheme;
        private CheckBox CheckDogMode;
        private Label LabelDogModeDescription;
        private Label LabelDogMode;
        private PictureBox IconDog;
        private CheckBox CheckLeftHanded;
        private Label LabelLeftHandedDescription;
        private Label LabelLeftHandedMode;
        private PictureBox IconMouse;
        private Button ButtonReturnToHome;
        private Button ButtonSettings;
        private Button ButtonFirstTime;
        private Button ButtonContinueWork;
        private Button ButtonStartNewWork;
        private Label label6;
        private PictureBox IconOpenMenu;
        private PictureBox PictureCat;
    }
}
