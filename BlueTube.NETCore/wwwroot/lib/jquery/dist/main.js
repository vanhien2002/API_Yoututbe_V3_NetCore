var lstMenu = document.getElementById('lstMenu')
var NavMenu = document.getElementById('nav_contentmenu_lst')
//
var bl_language = document.getElementById('choseLanguage')
const lstLangusge = document.getElementById('lstLanguge')
bl_language.addEventListener('click', function () {
    var _rptdisplay = window.getComputedStyle(lstLangusge).getPropertyValue('display');
    if (_rptdisplay == 'block') {
        //đóng
        lstLangusge.style.display = "none";
        //đóng overplay
        mainOverPlay.style.display = "none"
    }
    else {
        //mở
        lstLangusge.style.display = "block";
        //mở overplay
        mainOverPlay.style.display = "block"

    }
})
var btn_menu = document.getElementById('btn_menu')
const mainOverPlay = document.getElementById("overplayid");
mainOverPlay.addEventListener('click', function () {
    var _nav = document.getElementById('lstMenu')
    var nav = document.getElementById("lstMenu");
    var check = window.getComputedStyle(nav).getPropertyValue("display");    
    if (check == 'block') {
        //đóng nav menu
        _nav.classList.add('close_lstMenu');
        nav.style.display = "none"
        //đóng overplay 
        mainOverPlay.style.display = "none"
    }
    var _rptdisplay = window.getComputedStyle(lstLangusge).getPropertyValue('display');
    if (_rptdisplay == 'block') {
        //đóng language
        lstLangusge.style.display = "none";       
        //đóng overplay
        mainOverPlay.style.display = "none"
    }   
})
btn_menu.addEventListener('click', function () {
    var _oveplay = document.getElementById('overplayid');
    var _nav = document.getElementById('lstMenu')
    var nav = document.getElementById("lstMenu");
    var check = window.getComputedStyle(nav).getPropertyValue("display");
    if (check =='block') {
        //đóng
        _nav.classList.add('close_lstMenu');
        nav.style.display = "none"
        _oveplay.style.display = "none"
        
    }
    else {
        _nav.classList.remove('close_lstMenu');
        nav.style.display = "block"
        _oveplay.style.display = "block"
        //mở
    }
})