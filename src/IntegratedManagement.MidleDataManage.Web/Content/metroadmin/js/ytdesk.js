function popReloadTipWindow(){
	$(function(){
		$("[id=newnotice3]").css({"right":"0px","bottom":"1px","width":"400px"});
		$("div[id=newnotice3]").slideDown("slow");
	}).scroll(function(){
		$("[id=newnotice3]").css({"bottom":"0px"});
		$("[id=newnotice3]").css({"right":"0px","bottom":"1px"});
	}).resize(function(){
		$("[id=newnotice3]").css({"bottom":""});
		$("[id=newnotice3]").css({"right":"0px","bottom":"1px"});    
	});
}

//检查MenubarUL是否过宽
function checkMenubarULWidth(param){
	var curli_w = 0;
	$('#menubarUL [class=menuli]').each(function(e){
		if($(this).css('display') != 'none'){
			curli_w += $(this).width();
		}
	});
	//alert(curli_w+'|||'+menubarUL_w3);
	var lw = menubarUL_w3 - curli_w;
	if(curli_w > menubarUL_w3){
		$('#menubarUL').find('li[class=menuli]:visible:last').css('display','none');
		setTimeout(function(){ checkMenubarULWidth(param); }, 300);
	}else if(lw > 70){
		if(param == 'right'){
			$('#menubarUL').find('li[class=menuli]:visible:last').next('li[class=menuli]').css('display','block');
			var nextli_w = $('#menubarUL').find('li[class=menuli]:visible:last').width();
			$('#menubarUL').find('li[class=menuli]:visible:last').css('display','none');
			if(nextli_w < lw){
				$('#menubarUL').find('li[class=menuli]:visible:last').next('li[class=menuli]').css('display','block');
				var a = lw - nextli_w;
				if(a > 70){
					setTimeout(function(){ checkMenubarULWidth(param); }, 300);
				}
			}		

		}else{
			$('#menubarUL').find('li[class=menuli]:visible:first').prev('li[class=menuli]').css('display','block');
			var prevli_w = $('#menubarUL').find('li[class=menuli]:visible:first').width();
			$('#menubarUL').find('li[class=menuli]:visible:first').css('display','none');
			if(prevli_w < lw){
				$('#menubarUL').find('li[class=menuli]:visible:first').prev('li[class=menuli]').css('display','block');
				var a = lw - prevli_w;
				if(a > 70){
					setTimeout(function(){ checkMenubarULWidth(param); }, 300);
				}
			}
		}
	}
}

function menubarRight(){
	$('#menubarUL').find('li[class=menuli]:visible:first').css('display','none');
	$('#menubarUL').find('li[class=menuli]:visible:last').next('li[class=menuli]').css('display','block');
	checkMenubarULWidth('right');
	if($('#menubarUL').find('li[class=menuli]:visible:first').html() == $('#menubarUL').find('li[class=menuli]:first').html()){
		$('#menubar_left').html('<img src="img/working_platforms/left_btny.png"/>');		
	}else{
		$('#menubar_left').html('<img src="img/working_platforms/left_btn.png"  onclick="menubarLeft();" />');
	}
	
	if($('#menubarUL').find('li[class=menuli]:visible:last').html() == $('#menubarUL').find('li[class=menuli]:last').html()){
		$('#menubar_right').html('<img src="img/working_platforms/right_btny.png" />');
	}else{
		$('#menubar_right').html('<img src="img/working_platforms/right_btn.png"  onclick="menubarRight();" />');
	}
}

function menubarLeft(){
	$('#menubarUL').find('li[class=menuli]:visible:last').css('display','none');
	$('#menubarUL').find('li[class=menuli]:visible:first').prev('li[class=menuli]').css('display','block');
	checkMenubarULWidth('left');
	if($('#menubarUL').find('li[class=menuli]:visible:first').html() == $('#menubarUL').find('li[class=menuli]:first').html()){
		$('#menubar_left').html('<img src="img/working_platforms/left_btny.png"/>');		
	}else{
		$('#menubar_left').html('<img src="img/working_platforms/left_btn.png"  onclick="menubarLeft();" />');
	}
	
	if($('#menubarUL').find('li[class=menuli]:visible:last').html() == $('#menubarUL').find('li[class=menuli]:last').html()){
		$('#menubar_right').html('<img src="img/working_platforms/right_btny.png" />');
	}else{
		$('#menubar_right').html('<img src="img/working_platforms/right_btn.png"  onclick="menubarRight();" />');
	}
}

