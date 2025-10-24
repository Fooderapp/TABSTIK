using UnityEngine;

namespace TABSTIK.Units
{
    [CreateAssetMenu(fileName = "UnitStats", menuName = "TABSTIK/Unit Stats", order = 1)]
    public class UnitStats : ScriptableObject
    {
        [Header("Basic Stats")]
        public UnitType unitType;
        public string unitName;
        public float maxHealth = 100f;
        public float attackDamage = 10f;
        public float attackRange = 2f;
        public float attackCooldown = 1f;
        public float moveSpeed = 3f;

        [Header("Special Abilities")]
        public bool hasRangedAttack = false;
        public float projectileSpeed = 10f;
        public GameObject projectilePrefab;

        [Header("Cost")]
        public int spawnCost = 1; // Used for gift-based spawning

        [Header("Visuals")]
        public GameObject prefab;
        public RuntimeAnimatorController animatorController;
    }
}
