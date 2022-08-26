function pad(num) {
    return String(num).padStart(2, '0')
}

export function dateInfo(date) {
    let time = new Date(date)
    let timeStr = `${pad(time.getHours())}:${pad(time.getMinutes())}:${pad(time.getSeconds())}`
    let dateStr = `${pad(time.getDate())}/${pad(time.getMonth())}/${pad(time.getFullYear())}`
    return {
        time: timeStr,
        date: dateStr
    }
}

export function timeFormat(time) {
    let minutes = Math.floor(time/60)
    let seconds = time- minutes*60;
    
    return `${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}min`
}

export function timeUntil(dateString) {
    let date = new Date(dateString)
    let now = Date.now()
    let until = date.getTime() - now;
    if (until < 0) until = 0;
    let hours = Math.floor(until / (3600*1000));
    until -= hours * (3600*1000)
    let minutes = Math.floor(until/(60*1000))
    until -= minutes * (60*1000)
    let seconds = Math.floor(until/(1000))
    return `${pad(hours)}:${pad(minutes)}:${pad(seconds)}`;
}