function tabLeftMove(){
	$('#tabUL').find('li:visible:last').css('display','none');
	$('#tabUL').find('li:visible:first').prev('li').css('display','block');
	

	if($('#tabUL').find('li:visible:first').html() == $('#tabUL').find('li:first').html()){
		$('#tab_left').html('<img src="img/working_platforms/left_btny.png"/>');		
	}else{
		$('#tab_left').html('<img src="img/working_platforms/left_btn.png"  onclick="tabLeftMove();" />');
	}
	
	if($('#tabUL').find('li:visible:last').html() == $('#tabUL').find('li:last').html()){
		$('#tab_right').html('<img src="img/working_platforms/right_btny.png" />');
	}else{
		$('#tab_right').html('<img src="img/working_platforms/right_btn.png"  onclick="tabRightMove();" />');
	}	
}

function tabRightMove(){
	$('#tabUL').find('li:visible:first').css('display','none');
	$('#tabUL').find('li:visible:last').next('li').css('display','block');
	
	if($('#tabUL').find('li:visible:first').html() == $('#tabUL').find('li:first').html()){
		$('#tab_left').html('<img src="img/working_platforms/left_btny.png"/>');		
	}else{
		$('#tab_left').html('<img src="img/working_platforms/left_btn.png"  onclick="tabLeftMove();" />');
	}
	
	if($('#tabUL').find('li:visible:last').html() == $('#tabUL').find('li:last').html()){
		$('#tab_right').html('<img src="img/working_platforms/right_btny.png" />');
	}else{
		$('#tab_right').html('<img src="img/working_platforms/right_btn.png"  onclick="tabRightMove();" />');
	}	
}

function tabAutoLeftMove(Id){
	tabLeftMove();
	if($('#'+Id+'ALi').css('display') == 'none'){
		setTimeout(function(){ tabAutoLeftMove(Id); }, 200);
	}else{
		setTimeout(function(){ showDiv(''+Id+''); }, 200);
	}
}

function tabAutoRightMove(Id){
	tabRightMove();
	if($('#'+Id+'ALi').css('display') == 'none'){
		setTimeout(function(){ tabAutoRightMove(Id); }, 200);
	}else{
		setTimeout(function(){ showDiv(''+Id+''); }, 200);
	}
}

