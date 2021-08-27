let AutoCompleteService = function () {
    let init = function () {
        const autoCompleteJs = new autoComplete({
            selector: "#autoComplete",
            data: {
                src: async (query) => {
                    const source = await fetch("/api/wittyEntry/search/" + query);
                    const data = await source.json();

                    return data;
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