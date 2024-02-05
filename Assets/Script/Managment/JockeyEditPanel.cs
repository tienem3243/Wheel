using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockeyEditPanel : View
{
    [SerializeField] GameObject jockeyEditTagPrefab;
    [SerializeField] Transform rootJockey;
    public List<JockeyEditTag> jockeys;
    private JockeyEditTag currentSelected;

    [ButtonMethod]
    public void CreatBlankJockey()
    {
        Jockey jockey = new Jockey("",jockeys.Count.ToString(),new Animal());
        CreateJockeyUI(jockey, rootJockey);
    }
    public void CreateJockeyUI(Jockey jockey, Transform parentTransform)
    {
        GameObject jockeyUIPrefab = Instantiate(jockeyEditTagPrefab, parentTransform);
        JockeyEditTag jockeyEditTag = jockeyUIPrefab.GetComponent<JockeyEditTag>();

        if (jockeyEditTag != null)
        {
            jockeyEditTag.Initialize();
            if(jockey!=null)
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

    public void SetSelect(JockeyEditTag tag)
    {
      
        currentSelected=tag;
    }
    public JockeyEditTag GetCurrentSelected()
    {
        return currentSelected;
    }
    public override void UpdateView()
    {
        jockeys.ForEach(x => x.ApplyUpdate());
    }
    public override void Clear()
    {
        jockeys.ForEach(x => Destroy(x.gameObject));
        jockeys.Clear();
    }
    public void Delete()
    {
        if (currentSelected != null)
        {
            var temp = currentSelected;
            jockeys.Remove(currentSelected);
            Manager.Instance.dataManager.AddJockeyToDelete(temp.GetData());
            Destroy(temp.gameObject);
            
        }
    }
}
