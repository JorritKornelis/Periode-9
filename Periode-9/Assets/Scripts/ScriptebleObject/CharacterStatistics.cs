using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatistics", menuName = "Buyers/CharacterStatistics")]
public class CharacterStatistics : ScriptableObject
{
    public float waitTime;
    [Range(0, 100)]
    public float buyCheckChance, buyChance, otherThanInterestedChance;
    [Range(4, 10)]
    public int leaveChance;
    public int[] interestedItems;
    public Mesh characterMesh;
    public Material characterMaterial;
}
