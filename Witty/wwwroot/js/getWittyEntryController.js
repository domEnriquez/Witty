let GetWittyEntryController = function (wittyEntryService, filterWittyResponses, utilityFunctions)
{
    let deleteIcon;

    let init = function (containerId) {
        let container = document.getElementById(containerId);
        let responseItems = container.getElementsByClassName("response-item");
        utilityFunctions.addEvents(responseItems, "mouseover", showMenu);
        utilityFunctions.addEvents(responseItems, "mouseout", hideMenu);

        let deleteIcons = container.getElementsByClassName("delete-icon");
        utilityFunctions.addEvents(deleteIcons, "click", deleteResponse);

        filterWittyResponses.init(container);
    }

    let showMenu = function () {
        let menu = this.querySelector(".response-menu");

        utilityFunctions.showElement(menu);
    }

    let hideMenu = function () {
        let menu = this.querySelector(".response-menu");

        utilityFunctions.hideElement(menu);
    }

    let deleteResponse = function () {
        deleteIcon = this;
        let wittyEntryId = deleteIcon.dataset.wittyEntryId;
        let responseId = deleteIcon.dataset.responseId;

        wittyEntryService.deleteResponse(wittyEntryId, responseId, doneDelete, failDelete);
    }

    let doneDelete = function () {
        let responseItem = deleteIcon.closest(".response-item");
        let responseList = responseItem.closest(".response-list");

        responseList.removeChild(responseItem);

        if (responseList.querySelector(".response-item") === null) {
            responseList.closest(".response-card-group").remove();
        }

    }

    let failDelete = function () {
        alert("Something failed!");
    }

    return {
        init: init
    };
}(WittyEntryService, FilterWittyResponses, UtilityFunctions);