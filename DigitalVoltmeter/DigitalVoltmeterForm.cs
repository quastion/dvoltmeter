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

        private long[] singleCodes;
        private long[] b;
        private int bitsCount;

        public DigitalVoltmeterForm()
        {
            InitializeComponent();
            excel = new ExcelTools(this.progressBar);
        }

        private void buttonSaveToExel_Click(object sender, EventArgs e)
        {
            string[] singleCodesString = singleCodes.Select(a => processor.PrettyPrintBits(a, bitsCount)).ToArray();
            string[] bString = b.Select(a => processor.PrettyPrintBits(a, bitsCount)).ToArray();
            string[] BinaryCodes = new string[bitsCount];
            for (int i = 0; i < BinaryCodes.Length; i++)
                BinaryCodes[i] = processor.PrettyPrintBits(i, processor.GetN(bitsCount));
            excel.GenerateExcel(singleCodesString, bString, BinaryCodes);
            progressBar.Value = 0;
        }

        private void buttonGetFormules_Click(object sender, EventArgs e)
        {
            bitsCount = int.Parse(this.comboBoxResistorsCount.Text);
            this.textBox1.Text = String.Empty;
            processor = new MathProcessor();
            singleCodes = processor.SingleCodes(bitsCount);
            long[] ec = processor.GetElementsFromSingleCodes(singleCodes);
            b = processor.GetAllBFromE(ec);
            long[] a = processor.GetA(b);
            string[] formules = processor.Formules(b);
            for (int i = 0; i < formules.Length; i++)
                textBox1.Text += "a" + i + "=" + formules[i] + Environment.NewLine;
        }

        private void comboBoxResistorsCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}