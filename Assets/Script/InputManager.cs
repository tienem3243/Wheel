using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviourSingleton<InputManager>
{
    public bool lockInput = false;
    public UnityEvent OnPressUp;
    public UnityEvent OnPressDown;
    public UnityEvent OnPressLeft;
    public UnityEvent OnPressRight;
    [SerializeField] float delayInput=.2f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        OnPressDown.AddListener(()=>InputCooldown());
        OnPressUp.AddListener(()=>InputCooldown());
        OnPressRight.AddListener(()=>InputCooldown());
        OnPressLeft.AddListener(()=>InputCooldown());

    }
   
    void Update()
    {
        if (lockInput) return;
        if (Input.GetKeyDown(KeyCode.UpArrow)) OnPressUp.Invoke();
        if (Input.GetKeyDown(KeyCode.DownArrow)) OnPressDown.Invoke();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) OnPressLeft.Invoke();
        if (Input.GetKeyDown(KeyCode.RightArrow)) OnPressRight.Invoke();
    }

    private void InputCooldown()
    {
        StartCoroutine(Delay(delayInput));
    }

    public IEnumerator Delay(float delay)
    {
        lockInput = true;
        yield return new WaitForSeconds(delay);
        lockInput = false;
    }
    private void OnDisable()
    {
        OnPressDown.RemoveAllListeners();
        OnPressLeft.RemoveAllListeners();
        OnPressRight.RemoveAllListeners();
        OnPressUp.RemoveAllListeners();
    }
}
