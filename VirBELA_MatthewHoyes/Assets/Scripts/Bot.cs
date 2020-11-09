using UnityEngine;

public class Bot : BaseObject
{
    [SerializeField]
    private Color _selectedColor = Color.blue;

    /// <summary>
    /// Select this "Bot" object and change its color to the selected color.
    /// </summary>
    protected override void Select()
    {
        if (_renderer != null)
        {
            _renderer.material.color = _selectedColor;
        }
    }
}
