import axios from 'axios';
export const getToken = () => {
    return window.localStorage.getItem("token");
}
export const loginRequest = async (loginData) => {
    
    await axios.post("http://localhost:5555/users/login", loginData)
      .then(response => {
        window.localStorage.setItem("token", response.data.token.token);
     })
    .catch(error => {
        console.log(error);
    })
}


export const signupRequest = (signupData) => {
    
    axios.post("http://localhost:5555/users/register", signupData)
      .then(response => {
        console.log(response)
     })
    .catch(error => {
        console.log(error);
    })
}