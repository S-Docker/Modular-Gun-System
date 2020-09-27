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
    readonly string gunPrefabPath = "Assets/Prefabs/Guns/";
    string gunName;
    GameObject selectedGunModel;
    Material selectedGunMaterial;
    GunData selectedGunData;
    bool useExistingGunData, hasAbility, isHitscan;
    GunAbility selectedAbility;
    bool CanCreateNewGun => !String.IsNullOrEmpty(gunName) && selectedGunModel != null;

    [Header("Gun Data Settings")] 
    FireMode fireMode;
    AmmoCategory ammoCategory;
    int damage;
    int magazineSize;
    int roundsPerMinute;
    int baseCritChance;
    float baseCritMultiplier;
    int baseStunChance;

    GameObject createdGun;

    [Header("Projectile Settings")] 
    readonly string projectilePrefabPath = "Assets/Prefabs/Projectiles/";
    string projectileName;
    GameObject selectedExistingProjectile;
    GameObject selectedProjectileModel;
    Material selectedProjectileMaterial;
    float projectileSpeed, maxProjectileTravel;
    bool useExistingProjectile, isSticky, isBouncy;
    bool CanUseExistingProjectile => useExistingProjectile && selectedExistingProjectile != null;
    bool CanCreateNewProjectile => !String.IsNullOrEmpty(projectileName) && selectedProjectileModel != null;

    GameObject createdProjectile;

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

    void SetupArea(){
        GUILayout.BeginVertical();

        EditorGUILayout.LabelField("Gun Setup", EditorStyles.boldLabel);
        GunSetup();
        BuildGunAndProjectile();

        GUILayout.EndVertical();
    }

    #region Model preview area methods

    void PreviewArea(){
        GUILayout.BeginVertical();

        GunPreview();
        ProjectilePreview();

        GUILayout.EndVertical();
    }

    void GunPreview(){
        if (gunPreviewEditor == null){
            gunPreviewEditor = Editor.CreateEditor(selectedGunModel);
        }

        if (selectedGunModel == null) return;

        if (gunPreviewEditor.target != selectedGunModel){
            gunPreviewEditor = Editor.CreateEditor(selectedGunModel);
        }

        gunPreviewEditor.OnPreviewGUI(GUILayoutUtility.GetRect(Screen.width / 2, Screen.height),
            EditorStyles.whiteLabel);
    }

    void ProjectilePreview(){
        if (projectilePreviewEditor == null){
            projectilePreviewEditor = Editor.CreateEditor(selectedProjectileModel);
        }

        if (selectedProjectileModel == null) return;

        if (projectilePreviewEditor.target != selectedProjectileModel){
            projectilePreviewEditor = Editor.CreateEditor(selectedProjectileModel);
        }

        projectilePreviewEditor.OnPreviewGUI(GUILayoutUtility.GetRect(Screen.width / 2, Screen.height),
            EditorStyles.whiteLabel);
    }

    #endregion

    #region Gun setup methods

    void GunSetup(){
        EditorGUILayout.LabelField("Unique Gun Name:");
        gunName = EditorGUILayout.TextField(gunName);

        EditorGUILayout.LabelField("Gun Model:");
        selectedGunModel = (GameObject) EditorGUILayout.ObjectField(selectedGunModel, typeof(GameObject), false);

        EditorGUILayout.LabelField("Gun Material:");
        selectedGunMaterial = (Material) EditorGUILayout.ObjectField(selectedGunMaterial, typeof(Material), false);

        isHitscan = EditorGUILayout.Toggle("Hitscan?", isHitscan);

        if (hasAbility = EditorGUILayout.Toggle("Has Gun Ability?", hasAbility)){
            EditorGUILayout.LabelField("Ability:");
            selectedAbility = EditorGUILayout.ObjectField(selectedAbility, typeof(GunAbility), false) as GunAbility;
        }
        else{
            selectedAbility = null;
        }

        if (useExistingGunData = EditorGUILayout.Toggle("Use Existing Gun Data?", useExistingGunData)){
            SelectExistingGunData();
        }
        else{
            SetNewGunData();
            
            if (useExistingProjectile = EditorGUILayout.Toggle("Use Existing Projectile?", useExistingProjectile)){
                SelectExistingProjectile();
            }
            else{
                CreateNewProjectile();
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
        baseCritChance = EditorGUILayout.IntSlider("Base Crit Chance: ", baseCritChance, 0, 100);
        baseCritMultiplier = EditorGUILayout.Slider("Base Crit Multiplier: ", baseCritMultiplier, 0, 3);
        baseCritMultiplier = (float) System.Math.Round(baseCritMultiplier, 2);
        baseStunChance = EditorGUILayout.IntSlider("Base Stun Chance: ", baseStunChance, 0, 100);
    }

    void InitializeGun(GameObject gun){
        var gunScript = gun.AddComponent<Gun>();
        
        gunScript.InitializeGun(selectedGunData, isHitscan, selectedAbility);
    }

    #endregion

    #region Projectile methods

    void SelectExistingProjectile(){
        EditorGUILayout.LabelField("Projectile:");
        selectedExistingProjectile = EditorGUILayout.ObjectField(selectedExistingProjectile, typeof(GameObject), false) as GameObject;
    }

    void CreateNewProjectile(){
        EditorGUILayout.LabelField("New Projectile Settings", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Unique Projectile Name:");
        projectileName = EditorGUILayout.TextField(projectileName);

        EditorGUILayout.LabelField("Projectile Model:");
        selectedProjectileModel =
            EditorGUILayout.ObjectField(selectedProjectileModel, typeof(GameObject), false) as GameObject;
        
        EditorGUILayout.LabelField("Projectile Material:");
        selectedProjectileMaterial = (Material) EditorGUILayout.ObjectField(selectedProjectileMaterial, typeof(Material), false);

        if (!isHitscan){
            projectileSpeed = EditorGUILayout.Slider("Projectile Speed: ", projectileSpeed, 1, 25);
            projectileSpeed = (float) System.Math.Round(projectileSpeed, 2);
            
            maxProjectileTravel = EditorGUILayout.Slider("Max Projectile Distance: ", maxProjectileTravel, 1, 100);
            maxProjectileTravel = (float) System.Math.Round(maxProjectileTravel, 2);
        }
        
        if (isSticky = EditorGUILayout.Toggle("Can Stick?", isSticky)){
            isBouncy = false;
        }

        if (isBouncy = EditorGUILayout.Toggle("Can Bounce?", isBouncy)){
            isSticky = false;
        }
    }

    void InitializeProjectile(GameObject projectile){
        if (isHitscan){
            projectile.AddComponent<ProjectileHitscanComponent>();
        }
        else{
            var moveComponent = projectile.AddComponent<ProjectileMoveComponent>();
            moveComponent.InitialiseProjectileMove(projectileSpeed, maxProjectileTravel);
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
                                           !CanCreateNewGun)){
            if (GUILayout.Button("Build Gun")){
                // Create gun
                if (createdGun != null) DestroyImmediate(createdGun);

                createdGun = Instantiate(selectedGunModel, Vector3.zero, Quaternion.identity);
                createdGun.name = gunName;

                if (selectedGunMaterial != null){
                    MeshRenderer[] childMeshes = createdGun.GetComponentsInChildren<MeshRenderer>();
                    foreach (var child in childMeshes){
                        child.material = selectedGunMaterial;
                    }
                }

                if (useExistingGunData){
                    InitializeGun(createdGun);
                }
                else{
                    if (!useExistingProjectile){
                        // Create projectile
                        if (createdProjectile != null) DestroyImmediate(createdProjectile);

                        createdProjectile = Instantiate(selectedProjectileModel, Vector3.zero, Quaternion.identity);
                        createdProjectile.name = projectileName;

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
        GunData newGunData = ScriptableObject.CreateInstance(typeof(GunData)) as GunData;
        
        newGunData.InitialiseGunData(fireMode, ammoCategory, projectilePrefab, damage, magazineSize, roundsPerMinute, baseCritChance, baseCritMultiplier, baseStunChance);
        
        AssetDatabase.CreateAsset(newGunData, gunPrefabPath + gunName + "Data.asset");
        AssetDatabase.SaveAssets();

        selectedGunData = newGunData;
    }
}
