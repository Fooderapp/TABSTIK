using UnityEngine;

namespace TABSTIK.Setup
{
    /// <summary>
    /// Utility for creating sample unit prefabs with basic 3D models
    /// </summary>
    public static class PrefabFactory
    {
        /// <summary>
        /// Creates a soldier unit prefab with a cube body
        /// </summary>
        public static GameObject CreateSoldierPrefab()
        {
            GameObject soldier = new GameObject("Soldier_Prefab");
            
            // Create body (cube)
            GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cube);
            body.name = "Body";
            body.transform.SetParent(soldier.transform);
            body.transform.localPosition = Vector3.zero;
            body.transform.localScale = new Vector3(0.8f, 1.2f, 0.6f);
            
            // Create head (smaller cube)
            GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            head.name = "Head";
            head.transform.SetParent(soldier.transform);
            head.transform.localPosition = new Vector3(0f, 0.9f, 0f);
            head.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            
            // Add physics
            Rigidbody rb = soldier.AddComponent<Rigidbody>();
            rb.mass = 1f;
            rb.drag = 2f;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            
            // Add main collider
            BoxCollider collider = soldier.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.8f, 1.8f, 0.6f);
            collider.center = new Vector3(0f, 0.3f, 0f);
            
            // Remove individual colliders from primitives since we have a main collider
            Object.DestroyImmediate(body.GetComponent<Collider>());
            Object.DestroyImmediate(head.GetComponent<Collider>());
            
            // Add unit component
            Units.Unit unitComponent = soldier.AddComponent<Units.Unit>();
            unitComponent.unitType = Units.UnitType.Soldier;
            unitComponent.maxHealth = 100f;
            unitComponent.attackDamage = 10f;
            unitComponent.attackRange = 2f;
            unitComponent.attackCooldown = 1f;
            unitComponent.moveSpeed = 3f;
            
            // Add AI component
            soldier.AddComponent<AI.UnitAI>();
            
