using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : BaseObject
{
    [SerializeField]
    private Color _selectedColor = Color.green;

    /// <summary>
    /// Select this "Human" object and change its color to the selected color.
    /// </summary>
    public override void Select()
    {
        if (_renderer != null)
        {
            _renderer.material.color = _selectedColor;
        }
    }
}
