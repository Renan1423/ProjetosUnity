using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player" && other.tag != "DarkSphere" && other.tag != "Teleport" && other.tag != "Enemy"  && other.tag != "Skeleton" && other.tag != "ManaSphere" && other.tag != "BloodDrain" && other.tag != "TargetGetter" && other.tag!= "EnemySpawner" && other.gameObject.layer != 9 && other.gameObject.layer != 4)
		{
			Destroy(other.gameObject);
		}
	}
}
