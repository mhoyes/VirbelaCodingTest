using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _thingsParent;

    [SerializeField]
    private Transform _botsParent;

    private List<BaseObject> _objects;
    private Vector3 _previousPosition;

    private BaseObject _previousClosestObject;
    private BaseObject _closestObject;

    // Use this to broadcast a new Object was found.
    public static Action<BaseObject> OnNewClosestFound;

    private void Awake()
    {
        _previousPosition = transform.position;
        _previousClosestObject = null;
        _objects = new List<BaseObject>();

        InitializeObjects();
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
    /// Initialize any edit-time objects
    /// </summary>
    private void InitializeObjects()
    {
        InitializeThings();
        InitializeBots();
    }

    /// <summary>
    /// Initialize any edit-time "Thing" objects under "_thingsParent"
    /// </summary>
    private void InitializeThings()
    {
        if (_thingsParent != null)
        {
            foreach (Transform obj in _thingsParent)
            {
                Thing thing = obj.GetComponent<Thing>();

                if (thing != null)
                {
                    _objects.Add(thing);
                }
            }
        }
        else
        {
            Debug.LogError("Unable to initialize any Thing objects. '_thingsParent' must be assigned.");
        }
    }

    /// <summary>
    /// Initialize any edit-time "Bot" objects under "_botsParent"
    /// </summary>
    private void InitializeBots()
    {
        if (_botsParent != null)
        {
            foreach (Transform obj in _botsParent)
            {
                Bot bot = obj.GetComponent<Bot>();

                if (bot != null)
                {
                    _objects.Add(bot);
                }
            }
        }
        else
        {
            Debug.LogError("Unable to initialize any Bot objects. '_botsParent' must be assigned.");
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
        if (_objects != null || _objects.Count > 0)
        {
            float closest = Mathf.Infinity;
            BaseObject closestObj = null;

            foreach (BaseObject obj in _objects)
            {
                float distance = Vector3.Distance(transform.position, obj.GetPosition());

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
                // Check for null, and invoke if it exists
                OnNewClosestFound?.Invoke(_closestObject);
            }
        }
    }

    ///// <summary>
    ///// Add a new "Thing" object to the list.
    ///// </summary>
    public void AddThing(GameObject obj)
    {
        obj.name = "Thing";
        obj.transform.parent = _thingsParent;

        Thing thing = obj.AddComponent<Thing>();

        _objects.Add(thing);
    }

    ///// <summary>
    ///// Add a new "Bot" object to the list.
    ///// </summary>
    public void AddBot(GameObject obj)
    {
        obj.name = "Bot";
        obj.transform.parent = _botsParent;

        Bot bot = obj.AddComponent<Bot>();

        _objects.Add(bot);
    }
}
