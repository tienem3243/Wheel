using DG.Tweening;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectBox : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TextMeshPro info;
    private int intro = Animator.StringToHash("Cube2_In");



    [ButtonMethod]
    public void Intro()
    {
     
        animator.Play(intro);
    }
    public void SetText(string text)
    {
        info.text= text;    
    }
  
}
