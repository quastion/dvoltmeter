using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace DigitalVoltmeter
{
    public class ChartService
    {
        private Chart chart;

        public ChartService(Chart chart)
        {
            this.chart = chart;
        }

        /// <summary>
        /// Строит ступенчатый график последовательных значений напряжений
        /// </summary>
        /// <param name="valtages">массив напряжений</param>
        /// <param name="color">цвет графика</param>
        /// <param name="width">ширина линии</param>
        public void drawInputVoltageList(double[] valtages, Color color, int width)
        {
            Series valtageSeries = new Series("valtages");
            valtageSeries.ChartType = SeriesChartType.StepLine;
            valtageSeries.Color = color;
            valtageSeries.BorderWidth = width;
            valtageSeries.Points.AddXY(0, 0);
            for (int i = 0; i < valtages.Length; i++)
                valtageSeries.Points.AddXY(i + 1, valtages[i]);

            this.chart.ChartAreas[0].AxisX.Minimum = 0;
            this.chart.ChartAreas[0].AxisX.Title = "N";
            this.chart.ChartAreas[0].AxisY.Title = "Uвх";
            this.chart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            Series series = chart.Series.FindByName("valtages");
            if (series != null)
                chart.Series.Remove(series);
            chart.Series.Add(valtageSeries);
        }
    }
}
