using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class AddObjects : MonoBehaviour
{
    [SerializeField]
    private Button _addNewThingButton;

    [SerializeField]
    private Button _addNewBotButton;

    [SerializeField]
    private Button _addNewHumanButton;

    [SerializeField]
    private Transform _thingsParent;

    [SerializeField]
    private Transform _botsParent;

    [SerializeField]
    private Transform _humanParent;

    // This will be used to add new Things at a random position within this range from the player.
    [SerializeField]
    private float _rangeFromPlayerToAdd = 20;

    private Player _player;

    public List<BaseObject> ObjectsList { get; private set; }

    private void Awake()
    {
        _player = GetComponent<Player>();

        ObjectsList = new List<BaseObject>();

        InitializeButtons();

        InitializeObjects();
    }

    private void OnDestroy()
    {
        ResetButtons();
    }

    private void InitializeButtons()
    {
        if (_addNewThingButton != null)
        {
            _addNewThingButton.onClick.AddListener(AddThing);
        }
        else
        {
            Debug.LogWarning("No button has been assigned to add new Thing objects. Please assign it.");
        }

        if (_addNewBotButton != null)
        {
            _addNewBotButton.onClick.AddListener(AddBot);
        }
        else
        {
            Debug.LogWarning("No button has been assigned to add new Bot objects. Please assign it.");
        }

        if (_addNewHumanButton != null)
        {
            _addNewHumanButton.onClick.AddListener(AddHuman);
        }
        else
        {
            Debug.LogWarning("No button has been assigned to add new Human objects. Please assign it.");
        }
    }

    private void ResetButtons()
    {
        if (_addNewThingButton != null)
        {
            _addNewThingButton.onClick.RemoveAllListeners();
        }

        if (_addNewBotButton != null)
        {
            _addNewBotButton.onClick.RemoveAllListeners();
        }

        if (_addNewHumanButton != null)
        {
            _addNewHumanButton.onClick.RemoveAllListeners();
        }
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
        InitializeObjects<Thing>(_thingsParent);
        InitializeObjects<Bot>(_botsParent);
        InitializeObjects<Human>(_humanParent);
    }

    private GameObject CreateObject(PrimitiveType type)
    {
        GameObject obj = GameObject.CreatePrimitive(type);
        obj.transform.position = new Vector3(UnityEngine.Random.Range(transform.position.x - _rangeFromPlayerToAdd, transform.position.x + _rangeFromPlayerToAdd),
                                             0,
                                             UnityEngine.Random.Range(transform.position.z - _rangeFromPlayerToAdd, transform.position.z + _rangeFromPlayerToAdd));

        return obj;
    }

    /// <summary>
    /// Responsible for adding a given type of BaseObject to the list.
    /// </summary>
    /// <typeparam name="T">The type of object, of type BaseObject.</typeparam>
    /// <param name="obj">The GameObject created to add the type of object to.</param>
    /// <param name="parent">The parent object to the type of object.</param>
    public void AddObject<T>(GameObject obj, Transform parent) where T : BaseObject
    {
        T type = obj.gameObject.AddComponent<T>();

        obj.transform.parent = parent;
        obj.name = typeof(T).ToString();

        ObjectsList.Add(type);
    }

    /// <summary>
    /// Create a new GameObject of type Cube in a random area within a specific range of the Players current location.
    /// </summary>
    private void AddThing()
    {
        GameObject obj = CreateObject(PrimitiveType.Cube);
        AddObject<Thing>(obj, _thingsParent);

        _player.OnNewObjectAdded();
    }

    /// <summary>
    /// Create a new GameObject of type Capsule in a random area within a specific range of the Players current location.
    /// </summary>
    private void AddBot()
    {
        GameObject obj = CreateObject(PrimitiveType.Capsule);
        AddObject<Bot>(obj, _botsParent);

        _player.OnNewObjectAdded();
    }

    /// <summary>
    /// Create a new GameObject of type Capsule in a random area within a specific range of the Players current location.
    /// </summary>
    private void AddHuman()
    {
        GameObject obj = CreateObject(PrimitiveType.Cylinder);
        AddObject<Human>(obj, _humanParent);

        _player.OnNewObjectAdded();
    }
}
