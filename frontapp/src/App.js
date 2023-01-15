import './App.css';
import React from 'react';
import Login from './components/login';
import Signup from './components/signup';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import Link from '@mui/material/Link';

function App() {
  return (
    <div className='App'>
   <Signup/>
    </div>
  );
}
/* jak chcecie odpalić rejestracje to zmieńcie powyżej Login na signup */
export default App;
