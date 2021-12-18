using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
	public GameObject[] downRooms;
	public GameObject[] upRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime;

	public GameObject AStarObject;

    void Update()
    {
		waitTime -= Time.deltaTime;

		if (waitTime <= 0) {
			AStarObject.SetActive(true);
		}
    }

    /*void Update()
	{

		if (waitTime <= 0 && spawnedBoss == false)
		{
			for (int i = 0; i < rooms.Count; i++)
			{
				if (i == rooms.Count - 1)
				{
					Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
					spawnedBoss = true;
				}
			}
		}
		else
		{
			waitTime -= Time.deltaTime;

		}
	}*/
}
