using UnityEngine;

public class ObjectSelectionTracker : MonoBehaviour
{
    protected GameObject selectedObject;

    public virtual void UpdateSelection(GameObject selectedObject)
    {
        if (this.selectedObject == selectedObject)
            return;

        if (selectedObject != null)
            OnSelectObject(selectedObject);
        else
            OnDeselectObject(this.selectedObject);

        this.selectedObject = selectedObject;
    }

    protected virtual void OnSelectObject(GameObject selectedObject) { }

    protected virtual void OnDeselectObject(GameObject selectedObject) { }

}
