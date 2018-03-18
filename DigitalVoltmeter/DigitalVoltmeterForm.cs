using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DigitalVoltmeter
{
    public partial class DigitalVoltmeterForm : Form
    {
        public readonly Color errorCellBackColor = Color.Pink;
        public readonly Color errorCellTextColor = Color.OrangeRed;
        public readonly Font errorFont;

        /// <summary>
        /// Отсортированные списки индексов ошибочных бит ячеек Выхода
        /// </summary>
        List<List<int>> errorBitIndexes = new List<List<int>>() { };

        private ExcelTools excel;
        private WordTools word;

        private LongBits[] e;
        private LongBits[] b;
        private LongBits[] a;

        private double[] modelVoltages;
        private double[] idealVoltages;
        private double voltagesQuantumStep;

        private Color idealVoltageColor = Color.Green;
        private Color modelVoltageColor;

        private GraphExpandingForm expandingForm;

        public DigitalVoltmeterForm()
        {
            InitializeComponent();
            InitializeDataGrid();
            expandingForm = new GraphExpandingForm();
            errorFont = new Font(dataGridViewVect.Font, FontStyle.Bold);
            excel = new ExcelTools(progressBar);
            word = new WordTools(progressBar);
        }

        private void InitializeDataGrid()
        {
            dataGridViewVect.Columns.Clear();

            DataGridViewTextBoxColumn _in = new DataGridViewTextBoxColumn
            {
                Name = "Вход",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn _out = new DataGridViewTextBoxColumn
            {
                Name = "Выход",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = TextRenderer.MeasureText("Выход", dataGridViewVect.Font).Width + 5
            };

            DataGridViewTextBoxColumn _inDes = new DataGridViewTextBoxColumn
            {
                Name = "Вход 10",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn _outDes = new DataGridViewTextBoxColumn
            {
                Name = "Выход 10",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = TextRenderer.MeasureText("Выход 10", dataGridViewVect.Font).Width + 6
            };

            DataGridViewTextBoxColumn _errInds = new DataGridViewTextBoxColumn
            {
                Name = "Ошиб. b",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            dataGridViewVect.Columns.Add(_in);
            dataGridViewVect.Columns.Add(_out);
            dataGridViewVect.Columns.Add(_inDes);
            dataGridViewVect.Columns.Add(_outDes);
            dataGridViewVect.Columns.Add(_errInds);

            dataGridViewVect.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void buttonSaveToExel_Click(object sender, EventArgs e)
        {
            if (this.e == null || b == null || a == null)
                throw new Exception("Should be generate the equations!");

            string[] singleCodesString = this.e.Select(val => val.ToString()).ToArray();
            string[] bString = b.Select(val => val.ToString()).ToArray();
            string[] binaryString = MathProcessor.TransposeBitMatrix(a)
                                       .Select(val => val.ToString()).ToArray();
            excel.GenerateDocument(singleCodesString, bString, binaryString);
            progressBar.Value = 0;
        }

        private void buttonGetFormules_Click(object sender, EventArgs e)
        {
            int bitsCount = int.Parse(textBoxN.Text);
            richTextBox.Text = string.Empty;

            this.e = MathProcessor.GetAllEK((int)Math.Pow(2, bitsCount));
            b = MathProcessor.GetAllEPKFromEK(this.e);

            if (checkBoxOutToWord.Checked)
            {
                string wordFilePath = Environment.CurrentDirectory + "\\Formules[" + (b.Length + 1) + "].docx";
                word.GenerateDocument(b, out a, wordFilePath);
                FillRichTextBoxFromWord(wordFilePath);
                progressBar.Value = 0;
            }
            else
            {
                string[] formules = MathProcessor.Formules(b, out a);
                for (int i = 0; i < formules.Length; i++)
                    richTextBox.Text += "a" + i + " =" + formules[i] + Environment.NewLine;
            }
        }

        private void FillRichTextBoxFromWord(string docxFile)
        {
            richTextBox.Text = string.Empty;
            string rtfPath = string.Empty;
            rtfPath = WordTools.GetRTFFromDOCXFile(docxFile);
            richTextBox.LoadFile(rtfPath);
        }

        private void buttonGetModel_Click(object sender, EventArgs e)
        {
            int n = 0, deltaIndex = 0;
            double coeff = 0, deltaCoeff = 0, deltaSM = 0;
            try
            {
                n = int.Parse(textBoxN.Text);
                coeff = double.Parse(textBoxK.Text);
                deltaCoeff = double.Parse(textBoxDK.Text);
                deltaIndex = int.Parse(textBoxDi.Text);
                deltaSM = double.Parse(textBoxDUsm.Text);
            }
            catch
            {
                MessageBox.Show("Input parameters have wrong values!");
                return;
            }

            DACEmulator emulator = new DACEmulator(n, coeff, deltaCoeff, deltaIndex, deltaSM);
            voltagesQuantumStep = emulator.RealStep;
            int countNumbers = (int)Math.Pow(2, n);
            modelVoltages = new double[countNumbers];
            idealVoltages = new double[countNumbers];

            dataGridViewVect.Rows.Clear();
            for (int x = 0; x < countNumbers; x++)
            {
                modelVoltages[x] = emulator.Uin(x);
                idealVoltages[x] = emulator.IdealUin(x);
                LongBits binaryCode = emulator.GetDKFromComparators(x);
                LongBits inCode = new LongBits(x, n);
                int[] errorInds = emulator.GetEKPErrorFromComparators(x);
                
                dataGridViewVect.Rows.Add(new object[] { inCode, binaryCode, inCode.ToLong(), binaryCode.ToLong(), string.Join(", ", errorInds) });
                if (inCode != binaryCode)
                    dataGridViewVect.Rows[x].DefaultCellStyle.BackColor = errorCellBackColor;
            }
            modelVoltageColor = Color.DarkOrchid;
            VoltageChartService chartService = new VoltageChartService(this.mainChart, "Входное напряжение", voltagesQuantumStep);
            chartService.AddInputVoltageList("Voltages", modelVoltages, modelVoltageColor, 2);
            chartService.AddInputVoltageList("Ideal voltages", idealVoltages, idealVoltageColor, 2);
        }

        private List<ParamsContainer> TestingModel(int n, double coeff)
        {
            List<ParamsContainer> list = new List<ParamsContainer>();
            list.Add(DACEmulator.TestingDelta(n, coeff, DACEmulator.Delta.Coeff));
            list.Add(DACEmulator.TestingDelta(n, coeff, DACEmulator.Delta.Index));
            list.Add(DACEmulator.TestingDelta(n, coeff, DACEmulator.Delta.SM));
            return list;
        }

        /// NOTE метод не оптимизирован! Его ожидает рефакторинг.
        /// NOTE не рекомендуется выставлять startN и endN ниже числа 6 (возможен долгий поиск критических параметров)
        /// <summary>
        /// Изменение параметров для нахождения критических значений
        /// При которых начинается ошибка
        /// </summary>
        private void CalculateExtremeParameters(int startN, int endN, double coeff)
        {
            if (startN > endN)
                throw new Exception("Ошибка значений разрядности!");

            ExcelTools ex = new ExcelTools();
            int w = 15;
            for (int i = startN; i <= endN; i++)
            {
                List<ParamsContainer> pcsList = FindCriticalParameters(i + 1, coeff);
                ParamsContainer pc1 = pcsList[0];
                ParamsContainer pc2 = pcsList[1];
                ParamsContainer pc3 = pcsList[2];

                ex.Write(0, i * (w + 1), "Разрядность: " + pc1.N);
                ex.Write(1, i * (w + 1), "K");
                ex.Write(1, i * (w + 1) + 2, "ΔK");
                ex.Write(1, i * (w + 1) + 3, "Δi");
                ex.Write(1, i * (w + 1) + 4, "δсм:");
                ex.Write(2, i * (w + 1), "" + pc1.Coeff);
                ex.Write(2, i * (w + 1) + 2, "" + pc1.DeltaCoeff);
                ex.Write(2, i * (w + 1) + 3, "" + pc1.DeltaIndex);
                ex.Write(2, i * (w + 1) + 4, "" + pc1.DeltaSM);
                ex.Write(6, i * (w + 1), "Вход2");
                ex.Write(6, i * (w + 1) + 1, "Выход2");
                ex.Write(6, i * (w + 1) + 2, "Вход10");
                ex.Write(6, i * (w + 1) + 3, "Выход10");

                ex.Write(1, i * (w + 1) + 5, "K");
                ex.Write(1, i * (w + 1) + 7, "ΔK");
                ex.Write(1, i * (w + 1) + 8, "Δi");
                ex.Write(1, i * (w + 1) + 9, "δсм:");
                ex.Write(2, i * (w + 1) + 5, "" + pc2.Coeff);
                ex.Write(2, i * (w + 1) + 7, "" + pc2.DeltaCoeff);
                ex.Write(2, i * (w + 1) + 8, "" + pc2.DeltaIndex);
                ex.Write(2, i * (w + 1) + 9, "" + pc2.DeltaSM);
                ex.Write(6, i * (w + 1) + 5, "Вход2");
                ex.Write(6, i * (w + 1) + 6, "Выход2");
                ex.Write(6, i * (w + 1) + 7, "Вход10");
                ex.Write(6, i * (w + 1) + 8, "Выход10");

                ex.Write(1, i * (w + 1) + 10, "K");
                ex.Write(1, i * (w + 1) + 12, "ΔK");
                ex.Write(1, i * (w + 1) + 13, "Δi");
                ex.Write(1, i * (w + 1) + 14, "δсм:");
                ex.Write(2, i * (w + 1) + 10, "" + pc3.Coeff);
                ex.Write(2, i * (w + 1) + 12, "" + pc3.DeltaCoeff);
                ex.Write(2, i * (w + 1) + 13, "" + pc3.DeltaIndex);
                ex.Write(2, i * (w + 1) + 14, "" + pc3.DeltaSM);
                ex.Write(6, i * (w + 1) + 10, "Вход2");
                ex.Write(6, i * (w + 1) + 11, "Выход2");
                ex.Write(6, i * (w + 1) + 12, "Вход10");
                ex.Write(6, i * (w + 1) + 13, "Выход10");

                ex.Write(3, i * (w + 1), "Индексы компараторов со сбоем");
                ex.Write(4, i * (w + 1), string.Join(", ", pc1.ComparatorsErrorIndexes));
                ex.Write(4, i * (w + 1) + 5, string.Join(", ", pc2.ComparatorsErrorIndexes));
                ex.Write(4, i * (w + 1) + 10, string.Join(", ", pc3.ComparatorsErrorIndexes));
                ex.Write(5, i * (w + 1), "Пары двоичных кодов");

                //Для pc1
                for (int j = 0; j < pc1.InputBinaryCodes.Count; j++)
                {
                    LongBits bits = pc1.InputBinaryCodes[j];
                    ex.Write(j + 6 + 1, i * (w + 1), bits.ToString());
                    ex.Write(j + 6 + 1, i * (w + 1) + 2, bits.ToLong() + "");
                    if (pc1.ErrorIndexesFromInputAndOutputCodes.Contains(j))
                    {
                        ex.FillColor(j + 7, i * (w + 1));
                        ex.FillColor(j + 7, i * (w + 1) + 1);
                        ex.FillColor(j + 7, i * (w + 1) + 2);
                        ex.FillColor(j + 7, i * (w + 1) + 3);
                    }
                }
                for (int j = 0; j < pc1.OutputBinaryCodes.Count; j++)
                {
                    LongBits bits = pc1.OutputBinaryCodes[j];
                    ex.Write(j + 6 + 1, i * (w + 1) + 1, bits.ToString());
                    ex.Write(j + 6 + 1, i * (w + 1) + 3, bits.ToLong() + "");
                }
                //Для pc2
                for (int j = 0; j < pc2.InputBinaryCodes.Count; j++)
                {
                    LongBits bits = pc2.InputBinaryCodes[j];
                    ex.Write(j + 6 + 1, i * (w + 1) + 5, bits.ToString());
                    ex.Write(j + 6 + 1, i * (w + 1) + 7, bits.ToLong() + "");
                    if (pc2.ErrorIndexesFromInputAndOutputCodes.Contains(j))
                    {
                        ex.FillColor(j + 7, i * (w + 1) + 5);
                        ex.FillColor(j + 7, i * (w + 1) + 6);
                        ex.FillColor(j + 7, i * (w + 1) + 7);
                        ex.FillColor(j + 7, i * (w + 1) + 8);
                    }
                }
                for (int j = 0; j < pc2.OutputBinaryCodes.Count; j++)
                {
                    LongBits bits = pc2.OutputBinaryCodes[j];
                    ex.Write(j + 6 + 1, i * (w + 1) + 6, bits.ToString());
                    ex.Write(j + 6 + 1, i * (w + 1) + 8, bits.ToLong() + "");
                }
                //Для pc3
                for (int j = 0; j < pc3.InputBinaryCodes.Count; j++)
                {
                    LongBits bits = pc3.InputBinaryCodes[j];
                    ex.Write(j + 6 + 1, i * (w + 1) + 10, bits.ToString());
                    ex.Write(j + 6 + 1, i * (w + 1) + 12, bits.ToLong() + "");
                    if (pc3.ErrorIndexesFromInputAndOutputCodes.Contains(j))
                    {
                        ex.FillColor(j + 7, i * (w + 1) + 10);
                        ex.FillColor(j + 7, i * (w + 1) + 11);
                        ex.FillColor(j + 7, i * (w + 1) + 12);
                        ex.FillColor(j + 7, i * (w + 1) + 13);
                    }
                }
                for (int j = 0; j < pc3.OutputBinaryCodes.Count; j++)
                {
                    LongBits bits = pc3.OutputBinaryCodes[j];
                    ex.Write(j + 6 + 1, i * (w + 1) + 11, bits.ToString());
                    ex.Write(j + 6 + 1, i * (w + 1) + 13, bits.ToLong() + "");
                }


                ex.MergeCells(i * (w + 1), i * (w + 1) + (w - 1), 0, 0); //разрядность
                ex.MergeCells(i * (w + 1), i * (w + 1) + 1, 1, 1); //K1
                ex.MergeCells(i * (w + 1), i * (w + 1) + 1, 2, 2); //ΔK1
                ex.MergeCells(i * (w + 1) + 5, i * (w + 1) + 6, 1, 1); //K2
                ex.MergeCells(i * (w + 1) + 5, i * (w + 1) + 6, 2, 2); //ΔK2
                ex.MergeCells(i * (w + 1) + 10, i * (w + 1) + 11, 1, 1); //K3
                ex.MergeCells(i * (w + 1) + 10, i * (w + 1) + 11, 2, 2); //ΔK3
                ex.MergeCells(i * (w + 1), i * (w + 1) + (w - 1), 3, 3); //Индексы компараторов со сбоем
                ex.MergeCells(i * (w + 1), i * (w + 1) + 4, 4, 4); //Сами индекс компараторов
                ex.MergeCells(i * (w + 1) + 5, i * (w + 1) + 9, 4, 4); //Сами индекс компараторов
                ex.MergeCells(i * (w + 1) + 10, i * (w + 1) + 14, 4, 4); //Сами индекс компараторов
                ex.MergeCells(i * (w + 1), i * (w + 1) + (w - 1), 5, 5); //Индексы компараторов со сбоем

                ex.Borders(i * (w + 1), i * (w + 1) + (w - 1), 0, 0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, 4);
                ex.Borders(i * (w + 1), i * (w + 1) + 4, 0, 2, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, 4);
                ex.Borders(i * (w + 1), i * (w + 1) + 9, 0, 2, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, 4);
                ex.Borders(i * (w + 1), i * (w + 1) + 14, 0, 2, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, 4);
                ex.Borders(i * (w + 1), i * (w + 1) + 4, 4, 4, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, 4);
                ex.Borders(i * (w + 1), i * (w + 1) + 9, 4, 4, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, 4);
                ex.Borders(i * (w + 1), i * (w + 1) + 14, 4, 4, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, 4);

                ex.SetColumnsWidth(i * (w + 1), i * (w + 1) + (w - 1), 25);
            }
            ex.Show();
            ex.Dispose();
        }

        /// <summary>
        /// Поиск критических значений параметров
        /// deltaCoeff, deltaIndex, deltaSM
        /// при которых происходит сбой компараторов
        /// </summary>
        /// <param name="n"></param>
        /// <param name="coeff"></param>
        /// <returns></returns>
        List<ParamsContainer> FindCriticalParameters(int n, double coeff)
        {
            List<ParamsContainer> l = new List<ParamsContainer> { };
            DACEmulator emulator;
            double deltaCoeff = 0;
            int deltaIndex = 0;
            double deltaSM = 0;
            List<int> indexes = new List<int> { };
            List<int> diffs = null;
            ParamsContainer p = null;

            while (indexes.Count == 0)
            {
                emulator = new DACEmulator(n, coeff, deltaCoeff, deltaIndex, deltaSM);
                p = new ParamsContainer(n, coeff, 0, 0, 0);
                int countNumbers = (int)Math.Pow(2, n);
                int x;
                for (x = 0; x < countNumbers; x++)
                {
                    indexes.AddRange(emulator.GetEKPErrorFromComparators(x).ToList());
                    LongBits inputBinaryCode = new LongBits(x, n);
                    LongBits outputBinaryCode = emulator.GetDKFromComparators(x);
                    if (!GetIndexesOfDiffs(inputBinaryCode, outputBinaryCode, out diffs))
                    {
                        p.ErrorIndexesFromInputAndOutputCodes.Add(x);
                    }
                    p.InputBinaryCodes.Add(inputBinaryCode);
                    p.OutputBinaryCodes.Add(outputBinaryCode);
                }
                deltaCoeff += 0.0001;
            }
            p.ComparatorsErrorIndexes = indexes.Distinct().ToArray();
            p.DeltaCoeff = deltaCoeff;
            l.Add(p);

            deltaCoeff = 0;
            indexes = new List<int> { };
            while (indexes.Count == 0)
            {
                emulator = new DACEmulator(n, coeff, deltaCoeff, deltaIndex, deltaSM);
                p = new ParamsContainer(n, coeff, 0, 0, 0);
                int countNumbers = (int)Math.Pow(2, n);
                int x;
                for (x = 0; x < countNumbers; x++)
                {
                    indexes.AddRange(emulator.GetEKPErrorFromComparators(x).ToList());
                    LongBits inputBinaryCode = new LongBits(x, n);
                    LongBits outputBinaryCode = emulator.GetDKFromComparators(x);
                    if (!GetIndexesOfDiffs(inputBinaryCode, outputBinaryCode, out diffs))
                    {
                        p.ErrorIndexesFromInputAndOutputCodes.Add(x);
                    }
                    p.InputBinaryCodes.Add(inputBinaryCode);
                    p.OutputBinaryCodes.Add(outputBinaryCode);
                }
                deltaIndex++;
            }
            p.DeltaIndex = deltaIndex;
            p.ComparatorsErrorIndexes = indexes.Distinct().ToArray();
            l.Add(p);

            indexes = new List<int> { };
            deltaIndex = 0;
            while (indexes.Count == 0)
            {
                emulator = new DACEmulator(n, coeff, deltaCoeff, deltaIndex, deltaSM);
                p = new ParamsContainer(n, coeff, 0, 0, 0);
                int countNumbers = (int)Math.Pow(2, n);
                int x;
                for (x = 0; x < countNumbers; x++)
                {
                    indexes.AddRange(emulator.GetEKPErrorFromComparators(x).ToList());
                    LongBits inputBinaryCode = new LongBits(x, n);
                    LongBits outputBinaryCode = emulator.GetDKFromComparators(x);
                    if (!GetIndexesOfDiffs(inputBinaryCode, outputBinaryCode, out diffs))
                    {
                        p.ErrorIndexesFromInputAndOutputCodes.Add(x);
                    }
                    p.InputBinaryCodes.Add(inputBinaryCode);
                    p.OutputBinaryCodes.Add(outputBinaryCode);
                }
                deltaSM += 0.0001;
            }
            p.DeltaSM = deltaSM;
            p.ComparatorsErrorIndexes = indexes.Distinct().ToArray();
            l.Add(p);
            return l;
        }

        bool GetIndexesOfDiffs(LongBits first, LongBits second, out List<int> diffs)
         {//Все таки пришлось циклом сравнить. Ваня, не бей(
             diffs = null;
             if (first.Length != second.Length) return first == second;
             diffs = new List<int>() { };
             string sf = first.ToString();
             string ss = second.ToString();
             for (int i = 0; i<sf.Length; i++)
                 if (sf[i] != ss[i]) diffs.Add(i);
             return diffs.Count == 0;
         }
 


        private void buttonExpand_Click(object sender, EventArgs e)
        {
            if (modelVoltages == null)
            {
                MessageBox.Show("First generate the data!");
                return;
            }

            if (!expandingForm.Visible)
            {
                expandingForm = new GraphExpandingForm();
                expandingForm.Chart.Series.Clear();
                VoltageChartService chartService = new VoltageChartService(expandingForm.Chart, "Входное напряжение", voltagesQuantumStep);
                chartService.AddInputVoltageList("Voltages", modelVoltages, modelVoltageColor, 2);
                chartService.AddInputVoltageList("Ideal voltages", idealVoltages, idealVoltageColor, 2);
                expandingForm.Show();
            }
        }

        private void dataGridViewVect_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || (e.ColumnIndex != 1 && e.ColumnIndex != 3))
                return;

            DataGridView dataGridView = sender as DataGridView;
            LongBits inCode = dataGridView.Rows[e.RowIndex].Cells[0].Value as LongBits;
            LongBits binaryCode = dataGridView.Rows[e.RowIndex].Cells[1].Value as LongBits;
            int[] difference = LongBits.GetStringIndexesOfDifferenceBits(inCode, binaryCode);

            //кастомная отрисовка ячеек Выхода
            if (e.ColumnIndex == 1)
            {
                e.PaintBackground(e.ClipBounds, true);

                Font font = e.CellStyle.Font;
                TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter;

                string text = (string)e.FormattedValue;

                List<string> subStrings = new List<string>();
                int curInd = 0;
                foreach (int ind in difference)
                {
                    subStrings.Add(text.Substring(curInd, ind - curInd));
                    subStrings.Add(text.Substring(ind, 1));
                    curInd = ind + 1;
                }
                subStrings.Add(text.Substring(curInd, text.Length - curInd));

                bool errorState = false;
                Size size;
                Rectangle curBox = new Rectangle(e.CellBounds.X + 3, e.CellBounds.Y - 1, 0, e.CellBounds.Height);
                for (int i = 0; i < subStrings.Count; i++)
                {
                    Font curFont = errorState ? errorFont : font;
                    Color curColor = errorState ? errorCellTextColor : e.CellStyle.ForeColor;

                    size = TextRenderer.MeasureText(e.Graphics, subStrings[i], curFont, e.CellBounds.Size, flags);
                    curBox = new Rectangle(curBox.X + curBox.Width, curBox.Y, size.Width, curBox.Height);
                    TextRenderer.DrawText(e.Graphics, subStrings[i], curFont, curBox, curColor, flags);

                    errorState = !errorState;
                }
                int cellWidth = curBox.Location.X - e.CellBounds.Location.X + curBox.Width + 5;
                dataGridViewVect.Columns[1].Width = Math.Max(dataGridViewVect.Columns[1].Width, cellWidth);
                e.Handled = true;
            }
            else if (e.ColumnIndex == 3)
            {
                e.PaintBackground(e.ClipBounds, true);
                bool error = difference.Length > 0;
                Font font = error ? errorFont : e.CellStyle.Font;
                Color color = error ? errorCellTextColor : e.CellStyle.ForeColor;
                TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter;

                string text = (string)e.FormattedValue;

                Size size = TextRenderer.MeasureText(e.Graphics, text, font, e.CellBounds.Size, flags);
                Rectangle Box = new Rectangle(e.CellBounds.X + 3, e.CellBounds.Y - 1, size.Width, e.CellBounds.Height);
                TextRenderer.DrawText(e.Graphics, text, font, Box, color, flags);

                int cellWidth = e.CellBounds.Location.X - e.CellBounds.Location.X + Box.Width + 8;
                dataGridViewVect.Columns[3].Width = Math.Max(dataGridViewVect.Columns[3].Width, cellWidth);
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<ParamsContainer> list = TestingModel(int.Parse(textBoxN.Text), double.Parse(textBoxK.Text));
            labelCriticalDK.Text = list[0].DeltaCoeff.ToString();
            labelCriticalDi.Text = list[1].DeltaIndex.ToString();
            labelCriticalDsm.Text = list[2].DeltaSM.ToString();
        }

        private void comboBoxResistorsCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void DigitalVoltmeterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (excel != null)
                excel.Dispose();
        }

        private void dataGridViewVect_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewVect.ClearSelection();
        }
    }
}