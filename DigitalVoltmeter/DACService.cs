using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVoltmeter
{
    class DACService
    {
        DACEmulator Emulator { get; set; }

        public DACService() { }

        public DACService(DACEmulator emulator)
        {
            Emulator = emulator;
        }

        public double LimitDeltaCoeff(int input, double threshold = 0.001)
        {
            double oldDeltaCoeff = Emulator.DeltaCoeff;
            Emulator.DeltaCoeff = Emulator.Coeff;
            LongBits outputBinaryCode = Emulator.GetDKFromComparators(input);
            LongBits inputBinaryCode = new LongBits(input, Emulator.N);
            double k = 1.1;
            while (inputBinaryCode != outputBinaryCode)
            {
                if (Emulator.DeltaCoeff > 0 && Emulator.DeltaCoeff < threshold)
                    Emulator.DeltaCoeff = -Emulator.Coeff;
                else if (Emulator.DeltaCoeff < 0 && Emulator.DeltaCoeff > -threshold) return 0;

                Emulator.DeltaCoeff /= k;
                outputBinaryCode = Emulator.GetDKFromComparators(input);
            }
            double critical = Emulator.DeltaCoeff;
            Emulator.DeltaCoeff = oldDeltaCoeff;
            return critical;
        }

        public double LimitDeltaSM(int input, double threshold = 0.001)
        {
            double oldDeltaSM = Emulator.DeltaSM;
            Emulator.DeltaSM = Emulator.DeltaUsm();
            LongBits outputBinaryCode = Emulator.GetDKFromComparators(input);
            LongBits inputBinaryCode = new LongBits(input, Emulator.N);
            double k = 1.1;
            while (inputBinaryCode != outputBinaryCode)
            {
                if (Emulator.DeltaSM > 0 && Emulator.DeltaSM < threshold)
                    Emulator.DeltaSM = -Emulator.DeltaUsm();
                else if (Emulator.DeltaSM < 0 && Emulator.DeltaSM > -threshold) return 0;

                Emulator.DeltaSM /= k;
                outputBinaryCode = Emulator.GetDKFromComparators(input);
            }
            double critical = Emulator.DeltaSM;
            Emulator.DeltaSM = oldDeltaSM;
            return critical;
        }

        public double LimitDeltaIndex(int input, double threshold = 0.001)
        {
            double oldIndex = Emulator.DeltaIndex;
            Emulator.DeltaIndex = Emulator.N;
            LongBits outputBinaryCode = Emulator.GetDKFromComparators(input);
            LongBits inputBinaryCode = new LongBits(input, Emulator.N);
            double k = 1.1;
            while (inputBinaryCode != outputBinaryCode)
            {
                if (Emulator.DeltaIndex > 0 && Emulator.DeltaIndex < threshold)
                    Emulator.DeltaIndex = -Emulator.N;
                else if (Emulator.DeltaIndex < 0 && Emulator.DeltaIndex > -threshold) return 0;

                Emulator.DeltaIndex /= k;
                outputBinaryCode = Emulator.GetDKFromComparators(input);
            }
            double critical = Emulator.DeltaIndex;
            Emulator.DeltaIndex = oldIndex;
            return critical;
        }

        public double LimitDeltaIndexes(int input, double threshold = 0.001)
        {
            double oldIndex = Emulator.DeltaIndex;
            Emulator.DeltaIndex = Emulator.N;
            LongBits outputBinaryCode = Emulator.GetDKFromComparators(input);
            LongBits inputBinaryCode = new LongBits(input, Emulator.N);
            double k = 1.1;
            while (inputBinaryCode != outputBinaryCode)
            {
                if (Emulator.DeltaIndex > 0 && Emulator.DeltaIndex < threshold)
                    Emulator.DeltaIndex = -Emulator.N;
                else if (Emulator.DeltaIndex < 0 && Emulator.DeltaIndex > -threshold) return 0;

                Emulator.DeltaIndex /= k;
                outputBinaryCode = Emulator.GetDKFromComparators(input);
            }
            double critical = Emulator.DeltaIndex;
            Emulator.DeltaIndex = oldIndex;
            return critical;
        }
    }
}
