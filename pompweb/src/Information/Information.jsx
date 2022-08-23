import { List } from "./List";
import { Status } from "./Status";

function Information() {
    return (
      <div>
        <h1>List</h1>
        <Status />
        <div style={{padding: "20px"}}></div>
        <List />
      </div>
    );
}
export default Information;