using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/JockeyData", order = 1)]
public class JockeyData : ScriptableObject
{
    public List<Jockey> jockeys;   
}
