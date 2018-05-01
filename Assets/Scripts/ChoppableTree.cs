using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppableTree : MonoBehaviour
{
    public GameObject treeHitEffects;

    void Awake()
    {
        GetComponent<Hurtbox>().OnHit += OnHit;
    }

    private void OnHit(HitInfo hitInfo)
    {
        var gameObject = Instantiate(treeHitEffects, hitInfo.hitPosition, Quaternion.identity);
        if (hitInfo.hitNormal.magnitude > 0)
        {
            gameObject.transform.forward = hitInfo.hitNormal;
        }
    }
}
