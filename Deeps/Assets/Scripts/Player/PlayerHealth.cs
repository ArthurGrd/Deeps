using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public float invulnerabilityTime;
    
    public int _currentHealth { get; set; }
    private int _framesOfInvulnerability;
    private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        _spriteRenderer = GameObject.Find("Player").GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        _currentHealth = maxHealth;
        _framesOfInvulnerability = 0;
    }

    public void TakeDamage(int damage)
    {
        if (_framesOfInvulnerability==0)
        {
            _currentHealth -= damage;
            _framesOfInvulnerability = (int)(invulnerabilityTime * 120);
        }
    }

    private void Update()
    {
        if (_framesOfInvulnerability != 0)
        {
            _framesOfInvulnerability -= 1;
            _spriteRenderer.color =
                new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b,(_framesOfInvulnerability%2)*255);
        }
        else
        {
            _spriteRenderer.color =
                new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b,255);
        }
    }
}
