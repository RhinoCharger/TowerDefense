using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { close, far, mostHealth, leastHealth, fastest, slowest }

public class Tower : MonoBehaviour
{
    public float range;
    public float damage;
    public float fireRate;
    public TowerSO upgradeData;

    public TargetType targetType = TargetType.close;

    public Creep currentTarget;

    protected AudioSource source;
    public bool constantSound = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        FindTarget();
        InvokeRepeating("DamageTarget", 0, fireRate);
    }

    protected void FindTarget()
    {
        if (constantSound)
            source.volume = 0;

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
                currentTarget.TakeDamage(damage, this);

                transform.LookAt(currentTarget.transform);

                if (constantSound == false)
                {
                source.PlayOneShot(source.clip);
                }
                else
                {
                    source.volume = 1;
                }
            }
            else
            {
                FindTarget();
            }
        }
        else
        {
            FindTarget();
        }
        //Debug.Log("Tower is shooting");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (upgradeData == null)
        {
            return;
        }

        if (upgradeData.prefab == null)
        {
            return;
        }

        Manager manager = FindObjectOfType<Manager>();
        if (manager.BuySomething(upgradeData.price) == false)
        {
            return;
        }

        GameObject newTower = Instantiate(upgradeData.prefab, transform.position, transform.rotation);

        Tower towerScript = newTower.GetComponent<Tower>();


        towerScript.range = upgradeData.range;
        towerScript.damage = upgradeData.damage;
        towerScript.fireRate = upgradeData.fireRate;

        towerScript.upgradeData = upgradeData.upgrade;

        Destroy(gameObject);

    }
}
