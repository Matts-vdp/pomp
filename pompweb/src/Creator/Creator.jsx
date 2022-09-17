import { useState } from "react";
import { pages } from "../App";
import { dataService } from "../DataService";
import { pad } from "../Util";
import './Creator.css'

function InputField({name, value, handleChange}) {
  return (
    <div >
      <input 
        className="timeInput"
        type="number" 
        name={name} 
        id={name}
        min="0"
        max="60"
        value={value} 
        onChange={handleChange}
      />
    </div>
  )
}

function RepeatedCommandForm({onClickNav}) {
  let date = new Date(Date.now())
  console.log(`${date.getFullYear()}-${pad(date.getMonth()+1)}-${pad(date.getDate())}`)
  let [state, setState] = useState({
    offTime: 0,
    offTimeSec: 0,
    onTime: 0, 
    onTimeSec: 0, 
    amount: 1,
    startTime: "",
    startDate: `${date.getFullYear()}-${pad(date.getMonth()+1)}-${pad(date.getDate())}`,
  })

  function handleChange(event) {
    setState((prev)=>{
      console.log(event.target.value)
      return {...prev, [event.target.name]:event.target.value }
    })
  }

  function validate(result) {
    if (result.offTime < 0 ) return false;
    if (result.offTimeSec < 0 ||  result.offTimeSec >= 60 ) return false;
    if (result.onTimeSec < 0 ||  result.onTimeSec >= 60 ) return false;
    if (result.amount < 0 ) return false;
    return true;
  }

  function onClick() {
    if (!validate(state)) return;
    dataService.addRepeatedCommand(
      parseInt(state.offTime)*60 + parseInt(state.offTimeSec),
      parseInt(state.onTime)*60 + parseInt(state.onTimeSec),
      state.amount,
      state.startTime,
      state.startDate
    )
    onClickNav(pages.info)
  }

  return (
    <div className="table">
      <p>Tijd aan</p>
      <div className="timeField">
        <InputField name={"onTime"} value={state.onTime} handleChange={handleChange}/>
        <p className="timeEx">min</p>
      </div>
      :
      <div className="timeField">
        <InputField name={"onTimeSec"} value={state.onTimeSec} handleChange={handleChange}/> 
        <p className="timeEx">sec</p>
      </div>
      

      <p>Tijd uit</p>
      <div className="timeField">
        <InputField name={"offTime"} value={state.offTime} handleChange={handleChange}/>
        <p className="timeEx">min</p>
      </div>
      :
      <div className="timeField">
        <InputField name={"offTimeSec"} value={state.offTimeSec} handleChange={handleChange}/> 
        <p className="timeEx">sec</p>
      </div>


      <p className="formHead">Keer herhalen </p>
      <input 
        className="amountInput"
        type="number" 
        name="amount" 
        id="amount" 
        min="1"
        value={state.amount} 
        onChange={handleChange}
      />

      <p className="formHead">Start tijd</p>
      <input 
        className="amountInput"
        type="time" 
        name="startTime" 
        id="startTime" 
        value={state.startTime} 
        onChange={handleChange}
      />
      <p className="formHead">Start datum</p>
      <input 
        className="amountInput"
        type="date" 
        name="startDate" 
        id="startDate" 
        value={state.startDate} 
        onChange={handleChange}
      />

      <button className="button on" onClick={onClick}>Stel in</button>
    </div>
  )
}

function Creator({onClickNav}) {
    return (
      <div className="creator">
        <h2>Stel in</h2>
        <RepeatedCommandForm onClickNav={onClickNav}/>
      </div>
    );
}
export default Creator;