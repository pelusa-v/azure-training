import React, { useEffect, useState } from "react";

import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import { SignIn } from "./SignIn";
import { SignOut } from "./SignOut";
import { Profile } from "./Profile";
import { loginRequest } from "../auth/authConfig";
import { callMsGraph } from "../auth/graph";


export const Home = () => {
    
    const isAuthenticated = useIsAuthenticated();

    return (
        <>
            <h2>
                Complete authenticacion app!
            </h2>

            <br />
            <br />

            <div className="collapse navbar-collapse justify-content-end">
            {isAuthenticated ? <SignOut /> : <SignIn />}
            </div>
            { isAuthenticated ? <Profile/> : "No authenticated!" }
        </>
    );
};