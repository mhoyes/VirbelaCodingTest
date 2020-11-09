using UnityEngine;

[RequireComponent(typeof(Renderer))]
public abstract class BaseObject : MonoBehaviour
{
    protected Renderer _renderer;

    protected Color _originalColor;

    protected virtual void Awake()
    {
        _renderer = GetComponent<Renderer>();

        Player.OnNewClosestFound += OnNewClosestFound;

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
    protected virtual void Reset()
    {
        if (_renderer != null)
        {
            _renderer.material.color = _originalColor;
        }
    }

    /// <summary>
    /// A callback method for when a new closest object is found.
    /// </summary>
    /// <param name="obj">The new closest object.</param>
    protected virtual void OnNewClosestFound(BaseObject obj)
    {
        // Downcast this object to a BaseObject to valid if it's the same object
        if ((BaseObject)this == obj)
        {
            // We are closest, select it.
            Select();
        }
        else
        {
            // We are not closest, reset it.
            Reset();
        }
    }

    /// <summary>
    /// Called before the destruction of this object is complete.
    /// </summary>
    //protected abstract void OnDestroy();
    protected virtual void OnDestroy()
    {
        Player.OnNewClosestFound -= OnNewClosestFound;
    }

    /// <summary>
    /// Gets the position of the object.
    /// </summary>
    /// <returns>The position of the object.</returns>
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// Select this object.
    /// </summary>
    protected abstract void Select();
}
