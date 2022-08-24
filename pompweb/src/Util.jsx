function pad(num) {
    return String(num).padStart(2, '0')
}

export function dateInfo(date) {
    let time = new Date(date)
    let timeStr = `${pad(time.getHours())}:${pad(time.getMinutes())}:${pad(time.getSeconds())}`
    let dateStr = `${pad(time.getDay())}/${pad(time.getMonth())}/${pad(time.getFullYear())}`
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