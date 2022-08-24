import { List } from "./List";

function Information({commandList, onClickClear, onClickDelete}) {
    return (
      <div>
        <h1>List</h1>
        <List 
          commandList={commandList}
          onClickClear={onClickClear} 
          onClickDelete={onClickDelete}
        />
      </div>
    );
}
export default Information;