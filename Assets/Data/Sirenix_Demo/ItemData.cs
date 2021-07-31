using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Linq;
using UnityEditor;

public enum ItemType
{
    Empty,
    Weapon,
    Device,
    Gear,
    Resources,
    Materials
}

public enum ShotType
{
    Straight,
    Scatter,
    Recoil,
    ScatterAndRecoil
}


[CreateAssetMenu(fileName = "ItemData", menuName = "Item", order = 1)]

public class ItemData : SerializedScriptableObject
{
    [HideInInspector]
    public static string[] ProduceType = new string[]
    { "Cannot", "Player", "B_Workbench_LV1", "B_Furnace",
        "B_Workshop", "B_Workbench_LV2", "B_Arsenal",
        "B_ModDepot",  "B_MachineTool", "B_Workbench_LV3",
        "B_Centrifuge", "B_Furnace", "B_AutoWorkshop", "B_Electrolyzer"};

    [HideInInspector]
    public static string[] ProjectileChoice = new string[]
    { "Bullet_AP", "Bullet_Acid", "Bullet_FMJ", "Bullet_HP",
        "Bullet_Normal", "Bullet_Prism", "Bullet_Pulse", "Bullet_SABOT"};
    [LabelText("名称（唯一ID）")] public string idName;

    [Header("静态数据（需要配置）")]
    [LabelText("物品名称（中文）")] public string showName;
    [LabelText("物品类型")] public ItemType type;
    [LabelText("物品描述"), TextArea] public string itemDesc;
    [LabelText("场景及背包图像")] public Sprite image;
    [LabelText("持有图像")] public Sprite holdImage;
    [LabelText("最大叠加")] public int maxOverlap = 1;
    [LabelText("使用弹丸"), HideIf("@type != ItemType.Weapon")] public bool useProjectile;
    [LabelText("弹丸类型"), ValueDropdown("ProjectileChoice"), ShowIf("@type == ItemType.Weapon && useProjectile")] public string projectile;
    [LabelText("射击类型"), HideIf("@type != ItemType.Weapon")] public ShotType shotType;
    [LabelText("射击后推"), Range(0f, 10f), ShowIf("@type == ItemType.Weapon && (shotType==ShotType.ScatterAndRecoil || shotType==ShotType.Recoil)")]
    public float recoilSpeed;
    [LabelText("弹丸初速"), HideIf("@!useProjectile")] public float projectileSpeed;
    [LabelText("附加伤害"), HideIf("@!useProjectile")] public float attachDamage;
    [LabelText("射击速度"), HideIf("@!useProjectile"), Range(0, 30)] public float coolDown;
    [LabelText("可制造")] public bool canCraft;
    [LabelText("制造时间"), HideIf("@!canCraft"), ShowInInspector] public float craftTime;
    [LabelText("能被谁制造"), ValueDropdown("ProduceType"), HideIf("@!canCraft")] public string[] craftFrom;
    [LabelText("制造消耗材料"), HideIf("@!canCraft")] public List<ItemData> costData;
    [LabelText("对应消耗数量"), HideIf("@!canCraft")] public List<int> costNum;

    [Space]
    [Header("动态数据（无需配置）")]
    [LabelText("实例名称")] public string instanceName;
    [LabelText("当前叠加")] public int currentOverlap = 1;
    [LabelText("仓库序列")] public int indexOnInventory;
    [LabelText("耐久度")] public float durability;
}