using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] protected Sprite _sprite = null;
    [SerializeField] protected float _abilityTime = 5f;


    public float abilityTime
    {
        get { return _abilityTime; }
        set { _abilityTime = value; }
    }

    public Sprite sprite
    {
        get { return _sprite; }
        set { _sprite = value; }
    }

    public virtual IEnumerator activateAbility() {
        yield return null;
    }
}