//打开管理页面
function addTab(identity,url,name,closeflag,leftmenucategory){
	var max_w = document.getElementById('tabOfContent').offsetWidth;//总宽度
	var welcome_w = document.getElementById('welcomeDiv').offsetWidth;//欢迎语占用宽度

	var tab_w = document.getElementById('tabUL').offsetWidth;//标签已占用宽度
	var left_w = 0;//左移图标宽度
	if($('#tab_left').css('display') != 'none'){
		left_w = document.getElementById('tab_left').offsetWidth;
	}
	var right_w = 0;//右移图标宽度
	if($('#tab_right').css('display') != 'none'){
		right_w = document.getElementById('tab_right').offsetWidth;
	}
	if($.browser.msie){
		var imgw = 56;
	}else{
		var imgw = 36;
	}
	var available_w = max_w - welcome_w - tab_w - left_w - right_w - imgw - 20;//可用宽度
	
	var Id = 'gid'+identity;
	
	if($('#allopentabUL #allopentabULlist_'+Id).length < 1){
		$('<li id="allopentabULlist_'+Id+'" onclick="addTab(\''+identity+'\',\''+url+'\',\''+name+'\',\''+closeflag+'\');" ><a  href="javascript:void(null);">'+name+'</a></li>').prependTo("#allopentabUL");
	}
	
	if($('#'+Id+'ALi').length > 0){
		if($('#'+Id+'ALi').css('display') == 'none'){
			//先确定标签在 可见标签的前端还是后端
			var blockli = 'no';
			$('#tabUL  #'+Id+'ALi').prevAll('li').each(function(e){
				if($(this).css('display') != 'none'){
					blockli = 'yes';
				}
			});
			
			if(blockli == 'yes'){//可见标签在前端
				//右移
				tabAutoRightMove(Id);
			}else{
				//左移
				tabAutoLeftMove(Id);
			}
		}else{
			showDiv(''+Id+'');
			if(closeflag == 'ref'){
				var iframename = Id+'Frame';
				window.frames[''+iframename+''].location.reload();				
			}
		}

		if(url == 'custompages' && closeflag != 'virtualcustomerPreview'){
			//传事件
			var nameary = closeflag.split('-');
			var dbpopstr = 'source=virtualinterfaces&event=dbclick&vid='+nameary[0]+'&cid='+nameary[1];
			document.getElementById(Id+"Frame").contentWindow.sonAccept(""+dbpopstr+"");			
		}
		
		if(closeflag == 'virtualcustomerPreview'){
			alert(''+LabelAlreadyExistsShow+'');
		}
	}else{
		var basis_w = imgw + 140;
		if(available_w < basis_w && $('#tab_left').css('display') == 'none' && $('#tab_right').css('display') == 'none'){
			$('#tab_left').html('<img src="img/working_platforms/left_btn.png"  onclick="tabLeftMove();" />');
			$('#tab_left').css('display','block');
			$('#tab_right').html('<img src="img/working_platforms/right_btny.png" />');
			$('#tab_right').css('display','block');
			$('#tabUL').find('li:first').css('display','none');
		}else if(available_w < 140 && $('#tab_left').css('display') != 'none' && $('#tab_right').css('display') != 'none'){
			var total = $('#tabUL li').length;
			var visiblenum = $('#tabUL').find('li:visible').length;
			var gtnum = total - visiblenum;
			$('#tabUL li').css('display','none');
			$('#tabUL').find('li:gt('+gtnum+')').css('display','block');
			$('#tab_right').html('<img src="img/working_platforms/right_btny.png" />');
			$('#tab_left').html('<img src="img/working_platforms/left_btn.png"  onclick="tabLeftMove();" />');
		}
		
		$('#allTabDivSite div[name=tabDiv]').css("display","none");
		
		if(closeflag != 'no'){
			$('<li id="'+Id+'ALi" ><a  title="'+name+'" href="javascript:showDiv(\''+Id+'\');" id="'+Id+'Atag" name="atagNamem" alt="'+leftmenucategory+'">'+name+'</a><img id="'+Id+'Img" src="img/working_platforms/closex.gif" onMouseOver="$(this).attr(&quot;src&quot;,&quot;img/working_platforms/closex_on.gif&quot;);"  onMouseOut="$(this).attr(&quot;src&quot;,&quot;img/working_platforms/closex.gif&quot;);" onclick="closeTabA(&quot;'+Id+'&quot;);" ></img></li>').appendTo('#tabUL');
		}else{
			$('<li id="'+Id+'ALi" ><a  title="'+name+'" href="javascript:showDiv(\''+Id+'\');" id="'+Id+'Atag" name="atagNamem" alt="'+leftmenucategory+'">'+name+'</a></li>').appendTo('#tabUL');
		}
		
		var curIframeHeight = document.documentElement.clientHeight - $('#fixHeightDiv').height() - $('#footStatusDiv').height();
		if(closeflag.indexOf('business_apps||') != '-1'){
			//按应用处理
			var customer_url = url.split('/');
			var helperurl = customer_url[0]+'//'+customer_url[2]+'/astercc_hash_helper.html';
			$('<div id="'+Id+'" name="tabDiv" style="display:block;"><iframe id="'+Id+'Frame" name="'+Id+'Frame" src="javascript:;" style="width:100%;height:100%;" marginwidth="0" framespacing="0" marginheight="0" frameborder="no"></iframe></div>').appendTo('#allTabDivSite');
			//<iframe id="'+Id+'_helperFrame" name="'+Id+'_helperFrame" src="'+helperurl+'" style="display:none;"></iframe>
			//写form			
			var appFrameHtml = '<iframe id="astercc_hashhelperFrame" name="astercc_hashhelperFrame" src="'+helperurl+'" style="display:none;"></iframe><iframe id="astercc_customerFrame" name="astercc_customerFrame" style="width:100%;height:100%;" frameborder="no" marginheight="0" framespacing="0" marginwidth="0" src="javascript:;" >';
			
			var appFrameHtmlson = '<html>';
			appFrameHtmlson += '<head>';
			appFrameHtmlson += '<meta content="text/html; charset=UTF-8" http-equiv="Content-Type"/>';
			appFrameHtmlson += '<title>asterCC Application</title>';
			appFrameHtmlson += '</head>';
			appFrameHtmlson += '<body>';
			appFrameHtmlson += '<div style="width:100%;background-color:#EE6363;text-align:center;font-size:16px;">';
			appFrameHtmlson += 'asterCC应用加载中,请稍候...';
			appFrameHtmlson += '</div>';
			appFrameHtmlson += '<form id="ccappfrom" name="ccappfrom" method="post" action="'+url+'">';
			if(closeflag != 'business_apps||'){
				var frommessages = closeflag.split('business_apps||')[1].split('&');
				for(var i=0;i<frommessages.length;i++){
					var curfrommessages = frommessages[i].split('=');
					appFrameHtmlson += '<input type="hidden" id="'+curfrommessages[0]+'" name="'+curfrommessages[0]+'" value="'+curfrommessages[1]+'" />';
				}
			}
			appFrameHtmlson += '</form>';
			appFrameHtmlson += '<script type="text/javascript">';
			appFrameHtmlson += 'document.getElementById("ccappfrom").submit();';
			appFrameHtmlson += '</script>';	
			appFrameHtmlson += '</body>';		
			appFrameHtmlson += '</html>';
			appFrameHtml += '</iframe>';
			document.getElementById(Id+"Frame").contentWindow.document.write(appFrameHtml);
			document.getElementById(Id+"Frame").contentWindow.document.close();
			
			document.getElementById(Id+"Frame").contentWindow.document.getElementById('astercc_customerFrame').contentWindow.document.write(appFrameHtmlson);
			document.getElementById(Id+"Frame").contentWindow.document.getElementById('astercc_customerFrame').contentWindow.document.close();
			
			var iframe2 = $("#"+Id+"Frame");
			if (iframe2.attachEvent){
				iframe2.attachEvent("onload", function(){
					onloadTip += Id+",";
				});
			} else {
				iframe2.load(function () {
					onloadTip += Id+",";
				});
			}			
		}else{
			//打开遮罩	
			var jsystr = '';
			//alert(controller);
			//jsystr += '<div style="position: absolute;">Login...</div>';			
			$('<div id="'+Id+'" name="tabDiv" style="display:block;">'+jsystr+'<iframe id="'+Id+'Frame" name="'+Id+'Frame" src="'+url+'" style="width:100%;height:100%;" marginwidth="0" framespacing="0" marginheight="0" frameborder="no"></iframe></div>').appendTo('#allTabDivSite');
		}
		
		$('#'+Id).css("height",curIframeHeight+"px");
		//$('div [name=tabDiv]').css("height",curIframeHeight+"px");
		//$('#tabOfContent a[name=atagNamem]').attr('class','atagNameClass');
		//$('#'+Id+'Atag').attr('class','onlyMarkAtag');
		$('#tabOfContent a[name=atagNamem]').attr('class','atagNameClass');
		$('#'+Id+'Atag').attr('class','onlyMarkAtag');

		if(url == 'custompages'  && closeflag != 'virtualcustomerPreview'){
			//传事件
			var nameary = closeflag.split('-');
			var dbpopstr = 'source=virtualinterfaces&event=dbclick&vid='+nameary[0]+'&cid='+nameary[1];		
			var iframe2 = $("#"+Id+"Frame");
			if (iframe2.attachEvent){
				iframe2.attachEvent("onload", function(){
					fatherAccept(Id,dbpopstr,'');
					onloadTip += Id+",";
				});
			} else {
				iframe2.load(function () {
					fatherAccept(Id,dbpopstr,'');
					onloadTip += Id+",";
				});
			}			
		}
		
		if(closeflag == 'virtualcustomerPreview'){
			var nameary = Id.split('-');
			var dbpopstr = 'source=virtualinterfaces&event=dbclick&vid='+nameary[1]+'&cid=0';		
			var iframe2 = $("#"+Id+"Frame");
			if (iframe2.attachEvent){
				iframe2.attachEvent("onload", function(){
					fatherAccept(Id,dbpopstr,nameary[1]);
					onloadTip += Id+",";
				});
			} else {
				iframe2.load(function () {
					fatherAccept(Id,dbpopstr,nameary[1]);
					onloadTip += Id+",";
				});
			}		
		}
	}	
	
}

