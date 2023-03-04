using UnityEngine;

public class Axe : MonoBehaviour, IWeapons
{
    private string _name = "Axe";
    private int _damage = 4;
    private int _attackSpeed = 1;
    public SpriteRenderer SR;
    public ParticleSystem PS;
    public string GetName()
    {
        return _name;
    }
    public int GetDamage()
    {
        return _damage;
    }
    public int GetAttackSpeed()
    {
        return _attackSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerAttack._actualweapon == null)
        {
            PlayerAttack._actualweapon = this;
            SR.enabled = false;
            PS.Stop();
        }
    }
}