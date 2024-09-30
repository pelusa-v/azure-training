import { useMsal } from "@azure/msal-react";
import React, { useEffect, useState } from "react";
import { loginBackendRequest, loginRequest } from "../auth/authConfig";
import { callMsGraph } from "../auth/graph";
import { callItems } from "../resources/itemService";

export const Profile = () => {

  const { instance, accounts } = useMsal();
  const [graphData, setGraphData] = useState(null);
  const [itemsData, setItemsData] = useState(null);

  function GetProfileData() {
    instance
        .acquireTokenSilent({  // get token from azure
            ...loginRequest,
            account: accounts[0],
        })
        .then((response) => {  // get user data from graph
            callMsGraph(response.accessToken).then((response) => setGraphData(response));
        });
  }

  function GetItemsData() {
    instance
        .acquireTokenSilent({  // get token from azure
            ...loginBackendRequest,
            account: accounts[0],
        })
        .then((response) => {  // get user data from backend
            callItems(response.accessToken).then((response) => setItemsData(response));
        });
  }

  useEffect(() => {
    try {
        GetProfileData();
        GetItemsData();
    } catch (error) {
        console.log("No authenticated")
    }
  }, []);


  return (
    <>
      {graphData === null ? "" :
        <div>
          <p>
            <strong>User Name: </strong> {graphData.givenName}
          </p>
          <p>
            <strong>Email: </strong> {graphData.userPrincipalName}
          </p>
          <p>
            <strong>Id: </strong> {graphData.id}
          </p>
        </div>
      }

      {itemsData === null ? "" :
        <div>
          <p>
            <strong>Items: </strong> {itemsData}
          </p>
        </div>
      }
    </>
  );
};