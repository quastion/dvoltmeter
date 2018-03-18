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

        private ExcelTools excel;
        private WordTools word;

        private LongBits[] e;
        private LongBits[] b;
        private LongBits[] a;

        private double[] modelVoltages;
        private double[] idealVoltages;
        private double voltagesQuantumStep;

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
            int n = 0, coeff = 0, deltaCoeff = 0, deltaIndex = 0;
            double deltaSM = 0;
            try
            {
                n = int.Parse(textBoxN.Text);
                coeff = int.Parse(textBoxK.Text);
                deltaCoeff = int.Parse(textBoxDK.Text);
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
            VoltageChartService chartService = new VoltageChartService(this.mainChart, "Входное напряжение", voltagesQuantumStep);
            chartService.AddInputVoltageList("Voltages", modelVoltages, Color.Red, 2);
            chartService.AddInputVoltageList("Ideal voltages", idealVoltages, Color.Yellow, 2);
        }

        private List<ParamsContainer> TestingModel(int n, double coeff)
        {
            List<ParamsContainer> list = new List<ParamsContainer>();
            list.Add(DACEmulator.TestingDelta(n, coeff, DACEmulator.Delta.Coeff));
            list.Add(DACEmulator.TestingDelta(n, coeff, DACEmulator.Delta.Index));
            list.Add(DACEmulator.TestingDelta(n, coeff, DACEmulator.Delta.SM));
            return list;
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
                chartService.AddInputVoltageList("Voltages", modelVoltages, Color.Yellow, 2);
                chartService.AddInputVoltageList("Voltages", modelVoltages, Color.Red, 2);
                expandingForm.Show();
            }
        }

        private void dataGridViewVect_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridView dataGridView = sender as DataGridView;
            LongBits inCode = dataGridView.Rows[e.RowIndex].Cells[0].Value as LongBits;
            LongBits binaryCode = dataGridView.Rows[e.RowIndex].Cells[1].Value as LongBits;
            int[] difference = LongBits.GetStringIndexesOfDifferenceBits(inCode, binaryCode);
            
            if (difference.Length == 0)
                return;

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
            MessageBox.Show("Для n=" + list[0].N + ";K="+list[0].Coeff+"\n" +
            "ΔK критическое значение: " + list[0].DeltaCoeff + "\n" +
            "Δi критическое значение: " + list[1].DeltaIndex + "\n" +
            "δсм критическое значение: " + list[2].DeltaSM);
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