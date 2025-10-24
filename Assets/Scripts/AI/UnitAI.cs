using UnityEngine;
using System.Collections.Generic;

namespace TABSTIK.AI
{
    [RequireComponent(typeof(TABSTIK.Units.Unit))]
    public class UnitAI : MonoBehaviour
    {
        [Header("AI Settings")]
        public float detectionRange = 15f;
        public float updateInterval = 0.5f; // Update target every 0.5 seconds

        private TABSTIK.Units.Unit unit;
        private TABSTIK.Units.Unit currentTarget;
        private float lastUpdateTime;

        private void Awake()
        {
            unit = GetComponent<TABSTIK.Units.Unit>();
        }

        private void Update()
        {
            if (!unit.IsAlive) return;

            // Update target periodically
            if (Time.time >= lastUpdateTime + updateInterval)
            {
                FindNewTarget();
                lastUpdateTime = Time.time;
            }

            // Execute behavior based on current target
            if (currentTarget != null && currentTarget.IsAlive)
            {
                ExecuteCombatBehavior();
            }
            else
            {
                // No target, stop moving
                unit.StopMovement();
            }
        }

        private void FindNewTarget()
        {
            currentTarget = null;
            float closestDistance = detectionRange;

            // Find all units in scene
            TABSTIK.Units.Unit[] allUnits = FindObjectsOfType<TABSTIK.Units.Unit>();

            foreach (TABSTIK.Units.Unit potentialTarget in allUnits)
            {
                // Skip if same team, dead, or self
                if (potentialTarget.team == unit.team || !potentialTarget.IsAlive || potentialTarget == unit)
                    continue;

                float distance = Vector3.Distance(transform.position, potentialTarget.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    currentTarget = potentialTarget;
                }
            }
        }

        private void ExecuteCombatBehavior()
        {
            if (currentTarget == null) return;

            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);

            // Check if in attack range
            if (distanceToTarget <= unit.attackRange)
            {
                // Stop and attack
                unit.StopMovement();
                AttackTarget();
            }
            else
            {
                // Move towards target
                Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
                unit.MoveTo(direction);
            }
        }

        private void AttackTarget()
        {
            if (currentTarget == null || !currentTarget.IsAlive) return;

            // Check if this is an archer that should shoot
            TABSTIK.Units.ArcherUnit archer = unit as TABSTIK.Units.ArcherUnit;
            if (archer != null)
            {
                archer.ShootArrow(currentTarget);
            }
            else
            {
                unit.Attack(currentTarget);
            }
        }

        private void OnDrawGizmosSelected()
        {
            // Visualize detection range
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);

            // Visualize attack range
            if (unit != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, unit.attackRange);
            }

            // Draw line to current target
            if (currentTarget != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, currentTarget.transform.position);
            }
        }
    }
}
