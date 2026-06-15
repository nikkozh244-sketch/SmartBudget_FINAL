namespace Smart_Budget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetAnalys));
            this.pbxOpenMenu = new PictureBox();
            this.lblMessage = new Label();
            this.pbxCatHelper = new PictureBox();
            this.pnlMain = new Panel();
            this.lblReportHeader = new Label();
            this.lblActionsHeader = new Label();
            this.lblChartTypesHeader = new Label();
            this.pnlReport = new Panel();
            this.rtbReport = new RichTextBox();
            this.btnSaveReport = new Button();
            this.btnBackToData = new Button();
            this.btnRadarDiagram = new Button();
            this.btnTable = new Button();
            this.btnGistogram = new Button();
            this.btnGraph = new Button();
            this.btnScatterPlot = new Button();
            this.btnCircleDiagram = new Button();
            this.btnSetPredictionMode = new Button();
            this.pnlChart = new Panel();
            this.dgvTableData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOpenMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCatHelper)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlReport.SuspendLayout();
            this.pnlChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableData)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxOpenMenu
            // 
            this.pbxOpenMenu.Image = (Image)resources.GetObject("pbxOpenMenu.Image");
            this.pbxOpenMenu.Location = new Point(934, 3);
            this.pbxOpenMenu.Name = "pbxOpenMenu";
            this.pbxOpenMenu.Size = new Size(98, 94);
            this.pbxOpenMenu.TabIndex = 13;
            this.pbxOpenMenu.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = SystemColors.Window;
            this.lblMessage.BorderStyle = BorderStyle.FixedSingle;
            this.lblMessage.FlatStyle = FlatStyle.Flat;
            this.lblMessage.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            this.lblMessage.Location = new Point(105, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new Size(812, 111);
            this.lblMessage.TabIndex = 12;
            this.lblMessage.Text = "Анализ данных завершен! Мяу! Наведите курсор на функцию, чтобы узнать подробнее о ней.";
            this.lblMessage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pbxCatHelper
            // 
            this.pbxCatHelper.BackColor = SystemColors.Window;
            this.pbxCatHelper.BorderStyle = BorderStyle.FixedSingle;
            this.pbxCatHelper.Image = (Image)resources.GetObject("pbxCatHelper.Image");
            this.pbxCatHelper.Location = new Point(-1, 0);
            this.pbxCatHelper.Name = "pbxCatHelper";
            this.pbxCatHelper.Size = new Size(109, 111);
            this.pbxCatHelper.TabIndex = 11;
            this.pbxCatHelper.TabStop = false;
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.lblReportHeader);
            this.pnlMain.Controls.Add(this.lblActionsHeader);
            this.pnlMain.Controls.Add(this.lblChartTypesHeader);
            this.pnlMain.Controls.Add(this.pnlReport);
            this.pnlMain.Controls.Add(this.btnSaveReport);
            this.pnlMain.Controls.Add(this.btnBackToData);
            this.pnlMain.Controls.Add(this.btnRadarDiagram);
            this.pnlMain.Controls.Add(this.btnTable);
            this.pnlMain.Controls.Add(this.btnGistogram);
            this.pnlMain.Controls.Add(this.btnGraph);
            this.pnlMain.Controls.Add(this.btnScatterPlot);
            this.pnlMain.Controls.Add(this.btnCircleDiagram);
            this.pnlMain.Controls.Add(this.btnSetPredictionMode);
            this.pnlMain.Location = new Point(-91, 114);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new Size(1150, 595);
            this.pnlMain.TabIndex = 14;
            // 
            // lblReportHeader
            // 
            this.lblReportHeader.BackColor = SystemColors.Window;
            this.lblReportHeader.BorderStyle = BorderStyle.FixedSingle;
            this.lblReportHeader.FlatStyle = FlatStyle.Flat;
            this.lblReportHeader.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point, 204);
            this.lblReportHeader.Location = new Point(99, 32);
            this.lblReportHeader.Name = "lblReportHeader";
            this.lblReportHeader.Size = new Size(536, 51);
            this.lblReportHeader.TabIndex = 18;
            this.lblReportHeader.Text = "ОТЧЁТ ПО АНАЛИЗУ ОПЕРАЦИЙ";
            this.lblReportHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblActionsHeader
            // 
            this.lblActionsHeader.BackColor = SystemColors.Window;
            this.lblActionsHeader.BorderStyle = BorderStyle.FixedSingle;
            this.lblActionsHeader.FlatStyle = FlatStyle.Flat;
            this.lblActionsHeader.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point, 204);
            this.lblActionsHeader.Location = new Point(1344, 412);
            this.lblActionsHeader.Name = "lblActionsHeader";
            this.lblActionsHeader.Size = new Size(236, 39);
            this.lblActionsHeader.TabIndex = 17;
            this.lblActionsHeader.Text = "ДЕЙСТВИЯ";
            this.lblActionsHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblChartTypesHeader
            // 
            this.lblChartTypesHeader.BackColor = SystemColors.Window;
            this.lblChartTypesHeader.BorderStyle = BorderStyle.FixedSingle;
            this.lblChartTypesHeader.FlatStyle = FlatStyle.Flat;
            this.lblChartTypesHeader.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point, 204);
            this.lblChartTypesHeader.Location = new Point(1344, 3);
            this.lblChartTypesHeader.Name = "lblChartTypesHeader";
            this.lblChartTypesHeader.Size = new Size(300, 39);
            this.lblChartTypesHeader.TabIndex = 15;
            this.lblChartTypesHeader.Text = "ТИПЫ ВИЗУАЛИЗАЦИИ";
            this.lblChartTypesHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlReport
            // 
            this.pnlReport.Controls.Add(this.rtbReport);
            this.pnlReport.Location = new Point(99, 86);
            this.pnlReport.Name = "pnlReport";
            this.pnlReport.Size = new Size(536, 480);
            this.pnlReport.TabIndex = 16;
            // 
            // rtbReport
            // 
            this.rtbReport.Location = new Point(0, 0);
            this.rtbReport.Name = "rtbReport";
            this.rtbReport.ReadOnly = true;
            this.rtbReport.Size = new Size(537, 480);
            this.rtbReport.TabIndex = 0;
            this.rtbReport.Text = "";
            // 
            // btnSaveReport
            // 
            this.btnSaveReport.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            this.btnSaveReport.Location = new Point(1393, 524);
            this.btnSaveReport.Name = "btnSaveReport";
            this.btnSaveReport.Size = new Size(209, 47);
            this.btnSaveReport.TabIndex = 15;
            this.btnSaveReport.Text = "Сохранить отчет";
            this.btnSaveReport.UseVisualStyleBackColor = true;
            // 
            // btnBackToData
            // 
            this.btnBackToData.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            this.btnBackToData.Location = new Point(1393, 466);
            this.btnBackToData.Name = "btnBackToData";
            this.btnBackToData.Size = new Size(209, 47);
            this.btnBackToData.TabIndex = 14;
            this.btnBackToData.Text = "Назад к данным";
            this.btnBackToData.UseVisualStyleBackColor = true;
            // 
            // btnRadarDiagram
            // 
            this.btnRadarDiagram.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            this.btnRadarDiagram.Location = new Point(1398, 340);
            this.btnRadarDiagram.Name = "btnRadarDiagram";
            this.btnRadarDiagram.Size = new Size(198, 47);
            this.btnRadarDiagram.TabIndex = 10;
            this.btnRadarDiagram.Text = "Лепестковая";
            this.btnRadarDiagram.UseVisualStyleBackColor = true;
            // 
            // btnTable
            // 
            this.btnTable.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            this.btnTable.Location = new Point(1398, 59);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new Size(204, 49);
            this.btnTable.TabIndex = 6;
            this.btnTable.Text = "Таблица";
            this.btnTable.UseVisualStyleBackColor = true;
            // 
            // btnGistogram
            // 
            this.btnGistogram.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            this.btnGistogram.Location = new Point(1398, 287);
            this.btnGistogram.Name = "btnGistogram";
            this.btnGistogram.Size = new Size(198, 47);
            this.btnGistogram.TabIndex = 11;
            this.btnGistogram.Text = "Гистограмма";
            this.btnGistogram.UseVisualStyleBackColor = true;
            // 
            // btnGraph
            // 
            this.btnGraph.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            this.btnGraph.Location = new Point(1398, 114);
            this.btnGraph.Name = "btnGraph";
            this.btnGraph.Size = new Size(201, 49);
            this.btnGraph.TabIndex = 7;
            this.btnGraph.Text = "График";
            this.btnGraph.UseVisualStyleBackColor = true;
            // 
            // btnScatterPlot
            // 
            this.btnScatterPlot.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            this.btnScatterPlot.Location = new Point(1398, 228);
            this.btnScatterPlot.Name = "btnScatterPlot";
            this.btnScatterPlot.Size = new Size(198, 53);
            this.btnScatterPlot.TabIndex = 9;
            this.btnScatterPlot.Text = "Точечная";
            this.btnScatterPlot.UseVisualStyleBackColor = true;
            // 
            // btnCircleDiagram
            // 
            this.btnCircleDiagram.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            this.btnCircleDiagram.Location = new Point(1398, 169);
            this.btnCircleDiagram.Name = "btnCircleDiagram";
            this.btnCircleDiagram.Size = new Size(201, 53);
            this.btnCircleDiagram.TabIndex = 8;
            this.btnCircleDiagram.Text = "Круг. диаграмма";
            this.btnCircleDiagram.UseVisualStyleBackColor = true;
            // 
            // btnSetPredictionMode
            // 
            this.btnSetPredictionMode.Font = new Font("Times New Roman", 15F, FontStyle.Regular, GraphicsUnit.Point, 204);
            this.btnSetPredictionMode.Location = new Point(738, 522);
            this.btnSetPredictionMode.Name = "btnSetPredictionMode";
            this.btnSetPredictionMode.Size = new Size(551, 44);
            this.btnSetPredictionMode.TabIndex = 12;
            this.btnSetPredictionMode.Text = "Перейти в Режим предсказания";
            this.btnSetPredictionMode.UseVisualStyleBackColor = true;
            // 
            // pnlChart
            // 
            this.pnlChart.Location = new Point(668, 25);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new Size(673, 491);
            this.pnlChart.TabIndex = 19;
            // 
            // formsPlot
            // 
            // 
            // dgvTableData
            // 
            this.dgvTableData.AllowUserToAddRows = false;
            this.dgvTableData.AllowUserToDeleteRows = false;
            this.dgvTableData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableData.Location = new Point(668, 25);
            this.dgvTableData.Name = "dgvTableData";
            this.dgvTableData.ReadOnly = true;
            this.dgvTableData.RowHeadersVisible = false;
            this.dgvTableData.RowHeadersWidth = 51;
            this.dgvTableData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTableData.Size = new Size(673, 491);
            this.dgvTableData.TabIndex = 20;
            this.dgvTableData.MultiSelect = false;
            // 
            // GetAnalys
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.dgvTableData);
            this.Controls.Add(this.pnlChart);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pbxOpenMenu);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.pbxCatHelper);
            this.Name = "GetAnalys";
            this.Size = new Size(1062, 712);
            ((System.ComponentModel.ISupportInitialize)(this.pbxOpenMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCatHelper)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlReport.ResumeLayout(false);
            this.pnlChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableData)).EndInit();
            this.ResumeLayout(false);
        }

        // ==================== ПОЛЯ ====================

        private PictureBox pbxOpenMenu;
        private Label lblMessage;
        private PictureBox pbxCatHelper;
        private Panel pnlMain;
        private Label lblReportHeader;
        private Label lblActionsHeader;
        private Label lblChartTypesHeader;
        private Panel pnlReport;
        private RichTextBox rtbReport;
        private Button btnSaveReport;
        private Button btnBackToData;
        private Button btnRadarDiagram;
        private Button btnTable;
        private Button btnGistogram;
        private Button btnGraph;
        private Button btnScatterPlot;
        private Button btnCircleDiagram;
        private Button btnSetPredictionMode;
        private Panel pnlChart;
        private System.Windows.Forms.DataGridView dgvTableData;
    }
}