using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.Models
{
    public class CustomAuditable 
    {
        public DateTime _editedOn { get; set; }
        public DateTime _createdOn { get; set; }
    }
}