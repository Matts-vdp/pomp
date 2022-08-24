import './App.css';
import List from './Information/Information';
import Creator from './Creator/Creator';
import { Status } from './Status';
import { useState } from 'react';

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

  function onClickNav(page) {
    setPage(page);
  }

  return (
    <div>
      <NavBar onClick={onClickNav}/>
      <Status />
      {page===pages.info && <List />}
      {page===pages.creator && <Creator />}
    </div>
  );
}

export default App;
