let UtilityFunctions = function () {

    let addEvents = function (elemArray, eventName, callback) {
        for (let i = 0; i < elemArray.length; i++) {
            elemArray[i].addEventListener(eventName, callback);
        }
    }

    let hideElement = function (elem) {
        if (!elem.classList.contains("d-none")) {
            elem.classList.add("d-none");
        }
    }

    let showElement = function (elem) {
        if (elem.classList.contains("d-none")) {
            elem.classList.remove("d-none");
        }
    }

    return {
        addEvents: addEvents,
        hideElement: hideElement,
        showElement: showElement
    }
}();