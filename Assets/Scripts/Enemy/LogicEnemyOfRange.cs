using System.Collections.Generic;
using UnityEngine;

public class LogicEnemyOfRange: MonoBehaviour {

    public List<GameObject> CreateAndGetListPrefabs(GameObject prefab, Transform parentTransform) {
        List<GameObject> _fireBalls = new List<GameObject>();

        for (int i = 0; i < 5; i++) {
            GameObject _fireBall = Instantiate(prefab);
            _fireBall.transform.SetParent(parentTransform);
            _fireBall.SetActive(false);
            _fireBalls.Add(_fireBall);
        }

        return _fireBalls;
    }

    public List<FireBall> CreateAndGetListPrefabs(FireBall prefab, Transform parentTransform) {
        List<FireBall> _fireBalls = new List<FireBall>();

        for (int i = 0; i < 5; i++) {
            FireBall _fireBall = Instantiate(prefab);
            _fireBall.Init(parentTransform);
            _fireBall.transform.SetParent(parentTransform);
            _fireBall.gameObject.SetActive(false);
            _fireBalls.Add(_fireBall);
        }

        return _fireBalls;
    }

    public void SetPrefabDirectionShotPointAndEnable(List<GameObject> fireBalls, Transform shotPoint, float direction) {
        foreach (var fireBall in fireBalls) {
            if (!fireBall.activeSelf) {
                fireBall.transform.position = shotPoint.transform.position;
                fireBall.transform.SetParent(null);
                fireBall.transform.localScale = new Vector3(direction, fireBall.transform.localScale.y, fireBall.transform.localScale.z);
                fireBall.SetActive(true);
                return;
            }
        }
    }

    public void SetPrefabDirectionShotPointAndEnable(List<FireBall> fireBalls, Transform shotPoint, float direction) {
        foreach (var fireBall in fireBalls) {
            if (!fireBall.gameObject.activeSelf) {
                fireBall.transform.position = shotPoint.transform.position;
                fireBall.transform.SetParent(null);
                fireBall.transform.localScale = new Vector3(direction, fireBall.transform.localScale.y, fireBall.transform.localScale.z);
                fireBall.gameObject.SetActive(true);
                return;
            }
        }
    }
}
