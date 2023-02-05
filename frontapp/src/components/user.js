import React from "react";
import { getId, getToken } from "../services/auth";
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
import AddIcon from '@mui/icons-material/Add';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

import Modal from '@mui/material/Modal';
import TextField from '@mui/material/TextField';

const brake = { margin: '80px 30px' }
const label = { inputProps: { 'aria-label': 'Checkbox demo' } };

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

export default class User extends React.Component{
  constructor(props){
    super(props);
    this.state = {
      coctailsData: [],
      commentsData: [],
      open: false,
      open2: false
    }
  }

  handleOpen = (event) => {
    console.log("odpaliles komentarz dla " + event.currentTarget.id)
    this.setState({ open: true, currentCommentId: event.currentTarget.id });

  };

  handleEdit = (event) => {
  
    this.setState({ open2: true });

  };

  EditHandleClose = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    const addNewCommentData2 = {
      comment: data.get("comment"),
      userId: getId(),
      coctailId:this.state.currentCommentId

    }
    this.EditComment(addNewCommentData2);
    this.setState({ open2: false });
    this.coctailsRequest();
  };


  handleClose = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    const addNewCommentData = {
      comment: data.get("comment"),
      userId: getId(),
      coctailId:this.state.currentCommentId

    }
    this.CreateComment(addNewCommentData);
    this.setState({ open: false });
    this.coctailsRequest();
  };

  CreateComment  = (Commentdata) => {
  const token = getToken();
  console.log(Commentdata)
  axios.post(`http://localhost:5555/Comments/${token}`, Commentdata)
  
    .then(response => {
      this.CommentsRequest();
      console.log(response)
   })
  .catch(error => {
      console.log(error);
  })
  
}

DeleteComment = (id)  => {
  const token = getToken();
  console.log(id);
  const data = axios.delete(`http://localhost:5555/Comments/${token}/${id}`)
  .then(response => {
    this.CommentsRequest();
      return response.data 
  })
  .catch(error => {
      console.log(error);
  })
};

EditComment = (id,Commentdata2)  => {
  this.setState({ open2: true });
  const token = getToken();
  console.log(Commentdata2)
  console.log(id);
  const data = axios.put(`http://localhost:5555/Comments/${token}/${id}`, Commentdata2)

  return data;
};

 async coctailsRequest() {
    const token = getToken();
    if(token){
        const coctailsData = await axios.get(`http://localhost:5555/coctails/${token}`)
        .then(response => {
            return response.data 
        })
        .catch(error => {
            console.log(error);
        })
        this.setState({coctailsData})
       return coctailsData
    }
  }

   CommentsRequest() {
    const token = getToken();
    if(token){
        const data =  axios.get(`http://localhost:5555/Comments/${token}`)
        .then(response => {
          
          this.setState({ commentsData: response.data });
            
        })
        .catch(error => {
            console.log(error);
        })
        
        return data
       
    }
  }

  CommentsPost() {
    const token = getToken();
    if(token){
        const data =  axios.post(`http://localhost:5555/Comments/${token}`)
        .then(response => {
          
          this.setState({ commentsData: response.data });
            
        })
        .catch(error => {
            console.log(error);
        })
        
        return data
       
    }
  }


  componentDidMount(){
    this.coctailsRequest();
    this.CommentsRequest()
  }
  

  render() {
    const drinks = this.state.coctailsData;
    const comments = this.state.commentsData;
    console.log(this.state.coctailsData)
    console.log(this.state.commentsData)
    
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
            backgroundImage: 'url(https://images.unsplash.com/photo-1485872299829-c673f5194813?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2054&q=80)',
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
             <ThemeProvider theme={theme}>
             { <NavLink to="/barmanmain" style={{textDecoration: 'none'}} >
             <Button color="neutral" style={{ height: 80, width: 200, marginTop: 10, marginLeft: 120 }} variant="contained" startIcon={<DeckIcon />}>
                Main Page
                </Button>
                </NavLink> }

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

                <Grid style={{margin: '20px 0px 0px 30px'}}  sx={{
        width: 1380,
        maxWidth: '100%',
      }}>
      <TableContainer TableContainer sx={{maxHeight:"60vh", overflowY:"auto"}} >
      <Table sx={{ minWidth: 650, maxWidth: '70vw'}} style={{margin: '20px 0px 0px 30px'}} aria-label="customized table" stickyHeader>
        <TableHead>
          <StyledTableRow >
            <StyledTableCell>Id</StyledTableCell>
            <StyledTableCell>Coctail name</StyledTableCell>
            <StyledTableCell>Description</StyledTableCell>
            
            <StyledTableCell align="right">Comments</StyledTableCell>
            <StyledTableCell ></StyledTableCell>
            
          </StyledTableRow >
        </TableHead>
        <TableBody>
          {drinks?.map((el) => (
            <StyledTableRow 
              key={el.id}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {el.id}
              
                </TableCell>
              <TableCell component="th" scope="row">
                {el.name}
              </TableCell>
              <TableCell component="th" scope="row">
                {el.description}
              </TableCell>

              <TableCell align="right" >
              
              <div>{comments?.map((test) => {
                if(el.id === test.coctailId)
            return(<div>
            {test.comment}
            <Button startIcon={<DeleteIcon color="action" onClick={() => this.DeleteComment(test.id)}  />} >  </Button>
            <Button startIcon={<EditIcon  color="action" id={test.id} onClick={() => this.EditComment(test.id)}/>} >  </Button>
            
            <Modal
          open={this.state.open2}
          onClose={this.EditHandleClose}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={style}>
            <Typography id="modal-modal-title" variant="h6" component="h2">
              Edit Comment 
            </Typography>
            <Typography id="modal-modal-description" sx={{ mt: 2 }}>
            Change your thoughts
            </Typography>
            <Box component="form" noValidate onSubmit={this.EditHandleClose} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={25} sm={10}>
                <TextField
          
                  name="comment"
                  required
                  fullWidth
                  id="comment"
                  label="Comment"
                  autoFocus
                />
              </Grid>

            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              onSubmit={this.EditHandleClose}
            >
              Edit Comment
            </Button>
          </Box>
          </Box>
            </Modal>
            
            </div>)
            
          })}
          </div>
              </TableCell>
              <TableCell align="right">
              <Button  id={el.id} onClick={this.handleOpen} startIcon={<AddIcon /> }> </Button>
              <Modal
          open={this.state.open}
          onClose={this.handleClose}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={style}>
            <Typography id="modal-modal-title" variant="h6" component="h2">
              Add new Comment 
            </Typography>
            <Typography id="modal-modal-description" sx={{ mt: 2 }}>
              What do you think ?
            </Typography>
            <Box component="form" noValidate onSubmit={this.handleClose} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={25} sm={10}>
                <TextField
          
                  name="comment"
                  required
                  fullWidth
                  id="comment"
                  label="Comment"
                  autoFocus
                />
              </Grid>

            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              onSubmit={this.handleClose}
            >
              Add New Comment
            </Button>
          </Box>
          </Box>
            </Modal>
              </TableCell>



            </StyledTableRow >
          ))}
        </TableBody>
      </Table>
    </TableContainer>
                </Grid>
  
      </Grid>
        </Grid>

      </div>

      )
    }
  }





  