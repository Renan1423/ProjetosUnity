using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
	public int openingDirection;
	// 1 --> need down door
	// 2 --> need up door
	// 3 --> need left door
	// 4 --> need right door


	private RoomTemplates templates;
	private int rand;
	public bool spawned = false;


	private int RoomsLimit = 9;

	public float waitTime = 4f;

	void Start()
	{
		//Destroy(gameObject, waitTime);
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		Invoke("Spawn", 0.1f);
	}

    void Update()
    {
		waitTime -= Time.deltaTime;

		if (waitTime <= 0 && !spawned)
		{
			if (openingDirection == 1)
			{
				// Need to spawn a room with a BOTTOM door.
				Instantiate(templates.closedRoom, transform.position, templates.downRooms[0].transform.rotation);
			}
			else if (openingDirection == 2)
			{
				// Need to spawn a room with a TOP door.
				Instantiate(templates.closedRoom, transform.position, templates.upRooms[0].transform.rotation);
			}
			else if (openingDirection == 3)
			{
				// Need to spawn a room with a LEFT door.
				Instantiate(templates.closedRoom, transform.position, templates.leftRooms[0].transform.rotation);
			}
			else if (openingDirection == 4)
			{
				// Need to spawn a room with a RIGHT door.
				Instantiate(templates.closedRoom, transform.position, templates.rightRooms[0].transform.rotation);
			}
			Destroy(gameObject);
		}
		else if (waitTime <= 0 && spawned)
		{
			Destroy(gameObject);
		}
	}

    void Spawn()
	{


		if (templates.GetComponent<RoomTemplates>().rooms.Count < RoomsLimit)
		{
			if (spawned == false)
			{
				if (openingDirection == 1)
				{
					// Need to spawn a room with a BOTTOM door.
					rand = Random.Range(0, templates.downRooms.Length);
					Instantiate(templates.downRooms[rand], transform.position, templates.downRooms[rand].transform.rotation);
				}
				else if (openingDirection == 2)
				{
					// Need to spawn a room with a TOP door.
					rand = Random.Range(0, templates.upRooms.Length);
					Instantiate(templates.upRooms[rand], transform.position, templates.upRooms[rand].transform.rotation);
				}
				else if (openingDirection == 3)
				{
					// Need to spawn a room with a LEFT door.
					rand = Random.Range(0, templates.leftRooms.Length);
					Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
				}
				else if (openingDirection == 4)
				{
					// Need to spawn a room with a RIGHT door.
					rand = Random.Range(0, templates.rightRooms.Length);
					Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
				}
				spawned = true;
			}
		}
		else if (templates.GetComponent<RoomTemplates>().rooms.Count >= RoomsLimit && spawned == false)
		{
			if (openingDirection == 1)
			{
				// Need to spawn a room with a BOTTOM door.
				Instantiate(templates.closedRoom, transform.position, templates.downRooms[0].transform.rotation);
			}
			else if (openingDirection == 2)
			{
				// Need to spawn a room with a TOP door.
				Instantiate(templates.closedRoom, transform.position, templates.upRooms[0].transform.rotation);
			}
			else if (openingDirection == 3)
			{
				// Need to spawn a room with a LEFT door.
				Instantiate(templates.closedRoom, transform.position, templates.leftRooms[0].transform.rotation);
			}
			else if (openingDirection == 4)
			{
				// Need to spawn a room with a RIGHT door.
				Instantiate(templates.closedRoom, transform.position, templates.rightRooms[0].transform.rotation);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.CompareTag("SpawnPoint"))
		{
			if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
			{
				Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
			spawned = true;
		}
	}
}
