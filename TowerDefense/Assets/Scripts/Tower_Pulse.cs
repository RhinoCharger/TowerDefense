using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Pulse : Tower
{

    protected override void DamageTarget()
    {
        Collider[] nearbyColliders;
        List<Creep> nearbyCreeps = new List<Creep>();

        nearbyColliders = Physics.OverlapSphere(transform.position, range);

        for (int i = 0; i < nearbyColliders.Length; i++)
        {
            Creep tempCreep = nearbyColliders[i].GetComponent<Creep>();

            if (tempCreep != null)
            {
                //nearbyCreeps.Add(tempCreep);
                tempCreep.TakeDamage(damage, this);
            }

        }

    }

}
