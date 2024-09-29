import React from "react";
import { useMsal } from "@azure/msal-react";


export const SignOut = () => {
  const { instance } = useMsal();

  const handleLogout = () => {
    instance.logoutRedirect({
      postLogoutRedirectUri: "/",
    });
  };

  return (
    <button onClick={() => handleLogout()}>
        Sign out
    </button>
  );
};