using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   public int maxHealth = 20;
   private int currentHealth;

   public HealthBar healthBar;

   public PlayerSpawn playerSpawn;

   void Start()
   {
      currentHealth = maxHealth;
      healthBar.SetMaxHealth(maxHealth);
   }
   void Update()
   {
      if (Input.GetKeyDown(KeyCode.F))
      {
         TakeDamage(2);
      }

      if (currentHealth <= 0)
      {
         playerSpawn.Revive();
         currentHealth = maxHealth;
         healthBar.SetHealth(currentHealth);
      }
   }

   void TakeDamage(int damage)
   {
      currentHealth -= damage;
      healthBar.SetHealth(currentHealth);
   }
}
