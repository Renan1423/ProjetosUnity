using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMouse : MonoBehaviour
{
    public float speed;
    public float rotationOffset;

    private Animator spaceShipAnim;

    void Start()
    {
        spaceShipAnim = GetComponent<Animator>();
        spaceShipAnim.SetInteger("Dir", 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        //float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

    }
}
