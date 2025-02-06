import { useMsal } from "@azure/msal-react";
import { useEffect, useState } from "react";
import { loginRequestItemsData } from "../auth/config";
import { callItems } from "../services/itemsService";

export const UserData = () => {

    const { instance, accounts } = useMsal();
    const [itemsData, setItemsData] = useState(null);

    function GetItemsData() {
        instance
            .acquireTokenSilent({  // get token from azure
                ...loginRequestItemsData,
                account: accounts[0],
            })
            .then((response) => {  // get user data from backend
                callItems(response.accessToken).then((response) => {
                    setItemsData(response)
                    console.log(response)
                });
            });
    }

    useEffect(() => {
        try {
            console.log("SEE HERE")
            console.log(accounts)
            GetItemsData();
        } catch (error) {
            console.log("No authenticated")
        }
      }, []);
    
    return (
        <div className="text-center fs-5">
            Welcome!
            <div className="text-center my-3">
            {itemsData === null ? "No data" :
                <div>
                <p>
                    <strong>Items: </strong> {itemsData.map((item) => item.name).join(", ")}
                </p>
                </div>
            }
            </div>
        </div>
    )
}