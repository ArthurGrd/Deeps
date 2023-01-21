using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public PlayerGameplay.Scenes sceneToLoad;
    
    private PlayerProgress playerProgress;
    private PlayerGameplay playerGameplay;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerProgress = GameObject.Find("Player").GetComponent(typeof(PlayerProgress)) as PlayerProgress;
        playerGameplay = GameObject.Find("Player").GetComponent(typeof(PlayerGameplay)) as PlayerGameplay;

        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad.ToString());
            playerGameplay.setActualScene(sceneToLoad);
            
            switch (sceneToLoad.ToString())
            {
                case "Spawn":
                    playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Spawn);
                    break;
                case "Parkour1":
                    playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour1);
                    break;
                case "Parkour2":
                    playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour2);
                    break;
                case "Parkour3":
                    playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour3);
                    break;
                case "Parkour4":
                    playerProgress.SetCurrentProgressZone(PlayerProgress.ProgressZones.Parkour4);
                    break;
            }
            
        }
    }
}
