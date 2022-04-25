using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ScrollToggle : MonoBehaviour
{
    [SerializeField] Toggle[] toggles;
    [SerializeField] ToggleGroup toggleGroup;
    [SerializeField] Transform content;
    [SerializeField] BoundsNumber<int> currentIndex;

    [Min(0)]
    public float ScrollFactor;
    private int oldIndex;
    public bool Invert;
    public bool SelectAtToggle;
    private bool inited = false;
 

    //×¢²áGroup ºÍ toggle
    [Button]
    public void Init()
    {
        toggles = content.GetComponentsInChildren<Toggle>(true);
        Assert.IsNotNull(toggles);

        toggleGroup = GetComponent<ToggleGroup>();
        Assert.IsNotNull(toggleGroup);

        currentIndex = new BoundsNumber<int>(0,0, toggles.Length-1);

        foreach (var item in toggles)
        {
            item.group = toggleGroup;
            item.onValueChanged.AddListener(OnValueChanged);

        }
        currentIndex.value = 0;
        toggles[currentIndex].isOn = true;

        inited = true;
    }

    public void Update()
    {
        if (!inited) return;

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            bool leftCilck = Input.GetMouseButtonUp(0);
            if (leftCilck)
            {
                toggles[currentIndex].gameObject.SetActive(false);
                SelectItem(currentIndex += 1);
            }
        }




        var delta = Input.GetAxis("Mouse ScrollWheel");
        if (delta == 0) return;
        delta *= ScrollFactor;
        //if (delta < 0)
        //{
        //    currentIndex = currentIndex - 1;// (int)(delta*10);
        //}
        //else if (delta > 0)
        //{
        //    currentIndex = currentIndex + 1;//(int)(delta * 10);
        //}

        delta = Invert ? -delta : delta;
        //currentIndex = currentIndex + (int)(delta);

        //toggles[currentIndex].isOn = true;
        SelectItem(currentIndex + (int)(delta));
        //if( SelectAtToggle) toggles[currentIndex].Select();
        Debug.Log(delta);

    }
 
    private void SelectItem(int index)
    {
        currentIndex.value = index;
        toggles[currentIndex].isOn = true;
    }
    public void OnValueChanged(bool isOn)
    {
        
        if (isOn)
        {
            var item = toggles[currentIndex];
            if (item.isOn == true)
                {
                    //item.DoStateTransition
                    item.OnPointerEnter(null);
                }
        }
        else
        {
            //item.OnPointerExit(null);
            toggles[oldIndex].OnPointerExit(null);
        }
        oldIndex = currentIndex;
    }
}
