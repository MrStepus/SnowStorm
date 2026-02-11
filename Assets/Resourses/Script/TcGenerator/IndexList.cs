using System.Collections.Generic;
using UnityEngine;

public class IndexListSetings : MonoBehaviour
{
    public static Dictionary<int, List<float>> ZoneSeting = new Dictionary<int, List<float>>() 
    {
        {1, new List<float>(){40f, 2.5f, 1f}  },
        {2, new List<float>(){10f, 1f, 1f}  }
    };

}
