using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInfo
{
    public Vector3 hitPosition;
    public Vector3 hitNormal;

    public HitInfo(Vector3 hitPosition, Vector3 hitNormal)
    {
        this.hitPosition = hitPosition;
        this.hitNormal = hitNormal;
    }
}

public class Hitbox : MonoBehaviour
{
    HashSet<Hurtbox> hurtGroup = new HashSet<Hurtbox>();

    void OnTriggerEnter(Collider other)
    {
        Hurtbox hurtbox = other.GetComponent<Hurtbox>();
        if (hurtbox && !hurtGroup.Contains(hurtbox))
        {
            Vector3 hitPosition = other.ClosestPoint(transform.position);
            Vector3 hitNormal = transform.position - hitPosition;
            hurtbox.DoHit(new HitInfo(hitPosition, hitNormal.normalized));
            hurtGroup.Add(hurtbox);
        }
    }

    public void ClearHurtGroup()
    {
        hurtGroup.Clear();
    }
}
