import { useEffect, useState } from 'react';

function App() {
  
useEffect(()=>{
  const handleMessage = (event) =>{
    console.log(event.data);
    ReceiveEventDataFromUnity(event.data.type, event.data.value);
  };

   window.addEventListener('message', handleMessage);
   return () => {
     window.removeEventListener('message', handleMessage);
   }
})

const ReceiveEventDataFromUnity = (jsEventType, value) => {

  switch(jsEventType) {
    case 'UPDATE_ROTATESIGN':
      document.getElementById("rotateSign").innerHTML = "Rotate Sign:" + value;
      break;
    case 'Console_Log':
      console.log(value);
      break;
    default:
  }
}

const sendEventDataToUnity = (jsEventType, value) => {
  const iframe = document.querySelector('iframe');
  if(iframe){
    iframe.contentWindow.postMessage({type: jsEventType, value: value}, '*');
  }
}

const SpawnGameObject = ()=>{
  var hashtable = {};
  hashtable['mesh'] = 'Cube';
  hashtable['px'] = 2.0;
  hashtable['py'] = 2.0;
  hashtable['pz'] = 0.5;
  hashtable['rx'] = 38.0;
  hashtable['ry'] = 0.0;
  hashtable['rz'] = 88.0;

  var jsonString = JSON.stringify(hashtable);
  sendEventDataToUnity('SpawnGameObject', jsonString);
}

  return (
    <div className="App">
      <header className="App-header">
        <p> Welcome to React Unity Webgpu Sample Project </p>
        <button onClick={()=> sendEventDataToUnity('CALL_UNITY_FUNCTION', 2.0)}> Multiply Rotate Speed</button>
        <button onClick={SpawnGameObject}> Spawn Game Object</button>
        <p id="rotateSign">Rotate Sign: 0 </p>
        <iframe src="http://localhost/UnityWebBuild/" width={1000} height={500} ></iframe>
      </header>
    </div>
  );
}

export default App;
