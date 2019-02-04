var PreviewBtn = document.getElementById("PreviewBtn");
PreviewBtn.onclick = function () {
    document.getElementById("PreviewSection").style.display = "Block";
    console.log(document.getElementById("ThreadName").innerText)
    document.getElementById("PreviewTitle").innerHTML = document.getElementById("ThreadName").innerText;
    document.getElementById("PreviewText").innerHTML = document.getElementById("editor1").innerText;
}