using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RowSetUp", order = 1)]
public class RowSetUp : ScriptableObject
{
   public List<RaceRow> RaceRows;
}
