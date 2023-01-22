using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    
    private int _currentHealth;

    
    //-------------GETTERS-SETTERS-------------
    public int GetCurrentHealth() { return _currentHealth; } 
    public void SetCurrentHealth(int value) { _currentHealth = value; }
    //----------------------------------------


    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage>_currentHealth)
        {
            _currentHealth = 0;
        }
        else
        {
            _currentHealth -= damage;
        }
    }
}
