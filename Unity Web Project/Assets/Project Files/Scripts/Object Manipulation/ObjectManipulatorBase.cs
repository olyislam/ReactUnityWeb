using UnityEngine;

public abstract class ObjectManipulatorBase : ObjectSelectionTracker
{

    [SerializeField] private bool isPerforming = false;
    protected ManipulatableObject targetObj;
    protected virtual void Update()
    {
        OnTickReadInput();
        if (isPerforming)
            OnGoingValidInput();
    }

    protected virtual bool GetMouseHitPoint(out Vector3 hitPoint)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isGroundDetected = Physics.Raycast(ray, out RaycastHit hit);

        hitPoint = hit.point;
        return isGroundDetected;
    }

    protected override void OnSelectObject(GameObject selectedObject)
    {
        targetObj = selectedObject.GetComponent<ManipulatableObject>();
        enabled = true;
        RealignInticator(selectedObject);
    }

    protected override void OnDeselectObject(GameObject selectedObject)
    {
        targetObj = null;
        enabled = false;
    }

    protected abstract void RealignInticator(GameObject selectedObject);
    protected abstract void OnTickReadInput();

    protected virtual void OnBeginValidInput()
    {
        isPerforming = true;
    }

    protected abstract void OnGoingValidInput();

    protected virtual void OnEndValidInput()
    {
        isPerforming = false;
    }
} 