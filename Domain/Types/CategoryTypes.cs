using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Types
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

    public enum CategoryTypes: int
    {
        [StringValue("mmo")]
        MMO = 0,
        [StringValue("action")]
        Action = 1,
        [StringValue("rpg")]
        Rpg = 2,
        [StringValue("jrpg")]
        Jrpg = 3,
        [StringValue("OpenWorld")]
        OpenWorld = 4

    }
}