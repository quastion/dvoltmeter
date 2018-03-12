using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVoltmeter
{
    class DACEmulator
    {
        public int N { get; private set; }

        public int Coeff { get; set; }

        public int DeltaCoeff { get; set; }

        public int DeltaIndex { get; set; }

        public double DeltaSM { get; set; }

        public double QuantStep { get; private set; }

        public DACEmulator(int n = 8)
        {
            N = n;
            QuantStep = MaxSignal() / Math.Pow(2, N);
        }

        public DACEmulator(int n, int coeff, int deltaCoeff, int deltaIndex, double deltaSM)
        {
            N = n;
            Coeff = coeff;
            DeltaCoeff = deltaCoeff;
            DeltaIndex = deltaIndex;
            DeltaSM = deltaSM;
            QuantStep = MaxSignal() / Math.Pow(2, N);
        }

        public double MaxSignal()
        {
            return Coeff * (Math.Pow(2, N) - 1) / Math.Pow(2, N - 1);
        }

        public double DeltaUsm()
        {
            return QuantStep / 2;
        }

        public double Uin(int data, bool deltaIsUp = true)
        {
            LongBits x = new LongBits(data, N);
            double sum = 0;
            for (int i = 1; i <= N; i++)
            {
                sum += x[i - 1] * Math.Pow(2, -(N - i + DeltaIndex));
            }
            double delta = deltaIsUp ? DeltaSM : -DeltaSM;
            return (Coeff + DeltaCoeff) * sum + (DeltaUsm() + delta);
        }

        public LongBits GetEKFromComparators(int data)
        {
            int m = (int)Math.Pow(2, N) - 1;
            LongBits e = new LongBits(m);

            double deltaUsm = DeltaUsm();
            double uop = MaxSignal();
            double uin = Uin(data);
            for (int t = 1; t <= m; t++)
            {
                e[t - 1] = (uin + deltaUsm >= ((double)t / m) * uop) ? 1 : 0;
            }
            return e;
        }
    }
}
