import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

function NavBar() {
  return (
    <div>
      <a href="/">List</a>
      <a href="creator">Creator</a>
    </div>
  );
}

function List() {
  return (
    <div>
      <h1>List</h1>
    </div>
  );
}

function Creator() {
  return (
    <div>
      <h1>Creator</h1>
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
