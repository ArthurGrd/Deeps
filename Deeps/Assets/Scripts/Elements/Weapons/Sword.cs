using System;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapons
{
    private string _name = "Sword";
    private int _damage = 2;
    private int _attackSpeed = 2;
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