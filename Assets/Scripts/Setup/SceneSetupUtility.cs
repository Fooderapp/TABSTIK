using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TABSTIK.Setup
{
    /// <summary>
    /// Automated scene setup utility for TABSTIK
    /// Creates all necessary GameObjects, components, and prefabs
    /// </summary>
    public class SceneSetupUtility : MonoBehaviour
    {
        [Header("Setup Options")]
        [Tooltip("Number of spawn points per team")]
        public int spawnPointsPerTeam = 3;
        
        [Tooltip("Distance between spawn points")]
        public float spawnPointSpacing = 10f;
        
        [Tooltip("Create sample prefabs")]
        public bool createPrefabs = true;
        
        [Tooltip("Setup UI canvas and elements")]
        public bool setupUI = true;
        
        [Tooltip("Add test script for debugging")]
        public bool addTestScript = true;
        
        [Header("Status")]
        [SerializeField] private bool sceneSetupComplete = false;
        
        /// <summary>
        /// Performs the complete scene setup
        /// </summary>
        public void SetupScene()
        {
            Debug.Log("=== Starting TABSTIK Scene Setup ===");
            
            // Step 1: Create managers
            CreateManagers();
            
            // Step 2: Create spawn points
            CreateSpawnPoints();
            
            // Step 3: Create prefabs
            if (createPrefabs)
            {
                CreateUnitPrefabs();
            }
            
            // Step 4: Create environment
            CreateEnvironment();
            
            // Step 5: Setup camera
            SetupCamera();
            
            // Step 6: Setup UI
            if (setupUI)
            {
                CreateUICanvas();
            }
            
            // Step 7: Add test script
            if (addTestScript)
            {
                AddTestingComponents();
            }
            
            sceneSetupComplete = true;
            Debug.Log("=== Scene Setup Complete! ===");
            Debug.Log("Press Play to test the scene. Use keyboard keys to spawn units (see Console for controls).");
        }
        
        private void CreateManagers()
        {
            Debug.Log("Creating managers...");
            
            // Create GameManager
            GameObject gameManagerObj = GameObject.Find("GameManager");
            if (gameManagerObj == null)
            {
                gameManagerObj = new GameObject("GameManager");
                gameManagerObj.AddComponent<Managers.GameManager>();
                Debug.Log("✓ Created GameManager");
            }
            else
            {
                Debug.Log("✓ GameManager already exists");
            }
            
            // Create UnitSpawner
            GameObject unitSpawnerObj = GameObject.Find("UnitSpawner");
            if (unitSpawnerObj == null)
            {
                unitSpawnerObj = new GameObject("UnitSpawner");
                unitSpawnerObj.AddComponent<Managers.UnitSpawner>();
                Debug.Log("✓ Created UnitSpawner");
            }
            else
            {
                Debug.Log("✓ UnitSpawner already exists");
            }
            
            // Create TikTokEventListener
            GameObject tiktokListenerObj = GameObject.Find("TikTokEventListener");
            if (tiktokListenerObj == null)
            {
                tiktokListenerObj = new GameObject("TikTokEventListener");
                var listener = tiktokListenerObj.AddComponent<TikTokIntegration.TikTokEventListener>();
                // Configure for testing
                #if UNITY_EDITOR
                var so = new SerializedObject(listener);
                so.FindProperty("useMockAPI").boolValue = true;
                so.FindProperty("autoConnect").boolValue = true;
                so.FindProperty("alternateTeams").boolValue = true;
                so.ApplyModifiedProperties();
                #endif
                Debug.Log("✓ Created TikTokEventListener");
            }
            else
            {
                Debug.Log("✓ TikTokEventListener already exists");
            }
        }
        
        private void CreateSpawnPoints()
        {
            Debug.Log($"Creating {spawnPointsPerTeam} spawn points per team...");
            
            GameObject spawnPointsParent = GameObject.Find("SpawnPoints");
            if (spawnPointsParent == null)
            {
                spawnPointsParent = new GameObject("SpawnPoints");
            }
            
            // Create Red team spawn points
            GameObject redSpawnParent = new GameObject("RedTeamSpawnPoints");
            redSpawnParent.transform.SetParent(spawnPointsParent.transform);
            
            Transform[] redSpawnPoints = new Transform[spawnPointsPerTeam];
            for (int i = 0; i < spawnPointsPerTeam; i++)
            {
                GameObject spawnPoint = new GameObject($"RedSpawnPoint_{i + 1}");
                spawnPoint.transform.SetParent(redSpawnParent.transform);
                
                // Position spawn points in a line or arc
                float angle = (i - (spawnPointsPerTeam - 1) / 2f) * 15f; // Spread in arc
                float x = -25f; // Left side for red team
                float z = i * spawnPointSpacing - (spawnPointsPerTeam - 1) * spawnPointSpacing / 2f;
                
                spawnPoint.transform.position = new Vector3(x, 0f, z);
                redSpawnPoints[i] = spawnPoint.transform;
                
                // Add visual indicator (optional, can be disabled in play mode)
                CreateSpawnPointGizmo(spawnPoint, Color.red);
            }
            
            // Create Blue team spawn points
            GameObject blueSpawnParent = new GameObject("BlueTeamSpawnPoints");
            blueSpawnParent.transform.SetParent(spawnPointsParent.transform);
            
            Transform[] blueSpawnPoints = new Transform[spawnPointsPerTeam];
            for (int i = 0; i < spawnPointsPerTeam; i++)
            {
                GameObject spawnPoint = new GameObject($"BlueSpawnPoint_{i + 1}");
                spawnPoint.transform.SetParent(blueSpawnParent.transform);
                
                // Position spawn points in a line or arc
                float x = 25f; // Right side for blue team
                float z = i * spawnPointSpacing - (spawnPointsPerTeam - 1) * spawnPointSpacing / 2f;
                
                spawnPoint.transform.position = new Vector3(x, 0f, z);
                blueSpawnPoints[i] = spawnPoint.transform;
                
                // Add visual indicator
                CreateSpawnPointGizmo(spawnPoint, Color.blue);
            }
            
            // Assign spawn points to GameManager
            Managers.GameManager gameManager = FindObjectOfType<Managers.GameManager>();
            if (gameManager != null)
            {
                #if UNITY_EDITOR
                var so = new SerializedObject(gameManager);
                so.FindProperty("redSpawnPoints").arraySize = spawnPointsPerTeam;
                so.FindProperty("blueSpawnPoints").arraySize = spawnPointsPerTeam;
                
                for (int i = 0; i < spawnPointsPerTeam; i++)
                {
                    so.FindProperty("redSpawnPoints").GetArrayElementAtIndex(i).objectReferenceValue = redSpawnPoints[i];
                    so.FindProperty("blueSpawnPoints").GetArrayElementAtIndex(i).objectReferenceValue = blueSpawnPoints[i];
                }
                
                so.ApplyModifiedProperties();
                #endif
                Debug.Log($"✓ Created and assigned {spawnPointsPerTeam} spawn points per team");
            }
        }
        
        private void CreateSpawnPointGizmo(GameObject parent, Color color)
        {
            // Create a small visual indicator for spawn points
            GameObject gizmo = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            gizmo.name = "SpawnGizmo";
            gizmo.transform.SetParent(parent.transform);
            gizmo.transform.localPosition = Vector3.zero;
            gizmo.transform.localScale = new Vector3(2f, 0.1f, 2f);
            
            // Set color
            Renderer renderer = gizmo.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material mat = new Material(Shader.Find("Standard"));
                mat.color = new Color(color.r, color.g, color.b, 0.3f);
                renderer.material = mat;
            }
            
            // Remove collider so it doesn't interfere with gameplay
            Collider collider = gizmo.GetComponent<Collider>();
            if (collider != null)
            {
                DestroyImmediate(collider);
            }
        }
        
        private void CreateUnitPrefabs()
        {
            Debug.Log("Creating unit prefabs...");
            
            #if UNITY_EDITOR
            // Create Materials folder if it doesn't exist
            string materialsPath = "Assets/Materials";
            if (!AssetDatabase.IsValidFolder(materialsPath))
            {
                AssetDatabase.CreateFolder("Assets", "Materials");
            }
            
            // Create team materials
            Material redMaterial = CreateTeamMaterial("RedTeamMaterial", Color.red);
            Material blueMaterial = CreateTeamMaterial("BlueTeamMaterial", Color.blue);
            
            // Create Prefabs folder if it doesn't exist
            string prefabsPath = "Assets/Prefabs";
            if (!AssetDatabase.IsValidFolder(prefabsPath))
            {
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            }
            
            // Create unit prefabs
            GameObject soldierPrefab = PrefabFactory.CreateSoldierPrefab();
            GameObject archerPrefab = PrefabFactory.CreateArcherPrefab();
            GameObject tankPrefab = PrefabFactory.CreateTankPrefab();
            GameObject wackyPrefab = PrefabFactory.CreateWackyPrefab();
            GameObject projectilePrefab = PrefabFactory.CreateProjectilePrefab();
            
            // Save as prefabs
            PrefabUtility.SaveAsPrefabAsset(soldierPrefab, $"{prefabsPath}/Soldier.prefab");
            PrefabUtility.SaveAsPrefabAsset(archerPrefab, $"{prefabsPath}/Archer.prefab");
            PrefabUtility.SaveAsPrefabAsset(tankPrefab, $"{prefabsPath}/Tank.prefab");
            PrefabUtility.SaveAsPrefabAsset(wackyPrefab, $"{prefabsPath}/Wacky.prefab");
            PrefabUtility.SaveAsPrefabAsset(projectilePrefab, $"{prefabsPath}/Projectile.prefab");
            
            // Clean up temporary objects
            DestroyImmediate(soldierPrefab);
            DestroyImmediate(archerPrefab);
            DestroyImmediate(tankPrefab);
            DestroyImmediate(wackyPrefab);
            DestroyImmediate(projectilePrefab);
            
            // Assign prefabs to UnitSpawner
            Managers.UnitSpawner unitSpawner = FindObjectOfType<Managers.UnitSpawner>();
            if (unitSpawner != null)
            {
                var so = new SerializedObject(unitSpawner);
                so.FindProperty("soldierPrefab").objectReferenceValue = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabsPath}/Soldier.prefab");
                so.FindProperty("archerPrefab").objectReferenceValue = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabsPath}/Archer.prefab");
                so.FindProperty("tankPrefab").objectReferenceValue = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabsPath}/Tank.prefab");
                so.FindProperty("wackyPrefab").objectReferenceValue = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabsPath}/Wacky.prefab");
                so.FindProperty("redTeamMaterial").objectReferenceValue = redMaterial;
                so.FindProperty("blueTeamMaterial").objectReferenceValue = blueMaterial;
                so.ApplyModifiedProperties();
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            Debug.Log("✓ Created 4 unit prefabs and team materials");
            #else
            Debug.LogWarning("Prefab creation only available in Unity Editor");
            #endif
        }
        
        #if UNITY_EDITOR
        private Material CreateTeamMaterial(string name, Color color)
        {
            string path = $"Assets/Materials/{name}.mat";
            
            // Check if material already exists
            Material existingMat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (existingMat != null)
            {
                return existingMat;
            }
            
            // Create new material
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = color;
            mat.SetFloat("_Metallic", 0f);
            mat.SetFloat("_Glossiness", 0.5f);
            
            AssetDatabase.CreateAsset(mat, path);
            return mat;
        }
        #endif
        
        private void CreateEnvironment()
        {
            Debug.Log("Creating environment...");
            
            // Create ground plane
            GameObject ground = GameObject.Find("Ground");
            if (ground == null)
            {
                ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
                ground.name = "Ground";
                ground.transform.position = new Vector3(0f, 0f, 0f);
                ground.transform.localScale = new Vector3(10f, 1f, 10f);
                
                // Set ground material/color
                Renderer renderer = ground.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material groundMat = new Material(Shader.Find("Standard"));
                    groundMat.color = new Color(0.3f, 0.5f, 0.3f); // Greenish ground
                    renderer.material = groundMat;
                }
                
                Debug.Log("✓ Created ground plane");
            }
            
            // Create or setup directional light
            Light[] lights = FindObjectsOfType<Light>();
            bool hasDirectionalLight = false;
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    hasDirectionalLight = true;
                    break;
                }
            }
            
            if (!hasDirectionalLight)
            {
                GameObject lightObj = new GameObject("Directional Light");
                Light light = lightObj.AddComponent<Light>();
                light.type = LightType.Directional;
                light.intensity = 1f;
                lightObj.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
                Debug.Log("✓ Created directional light");
            }
        }
        
        private void SetupCamera()
        {
            Debug.Log("Setting up camera...");
            
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.transform.position = new Vector3(0f, 20f, -30f);
                mainCamera.transform.rotation = Quaternion.Euler(35f, 0f, 0f);
                mainCamera.fieldOfView = 60f;
                Debug.Log("✓ Configured main camera");
            }
            else
            {
                Debug.LogWarning("No main camera found in scene");
            }
        }
        
        private void CreateUICanvas()
        {
            Debug.Log("Creating UI canvas...");
            
            // Check if canvas already exists
            GameObject canvasObj = GameObject.Find("Canvas");
            if (canvasObj == null)
            {
                canvasObj = new GameObject("Canvas");
                Canvas canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                
                UnityEngine.UI.CanvasScaler scaler = canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920f, 1080f);
                
                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                
                Debug.Log("✓ Created UI Canvas");
            }
            
            // Create EventSystem if it doesn't exist
            if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
                Debug.Log("✓ Created EventSystem");
            }
            
            // Add UIManager component
            UI.UIManager uiManager = canvasObj.GetComponent<UI.UIManager>();
            if (uiManager == null)
            {
                canvasObj.AddComponent<UI.UIManager>();
                Debug.Log("✓ Added UIManager component");
            }
            
            Debug.Log("Note: UI elements can be created manually or via UIManager initialization");
        }
        
        private void AddTestingComponents()
        {
            Debug.Log("Adding testing components...");
            
            GameObject testObj = GameObject.Find("TestController");
            if (testObj == null)
            {
                testObj = new GameObject("TestController");
                testObj.AddComponent<Testing.SimpleTest>();
                Debug.Log("✓ Added SimpleTest controller");
            }
        }
    }
    
    #if UNITY_EDITOR
    /// <summary>
    /// Custom editor for SceneSetupUtility with a button to run setup
    /// </summary>
    [CustomEditor(typeof(SceneSetupUtility))]
    public class SceneSetupUtilityEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            SceneSetupUtility setupUtility = (SceneSetupUtility)target;
            
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(
                "Click 'Setup Scene' to automatically create all necessary GameObjects, " +
                "spawn points, prefabs, and UI elements for TABSTIK.", 
                MessageType.Info);
            
            if (GUILayout.Button("Setup Scene", GUILayout.Height(30)))
            {
                if (EditorUtility.DisplayDialog(
                    "Setup TABSTIK Scene",
                    "This will create managers, spawn points, prefabs, environment, and UI. Continue?",
                    "Yes", "Cancel"))
                {
                    setupUtility.SetupScene();
                    EditorUtility.DisplayDialog(
                        "Setup Complete",
                        "Scene setup is complete! Press Play to test the scene.",
                        "OK");
                }
            }
        }
    }
    #endif
}
