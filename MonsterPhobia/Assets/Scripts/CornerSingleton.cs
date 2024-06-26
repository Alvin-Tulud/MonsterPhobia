using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Stores all instances of the corners, seperated by peek and hide, so that there is no need to keep calling getGameObjectsByTag or something
 */ 

public class CornerSingleton : MonoBehaviour
{
    
    public static readonly HashSet<CornerSingleton> HideCorners = new HashSet<CornerSingleton>();
    public static readonly HashSet<CornerSingleton> PeekCorners = new HashSet<CornerSingleton>();

    void Start()
    {
        switch(gameObject.tag)
        {
            case "cornerhide":
                HideCorners.Add(this);
                break;
            case "cornerpeek":
                PeekCorners.Add(this);
                break;
            default:
                break;
        }
    }

}
