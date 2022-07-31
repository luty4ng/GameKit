using System;

namespace GameKit
{
    public static class EntityExtension
    {
        private const string ENTITY_PATH = "Assets/GameKit/Entity/Demo/Prefab/{0}.prefab";
        private static int s_SerialId = 0;
        public static void ShowEntity(this EntityComponent entityComponent, Type logicType, string AssetName, string entityGroup, int priority, EntityData data)
        {
            if (data == null)
            {
                Utility.Debugger.LogWarning("Data is invalid.");
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, GetEntityAsset(AssetName), entityGroup, priority, data);
        }

        public static int GenerateSerialId(this EntityComponent entityComponent) => --s_SerialId;
        public static string GetEntityAsset(string assetName) => Utility.Text.Format(ENTITY_PATH, assetName);
    }
}