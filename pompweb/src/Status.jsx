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

export function Status({status}) {
    let style = {
        border: "1px solid black",
        margin: "10px",
        padding: "10px"
    };

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
