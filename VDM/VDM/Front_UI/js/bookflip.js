/****************************************************************************
//** Software License Agreement (BSD License)
//** Book Flip slideshow script- Copyright 2011, Will Jones (coastworx.com)
//** This Script is wholly developed and owned by CoastWorx.com
//** Copywrite: http://www.coastworx.com/
//** You are free to use this script so long as this coptwrite notices
//** and link back to http://www.coastworx.com stays intact in its entirety.
//** If you want to remove the link back to http://www.coastworx.com then purchase a licence.
//** You are NOT Permitted to claim this script as your own or
//** use this script for commercial purposes without the express
//** permission of CoastWorx Technologies!
//***************************************************************************/


//globals advanced------------------->
var myBook,p1,p2,p3,p4,p5,p6,p7,p8,p9,
p10,p11,p12,p13,p14,p15,p16,p17,p18,p19,
p20,p21,p22,p23,p24,p25,p26,p27,p28,p29,
p30,p31,p32,p33,p34,p35,p36,p37,p38,p39,
p40,p41,p42,p43,p44,p45,p46,p47,p48,p49,
p50,p51,p52,p53,p54,p55,p56,p57,p58,p59,
p60,p61,p62,p63,p64,p65,p66,p67,p68,p69,
p70,p71,p72,p73,p74,p75,p76,p77,p78,p79,
p80,p81,p82,p83,p84,p85,p86,p87,p88,p89,
p90,p91,p92,p93,p94,p95,p96,p97,p99,p100;
var pageBorderColor="#000000",pageBorderStyle="solid",pageBorderWidth="1";
var leftCoverPageBorder=false,leftCoverPageBorderColor="#ffffff";
var startingPage=0;
var pageBackgroundColor="#CCCCCC",pageFontColor="#ffffff";
var isMoving=false;
var nextPage=0;

var ID,numPixels=20;
var pWidth=300;
var pHeight=361;
var pSpeed=15;
var counter=0;
var allowPageClick=true;
var allowNavigation=true,allowAutoflipFromUrl = false;
var pageShadow=0,pageShadowWidth=80,pageShadowOpacity=60;
var pageShadowLeftImgUrl  ="black_gradient.png";
var pageShadowRightImgUrl ="black_gradientr.png";
var pageNumberPrefix="page ";
var pBorders;
var pages=new Array;

function ini(){
	pBorders=parseInt(pageBorderWidth)*2;
	var pagesDiv=document.getElementById("pages");
	j=0;
	for(var i=0;i<pagesDiv.childNodes.length;i++){
		if(pagesDiv.childNodes[i].nodeType == 1){ //element nodes only

			var newElement = pagesDiv.childNodes[i].cloneNode(true);
			newElement.style.width=pWidth+"px";
			newElement.style.height=pHeight+"px";

			document.getElementById("bookflip").appendChild(newElement);

			if(pageShadow){createShadow(newElement,j);}; //shadow function
			if(j>0){createList(newElement,j);}; //create dropdown navigation list


			pages[j] = document.getElementById("bookflip").innerHTML;
			//alert(pages[j]);
			document.getElementById("bookflip").removeChild(newElement);
			j++;
		}
	}
	if(myBook=document.getElementById("bookflip")){

		myBook.style.width=((pWidth*2)+(pBorders*2))+'px';
		myBook.style.height=(pHeight+pBorders)+'px';
		myBook.style.position="relative";
		myBook.style.zIndex="0";
		myBook.style.overflow="hidden";

		for(i=4;i>=1;i--){
			var myDiv = document.createElement('div');
			myDiv.style.position="absolute";
			myDiv.style.left=(pWidth)+'px';
			myDiv.style.top="0px";
			myDiv.style.width=(pWidth)+"px";

			myDiv.style.height=(pHeight)+"px";
			myDiv.style.margin="0px";
			myDiv.style.overflow="hidden";
			myDiv.style.backgroundColor=pageBackgroundColor;
			myDiv.style.color=pageFontColor;

			myDiv.style.borderWidth="0" + "px";
			myDiv.style.borderColor=pageBorderColor;
			myDiv.style.borderStyle=pageBorderStyle;

			myDiv.setAttribute("id","p"+i);

			myBook.appendChild(myDiv);
			document.getElementById("p"+i).innerHTML=pages[i-1];

			//turn page by direct click
			if(allowPageClick){
				if(number_check(i)){
					myDiv.onclick=function(e){whichElement(e,true);};
				}else{
					myDiv.onclick=function(e){whichElement(e,false);};
				}
			}
			//end page click
		}

			p1=document.getElementById("p1");
			p2=document.getElementById("p2");
			p1.style.left=0+"px";
			startingPageUrl=0;

			if(allowAutoflipFromUrl){startingPageUrl=autoflipFromUrl();};	//check url or variable for starting page
			if(startingPageUrl){startingPage=startingPageUrl;};
			if(startingPage=="e"){startingPage=pages.length-2;};
			startingPage=parseInt(startingPage);
			if(startingPage>0){autoFlip(startingPage);};//autoflip on book start up
	}
}

