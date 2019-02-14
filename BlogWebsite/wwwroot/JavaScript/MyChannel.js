function onNewThread() {
    document.getElementById("overlay").style.display = "block";
    document.documentElement.style.overflow = 'hidden';
}

function onNewHeader() {
    document.getElementById("ChangeHedar").style.display = "block";
    document.documentElement.style.overflow = 'hidden';
}
//function offNewThread() {
//    document.getElementById("overlay").style.display = "none";
//}

function onNewFolder() {
    document.getElementById("NewFolder").style.display = "block";
}

function offNewFolder() {
    document.getElementById("NewFolder").style.display = "none";
}

function onClick(Parent) {
    console.log(Parent.childNodes);
    if (Parent.childNodes[3].style.display === 'none') {
        Parent.childNodes[1].className = ('fas fa-folder-open');
        Parent.childNodes[3].style.display = "block";
    }
    else {        
        Parent.childNodes[1].className = ('fas fa-folder');
        Parent.childNodes[3].style.display = "none";    
    }
}

function OnClose(e) {
    document.getElementById(e).style.display = "none";
    document.documentElement.style.overflow = 'auto';
}

