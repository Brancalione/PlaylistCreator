
import Principal from './components/Principal.jsx'
import React from "react";
import { BrowserRouter, Route, Routes } from 'react-router-dom';

function App(){
    return (
      <main>
      <BrowserRouter>
          <Routes>
              <Route index path="/" element={<Principal />} />
          </Routes>
      </BrowserRouter>
  </main>
);
}
    
export default App;