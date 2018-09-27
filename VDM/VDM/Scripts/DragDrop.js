
function dragged(ev) {
    ev.dataTransfer.setData("text", ev.target.id); // ส่ง Object ID   
}

function dropped(ev) {
    ev.preventDefault();

    var sourceObj =document.getElementById(ev.dataTransfer.getData("text"));

    var dropbtn = document.getElementById(DropActionButton);
    var dragTypeTxt = document.getElementById(DragTypeTextbox);
    var dragArgTxt = document.getElementById(DragArgTextbox);
    var dropTypeTxt = document.getElementById(DropTypeTextbox);
    var dropArgTxt = document.getElementById(DropArgTextbox);
        
    if (!dropbtn) { return; } // ถ้าไม่มีปุ่มรับก็ออก

    if (dragTypeTxt) {
        dragTypeTxt.value = sourceObj.getAttribute("dragdatatype"); //ข้อมูลที่ส่งมาด้วย
    }
    if (dragArgTxt) {
        dragArgTxt.value = sourceObj.getAttribute("dragdataarg"); //ข้อมูลที่ส่งมาด้วย
    }
    if (dropTypeTxt) {
        dropTypeTxt.value = ev.target.getAttribute("dropdatatype"); //ข้อมูลที่ Object ปลางทาง
    }
    if (dropArgTxt) {
        dropArgTxt.value = ev.target.getAttribute("dropdataArg"); //ข้อมูลที่ Object ปลางทาง
    }
    dropbtn.click();
}

function allowDrop(ev) {
    ev.preventDefault();
}

var DropActionButton;
var DragTypeTextbox;
var DragArgTextbox;
var DropTypeTextbox;
var DropArgTextbox;