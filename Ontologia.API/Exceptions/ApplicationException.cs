﻿using System.Globalization;

namespace Ontologia.API.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException() : base() { }

        public ApplicationException(string message) : base(message) { }

        public ApplicationException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
