import { useState } from "react";
import { dataService } from "../DataService";

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
    <div>
      <label htmlFor="onTime">Tijd aan </label>
      <input 
        type="number" 
        name="onTime" 
        id="onTime" 
        min="0"
        value={state.onTime} 
        onChange={handleChange}
      />:
      <input 
        type="number" 
        name="onTimeSec" 
        id="onTimeSec" 
        min="0"
        max="60"
        value={state.onTimeSec} 
        onChange={handleChange}
      />
      <br />
      <label htmlFor="offTime">Tijd uit </label>
      <input 
        type="number" 
        name="offTime" 
        id="offTime" 
        min="0"
        value={state.offTime} 
        onChange={handleChange}
      />:
      <input 
        type="number" 
        name="offTimeSec" 
        id="offTimeSec" 
        min="0"
        max="60"
        value={state.offTimeSec} 
        onChange={handleChange}
      />
      <br />
      <label htmlFor="amount">Aantal keer herhalen </label>
      <input 
        type="number" 
        name="amount" 
        id="amount" 
        min="1"
        value={state.amount} 
        onChange={handleChange}
      />
      <br />
      <button onClick={onClick}>Stel in</button>
    </div>
  )
}


function Creator() {
    return (
      <div>
        <h1>Creator</h1>
        <RepeatedCommandForm />
      </div>
    );
}
export default Creator;