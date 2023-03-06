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
    private PlayerHealth _playerHealth;
    private PlayerProgress _playerProgress;
    private PlayerMovement _playerMove;
    private PlayerAttack _playerAttack;
    private PauseMenu _pauseMenu;


    //-------------GETTERS-SETTERS-------------
    public void setActualScene(Scenes scene) { _actualScene = scene; }
    public Scenes getActualScene() { return _actualScene; }
    public void setActualSpawn(GameObject spawn) { _actualSpawn = spawn; }
    public GameObject getActualSpawn() { return _actualSpawn; }

    public AudioSource Attack;
    //----------------------------------------

    
    private void Start()
    {
        _playerProgress = GameObject.Find("Player").GetComponent(typeof(PlayerProgress)) as PlayerProgress;
        _playerHealth = GameObject.Find("Player").GetComponent(typeof(PlayerHealth)) as PlayerHealth;
        _playerAttack = GameObject.Find("Player").GetComponent(typeof(PlayerAttack)) as PlayerAttack;
        _playerMove = GameObject.Find("Player").GetComponent(typeof(PlayerMovement)) as PlayerMovement;
        _pauseMenu = GameObject.Find("OnScreen").GetComponent(typeof(PauseMenu)) as PauseMenu;
        _actualScene = Scenes.Spawn;
        transform.position = _actualSpawn.transform.position;
    }

    private void Update()
    {
        /*
         * ----Inputs---
         */

        _pauseMenu.UpdatePauseMenu();
        if (!_pauseMenu.GetIsGamePaused())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _playerHealth.TakeDamage(1);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && _playerAttack.GetWeapon() != null)
            {
                _playerAttack.Attack();
            }
            _playerMove.Update();   
        }


        /*
         * ----Death---
         */
        if (_playerHealth._currentHealth <= 0)
        {
            transform.localPosition = _actualSpawn.transform.position;
            _playerHealth._currentHealth = _playerHealth.maxHealth;
        }
    }
}
