using System;
using System.Collections.Generic;
using System.Text;

namespace ZhuoYue.Components.Core
{
    public class MathHelper
    {
        public static int Max(params object[] array)
        {
            int max = default;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != default)
                {
                    if (int.TryParse(array[i]?.ToString(), out var value))
                    {
                        if (value > max)
                        {
                            max = value;
                        }
                    }
                }
            }
            return max;
        }
    }
}
