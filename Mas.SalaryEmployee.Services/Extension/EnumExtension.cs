﻿using System;
using System.ComponentModel;

namespace Mas.SalaryEmployee.Services.Extension
{
    public static class EnumExtension
    {
        public static T GetFromDescription<T>(this string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", nameof(description));
        }
    }
}
