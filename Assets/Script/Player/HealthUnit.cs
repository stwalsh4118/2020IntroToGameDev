using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthUnit : MonoBehaviour
{
    public string type;
    public string addHealthType;
    public string takeDamageType;
    public float healthValue;

    public string onTakeDamage()
    {
        return takeDamageType;
    }

    public string onAddHealth()
    {
        return addHealthType;
    }
}
