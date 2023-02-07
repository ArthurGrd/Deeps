using UnityEngine;

public class Rapier : MonoBehaviour, IWeapons
{
    private string _name = "Rapier";
    private int _damage = 1;
    private int _attackSpeed = 4;
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
        }
    }
}