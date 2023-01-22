using UnityEngine;
using UnityEngine.UI;

public class BossGoblin : MonoBehaviour
{
    public int damage;
    public Sprite barAfter;
    
    private PlayerHealth _playerHealth;
    private GameObject _mapBeforeBoss;
    private GameObject _mapAfterBoss;
    private Image _sliderUI;


    private void Awake()
    {
        _playerHealth = GameObject.Find("Player").GetComponent(typeof(PlayerHealth)) as PlayerHealth;
        _mapBeforeBoss = GameObject.FindWithTag("MapBeforeBoss");
        _mapAfterBoss = GameObject.FindWithTag("MapAfterBoss");
        _sliderUI = GameObject.Find("BackgroundSliderUI").GetComponent(typeof(Image)) as Image;
        
        _mapBeforeBoss.SetActive(true);
        _mapAfterBoss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerHealth.TakeDamage(damage);
        }
        BossDeath();
    }

    private void BossDeath()
    {
        gameObject.SetActive(false);
        _mapBeforeBoss.SetActive(false);
        _mapAfterBoss.SetActive(true);
        _sliderUI.sprite = barAfter;
    }
}
