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
        /// <param name="voltages">массив напряжений</param>
        /// <param name="color">цвет графика</param>
        /// <param name="width">ширина линии</param>
        public static void DrawInputVoltageList(Chart chart, string seriesName, double[] voltages, Color color, int width)
        {
            Series voltageSeries = new Series(seriesName);
            voltageSeries.ChartType = SeriesChartType.StepLine;
            voltageSeries.Color = color;
            voltageSeries.BorderWidth = width;
            voltageSeries.Points.AddXY(0, 0);
            for (int x = 0; x < voltages.Length; x++)
                voltageSeries.Points.AddXY(x, voltages[x]);

            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.Title = "X";
            chart.ChartAreas[0].AxisY.Title = "Uвх";
            chart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            Series series = chart.Series.FindByName(seriesName);
            if (series != null)
                chart.Series.Remove(series);
            chart.Series.Add(voltageSeries);
        }
    }
}
