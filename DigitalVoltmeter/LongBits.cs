using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVoltmeter
{
    public class LongBits
    {
        private StringBuilder bitsString;

        public LongBits(string longBits)
        {
            if (!longBits.All(val => val == '0' || val == '1'))
                throw new ArgumentException();
            bitsString = new StringBuilder(longBits);
        }

        public LongBits(int countBits)
        {
            if (countBits < 1)
                throw new ArgumentException();
            bitsString = new StringBuilder();
            for (int i = 0; i < countBits; i++)
                bitsString.Append("0");
        }

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= bitsString.Length)
                    throw new IndexOutOfRangeException();
                return (int)Char.GetNumericValue(bitsString[bitsString.Length - 1 - index]);
            }
            set
            {
                if (index < 0 || index >= bitsString.Length)
                    throw new IndexOutOfRangeException();
                if (value != 0 && value != 1)
                    throw new ArgumentException();
                bitsString[bitsString.Length - 1 - index] = (value == 0) ? '0' : '1';
            }
        }

        public int Length
        {
            get { return bitsString.Length; }
        }

        public static LongBits operator >>(LongBits bits, int count)
        {
            LongBits newbits = new LongBits(bits.bitsString.ToString(0, bits.Length - count));
            for (int i = 0; i < count; i++)
                newbits.bitsString.Insert(0, '0');
            return newbits;
        }

        public static LongBits operator <<(LongBits bits, int count)
        {
            LongBits newbits = new LongBits(bits.bitsString.ToString(count, bits.Length - count));
            for (int i = 0; i < count; i++)
                newbits.bitsString.Append('0');
            return newbits;
        }

        public static LongBits operator ~(LongBits bits)
        {
            LongBits newbits = new LongBits(bits.ToString());
            for (int i = 0; i < newbits.Length; i++)
                newbits[i] = (newbits[i] == 0) ? 1 : 0;
            return newbits;
        }

        public static LongBits operator &(LongBits first, LongBits second)
        {
            LongBits newbits = new LongBits(first.ToString());
            for (int i = 0; i < newbits.Length; i++)
                newbits[i] = (newbits[i] == 1 && second[i] == 1) ? 1 : 0;
            return newbits;
        }

        public static LongBits operator |(LongBits first, LongBits second)
        {
            LongBits newbits = new LongBits(first.ToString());
            for (int i = 0; i < newbits.Length; i++)
                newbits[i] = (newbits[i] == 1 || second[i] == 1) ? 1 : 0;
            return newbits;
        }

        public override string ToString()
        {
            return bitsString.ToString();
        }
    }
}
