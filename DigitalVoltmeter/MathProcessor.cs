using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalVoltmeter
{
    class MathProcessor
    {
        /// <summary>
        /// Получить количество разрядов
        /// </summary>
        /// <param name="value">Количество резисторов</param>
        /// <returns>Количество разрядов</returns>
        public int GetN(int value)
        {
            return (int)Math.Ceiling(Math.Log(value, 2));
        }

        /// <summary>
        /// Преобразовать десятичное число в ЕК
        /// </summary>
        /// <param name="value">Десятичное число</param>
        /// <returns>ЕК</returns>
        public LongBits SingleCode(int value, int count)
        {
            LongBits singleCode = new LongBits(count);
            for (int i = 0; i < value; i++)
                singleCode[i] = 1;
            return singleCode;
        }

        /// <summary>
        /// Получить массив ЕК
        /// </summary>
        /// <param name="value">Верхняя граница</param>
        /// <returns>Массив ЕК</returns>
        public LongBits[] SingleCodes(int value)
        {
            LongBits[] singleCodes = new LongBits[value];
            for (int i = 0; i < value; i++)
                singleCodes[i] = SingleCode(i, value);
            return singleCodes;
        }

        /// <summary>
        /// Получение состовляющей ei, где i=numBit
        /// </summary>
        /// <param name="singleCodes">Массив ЕК</param>
        /// <param name="numBit">Номер состовляющей</param>
        /// <returns>i-я состовляющая</returns>
        public LongBits GetElementFromSingleCode(LongBits[] singleCodes, int numBit)
        {
            LongBits e = new LongBits(singleCodes.Length);
            for (int i = 0; i < singleCodes.Length; i++)
                e[i] = singleCodes[i][numBit];
            return e;
        }

        /// <summary>
        /// Разбить массив на отдельные состовляющие e1, ... , en
        /// </summary>
        /// <param name="singleCodes">Массив ЕК</param>
        /// <returns>Массив состовляющих</returns>
        public LongBits[] GetElementsFromSingleCodes(LongBits[] singleCodes)
        {
            LongBits[] e = new LongBits[singleCodes.Length - 1];
            for (int i = 0; i < e.Length; i++)
            {
                e[i] = GetElementFromSingleCode(singleCodes, i);
            }
            return e;
        }

        /// <summary>
        /// Получение массива состовляющих ЕПК
        /// </summary>
        /// <param name="e">Массив состовляющих ЕК</param>
        /// <returns>Массив состовляющих ЕПК</returns>
        public LongBits[] GetAllBFromE(LongBits[] e)
        {
            LongBits[] b = new LongBits[e.Length];
            for (int i = 0; i < b.Length; i++)
                b[i] = e[i] & ~(i == b.Length - 1 ? new LongBits(e[0].Length) : e[i + 1]);
            return b;
        }

        /// <summary>
        /// Вычисление ДК
        /// </summary>
        /// <param name="b">Массив состовляющих ЕПК</param>
        /// <returns>ДК</returns>
        public LongBits[] GetA(LongBits[] b)
        {
            int n = GetN(b.Length + 1);
            LongBits[] a = new LongBits[n];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = new LongBits(b[0].Length);
                a[i] = ~a[i];
                int kmax = (int)Math.Pow(2, n - 1 - i) - 1;
                for (int k = 0; k <= kmax; k++)
                {
                    int tmin = (int)Math.Pow(2, i) * (2 * k + 1);
                    int tmax = (int)Math.Pow(2, i + 1) * (k + 1) - 1;
                    for (int t = tmin; t <= tmax; t++)
                    {
                        a[i] &= ~b[t - 1];
                    }
                }
                a[i] = ~a[i];
            }
            return a;
        }

        public LongBits[] GetBinaryCodesFromElements(LongBits[] a)
        {
            LongBits[] binaryCodes = new LongBits[a[0].Length];
            for (int i = 0; i < binaryCodes.Length; i++)
            {
                binaryCodes[i] = new LongBits(a.Length);
                for (int j = 0; j < a.Length; j++)
                {
                    binaryCodes[i][j] = a[j][i];
                }
            }
            return binaryCodes;
        }

        /// <summary>
        /// Вывод формулы
        /// </summary>
        /// <param name="b">Массив состовляющих ЕПК</param>
        /// <returns>ДК</returns>
        public string[] Formules(LongBits[] b)
        {
            int n = GetN(b.Length + 1);
            LongBits[] a = new LongBits[n];

            string[] formules = new string[n];

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = new LongBits(b[0].Length);
                a[i] = ~a[i];
                string withoutDigit = string.Empty;
                string withDigit = string.Empty;
                int kmax = (int)Math.Pow(2, n - 1 - i) - 1;
                for (int k = 0; k <= kmax; k++)
                {
                    int tmin = (int)Math.Pow(2, i) * (2 * k + 1);
                    int tmax = (int)Math.Pow(2, i + 1) * (k + 1) - 1;
                    for (int t = tmin; t <= tmax; t++)
                    {
                        withoutDigit += "¬b" + t;
                        withDigit += "¬" + b[t - 1];

                        a[i] &= ~b[t - 1];

                        if (k != kmax || t != tmax)
                        {
                            withoutDigit += "ʌ";
                            withDigit += "ʌ";
                        }

                    }
                }
                a[i] = ~a[i];
                formules[i] = "¬(" + withoutDigit + ")=" + "¬(" + withDigit + ")=" + a[i];
            }
            return formules;
        }
    }
}
