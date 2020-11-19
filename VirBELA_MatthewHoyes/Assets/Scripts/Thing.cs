using UnityEngine;

public class Thing : BaseObject
{
    [SerializeField]
    private Color _selectedColor = Color.red;

    /// <summary>
    /// Select this "Thing" object and change its color to the selected color.
    /// </summary>
    public override void Select()
    {
        if (_renderer != null)
        {
            _renderer.material.color = _selectedColor;
        }
    }
}
