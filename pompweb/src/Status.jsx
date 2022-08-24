import { useEffect, useState } from "react";
import { dataService } from "./DataService";
import {dateInfo} from "./Util";

function BasicCommandForm() {
    function onClick(action) {
      dataService.addBasicCommand(action)
    }
  
    return (
      <div>
        <button onClick={()=>onClick(true)}>Aan</button>
        <button onClick={()=>onClick(false)}>Uit</button>
      </div>
    )
}

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

    let date = dateInfo(status?.lastUsed)
    return (
        <div style={style}>
            <h2>Status</h2>
            <p>Pomp staat: <b>{status?.active ? "Aan" : "Uit"}</b></p>
            <p>Laatste actie: <b>{date.time}</b>u op <b>{date.date}</b></p>
            <BasicCommandForm />
        </div>
    );

}
