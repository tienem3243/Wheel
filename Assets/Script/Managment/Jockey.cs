using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Jockey
{
    public string jockeyName;
    public string ID;
    public Animal animal;

    public Jockey(string name, string id)
    {
        jockeyName = name;
        ID = id;
      
    }
    public Jockey(string name, string id,Animal animal)
    {
        jockeyName = name;
        ID = id;
        this.animal = animal;
    }
    public Jockey() { }

    public override string ToString()
    {
        return jockeyName + "ID " + ID + " - Animal: " + animal.ID;
    }
}

