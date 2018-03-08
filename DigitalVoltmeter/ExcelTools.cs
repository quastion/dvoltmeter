using Microsoft.Office.Interop.Excel;
using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace DigitalVoltmeter
{
    class ExcelTools
    {
        private Excel.Application excelApp;
        private Workbook workBook;

        private ProgressBar bar = null;
        DelegatePerformStep performStep = null;

        private delegate void DelegatePerformStep();
        private delegate void SetMaxValue(int value);
        private delegate void ChangeValue(int value);

        public ExcelTools(ProgressBar bar = null)
        {
            SetProgressBar(bar);
        }

        public void SetProgressBar(ProgressBar bar)
        {
            this.bar = bar;
            if (bar != null)
                performStep = new DelegatePerformStep(bar.PerformStep);
        }

        private void SetMaxValueBar(int maxValue)
        {
            if (bar == null)
                return;
            ChangeValue changeValue = new ChangeValue(value => bar.Value = value);
            SetMaxValue setMaxValue = new SetMaxValue(value => bar.Maximum = value);
            bar.Invoke(changeValue, bar.Minimum);
            bar.Invoke(setMaxValue, maxValue);
        }

        private void PerformStepBar()
        {
            if (bar == null)
                return;
            bar.Invoke(performStep);
        }

        public void GenerateExcel(string[] singleCodes, string[] b, string[] binaryCodes)
        {
            excelApp = new Excel.Application();
            workBook = excelApp.Workbooks.Add();
            Worksheet workSheet = workBook.Worksheets.get_Item(1);

            SetMaxValueBar(singleCodes.Length + b.Length + binaryCodes.Length);

            int columnStartingNum = 1;
            int rowStartingNum = 1;

            //Выравнивание по центру во всех ячейках
            workSheet.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            workSheet.Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;

            //Таблица с единичным кодом
            //Заголовки
            workSheet.get_Range("A1", "A2").Cells.Merge(Type.Missing);
            string startingLiteral = "B1";
            string endLiteral = (singleCodes.Length - 1 > 26 ? ((char)('A' + (singleCodes.Length - 1) / 26 - 1)).ToString() : "") +
                 (char)('A' + (singleCodes.Length - 1) % 26)
                + "1";
            workSheet.get_Range(startingLiteral, endLiteral).Cells.Merge(Type.Missing);
            workSheet.Cells[rowStartingNum, 1] = "N10";
            workSheet.Cells[rowStartingNum, 2] = "Единичный код";
            rowStartingNum++;
            //Вывод значений единичных кодов
            printTable(singleCodes, rowStartingNum, columnStartingNum, workSheet, "e", 1);

            //Таблица с единичным позиционным кодом
            columnStartingNum += singleCodes.Length - 1;
            rowStartingNum = 1;
            //Заголовки
            startingLiteral = (columnStartingNum > 26 ? ((char)('A' + columnStartingNum / 26 - 1)).ToString() : "")
                + (char)('A' + (columnStartingNum % 26)) + "1";
            endLiteral = (columnStartingNum + singleCodes.Length - 2 > 26 ? ((char)('A' + (columnStartingNum + singleCodes.Length - 2) / 26 - 1)).ToString() : "")
                + (char)('A' + (columnStartingNum + singleCodes.Length - 2) % 26) + "1";
            workSheet.get_Range(startingLiteral, endLiteral).Cells.Merge(Type.Missing);
            workSheet.Cells[rowStartingNum, columnStartingNum + 1] = "Единичный позиционный код";
            rowStartingNum++;
            //Вывод индексов единичных позиционных кодов
            for (int i = b.Length; i > 0; i--)
            {
                workSheet.Cells[rowStartingNum, columnStartingNum + i] = "b" + (i);
            }
            rowStartingNum++;
            //Вывод значений с единичными позиционными кодами
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < b[i].Length; j++)
                {
                    workSheet.Cells[rowStartingNum + j, columnStartingNum + i + 1] =
                        b[i][b[i].Length - 1 - j].ToString();
                }
                PerformStepBar();
            }

            //Таблица с двоичным кодом
            columnStartingNum += singleCodes.Length - 1;
            rowStartingNum = 1;
            //Заголовки
            startingLiteral = (columnStartingNum > 26 ? ((char)('A' + columnStartingNum / 26 - 1)).ToString() : "")
                + (char)('A' + (columnStartingNum % 26)) + "1";
            endLiteral = (columnStartingNum + (int)Math.Ceiling(Math.Log(binaryCodes.Length, 2)) - 1 > 26 ? ((char)('A' + (columnStartingNum + (int)Math.Ceiling(Math.Log(binaryCodes.Length, 2)) - 1) / 26 - 1)).ToString() : "") +
                (char)('A' + (columnStartingNum + (int)Math.Ceiling(Math.Log(binaryCodes.Length, 2)) - 1) % 26)
                + "1";
            workSheet.get_Range(startingLiteral, endLiteral).Cells.Merge(Type.Missing);
            workSheet.Cells[rowStartingNum, columnStartingNum + 1] = "Двоичный код";
            rowStartingNum++;
            //Вывод заголовков с индексами двоичных кодов
            int bitsCount = (int)Math.Ceiling(Math.Log(binaryCodes.Length, 2));
            for (int i = 0; i < bitsCount; i++)
            {
                workSheet.Cells[rowStartingNum, columnStartingNum + i + 1] = "2^" + (bitsCount - i - 1);
            }
            rowStartingNum++;
            //Вывод значений с двоичными кодами
            for (int i = 0; i < binaryCodes.Length; i++)
            {
                for (int j = 0; j < binaryCodes[i].Length; j++)
                {
                    workSheet.Cells[rowStartingNum + i, columnStartingNum + j + 1] =
                        binaryCodes[i][j].ToString();
                }
                PerformStepBar();
            }

            excelApp.Visible = true;
            excelApp.UserControl = true;
        }

        /// <summary>
        /// Печать таблицы с единичными кодами
        /// </summary>
        /// <param name="values">Массив единичных кодов</param>
        /// <param name="row">Строка, с которой следует начать заполнение ячеек</param>
        /// <param name="column">Столбец, с которого следует начать заполнение ячеек</param>
        /// <param name="workSheet">Страница excel</param>
        /// <param name="literal">Буква индекса для заголовка</param>
        /// <param name="startingIndex">Индекс массива, с которого следует начать заполнение ячеек</param>
        private void printTable(string[] values, int row, int column, Worksheet workSheet, string literal, int startingIndex)
        {
            if (startingIndex >= values.Length)
                throw new Exception("Индекс первого выводимого элемента не может быть больше размера массива");

            for (int i = values.Length - startingIndex; i > 0; i--)
            {
                workSheet.Cells[row, column + i] = literal + (i);
            }
            row++;
            for (int i = 0; i < values.Length; i++)
            {
                workSheet.Cells[row + i, 1] = i;
                for (int j = startingIndex; j < values[i].Length; j++)
                {
                    workSheet.Cells[row + i, column + (values[i].Length - j)] =
                        values[i][j].ToString();
                }
                PerformStepBar();
            }
        }

        public void Dispose()
        {
            if (excelApp != null)
                excelApp.Quit();
        }
    }
}