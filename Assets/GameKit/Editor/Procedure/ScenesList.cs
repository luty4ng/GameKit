#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
public static class ScenesList
{
    [MenuItem("Scenes/Addressable")]
    public static void Assets__PlayGround_Addressable_Scenes_Addressable_unity() { UpdateSceneList.OpenScene("Assets/_PlayGround/Addressable/Scenes/Addressable.unity"); }
    [MenuItem("Scenes/Attributes")]
    public static void Assets__PlayGround_Attributes_Attributes_unity() { UpdateSceneList.OpenScene("Assets/_PlayGround/Attributes/Attributes.unity"); }
    [MenuItem("Scenes/MyDotween")]
    public static void Assets__PlayGround_MyDotween_MyDotween_unity() { UpdateSceneList.OpenScene("Assets/_PlayGround/MyDotween/MyDotween.unity"); }
    [MenuItem("Scenes/ShaderGround")]
    public static void Assets__PlayGround_ShaderGround_ShaderGround_unity() { UpdateSceneList.OpenScene("Assets/_PlayGround/ShaderGround/ShaderGround.unity"); }
    [MenuItem("Scenes/Basic")]
    public static void Assets__Prototype_Anisotropy_Basic_unity() { UpdateSceneList.OpenScene("Assets/_Prototype/Anisotropy/Basic.unity"); }
    [MenuItem("Scenes/Level1_WorldA")]
    public static void Assets__Prototype_Anisotropy_Worlds_Resources_Level1_Level1_WorldA_unity() { UpdateSceneList.OpenScene("Assets/_Prototype/Anisotropy/Worlds/Resources/Level1/Level1_WorldA.unity"); }
    [MenuItem("Scenes/Level1_WorldB")]
    public static void Assets__Prototype_Anisotropy_Worlds_Resources_Level1_Level1_WorldB_unity() { UpdateSceneList.OpenScene("Assets/_Prototype/Anisotropy/Worlds/Resources/Level1/Level1_WorldB.unity"); }
    [MenuItem("Scenes/Level1_WorldC")]
    public static void Assets__Prototype_Anisotropy_Worlds_Resources_Level1_Level1_WorldC_unity() { UpdateSceneList.OpenScene("Assets/_Prototype/Anisotropy/Worlds/Resources/Level1/Level1_WorldC.unity"); }
    [MenuItem("Scenes/Demo_Manager")]
    public static void Assets_GameKit_Core_Management_Scene_Demo_Manager_unity() { UpdateSceneList.OpenScene("Assets/GameKit/Core/Management/Scene/Demo_Manager.unity"); }
    [MenuItem("Scenes/Demo_UI")]
    public static void Assets_GameKit_Core_UGUI_Demo_Demo_UI_unity() { UpdateSceneList.OpenScene("Assets/GameKit/Core/UGUI/Demo/Demo_UI.unity"); }
    [MenuItem("Scenes/Demo_TopdownMove")]
    public static void Assets_GameKit_Features_TopdownMove_Scene_Demo_TopdownMove_unity() { UpdateSceneList.OpenScene("Assets/GameKit/Features/TopdownMove/Scene/Demo_TopdownMove.unity"); }
}
#endif