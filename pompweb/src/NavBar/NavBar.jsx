import { pages } from '../App';
import './NavBar.css'

export function NavBar({ onClick }) {
  return (
    <div className='navbar'>
      <button onClick={() => onClick(pages.info)}>Info</button>
      <button onClick={() => onClick(pages.creator)}>Instellen</button>
    </div>
  );
}
