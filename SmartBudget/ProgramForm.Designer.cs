namespace Smart_Budget
{
    partial class ProgramForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramForm));
            PanelContainer = new Panel();
            helpProvider1 = new HelpProvider();
            helpProvider1.HelpNamespace = Path.Combine(Application.StartupPath, "Справочная служба.chm");
            SuspendLayout();
            // 
            // PanelContainer
            // 
            PanelContainer.Dock = DockStyle.Fill;
            PanelContainer.Location = new Point(0, 0);
            PanelContainer.Name = "PanelContainer";
            PanelContainer.Size = new Size(1075, 712);
            PanelContainer.TabIndex = 0;
            // 
            // ProgramForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1075, 712);
            ControlBox = false;
            Controls.Add(PanelContainer);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            //Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "ProgramForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Smart Budget";
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelContainer;
        public HelpProvider helpProvider1;
    }
}
