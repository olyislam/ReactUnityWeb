using UnityEngine;

public abstract class ObjectManipulatorBase : ObjectSelectionTracker
{
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private bool isPerforming = false;
    protected ManipulatableObject manipulatableObj;

    public bool IsPerforming => isPerforming;
    protected virtual void Update()
    {
        OnTickReadInput();
        if (isPerforming)
            OnGoingValidInput();
    }

    protected virtual bool GetMouseHitPoint(out Vector3 hitPoint)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isGroundDetected = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayer);

        hitPoint = hit.point;
        return isGroundDetected;
    }

    protected override void OnSelectObject(GameObject selectedObject)
    {
        manipulatableObj = selectedObject.GetComponent<ManipulatableObject>();
        enabled = true;
        RealignInticator(selectedObject);
        OnUpdateInticatorVisiblity(true);
    }

    protected override void OnDeselectObject(GameObject selectedObject)
    {
        manipulatableObj = null;
        selectedObject = null;
        enabled = false;
        OnUpdateInticatorVisiblity(false);
    }

    protected abstract void RealignInticator(GameObject selectedObject);
    protected abstract void OnUpdateInticatorVisiblity(bool show);
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