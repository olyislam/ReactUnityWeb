using UnityEngine;

public class ManipulatableObject : MonoBehaviour
{
    [SerializeField] private bool isMoveable = true;
    [SerializeField] private bool isRotatable = true;
    [SerializeField] private bool isScalable = false;

    public bool IsMoveable => isMoveable;
    public bool IsRotatable => isRotatable;
    public bool IsScalable => isScalable;

}
