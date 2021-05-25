const serverUri = "https://localhost:44307/api/search";

function getQuerystring() {
    const url = decodeURIComponent(location.href.replaceAll("+", " "));
    return queryString = url.slice(location.href.indexOf("?") + 1);
}

function checkQueryString() {
    const queryString = getQuerystring();
    if (queryString.indexOf("query=") === -1 || queryString === "query=") {
        window.location.href = "index.html";
    }
    else {
        let f = queryString.indexOf("=") + 1;
        let t = queryString.slice(f);
        document.getElementById("search_query").value = t;
    }
}

function getResultsArray(inputJsonString) {
    return JSON.parse(inputJsonString);
}

function showSearchResults(results) {
    const resultObjects = getResultsArray(results);
    for (let index = 0; index < resultObjects.length; index++) {
        document.getElementsByClassName("result_list")[0].innerHTML +=
            '<div class="result_card" onclick="showSelectedResult(this)">' +
            ' <div class="id_container">' +
            "   <p>" +
            resultObjects[index].id +
            "   </p>" +
            " </div>" +
            " <hr />" +
            ' <div class="text_container">' +
            "   <p>" +
            resultObjects[index].text +
            "   </p>" +
            " </div>" +
            "</div>";
    }
}

function getSearchResult() {
    const queryString = getQuerystring();
    const xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                showSearchResults(this.responseText);
            }
            else {
                alert(this.statusText);
            }
        }
    };
    xhttp.open("GET", serverUri + "?" + queryString);
    xhttp.send();
}

function showSelectedResult(element) {
    const showElement = document.getElementsByClassName("result_show")[0];
    const idContainer = showElement.getElementsByClassName("id_container")[0];
    const textContainer = showElement.getElementsByClassName(
        "text_container"
    )[0];
    idContainer.innerHTML = element.getElementsByClassName(
        "id_container"
    )[0].innerHTML;
    textContainer.innerHTML = element.getElementsByClassName(
        "text_container"
    )[0].innerHTML;
    showElement.style.display = "flex";
    setTimeout(function () {
        showElement.style.opacity = "100";
    }, 0);
}

function hideElement(event) {
    const element = event.target;
    if (element.className === "result_show") {
        element.style.opacity = "0";
        setTimeout(function () {
            element.style.display = "none";
        }, 400);
    }
}