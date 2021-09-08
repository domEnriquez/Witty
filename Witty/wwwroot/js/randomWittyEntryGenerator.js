let RandomWittyEntryGenerator = function (wittyEntryService)
{
    let questionElem;
    let responseElem;


    let init = function () {
        questionElem = document.getElementById("random-question");
        responseElem = document.getElementById("random-response");
        randomUpdate();
    }

    let randomUpdate = function () {
        wittyEntryService.getRandomWittyEntry(initialDoneGetRandom, failGetRandom);

        setInterval(function () {
            wittyEntryService.getRandomWittyEntry(doneGetRandom, failGetRandom);
        }, 3000);
    }

    let initialDoneGetRandom = function (randEntry) {
        document.getElementById("random-section").classList.add("random-box");
        doneGetRandom(randEntry);
    }

    let doneGetRandom = function (randEntry) {
        questionElem.innerHTML = randEntry.question;
        responseElem.innerHTML = randEntry.response;
    }

    let failGetRandom = function () {
        alert("Something failed!");
    }

    return {
        init: init
    };

}(WittyEntryService);