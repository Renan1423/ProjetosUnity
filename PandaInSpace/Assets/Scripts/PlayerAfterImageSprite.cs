using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    private float alphaMultiplier = 0.85f;

    private Transform player;

    private SpriteRenderer spr;
    private SpriteRenderer playerSpr;

    private Color color;

    private void OnEnable()
    {
        spr = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Playerr").transform;
        playerSpr = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        spr.sprite = playerSpr.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);

        if (Time.time >= (timeActivated + activeTime)) {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }

}
