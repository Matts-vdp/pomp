function dateInfo(date) {
    let time = new Date(date)
    
    let str = `${time.getHours()}:${time.getMinutes()}:${time.getSeconds()}u op ${time.getDay()}/${time.getMonth()}/${time.getFullYear()}`
    return str
}
export default dateInfo;