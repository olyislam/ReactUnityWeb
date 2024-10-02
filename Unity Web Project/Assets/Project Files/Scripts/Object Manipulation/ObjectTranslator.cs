using UnityEngine;

public class ObjectTranslator : ObjectManipulatorBase
{

    public GameObject o;

    private void Start() => UpdateSelection(o);


    private Vector3 clickOffset;

    protected override void OnSelectObject(GameObject selectedObject)
    {
        base.OnSelectObject(selectedObject);
        if (selectedObject != null)
            transform.rotation = selectedObject.transform.rotation;
        else
            enabled = false;
    }
    protected override void RealignInticator(GameObject selectedObject)
    {
        transform.position = selectedObject.transform.position;
    }

    protected override void OnTickReadInput()
    {
        if (selectedObject == null)
            return;

        if (!targetObj.IsMoveable)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (GetMouseHitPoint(out Vector3 hitPoint)) 
            {
                var localHitPoint = selectedObject.transform.InverseTransformPoint(hitPoint);
                localHitPoint.x *= selectedObject.transform.localScale.x;
                localHitPoint.y *= selectedObject.transform.localScale.y;
                localHitPoint.z *= selectedObject.transform.localScale.z;
                var halfScale = selectedObject.transform.localScale * 0.5f;

                if(Mathf.Abs(localHitPoint.x) <= halfScale.x && Mathf.Abs(localHitPoint.z) <= halfScale.z)
                    OnBeginValidInput();
            }
        }
        else if (Input.GetMouseButtonUp(0))
            OnEndValidInput();
    }

    protected override void OnBeginValidInput()
    {
        if (GetMouseHitPoint(out Vector3 hitPoint))
        {
            base.OnBeginValidInput();
            clickOffset = hitPoint - selectedObject.transform.position;
        }
    }

    protected override void OnGoingValidInput()
    {
        if (GetMouseHitPoint(out Vector3 hitPoint))
        {
            hitPoint -= clickOffset;
            selectedObject.transform.position = hitPoint;
            transform.position = hitPoint;
        }
    }
}
