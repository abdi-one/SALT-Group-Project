using UnityEngine;

public class CombatLogic : MonoBehaviour
{
    public Animator animator;

    public Transform combatPoint;
    public float combatRange = 0.5f;
    public LayerMask combatantLayers;
    public int attackDamage = 40;

    //limit attack spam
    public float attackRate = 2f;
    float nextAttackTime = 0f; 


  // Update is called once per frame
    void Update()
    {
      if(Time.time >= nextAttackTime)
      {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
          Attack();
          nextAttackTime = Time.time + 1f / attackRate;
        } 
      }
    }

    void Attack()
    {
      // Combat Trigger 
      animator.SetTrigger("Attack");

      // Enemy Detection
      Collider2D[] hitCombatants =
        Physics2D.OverlapCircleAll(combatPoint.position, combatRange, combatantLayers);

      // Enemy Damage
      foreach(Collider2D combatant in hitCombatants) 
      {
        combatant.GetComponent<CombatantHealth>().TakeDamage(attackDamage);
      }
    }

    void onDrawGizmosSelected()
    {
      if(combatPoint == null)
        return;

      Gizmos.DrawWireSphere(combatPoint.position, combatRange);
    }
}
