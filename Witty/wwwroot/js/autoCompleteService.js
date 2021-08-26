let AutoCompleteService = function () {
    let init = function () {
        const autoCompleteJs = new autoComplete({
            selector: "#autoComplete",
            data: {
                src: async (query) => {                   
                    let xhr = new XMLHttpRequest();
                    xhr.open("GET", "/api/wittyEntry/search/" + query, false);

                    xhr.send();

                    return JSON.parse(xhr.responseText);
                }
            },
            resultItem: {
                highlight: true
            },
            events: {
                input: {
                    selection: (event) => {
                        const selection = event.detail.selection.value;
                        autoCompleteJs.input.value = selection;
                    }
                }
            }
        });
    }

    return {
        init: init
    };
}();