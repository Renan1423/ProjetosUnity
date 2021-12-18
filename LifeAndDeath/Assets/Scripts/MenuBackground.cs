using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour
{
    private RectTransform trans;
    private float initialPositionX;
    private float initialPositionY;

    private int distanceCount = 0;
    private float limit;
    private bool FirstTime = false;


    void Start()
    {
        trans = GetComponent<RectTransform>();

        initialPositionX = trans.position.x;
        initialPositionY = trans.position.y;

        limit = trans.rect.height * 1.5f;
    }


    void Update()
    {
        distanceCount++;
        if (distanceCount >= limit)
        {
            trans.position = new Vector2(initialPositionX, initialPositionY + (initialPositionY));
            distanceCount = 0;
            FirstTime = true;
        }
        else {
            if (!FirstTime)
            {
                trans.position = new Vector2(initialPositionX, initialPositionY + distanceCount);
            }
            else {
                trans.position = new Vector2(initialPositionX, initialPositionY + (initialPositionY) + distanceCount);
            }
        }
    }
}
