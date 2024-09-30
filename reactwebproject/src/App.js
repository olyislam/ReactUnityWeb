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
  if(jsEventType === "UPDATE_ROTATESIGN"){
    document.getElementById("rotateSign").innerHTML = "Rotate Sign:" + value;
  }
}

const sendEventDataToUnity = (jsEventType, value) => {
  const iframe = document.querySelector('iframe');
  if(iframe){
    iframe.contentWindow.postMessage({type: jsEventType, value: value}, '*');
  }
}


  return (
    <div className="App">
      <header className="App-header">
        <p> Welcome to React Unity Webgpu Sample Project </p>
        <button onClick={()=> sendEventDataToUnity('CALL_UNITY_FUNCTION', 2.0)}> Multiply Rotate Speed</button>
        <p id="rotateSign">Rotate Sign: 0 </p>
        <iframe src="http://localhost/UnityWebBuild/" width={1000} height={500} ></iframe>
      </header>
    </div>
  );
}

export default App;
