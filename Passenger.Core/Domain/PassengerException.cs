﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Core.Domain
{
    public abstract class PassengerException : Exception
    {
        public string Code { get; }

        public PassengerException()
        { 
        }

        public PassengerException(string code)
        {
            Code = code;
        }

        public PassengerException(string message, params object[] args) 
            : this(string.Empty, message, args)
        {
        }

        public PassengerException(string code, string message, params object[] args) 
            : this(null,code, message, args)
        {
        }

        public PassengerException(Exception innerException, string message, params object[] args) 
            : this(innerException, string.Empty, message, args)
        {
        }

        public PassengerException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }

    }
}
