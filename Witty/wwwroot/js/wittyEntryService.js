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

    return {
        deleteResponse: deleteResponse
    }
}();