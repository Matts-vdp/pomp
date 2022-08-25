import { useState } from "react";
import { dataService } from "../DataService";
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

function RepeatedCommandForm() {
  let [state, setState] = useState({
    offTime: 0,
    offTimeSec: 0,
    onTime: 0, 
    onTimeSec: 0, 
    amount: 1
  })

  function handleChange(event) {
    setState((prev)=>{
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
      state.offTime*60+state.offTimeSec,
      state.onTime*60+state.onTimeSec,
      state.amount
    )
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
      <button className="button on" onClick={onClick}>Stel in</button>
    </div>
  )
}

function Creator() {
    return (
      <div className="creator">
        <h1>Stel in</h1>
        <RepeatedCommandForm />
      </div>
    );
}
export default Creator;