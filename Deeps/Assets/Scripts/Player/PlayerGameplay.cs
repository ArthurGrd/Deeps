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
    public static GameObject _actualSpawn;
    public static Scenes _actualScene;
    
    private PlayerHealth _playerHealth;
    private PlayerProgress _playerProgress;
    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;
    private PauseMenu _pauseMenu;


    private void Start()
    {
        _playerProgress = GameObject.Find("Player").GetComponent(typeof(PlayerProgress)) as PlayerProgress;
        _playerHealth = GameObject.Find("Player").GetComponent(typeof(PlayerHealth)) as PlayerHealth;
        _playerAttack = GameObject.Find("Player").GetComponent(typeof(PlayerAttack)) as PlayerAttack;
        _playerMove = GameObject.Find("Player").GetComponent(typeof(PlayerMove)) as PlayerMove;
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
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _playerAttack.Attack();
            }
            _playerMove.UpdateMove();   
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