function showDiv(Id) {
	$('#allTabDivSite div[name=tabDiv]').css("display","none");
	$('#'+Id).css("display","block");
	if($('#'+Id+'Frame').length > 0){
		var framename = $('#'+Id+'Frame').attr('name');
		if(typeof(framename) != 'undefined' && framename != 'undefined' && framename != '') {
			try{
				if($(window.frames[''+framename+''].document).find("#mainContent").length > 0){
					$(window.frames[''+framename+''].document).find("#mainContent").css("display","block");
					document.getElementById(""+Id+"Frame").contentWindow.reMask();
				}
			}catch(e){}
		}
	}
	$('#tabOfContent a[name=atagNamem]').attr('class','atagNameClass');
	$('#'+Id+'Atag').attr('class','onlyMarkAtag');
}

//刷新当前标签
function refCurTab(){
	$('#tabUL li').each(function(e){
		if($(this).children('a').css('background-color') == 'rgb(255, 255, 255)' || $(this).children('a').css('background-color') == '#ffffff'){
			var refframe = $(this).children('a').attr('id').replace(/\Atag/g,'Frame');
			var iframename = $('#'+refframe).attr('name');
			try{
				window.frames[''+iframename+''].location.reload();
			}catch(e){
				alert(refresherrorShow);
			}
			return;
		}
	});
}

