import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import { ThemeProvider } from '@mui/material/styles';
import { margin } from '@mui/system';
//import axios from 'axios';
import { NavLink } from 'react-router-dom';
import LocalBarIcon from '@mui/icons-material/LocalBar';
import { createTheme } from '@mui/material/styles';
import LiquorIcon from '@mui/icons-material/Liquor';
import WineBarIcon from '@mui/icons-material/WineBar';
import DeckIcon from '@mui/icons-material/Deck';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';


const brake = { margin: '30px 0' }
const theme = createTheme({
    status: {
      danger: '#e53e3e',
    },
    palette: {
      primary: {
        main: '#0971f1',
        darker: '#053e85',
      },
      neutral: {
        main: '#DAA520',
        contrastText: '#fff',
      },
    },
  });
export default function Barmanmain() {
  const handleSubmit = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    const loginData = {
      email: data.get('email'),
      password: data.get('password'),
    };
    console.log(loginData);
    //axios.defaults.headers.post['Access-Control-Allow-Origin'] = 'http://localhost:3000';
    //axios.post("http://localhost:5555/users/login", loginData)
    //   .then(response => {
    //      console.log(response);
    //  })
    // .catch(error => {
    //     console.log(error);
  };
  //  };

  return (
    <ThemeProvider theme={theme}>
      <Grid container component="main" sx={{ height: '100vh' }}>
        <CssBaseline />
        <Grid
          item
          xs={false}
          sm={4}
          md={3}
          sx={{
            backgroundImage: 'url(https://images.unsplash.com/photo-1607446045926-3aee01b43c17?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80)',
            backgroundRepeat: 'no-repeat',
            backgroundColor: (t) =>
              t.palette.mode === 'light' ? t.palette.grey[50] : t.palette.grey[900],
            backgroundSize: 'cover',
            backgroundPosition: 'center',
          }}
        />
        <Grid>
        

        </Grid>
        
        <Grid  alignItems='center'>
        <div> 
            <Typography component="h1" variant="h2" align = 'center'>
              Barman Application

            </Typography>
            </div>
            <Box
            display='flex'
            justifyContent='center'
            alignItems='center'
            align = 'center'
            > 
             
             <ThemeProvider theme={theme}>
             <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 120 }} variant="contained" startIcon={<DeckIcon />}>
                {/* <NavLink to="Coctails" > Coctails </NavLink> */}
                Main Page
                </Button>

                <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 30 }} variant="contained" startIcon={<LocalBarIcon />}>
                {/* <NavLink to="Coctails" > Coctails </NavLink> */}
                Coctails
                </Button>

                <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 30 }} variant="contained" startIcon={<LiquorIcon />}>
                {/* <NavLink to="Coctails" > Coctails </NavLink> */}
                Ingredients
                </Button>

                <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 30 }} variant="contained" startIcon={<WineBarIcon />}>
                {/* <NavLink to="Coctails" > Coctails </NavLink> */}
                Favorite Coctails 
                </Button>

                <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 30 }} variant="contained" startIcon={<AccountCircleIcon />}>
                {/* <NavLink to="Coctails" > Coctails </NavLink> */}
                User
                </Button>
                
                </ThemeProvider>
                
                </Box>

           
      </Grid>
        </Grid>
    </ThemeProvider>
  );
};
