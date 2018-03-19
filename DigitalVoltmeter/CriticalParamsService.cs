using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVoltmeter
{
    class CriticalParamsService
    {
        private ExcelTools excelTools;
        private int outputParamsCount = 4; //coeff, delta(coeff, index), sigmaSm
        private int widthTable;

        public CriticalParamsService()
        {
            widthTable = outputParamsCount * 3;
            excelTools = new ExcelTools();
        }

        /// <summary>
        /// Вывод совокупности таблиц заданных разрядностей
        /// </summary>
        /// <param name="nStart">Разрядность первой таблицы из набора выводимых таблиц</param>
        /// <param name="nEnd">Разрядность последней таблицы из набора выводимых таблиц</param>
        /// <param name="coeff"></param>
        /// <param name="initialStep">Первоначальный шаг, с которым будут искаться критические значения параметров</param>
        /// <param name="accuracy">Желаемая точность измерения критических параметров</param>
        public void OutputTablesToExcel(int nStart, int nEnd, int coeff, double initialStep = 1, double accuracy = 0.0001)
        {
            int indexOfTable = 0;
            int spacingBetweenTables = 1;
            for (int i = nStart; i <= nEnd; i++)
            {
                int heightTable = (int)Math.Pow(2, i) + 10; 
                OutputTableToExcel(i, coeff, 0, indexOfTable * (widthTable + spacingBetweenTables), initialStep, accuracy);
                OutputTableToExcel(i, coeff, heightTable + spacingBetweenTables, indexOfTable * (widthTable + spacingBetweenTables), -initialStep, accuracy);
                indexOfTable++;
            }
        }

        /// <summary>
        /// Вывод одной таблицы заданной разрядности
        /// </summary>
        /// <param name="n"></param>
        /// <param name="coeff"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="initialStep">Первоначальный шаг, с которым будут искаться критические значения параметров</param>
        /// <param name="accuracy">Желаемая точность измерения критических параметров</param>
        public void OutputTableToExcel(int n, int coeff, int row = 0, int column = 0, double initialStep = 1, double accuracy = 0.0001)
        {
            List<ParamsContainer> parameters = new List<ParamsContainer>();
            parameters.Add(DACEmulator.TestingDelta(n, coeff, DACEmulator.Delta.Coeff, initialStep, accuracy));
            parameters.Add(DACEmulator.TestingDelta(n, coeff, DACEmulator.Delta.Index, initialStep, accuracy));
            parameters.Add(DACEmulator.TestingDelta(n, coeff, DACEmulator.Delta.SM, initialStep, accuracy));

            string title = "Разрядность " + n + " (" + (initialStep > 0 ? "положительные" : "отрицательные") + " значения критических параметров)";
            PrintTableWithCriticalData(parameters, row, column, title);
        }

        /// <summary>
        /// Печать таблицы, которая содержит 3 набора критических параметров
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private void PrintTableWithCriticalData(List<ParamsContainer> parameters, int row = 0, int column = 0, string title= "Заголовок таблицы")
        {
            if (parameters.Count != 3)
                throw new Exception("Число наборов параметров должно быть равно 3!");
            if (outputParamsCount == 0)
                throw new Exception("Непроинициализированное число выводимых параметров в таблицу!");


            Print(title, row, column, widthTable, 2, 4, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.LightGreen);
            for (int i = 0; i < parameters.Count; i++)
            {
                PrintCriticalParameter("K", parameters[i].Coeff.ToString(), row + 2, column + i * outputParamsCount);
                PrintCriticalParameter("ΔK", parameters[i].DeltaCoeff.ToString(), row + 2, column + 1 + i * outputParamsCount);
                PrintCriticalParameter("Δi", parameters[i].DeltaIndex.ToString(), row + 2, column + 2 + i * outputParamsCount);
                PrintCriticalParameter("δсм", parameters[i].DeltaSM.ToString(), row + 2, column + 3 + i * outputParamsCount);
                Borders(row + 2, column + i * outputParamsCount, outputParamsCount, 2);
            }
            Print("Индексы компараторов со сбоем", row + 4, column, widthTable, 2, 4, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Gainsboro);
            Print("Входные и выходные данные", row + 7, column, widthTable, 2, 0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Gainsboro);
            Borders(row + 7, column, widthTable, 2, 4);
            Borders(row + 9, column, widthTable, 1, 4);
            for (int i = 0; i < parameters.Count; i++)
            {
                Print(string.Join(", ", parameters[i].ComparatorsErrorIndexes), row + 6, column + outputParamsCount * i, outputParamsCount, 1, 4);
                PrintVector(parameters[i].InputBinaryCodes.ToArray(), row + 10, column + outputParamsCount * i);
                PrintVector(parameters[i].OutputBinaryCodes.ToArray(), row + 10, column + 1 + outputParamsCount * i);
                PrintVector(parameters[i].InputBinaryCodes.Select(x => x.ToLong().ToString()).ToArray<string>(), row + 10, column + 2 + outputParamsCount * i);
                PrintVector(parameters[i].OutputBinaryCodes.Select(x => x.ToLong().ToString()).ToArray<string>(), row + 10, column + 3 + outputParamsCount * i);
                Print("Вход2", row + 9, column + i * outputParamsCount, 1, 1, 0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Silver);
                Print("Выход2", row + 9, column + i * outputParamsCount + 1, 1, 1, 0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Silver);
                Print("Вход10", row + 9, column + i * outputParamsCount + 2, 1, 1, 0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Silver);
                Print("Выход10", row + 9, column + i * outputParamsCount + 3, 1, 1, 0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color.Silver);
                Borders(row + 10, column + i * outputParamsCount, 4, parameters[i].InputBinaryCodes.Count);
                FillColor(row + 10, column + i * outputParamsCount, Color.LavenderBlush, 1, parameters[i].InputBinaryCodes.Count);
                FillColor(row + 10, column + i * outputParamsCount+1, Color.Gainsboro, 1, parameters[i].InputBinaryCodes.Count);
                FillColor(row + 10, column + i * outputParamsCount+2, Color.LavenderBlush, 1, parameters[i].InputBinaryCodes.Count);
                FillColor(row + 10, column + i * outputParamsCount+3, Color.Gainsboro, 1, parameters[i].InputBinaryCodes.Count);
                for (int j = 0; j < parameters[i].ErrorIndexesFromInputAndOutputCodes.Count; j++)
                    FillColor(row + 10 + parameters[i].ErrorIndexesFromInputAndOutputCodes[j], column + outputParamsCount * i, Color.Red, 4);
            }
            excelTools.SetColumnsWidth(column, column + widthTable, -1);
        }

        /// <summary>
        /// Нанесение границ
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWeight"></param>
        /// <param name="borderStyle"></param>
        private void Borders(int row, int column, int width = 1, int height = 1, int borderWeight = 4, Microsoft.Office.Interop.Excel.XlLineStyle borderStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous)
        {
            if (width < 1 || height < 1 || row < 0 | column < 0 || borderWeight < 0)
                throw new Exception("Отрицательные параметры недопустимы!");
            if (borderWeight > 0)
                excelTools.Borders(column, column + width - 1, row, row + height - 1, borderStyle, borderWeight);
        }

        /// <summary>
        /// Занесение данных в ячейку
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWeight"></param>
        /// <param name="borderStyle"></param>
        private void Print(object text, int row, int column, int width = 1, int height = 1, int borderWeight = 0, Microsoft.Office.Interop.Excel.XlLineStyle borderStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Color? color = null)
        {
            if (width < 1 || height < 1 || row < 0 | column < 0 || borderWeight < 0)
                throw new Exception("Отрицательные параметры недопустимы!");

            excelTools.Write(row, column, text);
            if (width > 1 || height > 1)
                excelTools.MergeCells(column, column + width - 1, row, row + height - 1);
            Borders(row, column, width, height, borderWeight, borderStyle);
            if (color != null)
                FillColor(row, column, color.Value, width, height);
        }

        /// <summary>
        /// Печать критического параметра 
        /// Верхняя строка - название
        /// Нижняя строка - значение
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        private void PrintCriticalParameter(string name, string value, int row, int column, int width = 1)
        {
            Print(name, row, column, width, 1, 0);
            Print(value, row + 1, column, width, 1, 0);
        }

        /// <summary>
        /// Печать массива данных
        /// </summary>
        /// <param name="values"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="cellWidth"></param>
        /// <param name="cellHeight"></param>
        /// <param name="isHorizontal"></param>
        /// <param name="borderWeight"></param>
        /// <param name="borderStyle"></param>
        private void PrintVector(object[] values, int row, int column, int cellWidth = 1, int cellHeight = 1, bool isHorizontal = false, int borderWeight = 0, Microsoft.Office.Interop.Excel.XlLineStyle borderStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous)
        {
            if (cellWidth < 0 || cellWidth < 0)
                throw new Exception("Отрицательные параметры недопустимы!");

            for (int i = 0; i < values.Length; i++)
            {
                if (isHorizontal)
                    Print(values[i], row, column + cellHeight * i, cellWidth, cellHeight, borderWeight, borderStyle);
                else
                    Print(values[i], row + i * cellHeight, column, cellWidth, cellHeight, borderWeight, borderStyle);
            }
            if (isHorizontal)
                Borders(row, column, values.Length * cellWidth, cellHeight, borderWeight, borderStyle);
            else
                Borders(row, column, cellWidth, values.Length * cellHeight, borderWeight, borderStyle);
        }

        private void FillColor(int row, int column, Color color, int width = 1, int height = 1)
        {
            excelTools.FillColor(row, column, color, width, height);
        }

        public void Dispose()
        {
            excelTools.Dispose();
        }

        public void Show()
        {
            excelTools.Show();
        }
    }
}
