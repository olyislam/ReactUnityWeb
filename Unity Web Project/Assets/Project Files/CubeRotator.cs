using UnityEngine;
using UnityEngine.UI;

public class CubeRotator : MonoBehaviour
{
    public float speed = 10;
    [SerializeField] private float rotateSign = 0;

    public Text message;
    private static CubeRotator cubeRotator;

    private void Start()
    {
        cubeRotator = this;
        message.text = "Now it's ready to test";
        message.color = Color.green;
    }

    void Update()
    {
        transform.Rotate(0, rotateSign * speed * Time.deltaTime, 0);
    }

    public void leftrotate()
    {
        rotateSign = 1;
        SendRotateSignToReact(rotateSign);
    }
    public void rightrotate()
    { 
        rotateSign = -1;
        SendRotateSignToReact(rotateSign);
    }
    public void stoptrotate()
    {
        rotateSign = 0;
        SendRotateSignToReact(rotateSign);
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

    private void SendRotateSignToReact(float rotateSign)
    { 
        string js = $"window.parent.postMessage({{ type: 'UPDATE_ROTATESIGN', sign: {rotateSign} }}, '*');";
        Application.ExternalEval(js);
    }
}
