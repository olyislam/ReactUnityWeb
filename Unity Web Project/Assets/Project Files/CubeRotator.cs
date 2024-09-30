using System;
using UnityEngine;
using UnityEngine.UI;

public class CubeRotator : JSMessenger
{
    public float speed = 10;
    [SerializeField] private float rotateSign = 0;

    public Text message;
    private static CubeRotator cubeRotator;

    private void Start()
    {
        RegisterEventToReceiveData(nameof(ReceiveDataFromReact), "CALL_UNITY_FUNCTION");

        cubeRotator = this;
        message.text = "Now it's ready to test";
        message.color = Color.green;
    }

    void Update()
    {
        transform.Rotate(0, rotateSign * speed * Time.deltaTime, 0);
    }

    private void ReceiveDataFromReact(object speedMultiplayer) => rotateSign *= (float)speedMultiplayer;

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
