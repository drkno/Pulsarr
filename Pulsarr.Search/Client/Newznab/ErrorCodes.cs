using System;
using System.Collections.Generic;

namespace Pulsarr.Search.Client.Newznab
{
    public static class ErrorCodes
    {
        private static readonly IDictionary<ushort, string> _errorCodes = new Dictionary<ushort, string>
        {
            [100] = "Incorrect user credentials",
            [101] = "Account suspended",
            [102] = "Insufficient privileges/not authorized",
            [103] = "Registration denied",
            [104] = "Registrations are closed",
            [105] = "Invalid registration (Email Address Taken)",
            [106] = "Invalid registration (Email Address Bad Format)",
            [107] = "Registration Failed (Data error)",
            [200] = "Missing parameter",
            [201] = "Incorrect parameter",
            [202] = "No such function. (Function not defined in this specification).",
            [203] = "Function not available. (Optional function is not implemented).",
            [300] = "No such item.",
            [300] = "Item already exists.",
            [900] = "Unknown error",
            [910] = "API Disabled"
        };

        public static string Get(ushort code)
        {
            try
            {
                return _errorCodes[code];
            }
            catch (Exception)
            {
                return _errorCodes[900];
            }
        }
    }
}
