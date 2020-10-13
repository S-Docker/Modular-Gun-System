using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class GunCreationTool : EditorWindow
{
    Vector2 scrollPosition = Vector2.zero;

    [Header("Gun Settings")] 
    GameObject createdGun;
    
    readonly string gunPrefabPath = "Assets/Prefabs/Guns/";
    string gunName;
    GameObject selectedGunModel;
    Material selectedGunMaterial;
    GunData selectedGunData;
    bool useExistingGunData, hasAbility, isHitscan;
    GunAbility selectedAbility;
    bool GunExists => createdGun != null;

    [Header("Gun Data Settings")] 
    FireMode fireMode;
    AmmoCategory ammoCategory;
    int damage;
    int magazineSize;
    int roundsPerMinute;
    float baseSpreadRadius;
    int baseCritChance;
    float baseCritMultiplier;
    int baseStunChance;

    [Header("Projectile Settings")] 
    GameObject createdProjectile;
    
    readonly string projectilePrefabPath = "Assets/Prefabs/Projectiles/";
    string projectileName;
    GameObject selectedExistingProjectile;
    GameObject selectedProjectileModel;
    Material selectedProjectileMaterial;
    float projectileSpeed, maxProjectileTravel;
    bool useExistingProjectile, isSticky, isBouncy;
    bool CanUseExistingProjectile => useExistingProjectile && selectedExistingProjectile != null;
    bool CanCreateNewProjectile => !String.IsNullOrEmpty(projectileName) && selectedProjectileModel != null;
    bool ProjectileExists => createdProjectile != null || (useExistingProjectile && selectedExistingProjectile != null);

    [Header("Gun and Projectile Previews")]
    Editor gunPreviewEditor;

    Editor projectilePreviewEditor;

    [MenuItem("Tools/Gun Creation Tool")]
    public static void OpenWindow() => GetWindow<GunCreationTool>("Gun Creator");

    void OnGUI(){
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        GUILayout.BeginHorizontal();
        SetupArea();
        PreviewArea();
        GUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();

    }

    void OnDisable(){
        if (GunExists){
            DestroyImmediate(createdGun);
        }
        
        if (ProjectileExists){
            DestroyImmediate(createdProjectile);
        }
    }

    void SetupArea(){
        GUILayout.BeginVertical();

        EditorGUILayout.LabelField("Gun Setup", EditorStyles.boldLabel);
        GunAppearanceSettings();
        
        if (GunExists){
            GunBehaviourSettings();
            GunDataSettings();
        }

        if (!useExistingProjectile && selectedProjectileModel != null){
            NewProjectileBehaviourSettings();
            NewProjectileDataSettings();
        }

        BuildGunAndProjectile();

        GUILayout.EndVertical();
    }

    #region Model preview area methods

    void PreviewArea(){
        GUILayout.BeginVertical();

        if (GunExists){
            GunPreview();
        }

        if (ProjectileExists){
            ProjectilePreview();
        }

        GUILayout.EndVertical();
    }

    void GunPreview(){
        if (gunPreviewEditor == null){
            gunPreviewEditor = Editor.CreateEditor(createdGun);
        }

        gunPreviewEditor.OnPreviewGUI(GUILayoutUtility.GetRect(Screen.width / 2, Screen.height),
        EditorStyles.whiteLabel);
    }

    void ProjectilePreview(){
        if (projectilePreviewEditor == null){
            projectilePreviewEditor = Editor.CreateEditor(useExistingProjectile ? selectedExistingProjectile : createdProjectile);
        } else if (useExistingProjectile && projectilePreviewEditor.target != selectedExistingProjectile){
            projectilePreviewEditor = Editor.CreateEditor(selectedExistingProjectile);
        } else if (!useExistingProjectile && projectilePreviewEditor.target != selectedProjectileModel){
            projectilePreviewEditor = Editor.CreateEditor(createdProjectile);
        }
        
        projectilePreviewEditor.OnPreviewGUI(GUILayoutUtility.GetRect(Screen.width / 2, Screen.height),
            EditorStyles.whiteLabel);
    }

    #endregion

    #region Gun setup methods

    void GunAppearanceSettings(){
        EditorGUILayout.LabelField("Unique Gun Name:");
        EditorGUI.BeginChangeCheck();
        gunName = EditorGUILayout.TextField(gunName);
        if (EditorGUI.EndChangeCheck()){
            if (GunExists){
                createdGun.name = gunName;
            }
        }

        EditorGUILayout.LabelField("Gun Model:");
        EditorGUI.BeginChangeCheck();
        using (new EditorGUI.DisabledScope(String.IsNullOrEmpty(gunName))){
            selectedGunModel = (GameObject) EditorGUILayout.ObjectField(selectedGunModel, typeof(GameObject), false);
        }
        if (EditorGUI.EndChangeCheck()){
            if (GunExists){
                DestroyImmediate(createdGun);
            }

            if (selectedGunModel != null){
                createdGun = Instantiate(selectedGunModel, Vector3.zero, Quaternion.identity);
                createdGun.name = gunName;
            }
            else{
                selectedGunMaterial = null;
            }
        }
        
        EditorGUILayout.LabelField("Gun Material:");
        EditorGUI.BeginChangeCheck();
        using (new EditorGUI.DisabledScope(selectedGunModel == null)){
            selectedGunMaterial = (Material) EditorGUILayout.ObjectField(selectedGunMaterial, typeof(Material), false);
        }
        if (EditorGUI.EndChangeCheck()){
            if (selectedGunMaterial != null){
                ApplyMaterial(createdGun, selectedGunMaterial);
            }
        }
    }
    
    void GunBehaviourSettings(){
        isHitscan = EditorGUILayout.Toggle("Hitscan?", isHitscan);

        if (hasAbility = EditorGUILayout.Toggle("Has Gun Ability?", hasAbility)){
            EditorGUILayout.LabelField("Ability:");
            selectedAbility = EditorGUILayout.ObjectField(selectedAbility, typeof(GunAbility), false) as GunAbility;
        }
        else{
            selectedAbility = null;
        }
    }

    void GunDataSettings(){
        if (useExistingGunData = EditorGUILayout.Toggle("Use Existing Gun Data?", useExistingGunData)){
            SelectExistingGunData();
        }
        else{
            SetNewGunData();
            
            EditorGUILayout.LabelField("Projectile Setup", EditorStyles.boldLabel);
            if (useExistingProjectile = EditorGUILayout.Toggle("Use Existing Projectile?", useExistingProjectile)){
                if (ProjectileExists){
                    DestroyImmediate(createdProjectile);
                    selectedProjectileModel = null;
                }
                SelectExistingProjectile();
            }
            else{
                NewProjectileAppearanceSettings();
            }
        }
    }

    void SelectExistingGunData(){
        EditorGUILayout.LabelField("Gun Data:");
        selectedGunData = EditorGUILayout.ObjectField(selectedGunData, typeof(GunData), false) as GunData;
    }

    void SetNewGunData(){
        fireMode = (FireMode)EditorGUILayout.EnumPopup("Fire Mode: ", fireMode);
        ammoCategory = (AmmoCategory)EditorGUILayout.EnumPopup("Ammo Type: ", ammoCategory);;
        damage = EditorGUILayout.IntSlider("Damage: ", damage, 1, 1000);
        magazineSize = EditorGUILayout.IntSlider("Magazine Size: ", magazineSize, 1, 100);
        roundsPerMinute = EditorGUILayout.IntSlider("Rounds Per Minute: ", roundsPerMinute, 1, 500);
        baseSpreadRadius = EditorGUILayout.Slider("Spread Radius: ", baseSpreadRadius, 0, 10);
        baseSpreadRadius = (float) System.Math.Round(baseSpreadRadius, 2);
        baseCritChance = EditorGUILayout.IntSlider("Base Crit Chance: ", baseCritChance, 0, 100);
        baseCritMultiplier = EditorGUILayout.Slider("Base Crit Multiplier: ", baseCritMultiplier, 0, 3);
        baseCritMultiplier = (float) System.Math.Round(baseCritMultiplier, 2);
        baseStunChance = EditorGUILayout.IntSlider("Base Stun Chance: ", baseStunChance, 0, 100);
    }

    void InitializeGun(GameObject gun){
        var gunScript = gun.AddComponent<Gun>();
        gun.AddComponent<GunFireComponent>();
        gun.AddComponent<GunReloadComponent>();
        
        gunScript.InitializeGun(selectedGunData, selectedAbility);
    }

    #endregion

    #region Projectile methods

    void SelectExistingProjectile(){
        EditorGUILayout.LabelField("Projectile:");
        
        EditorGUI.BeginChangeCheck();
        selectedExistingProjectile = EditorGUILayout.ObjectField(selectedExistingProjectile, typeof(GameObject), false) as GameObject;
        if (EditorGUI.EndChangeCheck()){
            if (ProjectileExists){
                DestroyImmediate(createdProjectile);
            }
        }
    }

    void NewProjectileAppearanceSettings(){
        EditorGUILayout.LabelField("Unique Projectile Name:");
        EditorGUI.BeginChangeCheck();
        projectileName = EditorGUILayout.TextField(projectileName);
        if (EditorGUI.EndChangeCheck()){
            if (ProjectileExists){
                createdProjectile.name = projectileName;
            }
        }
        
        EditorGUILayout.LabelField("Projectile Model:");
        EditorGUI.BeginChangeCheck();
        using (new EditorGUI.DisabledScope(String.IsNullOrEmpty(projectileName))){
            selectedProjectileModel =
                EditorGUILayout.ObjectField(selectedProjectileModel, typeof(GameObject), false) as GameObject;
        }
        if (EditorGUI.EndChangeCheck()){
            if (ProjectileExists){
                DestroyImmediate(createdProjectile);
            }

            if (selectedProjectileModel != null){
                createdProjectile = Instantiate(selectedProjectileModel, Vector3.zero, Quaternion.identity);
                createdProjectile.name = projectileName;
            }
            else{
                selectedGunMaterial = null;
            }
        }
        EditorGUILayout.LabelField("Projectile Material:");
        selectedProjectileMaterial =
            (Material) EditorGUILayout.ObjectField(selectedProjectileMaterial, typeof(Material), false);
    }

    void NewProjectileBehaviourSettings(){
        EditorGUILayout.LabelField("New Projectile Settings", EditorStyles.boldLabel);

        if (isSticky = EditorGUILayout.Toggle("Can Stick?", isSticky)){
            isBouncy = false;
        }

        if (isBouncy = EditorGUILayout.Toggle("Can Bounce?", isBouncy)){
            isSticky = false;
        }
    }

    void NewProjectileDataSettings(){
        maxProjectileTravel = EditorGUILayout.Slider("Max Projectile Distance: ", maxProjectileTravel, 1, 100);
        maxProjectileTravel = (float) System.Math.Round(maxProjectileTravel, 2);
        
        if (isHitscan) return;
        
        projectileSpeed = EditorGUILayout.Slider("Projectile Speed: ", projectileSpeed, 1, 100);
        projectileSpeed = (float) System.Math.Round(projectileSpeed, 2);
    }

    void InitializeProjectile(GameObject projectile){
        if (isHitscan){
            var hitscanComponent = projectile.AddComponent<ProjectileHitscanComponent>();
            hitscanComponent.SetUpProjectileHitscanComponent(maxProjectileTravel);
        }
        else{
            var moveComponent = projectile.AddComponent<ProjectileMoveComponent>();
            moveComponent.SetUpProjectileMoveComponent(projectileSpeed, maxProjectileTravel);
        }
        if (isSticky){
            projectile.AddComponent<ProjectileStickyCollisionComponent>();
        }
        else{
            projectile.AddComponent<ProjectileStandardCollisionComponent>();
        }

        if (isBouncy){
            projectile.AddComponent<ProjectileBounceComponent>();
        }
    }

    #endregion

    void BuildGunAndProjectile(){
        using (new EditorGUI.DisabledScope((!CanUseExistingProjectile && !CanCreateNewProjectile) ||
                                           !GunExists)){
            if (GUILayout.Button("Build Gun")){
                if (useExistingGunData){
                    InitializeGun(createdGun);
                }
                else{
                    if (!useExistingProjectile){
                        if (selectedProjectileMaterial != null){
                            createdProjectile.GetComponent<MeshRenderer>().material = selectedProjectileMaterial;
                        }

                        InitializeProjectile(createdProjectile);
                        GameObject projectilePrefab = PrefabUtility.SaveAsPrefabAsset(createdProjectile, projectilePrefabPath + projectileName + ".prefab");

                        CreateNewGunData(projectilePrefab);
                        InitializeGun(createdGun);
                        
                        DestroyImmediate(createdProjectile);
                    }
                    else{
                        CreateNewGunData(selectedExistingProjectile);
                        InitializeGun(createdGun);
                    }
                    
                    PrefabUtility.SaveAsPrefabAsset(createdGun, gunPrefabPath + gunName + ".prefab");
                    DestroyImmediate(createdGun);
                }
            }
        }
    }

    void CreateNewGunData(GameObject projectilePrefab){
        GunData newGunData = CreateInstance(typeof(GunData)) as GunData;
        
        newGunData.InitialiseGunData(fireMode, ammoCategory, projectilePrefab, damage, magazineSize, roundsPerMinute, baseSpreadRadius, baseCritChance, baseCritMultiplier, baseStunChance);
        
        AssetDatabase.CreateAsset(newGunData, gunPrefabPath + gunName + "Data.asset");
        AssetDatabase.SaveAssets();

        selectedGunData = newGunData;
    }

    void ApplyMaterial(GameObject obj, Material mat){
        MeshRenderer rend = obj.GetComponent<MeshRenderer>();
        
        if (rend == null){
            MeshRenderer[] childMeshes = createdGun.GetComponentsInChildren<MeshRenderer>();
            foreach (var child in childMeshes){
                child.material = mat;
            }
        }
        else{
            rend.material = mat;
        }
    }

    void RemoveMaterial(GameObject obj){
        
    }
}
