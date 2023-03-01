const storageHelper = {
    get: (key) => {
        let parsedData;

        if (localStorage)
        {
            const data = localStorage.getItem(key);
            parsedData = JSON.parse(data);
        }

        return parsedData;
    },
    
    set: (key, data) => {
        if (localStorage)
        {
            localStorage.setItem(key, JSON.stringify(data));
        }        
    },
    
    remove: (key) => {
        if (localStorage)
        {
            localStorage.removeItem(key);
        }
    },
};

export default storageHelper;
