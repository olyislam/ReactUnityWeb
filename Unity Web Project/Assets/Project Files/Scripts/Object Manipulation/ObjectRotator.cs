using UnityEngine;

public class ObjectRotator : ObjectManipulatorBase
{
    [SerializeField] float maxSelectionDistance = 0.5f;
    [SerializeField] private Renderer lF, lB, rF, rB;

    private Vector3 lastClickPositionIn3D;

    protected override void OnSelectObject(GameObject selectedObject)
    {
      base.OnSelectObject(selectedObject);

        if (manipulatableObj != null)
            enabled = manipulatableObj.IsRotatable;
        else
            enabled = false;
    }

    protected override void RealignInticator(GameObject selectedObject)
    {
        transform.rotation = selectedObject.transform.rotation;

        Vector3 selectorScale = Vector3.one * maxSelectionDistance;
        lF.transform.localScale = selectorScale;
        lB.transform.localScale = selectorScale;
        rF.transform.localScale = selectorScale;
        rB.transform.localScale = selectorScale;

        Vector3 halfScale = selectedObject.transform.localScale * 0.5f;
   
        float x = halfScale.x + selectorScale.x * 0.499f;
        float y = halfScale.y - selectorScale.y * 0.5f;
        float z = halfScale.z + selectorScale.z * 0.499f;

        lF.transform.localPosition = new Vector3(-x, -y, z);
        lB.transform.localPosition = new Vector3(-x, -y, -z);
        rF.transform.localPosition = new Vector3(x, -y, z);
        rB.transform.localPosition = new Vector3(x, -y, -z);
    }

    protected override void OnTickReadInput()
    {
        if (selectedObject == null)
            return;

        if (!manipulatableObj.IsRotatable)
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

    protected override void OnUpdateInticatorVisiblity(bool show)
    {
        lF.enabled = show;
        lB.enabled = show;
        rF.enabled = show;
        rB.enabled = show;
    }
}
