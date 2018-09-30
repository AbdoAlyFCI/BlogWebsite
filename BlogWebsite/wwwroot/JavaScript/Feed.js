//var esdd = document.getElementsByClassName("SChannel")[0].childNodes[1];
//esdd.style.height = esdd.style.width;
//console.log(esdd.style.width);

function iconOver(e) { 
    e.childNodes[1].hidden = true;
    e.childNodes[3].hidden = false; 
}

function iconOut (e) {
    e.childNodes[1].hidden = false;
    e.childNodes[3].hidden = true;
}
var listIcon = document.getElementById("list");
var blockIcon = document.getElementById("block");

listIcon.onclick = function () {
    listIcon.style.color = "#ffffff";
    blockIcon.style.color = "#212529";
    var element = document.getElementsByClassName("BV");
    while (element.length) {
        var temp = element[0];
        temp.className = "col-12 LV";
        temp.childNodes[1].className = "New";
        temp.childNodes[1].childNodes[1].className = "float-left image";
        temp.childNodes[1].childNodes[3].className = "float-left Discription";
        temp.childNodes[1].childNodes[3].childNodes[3].hidden = false;
        temp.childNodes[1].childNodes[5].className = "float-left react";
    }
};



blockIcon.onclick = function () {
    listIcon.style.color = "#212529";
    blockIcon.style.color = "#ffffff";
    var element = document.getElementsByClassName("LV");
    while (element.length) {
        var temp = element[0];
        temp.className = "col-lg-4 col-md-6 col-12 BV";
        temp.childNodes[1].className = "NewBlock";
        temp.childNodes[1].childNodes[1].className = "imageBlock";
        temp.childNodes[1].childNodes[3].className = "DiscriptionBlock";
        temp.childNodes[1].childNodes[3].childNodes[3].hidden = true;
        temp.childNodes[1].childNodes[5].className = "reactBlock";
    } 

};




