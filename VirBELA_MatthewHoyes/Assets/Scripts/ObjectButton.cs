using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class ObjectButton
{
    [SerializeField]
    public Button NewButton;

    [SerializeField]
    public Transform ObjectParent;

    /// <summary>
    /// Add a listener with a specific UnityAction callback
    /// </summary>
    /// <param name="action"></param>
    public void AddButtonListener(UnityAction action)
    {
        if (NewButton != null)
        {
            NewButton.onClick.AddListener(action);
        }
        else
        {
            Debug.LogWarning("No button has been assigned to add new objects. Please assign it.");
        }
    }

    /// <summary>
    /// Remove all listeners from the given button.
    /// </summary>
    public void RemoveListeners()
    {
        if (NewButton != null)
        {
            NewButton.onClick.RemoveAllListeners();
        }
    }
}
