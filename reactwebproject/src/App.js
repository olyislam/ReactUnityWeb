import { useEffect, useState } from 'react';

function App() {

useEffect(()=>{
  const handleMessage = (event) =>{
    console.log(event.data);
    if(event.data.type === "UPDATE_ROTATESIGN"){
      document.getElementById("rotateSign").innerHTML = "Rotate Sign:" + event.data.value;
    }
  };

   window.addEventListener('message', handleMessage);
   return () => {
     window.removeEventListener('message', handleMessage);
   }
})





const sendMessageToUnity = (value) => {
  const iframe = document.querySelector('iframe');
  if(iframe){
    iframe.contentWindow.postMessage({type: 'CALL_UNITY_FUNCTION', value: value}, '*');
  }
}

  return (
    <div className="App">
      <header className="App-header">
        <p> Welcome to React Unity Webgpu Sample Project </p>
        <button onClick={()=> sendMessageToUnity(2.0)}> Multiply Rotate Speed</button>
        <p id="rotateSign">Rotate Sign: 0 </p>
        <iframe src="http://localhost/UnityWebBuild/" width={1000} height={500} ></iframe>
      </header>
    </div>
  );
}

export default App;
