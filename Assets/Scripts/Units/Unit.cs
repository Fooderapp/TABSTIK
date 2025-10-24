using UnityEngine;

namespace TABSTIK.Units
{
    public enum Team
    {
        Red,
        Blue
    }

    public enum UnitType
    {
        Soldier,
        Archer,
        Tank,
        Wacky
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Unit : MonoBehaviour
    {
        [Header("Unit Properties")]
        public UnitType unitType = UnitType.Soldier;
        public Team team = Team.Red;
        public float maxHealth = 100f;
        public float attackDamage = 10f;
        public float attackRange = 2f;
        public float attackCooldown = 1f;
        public float moveSpeed = 3f;

        [Header("Visual Settings")]
        public Material redTeamMaterial;
        public Material blueTeamMaterial;

        private float currentHealth;
        private float lastAttackTime;
        private Rigidbody rb;
        private Renderer unitRenderer;

        public bool IsAlive => currentHealth > 0;
        public float CurrentHealth => currentHealth;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            unitRenderer = GetComponentInChildren<Renderer>();
            currentHealth = maxHealth;
        }

        private void Start()
        {
            ApplyTeamColor();
        }

        private void ApplyTeamColor()
        {
            if (unitRenderer != null)
            {
                Material teamMaterial = team == Team.Red ? redTeamMaterial : blueTeamMaterial;
                if (teamMaterial != null)
                {
                    unitRenderer.material = teamMaterial;
                }
                else
                {
                    // Fallback to simple color
                    unitRenderer.material.color = team == Team.Red ? Color.red : Color.blue;
                }
            }
        }

        public void TakeDamage(float damage)
        {
            if (!IsAlive) return;

            currentHealth -= damage;
            currentHealth = Mathf.Max(0, currentHealth);

            if (!IsAlive)
            {
                Die();
            }
        }

        public bool CanAttack()
        {
            return Time.time >= lastAttackTime + attackCooldown;
        }

        public void Attack(Unit target)
        {
            if (!CanAttack() || target == null || !target.IsAlive) return;

            lastAttackTime = Time.time;
            target.TakeDamage(attackDamage);

            // Play attack animation trigger would go here
            PlayAttackAnimation();
        }

        private void PlayAttackAnimation()
        {
            // Placeholder for animation trigger
            // Animator would handle this: GetComponent<Animator>()?.SetTrigger("Attack");
        }

        private void Die()
        {
            // Notify managers that unit has died
            FindObjectOfType<TABSTIK.Managers.GameManager>()?.OnUnitDied(this);

            // Play death animation or effect
            PlayDeathEffect();

            // Destroy the unit
            Destroy(gameObject, 0.5f);
        }

        private void PlayDeathEffect()
        {
            // Placeholder for death effects
            // Could instantiate particle effect, play sound, etc.
        }

        public void MoveTo(Vector3 direction)
        {
            if (!IsAlive || rb == null) return;

            Vector3 movement = direction.normalized * moveSpeed;
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

            // Rotate to face movement direction
            if (direction.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        public void StopMovement()
        {
            if (rb != null)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
    }
}
