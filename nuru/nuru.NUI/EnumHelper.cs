using System;

namespace nuru.NUI
{
    internal static class EnumHelper
    {
        public static bool IsUndefined<T>(this T value) where T : Enum
        {
            return !Enum.IsDefined(typeof(T), value);
        }
    }
}
