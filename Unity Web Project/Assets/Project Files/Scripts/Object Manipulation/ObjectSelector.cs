using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayer;

    private ObjectSelectionTracker[] trackers;
    private GameObject currentSelectedObject;

    private float maxDelayAllowToClick = 0.2f;
    private float maxDragAllowToClick = 2;
    private float lastClickTime = 0;
    private Vector3 clickPoint;

    private void Awake()
    {
        trackers = GetComponentsInChildren<ObjectSelectionTracker>();
        SetSelectedObject(null);
    }

    void Update()
    {
        if (ClickGestureInput())
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayer))
            {
                if (currentSelectedObject == hit.collider.gameObject)
                {
                    SetSelectedObject(null);
                }
                else if (hit.collider.GetComponent<ManipulatableObject>())
                {
                    SetSelectedObject(hit.collider.gameObject);
                }
                else
                {
                    SetSelectedObject(null);
                }
            }
        }            
    }

    private void SetSelectedObject(GameObject newSelectedObject)
    {
        currentSelectedObject = newSelectedObject;
        for (int i = 0; i < trackers.Length; i++)
            trackers[i].UpdateSelection(newSelectedObject);
    }

    private bool ClickGestureInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastClickTime = Time.time;
            clickPoint = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        { 
            float duration = Time.time - lastClickTime;
            if(duration> maxDelayAllowToClick)
                return false;

            if(Vector3.Distance(Input.mousePosition, clickPoint) <= maxDragAllowToClick)
                return true;

        }


        return false;
    }


}
