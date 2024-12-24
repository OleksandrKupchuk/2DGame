using UnityEngine;

public class ResourcesPath : MonoBehaviour
{
    //Assets/Resources/Sprites/Inventory/ItemTooltip/Speed.png
    //Folders
    public static readonly string FolderPrefabs = "Prefabs/";
    public static readonly string FolderFieldOfView = FolderPrefabs + "FieldOfView/";
    public static readonly string FolderTooltip = "Sprites/Inventory/ItemTooltip/";
    public static readonly string FolderPlayerConfig = "Config/PlayerConfig/";

    //Prefabs
    public static readonly string FieldOfViewPrefab = FolderFieldOfView + "FieldOfView";
    public static readonly string FieldOfViewPrefabByMonkey = FolderFieldOfView + "FieldOfViewByMonkey";

    //Sprites
    public static readonly string SpriteHealth = FolderTooltip + "MaxHealth";
    public static readonly string SpriteHealthRegeneration = FolderTooltip + "HealthRegeneration";
    public static readonly string SpriteDamage = FolderTooltip + "Damage";
    public static readonly string SpriteArmor = FolderTooltip + "Armor";
    public static readonly string SpriteSpeed = FolderTooltip + "Speed";
    public static readonly string PlayerConfig = FolderPlayerConfig + "PlayerConfig";
}