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
            excel = new ExcelTools(this.progressBar);
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
            int bitsCount = int.Parse(this.comboBoxResistorsCount.Text);
            this.textBox.Text = String.Empty;

            singleCodes = processor.SingleCodes(bitsCount);
            LongBits[] ec = processor.GetElementsFromSingleCodes(singleCodes);
            b = processor.GetAllBFromE(ec);
            a = processor.GetA(b);
            string[] formules = processor.Formules(b);
            for (int i = 0; i < formules.Length; i++)
                textBox.Text += "a" + i + "=" + formules[i] + Environment.NewLine;
        }

        private void comboBoxResistorsCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}