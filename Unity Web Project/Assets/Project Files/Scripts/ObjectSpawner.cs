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

    public void Spawn(string id, Vector3 pos)
    { 
        var obj = Spawn(id);
        if(obj)
            obj.transform.position = pos;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var spawnPoint = new Vector3(Random.Range(-4.5f, 4.5f), 0.5f, Random.Range(-4.5f, 4.5f));
            int index = Random.Range(0, prefabs.Length-1);

            if (prefabs.Length > 0)
                Spawn(prefabs[index].id, spawnPoint);
            else
            { 
                var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = spawnPoint;
                obj.AddComponent<BoxCollider>();
                obj.AddComponent<ManipulatableObject>();
            }
        }
    }
}
 