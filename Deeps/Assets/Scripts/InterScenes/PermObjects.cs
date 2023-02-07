using UnityEngine;

public class PermObjects : MonoBehaviour
{
    public GameObject[] objectsToKeep;

    private PlayerProgress _playerProgress;
    private CameraFollow _cameraFollow;
    private BossManager _bossManager;
    private GameObject _sliderProgress;
    private GameObject _sliderBoss;

    public static bool _needToUpdate = false;
    
    private void Start()
    {
        _playerProgress = GameObject.Find("Player").GetComponent(typeof(PlayerProgress)) as PlayerProgress;
        _cameraFollow = GameObject.FindWithTag("MainCamera").GetComponent(typeof(CameraFollow)) as CameraFollow;
        _bossManager = GetComponent(typeof(BossManager)) as BossManager;
        _sliderProgress = GameObject.Find("SliderProgress");
        _sliderBoss = GameObject.Find("SliderBoss");
        _sliderProgress.SetActive(true);
        _sliderBoss.SetActive(false);
    }

    private void Awake()
    {
        foreach (GameObject gameObject in objectsToKeep)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (_needToUpdate)
        {
            if (PlayerGameplay._actualScene.ToString() == _playerProgress.GetCurrentProgressZone().ToString())
            {
                if (PlayerGameplay._actualScene == PlayerGameplay.Scenes.Spawn)
                {
                    _cameraFollow.CamPlacement(false);
                }
                else
                {
                    _cameraFollow.transform.position =
                        new Vector3(_cameraFollow.camPos[0], _cameraFollow.camPos[1], -10); // A EDIT QUAND CHANGER LA CAMERA DE PLACE
                    _cameraFollow.CamPlacement(true);
                }
                _sliderProgress.SetActive(true);
                _sliderBoss.SetActive(false);
            }
            else
            {
                _cameraFollow.CamPlacement(false);
                _sliderProgress.SetActive(false);
                _sliderBoss.SetActive(true);
                _bossManager.UpdateCurrentBoss();
            }
            _needToUpdate = false;
        }
    }
}
