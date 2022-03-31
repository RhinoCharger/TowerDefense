using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { close, far, mostHealth, leastHealth, fastest, slowest }

public class Tower : MonoBehaviour
{
    public float range;
    public float damage;
    public float fireRate;
    public TargetType targetType = TargetType.close;

    public Creep currentTarget;


    // Start is called before the first frame update
    void Start()
    {
        FindTarget();
        InvokeRepeating("DamageTarget", 0, fireRate);
    }

    protected void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider item in colliders)
        {
            Creep thisCreep = item.GetComponent<Creep>();

            if (thisCreep != null)
            {
                currentTarget = thisCreep;
            }
        }
    }

    protected virtual void DamageTarget()
    {
        if(currentTarget != null)
        {
            if(Vector3.Distance(transform.position, currentTarget.transform.position) < range)
            {
            currentTarget.TakeDamage(damage);

            transform.LookAt(currentTarget.transform);
            }

        }
        else
        {
            FindTarget();
        }
        Debug.Log("Tower is shooting");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
