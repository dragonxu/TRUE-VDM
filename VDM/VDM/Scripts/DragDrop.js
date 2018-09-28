
function dragged(ev) {
    ev.dataTransfer.setData("text", ev.target.id); // ส่ง Object ID   
    
}



function dropped(ev) {
    ev.preventDefault();

    // Access nearest target
    var targetObj = ev.target;
    while (!targetObj.hasAttribute("dropdatatype")) {
        targetObj = targetObj.parentElement;        
    }

    var sourceObj =document.getElementById(ev.dataTransfer.getData("text"));

    var dropbtn = document.getElementById(DropActionButton);
    var dragTypeTxt = document.getElementById(DragTypeTextbox);
    var dragArgTxt = document.getElementById(DragArgTextbox);
    var dropTypeTxt = document.getElementById(DropTypeTextbox);
    var dropArgTxt = document.getElementById(DropArgTextbox);

    dragTypeTxt.value = "";
    dragArgTxt.value = "";
    dropTypeTxt.value = "";
    dropArgTxt.value = "";
        
    if (!dropbtn) { return; } // ถ้าไม่มีปุ่มรับก็ออก

    if (dragTypeTxt) {
        if (sourceObj.getAttribute("dragdatatype"))
            dragTypeTxt.value = sourceObj.getAttribute("dragdatatype"); //ข้อมูลที่ส่งมาด้วย
    }
    if (dragArgTxt) {
        if (sourceObj.getAttribute("dragdataarg"))
            dragArgTxt.value = sourceObj.getAttribute("dragdataarg"); //ข้อมูลที่ส่งมาด้วย
    }
    if (dropTypeTxt) {
        if (targetObj.getAttribute("dropdatatype"))
            dropTypeTxt.value = targetObj.getAttribute("dropdatatype"); //ข้อมูลที่ Object ปลางทาง
    }
    if (dropArgTxt) {
        if (targetObj.getAttribute("dropdataArg"))
            dropArgTxt.value = targetObj.getAttribute("dropdataArg"); //ข้อมูลที่ Object ปลางทาง
    }
    dropbtn.click();
}

function draggedOver(ev) {
    ev.preventDefault();
    ev.dataTransfer.dropEffect = "move"
}

var DropActionButton;
var DragTypeTextbox;
var DragArgTextbox;
var DropTypeTextbox;
var DropArgTextbox;