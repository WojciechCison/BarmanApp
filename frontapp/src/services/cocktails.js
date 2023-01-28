import { getToken } from "./auth";
import axios from "axios";
export const coctailsRequest = async () => {
    const token = getToken();
    if(token){
        const data = await axios.get(`http://localhost:5555/coctails/${token}`)
        .then(response => {
            return response.data 
        })
        .catch(error => {
            console.log(error);
        })
        return data
    }

}
