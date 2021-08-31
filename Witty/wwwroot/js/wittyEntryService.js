let WittyEntryService = function () {
    let deleteResponse = function (wittyEntryId, responseId, done, fail) {
        let xhr = new XMLHttpRequest();
        xhr.open("DELETE",
            "/api/wittyEntry/" + wittyEntryId + "&&" + responseId,
            true);

        xhr.onload = done;
        xhr.onerror = fail;

        xhr.send();
    }

    let getRandomWittyEntry = function (done, fail) {
        let xhr = new XMLHttpRequest();
        xhr.open("GET", "/api/wittyEntry/random", true);

        xhr.onload = function () {
            if (this.status === 200) {
                done(JSON.parse(this.responseText));
            }
        };

        xhr.onerror = fail;

        xhr.send();
    }

    return {
        deleteResponse: deleteResponse,
        getRandomWittyEntry: getRandomWittyEntry
    }
}();