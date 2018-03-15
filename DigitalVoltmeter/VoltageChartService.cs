using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace DigitalVoltmeter
{
    public static class VoltageChartService
    {
        /// <summary>
        /// Строит ступенчатый график последовательных значений напряжений
        /// </summary>
        /// <param name="chart">график для рисования</param>
        /// <param name="valtages">массив напряжений</param>
        /// <param name="color">цвет графика</param>
        /// <param name="width">ширина линии</param>
        public static void DrawInputVoltageList(Chart chart, double[] valtages, Color color, int width)
        {
            Series valtageSeries = new Series("valtages");
            valtageSeries.ChartType = SeriesChartType.StepLine;
            valtageSeries.Color = color;
            valtageSeries.BorderWidth = width;
            valtageSeries.Points.AddXY(0, 0);
            for (int x = 0; x < valtages.Length; x++)
                valtageSeries.Points.AddXY(x, valtages[x]);

            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.Title = "X";
            chart.ChartAreas[0].AxisY.Title = "Uвх";
            chart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            Series series = chart.Series.FindByName("valtages");
            if (series != null)
                chart.Series.Remove(series);
            chart.Series.Add(valtageSeries);
        }
    }
}
