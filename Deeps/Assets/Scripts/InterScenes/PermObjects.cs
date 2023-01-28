using UnityEngine;

public class PermObjects : MonoBehaviour
{
    public GameObject[] objectsToKeep;

    private PlayerGameplay _playerGameplay;
    private PlayerProgress _playerProgress;
    private CameraFollow _cameraFollow;
    private BossManager _bossManager;
    private GameObject _sliderProgress;
    private GameObject _sliderBoss;

    private bool _needToUpdate = false;

    public void SetNeedToUpdate(bool value) { _needToUpdate = value;}

    private void Start()
    {
        _playerGameplay = GameObject.Find("Player").GetComponent(typeof(PlayerGameplay)) as PlayerGameplay;
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
            if (_playerGameplay.getActualScene().ToString() == _playerProgress.GetCurrentProgressZone().ToString())
            {
                if (_playerGameplay.getActualScene() == PlayerGameplay.Scenes.Spawn)
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
