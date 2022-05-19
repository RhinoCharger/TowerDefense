using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu] 
public class CreepSO : ScriptableObject
{
    public float maxHealth;
    public float armour;
    public float speed;
    public float money;

    public Mesh creepMesh;
    public Material creepMaterial;
}
