using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoScale()
    {
        transform.DOScale(1.1f, 0.3f).From(1f);
    }

    public void DoMove()
    {
        transform.DOLocalMoveX(1f, 1f);
    }
}
