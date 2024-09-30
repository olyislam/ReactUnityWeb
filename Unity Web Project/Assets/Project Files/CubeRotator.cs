using UnityEngine;
using UnityEngine.UI;

public class CubeRotator : JSMessenger
{
    public class SimpleHash
    {
        public string mesh;
        public float px;
        public float py;
        public float pz;
        public float rx;
        public float ry;
        public float rz;
    }

    public float speed = 10;
    [SerializeField] private float rotateSign = 0;

    public Text message;
    private static CubeRotator cubeRotator;

    private void Start()
    {
        RegisterEventToReceiveData(nameof(ReceiveDataFromReact), "CALL_UNITY_FUNCTION");
        RegisterEventToReceiveData(nameof(SpawnGameObject), "SpawnGameObject");

        cubeRotator = this;
        message.text = "Now it's ready to test";
        message.color = Color.green;
    }

    void Update()
    {
        transform.Rotate(0, rotateSign * speed * Time.deltaTime, 0);
    }

    private void ReceiveDataFromReact(object speedMultiplayer) => rotateSign *= (float)speedMultiplayer;
    private void SpawnGameObject(object speedMultiplayer)
    {
        var json = speedMultiplayer as string;
        Debug.Log("Unity Logging");
        SendDataToReact(json, "Console_Log");
        var h = JsonUtility.FromJson<SimpleHash>(json);
        var mesh = h.mesh;
        var pos = new Vector3((float)h.px, (float)h.py, h.pz);
        var rot = Quaternion.Euler(new Vector3((float)h.rx, (float)h.ry, (float)h.rz));


        if (mesh == "Cube")
            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.SetPositionAndRotation(pos, rot);
        else
            GameObject.CreatePrimitive(PrimitiveType.Cylinder).transform.SetPositionAndRotation(pos, rot);
    }
    public void leftrotate()
    {
        rotateSign = 1;
        SendDataToReact(rotateSign, "UPDATE_ROTATESIGN");
    }
    public void rightrotate()
    { 
        rotateSign = -1;
        SendDataToReact(rotateSign, "UPDATE_ROTATESIGN");
    }
    public void stoptrotate()
    {
        rotateSign = 0;
        SendDataToReact(rotateSign, "UPDATE_ROTATESIGN");
    }
    public static void sleftrotate()
    {
        cubeRotator.leftrotate();
    }
    public static void srightrotate()
    {
        cubeRotator.rightrotate();
    }
    public static void sstoptrotate()
    {
        cubeRotator.stoptrotate(); 
    }
}
