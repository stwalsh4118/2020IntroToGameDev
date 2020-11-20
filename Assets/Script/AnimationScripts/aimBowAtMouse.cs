using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimBowAtMouse : MonoBehaviour
{
    Vector3 mouse_pos;
    Transform target; //Assign to the object you want to rotate
    Vector3 object_pos;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        target = transform;
    }

    // Update is called once per frame

 
    void  Update()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private float ShootAtMouse()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 directionVector;
        directionVector = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(directionVector.x, directionVector.y);
        angle = angle * 180 / Mathf.PI;
        angle = angle - 90;
        return angle;
    }
}
