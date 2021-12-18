using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkeletonTargeting : MonoBehaviour
{
    public Skeleton Skeleton;
    public AIPath aipath;
    public AIDestinationSetter aiDestinationSetter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Skeleton.isTargeting == false)
        {
            if (collision.tag == "Enemy" && collision.GetComponent<Enemy>().HP > 0)
            {
                aiDestinationSetter.target = collision.transform;
                Skeleton.isTargeting = true;
            }
        }
        else { 
            if(collision.tag == "Enemy" && aiDestinationSetter.target.GetComponent<Enemy>().HP <= 0)
            {
                aiDestinationSetter.target = null;
                Skeleton.isTargeting = false;
            }
        }
    }
}
