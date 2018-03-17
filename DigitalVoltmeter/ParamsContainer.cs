using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVoltmeter
{
    /// <summary>
    /// Класс-контейнер для хранения критических параметров
    /// coeff, deltaCoeff, deltaIndex, deltaSm
    /// при которых компараторы начинают давать сбой
    /// </summary>
    class ParamsContainer
    {
        public int N { get; set; }

        public double Coeff { get; set; }

        public double DeltaCoeff { get; set; }

        public int DeltaIndex { get; set; }

        public double DeltaSM { get; set; }

        public List<LongBits> InputBinaryCode { get; set; }
        public List<LongBits> OutputBinaryCode { get; set; }

        public int[] comparatorsErrorIndexes { get; set; }
        public int[] errorIndexesFromInputAndOutputCodes { get; set; }


        public ParamsContainer(int n, double coeff, double deltaCoeff, int deltaIndex, double deltaSM)
        {
            N = n;
            Coeff = coeff;
            DeltaCoeff = deltaCoeff;
            DeltaIndex = deltaIndex;
            DeltaSM = deltaSM;
        }

        public ParamsContainer()
        {
        }
    }
}
