import { useEffect, useState } from "react";
import { dataService } from "../DataService";
import dateInfo from "./DateInfo";

export function Status() {
    let style = {
        border: "1px solid black",
        margin: "10px",
        padding: "10px"
    };
    let [status, setStatus] = useState(null);

    useEffect(() => {
        dataService.getStatus().then((result) => {
            console.log(result);
            setStatus(result);
        });
    }, []);

    return (
        <div style={style}>
            <h2>Status</h2>
            <p>Pomp staat: {status?.active ? "Aan" : "Uit"}</p>
            <p>Laatste wijziging: {dateInfo(status?.lastUsed)}</p>
        </div>
    );

}
