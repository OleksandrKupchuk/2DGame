using UnityEngine;

public class ResourcesPath : MonoBehaviour
{
    //Folders
    public static readonly string FolderPrefabs = "Prefabs/";
    public static readonly string FolderFieldOfView = FolderPrefabs + "FieldOfView/";

    //Prefabs
    public static readonly string FieldOfViewPrefab = FolderFieldOfView + "FieldOfView";
    public static readonly string FieldOfViewPrefabByMonkey = FolderFieldOfView + "FieldOfViewByMonkey";
}
