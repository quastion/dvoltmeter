using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

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
            excel = new ExcelTools(progressBar);
            word = new WordTools(progressBar);
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
                //string[] formules = MathProcessor.Formules(b, out a);

                //for (int i = 0; i < formules.Length; i++)
                //    richTextBox.Text += "a" + i + " =" + formules[i] + Environment.NewLine;

                //---> здесь были тесты нового функционала (ДА - ОНО РАБОТАЕТ)
                DACEmulator dac = new DACEmulator(8, 1000, 0, 0, 0);
                LongBits eeeboy = dac.GetEKFromComparators(254);
                LongBits bbbboy = MathProcessor.GetEPKFromEK(eeeboy);
                LongBits aaaboy;
                string[] formules = MathProcessor.Formules(bbbboy, out aaaboy);

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

        private void comboBoxResistorsCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void DigitalVoltmeterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (excel != null)
                excel.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int N = 0, K = 0, DK = 0, Di = 0;
            double DUsm = 0;
            try
            {
                N = int.Parse(textBoxN.Text);
                K = int.Parse(textBoxK.Text);
                DK = int.Parse(textBoxDK.Text);
                Di = int.Parse(textBoxDi.Text);
                DUsm = double.Parse(textBoxDUsm.Text);
            }
            catch
            {
                MessageBox.Show("Input parameters have wrong values!");
                return;
            }

            DACEmulator emulator = new DACEmulator(N, K, DK, Di, DUsm);
            double quantStep = emulator.QuantStep;
            double deltaUsm = 0;
            voltages = new double[(int)Math.Pow(2, N) - 1];
            for (int i = 0; i < voltages.Length; i++)
                voltages[i] = quantStep * (i + 1) + deltaUsm;
            ChartService chartService = new ChartService(mainChart);
            chartService.drawInputVoltageList(voltages, Color.Red, 1);

            listBoxInputX.Items.Clear();
            listBoxOutputA.Items.Clear();
            for (int i = 1; i < (int)Math.Pow(2, N); i++)
            {
                LongBits ee = emulator.GetEKFromComparators(i);
                LongBits bb = MathProcessor.GetEPKFromEK(ee);
                LongBits aa;
                string[] formules = MathProcessor.Formules(bb, out aa);
                listBoxInputX.Items.Add(new LongBits(i, N).ToString());
                listBoxOutputA.Items.Add(aa.ToString());
            }
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
                ChartService chartService = new ChartService(expandingForm.Chart);
                chartService.drawInputVoltageList(voltages, Color.Red, 1);
                expandingForm.Show();
            }
        }
    }
}