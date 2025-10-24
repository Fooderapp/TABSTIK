using UnityEngine;

namespace TABSTIK.Units
{
    public class TankUnit : Unit
    {
        [Header("Tank Specific")]
        public float armorMultiplier = 0.5f; // Takes 50% damage

        private void Start()
        {
            unitType = UnitType.Tank;
            maxHealth = 300f; // Tanks have more health
            moveSpeed = 1.5f; // But move slower
        }

        public new void TakeDamage(float damage)
        {
            base.TakeDamage(damage * armorMultiplier);
        }
    }

    public class WackyUnit : Unit
    {
        [Header("Wacky Specific")]
        public float randomBehaviorChance = 0.3f;
        public float spinSpeed = 360f;

        private void Start()
        {
            unitType = UnitType.Wacky;
        }

        private void Update()
        {
            // Random wacky behavior
            if (Random.value < randomBehaviorChance * Time.deltaTime)
            {
                PerformWackyAction();
            }
        }

        private void PerformWackyAction()
        {
            // Random spin
            if (Random.value > 0.5f)
            {
                transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            }
            // Random jump
            else
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
                }
            }
        }
    }
}
