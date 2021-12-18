using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryText : MonoBehaviour
{
    private Transform trans;

    public LevelLoader levelLoader;

    public float speed;
    public float Delay;

    private float nowPosition;

    void Start()
    {
        trans = GetComponent<Transform>();

        StartCoroutine(StartGameCoroutine());
    }

    void Update()
    {
        nowPosition += speed * Time.deltaTime;

        trans.position = new Vector2(trans.position.x, trans.position.y + nowPosition);
    }

    IEnumerator StartGameCoroutine() {
        yield return new WaitForSeconds(Delay);

        levelLoader.LoadNextLevel();
    }
}
