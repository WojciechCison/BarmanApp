import './App.css';
import React from 'react';
import Login from './components/login';
import Signup from './components/signup';
import Link from '@mui/material/Link';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import SignInSide from './components/login';
import SignUp from './components/signup';
import Barmanmain from './components/barmanmain';
import Coctails from './components/coctails';
import Ingredients from './components/ingredients';
import User from './components/user';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<SignInSide></SignInSide>} />
        <Route path='/signup' element={<SignUp></SignUp>} />
        <Route path='/barmanmain' element={<Barmanmain></Barmanmain>} />
        <Route path='/coctails' element={<Coctails></Coctails>} />
        <Route path='/ingredients' element={<Ingredients></Ingredients>} />
        <Route path='/user' element={<User></User>} />
      </Routes>
    
    </BrowserRouter>
  );
}
/* jak chcecie odpalić rejestracje to zmieńcie powyżej Login na signup */


export default App;
