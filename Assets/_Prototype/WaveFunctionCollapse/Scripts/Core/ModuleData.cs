using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using GameKit;

[CreateAssetMenu(menuName = "Wave Function Collapse/Module Data", fileName = "Modules.asset")]
public class ModuleData : ScriptableObject, ISerializationCallbackReceiver
{
    public static Module[] Current;
    public static ModuleData instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public GameObject Prototypes;
    public Module[] Modules;


    // private IEnumerable<Prototype> getPrototypes()
    // {
    //     foreach (Transform transform in this.Prototypes.transform)
    //     {
    //         var item = transform.GetComponent<Prototype>();
    //         if (item != null && item.enabled)
    //         {
    //             yield return item;
    //         }
    //     }
    // }

    public void CreateModules(bool respectNeigborExclusions = true)
    {
        int count = 0;
        var modules = new List<Module>();
        Prototype[] prototypes = Prototypes.GetComponentsInChildren<Prototype>();
        var scenePrototype = new Dictionary<Module, Prototype>();
        for (int i = 0; i < prototypes.Length; i++)
        {
            var prototype = prototypes[i];
            for (int face = 0; face < 6; face++)
            {
                if (prototype.Faces[face].ExcludedNeighbours == null)
                {
                    prototype.Faces[face].ExcludedNeighbours = new Prototype[0];
                }
            }

            for (int rotation = 0; rotation < 4; rotation++)
            {
                if (rotation == 0 || !prototype.CompareRotatedVariants(0, rotation))
                {
                    var module = new Module(prototype.gameObject, rotation, count);
                    modules.Add(module);
                    scenePrototype[module] = prototype;
                    count++;
                }
            }
            EditorUtility.DisplayProgressBar("Creating module prototypes...", prototype.gameObject.name, (float)i / prototypes.Length);
        }

        foreach (var module in modules)
        {
            // module.possibleNeighbors = new List<FaceAdjacent>(new FaceAdjacent[6]);
            for (int direction = 0; direction < 6; direction++)
            {
                var face = scenePrototype[module].Faces[direction];
                // if (face.Socket == 0 && module.name != "Empty R0")
                // {
                //     module.possibleNeighbors[direction].adjacent.Add("Empty R0");
                // }
                // else
                // {
                    IEnumerable<Module> neighbors = modules.Where(neighbor => module.Fits(direction, neighbor));
                    foreach (var neighbor in neighbors)
                    {
                        module.possibleNeighbors[direction].adjacent.Add(neighbor.name);
                    }
                // }

            }
        }

        ModuleData.Current = modules.ToArray();
        EditorUtility.ClearProgressBar();
        this.Modules = modules.ToArray();
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }


    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
    }

    public void SaveConfig()
    {
        JsonManager.instance.SaveJsonData<ModuleData>("WFC/" + this.name, this);
    }

}
