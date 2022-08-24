import './App.css';
import Information from './Information/Information';
import Creator from './Creator/Creator';
import { Status } from './Status';
import { useEffect, useState } from 'react';
import { dataService } from './DataService';

const pages = {
  info: "info",
  creator: "creator"
}


function NavBar({onClick}) {
  return (
    <div>
      <button onClick={()=>onClick(pages.info)}>Info</button>
      <button onClick={()=>onClick(pages.creator)}>Instellen</button>
    </div>
  );
}


function App() {
  let [page, setPage] = useState(pages.info)

  let [status, setStatus] = useState(null);
  let [commands, setCommands] = useState(null);

  function getCommands(){
      dataService.getCommands().then((result) => {
          console.log(result);
          setCommands(result);
      });
  }

  function getStatus(){
    dataService.getStatus().then((result) => {
      console.log(result);
      setStatus(result);
    });
  }

  function onClickDelete(id) {
    dataService.deleteCommand(id).then(()=>{})
  }

  function onClickClear() {
      dataService.clearCommands().then(()=>{})
  }

  function onClickNav(page) {
    setPage(page);
  }

  useEffect(() => {
      getCommands();
      getStatus();
      dataService.connectToHub((data)=>{
        console.log(data)
        getCommands();
        getStatus();
      })
  }, []);

  return (
    <div>
      <NavBar onClick={onClickNav}/>
      <Status status={status}/>
      {page===pages.info && <Information
        commandList={commands}
        onClickClear={onClickClear} 
        onClickDelete={onClickDelete}
      />}
      {page===pages.creator && <Creator />}
    </div>
  );
}

export default App;
