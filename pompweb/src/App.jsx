import './App.css';
import { useState } from 'react';
import { NavBar } from './NavBar/NavBar';
import { Body } from './Body';

export const pages = {
  info: "info",
  creator: "creator"
}


function App() {
  let [page, setPage] = useState(pages.info)

  function onClickNav(page) {
    setPage(page);
  }

  return (
    <div>
      <NavBar onClick={onClickNav}/>
      <Body page={page} onClickNav={onClickNav}/>
    </div>
  );
}

export default App;
