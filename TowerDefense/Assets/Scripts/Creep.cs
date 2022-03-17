using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creep : MonoBehaviour
{
    public float health, maxHealth;
    public float armour;
    public float speed;
    public Vector3 objective;

    public Image healthBar;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, objective, 
            speed * Time.deltaTime);
    }

    public void TakeDamage(float value)
    {
        if(armour <= 0)
        {
            health = health - value;
        }
        else
        {
            health = health - (value / armour);
        }

        healthBar.fillAmount = health / maxHealth;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
