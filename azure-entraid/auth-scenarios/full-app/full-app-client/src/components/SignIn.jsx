import { useMsal } from "@azure/msal-react";
import React from "react";
import { loginRequestData } from "../auth/config";

// HANDLE LOGIN USING THE SCOPES DEFINED IN THE CONFIGURATION
export const SignIn = () => {
    const { instance } = useMsal();

    const handleLogin = () => {
        instance.loginRedirect(loginRequestData);
        // try {
        //     msal.loginRedirect(loginRequestData);
        // } catch (err) {
        //     console.log(err);
        // }
    };

    return (
        <button onClick={() => handleLogin()} className="btn btn-primary">
            Sign in with Azure
        </button>
    );
};