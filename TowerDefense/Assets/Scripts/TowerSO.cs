using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Tower Template", menuName = "New Tower")]
public class TowerSO : ScriptableObject
{
    public float range = 5;
    public float damage = 2;
    public float fireRate = 1;
    public float price = 20;
    public TowerSO upgrade;
    public Color towerColour = Color.white;
}
