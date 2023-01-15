import React from "react";
import SignUp from "../components/signup";
import Login from "../components/login";

const SignInOutContainer=()=>{
    const [value, setValue]= useState(0)
    const handleChange = (event, newValue) => {
        setValue(newValue);
                                             };
    return (
        <div><SignUp /> </div>
    )
}