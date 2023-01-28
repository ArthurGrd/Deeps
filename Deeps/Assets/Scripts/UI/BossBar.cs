using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    private Slider _slider;
    private BossManager _bossManager;
    
    private void Start()
    {
        _bossManager = GameObject.Find("GameManager").GetComponent(typeof(BossManager)) as BossManager;
        _slider = GameObject.Find("SliderBoss").GetComponent(typeof(Slider)) as Slider;
    }
    private void Update()
    {
        if (_bossManager.GetCurrentBoss()!=null)
        {
            _slider.value = _bossManager.GetCurrentBoss().GetHealth();
        }
    }
    
    public void InitializeBossBar()
    {
        _slider.maxValue = _bossManager.GetCurrentBoss().GetHealth();
    }
}
