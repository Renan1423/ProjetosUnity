using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyTargeting : MonoBehaviour
{
    public Enemy Enemy;
    public AIPath aipath;
    public AIDestinationSetter aiDestinationSetter;

    private Player Player;

    void Start()
    {
        Player = FindObjectOfType<Player>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Skeleton" && collision.GetComponent<Skeleton>().HP > 0)
        {
            aiDestinationSetter.target = collision.transform;
        }
        else {
            aiDestinationSetter.target = Player.transform;
        }
    }
}
