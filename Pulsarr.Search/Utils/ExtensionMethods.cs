using System.IO;

namespace Pulsarr.Search.Utils
{
    public static class ExtensionMethods
    {
        public static bool FromYesNo(this string input)
        {
            switch (input.Trim().ToLower())
            {
                    case "yes":
                        return true;
                    case "no":
                        return false;
                    default:
                        throw new InvalidDataException("Invalid Value");
            }
        }
    }
}
