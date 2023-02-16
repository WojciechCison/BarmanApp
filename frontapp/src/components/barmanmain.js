import React  from "react";
import { getAdmin, getId, getName, getToken } from "../services/auth";
import axios from "axios";
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import Checkbox from '@mui/material/Checkbox';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import { ThemeProvider } from '@mui/material/styles';
import { NavLink, Navigate, useNavigate } from 'react-router-dom';
import LocalBarIcon from '@mui/icons-material/LocalBar';
import { createTheme } from '@mui/material/styles';
import LiquorIcon from '@mui/icons-material/Liquor';
import DeckIcon from '@mui/icons-material/Deck';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import LogoutIcon from '@mui/icons-material/Logout';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import { coctailsRequest } from '../services/cocktails';
import { useState } from 'react';
import { styled } from '@mui/material/styles';
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import FavoriteBorder from '@mui/icons-material/FavoriteBorder';
import Favorite from '@mui/icons-material/Favorite';
import { getFavoriteCoctailsList } from "../services/auth";
import PropTypes from 'prop-types';
import AdSense from 'react-adsense';


const brake = { margin: '80px 30px' }
const label = { inputProps: { 'aria-label': 'Checkbox demo' } };

const isAdmin = getAdmin();
const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: '#6B4D37',
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

const handleSubmit = (event) => {
  event.preventDefault();
  const data = new FormData(event.currentTarget);
  const loginData = {
    email: data.get('email'),
    password: data.get('password'),
  };
  console.log(loginData);
  
};

const StyledTableRow = styled(TableRow)(({ theme }) => ({
  '&:nth-of-type(odd)': {
    backgroundColor: theme.palette.action.hover,
  },

  '&:last-child td, &:last-child th': {
    border: 0,
  },
}));

export  const clearCacheData = async () => {
  caches.keys().then((names) => {
    names.forEach((name) => {
      caches.delete(name);
    });
  });
  alert('Logout')
  localStorage.clear();
};

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
        main: '#80461B',
        contrastText: '#fff',
      },
    },
  });

  
  
export default class Barmanmain extends React.Component{
  constructor(props){
    super();
    this.state = {}
    this.checkedlist = JSON.parse(getFavoriteCoctailsList()).map(a => a.coctailId);
    
    
  }

  handleFavouriteChange(id){
    
    const data = new FormData(id.currentTarget);
  
    if(data === 1 ){
      this.RemoveFavCoctail(this.el.id)
      this.checkedlist = JSON.parse(getFavoriteCoctailsList()).map(a => a.coctailId);
    }
    else{
    this.FavCoctail((this.el.id))
    }
  }

 async coctailsRequest() {
    const token = getToken();
    if(token){
        const data = await axios.get(`http://localhost:5555/coctails/${token}`)
        .then(response => {
            return response.data 
        })
        .catch(error => {
            console.log(error);
        })
        this.setState({data: data})
        return data
    }
  }
  componentDidMount(){
    this.coctailsRequest();

  }
  async  FavCoctail (id)  {
    const token = getToken();
    // console.log(token)
    const userId = getId();
    // console.log(userId)
  
    const response = await axios.put(
      `http://localhost:5555/users/Coctails/${userId}/Add/${id}`,
      `${token}` 
    );
    return response;
  } 

  async  RemoveFavCoctail (id)  {
    const token = getToken();
    // console.log(token)
    const userId = getId();
    // console.log(userId)
  
    const response = await axios.put(
      `http://localhost:5233/users/Coctails/${userId}/Remove/${id}`,
      `${token}` 
    );
    return response;
  } 
 

  render() {

    const isAdmin = getAdmin();
    const drinks = this.state;
    // console.log(isAdmin)
    return (
      <div>
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

        
        <Grid  alignItems='center'>

            <Box sx={{backgroundColor: "neutral"}}> 
             
             <div> 
            <Typography component="h1" variant="h2" align = 'center'>
              Barman Application

            </Typography>
            </div>
                
          
          <AdSense.Google
  client="ca-pub-7181337091431123"
  slot="7060336981"
  style={{ display: 'block' }}
  data-ad-format="auto"
  data-full-width-responsive="true"
  data-adtest="on"
  
/>
            
             <ThemeProvider theme={theme}>
             <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 120 }} variant="contained" startIcon={<DeckIcon />}>
                
                Main Page
                </Button>

                { <NavLink to="/coctails" style={{textDecoration: 'none'}} >
                <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 30 }} variant="contained" startIcon={<LocalBarIcon />}>
                 Coctails  
                </Button>
                </NavLink> }


                { <NavLink to="/ingredients" style={{textDecoration: 'none'}} >
                <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 30 }} variant="contained" startIcon={<LiquorIcon />}>
                
                Ingredients
                </Button>
                </NavLink> }


                { <NavLink to="/user" style={{textDecoration: 'none'}} >
                <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 30 }} variant="contained" startIcon={<AccountCircleIcon />}>
                User
                </Button>
                </NavLink> }


                { <NavLink style={{textDecoration: 'none'}} to="/"  > 
                <Button color="neutral" onClick={() => clearCacheData()} type="submit" onSubmit = {handleSubmit} style={{ height: 80, width: 200, marginTop: 10, marginLeft: 30 }} variant="contained" startIcon={<LogoutIcon />}>
                
                Logout
                </Button>
                </NavLink> }
              
                </ThemeProvider>
                
                </Box>

                <Grid style={{margin: '20px 0px 0px 30px'}} sx={{
        width: 1380,
        maxWidth: '100%',
      }}>
                </Grid>

                <TableContainer TableContainer sx={{maxHeight:"60vh", overflowY:"auto"}} >
      <Table sx={{ minWidth: 650, maxWidth: '70vw'}} style={{margin: '20px 0px 0px 30px'}} aria-label="customized table" stickyHeader>
        <TableHead>
          <StyledTableRow >
            <StyledTableCell>Id</StyledTableCell>
            <StyledTableCell>Coctail name</StyledTableCell>
            <StyledTableCell>Description</StyledTableCell>
            <StyledTableCell align='right'>Ingridients</StyledTableCell>
            
          </StyledTableRow >
        </TableHead>
        <TableBody>
          {drinks?.data?.map((el) => (
            <StyledTableRow 
              key={el.id}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {el.id}
                
                <div>
             {isAdmin === "false" ? (
             <Checkbox {...label} onChange={() => this.handleFavouriteChange(el.id)} icon={<FavoriteBorder /> } checkedIcon={<Favorite color="secondary" /> } checked={this.checkedlist.includes(el.id)} on />
              ) : null}
              </div>

                </TableCell>
              <TableCell component="th" scope="row">
                {el.name}
              </TableCell>
              <TableCell component="th" scope="row">
              {el.description}
              </TableCell>
              <TableCell align="right">
              
              <div>{el.coctailIngridients.map((test) => {
            return(<div>{test.name+" "+test.dose+"x"+test.unit}</div>)
          })}</div>
              
              
              </TableCell>

            </StyledTableRow >
          ))}
        </TableBody>
      </Table>
    </TableContainer>

      </Grid>
        </Grid>

      </div>

      




      )
    }
  }





  