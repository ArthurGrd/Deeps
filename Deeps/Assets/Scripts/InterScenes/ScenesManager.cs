using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public PlayerGameplay.Scenes sceneToLoad;
    
    private PlayerProgress _playerProgress;
    private PlayerGameplay _playerGameplay;
    private PermObjects _permObjects;
    
    private void Start()
    {
        _playerProgress = GameObject.Find("Player").GetComponent(typeof(PlayerProgress)) as PlayerProgress;
        _playerGameplay = GameObject.Find("Player").GetComponent(typeof(PlayerGameplay)) as PlayerGameplay;
        _permObjects = GameObject.Find("GameManager").GetComponent(typeof(PermObjects)) as PermObjects;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad.ToString());
            _playerGameplay.setActualScene(sceneToLoad);
            switch (sceneToLoad.ToString())
            {
                case "Spawn":
                    _playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Spawn);
                    break;
                case "Parkour1":
                    _playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour1);
                    break;
                case "Parkour2":
                    _playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour2);
                    break;
                case "Parkour3":
                    _playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour3);
                    break;
                case "Parkour4":
                    _playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour4);
                    break;
            }
            _permObjects.SetNeedToUpdate(true);
        }
    }
}
