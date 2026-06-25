namespace SmartBudget
{
    partial class GetAnalys
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
            pbxOpenMenu = new PictureBox();
            lblMessage = new Label();
            pnlMain = new Panel();
            pnlChart = new Panel();
            dgvTable = new DataGridView();
            formsPlot = new ScottPlot.WinForms.FormsPlot();
            lblReportHeader = new Label();
            lblActionsHeader = new Label();
            lblChartTypesHeader = new Label();
            pnlReport = new Panel();
            rtbReport = new RichTextBox();
            btnSaveReport = new Button();
            btnBackToData = new Button();
            btnRadarDiagram = new Button();
            btnTable = new Button();
            btnGistogram = new Button();
            btnGraph = new Button();
            btnScatterPlot = new Button();
            btnCircleDiagram = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbxOpenMenu).BeginInit();
            pnlMain.SuspendLayout();
            pnlChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTable).BeginInit();
            pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pbxOpenMenu
            // 
            pbxOpenMenu.Image = Properties.Resources.pictureMenu;
            pbxOpenMenu.Location = new Point(934, 3);
            pbxOpenMenu.Name = "pbxOpenMenu";
            pbxOpenMenu.Size = new Size(98, 94);
            pbxOpenMenu.TabIndex = 13;
            pbxOpenMenu.TabStop = false;
            pbxOpenMenu.Click += pbxOpenMenu_Click;
            // 
            // lblMessage
            // 
            lblMessage.BackColor = SystemColors.Window;
            lblMessage.BorderStyle = BorderStyle.FixedSingle;
            lblMessage.FlatStyle = FlatStyle.Flat;
            lblMessage.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblMessage.Location = new Point(105, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(812, 111);
            lblMessage.TabIndex = 12;
            lblMessage.Text = "Анализ данных завершен! Мяу! Если захотите изменить данные об операциях, то нажмите на кнопку \"Назад к данным\", а когда закончите работать - не забудьте \"Сохранить отчет\", мур!\r\n";
            lblMessage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            pnlMain.AutoScroll = true;
            pnlMain.Controls.Add(pnlChart);
            pnlMain.Controls.Add(lblReportHeader);
            pnlMain.Controls.Add(lblActionsHeader);
            pnlMain.Controls.Add(lblChartTypesHeader);
            pnlMain.Controls.Add(pnlReport);
            pnlMain.Controls.Add(btnSaveReport);
            pnlMain.Controls.Add(btnBackToData);
            pnlMain.Controls.Add(btnRadarDiagram);
            pnlMain.Controls.Add(btnTable);
            pnlMain.Controls.Add(btnGistogram);
            pnlMain.Controls.Add(btnGraph);
            pnlMain.Controls.Add(btnScatterPlot);
            pnlMain.Controls.Add(btnCircleDiagram);
            pnlMain.Location = new Point(-91, 117);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1150, 595);
            pnlMain.TabIndex = 14;
            // 
            // pnlChart
            // 
            pnlChart.Controls.Add(dgvTable);
            pnlChart.Controls.Add(formsPlot);
            pnlChart.Location = new Point(664, 16);
            pnlChart.Name = "pnlChart";
            pnlChart.Size = new Size(673, 550);
            pnlChart.TabIndex = 19;
            // 
            // dgvTable
            // 
            dgvTable.AllowUserToAddRows = false;
            dgvTable.AllowUserToDeleteRows = false;
            dgvTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTable.Location = new Point(0, 3);
            dgvTable.Name = "dgvTable";
            dgvTable.ReadOnly = true;
            dgvTable.RowHeadersWidth = 51;
            dgvTable.Size = new Size(670, 547);
            dgvTable.TabIndex = 20;
            // 
            // formsPlot
            // 
            formsPlot.Location = new Point(0, 0);
            formsPlot.Name = "formsPlot";
            formsPlot.Size = new Size(673, 547);
            formsPlot.TabIndex = 20;
            // 
            // lblReportHeader
            // 
            lblReportHeader.BackColor = SystemColors.Window;
            lblReportHeader.BorderStyle = BorderStyle.FixedSingle;
            lblReportHeader.FlatStyle = FlatStyle.Flat;
            lblReportHeader.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblReportHeader.Location = new Point(100, 16);
            lblReportHeader.Name = "lblReportHeader";
            lblReportHeader.Size = new Size(536, 51);
            lblReportHeader.TabIndex = 18;
            lblReportHeader.Text = "ОТЧЁТ ПО АНАЛИЗУ ОПЕРАЦИЙ";
            lblReportHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblActionsHeader
            // 
            lblActionsHeader.BackColor = SystemColors.Window;
            lblActionsHeader.BorderStyle = BorderStyle.FixedSingle;
            lblActionsHeader.FlatStyle = FlatStyle.Flat;
            lblActionsHeader.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblActionsHeader.Location = new Point(1381, 414);
            lblActionsHeader.Name = "lblActionsHeader";
            lblActionsHeader.Size = new Size(236, 39);
            lblActionsHeader.TabIndex = 17;
            lblActionsHeader.Text = "ДЕЙСТВИЯ";
            lblActionsHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblChartTypesHeader
            // 
            lblChartTypesHeader.BackColor = SystemColors.Window;
            lblChartTypesHeader.BorderStyle = BorderStyle.FixedSingle;
            lblChartTypesHeader.FlatStyle = FlatStyle.Flat;
            lblChartTypesHeader.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblChartTypesHeader.Location = new Point(1344, 3);
            lblChartTypesHeader.Name = "lblChartTypesHeader";
            lblChartTypesHeader.Size = new Size(300, 39);
            lblChartTypesHeader.TabIndex = 15;
            lblChartTypesHeader.Text = "ТИПЫ ВИЗУАЛИЗАЦИИ";
            lblChartTypesHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlReport
            // 
            pnlReport.Controls.Add(rtbReport);
            pnlReport.Location = new Point(99, 70);
            pnlReport.Name = "pnlReport";
            pnlReport.Size = new Size(536, 496);
            pnlReport.TabIndex = 16;
            // 
            // rtbReport
            // 
            rtbReport.Dock = DockStyle.Fill;
            rtbReport.Location = new Point(0, 0);
            rtbReport.Name = "rtbReport";
            rtbReport.ReadOnly = true;
            rtbReport.Size = new Size(536, 496);
            rtbReport.TabIndex = 0;
            rtbReport.Text = "";
            // 
            // btnSaveReport
            // 
            btnSaveReport.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSaveReport.Location = new Point(1393, 524);
            btnSaveReport.Name = "btnSaveReport";
            btnSaveReport.Size = new Size(209, 47);
            btnSaveReport.TabIndex = 15;
            btnSaveReport.Text = "Сохранить отчет";
            btnSaveReport.UseVisualStyleBackColor = true;
            btnSaveReport.Click += btnSaveReport_Click;
            // 
            // btnBackToData
            // 
            btnBackToData.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnBackToData.Location = new Point(1393, 466);
            btnBackToData.Name = "btnBackToData";
            btnBackToData.Size = new Size(209, 47);
            btnBackToData.TabIndex = 14;
            btnBackToData.Text = "Назад к данным";
            btnBackToData.UseVisualStyleBackColor = true;
            btnBackToData.Click += btnBackToData_Click;
            // 
            // btnRadarDiagram
            // 
            btnRadarDiagram.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnRadarDiagram.Location = new Point(1398, 340);
            btnRadarDiagram.Name = "btnRadarDiagram";
            btnRadarDiagram.Size = new Size(198, 47);
            btnRadarDiagram.TabIndex = 10;
            btnRadarDiagram.Text = "Лепестковая";
            btnRadarDiagram.UseVisualStyleBackColor = true;
            btnRadarDiagram.Click += btnRadarDiagram_Click;
            // 
            // btnTable
            // 
            btnTable.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnTable.Location = new Point(1398, 59);
            btnTable.Name = "btnTable";
            btnTable.Size = new Size(204, 49);
            btnTable.TabIndex = 6;
            btnTable.Text = "Таблица";
            btnTable.UseVisualStyleBackColor = true;
            btnTable.Click += btnTable_Click;
            // 
            // btnGistogram
            // 
            btnGistogram.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnGistogram.Location = new Point(1398, 287);
            btnGistogram.Name = "btnGistogram";
            btnGistogram.Size = new Size(198, 47);
            btnGistogram.TabIndex = 11;
            btnGistogram.Text = "Гистограмма";
            btnGistogram.UseVisualStyleBackColor = true;
            btnGistogram.Click += btnGistogram_Click;
            // 
            // btnGraph
            // 
            btnGraph.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnGraph.Location = new Point(1398, 114);
            btnGraph.Name = "btnGraph";
            btnGraph.Size = new Size(201, 49);
            btnGraph.TabIndex = 7;
            btnGraph.Text = "График";
            btnGraph.UseVisualStyleBackColor = true;
            btnGraph.Click += btnGraph_Click;
            // 
            // btnScatterPlot
            // 
            btnScatterPlot.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnScatterPlot.Location = new Point(1398, 228);
            btnScatterPlot.Name = "btnScatterPlot";
            btnScatterPlot.Size = new Size(198, 53);
            btnScatterPlot.TabIndex = 9;
            btnScatterPlot.Text = "Точечная";
            btnScatterPlot.UseVisualStyleBackColor = true;
            btnScatterPlot.Click += btnScatterPlot_Click;
            // 
            // btnCircleDiagram
            // 
            btnCircleDiagram.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCircleDiagram.Location = new Point(1398, 169);
            btnCircleDiagram.Name = "btnCircleDiagram";
            btnCircleDiagram.Size = new Size(201, 53);
            btnCircleDiagram.TabIndex = 8;
            btnCircleDiagram.Text = "Круг. диаграмма";
            btnCircleDiagram.UseVisualStyleBackColor = true;
            btnCircleDiagram.Click += btnCircleDiagram_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.Window;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = Properties.Resources.pictureCatHelperSmaller;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(108, 111);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // GetAnalys
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBox1);
            Controls.Add(pnlMain);
            Controls.Add(pbxOpenMenu);
            Controls.Add(lblMessage);
            Name = "GetAnalys";
            Size = new Size(1062, 712);
            ((System.ComponentModel.ISupportInitialize)pbxOpenMenu).EndInit();
            pnlMain.ResumeLayout(false);
            pnlChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTable).EndInit();
            pnlReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        // ==================== ПОЛЯ ====================

        private PictureBox pbxOpenMenu;
        private Label lblMessage;
        private Panel pnlMain;
        private Label lblReportHeader;
        private Label lblActionsHeader;
        private Label lblChartTypesHeader;
        private Panel pnlReport;
        private RichTextBox rtbReport;
        private Button btnSaveReport;
        private Button btnBackToData;
        private Button btnRadarDiagram;
        public Button btnTable;
        private Button btnGistogram;
        private Button btnGraph;
        private Button btnScatterPlot;
        private Button btnCircleDiagram;
        private Panel pnlChart;
        private ScottPlot.WinForms.FormsPlot formsPlot;
        private PictureBox pictureBox1;
        private DataGridView dgvTable;
    }
}