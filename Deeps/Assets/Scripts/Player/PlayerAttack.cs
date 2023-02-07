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
    private Animator _animator;
    public static IWeapons _actualweapon =  null;

    private int _attackDelay = 0;

    private void Start()
    {
        _animator = GetComponent(typeof(Animator)) as Animator;
    }

    public void Attack()
    {
        if (_attackDelay==0 && _actualweapon!=null)
        {
            _animator.SetTrigger("Attack");

            _attackDelay = 120 / _actualweapon.GetAttackSpeed();
        }
    }

    private void Update()
    {
        if (_attackDelay!=0)
        {
            _attackDelay -= 1;
        }
    }
}