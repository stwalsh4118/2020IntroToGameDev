using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon", order = 52)]
public class Weapons : ScriptableObject
{
    new public string name = "New Item";
    public AudioClip shootSound = null;
    public Sprite icon = null;
    public float maxDamage = 20f;
    public float minDamage = 10f;
    
}
