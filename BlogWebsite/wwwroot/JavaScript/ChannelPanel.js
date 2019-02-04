var GenralinfoBtn = document.getElementById("GenralinfoBtn");
var NavbarBtn = document.getElementById("NavbarBtn");
var DraftsBtn = document.getElementById("DraftsBtn");
var BlockusersBtn = document.getElementById("BlockusersBtn");
var AboutBtn = document.getElementById("AboutBtn");




function ClearResult() {
    document.getElementById("GenralTab").style.display = "none";
    document.getElementById("Navbar").style.display = "none";
    document.getElementById("Drafts").style.display = "none";
    document.getElementById("BlockedUser").style.display = "none";
    document.getElementById("About").style.display = "none";
}
console.log(GenralinfoBtn);
GenralinfoBtn.onclick = function () {
  
    ClearResult();
    document.getElementById("GenralTab").style.display = "Block";

};

document.getElementById("NavbarBtn").onclick = function () {
    ClearResult();
    document.getElementById("Navbar").style.display = "Block";
};

DraftsBtn.onclick = function () {
    ClearResult();
    document.getElementById("Drafts").style.display = "Block";

};
    
BlockusersBtn.onclick = function () {
    ClearResult();
    document.getElementById("BlockedUser").style.display = "Block";

};

AboutBtn.onclick = function () {
    ClearResult();
    document.getElementById("About").style.display = "Block";
};

