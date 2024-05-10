using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ex
{
    public static class Debug
    {
        public static void Print<T>(T msg)
        {
#if true
            UnityEngine.Debug.Log(msg);
#else
            return;
#endif
        }

        public static void Show<T>(this T self)
        {
            Print(self);
        }
    }

    public static class Math
    {
        /// <summary>
        /// Return "true" if the distance between "a" and "b" is larger than or equal to "d", else "false";
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="d"></param>
        public static bool CompareDistance(Vector3 a, Vector3 b, float d)
        {
            return (a - b).sqrMagnitude >= Mathf.Pow(d, 2);
        }

        public static float Cos(this float theta)
        {
            return Mathf.Cos(theta);
        }

        public static float Sin(this float theta)
        {
            return Mathf.Sin(theta);
        }

        public static float Tan(this float theta)
        {
            return Mathf.Tan(theta);
        }

        public static float Acos(this float theta)
        {
            return Mathf.Acos(theta);
        }

        public static float Asin(this float theta)
        {
            return Mathf.Asin(theta);
        }

        public static float Atan(this float theta)
        {
            return Mathf.Atan(theta);
        }

        public static float Atan2(this Vector2 vec)
        {
            return Mathf.Atan2(vec.y, vec.x);
        }

        public static float Pow(this float _base, float exponent)
        {
            return Mathf.Pow(_base, exponent);
        }
    }

    public static class Random
    {
        public static bool RandomBool()
        {
            return RandInt(0, 1) == 0;
        }

        public static int RandInt(int min, int max)
        {
            return UnityEngine.Random.Range(min, max + 1);
        }

        public static float RandFloat(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public static Vector2 RandSquare(float sizeX, float sizeY)
        {
            return new(RandFloat(0, sizeX), RandFloat(0, sizeY));
        }

        public static Vector2Int RandSquare(int sizeX, int sizeY)
        {
            return new(RandInt(0, sizeX), RandInt(0, sizeY));
        }

        /// <param name="sizeR">sizeR >= 0</param>
        public static Vector2 RandCircle(float sizeR)
        {
            float theta = RandFloat(0, 2 * Mathf.PI);
            return new Vector2(theta.Cos(), theta.Sin()) * sizeR;
        }

        public static Vector3 RandCube(float sizeX, float sizeY, float sizeZ)
        {
            return new(RandFloat(0, sizeX), RandFloat(0, sizeY), RandFloat(0, sizeZ));
        }

        public static Vector3Int RandCube(int sizeX, int sizeY, int sizeZ)
        {
            return new(RandInt(0, sizeX), RandInt(0, sizeY), RandInt(0, sizeZ));
        }

        /// <param name="sizeR">sizeR >= 0</param>
        public static Vector3 RandSphere(float sizeR)
        {
            float theta = RandFloat(0, Mathf.PI);
            float phi = RandFloat(0, 2 * Mathf.PI);
            return new Vector3(theta.Sin() * phi.Cos(), theta.Sin() * phi.Sin(), theta.Cos()) * sizeR;
        }
    }

    public static class Collection
    {
        public static List<int> Range(int stop)
        {
            List<int> ret = new();
            for (int i = 0; i < stop; i += 1)
            {
                ret.Add(i);
            }
            return ret;
        }

        public static List<int> Range(int start, int stop)
        {
            List<int> ret = new();
            for (int i = start; i < stop; i += 1)
            {
                ret.Add(i);
            }
            return ret;
        }

        public static List<int> Range(int start, int stop, int step)
        {
            List<int> ret = new();
            for (int i = start; i < stop; i += step)
            {
                ret.Add(i);
            }
            return ret;
        }

        public static List<(int, T)> Enumerate<T>(this List<T> self)
        {
            List<(int, T)> ret = new();
            for (int i = 0; i < self.Count; i++)
            {
                ret.Add((i, self[i]));
            }
            return ret;
        }

        public static List<T> Shuffle<T>(this List<T> self)
        {
            int n = self.Count;
            List<T> ret = new(self);

            for (int i = n - 1; i >= 1; i--)
            {
                int j = Random.RandInt(0, i);
                (ret[i], ret[j]) = (ret[j], ret[i]);
            }

            return ret;
        }

        public static T RandomChoice<T>(this List<T> self)
        {
            return self[Random.RandInt(0, self.Count - 1)];
        }

        public static void Look<T>(this List<T> self)
        {
            string start = "List(";
            string end = ")";
            string middle = "";

            foreach ((int, T) e in Enumerate(self))
            {
                if (e.Item1 == 0)
                {
                    middle += $"{e.Item2}";
                }
                else
                {
                    middle += $", {e.Item2}";
                }
            }

            Debug.Show(start + middle + end);
        }

        public static void Look<T1, T2>(this List<T1> self, Func<T1, T2> func)
        {
            string start = "List(";
            string end = ")";
            string middle = "";

            foreach ((int, T1) e in Enumerate(self))
            {
                if (e.Item1 == 0)
                {
                    middle += $"{func(e.Item2)}";
                }
                else
                {
                    middle += $", {func(e.Item2)}";
                }
            }

            Debug.Show(start + middle + end);
        }

        public static List<(T1, T2)> Zip<T1, T2>(List<T1> list1, List<T2> list2)
        {
            List<(T1, T2)> ret = new();
            int len = Mathf.Min(list1.Count, list2.Count);
            foreach (int i in Range(len))
            {
                ret.Add((list1[i], list2[i]));
            }

            return ret;
        }

        public static uint Count(this List<bool> self, bool val)
        {
            uint num = 0;

            foreach (bool e in self)
            {
                if (e == val)
                {
                    num++;
                }
            }

            return num;
        }

        public static bool All(List<bool> list)
        {
            foreach (bool e in list)
            {
                if (e != true)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Any(List<bool> list)
        {
            foreach (bool e in list)
            {
                if (e == true)
                {
                    return true;
                }
            }

            return false;
        }

        public static List<T2> Make<T1, T2>(Func<T1, T2> func, List<T1> collection)
        {
            List<T2> ret = new();
            foreach (T1 e in collection)
            {
                ret.Add(func(e));
            }

            return ret;
        }

        public static List<T2> Make<T1, T2>(Func<T1, T2> func, Func<T1, bool> condition, List<T1> collection)
        {
            List<T2> ret = new();
            foreach (T1 e in collection)
            {
                if (condition(e))
                {
                    ret.Add(func(e));
                }
            }

            return ret;
        }

        public static List<T> Set<T>(this List<T> self)
        {
            List<T> ret = new();
            foreach (T e in self)
            {
                if (!ret.Contains(e))
                {
                    ret.Add(e);
                }
            }

            return ret;
        }

        public static List<T2> Map<T1, T2>(Func<T1, T2> func, List<T1> list)
        {
            List<T2> ret = new();

            foreach (T1 e in list)
            {
                ret.Add(func(e));
            }

            return ret;
        }
    }

    public static class Flow
    {
        public static void Loop(uint loopNum, Action action)
        {
            for (int _ = 0; _ < loopNum; _++)
            {
                action();
            }
        }
    }

    public static class Tag
    {
        public static GameObject FindTag(this string self)
        {
            return GameObject.FindGameObjectWithTag(self);
        }

        public static GameObject[] FindsTag(this string self)
        {
            return GameObject.FindGameObjectsWithTag(self);
        }

        public static bool HasTag(this Collider self, string tag)
        {
            return self.CompareTag(tag);
        }

        public static bool HasTag(this Collision self, string tag)
        {
            return self.gameObject.CompareTag(tag);
        }
    }

    public static class Colors
    {
        public static readonly Color AliceBlue = new Color32(240, 248, 255, 255);
        public static readonly Color AntiqueWhite = new Color32(250, 235, 215, 255);
        public static readonly Color Aqua = new Color32(0, 255, 255, 255);
        public static readonly Color Aquamarine = new Color32(127, 255, 212, 255);
        public static readonly Color Azure = new Color32(240, 255, 255, 255);
        public static readonly Color Beige = new Color32(245, 245, 220, 255);
        public static readonly Color Bisque = new Color32(255, 228, 196, 255);
        public static readonly Color Black = new Color32(0, 0, 0, 255);
        public static readonly Color BlanchedAlmond = new Color32(255, 235, 205, 255);
        public static readonly Color Blue = new Color32(0, 0, 255, 255);
        public static readonly Color BlueViolet = new Color32(138, 43, 226, 255);
        public static readonly Color Brown = new Color32(165, 42, 42, 255);
        public static readonly Color Burlywood = new Color32(222, 184, 135, 255);
        public static readonly Color CadetBlue = new Color32(95, 158, 160, 255);
        public static readonly Color Chartreuse = new Color32(127, 255, 0, 255);
        public static readonly Color Chocolate = new Color32(210, 105, 30, 255);
        public static readonly Color Coral = new Color32(255, 127, 80, 255);
        public static readonly Color CornflowerBlue = new Color32(100, 149, 237, 255);
        public static readonly Color Cornsilk = new Color32(255, 248, 220, 255);
        public static readonly Color Crimson = new Color32(220, 20, 60, 255);
        public static readonly Color Cyan = new Color32(0, 255, 255, 255);
        public static readonly Color DarkBlue = new Color32(0, 0, 139, 255);
        public static readonly Color DarkCyan = new Color32(0, 139, 139, 255);
        public static readonly Color DarkGoldenrod = new Color32(184, 134, 11, 255);
        public static readonly Color DarkGray = new Color32(169, 169, 169, 255);
        public static readonly Color DarkGreen = new Color32(0, 100, 0, 255);
        public static readonly Color DarkKhaki = new Color32(189, 183, 107, 255);
        public static readonly Color DarkMagenta = new Color32(139, 0, 139, 255);
        public static readonly Color DarkOliveGreen = new Color32(85, 107, 47, 255);
        public static readonly Color DarkOrange = new Color32(255, 140, 0, 255);
        public static readonly Color DarkOrchid = new Color32(153, 50, 204, 255);
        public static readonly Color DarkRed = new Color32(139, 0, 0, 255);
        public static readonly Color DarkSalmon = new Color32(233, 150, 122, 255);
        public static readonly Color DarkSeaGreen = new Color32(143, 188, 143, 255);
        public static readonly Color DarkSlateBlue = new Color32(72, 61, 139, 255);
        public static readonly Color DarkSlateGray = new Color32(47, 79, 79, 255);
        public static readonly Color DarkTurquoise = new Color32(0, 206, 209, 255);
        public static readonly Color DarkViolet = new Color32(148, 0, 211, 255);
        public static readonly Color DeepPink = new Color32(255, 20, 147, 255);
        public static readonly Color DeepSkyBlue = new Color32(0, 191, 255, 255);
        public static readonly Color DimGray = new Color32(105, 105, 105, 255);
        public static readonly Color DodgerBlue = new Color32(30, 144, 255, 255);
        public static readonly Color FireBrick = new Color32(178, 34, 34, 255);
        public static readonly Color FloralWhite = new Color32(255, 250, 240, 255);
        public static readonly Color ForestGreen = new Color32(34, 139, 34, 255);
        public static readonly Color Fuchsia = new Color32(255, 0, 255, 255);
        public static readonly Color Gainsboro = new Color32(220, 220, 220, 255);
        public static readonly Color GhostWhite = new Color32(248, 248, 255, 255);
        public static readonly Color Gold = new Color32(255, 215, 0, 255);
        public static readonly Color Goldenrod = new Color32(218, 165, 32, 255);
        public static readonly Color Gray = new Color32(128, 128, 128, 255);
        public static readonly Color Green = new Color32(0, 128, 0, 255);
        public static readonly Color GreenYellow = new Color32(173, 255, 47, 255);
        public static readonly Color Honeydew = new Color32(240, 255, 240, 255);
        public static readonly Color HotPink = new Color32(255, 105, 180, 255);
        public static readonly Color IndianRed = new Color32(205, 92, 92, 255);
        public static readonly Color Indigo = new Color32(75, 0, 130, 255);
        public static readonly Color Ivory = new Color32(255, 255, 240, 255);
        public static readonly Color Khaki = new Color32(240, 230, 140, 255);
        public static readonly Color Lavender = new Color32(230, 230, 250, 255);
        public static readonly Color Lavenderblush = new Color32(255, 240, 245, 255);
        public static readonly Color LawnGreen = new Color32(124, 252, 0, 255);
        public static readonly Color LemonChiffon = new Color32(255, 250, 205, 255);
        public static readonly Color LightBlue = new Color32(173, 216, 230, 255);
        public static readonly Color LightCoral = new Color32(240, 128, 128, 255);
        public static readonly Color LightCyan = new Color32(224, 255, 255, 255);
        public static readonly Color LightGoldenodYellow = new Color32(250, 250, 210, 255);
        public static readonly Color LightGray = new Color32(211, 211, 211, 255);
        public static readonly Color LightGreen = new Color32(144, 238, 144, 255);
        public static readonly Color LightPink = new Color32(255, 182, 193, 255);
        public static readonly Color LightSalmon = new Color32(255, 160, 122, 255);
        public static readonly Color LightSeaGreen = new Color32(32, 178, 170, 255);
        public static readonly Color LightSkyBlue = new Color32(135, 206, 250, 255);
        public static readonly Color LightSlateGray = new Color32(119, 136, 153, 255);
        public static readonly Color LightSteelBlue = new Color32(176, 196, 222, 255);
        public static readonly Color LightYellow = new Color32(255, 255, 224, 255);
        public static readonly Color Lime = new Color32(0, 255, 0, 255);
        public static readonly Color LimeGreen = new Color32(50, 205, 50, 255);
        public static readonly Color Linen = new Color32(250, 240, 230, 255);
        public static readonly Color Magenta = new Color32(255, 0, 255, 255);
        public static readonly Color Maroon = new Color32(128, 0, 0, 255);
        public static readonly Color MediumAquamarine = new Color32(102, 205, 170, 255);
        public static readonly Color MediumBlue = new Color32(0, 0, 205, 255);
        public static readonly Color MediumOrchid = new Color32(186, 85, 211, 255);
        public static readonly Color MediumPurple = new Color32(147, 112, 219, 255);
        public static readonly Color MediumSeaGreen = new Color32(60, 179, 113, 255);
        public static readonly Color MediumSlateBlue = new Color32(123, 104, 238, 255);
        public static readonly Color MediumSpringGreen = new Color32(0, 250, 154, 255);
        public static readonly Color MediumTurquoise = new Color32(72, 209, 204, 255);
        public static readonly Color MediumVioletRed = new Color32(199, 21, 133, 255);
        public static readonly Color MidnightBlue = new Color32(25, 25, 112, 255);
        public static readonly Color Mintcream = new Color32(245, 255, 250, 255);
        public static readonly Color MistyRose = new Color32(255, 228, 225, 255);
        public static readonly Color Moccasin = new Color32(255, 228, 181, 255);
        public static readonly Color NavajoWhite = new Color32(255, 222, 173, 255);
        public static readonly Color Navy = new Color32(0, 0, 128, 255);
        public static readonly Color OldLace = new Color32(253, 245, 230, 255);
        public static readonly Color Olive = new Color32(128, 128, 0, 255);
        public static readonly Color Olivedrab = new Color32(107, 142, 35, 255);
        public static readonly Color Orange = new Color32(255, 165, 0, 255);
        public static readonly Color Orangered = new Color32(255, 69, 0, 255);
        public static readonly Color Orchid = new Color32(218, 112, 214, 255);
        public static readonly Color PaleGoldenrod = new Color32(238, 232, 170, 255);
        public static readonly Color PaleGreen = new Color32(152, 251, 152, 255);
        public static readonly Color PaleTurquoise = new Color32(175, 238, 238, 255);
        public static readonly Color PaleVioletred = new Color32(219, 112, 147, 255);
        public static readonly Color PapayaWhip = new Color32(255, 239, 213, 255);
        public static readonly Color PeachPuff = new Color32(255, 218, 185, 255);
        public static readonly Color Peru = new Color32(205, 133, 63, 255);
        public static readonly Color Pink = new Color32(255, 192, 203, 255);
        public static readonly Color Plum = new Color32(221, 160, 221, 255);
        public static readonly Color PowderBlue = new Color32(176, 224, 230, 255);
        public static readonly Color Purple = new Color32(128, 0, 128, 255);
        public static readonly Color Red = new Color32(255, 0, 0, 255);
        public static readonly Color RosyBrown = new Color32(188, 143, 143, 255);
        public static readonly Color RoyalBlue = new Color32(65, 105, 225, 255);
        public static readonly Color SaddleBrown = new Color32(139, 69, 19, 255);
        public static readonly Color Salmon = new Color32(250, 128, 114, 255);
        public static readonly Color SandyBrown = new Color32(244, 164, 96, 255);
        public static readonly Color SeaGreen = new Color32(46, 139, 87, 255);
        public static readonly Color Seashell = new Color32(255, 245, 238, 255);
        public static readonly Color Sienna = new Color32(160, 82, 45, 255);
        public static readonly Color Silver = new Color32(192, 192, 192, 255);
        public static readonly Color SkyBlue = new Color32(135, 206, 235, 255);
        public static readonly Color SlateBlue = new Color32(106, 90, 205, 255);
        public static readonly Color SlateGray = new Color32(112, 128, 144, 255);
        public static readonly Color Snow = new Color32(255, 250, 250, 255);
        public static readonly Color SpringGreen = new Color32(0, 255, 127, 255);
        public static readonly Color SteelBlue = new Color32(70, 130, 180, 255);
        public static readonly Color Tan = new Color32(210, 180, 140, 255);
        public static readonly Color Teal = new Color32(0, 128, 128, 255);
        public static readonly Color Thistle = new Color32(216, 191, 216, 255);
        public static readonly Color Tomato = new Color32(255, 99, 71, 255);
        public static readonly Color Turquoise = new Color32(64, 224, 208, 255);
        public static readonly Color Violet = new Color32(238, 130, 238, 255);
        public static readonly Color Wheat = new Color32(245, 222, 179, 255);
        public static readonly Color White = new Color32(255, 255, 255, 255);
        public static readonly Color WhiteSmoke = new Color32(245, 245, 245, 255);
        public static readonly Color Yellow = new Color32(255, 255, 0, 255);
        public static readonly Color YellowGreen = new Color32(154, 205, 50, 255);
    }
}