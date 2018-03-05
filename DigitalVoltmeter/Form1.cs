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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MathProcessor processor = new MathProcessor();
            textBox1.Text += processor.GetN(8) + Environment.NewLine + Environment.NewLine;
            long[] singleCodes = processor.SingleCodes(8);
            long[] e = processor.GetElementsFromSingleCodes(singleCodes);
            long[] b = processor.GetAllBFromE(e);
            long[] a = processor.GetA(b);
            for (int i = 0; i < a.Length; i++)
                textBox1.Text += Convert.ToString(a[i], 2) + Environment.NewLine;
            string[] formules = processor.Formules(b);
            for (int i = 0; i < formules.Length; i++)
                textBox1.Text += formules[i] + Environment.NewLine;
        }
    }
}
