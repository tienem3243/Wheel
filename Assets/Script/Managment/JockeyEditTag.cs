using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JockeyEditTag : TagView<Jockey> 
{
    [SerializeField] TextMeshProUGUI ID;
    [SerializeField] TMP_InputField nameJockey;
    [SerializeField] TMP_Dropdown animal;


    private void PopulateAnimalDropdown()
    {
        if (Manager.Instance.dataManager.animalList != null)
        {
            // Clear existing options
            animal.ClearOptions();

            // Get the list of animals from DataManager
            List<Animal> animalList = Manager.Instance.dataManager.animalList;

            // Create options based on animal IDs
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            foreach (Animal animalData in animalList)
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(animalData.ID);
                options.Add(option);
            }

            // Add options to the dropdown
            animal.AddOptions(options);
        }
    }

    public void UpdateInfo()
    {
        if (ID != null && Manager.Instance.dataManager != null)
        {
     
            Jockey jockey = new Jockey(nameJockey.text, ID.text, Manager.Instance.dataManager.animalList.Find(x => x.ID == animal.options[animal.value].text));
          

            // Update Jockey information in DataManager
            Manager.Instance.dataManager.UpdateJockeyInfo(jockey);
        }
    }

    public void SetInfo(Jockey info)
    {
        ID.text = info.ID.ToString();
        nameJockey.text = info.jockeyName;
        SetHolder(info);
        // You may need to set the selected value in the dropdown based on info.animal
        // For example: animal.value = FindAnimalIndex(info.animal.nameID);
    }

  

    public override void Initialize()
    {
     
        // Populate the dropdown with animal choices
        PopulateAnimalDropdown();
    }

    public override void ApplyUpdate()
    {
        UpdateInfo();
    }
}
