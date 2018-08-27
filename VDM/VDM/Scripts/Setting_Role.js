
$(document).ready(function () {
    initDataTable();
});

$(window).resize(function () {
    resizeScreen();    
});

function resizeScreen() {
    if ($(window).width() < 760) {
        var newheight = ($(window).height() - 350);
        $('.dataTables_scrollBody').css('max-height', newheight + 'px');
        $('.fixedHeight').height(200);        
    }
    else {
        var newheight = ($(window).height() - 250);
        $('.dataTables_scrollBody').css('max-height', newheight + 'px');
        $('.fixedHeight').height(140);       
    }
    var newtop = ($(window).height() - $('.fixedHeight').height() - 50) / 2;
    $('.fixedHeight').css('top', newtop + 'px');
    $('.fixedHeight').focus();

} resizeScreen();

function initDataTable()
{
    $("body").height();
    var LDAPTable = $('.datatable').DataTable({
        "scrollY": ($("body").height()-250) + "px",
        "bAutoWidth": false,
        "scrollCollapse": true,
        "paging": false,
        "oLanguage": {
            "sSearch": "",
            "sSearchPlaceholder": "Search LDAP User",
            "sZeroRecords": "No User found."            
        }
        
    });
    $('.dataTables_filter label input').focus();
    //var keyWord = 'A'; //Change Default Keyword Here
    //$('.dataTables_filter label input').val(keyWord);
    //LDAPTable.search(keyWord).draw();
}