using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image refHearth;

    private Image[] _hearths;
    private PlayerHealth _playerHealth;


    private void Start()
    {
        _playerHealth = GameObject.Find("Player").GetComponent(typeof(PlayerHealth)) as PlayerHealth;
        _hearths = new Image[_playerHealth.maxHealth];
        for (int i = 0; i < _hearths.Length; i++)
        {
            _hearths[i] = Instantiate(refHearth, transform.position, Quaternion.identity);
            _hearths[i].transform.SetParent(transform);
            _hearths[i].transform.localScale = new Vector3(1, 1, 1);
            _hearths[i].transform.localPosition = new Vector3(transform.position.x + (i * 70), transform.position.y,transform.position.z);
            _hearths[i].name = "Hearth N" + i;
        }
    }

    private void Update()
    {
        for (int i = 0; i < _hearths.Length; i++)
        {
            if (i < _playerHealth._currentHealth)
                _hearths[i].enabled = true;
            else
                _hearths[i].enabled = false;
        }    
    }
}
