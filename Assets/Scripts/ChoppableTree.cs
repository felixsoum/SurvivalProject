using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppableTree : MonoBehaviour
{
    public GameObject treeHitEffects;
    int hp = 5;

    void Awake()
    {
        GetComponent<Hurtbox>().OnHit += OnHit;
    }

    private void OnHit(HitInfo hitInfo)
    {
        var treeHitObject = Instantiate(treeHitEffects, hitInfo.hitPosition, Quaternion.identity);
        if (hitInfo.hitNormal.magnitude > 0)
        {
            treeHitObject.transform.forward = hitInfo.hitNormal;
        }

        if (--hp == 0)
        {
            Destroy(gameObject);
        }
    }
}
