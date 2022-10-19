using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Domain.Types;

namespace Extensions
{
  public static class StringExtensions
  {
    /// <summary>
    /// Returns the string value for the given enum
    /// This extension method is meant to be used in conjuction 
    /// with StringValueAttribute
    /// </summary> 
    /// <param name="value">Enum value</param>
    /// <returns>The string value within StringValueAttribute</returns>
    public static string GetStringValue(this Enum value)
    {
      Type type = value.GetType();

      FieldInfo fieldInfo = type.GetField(value.ToString());

      StringValueAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

      return attributes.Length > 0 ? attributes[0].StringValue : null;
    } 
  }
}