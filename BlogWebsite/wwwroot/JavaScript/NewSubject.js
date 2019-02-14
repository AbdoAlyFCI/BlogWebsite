var pNum;
var texts;
window.onload = function () {
    var formDiv = document.getElementById("textArea");
    var option = document.createElement("p");
    option.innerText = "Hello";
    formDiv.appendChild(option);
    //console.log();
    pNum = document.getElementById("textArea").childNodes[0];
    console.log(pNum.childNodes[0].length);
    texts = pNum.childNodes[0];   
}
document.getElementById("textArea").onkeydown = function (e) {

    if (e.keyCode == 8 && e.target == document.getElementById("textArea")) {
        
        if (texts.length === 0) {
            window.alert("hmmm");
            e.preventDefault();
        }
        else {}
    }     
}














function replaceSelectedText(replacementText) {
    window.alert("SELECR");
    var sel, range;
    if (window.getSelection) {
        sel = window.getSelection();
        var texts = window.getSelection().toString();

        if (sel.rangeCount) {
            range = sel.getRangeAt(0);
            range.deleteContents();
            
            var option = document.createElement("strong");
            alert(texts);
            option.innerText = texts;
            option.contenteditable = true;
            range.insertNode(option);
            //range.appendChild(document.getElementById("textArea").appendChild(option));
        }
    } else if (document.selection && document.selection.createRange) {
        range = document.selection.createRange();
        range.text = "Hed";
    }
}

//document.addEventListener("keydown", KeyCheck);  //or however you are calling your method
//function KeyCheck(event) {
//    var KeyID = event.keyCode;
//    switch (KeyID) {
//        case 8:
//            if (texts.length === 0) {
//                KeyID.preventDefault();
//            }
//            break;

//        default:
//            break;
//    }
//}
