using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using Domain.Extensions;

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

    /// <summary>
    /// Converts string to Guids, allowing for fake repeatable Guids
    /// </summary>
    /// <param name="src">String value to be converted to a GUID</param>
    /// <returns>A repeatbale GUID for the same src</returns>
     public static Guid AsGuid(this string src)
    {
      return string.IsNullOrEmpty(src)
        ? Guid.Empty
        : new Guid(MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(src)));
    }
  }

 
}