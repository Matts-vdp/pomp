import { BrowserRouter, Routes, Route } from 'react-router-dom';

import './App.css';
import List from './Information/Information';
import Creator from './Creator';

function NavBar() {
  return (
    <div>
      <a href="/">List</a>
      <a href="creator">Creator</a>
    </div>
  );
}

function App() {
  return (
    <div>
      <NavBar />
      <BrowserRouter>
        <Routes>
            <Route path="" element={<List />} />
            <Route path="creator" element={<Creator />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
