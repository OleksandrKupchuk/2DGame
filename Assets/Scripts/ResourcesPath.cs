using UnityEngine;

public class ResourcesPath : MonoBehaviour
{
    //Folders
    public static readonly string FolderPrefabs = "Prefabs/";
    public static readonly string FolderFieldOfView = FolderPrefabs + "FieldOfView/";
    public static readonly string FolderTooltip = "Sprites/Inventory/ItemTooltip/";

    //Prefabs
    public static readonly string FieldOfViewPrefab = FolderFieldOfView + "FieldOfView";
    public static readonly string FieldOfViewPrefabByMonkey = FolderFieldOfView + "FieldOfViewByMonkey";

    //Sprites
    public static readonly string SpriteHealth = FolderTooltip + "Health";
    public static readonly string SpriteHealthRegeneration = FolderTooltip + "Health_Regeneration";
}
