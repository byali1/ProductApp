﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Application.Exceptions
{
    public class ValidationException : System.Exception
    {
        public ValidationException() : base("Validation error occured")
        {

        }

        public ValidationException(string message) : base(message)
        {

        }

        public ValidationException(Exception ex) : this(ex.Message)
        {

        }
    }
}