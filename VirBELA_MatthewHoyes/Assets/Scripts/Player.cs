using UnityEngine;

[RequireComponent(typeof(AddObjects))]
public class Player : MonoBehaviour
{
    private Vector3 _previousPosition;

    private BaseObject _previousClosestObject;
    private BaseObject _closestObject;

    private AddObjects _objectsManager;

    private void Awake()
    {
        _previousPosition = transform.position;
        _previousClosestObject = null;

        _objectsManager = GetComponent<AddObjects>();
    }

    private void Start()
    {
        // Do it initially to find the first closest.
        FindClosestObject();
    }

    private void Update()
    {
        // Position changed
        if (transform.position != _previousPosition)
        {
            OnPositionChanged();
        }
    }

    /// <summary>
    /// Players position has changed.
    /// Time to find a new closest object.
    /// </summary>
    private void OnPositionChanged()
    {
        _previousPosition = transform.position;

        FindClosestObject();
    }

    /// <summary>
    /// Find the closest object to the Player, then
    /// </summary>
    private void FindClosestObject()
    {
        if (_objectsManager.ObjectsList != null || _objectsManager.ObjectsList.Count > 0)
        {
            float closest = Mathf.Infinity;
            BaseObject closestObj = null;

            foreach (BaseObject obj in _objectsManager.ObjectsList)
            {
                float distance = Vector3.Distance(transform.position, obj.Position);

                if (distance < closest)
                {
                    closest = distance;
                    closestObj = obj;
                }
            }

            _previousClosestObject = _closestObject;

            _closestObject = closestObj;

            // Ensure a new closest was found
            if (_closestObject != null && _closestObject != _previousClosestObject)
            {
                // Ensure previous isn't null before resetting
                _previousClosestObject?.Reset();

                // Select the new closest
                _closestObject.Select();
            }
        }
    }

    public void OnNewObjectAdded()
    {
        FindClosestObject();
    }
}
