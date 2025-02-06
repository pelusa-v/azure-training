import { LogLevel, PublicClientApplication } from "@azure/msal-browser";

// FIRST, ADD RELEVANT CONFIGURATION OF AZURE APPLICATION
export const msalConfig = {
    auth: {
        clientId: "4148b6ed-6ee6-416e-9a22-d0eda46806d4",
        authority: "https://login.microsoftonline.com/27b8e21b-adae-4ec5-8fe0-85042b64d2d4",
        redirectUri: "http://localhost:3000",
    },
    cache: {
        cacheLocation: "sessionStorage",
        storeAuthStateInCookie: true,
    },
    // system: {
    //     loggerOptions: {
    //         loggerCallback(logLevel, message, containsPii) {
    //             console.info(message);
    //             if (containsPii) {
    //                 return;
    //             }
    //             switch (logLevel) {
    //                 case LogLevel.Error:
    //                     console.error(message);
    //                     return;
    //                 case LogLevel.Warning:
    //                     console.warn(message);
    //                     return;
    //                 default:
    //                     return;
    //             }
    //         },
    //         logLevel: LogLevel.Verbose,
    //     },
    // },
};

// THEN, DEFINE THE SCOPES YOU NEED FOR THE APPLICATION
export const loginRequestData = {
    scopes: ["User.Read"]
};


// FINALLY, CREATE AN INSTANCE OF MSAL TO USE IN YOUR APPLICATION (index.js)
export const appMsalInstance = new PublicClientApplication(msalConfig);