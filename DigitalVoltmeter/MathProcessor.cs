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
        public long SingleCode(int value)
        {
            long singleCode = 0;
            for (int i = 0; i < value; i++)
                singleCode += 1 << i;
            return singleCode;
        }

        /// <summary>
        /// Получить массив ЕК
        /// </summary>
        /// <param name="value">Верхняя граница</param>
        /// <returns>Массив ЕК</returns>
        public long[] SingleCodes(int value)
        {
            long[] singleCodes = new long[value];
            for (int i = 0; i < value; i++)
                singleCodes[i] = SingleCode(i);
            return singleCodes;
        }

        /// <summary>
        /// Получение состовляющей ei, где i=numBit
        /// </summary>
        /// <param name="singleCodes">Массив ЕК</param>
        /// <param name="numBit">Номер состовляющей</param>
        /// <returns>i-я состовляющая</returns>
        public long GetElementFromSingleCode(long[] singleCodes, int numBit)
        {
            long e = 0;
            for (int i = 0; i < singleCodes.Length; i++)
            {
                e += ((singleCodes[i] & (1 << numBit)) >> numBit) << i;
            }
            return e;
        }

        /// <summary>
        /// Разбить массив на отдельные состовляющие e1, ... , en
        /// </summary>
        /// <param name="singleCodes">Массив ЕК</param>
        /// <returns>Массив состовляющих</returns>
        public long[] GetElementsFromSingleCodes(long[] singleCodes)
        {
            Array.Sort(singleCodes);
            long[] e = new long[singleCodes.Length - 1];
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
        public long[] GetAllBFromE(long[] e)
        {
            long[] b = new long[e.Length];
            for (int i = 0; i < b.Length; i++)
                b[i] = e[i] & ~(i == b.Length - 1 ? 0 : e[i + 1]);
            return b;
        }

        /// <summary>
        /// Вычисление ДК
        /// </summary>
        /// <param name="b">Массив состовляющих ЕПК</param>
        /// <returns>ДК</returns>
        public long[] GetA(long[] b)
        {
            int n = GetN(b.Length + 1);
            long[] a = new long[n];
            for (int i = 0; i < a.Length; i++)
            {
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

        /// <summary>
        /// Вывод формулы
        /// </summary>
        /// <param name="b">Массив состовляющих ЕПК</param>
        /// <returns>ДК</returns>
        public string[] Formules(long[] b)
        {
            int n = GetN(b.Length + 1);
            long[] a = new long[n];

            string[] formules = new string[n]; 

            for (int i = 0; i < a.Length; i++)
            {
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
                        withDigit += "¬" + Convert.ToString(b[t - 1], 2);

                        a[i] &= ~b[t - 1];

                        if (k != kmax || t != tmax)
                        {
                            withoutDigit += "⋀";
                            withDigit += "⋀";
                        }

                    }
                }
                a[i] = ~a[i];
                formules[i] = "¬(" + withoutDigit + ")=" + "¬(" + withDigit + ")=" + Convert.ToString(a[i], 2);
            }
            return formules;
        }
    }
}
