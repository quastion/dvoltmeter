using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalVoltmeter
{
    public partial class DigitalVoltmeterForm : Form
    {
        private MathProcessor processor;
        private ExcelTools excel;

        private LongBits[] singleCodes;
        private LongBits[] b;
        private LongBits[] a;

        public DigitalVoltmeterForm()
        {
            InitializeComponent();
            excel = new ExcelTools(progressBar);
            processor = new MathProcessor();
        }

        private void buttonSaveToExel_Click(object sender, EventArgs e)
        {
            string[] singleCodesString = singleCodes.Select(val => val.ToString()).ToArray();
            string[] bString = b.Select(val => val.ToString()).ToArray();
            string[] binaryString = processor.GetBinaryCodesFromElements(a)
                                       .Select(val => val.ToString()).ToArray();
            excel.GenerateExcel(singleCodesString, bString, binaryString);
            progressBar.Value = 0;
        }

        private void buttonGetFormules_Click(object sender, EventArgs e)
        {
            int bitsCount = int.Parse(comboBoxResistorsCount.Text);
            richTextBox.Text = string.Empty;

            singleCodes = processor.SingleCodes(bitsCount);
            LongBits[] ec = processor.GetElementsFromSingleCodes(singleCodes);
            b = processor.GetAllEPKFromEK(ec);
            string[] formules = processor.Formules(b, out a);

            for (int i = 0; i < formules.Length; i++)
                richTextBox.Text += "a" + i + " =" + formules[i] + Environment.NewLine;

            //FillRichTextBoxFromWord();
        }

        private void FillRichTextBoxFromWord()
        {
            richTextBox.Text = string.Empty;
            string rtfPath = string.Empty;
            {//переделать под статичный путь, когда подкатит экспорт формул в Word
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "MS Word documents (*.docx)|*.docx|Rich text format (*.rtf)|*.rtf";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog.FilterIndex == 1)
                        rtfPath = WordTools.GetRTFFromDOCXFile(openFileDialog.FileName);
                    else if (openFileDialog.FilterIndex == 2)
                        rtfPath = openFileDialog.FileName;
                }
            }
            richTextBox.LoadFile(rtfPath);
        }

        private void comboBoxResistorsCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}