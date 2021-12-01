using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GameKit
{
    [System.Serializable]
    public class LevelRoomData
    {
        public bool isTitled;
        public int gameStage;
        public List<int> passedLevel;

        public LevelRoomData(int stage)
        {
            isTitled = false;
            gameStage = stage;
            passedLevel = new List<int>();
        }
    }

    public class LevelController : MonoBehaviour
    {
        public static LevelController instance;
        public LevelRoomData data;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            if (JsonManager.instance.CheckJsonExist("SerializedData/LevelRoomData"))
                data = JsonManager.instance.LoadJsonData<LevelRoomData>("SerializedData/LevelRoomData");
            else
                data = new LevelRoomData(1);
        }

        public void SaveStage()
        {
            JsonManager.instance.SaveJsonData<LevelRoomData>("SerializedData/LevelRoomData", data);
        }

        public void UpdateStage(int level)
        {
        
        }
        private void OnDestroy()
        {
            SaveStage();
        }

        public int GetStage() => data.gameStage;

    }
}

