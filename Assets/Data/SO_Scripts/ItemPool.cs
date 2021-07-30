using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEditor;

[CreateAssetMenu(fileName = "ItemPool", menuName = "Pool/ItemPool")]
public class ItemPool : ScriptableObject
{
    public List<ItemData> pool;
    public ItemData defaultItem;
    public ItemData CreateItemSO(string name)
    {
        ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
        ItemData template = GetAsset(name);
        SetValue(ref itemData, ref template);

        itemData.instanceName = itemData.idName + itemData.GetInstanceID().ToString();
        itemData.name = itemData.instanceName;
        itemData.currentOverlap = 1;
        return itemData;
    }

    private void SetValue(ref ItemData itemData, ref ItemData template)
    {
        itemData.idName = template.idName;
        itemData.showName = template.showName;
        itemData.type = template.type;
        itemData.itemDesc = template.itemDesc;
        itemData.image = template.image;
        itemData.holdImage = template.holdImage;
        itemData.maxOverlap = template.maxOverlap;
        itemData.useProjectile = template.useProjectile;
        itemData.projectileSpeed = template.projectileSpeed;
        itemData.coolDown = template.coolDown;
        itemData.instanceName = template.instanceName;
        itemData.currentOverlap = template.currentOverlap;
        itemData.indexOnInventory = template.indexOnInventory;
        itemData.durability = template.durability;
        itemData.craftFrom = template.craftFrom;
        itemData.canCraft = template.canCraft;
        itemData.costData = template.costData;
        itemData.costNum = template.costNum;
        itemData.craftTime = template.craftTime;
        itemData.projectile = template.projectile;
        itemData.recoilSpeed = template.recoilSpeed;
        itemData.shotType = template.shotType;
        itemData.attachDamage = template.attachDamage;
    }

    public ItemData GetAsset(string name)
    {
        foreach (var item in pool)
        {
            if (item.idName == name)
            {
                return item;
            }
        }
        return null;
    }

    public List<ItemData> GetAsset(ItemType type)
    {
        List<ItemData> list = new List<ItemData>();
        foreach (var item in pool)
        {
            if (item.type == type)
            {
                list.Add(item);
            }
        }
        return list;
    }

    public void LoadData(string name, bool isPath = true)
    {
        if (!isPath)
        {
            ItemData data = AssetDatabase.LoadAssetAtPath<ItemData>(name);
            if (!pool.Contains(data))
                pool.Add(data);
        }
        else
        {
            var files = Directory.GetFiles(name);
            foreach (string file in files)
            {
                if (file.Split('.').LastOrDefault() == "meta")
                    continue;
                ItemData data = AssetDatabase.LoadAssetAtPath<ItemData>(file);
                Debug.Log(data);
                if (!pool.Contains(data))
                    pool.Add(data);
            }
        }
    }


    [Button(ButtonSizes.Large)]
    public void Load()
    {
        pool = new List<ItemData>();
        LoadData("Assets/Data/SO_Assets/Individual/");
    }

    [Button(ButtonSizes.Large)]
    public void Clear()
    {
        if (pool != null)
            pool.Clear();
    }


}