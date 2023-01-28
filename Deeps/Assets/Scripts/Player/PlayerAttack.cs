using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 4;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent(typeof(Animator)) as Animator;
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }
}