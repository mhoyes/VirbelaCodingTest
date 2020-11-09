
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class AddObjects : MonoBehaviour
{
    [SerializeField]
    private Button _addNewThingButton;

    [SerializeField]
    private Button _addNewBotButton;

    // This will be used to add new Things at a random position within this range from the player.
    [SerializeField]
    private float _rangeFromPlayerToAdd = 20;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();

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
    }

    private void OnDestroy()
    {
        if (_addNewThingButton != null)
        {
            _addNewThingButton.onClick.RemoveAllListeners();
        }

        if (_addNewBotButton != null)
        {
            _addNewBotButton.onClick.RemoveAllListeners();
        }
    }

    private GameObject CreateObject(PrimitiveType type)
    {
        GameObject obj = GameObject.CreatePrimitive(type);
        obj.transform.position = new Vector3(Random.Range(transform.position.x - _rangeFromPlayerToAdd, transform.position.x + _rangeFromPlayerToAdd),
                                             0,
                                             Random.Range(transform.position.z - _rangeFromPlayerToAdd, transform.position.z + _rangeFromPlayerToAdd));

        return obj;
    }

    /// <summary>
    /// Create a new GameObject of type Cube in a random area within a specific range of the Players current location.
    /// </summary>
    private void AddThing()
    {
        GameObject obj = CreateObject(PrimitiveType.Cube);
        _player.AddThing(obj);
    }

    /// <summary>
    /// Create a new GameObject of type Capsule in a random area within a specific range of the Players current location.
    /// </summary>
    private void AddBot()
    {
        GameObject obj = CreateObject(PrimitiveType.Capsule);
        _player.AddBot(obj);
    }
}
