import './App.css';
import React from 'react';
import Login from './components/login';
import Signup from './components/signup';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import Link from '@mui/material/Link';
import { useState } from 'react';

function App() {
    return (
        <div className='App'>
            <Login/>
        </div>
    );
}
 //jak chcecie odpalić rejestracje to zmieńcie powyżej Login na signup 
export default App;
