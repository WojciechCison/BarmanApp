import axios from 'axios';
export const getToken = () => {
    return window.localStorage.getItem("token");
}

export const getId = () => {
    return window.localStorage.getItem("userId");
}

export const getFavoriteCoctailsList = () => {
    return window.localStorage.getItem("favoriteCoctailsList");
}

export const getName = () => {
    return window.localStorage.getItem("name");
}

export const loginRequest = async (loginData) => {
    
    await axios.post("http://localhost:5555/users/login", loginData)
      .then(response => {
        window.localStorage.setItem("token", response.data.token.token);
        window.localStorage.setItem("userId", response.data.token.userId);
        window.localStorage.setItem("name", response.data.user.name);
        window.localStorage.setItem("favoriteCoctailsList", JSON.stringify(response.data.user.favoriteCoctailsList));
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

export const loginGitHub = () => {
    
    axios.get("http://localhost:5555/users/Github")
      .then(response => {
        console.log(response)
     })
    .catch(error => {
        console.log(error);
    })
}

