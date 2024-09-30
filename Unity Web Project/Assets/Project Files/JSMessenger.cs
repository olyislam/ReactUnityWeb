using UnityEngine;

public class JSMessenger : MonoBehaviour
{
    protected void SendDataToReact(object value, string jsEventType)
    {
        string js = $"window.parent.postMessage({{ type: '" + jsEventType + "', value: " + value + " }, '*');";
        Application.ExternalEval(js);
    }

    protected void RegisterEventToReceiveData(string ReceivedMethode, string jsEventType)
    {
        string js = @" window.addEventListener('message', function(event){
            if(event.data.type === '" + jsEventType + "') {" +
                "SendMessage('" + gameObject.name + "', '" + ReceivedMethode + "', event.data.value);" +
                "}});";
   
        Application.ExternalEval(js);
    }
}
