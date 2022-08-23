import dateInfo from "./DateInfo";
import { useEffect, useState } from "react";
import { dataService } from "../DataService";

export function ListItem({command, onClickDelete}) {
    let style = {
        border: "1px solid black",
        margin: "10px",
        padding: "10px"
    }

    return (
        <div style={style}>
            <p>Volgende actie: {dateInfo(command.nextTime)}</p>
            <p>Actie: {command.action? "Aan":"Uit"}</p>
            <p>Aantal keer herhalen: {command.amount}</p>
            <p>Tijd aan: {command.onTime}</p>
            <p>Tijd uit: {command.offTime}</p>
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
