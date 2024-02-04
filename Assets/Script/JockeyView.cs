using DG.Tweening;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class JockeyView : View
{
    private int hashIntro = Animator.StringToHash("Intro");
    [SerializeField] SelectBox selectTag3D;
    [SerializeField] RowSetUp selectTag2D;
    [SerializeField] JockeyData data;
    [SerializeField] JockeyTag tagName;
    [SerializeField] Transform root;
    private List<JockeyTag> visibleTags = new();
    private int currentSelect;
    private int previousSelect = -1;
    [SerializeField] private float space;
    [SerializeField] private float moveDuration = 0.2f;
    [SerializeField] private float rotateDuration = 0.5f;
    [SerializeField] AnimationCurve swingCurve;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotationOffset;

    [SerializeField] private int visualJockeyCount = 10;
    private int currentPage;
    private List<JockeyTag> objectPool = new List<JockeyTag>();
  
    private Vector3 basePos;

    public int CurrentSelect
    {
        get => currentSelect; set
        {
            if (value < 0 || value == currentSelect) return;
            int visualLimit = visualJockeyCount - 1;
            int dataLimit = data.jockeys.Count - (currentPage) * visualJockeyCount - 1;
            var limit = visualLimit < dataLimit ? visualLimit : dataLimit;
            previousSelect = currentSelect;
            currentSelect = Mathf.Clamp(value, 0, limit);




        }
    }

    private void OnSelectChange()
    {
        if (currentSelect == previousSelect) return;
        var newPos = visibleTags[CurrentSelect].transform.position + offset;
        newPos.z = selectTag3D.transform.position.z;

        selectTag3D.transform.DOMove(newPos, moveDuration);

        selectTag3D
            .transform
            .DORotate(new Vector3(360 * ((previousSelect > currentSelect) ? 1 : -1), 0, 0), rotateDuration, RotateMode.FastBeyond360).SetEase(swingCurve)
             .OnStart(() => StartCoroutine(SetDelay(() => SetJockeyInfoByID(currentSelect), rotateDuration / 2f)));
  
    }

    public override void Initialize()
    {
        CreateObjectPool();
        
        basePos = selectTag3D.transform.position;
        VisualPage(0);
    }

    private void CreateObjectPool()
    {
        for (int i = 0; i < visualJockeyCount; i++)
        {
            JockeyTag tag = Instantiate(tagName, root);
            tag.gameObject.SetActive(false);
            objectPool.Add(tag);
        }
    }

    private JockeyTag GetObjectFromPool()
    {
        foreach (var tag in objectPool)
        {
            if (!tag.gameObject.activeSelf)
            {
                return tag;
            }
        }

        // If no inactive object is found, you can optionally expand the pool by instantiating a new object
        JockeyTag newTag = Instantiate(tagName, root);
        newTag.gameObject.SetActive(false);
        objectPool.Add(newTag);

        return newTag;
    }



    private void VisualPage(int pageNum)
    {
        if (pageNum < 0 || pageNum * visualJockeyCount >= data.jockeys.Count)
        {
            return;
        }
        ClearPage();
        InputManager.Instance.lockInput = true; // Lock input

        Sequence sequence = DOTween.Sequence();

        int startIndex = pageNum * visualJockeyCount;
        int endIndex = Mathf.Min(startIndex + visualJockeyCount, data.jockeys.Count);
        sequence.AppendCallback(() => {
          
            selectTag3D.Intro();
            });
        sequence.AppendInterval(1.3f);

        for (int i = startIndex; i < endIndex; i++)
        {

            JockeyTag tag = GetObjectFromPool();
            tag.gameObject.SetActive(true);
            tag.SetVisible(false);
            var newPos = new Vector3(root.position.x, root.position.y - ((i - startIndex) % visualJockeyCount) * space, 0);
            tag.transform.position = newPos;
         
            // Make sure the index is within the valid range
            if (i < data.jockeys.Count)
            {
      
                tag.SetJockeyInfo(data.jockeys[i - startIndex].ToString());
                visibleTags.Add(tag);

            }

            var t = i - startIndex;
            if (i != startIndex)
            {
                newPos.z = selectTag3D.transform.position.z;
                sequence.Append(selectTag3D.transform.DOMove(newPos, moveDuration));


                sequence.Join(selectTag3D.transform.
                    DORotate(
                        new Vector3(360 * ((previousSelect > currentSelect) ? 1 : -1), 0, 0)
                        , rotateDuration
                        , RotateMode.FastBeyond360)
                        .SetEase(swingCurve)
                        .OnStart(() => StartCoroutine(SetDelay(() => SetJockeyInfoByID(t), rotateDuration / 2f)))
                        );


            }
            else
            {
                SetJockeyInfoByID(t);
            }

            sequence.AppendCallback(() =>
            {
                SetActiveWithTag(t, true);
            });
            //sequence.Insert((i - startIndex) * delayIntro, RotateAroundAnchor(tag.transform, transform.position + rotationOffset, 90f, tag.transform.up, introDuration / (visibleTags.Count * 1f)));

        }



        sequence.onComplete += () =>
        {
            InputManager.Instance.lockInput = false;
            CurrentSelect = visualJockeyCount - 1;
        };

    }

    private IEnumerator SetDelay(UnityAction action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
    private void SetJockeyInfoByID(int t)
    {
        selectTag3D.SetText(data.jockeys[t].ToString());
    }

    void SetActiveWithTag(int index, bool visible)
    {
        visibleTags[index].SetVisible(visible);
    }
    private void ClearPage()
    {
        if (visibleTags != null)
        {
            foreach (var tag in visibleTags)
            {
                if (tag != null)
                {
                    tag.Reset();
                    tag.gameObject.SetActive(false);

                }
            }
        }
        selectTag3D.transform.position = basePos;
        visibleTags.Clear();
    }
    private void Select(int i)
    {

        CurrentSelect = i;
    }
    [ButtonMethod]
    public void Up()
    {
        Debug.Log("Next");

        CurrentSelect -= 1;

        Select(CurrentSelect);
        OnSelectChange();
    }
    [ButtonMethod]
    public void Down()
    {
        Debug.Log("Before");

        CurrentSelect += 1;


        Select(CurrentSelect);
        OnSelectChange();
    }
    [ButtonMethod]
    public void NextPage()
    {


        int maxPage = Mathf.CeilToInt((float)data.jockeys.Count / visualJockeyCount) - 1;
        Debug.Log(maxPage);
        currentPage = Mathf.Min(currentPage + 1, maxPage);
        VisualPage(currentPage);


    }
    [ButtonMethod]
    public void PreviousPage()
    {
        currentPage = Mathf.Max(currentPage - 1, 0);
        VisualPage(currentPage);
    }
    Tween RotateAroundAnchor(Transform target, Vector3 anchorPoint, float rotationAngle, Vector3 rotationAxis, float duration)
    {

        if (anchorPoint == null)
        {
            Debug.LogError("Vui lòng gán điểm neo (anchor point)!");
            return null;
        }


        Vector3 anchorPointWorldPosition = anchorPoint;

        Quaternion targetRotation = Quaternion.AngleAxis(rotationAngle, rotationAxis);


        return target.DORotateQuaternion(targetRotation, duration)
             // Có thể thay đổi kiểu chuyển động tại đây
             .From()
             .OnComplete(() =>
             {
                 Debug.Log("Xoay quanh điểm neo hoàn thành!");
             });
    }

}
