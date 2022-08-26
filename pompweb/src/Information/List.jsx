import { useEffect, useState } from "react";
import {dateInfo, timeFormat, timeUntil} from "../Util";
import './List.css'



export function ListItem({command, onClickDelete}) {
    let endDate = dateInfo(command.endTime);
    let [until, setUntil] = useState("");
    useEffect(()=>{
        setUntil(timeUntil(command.nextTime))
        let timer = setInterval(()=>{
            setUntil(timeUntil(command.nextTime))
        }, 200)
        return ()=>{
            clearInterval(timer)
        }
    },[command.nextTime])

    let color = {
        color: command.action? "var(--button-on)": "var(--button-off)"
    }
    return (
        <div className="listItem">
            <p></p>
            <p>Zet <b style={color}>{command.action? "Aan":"Uit"}</b> binnen <b>{until}</b>u </p>
            <p>Klaar om <b>{endDate.time}</b>u op <b>{endDate.date}</b></p>
            <p>Aantal keer herhalen: <b>{command.amount}</b></p>
            <div className="time">
                <div className="timeTable">
                    <p>Tijd aan: </p>
                    <p><b>{timeFormat(command.onTime)}</b></p>
                    <p>Tijd uit: </p>
                    <p><b>{timeFormat(command.offTime)}</b></p>
                </div>
                <button className="button off-border" onClick={()=>onClickDelete(command.id)}>Delete</button>
            </div>
            
        </div>
    )
}

export function List({commandList, onClickClear, onClickDelete}) {
    return (
        <div className="list">
            <div className="listHeader">
                <h2>Geplande acties</h2>
                <button className="button off" onClick={onClickClear}>Delete all</button>
            </div>
            {commandList?.map((value) => <ListItem 
                key={value.id} 
                command={value} 
                onClickDelete={onClickDelete}
            />)}
        </div>
    );
}
