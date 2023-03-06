using System;
using UnityEngine;

public interface IWeapons
{
    public int GetDamage();
    public string GetName();
    public int GetAttackSpeed();
}


public class PlayerAttack : MonoBehaviour
{
    public AudioSource attack;
    private Animator _animator;
    public static IWeapons _actualweapon =  null;
    public Transform Attackr;
    public LayerMask ennemilayer;

    private int _attackDelay = 0;

    private void Start()
    {
        _animator = GetComponent(typeof(Animator)) as Animator;
    }

    public IWeapons GetWeapon()
    {
        return _actualweapon;
    }

    public void Attack()
    {
        if (_attackDelay==0 && _actualweapon!=null)
        {
            _animator.SetTrigger("Attack");

            _attackDelay = 120 / _actualweapon.GetAttackSpeed();
            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Attackr.position,1f,ennemilayer);
            
            foreach (var VARIABLE in hitEnemies)
            {
                VARIABLE.GetComponent<BossGoblin>().TakeDamage(_actualweapon.GetDamage());
            }
            attack.Play();
        }
    }

    private void FixedUpdate()
    {
        if (_attackDelay!=0)
        {
            _attackDelay -= 1;
        }
    }
}