using System.Collections.Generic;
using UnityEngine;

public class ObjectDescriptions 
{
    private static readonly Dictionary<string, string> descriptions = new Dictionary<string, string>{
        // THIS IS WHERE YOU DEFINE THE OBJECT AND ITS DESCRIPTION
        {"Key", "This key looks like it could unlock something."},
        {"Key 2", "This key is useless"} 
        // AND SO ON... WHERE THE KEY IS THE NAME OF THE OBJECT AND THE VALUE IS THE DESCRIPTION
    };
    public static string GetDescription(string key)
    {
        if (descriptions.ContainsKey(key))
        {
            return descriptions[key];
        }
        return null;
    }

}
