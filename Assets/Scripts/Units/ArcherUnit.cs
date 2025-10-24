using UnityEngine;

namespace TABSTIK.Units
{
    public class ArcherUnit : Unit
    {
        [Header("Archer Specific")]
        public GameObject arrowPrefab;
        public Transform arrowSpawnPoint;
        public float arrowSpeed = 15f;

        private void Start()
        {
            unitType = UnitType.Archer;
            attackRange = 10f; // Archers have longer range
        }

        public void ShootArrow(Unit target)
        {
            if (!CanAttack() || target == null || !target.IsAlive) return;
            if (arrowPrefab == null || arrowSpawnPoint == null) return;

            // Spawn arrow
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
            Vector3 direction = (target.transform.position - arrowSpawnPoint.position).normalized;
            
            Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
            if (arrowRb != null)
            {
                arrowRb.velocity = direction * arrowSpeed;
            }

            // Setup arrow damage
            Projectile projectile = arrow.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.damage = attackDamage;
                projectile.ownerTeam = team;
            }

            // Mark attack as performed
            Attack(null); // Uses base cooldown without dealing direct damage
        }
    }

    public class Projectile : MonoBehaviour
    {
        public float damage = 10f;
        public Team ownerTeam;
        public float lifeTime = 5f;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Unit unit = other.GetComponent<Unit>();
            if (unit != null && unit.team != ownerTeam && unit.IsAlive)
            {
                unit.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
