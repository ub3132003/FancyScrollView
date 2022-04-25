 
using UnityEngine;


public class LayoutElementSO : ScriptableObject
{
    [Min(0)] [SerializeField] private bool ignoreLayout;
    [Min(0)] [SerializeField] private float minWidth;
    [Min(0)] [SerializeField] private float minHeight;
    [Min(0)] [SerializeField] private float preferredWidth;
    [Min(0)] [SerializeField] private float preferredHeight;
    [Min(0)] [SerializeField] private float flexibleWidth;
    [Min(0)] [SerializeField] private float flexibleHeight;
    [Min(0)] [SerializeField] private int layoutPriority;

    public void SetLayoutElement(UnityEngine.UI.LayoutElement layout)
    {
        layout.ignoreLayout = ignoreLayout;
        layout.minWidth = minWidth;
        layout.minHeight = minHeight;
        layout.preferredWidth = preferredWidth;
        layout.preferredHeight = preferredHeight;
        layout.flexibleWidth = flexibleWidth;
        layout.flexibleHeight = flexibleHeight;
        layout.layoutPriority = layoutPriority;
    }
}