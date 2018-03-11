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
            if (singleCodes == null || b == null || processor == null)
                throw new Exception("Необходимо сгенерировать уравнения!");

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
            if (checkBoxOutToWord.Checked)
            {
                string wordFilePath = Environment.CurrentDirectory + "\\Formules[" + (b.Length + 1) + "].docx";
                new WordTools().createDocumentWithFormules(b,out a,wordFilePath);
                FillRichTextBoxFromWord(wordFilePath);
            }
            else
            {
                string[] formules = processor.Formules(b, out a);

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
            if(excel!=null)
                excel.Dispose();
        }
    }
}