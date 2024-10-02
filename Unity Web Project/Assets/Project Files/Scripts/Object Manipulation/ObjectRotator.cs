using UnityEngine;

public class ObjectRotator : ObjectManipulatorBase
{
    public GameObject o;
    private void Start() =>  UpdateSelection(o);
    


    [SerializeField] float maxSelectionDistance = 0.5f;
    [SerializeField] private Renderer lF, lB, rF, rB;

    private Vector3 lastClickPositionIn3D;

    protected override void OnSelectObject(GameObject selectedObject)
    {
      base.OnSelectObject(selectedObject);

        if (targetObj != null)
        {
            enabled = targetObj.IsRotatable;
            transform.rotation = selectedObject.transform.rotation;
        }
        else
            enabled = false;
    }

    protected override void RealignInticator(GameObject selectedObject)
    {
        transform.rotation = selectedObject.transform.rotation;

        Vector3 selectedObjHalfScale = selectedObject.transform.localScale * 0.5f;
        Vector3 selectorScale = Vector3.one * maxSelectionDistance;
        lF.transform.localScale = selectorScale;
        lB.transform.localScale = selectorScale;
        rF.transform.localScale = selectorScale;
        rB.transform.localScale = selectorScale;

        float AbsGroundOffset = Mathf.Abs(selectedObjHalfScale.y - selectorScale.y * 0.5f);
        selectorScale *= 0.499f;
        lF.transform.localPosition = transform.up * -AbsGroundOffset + transform.right * (selectedObjHalfScale.x + selectorScale.x) * -1 + transform.forward * (selectedObjHalfScale.z + selectorScale.z);
        lB.transform.localPosition = transform.up * -AbsGroundOffset + transform.right * (selectedObjHalfScale.x + selectorScale.x) * -1 + transform.forward * (selectedObjHalfScale.z + selectorScale.z) * -1;
        rF.transform.localPosition = transform.up * -AbsGroundOffset + transform.right * (selectedObjHalfScale.x + selectorScale.x) + transform.forward * (selectedObjHalfScale.z + selectorScale.z);
        rB.transform.localPosition = transform.up * -AbsGroundOffset + transform.right * (selectedObjHalfScale.x + selectorScale.x) + transform.forward * (selectedObjHalfScale.z + selectorScale.z) * -1;
    }

    protected override void OnTickReadInput()
    {
        if (selectedObject == null)
            return;

        if (!targetObj.IsRotatable)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (IsAnyCornerContainMouse())
                    OnBeginValidInput();
        }
        else if (Input.GetMouseButtonUp(0))
            OnEndValidInput();
    }

    private bool ContainInCorner(Vector3 cornerXZ, Vector3 localClickPoint)
    {
        var boundXZ = new Vector3(maxSelectionDistance, 0, maxSelectionDistance) * 0.5f;
        boundXZ.y = 0;
        boundXZ.x *= Mathf.Sign(cornerXZ.x);
        boundXZ.z *= Mathf.Sign(cornerXZ.z);

        cornerXZ += boundXZ;
        var clickOffset = localClickPoint - cornerXZ;

        return Mathf.Abs(clickOffset.x) <= boundXZ.x && Mathf.Abs(clickOffset.z) <= boundXZ.z;
    }

    protected override void OnBeginValidInput()
    {
        if (GetMouseHitPoint(out Vector3 hitPoint))
        { 
            base.OnBeginValidInput();
            lastClickPositionIn3D = hitPoint;
        }
    }

    protected override void OnGoingValidInput()
    {
        if (!GetMouseHitPoint(out Vector3 hitPoint))
            return;
        var lastDirection = (lastClickPositionIn3D - selectedObject.transform.position).normalized;
        var newDirection = (hitPoint - selectedObject.transform.position).normalized;
        float dot = Vector3.Dot(newDirection, lastDirection);
        float angle = -Vector3.SignedAngle(newDirection, lastDirection, Vector3.up);
        selectedObject.transform.Rotate(Vector3.up * angle);
        lastClickPositionIn3D = hitPoint;
        transform.rotation = selectedObject.transform.rotation;
    }

    protected bool IsAnyCornerContainMouse()
    {
        if (!GetMouseHitPoint(out Vector3 hitPoint))
            return false;

        if (IsCornerBoundContain(hitPoint, lF.bounds))
            return true;
        else if (IsCornerBoundContain(hitPoint, lB.bounds))
            return true;
        if (IsCornerBoundContain(hitPoint, rF.bounds))
            return true;
        if (IsCornerBoundContain(hitPoint, rB.bounds))
            return true;

        return false;
    }

    private bool IsCornerBoundContain(Vector3 hitPoint, Bounds cornerBound)
    {
        hitPoint.y = lF.bounds.center.y;
        return cornerBound.Contains(hitPoint);
    }
}
