using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    private SpriteRenderer spr;

    private Player player;

    public LayerMask playerMask;

    public int pointsRequired;
    public Text pointsRequiredText;
    public bool isCollectable = false;
    public bool isTouchingPlayer = false;

    public float checkRadius;
    public Transform collisionPoint;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();

        player = FindObjectOfType<Player>();

        pointsRequiredText.text = "Points required:" + pointsRequired;
    }

    void Update()
    {
        if (player.points >= pointsRequired) {
            isCollectable = true;
            spr.color = new Color(1, 1, 1, 1f);
        }
        else
        {
            isCollectable = false;
            spr.color = new Color(1, 1, 1, 0.5f);
        }

        isTouchingPlayer = Physics2D.OverlapCircle(collisionPoint.position, checkRadius, playerMask);

        if (isTouchingPlayer) { 
            pointsRequiredText.color = new Color(1, 1, 1, 1f);
        }
        else
        {
            pointsRequiredText.color = new Color(1, 1, 1, 0f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(collisionPoint.position, checkRadius);

    }
}
