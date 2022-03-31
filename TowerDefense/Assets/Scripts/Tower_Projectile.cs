using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Projectile : Tower
{
    [Header("Projectile Tower Mechanics")]
    public GameObject projectile;
    public Vector3 spawnPoint = Vector3.up;
    public float projSpeed = 10;
    public float explosionRadius = 2;

    protected override void DamageTarget()
    {
        if (currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.transform.position) < range)
            {
                GameObject newProj = Instantiate(projectile, transform.position + spawnPoint, Quaternion.identity);

                newProj.transform.LookAt(currentTarget.transform);

                newProj.GetComponent<Rigidbody>().velocity = newProj.transform.forward * projSpeed;

                newProj.GetComponent<Projectile>().origin = this;

                transform.LookAt(currentTarget.transform);
            }

        }
        else
        {
            FindTarget();
        }
    }

    public void HitTarget(Vector3 collidePoint)
    {
        Collider[] nearbyColliders;
        List<Creep> nearbyCreeps = new List<Creep>();

        nearbyColliders = Physics.OverlapSphere(collidePoint, range);

        for (int i = 0; i < nearbyColliders.Length; i++)
        {
            Creep tempCreep = nearbyColliders[i].GetComponent<Creep>();

            if (tempCreep != null)
            {
                //nearbyCreeps.Add(tempCreep);
                tempCreep.TakeDamage(damage);
            }

        }
    }

}
