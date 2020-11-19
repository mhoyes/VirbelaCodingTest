using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsManager : MonoBehaviour
{
    [SerializeField]
    private ObjectButton _thing;

    [SerializeField]
    private ObjectButton _bot;

    [SerializeField]
    private ObjectButton _human;

    // This will be used to add new objects at a random position within this range from the target.
    [SerializeField]
    private float _rangeFromPlayerToAdd = 20;

    private Transform _target;

    // Use this to broadcast a new Object was found.
    public static Action OnNewObjectAdded;

    public List<BaseObject> ObjectsList { get; private set; }

    public void SetTargetToCreateObjectsNear(Transform target)
    {
        _target = target;
    }

    private void Awake()
    {
        ObjectsList = new List<BaseObject>();

        InitializeButtons();

        InitializeObjects();
    }

    private void OnDestroy()
    {
        ResetButtons();
    }

    /// <summary>
    /// Initialize all buttons
    /// </summary>
    private void InitializeButtons()
    {
        _thing.AddButtonListener(AddThing);
        _bot.AddButtonListener(AddBot);
        _human.AddButtonListener(AddHuman);
    }

    /// <summary>
    /// Resets all buttons.
    /// </summary>
    private void ResetButtons()
    {
        _thing.RemoveListeners();
        _bot.RemoveListeners();
        _human.RemoveListeners();
    }

    /// <summary>
    /// Initialize any edit-time objects under a given parent
    /// </summary>
    /// <typeparam name="T">The type of object, of type BaseObject.</typeparam>
    /// <param name="parent">The parent object to the type of object.</param>
    private void InitializeObjects<T>(Transform parent) where T : BaseObject
    {
        if (parent != null)
        {
            foreach (Transform obj in parent)
            {
                T type = obj.GetComponent<T>();

                if (type != null)
                {
                    ObjectsList.Add(type);
                }
            }
        }
        else
        {
            string type = typeof(T).ToString();
            Debug.LogError($"Unable to initialize any {type} objects. The parent object must be assigned.");
        }
    }

    /// <summary>
    /// Initialize any edit-time objects
    /// </summary>
    private void InitializeObjects()
    {
        InitializeObjects<Thing>(_thing.ObjectParent);
        InitializeObjects<Bot>(_bot.ObjectParent);
        InitializeObjects<Human>(_human.ObjectParent);
    }

    /// <summary>
    /// Create a GameObject of a specific Primitive Type, and randomize the position within a specific range.
    /// </summary>
    /// <param name="type">The Primitive Type of object</param>
    /// <returns>The GameObject created.</returns>
    private GameObject CreateObject(PrimitiveType type)
    {
        GameObject obj = GameObject.CreatePrimitive(type);
        Vector3 pos = (_target == null) ? transform.position : _target.position;

        obj.transform.position = new Vector3(UnityEngine.Random.Range(pos.x - _rangeFromPlayerToAdd, pos.x + _rangeFromPlayerToAdd),
                                             0,
                                             UnityEngine.Random.Range(pos.z - _rangeFromPlayerToAdd, pos.z + _rangeFromPlayerToAdd));

        return obj;
    }

    /// <summary>
    /// Responsible for adding a given type of BaseObject to the list.
    /// </summary>
    /// <typeparam name="T">The type of object, of type BaseObject.</typeparam>
    /// <param name="obj">The GameObject created to add the type of object to.</param>
    /// <param name="parent">The parent object to the type of object.</param>
    private void AddObject<T>(GameObject obj, Transform parent) where T : BaseObject
    {
        T type = obj.gameObject.AddComponent<T>();

        obj.transform.parent = parent;
        obj.name = typeof(T).ToString();

        ObjectsList.Add(type);

        OnNewObjectAdded?.Invoke();
    }

    /// <summary>
    /// Create a new GameObject of type Cube in a random area within a specific range of the Players current location.
    /// </summary>
    private void AddThing()
    {
        GameObject obj = CreateObject(PrimitiveType.Cube);
        AddObject<Thing>(obj, _thing.ObjectParent);
    }

    /// <summary>
    /// Create a new GameObject of type Capsule in a random area within a specific range of the Players current location.
    /// </summary>
    private void AddBot()
    {
        GameObject obj = CreateObject(PrimitiveType.Capsule);
        AddObject<Bot>(obj, _bot.ObjectParent);
    }

    /// <summary>
    /// Create a new GameObject of type Capsule in a random area within a specific range of the Players current location.
    /// </summary>
    private void AddHuman()
    {
        GameObject obj = CreateObject(PrimitiveType.Cylinder);
        AddObject<Human>(obj, _human.ObjectParent);
    }
}
