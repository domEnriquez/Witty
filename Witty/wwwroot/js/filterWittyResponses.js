let FilterWittyResponses = function (utilityFunctions) {
    let chosenCategories = [];

    let init = function (container) {
        let categoryLinks = container.querySelectorAll(".dropdown-menu a");
        utilityFunctions.addEvents(categoryLinks, "click", filterCategories);

        let allCatCheckbox = container.querySelector("[data-category='all']");
        allCatCheckbox.click();

        let applyFilterButton = container.getElementsByClassName("apply-filter");
        utilityFunctions.addEvents(applyFilterButton, "click", applyFilter);
    }

    let filterCategories = function (event) {
        event.stopPropagation();

        let category = this.dataset.category;
        let checkbox = this.querySelector("input");

        if (chosenCategories.indexOf(category) === -1) {
            if (category === "all") {
                addAllCategoriesToChosenCategories(this);
                tickAllCheckboxes();
            } else {
                chosenCategories.push(category);
                tickCheckbox(checkbox);
            }
        }
        else {
            if (category === "all") {
                chosenCategories = [];
                untickAllCheckboxes();
            } else {
                removeCategoryFromChosenCategories(category);
                untickCheckbox(checkbox);
                untickAllCategoryCheckbox();
            }
        }
    }

    let applyFilter = function () {
        let responseCards = document.querySelectorAll(".response-card-group");

        for (let i = 0; i < responseCards.length; i++) {
            let responseCard = responseCards[i];
            let index = chosenCategories.indexOf(responseCard.dataset.category);

            if (index === -1) {
                utilityFunctions.hideElement(responseCard);
            } else {
                utilityFunctions.showElement(responseCard);
            }

        }
    }

    let addAllCategoriesToChosenCategories = function (allCategoryCheckbox) {
        chosenCategories = [];
        chosenCategories = chosenCategories.concat(allCategories(allCategoryCheckbox));
    }

    let allCategories = function (allCategoryCheckbox) {
        return allCategoryCheckbox.dataset.allCategories.split(',');
    }

    let tickAllCheckboxes = function () {
        let allCheckboxes = getAllCheckboxes();

        for (let i = 0; i < allCheckboxes.length; i++) {
            tickCheckbox(allCheckboxes[i]);
        }
    }

    let untickAllCheckboxes = function () {
        let allCheckboxes = getAllCheckboxes();

        for (let i = 0; i < allCheckboxes.length; i++) {
            untickCheckbox(allCheckboxes[i]);
        }
    }

    let getAllCheckboxes = function () {
        return document.querySelectorAll(".dropdown-menu .dropdown-item input");
    }

    let untickAllCategoryCheckbox = function () {
        let index = chosenCategories.indexOf("all");

        if (index !== -1) {
            let cb = document.querySelector(".dropdown-menu [data-category='all'] input");
            untickCheckbox(cb);
            removeCategoryFromChosenCategories("all");
        }
    }

    let removeCategoryFromChosenCategories = function (category) {
        let index = chosenCategories.indexOf(category);

        chosenCategories.splice(index, 1);
    }

    let tickCheckbox = function (checkbox) {
        setTimeout(function () { checkbox.checked = true }, 0);
    }

    let untickCheckbox = function (checkbox) {
        setTimeout(function () { checkbox.checked = false }, 0);
    }

    return {
        init: init
    };
}(UtilityFunctions);