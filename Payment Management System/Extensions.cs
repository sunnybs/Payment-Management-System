﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Payment_Management_System
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());
            var displayAttribute = fi.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(DisplayAttribute));
            if (displayAttribute == null) return enumValue.ToString();
            return displayAttribute.NamedArguments.FirstOrDefault(a => a.MemberName == "Name").TypedValue.Value.ToString();
        }
    }
}
