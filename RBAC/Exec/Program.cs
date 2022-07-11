using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exec
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] arr = { "1", "2", "3", "4" };
            int[] arr1 = { };
            int[] arr2 = arr.Select<string, int>(m=>int.Parse(m)).ToArray();
            foreach (string item in arr)
            {
                arr1.Append(Convert.ToInt32(item));
            }
            //for (int j = 0; j < arr.Length; j++)
            //{
            //    arr1.Append<>
            //}

            //字符串数组（源数组）
            string[] sNums = new[] { "1", "2" };

            //整型数组（目标数组）
            int[] iNums;

            //转换方法
            iNums = Array.ConvertAll<string, int>(sNums, s => int.Parse(s));

            //转换方法-简写
            iNums = Array.ConvertAll<string, int>(sNums, int.Parse);

            //转换方法-继续简写
            iNums = Array.ConvertAll(sNums, int.Parse);

            foreach (int item in arr1)
            {
                Console.Write(item + " ");
            }
            Console.ReadLine();
        }
    }
}
