using UnityEngine;
using UnityEngine.UI;

public interface IBoss
{
    public int GetHealth();
}

public class BossManager : MonoBehaviour
{
    private IBoss _currentBoss;
    private BossBar _bossBar;
    private GameObject _mapBeforeBoss;
    private GameObject _mapAfterBoss;
    private Image _backProgressBar;
    private Image _backBossBar;


    public IBoss GetCurrentBoss()
    {
        return _currentBoss;
    }
    
    private void Awake()
    {
        _backBossBar = GameObject.Find("BossBarBack").GetComponent(typeof(Image)) as Image;
        _backProgressBar = GameObject.Find("ProgressBarBack").GetComponent(typeof(Image)) as Image;
        _bossBar = GameObject.Find("SliderBoss").GetComponent(typeof(BossBar)) as BossBar;
    }

    public void UpdateCurrentBoss()
    {
        
        GameObject boss = GameObject.FindWithTag("Boss");
        switch (boss.name)
        {
            case "Goblin":
                _currentBoss = boss.GetComponent(typeof(BossGoblin)) as BossGoblin;
                break;
            case "King":
                _currentBoss = boss.GetComponent(typeof(BossKing)) as BossKing;
                break;
            case "Slime":
                _currentBoss = boss.GetComponent(typeof(BossSlime)) as BossSlime;
                break;
            case "Skeleton":
                _currentBoss = boss.GetComponent(typeof(BossSkeleton)) as BossSkeleton;
                break;
            case "Zombie":
                _currentBoss = boss.GetComponent(typeof(BossZombie)) as BossZombie;
                break;
        }
        _bossBar.InitializeBossBar();
    }



    public void InitializeBoss(Sprite backBossBar)
    {
        _mapBeforeBoss = GameObject.FindWithTag("MapBeforeBoss");
        _mapAfterBoss = GameObject.FindWithTag("MapAfterBoss");
        _mapBeforeBoss.SetActive(true);
        _mapAfterBoss.SetActive(false);
        _backBossBar.sprite = backBossBar;
    }

    public void BossDeath(GameObject boss, Sprite nextProgressBar)
    {
        boss.SetActive(false);
        _mapBeforeBoss.SetActive(false);
        _mapAfterBoss.SetActive(true);
        _backProgressBar.sprite = nextProgressBar;
        _currentBoss = null;
    }
}