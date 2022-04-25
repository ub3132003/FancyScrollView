using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public void SetEvent(int opt)
    {
        gameObject.SetActive(opt==1);
    }

    public void Event()
    {
        gameObject.SetActive(false);
    }
}
