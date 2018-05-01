using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public delegate void HitHandler(HitInfo hitInfo);
    public event HitHandler OnHit = delegate { };

    public void DoHit(HitInfo hitInfo)
    {
        OnHit(hitInfo);
    }
}
