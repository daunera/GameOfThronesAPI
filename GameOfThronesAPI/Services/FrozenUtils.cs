using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesAPI.Services
{
    public class FrozenUtils
    {
        /// <summary>
        /// Make a string with ',' separator from a string array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string GetArrayInString(string[] array)
        {
            string title = "";
            if (array == null)
                return title;
            foreach (string item in array)
            {
                title += item;
                if (item != array.GetValue(array.Length - 1).ToString())
                    title += ", ";
            }
            return title;
        }

    }
}
