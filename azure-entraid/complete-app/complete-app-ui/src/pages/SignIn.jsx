import React from "react";
import { useMsal } from "@azure/msal-react";
import { loginRequest } from "../auth/authConfig";


export const SignIn = () => {
  const { instance } = useMsal();

  const handleLogin = () => {
    instance.loginRedirect(loginRequest).catch((e) => {
        console.log(e);
    });
  };
  return (
    <button onClick={() => handleLogin()}>
        Sign in
    </button>
  );
};