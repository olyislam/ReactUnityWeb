import logo from './logo.svg';
import './App.css';
import { useEffect, useState } from 'react';

function App() {
const [sign, setData] = useState(0);
useEffect(()=>{
  const handleMessage = (event) =>{
    console.log(event.data);
    if(event.data.type === "UPDATE_ROTATESIGN"){
      setData(event.data.sign);
    }
  };

   window.addEventListener('message', handleMessage);
   return () => {
     window.removeEventListener('message', handleMessage);
   }
})

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Welcome to React Unity Webgpu Sample Project
        </p>
        <iframe src="http://localhost/UnityWebBuild/" width={1000} height={500} ></iframe>
        
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
