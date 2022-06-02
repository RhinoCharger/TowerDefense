using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Creep : MonoBehaviour
{
    [Header("Stats")]
    public float health, maxHealth;
    public float armour;
    public float speed;
    public float money;
    //public float lives;
    public Vector3 objective;

    [Header("UI")]
    public GameObject canvas;
    public Image healthBar;

    public NavMeshAgent agent;
    Camera cam;

    public GameObject deathParticle;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = 1;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(objective);
        agent.speed = speed;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.rotation = cam.transform.rotation;

        float dist = Vector3.Distance(transform.position, objective);

        if(dist < 1f)
        {
            Manager man = FindObjectOfType<Manager>();
            man.CreepDied(0);
            man.ChangeLives(-1);
            man.money -= 100;
            man.ChangeMoney();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float value, Tower damageSource)
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
            FindObjectOfType<Manager>().CreepDied(money);

            GameObject particle = Instantiate(deathParticle, transform.position, Quaternion.identity);
            //Destroy(particle, 10);
            particle.transform.LookAt(damageSource.transform, Vector3.back);

            AudioSource source = Camera.main.GetComponent<AudioSource>();
            source.PlayOneShot(source.clip);

            Destroy(gameObject);
        }
    }

}
