using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Manager manager;
    public Material mat;
    public GameObject spawnedTower;


    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;

        manager = FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        mat.color = Color.red;
        if (Input.GetMouseButtonDown(0))
        {
            if(spawnedTower != null)
            {
                return;
            }

            float price = manager.towerData.price;

            if (manager.BuySomething(price) == false)
            {
                return;
            }

            Debug.Log("I have been clicked");

            GameObject newObject = Instantiate(manager.towerPrefab);

            spawnedTower = newObject;

            spawnedTower.GetComponent<Renderer>().material.color = manager.towerData.towerColour;

            newObject.transform.position = transform.position;

            Tower tower = newObject.GetComponent<Tower>();

            tower.damage = manager.towerData.damage;
            tower.range = manager.towerData.range;
            tower.fireRate = manager.towerData.fireRate;
            tower.upgradeData = manager.towerData.upgrade;
        }
    }

    private void OnMouseExit()
    {
        mat.color = Color.white;
    }


}
