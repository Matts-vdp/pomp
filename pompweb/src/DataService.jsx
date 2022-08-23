let url = "https://localhost:7256/Pump/"

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
    } 
};