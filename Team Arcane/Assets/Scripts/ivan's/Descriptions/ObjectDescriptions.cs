using System.Collections.Generic;
using UnityEngine;

public class ObjectDescriptions 
{
    private static readonly Dictionary<string, string> descriptions = new Dictionary<string, string>{
        // THIS IS WHERE YOU DEFINE THE OBJECT AND ITS DESCRIPTION
        {"Key", "This key looks like it could unlock something."},
        {"Key 2", "This key is useless"},
        {"Painting","This was painted by Mrs. Magnate... I miss the smell of fresh oil and varnish. \nI wish I know where she went."}
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
