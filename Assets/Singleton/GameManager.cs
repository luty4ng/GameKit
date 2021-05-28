using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public List<GameObject> checkers;
    public static GameManager instance {get; private set;}
    private static Dictionary<string, GameObject> objList;
    public static void RegisterObj(string name, GameObject newObj)
    {
        objList.Add(name, newObj);
    }
    public static GameObject GetObject(string name)
    {
        if(objList.ContainsKey(name))
            return objList[name];
        return null;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
        }
        
        InputManager.GetInstance().SetActive(true);
    }
}