function rotateDivs(){
	if(counter>0){
		p1=document.getElementById("p1");
		p2=document.getElementById("p2");
		p3=document.getElementById("p3");
		p4=document.getElementById("p4");
		p5=document.getElementById("p5");
		p6=document.getElementById("p6");
		p7=document.getElementById("p7");
		p8=document.getElementById("p8");
		p9=document.getElementById("p9");
		p10=document.getElementById("p10");
		p11=document.getElementById("p11");
		p12=document.getElementById("p12");
		p13=document.getElementById("p13");
		p14=document.getElementById("p14");
		p15=document.getElementById("p15");
		p16=document.getElementById("p16");
		p17=document.getElementById("p17");
		p18=document.getElementById("p18");
		p19=document.getElementById("p19");
		p20=document.getElementById("p20");
		p21=document.getElementById("p21");
		p22=document.getElementById("p22");
		p23=document.getElementById("p23");
		p24=document.getElementById("p24");
		p25=document.getElementById("p25");
		p26=document.getElementById("p26");
		p27=document.getElementById("p27");
		p28=document.getElementById("p28");
		p29=document.getElementById("p29");
		p30=document.getElementById("p30");
		p31=document.getElementById("p31");
		p32=document.getElementById("p32");
		p33=document.getElementById("p33");
		p34=document.getElementById("p34");
		p35=document.getElementById("p35");
		p36=document.getElementById("p36");
		p37=document.getElementById("p37");
		p38=document.getElementById("p38");
		p39=document.getElementById("p39");
		p40=document.getElementById("p40");
		p41=document.getElementById("p41");
		p42=document.getElementById("p42");
		p43=document.getElementById("p43");
		p44=document.getElementById("p44");
		p45=document.getElementById("p45");
		p46=document.getElementById("p46");
		p47=document.getElementById("p47");
		p48=document.getElementById("p48");
		p49=document.getElementById("p49");
		p50=document.getElementById("p50");
		p51=document.getElementById("p51");
		p52=document.getElementById("p52");
		p53=document.getElementById("p53");
		p54=document.getElementById("p54");
		p55=document.getElementById("p55");
		p56=document.getElementById("p56");
		p57=document.getElementById("p57");
		p58=document.getElementById("p58");
		p59=document.getElementById("p59");
		p60=document.getElementById("p60");
		p61=document.getElementById("p61");
		p62=document.getElementById("p62");
		p63=document.getElementById("p63");
		p64=document.getElementById("p64");
		p65=document.getElementById("p65");
		p66=document.getElementById("p66");
		p67=document.getElementById("p67");
		p68=document.getElementById("p68");
		p69=document.getElementById("p69");
		p70=document.getElementById("p70");
		p71=document.getElementById("p71");
		p72=document.getElementById("p72");
		p73=document.getElementById("p73");
		p74=document.getElementById("p74");
		p75=document.getElementById("p75");
		p76=document.getElementById("p76");
		p77=document.getElementById("p77");
		p78=document.getElementById("p78");
		p79=document.getElementById("p79");
		p80=document.getElementById("p80");
		p81=document.getElementById("p81");
		p82=document.getElementById("p82");
		p83=document.getElementById("p83");
		p84=document.getElementById("p84");
		p85=document.getElementById("p85");
		p86=document.getElementById("p86");
		p87=document.getElementById("p87");
		p88=document.getElementById("p88");
		p89=document.getElementById("p89");
		p90=document.getElementById("p90");
		p91=document.getElementById("p91");
		p92=document.getElementById("p92");
		p93=document.getElementById("p93");
		p94=document.getElementById("p94");
		p95=document.getElementById("p95");
		p96=document.getElementById("p96");
		p97=document.getElementById("p97");
		p98=document.getElementById("p98");
		p99=document.getElementById("p99");
		p100=document.getElementById("p100");

		counter=0;

	}else{
		p1=document.getElementById("p51");
		p2=document.getElementById("p52");
		p3=document.getElementById("p53");
		p4=document.getElementById("p54");
		p5=document.getElementById("p55");
		p6=document.getElementById("p56");
		p7=document.getElementById("p57");
		p8=document.getElementById("p58");
		p9=document.getElementById("p59");
		p10=document.getElementById("p60");
		p11=document.getElementById("p61");
		p12=document.getElementById("p62");
		p13=document.getElementById("p63");
		p14=document.getElementById("p64");
		p15=document.getElementById("p65");
		p16=document.getElementById("p66");
		p17=document.getElementById("p67");
		p18=document.getElementById("p68");
		p19=document.getElementById("p69");
		p20=document.getElementById("p70");
		p21=document.getElementById("p71");
		p22=document.getElementById("p72");
		p23=document.getElementById("p73");
		p24=document.getElementById("p74");
		p25=document.getElementById("p75");
		p26=document.getElementById("p76");
		p27=document.getElementById("p77");
		p28=document.getElementById("p78");
		p29=document.getElementById("p79");
		p30=document.getElementById("p80");
		p31=document.getElementById("p81");
		p32=document.getElementById("p82");
		p33=document.getElementById("p83");
		p34=document.getElementById("p84");
		p35=document.getElementById("p85");
		p36=document.getElementById("p86");
		p37=document.getElementById("p87");
		p38=document.getElementById("p88");
		p39=document.getElementById("p89");
		p40=document.getElementById("p90");
		p41=document.getElementById("p91");
		p42=document.getElementById("p92");
		p43=document.getElementById("p93");
		p44=document.getElementById("p94");
		p45=document.getElementById("p95");
		p46=document.getElementById("p96");
		p47=document.getElementById("p97");
		p48=document.getElementById("p98");
		p49=document.getElementById("p99");
		p50=document.getElementById("p100");
		p51=document.getElementById("p1");
		p52=document.getElementById("p2");
		p53=document.getElementById("p3");
		p54=document.getElementById("p4");
		p55=document.getElementById("p5");
		p56=document.getElementById("p6");
		p57=document.getElementById("p7");
		p58=document.getElementById("p8");
		p59=document.getElementById("p9");
		p60=document.getElementById("p10");
		p61=document.getElementById("p11");
		p62=document.getElementById("p12");
		p63=document.getElementById("p13");
		p64=document.getElementById("p14");
		p65=document.getElementById("p15");
		p66=document.getElementById("p16");
		p67=document.getElementById("p17");
		p68=document.getElementById("p18");
		p69=document.getElementById("p19");
		p70=document.getElementById("p20");
		p71=document.getElementById("p21");
		p72=document.getElementById("p22");
		p73=document.getElementById("p23");
		p74=document.getElementById("p24");
		p75=document.getElementById("p25");
		p76=document.getElementById("p26");
		p77=document.getElementById("p27");
		p78=document.getElementById("p28");
		p79=document.getElementById("p29");
		p80=document.getElementById("p30");
		p81=document.getElementById("p31");
		p82=document.getElementById("p32");
		p83=document.getElementById("p33");
		p84=document.getElementById("p34");
		p85=document.getElementById("p35");
		p86=document.getElementById("p36");
		p87=document.getElementById("p37");
		p88=document.getElementById("p38");
		p89=document.getElementById("p39");
		p90=document.getElementById("p40");
		p91=document.getElementById("p41");
		p92=document.getElementById("p42");
		p93=document.getElementById("p43");
		p94=document.getElementById("p44");
		p95=document.getElementById("p45");
		p96=document.getElementById("p46");
		p97=document.getElementById("p47");
		p98=document.getElementById("p48");
		p99=document.getElementById("p49");
		p100=document.getElementById("p50");
		counter=1;
	}
}

