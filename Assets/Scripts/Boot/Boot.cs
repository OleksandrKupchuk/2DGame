using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour {
    [SerializeField]
    private string _sceneName;

    private void OnEnable() {
        ProjectContext.Instance.Init();
    }

    private IEnumerator Start() {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(_sceneName);
    }
}