//关闭所有可关闭的标签
function closeAllTab(){
	var visiblenum = $('#tabUL').find('li:visible').length;	
	$('#tabUL li').each(function(e){
		if($(this).children('img').attr('src') == 'img/working_platforms/closex.gif'){
			var divid = $(this).attr('id').replace(/\ALi/g,'');
			setTimeout(function(){ closeTabA(divid); }, 300);
		}
	});
}

function closeTabA(tabTag) {
	$('#allopentabULlist_'+tabTag).remove();
	$('#tabOfContent a[name=atagNamem]').attr('class','atagNameClass');
	$('#tabUL  #'+tabTag+'ALi').prev('li').children('a').attr('class','onlyMarkAtag');
	
	$('#allTabDivSite div[name=tabDiv]').css('display','none');
	$('#allTabDivSite  #'+tabTag).prev().css('display','block');							
	$('#allTabDivSite  #'+tabTag).remove();
	
	if(onloadTip.indexOf(''+tabTag+',') != -1){		
		var zzstr = '/'+tabTag+',/g';
		onloadTip = onloadTip.replace(eval(zzstr),'');			
	}
	
	if($('#tabUL').find('li:hidden').length == '1'){
		//显示出来   并且隐藏左右按钮
		$('#tab_left').css('display','none');
		$('#tab_right').css('display','none');		
		$('#tabUL').find('li:hidden').css('display','block');
	}else if($('#tabUL').find('li:hidden').length > 1){		
		//如果之前有隐藏的
		var noneli = 'no';
		$('#tabUL  #'+tabTag+'ALi').prevAll('li').each(function(e){
			if($(this).css('display') == 'none'){
				noneli = 'yes';
			}
		});

		if(noneli == 'yes'){
			//把最后一个隐藏的显示
			$('#tabUL  #'+tabTag+'ALi').prevAll('li:hidden:first').css('display','block');			
		}else{
			//否则 把之后的第一个隐藏的显示
			$('#tabUL  #'+tabTag+'ALi').nextAll('li:hidden:first').css('display','block');			
		}
		
		if($('#tabUL').find('li:visible:first').html() == $('#tabUL').find('li:first').html()){
			$('#tab_left').html('<img src="img/working_platforms/left_btny.png"/>');		
		}else{
			$('#tab_left').html('<img src="img/working_platforms/left_btn.png"  onclick="tabLeftMove();" />');
		}
		
		if($('#tabUL').find('li:visible:last').html() == $('#tabUL').find('li:last').html()){
			$('#tab_right').html('<img src="img/working_platforms/right_btny.png" />');
		}else{
			$('#tab_right').html('<img src="img/working_platforms/right_btn.png"  onclick="tabRightMove();" />');
		}		
	}
	$('#tabUL  #'+tabTag+'ALi').remove();
}