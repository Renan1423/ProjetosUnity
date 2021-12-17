using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Rigidbody2D rig;

    public float moveLimit;
    public float moveCount;
    public int restartCount;
    private int restartCountLimit;

    public Transform restartPoint;

    public GameObject nextBiome;

    private SpeedController spdController;


    void Start()
    {
        spdController = FindObjectOfType<SpeedController>();

        rig = GetComponent<Rigidbody2D>();

        restartCountLimit = GameObject.Find("Grid").GetComponent<TilesControl>().restartCountLimit;
    }

    void Update()
    {
        moveCount += spdController.objSpeed * Time.deltaTime;

        if (moveCount >= moveLimit) {
            moveLimit = 12.25f;
            moveCount = 0;
            transform.position = restartPoint.position;
            if (restartCount > restartCountLimit)
            {
                nextBiome.SetActive(true);
                restartCount = 0;
                gameObject.SetActive(false);
            }
            else {
                restartCount++;
            }
        }

        rig.velocity = new Vector2(rig.velocity.x, spdController.objSpeed);
    }
}
