let GetController = function (wittyEntryService)
{
    let deleteIcon;

    let init = function (containerId) {
        let container = document.getElementById(containerId);
        let responseItems = container.getElementsByClassName("response-item");
        addHoverEvents(responseItems);
        let deleteIcons = container.getElementsByClassName("delete-icon");
        addDeleteEvents(deleteIcons);

    }

    let addHoverEvents = function (elemArray) {
        for (let i = 0; i < elemArray.length; i++) {
            elemArray[i].addEventListener("mouseover", showMenu);
            elemArray[i].addEventListener("mouseout", hideMenu);
        }
    }

    let showMenu = function () {
        let menu = this.querySelector(".response-menu");

        if (menu.classList.contains("d-none")) {
            menu.classList.remove("d-none");
        }
    }

    let hideMenu = function () {
        let menu = this.querySelector(".response-menu");

        if (!menu.classList.contains("d-none"))
            menu.classList.add("d-none");
    }

    let addDeleteEvents = function (elemArray) {
        for (let i = 0; i < elemArray.length; i++) {
            elemArray[i].addEventListener("click", deleteResponse);
        }
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
}(WittyEntryService);