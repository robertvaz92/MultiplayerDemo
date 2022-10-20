using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class UtilitiesEditorMeu : Editor
{

    [MenuItem("FOCUS/RESOURCE_LEVEL")]
    private static void SelectResourceLevel()
    {
        EditorUtility.FocusProjectWindow();
        Object obj = AssetDatabase.LoadAssetAtPath<Object>("Assets/Resources/PackBase/Pack_01");
        Selection.activeObject = obj;
        EditorGUIUtility.PingObject(obj);
    }

    [MenuItem("Scenes/LOADING")]
    static void SwitchToLoading()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/StarForce-Swarm/Scenes/LoadingScene.unity");
    }

    [MenuItem("Scenes/INTRO")]
    static void SwitchToIntro()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/StarForce-Swarm/Scenes/Intro Scene.unity");
    }

    [MenuItem("Scenes/MAIN MENU")]
    static void SwitchToMainMenu()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/StarForce-Swarm/Scenes/MainMenuScene.unity");
    }

    [MenuItem("Scenes/EXTERIOR")]
    static void SwitchToExterior()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/StarForce-Swarm/Scenes/Scene_Exterior.unity");
    }

    [MenuItem("Scenes/INTERIOR")]
    static void SwitchToInterior()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/StarForce-Swarm/Scenes/Scene_Interior.unity");
    }

    [MenuItem("Scenes/PRACTICE AREA")]
    static void SwitchPracticeArea()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/StarForce-Swarm/Scenes/Scene_ShootingRange.unity");
    }

    [MenuItem("Scenes/BATTLE OF BRAZIL")]
    static void SwitchToBOB()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/StarForce-Swarm/Scenes/BattleofBrazil.unity");
    }

    [MenuItem("Scenes/NANOTISATION")]
    static void SwitchToNan()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/StarForce-Swarm/Scenes/Scene_Kitchen&NaniteChamber.unity");
    }

    [MenuItem("Scenes/CRASH SCENE")]
    static void SwitchToCrash()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/StarForce-Swarm/Scenes/Scene_SoldierContainerCrash.unity");
    }


    [MenuItem("Scenes/OUTRO")]
    static void SwitchToOutro()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/StarForce-Swarm/Scenes/OutroScene.unity");
    }
}
