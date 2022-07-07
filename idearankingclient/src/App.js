import './App.css';
import Header from './Components/Header';
import Footer from './Components/Footer';
import Vote from './Components/Vote';
import Form from './Components/Form';
import Idea from './Components/Idea';
import List from './Components/List';
import React from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
} from "react-router-dom";

function App() {
  return (
    <div className="App">
        <Routes> 
          <Route path="/Vote" element={<Header LinkToCreate={true}/>}></Route> 
          <Route path="/" element={<Header LinkToCreate={true}/>}></Route> 
          <Route path="*" element={<Header LinkToCreate={false} />}></Route> 
        </Routes> 
        <Routes> 
          {/* Form */}
          <Route path="/Create" element={<Form />}></Route> 

          <Route path="/All" element={<List/>}></Route> 
          <Route path="/idea/:id" element={<Idea />}></Route> 
          <Route path="*" element={<Vote />}></Route> 
        </Routes> 
        <Footer />
     </div>
  );
}

export default App;
