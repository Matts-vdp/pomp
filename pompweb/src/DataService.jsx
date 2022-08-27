import { HubConnectionBuilder } from '@microsoft/signalr';
let baseUrl = "/"
// let baseUrl = "http://localhost:5000/"
let url = baseUrl + "Pump/"
let connection = null
let started = false

export const dataService = {
    getStatus: async ()=>{
        let response = await fetch(url + "Status");
        let js = await response.json();
        return js;
    },

    getCommands: async () => {
        let response = await fetch(url + "Commands");
        let js = await response.json();
        return js;
    },

    deleteCommand: async (id) => {
        let response = await fetch(url + "Commands/" + id, {
            method: "DELETE"
        });
        return response
    },

    clearCommands: async (id) => {
        let response = await fetch(url + "Clear", {
            method: "POST"
        });
        return response
    },

    addBasicCommand: async (action) => {
        let response = await fetch(url + "BasicCommand?action="+action, {
            method: "POST"
        });
        return response
    },

    addRepeatedCommand: async (offTime, onTime, amount, startTime, startDate) => {
        let params = `offTime=${offTime}&onTime=${onTime}&amount=${amount}`
        if (startTime !== "" && startDate !== "") {
            let time = startTime + ";" + startDate
            params += `&startTime=${time}`
        }
        let response = await fetch(url + "RepeatedCommand?" + params, {
            method: "POST"
        });
        return response
    },


    connectToHub: (func) => {
        if (connection == null)
            connection = new HubConnectionBuilder()
                .withUrl(baseUrl+"updatehub")
                .build()
        if (!started) {
            connection.start().then(()=>{
                started = true
                connection.on("update", data => {
                    func(data);
                });
            })
        }
        else {
            connection.on("update", data => {
                func(data);
            });
        }
    }
};