using UnityEngine;

[RequireComponent(typeof(Renderer))]
public abstract class BaseObject : MonoBehaviour
{
    protected Renderer _renderer;

    protected Color _originalColor;

    public Vector3 Position { get { return transform.position; } }

    protected virtual void Awake()
    {
        _renderer = GetComponent<Renderer>();

        Initialize();
    }

    /// <summary>
    /// Initialize the object.
    /// </summary>
    protected virtual void Initialize()
    {
        // Do any initialization here
        if (_renderer != null)
        {
            _originalColor = _renderer.material.color;
        }
    }

    /// <summary>
    /// Resets the Renderer color back to its original color
    /// </summary>
    public virtual void Reset()
    {
        if (_renderer != null)
        {
            _renderer.material.color = _originalColor;
        }
    }

    /// <summary>
    /// Select this object.
    /// </summary>
    public abstract void Select();
}
