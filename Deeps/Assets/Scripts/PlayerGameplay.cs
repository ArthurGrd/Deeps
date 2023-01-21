using UnityEngine;

public class PlayerGameplay : MonoBehaviour
{
    public enum Scenes
    {
        Spawn,
        Boss1,
        Parkour1,
        Boss2,
        Parkour2,
        Boss3,
        Parkour3,
        Boss4,
        Parkour4,
        BossF
    }

    private GameObject _actualSpawn;
    private Scenes _actualScene;
    private PlayerHealth playerHealth;
    private PlayerProgress playerProgress;

    
    //-------------GETTERS-SETTERS-------------
    public void setActualScene(Scenes scene) { _actualScene = scene; }
    public Scenes getActualScene() { return _actualScene; }
    public void setActualSpawn(GameObject spawn) { _actualSpawn = spawn; }
    public GameObject getActualSpawn() { return _actualSpawn; }
    //----------------------------------------

    
    private void Start()
    {
        playerProgress = GameObject.Find("Player").GetComponent(typeof(PlayerProgress)) as PlayerProgress;
        playerHealth = GameObject.Find("Player").GetComponent(typeof(PlayerHealth)) as PlayerHealth;
        _actualScene = Scenes.Spawn;
        transform.position = _actualSpawn.transform.position;
    }

    private void Update()
    {
        /*
         * ----Inputs---
         */
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerHealth.TakeDamage(1);
        }
        
        /*
         * ----Death---
         */
        if (playerHealth.GetCurrentHealth() <= 0)
        {
            transform.localPosition = _actualSpawn.transform.position;
            playerHealth.SetCurrentHealth(playerHealth.maxHealth);
        }
    }
}
