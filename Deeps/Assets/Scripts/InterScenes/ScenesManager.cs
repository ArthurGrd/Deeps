using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public PlayerGameplay.Scenes sceneToLoad;
    
    private PlayerProgress playerProgress;
    private PlayerGameplay playerGameplay;
    private CameraFollow _cameraFollow;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerProgress = GameObject.Find("Player").GetComponent(typeof(PlayerProgress)) as PlayerProgress;
        playerGameplay = GameObject.Find("Player").GetComponent(typeof(PlayerGameplay)) as PlayerGameplay;
        _cameraFollow = GameObject.FindWithTag("MainCamera").GetComponent(typeof(CameraFollow)) as CameraFollow;

        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad.ToString());
            playerGameplay.setActualScene(sceneToLoad);
            
            switch (sceneToLoad.ToString())
            {
                case "Spawn":
                    playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Spawn);
                    _cameraFollow.CamPlacement(false);
                    break;
                case "Boss1":
                    _cameraFollow.CamPlacement(false);
                    break;
                case "Parkour1":
                    playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour1);
                    _cameraFollow.CamPlacement(true);
                    _cameraFollow.transform.position =
                        new Vector3(_cameraFollow.camPos[0], _cameraFollow.camPos[1], -10);
                    break;
                case "Boss2":
                    _cameraFollow.CamPlacement(false);
                    break;
                case "Parkour2":
                    playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour2);
                    _cameraFollow.CamPlacement(true);
                    _cameraFollow.transform.position =
                        new Vector3(_cameraFollow.camPos[2], _cameraFollow.camPos[3],-10);
                    break;
                case "Boss3":
                    _cameraFollow.CamPlacement(false);
                    break;
                case "Parkour3":
                    playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour3);
                    _cameraFollow.CamPlacement(true);
                    _cameraFollow.transform.position =
                        new Vector3(_cameraFollow.camPos[4], _cameraFollow.camPos[5],-10);
                    break;
                case "Boss4":
                    _cameraFollow.CamPlacement(false);
                    break;
                case "Parkour4":
                    playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour4);
                    _cameraFollow.CamPlacement(true);
                    _cameraFollow.transform.position =
                        new Vector3(_cameraFollow.camPos[6], _cameraFollow.camPos[7],-10);
                    break;
                case "BossF":
                    _cameraFollow.CamPlacement(false);
                    break;
            }
            
        }
    }
}
