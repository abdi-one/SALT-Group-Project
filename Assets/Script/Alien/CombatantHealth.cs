using UnityEngine;

public class CombatantHealth : MonoBehaviour
{
  
  public Animator animator;

  public int maxHealth = 100;
  int currentHealth;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      currentHealth = maxHealth; 
    }       
    
    public void TakeDamage(int damage)
    {
      currentHealth -= damage;

      //Hurt anim
      animator.SetTrigger("Hurt");

      if(currentHealth <= 0)
      {
        Die();
      }

    }

    void Die()
    {
      Debug.Log("Combatant Down!");
      //Death anim 
      animator.SetBool("Dead", true);


      //Dead Combatant
      GetComponent<Collider2D>().enabled = false;
      this.enabled = false;
    }
}
