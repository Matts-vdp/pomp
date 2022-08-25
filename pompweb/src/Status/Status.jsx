import { dataService } from "../DataService";
import {dateInfo} from "../Util";
import './Status.css'

function BasicCommandForm() {
    function onClick(action) {
      dataService.addBasicCommand(action)
    }
  
    return (
      <div>
        <button className="button on" onClick={()=>onClick(true)}>Aan</button>
        <button className="button off" onClick={()=>onClick(false)}>Uit</button>
      </div>
    )
}

export function Status({status}) {
    let date = dateInfo(status?.lastUsed)
    let color = {
      color: status?.active? "var(--button-on)": "var(--button-off)"
    }
    return (
        <div className="status">
            <h2>Status</h2>
            <p>Pomp staat: <b style={color}>{status?.active ? "Aan" : "Uit"}</b></p>
            <p>Laatste actie: <b>{date.time}</b>u op <b>{date.date}</b></p>
            <BasicCommandForm />
        </div>
    );

}
