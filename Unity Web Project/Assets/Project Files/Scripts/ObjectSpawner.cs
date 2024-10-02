using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField] private ObjectIdentity[] prefabs;
    public GameObject GetPrefab(string id)
    {
        for (int i = 0; i < prefabs.Length; i++)
            if (prefabs[i].id == id)
                    return prefabs[i].gameObject;

        return null;
    }

    public GameObject Spawn(string id)
    {
        var obj = GetPrefab(id);

        return Instantiate(obj) as GameObject;
    }
}
 