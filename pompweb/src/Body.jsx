import { List } from './Information/List';
import Creator from './Creator/Creator';
import { Status } from './Status/Status';
import { useEffect, useState } from 'react';
import { dataService } from './DataService';
import { pages } from './App';

export function Body({ page }) {
  let [status, setStatus] = useState(null);
  let [commands, setCommands] = useState(null);

  function getCommands() {
    dataService.getCommands().then((result) => {
      console.log(result);
      setCommands(result);
    });
  }

  function getStatus() {
    dataService.getStatus().then((result) => {
      console.log(result);
      setStatus(result);
    });
  }

  function onClickDelete(id) {
    dataService.deleteCommand(id).then(() => { });
  }

  function onClickClear() {
    dataService.clearCommands().then(() => { });
  }

  useEffect(() => {
    getCommands();
    getStatus();
    dataService.connectToHub((data) => {
      console.log(data);
      getCommands();
      getStatus();
    });
  }, []);

  return (
    <div className='body'>
      <Status status={status} />
      {page === pages.info && <List
        commandList={commands}
        onClickClear={onClickClear}
        onClickDelete={onClickDelete} />}
      {page === pages.creator && <Creator />}
    </div>
  );
}
