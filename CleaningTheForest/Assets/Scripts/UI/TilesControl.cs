using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesControl : MonoBehaviour
{
    
    public int restartCountLimit;

    private void Awake()
    {
        restartCountLimit = Random.Range(5, 12);
    }


}
