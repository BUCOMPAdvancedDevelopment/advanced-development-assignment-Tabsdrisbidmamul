using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Extensions
{
  public class StringValueAttribute: Attribute
  {
    # region properties
    
    /// <summary?>
    /// Holds the string value in the an enum
    /// </summary?
    public string StringValue {get; protected set;}

    #endregion

    #region constructor
    
    /// <summary>
    /// Initialse StringValue attribute
    /// </summary>
    /// <param name="value">A string value</param>
    public StringValueAttribute(string value) => StringValue = value;

    #endregion
  }
}