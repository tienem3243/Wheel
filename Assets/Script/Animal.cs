using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Animal
{
    public string ID;
    public string nameAnimal;
    public string imagePath;

    public Animal(string ID,string nameAnimal,string imagePath)
    {
        this.ID = ID;
        this.nameAnimal= nameAnimal;    
        this.imagePath = imagePath;
    }
}