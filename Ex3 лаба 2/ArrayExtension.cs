using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ex3
{
    internal static class ArrayExtension
    {

        public delegate int _comparerDelegate<in T>(T x, T y);

        #region Enums
        public enum Order
        {
            Ascending = 0,
            Descending=1
        };
        public enum Algorithm
        {
            Inserts=0, 
            Selection=1, 
            Heap=2, 
            Quick=3, 
            Merge=4
        };
        #endregion

        #region Public methods
        public static void SortExtend<T>(this ICollection<T> _, ref List<T> values, Order sorting, Algorithm algorithm, IComparer comparer)
        {
            SortExtend(_, ref values, sorting, algorithm, comparer.Compare);
        }
        public static void SortExtend<T>(this ICollection<T> _, ref List<T> values, Order sorting, Algorithm algorithm, Comparer comparer)
        {
            SortExtend(_, ref values, sorting, algorithm, comparer.Compare);
        }
        public static void SortExtend<T>(this ICollection<T> _, ref List<T> values, Order sorting, Algorithm algorithm, Program.comparisonDelegate<T> comparer)
        {
            switch (algorithm)
            {
                case Algorithm.Inserts:
                    SortDataInserts(ref values, sorting, comparer);
                    break;

                case Algorithm.Selection:
                    SortDataSelection(ref values, sorting, comparer);
                    break;

                case Algorithm.Heap:
                    SortDataHeap(ref values, sorting, comparer);
                    break;

                case Algorithm.Quick:
                    SortDataQuick(ref values, sorting, comparer);
                    break;

                case Algorithm.Merge:
                    SortDataMerge(ref values, sorting, comparer);
                    break;
            }
        }

        #endregion

        #region Sorting methods
        private static void SortDataInserts<T>(ref List<T> values, Order sorting, Program.comparisonDelegate<T> Compare)
        {
            int n = values.Count;
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j >0; j--)
                {
                    if (LowerThen(values[j], values[j - 1], sorting, Compare))
                    {
                        var swipe = values[j];
                        values[j] = values[j - 1];
                        values[j - 1] = swipe;
                    }
                }
            }
        }

        private static void SortDataSelection<T>(ref List<T> values, Order sorting, Program.comparisonDelegate<T> Compare)
        {
            int n = values.Count;
            int recordNumber = 0;


            while (recordNumber < n)
            {
                var findValue = values[recordNumber];
                int findValueIndex = recordNumber;

                for (int i = recordNumber; i < n; i++)
                {

                    if (LowerThen(values[i], findValue, sorting, Compare))
                    {
                        findValue = values[i];
                        findValueIndex = i;
                    }
                }

                var swipe = findValue;
                values[findValueIndex] = values[recordNumber];
                values[recordNumber] = swipe;
                recordNumber++;
            }
        }

        private static void SortDataHeap<T>(ref List<T> values, Order sorting, Program.comparisonDelegate<T> Compare)
        {
            int heapSize = values.Count;

            for (int p = (heapSize - 1) / 2; p >= 0; --p)
                SortDataHeap_Max(ref values, heapSize, p, sorting, Compare);

            for (int i = values.Count - 1; i > 0; --i)
            {
                var temp = values[i];
                values[i] = values[0];
                values[0] = temp;

                --heapSize;
                SortDataHeap_Max(ref values, heapSize, 0, sorting, Compare);
            }
        }

        private static void SortDataQuick<T>(ref List<T> values, Order sorting, Program.comparisonDelegate<T> Compare)
        {
            SortDataQuick_Sort(values, 0, values.Count-1, sorting, Compare);  
        }

        private static void SortDataMerge<T>(ref List<T> values, Order sorting, Program.comparisonDelegate<T> Compare)
        {
            List<List<T>> array = SortDataMerge_ReArrays(values);

            while (array.Count!=1) {
                List<List<T>> newArray = new List<List<T>>();
                for (int i = 0; i < array.Count - 1; i += 2)
                {
                    if (SortDataMerge_LowerThen(array[i], array[i + 1], sorting, Compare))
                    {
                        newArray.Add(SortDataMerge_UnionArrays(array[i], array[i + 1]));
                    }
                    else
                    {
                        newArray.Add(SortDataMerge_UnionArrays(array[i + 1], array[i]));
                    }
                }

                array = new List<List<T>>();
                array.AddRange(newArray);
            }

            values = new List<T>();
            values.AddRange(array[0]);
        }

        public static List<T> SortDataQuick_Sort<T>(List<T> values, int leftIndex, int rightIndex, Order sorting, Program.comparisonDelegate<T> Compare)
        {
            var i = leftIndex;
            var j = rightIndex;
            var p = values[leftIndex];
            while (i <= j)
            {
                while (LowerThen(values[i], p, sorting, Compare)) { i++; }
                while (LowerThen(p, values[j], sorting, Compare)) { j--; }

                if (i <= j)
                {
                    var temp = values[i];
                    values[i] = values[j];
                    values[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j) SortDataQuick_Sort(values, leftIndex, j, sorting, Compare);
            if (i < rightIndex) SortDataQuick_Sort(values, i, rightIndex, sorting, Compare);
            return values;
        }

        private static void SortDataHeap_Max<T>(ref List<T> data, int heapSize, int index, Order sorting, Program.comparisonDelegate<T> Compare)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;

            if (left < heapSize && LowerThen(data[index], data[left], sorting, Compare)) largest = left;
            else largest = index;

            if (right < heapSize && LowerThen(data[largest], data[right], sorting, Compare)) largest = right;

            if (largest != index)
            {
                var temp = data[index];
                data[index] = data[largest];
                data[largest] = temp;

                SortDataHeap_Max(ref data, heapSize, largest, sorting, Compare);
            }
        }
        #endregion

        #region Helpers methods

        private static List<List<T>> SortDataMerge_ReArrays<T>(List<T> values)
        {
            List<List<T>> array = new List<List<T>>();
            for (int i=0; i<values.Count; i++)
            {
                array.Add(new List<T>() { values[i] });
            }
            return array;
        }

        private static List<T> SortDataMerge_UnionArrays<T>(List<T> list1, List<T> list2)
        {
            List<T> array = new List<T>();
           
            array.AddRange(list1);
            array.AddRange(list2);

            return array;
        }

        private static bool SortDataMerge_LowerThen<T>(List<T> values1, List<T> values2, Order sorting, Program.comparisonDelegate<T> Compare)
        {
            bool smallest = true;
            for (int i=0; i<values1.Count; i++)
            {

                smallest = smallest && LowerThen(values1[i], values2[i], sorting, Compare);//LowerThen(data1, data2, sorting, Compare);
                
            }
            return smallest;

        }

        private static bool LowerThen<T>(T value1, T value2, Order sorting, Program.comparisonDelegate<T> Compare)
        {
            bool compRes = Compare(value1, value2) < 0;
            return sorting==Order.Ascending? compRes : !compRes;
        }

        #endregion
    }

}
