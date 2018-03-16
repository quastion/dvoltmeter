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
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            dataGridViewVect.Columns.Add(_in);
            dataGridViewVect.Columns.Add(_out);

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
                dataGridViewVect.Rows.Add(new object[] { inCode, binaryCode });
                if (inCode != binaryCode)
                    foreach (DataGridViewCell cell in dataGridViewVect.Rows[x].Cells)
                        cell.Style.BackColor = Color.Tomato;
            }
            VoltageChartService.DrawInputVoltageList(mainChart, "voltages", voltages, Color.Red, 2);
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
                VoltageChartService.DrawInputVoltageList(expandingForm.Chart, "voltages", voltages, Color.Red, 2);
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
    }
}