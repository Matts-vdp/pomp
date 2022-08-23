import {dateInfo, timeFormat} from "./Util";
import { useEffect, useState } from "react";
import { dataService } from "../DataService";

export function ListItem({command, onClickDelete}) {
    let style = {
        border: "1px solid black",
        margin: "10px",
        padding: "10px"
    }
    let nextDate = dateInfo(command.nextTime);
    return (
        <div style={style}>
            <p>Zet <b>{command.action? "Aan":"Uit"}</b> om <b>{nextDate.time}</b>u op <b>{nextDate.date}</b></p>
            <p>Aantal keer herhalen: <b>{command.amount}</b></p>
            <p>Waarbij Tijd aan: <b>{timeFormat(command.onTime)}</b> en Tijd uit: <b>{timeFormat(command.offTime)}</b></p>
            <button onClick={()=>onClickDelete(command.id)}>Delete</button>
        </div>
    )
}

export function List() {
    let [commands, setCommands] = useState(null);

    function getCommands(){
        dataService.getCommands().then((result) => {
            console.log(result);
            setCommands(result);
        });
    }

    useEffect(() => {
        getCommands();
    }, []);

    function onClickDelete(id) {
        dataService.deleteCommand(id).then(()=>{
            getCommands();
        })
    }

    function onClickClear() {
        dataService.clearCommands().then(()=>{
            getCommands();
        })
    }

    return (
        <div>
            <button onClick={onClickClear}>Delete all</button>
            {commands?.map((value) => <ListItem 
                key={value.id} 
                command={value} 
                onClickDelete={onClickDelete}
            />)}
        </div>
    );
}
