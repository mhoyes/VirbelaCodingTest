using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _previousPosition;

    private BaseObject _previousClosestObject;
    private BaseObject _closestObject;

    [SerializeField]
    private ObjectsManager _objectsManager;

    private void Awake()
    {
        _previousPosition = transform.position;
        _previousClosestObject = null;

        if (_objectsManager == null)
        {
            Debug.LogError("ObjectManager is not assigned in Player. Please assign.");
        }
    }

    private void Start()
    {
        ObjectsManager.OnNewObjectAdded += OnNewObjectAdded;

        _objectsManager.SetTargetToCreateObjectsNear(transform);

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

    private void OnDestroy()
    {
        ObjectsManager.OnNewObjectAdded -= OnNewObjectAdded;
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
        if (_objectsManager == null)
            return;

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

    private void OnNewObjectAdded()
    {
        FindClosestObject();
    }
}