            return soldier;
        }
        
        /// <summary>
        /// Creates an archer unit prefab with a distinct shape
        /// </summary>
        public static GameObject CreateArcherPrefab()
        {
            GameObject archer = new GameObject("Archer_Prefab");
            
            // Create body (capsule for different look)
            GameObject body = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            body.name = "Body";
            body.transform.SetParent(archer.transform);
            body.transform.localPosition = Vector3.zero;
            body.transform.localScale = new Vector3(0.6f, 0.8f, 0.6f);
            
            // Create head
            GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            head.name = "Head";
            head.transform.SetParent(archer.transform);
            head.transform.localPosition = new Vector3(0f, 1.0f, 0f);
            head.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            
            // Create bow (simple representation)
            GameObject bow = GameObject.CreatePrimitive(PrimitiveType.Cube);
            bow.name = "Bow";
            bow.transform.SetParent(archer.transform);
            bow.transform.localPosition = new Vector3(0.5f, 0.5f, 0f);
            bow.transform.localScale = new Vector3(0.1f, 0.6f, 0.1f);
            bow.transform.localRotation = Quaternion.Euler(0f, 0f, 45f);
            
            // Add physics
            Rigidbody rb = archer.AddComponent<Rigidbody>();
            rb.mass = 0.8f;
            rb.drag = 2f;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            
            // Add main collider
            CapsuleCollider collider = archer.AddComponent<CapsuleCollider>();
            collider.height = 2f;
            collider.radius = 0.4f;
            collider.center = new Vector3(0f, 0.4f, 0f);
            
            // Remove individual colliders
            Object.DestroyImmediate(body.GetComponent<Collider>());
            Object.DestroyImmediate(head.GetComponent<Collider>());
            Object.DestroyImmediate(bow.GetComponent<Collider>());
            
            // Add unit component
            Units.ArcherUnit unitComponent = archer.AddComponent<Units.ArcherUnit>();
            unitComponent.unitType = Units.UnitType.Archer;
            unitComponent.maxHealth = 80f;
            unitComponent.attackDamage = 8f;
            unitComponent.attackRange = 10f;
            unitComponent.attackCooldown = 1.5f;
            unitComponent.moveSpeed = 3.5f;
            
            // Add AI component
            archer.AddComponent<AI.UnitAI>();
            
            return archer;
        }
        
        /// <summary>
        /// Creates a tank unit prefab (larger and bulkier)
        /// </summary>
        public static GameObject CreateTankPrefab()
        {
            GameObject tank = new GameObject("Tank_Prefab");
            
            // Create body (larger cube)
            GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cube);
            body.name = "Body";
            body.transform.SetParent(tank.transform);
            body.transform.localPosition = Vector3.zero;
            body.transform.localScale = new Vector3(1.5f, 1.5f, 1.2f);
            
            // Create head (larger)
            GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            head.name = "Head";
            head.transform.SetParent(tank.transform);
            head.transform.localPosition = new Vector3(0f, 1.2f, 0f);
            head.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            
            // Create armor plates (decorative cubes)
            GameObject armor1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            armor1.name = "Armor_Shoulder";
            armor1.transform.SetParent(tank.transform);
            armor1.transform.localPosition = new Vector3(0.9f, 0.8f, 0f);
            armor1.transform.localScale = new Vector3(0.4f, 0.6f, 0.8f);
            
            // Add physics
            Rigidbody rb = tank.AddComponent<Rigidbody>();
            rb.mass = 2f;
            rb.drag = 3f;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            
            // Add main collider
            BoxCollider collider = tank.AddComponent<BoxCollider>();
            collider.size = new Vector3(1.8f, 2.5f, 1.2f);
            collider.center = new Vector3(0f, 0.5f, 0f);
            
            // Remove individual colliders
            Object.DestroyImmediate(body.GetComponent<Collider>());
            Object.DestroyImmediate(head.GetComponent<Collider>());
            Object.DestroyImmediate(armor1.GetComponent<Collider>());
            
            // Add unit component
            Units.TankUnit unitComponent = tank.AddComponent<Units.TankUnit>();
            unitComponent.unitType = Units.UnitType.Tank;
            unitComponent.maxHealth = 300f;
            unitComponent.attackDamage = 25f;
            unitComponent.attackRange = 2.5f;
            unitComponent.attackCooldown = 2f;
            unitComponent.moveSpeed = 1.5f;
            
            // Add AI component
            tank.AddComponent<AI.UnitAI>();
            
            return tank;
        }
        
        /// <summary>
        /// Creates a wacky unit prefab (unusual proportions)
        /// </summary>
        public static GameObject CreateWackyPrefab()
        {
            GameObject wacky = new GameObject("Wacky_Prefab");
            
            // Create body (cylinder for different look)
            GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            body.name = "Body";
            body.transform.SetParent(wacky.transform);
            body.transform.localPosition = Vector3.zero;
            body.transform.localScale = new Vector3(0.7f, 1f, 0.7f);
            
            // Create oversized head
            GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            head.name = "Head";
            head.transform.SetParent(wacky.transform);
            head.transform.localPosition = new Vector3(0f, 1.5f, 0f);
            head.transform.localScale = new Vector3(1f, 1f, 1f);
            
            // Create funny appendages
            GameObject arm1 = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            arm1.name = "Arm";
            arm1.transform.SetParent(wacky.transform);
            arm1.transform.localPosition = new Vector3(0.8f, 0.5f, 0f);
            arm1.transform.localScale = new Vector3(0.2f, 0.8f, 0.2f);
            arm1.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            
            // Add physics
            Rigidbody rb = wacky.AddComponent<Rigidbody>();
            rb.mass = 0.7f;
            rb.drag = 1.5f;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            
            // Add main collider
            CapsuleCollider collider = wacky.AddComponent<CapsuleCollider>();
            collider.height = 2.5f;
            collider.radius = 0.6f;
            collider.center = new Vector3(0f, 0.5f, 0f);
            
            // Remove individual colliders
            Object.DestroyImmediate(body.GetComponent<Collider>());
            Object.DestroyImmediate(head.GetComponent<Collider>());
            Object.DestroyImmediate(arm1.GetComponent<Collider>());
            
            // Add unit component
            Units.WackyUnit unitComponent = wacky.AddComponent<Units.WackyUnit>();
            unitComponent.unitType = Units.UnitType.Wacky;
            unitComponent.maxHealth = 80f;
            unitComponent.attackDamage = 15f;
            unitComponent.attackRange = 3f;
            unitComponent.attackCooldown = 0.8f;
            unitComponent.moveSpeed = 4f;
            
            // Add AI component
            wacky.AddComponent<AI.UnitAI>();
            
            return wacky;
        }
        
        /// <summary>
        /// Creates a simple projectile prefab for archers
        /// </summary>
        public static GameObject CreateProjectilePrefab()
        {
            GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            projectile.name = "Projectile_Prefab";
            projectile.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            
            // Add rigidbody
            Rigidbody rb = projectile.AddComponent<Rigidbody>();
            rb.mass = 0.1f;
            rb.useGravity = true;
            
            // Collider already added by CreatePrimitive
            SphereCollider collider = projectile.GetComponent<SphereCollider>();
            collider.isTrigger = true;
            
            return projectile;
        }
    }
}
