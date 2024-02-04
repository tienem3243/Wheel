using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockeyEditPanel : View
{
    [SerializeField] GameObject jockeyEditTagPrefab;
    [SerializeField] Transform rootJockey;
    public List<JockeyEditTag> jockeys;
   

    [ButtonMethod]
    public void CreatBlankJockey()
    {
        CreateJockeyUI(new Jockey(), rootJockey);
    }
    public void CreateJockeyUI(Jockey jockey, Transform parentTransform)
    {
        GameObject jockeyUIPrefab = Instantiate(jockeyEditTagPrefab, parentTransform);
        JockeyEditTag jockeyEditTag = jockeyUIPrefab.GetComponent<JockeyEditTag>();

        if (jockeyEditTag != null)
        {
            jockeyEditTag.Initialize();
            jockeyEditTag.SetInfo(jockey);
            jockeys.Add(jockeyEditTag);    
        }
    }

    public override void Initialize()
    {
        jockeys = new List<JockeyEditTag>();
        // Example usage in Start method
        foreach (Jockey jockey in Manager.Instance.dataManager.jockeyList)
        {
            CreateJockeyUI(jockey, rootJockey);
        }
    }
    public override void UpdateView()
    {
        jockeys.ForEach(x => x.ApplyUpdate());
    }

}
