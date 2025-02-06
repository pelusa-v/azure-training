import { useMsal } from "@azure/msal-react";

// HANDLE LOGOUT USING THE INSTANCE OF MSAL
export const SignOut = () => {
    const { instance } = useMsal();

    const handleLogout = () => {
        instance.logoutRedirect({
            postLogoutRedirectUri: "/"
        });
    };

    return (
        <button onClick={() => handleLogout()} className="btn btn-danger">
            Sign out
        </button>
    );
}