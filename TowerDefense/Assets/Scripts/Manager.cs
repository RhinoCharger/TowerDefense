using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [Header("User Interface")]
    public Button nextWaveButton;

    [Header("Player Data")]
    public int lives;
    public float money;

    [Header("Creeps")]
    public GameObject creepPrefab;
    public Vector3 creepSpawn;
    public Vector3 creepTarget;
    public WaveSO currentWave;
    public int creepInWave;
    public List<WaveSO> allWaves;
    public int waveInAll;
    int livingCreeps = 0;

    [Header("Towers")]
    public GameObject towerPrefab;
    [Tooltip("The one we have currently selected")]
    public TowerSO towerData;
    [Tooltip("All of the possible towers")]
    public List<TowerSO> allTowers;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectTower(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectTower(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectTower(2);
        }
    }

    public bool BuySomething(float price)
    {
        if(price <= money)
        {
            money = money - price;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SelectTower(int value)
    {
        if(value < 0 || value >= allTowers.Count)
        {
            return;
        }

        towerData = allTowers[value];
        towerPrefab = towerData.prefab;
    }

    public void CreepDied(float creepValue)
    {
        money = money + creepValue;
        livingCreeps--;
        if(livingCreeps == 0)
        {
            if(creepInWave == 0)
            {
                nextWaveButton.interactable = true;

                if(waveInAll >= allWaves.Count)
                {
                    Debug.Log("You have beaten all the waves, now go do something with your life...");
                }
            }
        }
    }

    public void SpawnNextCreep()
    {
        currentWave = allWaves[waveInAll];

        nextWaveButton.interactable = false;

        livingCreeps++;

        GameObject newObject = Instantiate(creepPrefab);

        newObject.transform.position = creepSpawn;

        Creep creep = newObject.GetComponent<Creep>();

        creep.objective = creepTarget;

        creep.maxHealth = currentWave.creeps[creepInWave].maxHealth;
        creep.armour = currentWave.creeps[creepInWave].armour;
        creep.speed = currentWave.creeps[creepInWave].speed;
        creep.money = currentWave.creeps[creepInWave].money;

        if(creepInWave < currentWave.creeps.Count - 1)
        {
            creepInWave = creepInWave + 1;
            Invoke("SpawnNextCreep", currentWave.spacing[creepInWave]);
        }
        else
        {
            creepInWave = 0;

            waveInAll++;
        }
    }

}
