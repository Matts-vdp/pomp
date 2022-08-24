import {dateInfo, timeFormat} from "../Util";

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

export function List({commandList, onClickClear, onClickDelete}) {
    return (
        <div>
            <button onClick={onClickClear}>Delete all</button>
            {commandList?.map((value) => <ListItem 
                key={value.id} 
                command={value} 
                onClickDelete={onClickDelete}
            />)}
        </div>
    );
}
