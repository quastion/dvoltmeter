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
        Color errorCellBackColor = Color.Pink;
        Color errorCellTextColor = Color.Firebrick;

        /// <summary>
        /// Отсортированные списки индексов ошибочных бит ячеек Выхода
        /// </summary>
        List<List<int>> errorBitIndexes = new List<List<int>>() { };

        private ExcelTools excel;
        private WordTools word;

        private LongBits[] e;
        private LongBits[] b;
        private LongBits[] a;

        private double[] voltages;

        private GraphExpandingForm expandingForm = new GraphExpandingForm();

        public DigitalVoltmeterForm()
        {
            InitializeComponent();
            initializeDataGrid();
            excel = new ExcelTools(progressBar);
            word = new WordTools(progressBar);
        }

        void initializeDataGrid()
        {
            dataGridViewVect.Columns.Clear();

<<<<<<< HEAD
            DataGridViewTextBoxColumn _num = new DataGridViewTextBoxColumn();
            _num.Name = "№";
            _num.SortMode = DataGridViewColumnSortMode.NotSortable;
            _num.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
=======
            DataGridViewTextBoxColumn _in = new DataGridViewTextBoxColumn
            {
                Name = "Вход",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
>>>>>>> 80c1c35660355b40fc8af46632a746554f09dde2

            DataGridViewTextBoxColumn _out = new DataGridViewTextBoxColumn
            {
                Name = "Выход",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn _inDes = new DataGridViewTextBoxColumn();
            _inDes.Name = "Вход 10";
            _inDes.SortMode = DataGridViewColumnSortMode.NotSortable;
            _inDes.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn _outDes = new DataGridViewTextBoxColumn();
            _outDes.Name = "Выход 10";
            _outDes.SortMode = DataGridViewColumnSortMode.NotSortable;
            _outDes.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridViewVect.Columns.Add(_num);
            dataGridViewVect.Columns.Add(_in);
            dataGridViewVect.Columns.Add(_out);
            dataGridViewVect.Columns.Add(_inDes);
            dataGridViewVect.Columns.Add(_outDes);

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
            int countNumbers = (int)Math.Pow(2, n);
            voltages = new double[countNumbers];
            dataGridViewVect.Rows.Clear();
            for (int x = 0; x < countNumbers; x++)
            {
                voltages[x] = emulator.Uin(x);
                LongBits binaryCode = emulator.GetDKFromComparators(x);
                LongBits inCode = new LongBits(x, n);

                dataGridViewVect.Rows.Add(new object[] { x, inCode, binaryCode });
                if (inCode != binaryCode)
<<<<<<< HEAD
                    dataGridViewVect.Rows[x].DefaultCellStyle.BackColor = errorCellBackColor;
=======
                {
                    foreach (DataGridViewCell cell in dataGridViewVect.Rows[x].Cells)
                        cell.Style.BackColor = Color.Tomato;
                }
>>>>>>> 80c1c35660355b40fc8af46632a746554f09dde2
            }
            VoltageChartService.DrawInputVoltageList(mainChart, "Voltages", voltages, Color.Red, 2);
        }

        private void buttonExpand_Click(object sender, EventArgs e)
        {
            if (voltages == null)
            {
                MessageBox.Show("First generate the data!");
                return;
            }

            if (!expandingForm.Visible)
            {
                expandingForm = new GraphExpandingForm();
                expandingForm.Chart.Series.Clear();
                VoltageChartService.DrawInputVoltageList(expandingForm.Chart, "Voltages", voltages, Color.Red, 2);
                expandingForm.Show();
            }
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
            dataGridViewVect.ClearSelection();//всем привет, я костыль
        }

        void MarkCellErrors()
        {
        }

        private void dataGridViewVect_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0 && errorBitIndexes.ElementAtOrDefault(e.RowIndex) != null)
            {//кастомная отрисовка ячеек Выхода
                e.PaintBackground(e.ClipBounds, true);

                Font font = e.CellStyle.Font;
                Font errorFont = new Font(font, FontStyle.Bold);
                TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter;

                string text = (string)e.FormattedValue;

                List<string> subStrings = new List<string>() { };
                int curInd = 0;
                foreach (int ind in errorBitIndexes[e.RowIndex])
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

                e.Handled = true;
            }
        }
    }
}