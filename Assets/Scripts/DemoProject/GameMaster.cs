using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameMaster : MonoBehaviour
{
    [FormerlySerializedAs("HaveSword")] public bool haveSword = false;
    [FormerlySerializedAs("InitCheckPoint")] public Vector2 initCheckPoint = Vector2.zero;

    private float _soulPoints = 0f;
    private float _diamondsPoint = 0f;
    private Vector2 _checkPoint;
    private DemoPlayer _player;
    private UIMaster _ui;

    private static GameMaster _instance;

    private void Awake()
    {
        _checkPoint = initCheckPoint;

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<DemoPlayer>();
        _ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIMaster>();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _ui.SetMaxHealth(_player.GetMaxHp());
        _ui.SetHealth(_player.GetCurrentHp());
        _ui.SetSoulsPoints(_soulPoints);
        _ui.SetDiamondsPoints(_diamondsPoint);
    }

    public void AddSoulPoints(float soulPoints)
    {
        _soulPoints += soulPoints;
    }

    public void AddDiamondPoints(float soulPoints)
    {
        _diamondsPoint += soulPoints;
    }

    public void SetCheckPoint(Vector2 checkPoint)
    {
        _checkPoint = checkPoint;
    }

    public Vector2 GetCheckPoint()
    {
        return _checkPoint;
    }

    public DemoPlayer GetPlayer()
    {
        return _player;
    } 
}