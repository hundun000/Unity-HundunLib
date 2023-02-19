using System;
using System.Collections.Generic;
using System.Linq;

namespace hundun.unitygame.gamelib
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that replace first occurence.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the first occurence of old value replace by new value.</returns>
        public static string ReplaceFirst(this string @this, string oldValue, string newValue)
        {
            int startindex = @this.IndexOf(oldValue);

            if (startindex == -1)
            {
                return @this;
            }

            return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
        }
    }

        public static class JavaFeatureForGwt
    {
        public static String stringFormat(String format, params Object[] args)
        {

            // try type %s
            String delimiter = "%s";
            for (int i = 0; i < args.Length; i++)
            {
                format = format.ReplaceFirst(delimiter, args[i] != null ? args[i].ToString() : "null");
            }

            // try type {i}
            for (int i = 0; i < args.Length; i++)
            {
                format = format.Replace("{" + i + "}", args[i] != null ? args[i].ToString() : "null");
            }

            return format;
        }

        public static T requireNonNull<T>(T value)
        {
            if (value == null)
            {
                throw new NullReferenceException();
            }
            return value;
        }

        public static Dictionary<K, V> mapOf<K, V>(K k1, V v1)
        {
            Dictionary<K, V> map = new Dictionary<K, V>(2);
            map.Add(k1, v1);
            return map;
        }

        public static Dictionary<K, V> mapOf<K, V>(K k1, V v1, K k2, V v2)
        {
            Dictionary<K, V> map = new Dictionary<K, V>(2);
            map.Add(k1, v1);
            map.Add(k2, v2);
            return map;
        }

        public static Dictionary<K, V> mapOf<K, V>(K k1, V v1, K k2, V v2, K k3, V v3)
        {
            Dictionary<K, V> map = new Dictionary<K, V>(3);
            map.Add(k1, v1);
            map.Add(k2, v2);
            map.Add(k3, v3);
            return map;
        }

        public static Dictionary<K, V> mapOf<K, V>(K k1, V v1, K k2, V v2, K k3, V v3, K k4, V v4)
        {
            Dictionary<K, V> map = new Dictionary<K, V>(4);
            map.Add(k1, v1);
            map.Add(k2, v2);
            map.Add(k3, v3);
            map.Add(k4, v4);
            return map;
        }

        public static Dictionary<K, V> mapOf<K, V>(K k1, V v1, K k2, V v2, K k3, V v3, K k4, V v4, K k5, V v5)
        {
            Dictionary<K, V> map = new Dictionary<K, V>(5);
            map.Add(k1, v1);
            map.Add(k2, v2);
            map.Add(k3, v3);
            map.Add(k4, v4);
            map.Add(k5, v5);
            return map;
        }

        public static List<T> arraysAsList<T>(params T[] vs)
        {
            return vs.ToList();
        }

    }

    public class NumberFormat
    {
        int integerBit;
        int decimalBit;

        private NumberFormat(int integerBit, int decimalBit)
        {
            this.integerBit = integerBit;
            this.decimalBit = decimalBit;
        }

        public String format(double value)
        {
            String str = value.ToString();
            String[] parts = str.Split(".");
            String integerPart;
            String decimalPart;
            if (parts.Length == 1)
            {
                integerPart = parts[0];
                decimalPart = "";
            }
            else
            {
                integerPart = parts[0];
                decimalPart = parts[1];
            }
            while (integerPart.Length < integerBit)
            {
                integerPart = "0" + integerPart;
            }
            while (decimalPart.Length < decimalBit)
            {
                decimalPart = decimalPart + "0";
            }
            if (decimalPart.Length > decimalBit)
            {
                decimalPart = decimalPart.Substring(0, decimalBit);
            }
            if (!decimalPart.Equals(""))
            {
                decimalPart = "." + decimalPart;
            }
            return integerPart + decimalPart;
        }

        public static NumberFormat getFormat(int integerBit, int decimalBit)
        {
            NumberFormat result = new NumberFormat(decimalBit, decimalBit);
            return result;
        }
    }
}
