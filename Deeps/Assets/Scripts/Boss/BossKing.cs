using UnityEngine;

public class BossKing : MonoBehaviour, IBoss
{
    public Sprite nextProgressBar;
    public Sprite bossBar;

    private int _maxHealth = 30;
    private int _attackDamage = 4;
    private BossManager _bossManager;
    private PlayerHealth _playerHealth;
    private PlayerAttack _playerAttack;
    private int _health;
    
    private void Awake()
    {
        _bossManager = GameObject.Find("GameManager").GetComponent(typeof(BossManager)) as BossManager;
        _playerHealth = GameObject.Find("Player").GetComponent(typeof(PlayerHealth)) as PlayerHealth;
        _playerAttack = GameObject.Find("Player").GetComponent(typeof(PlayerAttack)) as PlayerAttack;
        _bossManager.InitializeBoss(bossBar);
        _health = _maxHealth;
    }

    public int GetHealth()
    {
        return _health;
    }
    

    public void TakeDamage(int damage)
    {
        if (_health>0)
        {
            _health -= damage;
        }
    }
    
    private void Update()
    {
        if (_health<=0)
        {
            _bossManager.BossDeath(gameObject,nextProgressBar);
        }
    }
}