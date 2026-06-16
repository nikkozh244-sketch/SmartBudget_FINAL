using ScottPlot;
using Smart_Budget.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace Smart_Budget
{
    public partial class GetAnalys : UserControl
    {
        private List<ObjectOfAnalysis> _operationsData; // Поле для хранения данных

        // События
        public event EventHandler NavigateToChangeData;
        public event EventHandler NavigateToHome;


        /// <summary>
        /// Инициализация экрана 
        /// </summary>
        public GetAnalys()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Получение данных из предыдущего экрана
        /// </summary>
        public void LoadData(List<ObjectOfAnalysis> operations)
        {
            _operationsData = operations;
            // TODO: Здесь вы будете отображать данные
            // Например, показать сообщение, что данные загружены
        }

        private void pbxOpenMenu_Click(object sender, EventArgs e)
        {
            NavigateToHome?.Invoke(this, EventArgs.Empty);
        }

        private void btnBackToData_Click(object sender, EventArgs e)
        {
            NavigateToChangeData?.Invoke(this, EventArgs.Empty);
        }

        private void btnSaveReport_Click(object sender, EventArgs e)
        {

        }

        private void btnTable_Click(object sender, EventArgs e)
        {

        }

        private void btnGraph_Click(object sender, EventArgs e)
        {

        }

        private void btnCircleDiagram_Click(object sender, EventArgs e)
        {

        }

        private void btnScatterPlot_Click(object sender, EventArgs e)
        {

        }

        private void btnGistogram_Click(object sender, EventArgs e)
        {

        }

        private void btnRadarDiagram_Click(object sender, EventArgs e)
        {

        }
    }
}