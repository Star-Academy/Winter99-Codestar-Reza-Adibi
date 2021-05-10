const serverUri = "https://localhost:44307/api/search";

function goHome() {
    window.location.href = "index.html";
}

function sendRequest() {
    const id = document.getElementById("id").value;
    const text = document.getElementById("text").value;
    const xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                alert("Success");
            } else {
                alert(this.statusText);
            }
        }
    };
    xhttp.open("POST", serverUri);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.send(JSON.stringify({ id: id, text: text }));
}