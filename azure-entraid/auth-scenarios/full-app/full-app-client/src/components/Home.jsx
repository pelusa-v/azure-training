import React from "react";
import { SignIn } from "./SignIn";
import { useIsAuthenticated } from "@azure/msal-react";
import { SignOut } from "./SignOut";
import { UserData } from "./UserData";


export const Home = () => {
    // USE HIS HOOK TO CHECK IF THE USER IS AUTHENTICATED
    const isAuthenticated = useIsAuthenticated();

    return (
        <>
            <h2 className="text-center my-5">
                Full authenticacion app
            </h2>
            
            <div className="text-center fs-5">
                { isAuthenticated ? <UserData/> : "No authenticated!" }
            </div>

            <br />
            
            <div>
            { isAuthenticated ? <SignOut /> : <SignIn />}
            </div>
        </>
    );
};