#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
public static class ScenesList
{
    [MenuItem("Scenes/Addressable")]
    public static void Assets__PlayGround_Addressable_Scenes_Addressable_unity() { ScenesUpdate.OpenScene("Assets/_PlayGround/Addressable/Scenes/Addressable.unity"); }
    [MenuItem("Scenes/Attributes")]
    public static void Assets__PlayGround_Attributes_Attributes_unity() { ScenesUpdate.OpenScene("Assets/_PlayGround/Attributes/Attributes.unity"); }
    [MenuItem("Scenes/MyDotween")]
    public static void Assets__PlayGround_MyDotween_MyDotween_unity() { ScenesUpdate.OpenScene("Assets/_PlayGround/MyDotween/MyDotween.unity"); }
    [MenuItem("Scenes/ShaderGround")]
    public static void Assets__PlayGround_ShaderGround_ShaderGround_unity() { ScenesUpdate.OpenScene("Assets/_PlayGround/ShaderGround/ShaderGround.unity"); }
    [MenuItem("Scenes/Basic")]
    public static void Assets__Prototype_Anisotropy_Basic_unity() { ScenesUpdate.OpenScene("Assets/_Prototype/Anisotropy/Basic.unity"); }
    [MenuItem("Scenes/Level1_WorldA")]
    public static void Assets__Prototype_Anisotropy_Worlds_Resources_Level1_Level1_WorldA_unity() { ScenesUpdate.OpenScene("Assets/_Prototype/Anisotropy/Worlds/Resources/Level1/Level1_WorldA.unity"); }
    [MenuItem("Scenes/Level1_WorldB")]
    public static void Assets__Prototype_Anisotropy_Worlds_Resources_Level1_Level1_WorldB_unity() { ScenesUpdate.OpenScene("Assets/_Prototype/Anisotropy/Worlds/Resources/Level1/Level1_WorldB.unity"); }
    [MenuItem("Scenes/Level1_WorldC")]
    public static void Assets__Prototype_Anisotropy_Worlds_Resources_Level1_Level1_WorldC_unity() { ScenesUpdate.OpenScene("Assets/_Prototype/Anisotropy/Worlds/Resources/Level1/Level1_WorldC.unity"); }
    [MenuItem("Scenes/GameFramework_Test")]
    public static void Assets__Prototype_GameFramework_Test_GameFramework_Test_unity() { ScenesUpdate.OpenScene("Assets/_Prototype/GameFramework_Test/GameFramework_Test.unity"); }
    [MenuItem("Scenes/Reseter")]
    public static void Assets__Prototype_Reseter_Scenes_Reseter_unity() { ScenesUpdate.OpenScene("Assets/_Prototype/Reseter/Scenes/Reseter.unity"); }
    [MenuItem("Scenes/Demo_UI")]
    public static void Assets_GameKit_Core_UGUI_Demo_Demo_UI_unity() { ScenesUpdate.OpenScene("Assets/GameKit/Core/UGUI/Demo/Demo_UI.unity"); }
    [MenuItem("Scenes/S_Menu")]
    public static void Assets_GameKit_Prototype_Scenes_Procedure_S_Menu_unity() { ScenesUpdate.OpenScene("Assets/GameKit/Prototype/Scenes/Procedure/S_Menu.unity"); }
    [MenuItem("Scenes/S_Procedure")]
    public static void Assets_GameKit_Prototype_Scenes_Procedure_S_Procedure_unity() { ScenesUpdate.OpenScene("Assets/GameKit/Prototype/Scenes/Procedure/S_Procedure.unity"); }
    [MenuItem("Scenes/S_Select")]
    public static void Assets_GameKit_Prototype_Scenes_Procedure_S_Select_unity() { ScenesUpdate.OpenScene("Assets/GameKit/Prototype/Scenes/Procedure/S_Select.unity"); }
}
#endif
