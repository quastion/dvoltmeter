using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVoltmeter
{
    class DACEmulator
    {
        public enum Delta { Coeff = 0, Index = 1, SM = 2 }

        public int N { get; private set; }

        public double Coeff { get; set; }

        public double DeltaCoeff { get; set; }

        public double DeltaIndex { get; set; }

        public double[] DeltaIndexes { get; private set; }

        public double DeltaSM { get; set; }

        public double QuantStep { get; private set; }

        public double RealStep { get { return IdealUin(1); } }

        public DACEmulator(int n = 8)
        {
            N = n;
            QuantStep = MaxSignal() / Math.Pow(2, N);
        }

        public DACEmulator(int n, double coeff, double deltaCoeff, double deltaIndex, double deltaSM)
        {
            N = n;
            Coeff = coeff;
            DeltaCoeff = deltaCoeff;
            DeltaIndex = deltaIndex;
            DeltaSM = deltaSM;
            QuantStep = MaxSignal() / Math.Pow(2, N);
        }

        public DACEmulator(double coeff, double deltaCoeff, double[] deltaIndexes, double deltaSM)
        {
            N = deltaIndexes.Length;
            Coeff = coeff;
            DeltaCoeff = deltaCoeff;
            DeltaIndexes = deltaIndexes;
            DeltaSM = deltaSM;
            QuantStep = MaxSignal() / Math.Pow(2, N);
        }

        /// <summary>
        /// Получить  максимальный сигнал
        /// </summary>
        /// <returns>Максимальный сигнал</returns>
        public double MaxSignal()
        {
            return Coeff * (Math.Pow(2, N) - 1) / Math.Pow(2, N - 1);
        }

        /// <summary>
        /// Получить смещение
        /// </summary>
        /// <returns>Смещение</returns>
        public double DeltaUsm()
        {
            return QuantStep / 2;
        }

        /// <summary>
        /// Получить идеальный сигнал
        /// </summary>
        /// <param name="data">Входное число</param>
        /// <returns>Идеальный сигнал</returns>
        public double IdealUin(int data)
        {
            LongBits x = new LongBits(data, N);
            double sum = 0;
            for (int i = 1; i <= N; i++)
            {
                sum += x[i - 1] * Math.Pow(2, -(N - i));
            }
            return Coeff * sum;
        }

        /// <summary>
        /// Получить сигнал учитывая отклонения
        /// </summary>
        /// <param name="data">Входное число</param>
        /// <param name="deltaIsUp">Задания знака для delta</param>
        /// <returns>Сигнал учитывая отклонения</returns>
        public double Uin(int data, bool deltaIsUp = true)
        {
            LongBits x = new LongBits(data, N);
            double sum = 0;
            for (int i = 1; i <= N; i++)
            {
                sum += x[i - 1] * Math.Pow(2, -(N - i + (DeltaIndexes == null ? DeltaIndex : DeltaIndexes[i])));
            }
            double delta = deltaIsUp ? DeltaSM : -DeltaSM;
            return (Coeff + DeltaCoeff) * sum + (DeltaUsm() + delta);
        }

        /// <summary>
        /// Получить позиционный код
        /// </summary>
        /// <param name="data">Входное число</param>
        /// <returns>Позиционный код</returns>
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

        /// <summary>
        /// Получить бинарный код
        /// </summary>
        /// <param name="data">Входное число</param>
        /// <returns>Бинарный код</returns>
        public LongBits GetDKFromComparators(int data)
        {
            LongBits simpleCode = GetEKFromComparators(data);
            LongBits simplePositionCode = MathProcessor.GetEPKFromEK(simpleCode);
            return MathProcessor.GetDK(simplePositionCode);
        }

        /// <summary>
        /// Получить индексы битов с ошибками в последовательном позиционном коде
        /// </summary>
        /// <param name="data">Входное число</param>
        /// <returns>Индексы битов с ошибками в последовательном позиционном коде</returns>
        public int[] GetEKPErrorFromComparators(int data)
        {
            int m = (int)Math.Pow(2, N) - 1;
            LongBits idealSimpleCode = MathProcessor.GetEK(data, m);
            LongBits idealSimplePositionCode = MathProcessor.GetEPKFromEK(idealSimpleCode);

            LongBits realSimpleCode = GetEKFromComparators(data);
            LongBits realSimplePositionCode = MathProcessor.GetEPKFromEK(realSimpleCode);

            List<int> errors = new List<int>();
            for (int i = 0; i < realSimpleCode.Length; i++)
            {
                if (idealSimplePositionCode[i] != realSimplePositionCode[i])
                    errors.Add(i + 1);
            }
            return errors.ToArray();
        }

        public static ParamsContainer TestingDelta(int n, double coeff, Delta delta, double initialStep = 1, double accuracy = 0.0001)
        {
            double deltaCoeff = 0;
            double deltaIndex = 0;
            double deltaSM = 0;
            int countNumbers = (int)Math.Pow(2, n);
            List<int> indexes = new List<int>();
            ParamsContainer container = new ParamsContainer(n, coeff, deltaCoeff, deltaIndex, deltaSM);
            LongBits inputBinaryCode = new LongBits(0, n), outputBinaryCode = inputBinaryCode;
            DACEmulator emulator = new DACEmulator(n, coeff, deltaCoeff, deltaIndex, deltaSM);
            while (Math.Abs(initialStep * 2) > accuracy)
            {
                inputBinaryCode = outputBinaryCode;
                while (inputBinaryCode == outputBinaryCode)
                {
                    emulator.DeltaCoeff = deltaCoeff;
                    emulator.DeltaIndex = deltaIndex;
                    emulator.DeltaSM = deltaSM;
                    for (int x = 0; x < countNumbers; x++)
                    {
                        inputBinaryCode = new LongBits(x, n);
                        outputBinaryCode = emulator.GetDKFromComparators(x);
                        if (inputBinaryCode != outputBinaryCode)
                            break;
                    }
                    if (inputBinaryCode == outputBinaryCode)
                    { 
                        if (delta == Delta.Coeff) deltaCoeff += initialStep;
                        else if (delta == Delta.Index) deltaIndex += initialStep;
                        else if (delta == Delta.SM) deltaSM += initialStep;
                    }
                }
                if (delta == Delta.Coeff) deltaCoeff -= initialStep;
                else if (delta == Delta.Index) deltaIndex -= initialStep;
                else if (delta == Delta.SM) deltaSM -= initialStep;
                initialStep /= 2;
            }

            for (int x = 0; x < countNumbers; x++)
            {
                indexes.AddRange(emulator.GetEKPErrorFromComparators(x).ToList());
                inputBinaryCode = new LongBits(x, n);
                outputBinaryCode = emulator.GetDKFromComparators(x);
                if (inputBinaryCode != outputBinaryCode)
                {
                    container.ErrorIndexesFromInputAndOutputCodes.Add(x);
                }
                container.InputBinaryCodes.Add(inputBinaryCode);
                container.OutputBinaryCodes.Add(outputBinaryCode);
            }

            container.DeltaCoeff = emulator.DeltaCoeff;
            container.DeltaIndex = emulator.DeltaIndex;
            container.DeltaSM = emulator.DeltaSM;
            container.ComparatorsErrorIndexes = indexes.Distinct().ToArray();
            return container;
        }
    }
}
