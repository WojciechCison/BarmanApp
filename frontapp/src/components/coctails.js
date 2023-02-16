import React from "react";
import { getAdmin, getToken, getId, removeFavoriteCoctail } from "../services/auth";
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
import { getFavoriteCoctailsList } from "../services/auth";
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import Modal from '@mui/material/Modal';
import AddIcon from '@mui/icons-material/Add';
import TextField from '@mui/material/TextField';
import RemoveIcon from '@mui/icons-material/Remove';
import DeleteIcon from '@mui/icons-material/Delete';
import FavoriteBorder from '@mui/icons-material/FavoriteBorder';
import Favorite from '@mui/icons-material/Favorite';
import StarIcon from '@mui/icons-material/Star';



const brake = { margin: '80px 30px' }
const label = { inputProps: { 'aria-label': 'Checkbox demo' } };


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
        main: '#050505',
        darker: '#053e85',
      },
      neutral: {
        main: '#80461B',
        contrastText: '#fff',
      },
    },
  });

  const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
  };

export default class Coctails extends React.Component{
  constructor(props){
    super(props);
    this.state = { open: false };
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
  
  handleOpen = () => {
    this.setState({ open: true });
  };

  handleClose = () => {
    this.setState({ open: false });
  };
  
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
  DeleteCoctail = (id)  => {
    const token = getToken();
    
    const data = axios.delete(`http://localhost:5555/coctails/${id}`,{data:`"${token}"`} )
    .then(response => {
      
       this.coctailsRequest();
        return response.data 
    })
    .catch(error => {
        console.log(error);
    })
};

async  RemoveFavCoctail (id)  {
  const config = { headers: {'Content-Type': 'application/json'} };
  const token = getToken();
  // console.log(token)
  const userId = getId();
  // console.log(userId)

  const data = await axios.put(`http://localhost:5555/users/Coctails/${userId}/Remove/${id}`, JSON.stringify(token),config)
  .then(response => {
    removeFavoriteCoctail(id);
    this.coctailsRequest();
      return response.data 
  })
  .catch(error => {
      console.log(error);
  })
} 


  
  

  render() {

    const isAdmin = getAdmin();
    const drinks = this.state;
    const display = getFavoriteCoctailsList();
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
            backgroundImage: 'url(https://images.unsplash.com/photo-1572590016064-3e6ae9c04947?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=627&q=80)',
            backgroundRepeat: 'no-repeat',
            backgroundColor: (t) =>
              t.palette.mode === 'light' ? t.palette.grey[50] : t.palette.grey[900],
              backgroundPosition: 'center',
              height: '100%'
          }}
        />

        
        <Grid  alignItems='center'>

            <Box sx={{backgroundColor: "neutral"}}> 
             
             <div> 
            <Typography component="h1" variant="h2" align = 'center'>
              Barman Application

            </Typography>
            </div>
             <ThemeProvider theme={theme}>


                { <NavLink to="/coctails" style={{textDecoration: 'none'}} >
                <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 250 }} variant="contained" startIcon={<StarIcon  />}>
                Favourites  
                </Button>
                </NavLink> }

                { <NavLink to="/ingredients" style={{textDecoration: 'none'}} >
                <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 30 }} variant="contained" startIcon={<LiquorIcon />}>
                
                Ingredients
                </Button>
                </NavLink> }


                { <NavLink to="/user" style={{textDecoration: 'none'}} >
                <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 30 }} variant="contained" startIcon={<LocalBarIcon />}>
                Coctails
                </Button>
                </NavLink> }

                
                { <NavLink style={{textDecoration: 'none'}} to="/"  > 
                <Button color="neutral" onClick={() => clearCacheData()} type="submit" onSubmit = {handleSubmit} style={{ height: 80, width: 200, marginTop: 10, marginLeft: 30 }} variant="contained" startIcon={<LogoutIcon />}>
                
                Logout
                </Button>
                </NavLink>}

                <div> 
                <div>
             {isAdmin === "true" ? ( 
                <Button variant="outlined" style={{ height: 60, width: 200, marginBottom: 0,  marginTop: 80, marginLeft: 30 }} color="primary" onClick={this.handleOpen} startIcon={<AddIcon /> }> 
                  Add New Coctail
                </Button>

              ) : null}
              </div>
                <Modal
          open={this.state.open}
          onClose={this.handleClose}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={style}>
            <Typography id="modal-modal-title" variant="h6" component="h2">
              Add new Coctail
            </Typography>
            <Typography id="modal-modal-description" sx={{ mt: 2 }}>
              Fill in all required informations
              
            </Typography>
            <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12} sm={6}>
                <TextField
          
                  name="name"
                  required
                  fullWidth
                  id="name"
                  label="Coctail Name"
                  autoFocus
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="ingridients"
                  label="Ingridients"
                  name="ingridients"
                
                />
              </Grid>
      

            </Grid>
            <Button
              type="submit"
              fullWidth
              disabled="true"
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              onSubmit = {handleSubmit}
            >
              //Create New Coctail//    Development in progress
            </Button>
          </Box>
          </Box>
            </Modal>

                </div>

                </ThemeProvider>
                
                </Box>

                <Grid style={{margin: '20px 0px 0px 30px'}}  sx={{
        width: 1380,
        
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
          {
          drinks?.data?.filter((a) => {
            return JSON.parse(display).find((b) => 
            {
             return a.id == b.coctailId
            })
          }).map((el) => (
            <StyledTableRow 
              key={el.id}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
              
                {el.id}

                <Button startIcon={<Favorite color="primary" onClick={() => this.RemoveFavCoctail(el.id)}  />} >  </Button>

                <div>
                {isAdmin === "true" ? ( 

                <Button onClick={() => this.DeleteCoctail(el.id)}  startIcon={<DeleteIcon color="action" />} >  </Button>

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





  