using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    [Header("User Interface")]
    public Button nextWaveButton;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI moneyText;

    public GameObject combatUI;
    public GameObject winScreen;
    public GameObject loseScreen;

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
        ChangeMoney();
        ChangeLives(0);
        combatUI.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
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
            ChangeMoney();
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
        ChangeMoney();
        livingCreeps--;
        if(livingCreeps == 0)
        {
            if(creepInWave == 0)
            {
                nextWaveButton.interactable = true;

                if(waveInAll >= allWaves.Count)
                {
                    Debug.Log("You have beaten all the waves, now go do something with your life...");
                    winScreen.SetActive(true);
                    combatUI.SetActive(false);
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

    void ChangeMoney()
    {
        moneyText.text = "Money: " + money;
    }

    public void ChangeLives(int value) 
    {
        lives = lives + value;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            loseScreen.SetActive(true);
            combatUI.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(creepSpawn, 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(creepTarget, 0.5f);
    }
}
