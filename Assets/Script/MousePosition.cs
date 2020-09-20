using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public Vector3 mousePosition;
    public Vector3 mousePositionWorld;

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        
    }
}