function clipmeR(){
	if(isMoving || nextPage<2){return;};
	isMoving=true;

	rotateDivs();

	p1.innerHTML=pages[nextPage-2];
	p2.innerHTML=pages[nextPage-1];

	nextPage=nextPage-2;
	p1.style.clip="rect(0px,"+0+"px,"+(pHeight+pBorders)+"px,"+0+"px)";//rect(top,right,bottom,left)
	p1.style.zIndex=3;
	p3.style.zIndex=1;
	p2.style.clip="rect(0px,"+0+"px,"+(pHeight+pBorders)+"px,"+0+"px)";//rect(top,right,bottom,left)
	p2.style.zIndex=4;
	p4.style.zIndex=2;

	selectIndex = nextPage-1;
	if(selectIndex<0){selectIndex=0;};
	if(allowNavigation && document.getElementById("flipSelect")){
		document.getElementById("flipSelect").selectedIndex=selectIndex;
	}

	goleft(-pWidth,pWidth);
}
function goleft(currLeft,currWidth){
	if(currLeft >=(pWidth-(numPixels*2))){
		window.clearTimeout(ID);
		p2.style.left=(pWidth+pBorders)+"px";
		p1.style.clip=p2.style.clip="rect(0px,"+(pWidth+pBorders)+"px,"+(pHeight+pBorders)+"px,"+0+"px)";//rect(top,right,bottom,left)
		isMoving=false;
		return;
	}
	currLeft=currLeft+(numPixels*2);//2 * width reveal
	currWidth = currWidth-numPixels;
	hideWidth=pWidth-currWidth;
	p1.style.clip="rect(0px,"+hideWidth+"px,"+(pHeight+pBorders)+"px,"+0+"px)";//rect(top,right,bottom,left) //low to high

	p2.style.clip="rect(0px,"+(pWidth+pBorders)+"px,"+(pHeight+pBorders)+"px,"+currWidth+"px)";//rect(top,right,bottom,left)
	p2.style.left=currLeft+'px';

	ID = window.setTimeout('goleft('+currLeft+','+currWidth+')',pSpeed);
}
function clipmeL(){
	if(isMoving || (nextPage+4) > pages.length){return;};

	isMoving=true;
	rotateDivs();
	//p1.style.borderColor=pageBorderColor; //reset left cover border color

	p1.innerHTML=pages[nextPage+2];
	p2.innerHTML=pages[nextPage+3];
	nextPage=nextPage+2;

	p1.style.clip="rect(0px,"+0+"px,"+(pHeight+pBorders)+"px,"+0+"px)";//rect(top,right,bottom,left) clip
	p1.style.zIndex=5;
	p2.style.clip="rect(0px,"+0+"px,"+(pHeight+pBorders)+"px,"+0+"px)";//rect(top,right,bottom,left) clip

	p2.style.zIndex=4;
	p3.style.zIndex=2;
	p4.style.zIndex=1;

	selectIndex = nextPage-1;
	if(selectIndex<0){selectIndex=0;};
	if(allowNavigation && document.getElementById("flipSelect")){
		document.getElementById("flipSelect").selectedIndex=selectIndex;
	}

	goright(pWidth*2,0);
}
function goright(currLeft,currWidth){
	if(currLeft <= numPixels*2){
		//alert("");

		window.clearTimeout(ID);
		p1.style.clip=p2.style.clip="rect(0px,"+(pWidth+pBorders)+"px,"+(pHeight+pBorders)+"px,"+0+"px)";//rect(top,right,bottom,left)
		p1.style.left=0+'px';
		p2.style.left=(pWidth)+'px';

		isMoving=false;
		return;
	}
	currLeft=currLeft-(numPixels*2);//2 * width reveal
	currWidth=currWidth+numPixels;
	hideWidth = currLeft-pWidth;

	p1.style.clip="rect(0px,"+currWidth+"px,"+(pHeight+pBorders)+"px,"+0+"px)";//rect(top,right,bottom,left) clip
	p1.style.left=currLeft+'px';

	p2.style.clip="rect(0px,"+(pWidth+pBorders)+"px,"+(pHeight+pBorders)+"px,"+hideWidth+"px)";//rect(top,right,bottom,left)
	ID = window.setTimeout('goright('+currLeft+','+currWidth+')',pSpeed);
}


