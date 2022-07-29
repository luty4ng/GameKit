#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;

[InitializeOnLoad]
[CreateAssetMenu(fileName = "GameKit Settings", menuName = "GameKit/GameKit Settings", order = 1)]
public class GameKitStartUp : ScriptableObject
{
    public int a = 1;
    static AddRequest AddRequest;
    static ListRequest ListRequest;
    static bool AllInstalled = false;
    static GameKitStartUp()
    {
        // Debug.Log($"Check Packages Integrity.");
        ListRequest = Client.List();
        EditorApplication.update += CheckProgress;
    }

    static void Progress()
    {
        if (AddRequest.IsCompleted)
        {
            if (AddRequest.Status == StatusCode.Success)
                Debug.Log("Installing: " + AddRequest.Result.packageId);
            else if (AddRequest.Status >= StatusCode.Failure)
                Debug.Log(AddRequest.Error.message);
            EditorApplication.update -= Progress;
        }
    }

    static void CheckProgress()
    {
        if (AllInstalled)
            return;

        bool IsInstall = false;
        if (ListRequest.IsCompleted)
        {
            if (ListRequest.Status == StatusCode.Success)
            {
                foreach (var package in ListRequest.Result)
                {
                    if (package.name == "com.unity.addressables")
                    {
                        IsInstall = true;
                    }

                }

                if (!IsInstall)
                {
                    AddRequest = Client.Add("com.unity.addressables@1.18.19");
                    EditorApplication.update += Progress;
                }
                else
                {
                    // Debug.Log($"All Packages Has Been Installed.");
                    AllInstalled = true;
                }
            }
            else if (ListRequest.Status >= StatusCode.Failure)
                Debug.Log(ListRequest.Error.message);

            EditorApplication.update -= CheckProgress;
        }
    }
}
#endif