function autoFlip(pgNumber){
	pgNumber=parseInt(pgNumber);
	chcknextPage=nextPage;

	if(!number_check(pgNumber)){
		nextPage=pgNumber-1;
	}else{
		nextPage=pgNumber-2;
	}
	if(pgNumber>chcknextPage){
		clipmeL();
	}else{
		nextPage=nextPage+4;
		clipmeR();
	}
}

//-->worker functions
function createList(dv,num){
	if( allowNavigation && document.getElementById("flipSelect") ){
	num=num-1;
		var y=document.createElement('option');
		if(num==0){
			if(dv.getAttribute("name")){
				y.text=dv.getAttribute("name");
			}else{
				y.text="Cover ";
			}
		}else{
			if(dv.getAttribute("name")){
				y.text=dv.getAttribute("name");
			}else{
				y.text = pageNumberPrefix + num;
			}
		}
		y.value=num;
		//y.selected="selected";
			var x=document.getElementById("flipSelect");
		x.style.display="";
		x.onchange=function() {this.blur();document.body.focus();autoFlip(this.value);};
		try{
			x.add(y,null); // standards compliant
		}catch(ex){
			x.add(y); // IE only
		}
	}
}

function createShadow(dv,pg){
		//alert(pg);
	if(pg < 1){return};//no shadow page 0
	if(number_check(pg)){

		var myPngDiv = document.createElement('div');
		myPngDiv.style.position="absolute";
		myPngDiv.style.top="0px";
		myPngDiv.style.left=(pWidth-pageShadowWidth)+'px';

		myPngDiv.style.width=(pageShadowWidth)+"px";
		myPngDiv.style.height=pHeight+"px";
		myPngDiv.style.backgroundColor="#000000";

		var ie6 = (/MSIE ((5\.5)|6)/.test(navigator.userAgent) && navigator.platform == "Win32");

		if( ie6 ) {
			myPngDiv.style.filter="progid:DXImageTransform.Microsoft.Alpha(Opacity="+pageShadowOpacity+", FinishOpacity=0, Style=1, StartX="+pageShadowWidth+", FinishX=0, StartY=0, FinishY=0)";
		}else{
			myPngDiv.style.background="transparent url("+pageShadowLeftImgUrl+") top right repeat-y";
		}
		dv.appendChild(myPngDiv);
	}
}

function whichElement(e,dir){
	var targ;
	if (!e){
		var e = window.event;
	}

	if (e.target){
		targ = e.target;
	}else if (e.srcElement) {
		targ = e.srcElement;
	}

	if (targ.nodeType == 3){ // defeat Safari bug
		targ = targ.parentNode;
	}

	//var tname;
	pname=targ.parentNode.tagName;

	tname=targ.tagName;
	if(tname=="A" || tname=="INPUT" || pname=="A" || pname=="INPUT" ){
		//alert("You clicked on a " + tname + " element. pname:"+pname);
		return false;
	}else{
//		alert("You clicked on a " + tname + " element. pname:"+pname);
		if(dir){
			return clipmeL();
		}else{
			return clipmeR();
		}
	}
}

function number_check(value) {
  //  returns true if value is even, false if value is odd
      return ( 1 - (value%2) )
}

//get querystring to obtain autoflip value
function autoflipFromUrl(){
	var pos;
	var searchStr = window.location.search;
	var searchArray = searchStr.substring(1,searchStr.length).split('&');
	var result = '';
	for (var i=0; i<searchArray.length; i++) {
		result = searchArray[i].split('=');
		if(result[0]=="autoflip"){
			result=result[1];
			break;
		}
	}
	return(result);
}
