$(function(){
				//$(".banner-wrap span").eq(0).addClass("show");
				var num = 0
				/*function removeImg(){
					$(".banner-wrap span").eq(num-1).removeClass("show");
					$(".banner-wrap span").eq(num+1).removeClass("show");
				}*/
				/*function nextImg(){
					num ++;
					$(".banner-wrap span").eq(num).addClass("show");
					removeImg()
					if(num > 2){
						num = 0
						$(".banner-wrap span").eq(num).addClass("show");
						removeImg()
					}
				}
				function prevImg(){
					num --;
					$(".banner-wrap span").eq(num).addClass("show");
					removeImg()
					if(num < 0){
						num = 2
						$(".banner-wrap span").eq(num).addClass("show");
						removeImg()
					}
				}*/
				function prevImg() {
				    var spanMarLef = parseInt($(".banner-wrap span").css("margin-left"))
				    if (spanMarLef == -2384) {
				        $(".banner-wrap span").animate({ marginLeft: "-1192" })
				    } else if (spanMarLef == -1192) {
				        $(".banner-wrap span").animate({ marginLeft: "0" })
				    } else {
				        $(".banner-wrap span").animate({ marginLeft: "-2384" })
				    }

				}
				function nextImg() {
				    var spanMarLef = parseInt($(".banner-wrap span").css("margin-left"))
				    if (spanMarLef == 0) {
				        $(".banner-wrap span").animate({ marginLeft: "-1192" })
				    } else if (spanMarLef == -1192) {
				        $(".banner-wrap span").animate({ marginLeft: "-2384" })
				    } else {
				        $(".banner-wrap span").animate({ marginLeft: "0" })
				    }
				}
				$(".left-btn").click(function () {
				    prevImg()
				})
				$(".right-btn").click(function () {
				    nextImg()
				})
				var tc = {
					showTc:function(w){
						$(w).show();
						$(w).children(".tc-window").click(function(){
							$(".tc .tc-wrap embed").stop()
							$(".tc .tc-wrap embed").attr("src","")
							//$('video').trigger('pause');
							$(w).hide();
						})
					},
					chanTc:function(n,t){
						tc.showTc($(".tc"))
						$(".tc .tc-wrap embed").attr("src","img/video\/"+n+"\/"+t+".mp4")
					},
				}
				var tc2 = {
					showTc:function(w){
						$(w).show();
						$(w).find(".tc-window,.dbg-cancel").click(function(){
							$(w).hide();
						})
					},
				}
				$(".dbgm").click(function(){
					tc2.showTc($(".tc"))
				})
					
				/*$("a.yyplay").click(function(){
					tc.chanTc(1,1)
				})
				$("a.hxplay").click(function(){
					tc.chanTc(2,1)
				})
				$("a.swplay").click(function(){
					tc.chanTc(3,1)
				})
				$("a.sxplay").click(function(){
					tc.chanTc(4,1)
				})
				$("a.wlplay").click(function(){
					tc.chanTc(5,1)
				})*/
				
				
		/*$("a.yyplay").each(function(i){
			//alert(i)
			$(this).click(function(){
				if(i>9){
					i = i % 10
				}
				tc.chanTc(1,i+1)
			})
		})
		$("a.hxplay").each(function(i){
			//alert(i)
			$(this).click(function(){
				if(i>9){
					i = i % 10
				}
				tc.chanTc(2,i+1)
			})
		})
		$("a.swplay").each(function(i){
			//alert(i)
			$(this).click(function(){
				if(i>9){
					i = i % 10
				}
				tc.chanTc(3,i+1)
			})
		})
		$("a.sxplay").each(function(i){
			//alert(i)
			$(this).click(function(){
				if(i>9){
					i = i % 10
				}
				tc.chanTc(4,i+1)
			})
		})
		$("a.wlplay").each(function(i){
			//alert(i)
			$(this).click(function(){
				if(i>9){
					i = i % 10
				}
				tc.chanTc(5,i+1)
			})
		})*/
		
		
		$(".swplay.sw1").click(function(){
			tc.chanTc(2,1)
			window.location.href="play.html"
		})
		$(".swplay.sw2").click(function(){
			tc.chanTc(2,2)
			window.location.href="play.html"
		})
		$(".swplay.sw3").click(function(){
			tc.chanTc(2,3)
			window.location.href="play.html"
		})
		$(".swplay.sw4").click(function(){
			tc.chanTc(2,4)
			window.location.href="play.html"
		})
		$(".swplay.sw5").click(function(){
			tc.chanTc(2,5)
			window.location.href="play.html"
		})
		$(".swplay.sw6").click(function(){
			tc.chanTc(2,6)
			window.location.href="play.html"
		})
		$(".swplay.sw7").click(function(){
			tc.chanTc(2,7)
			window.location.href="play.html"
		})
		$(".swplay.sw8").click(function(){
			tc.chanTc(2,8)
			window.location.href="play.html"
		})
		$(".swplay.sw9").click(function(){
			tc.chanTc(2,9)
			window.location.href="play.html"
		})
		$(".swplay.sw10").click(function(){
			tc.chanTc(2,10)
			window.location.href="play.html"
		})
		$(".hx1").click(function(){
			tc.chanTc(1,1)
			window.location.href="play.html"
		})
		$(".hx2").click(function(){
			tc.chanTc(1,2)
			window.location.href="play.html"
		})
		$(".hx3").click(function(){
			tc.chanTc(1,3)
			window.location.href="play.html"
		})
		$(".hx4").click(function(){
			tc.chanTc(1,4)
			window.location.href="play.html"
		})
		$(".hx5").click(function(){
			tc.chanTc(1,5)
			window.location.href="play.html"
		})
		$(".hx6").click(function(){
			tc.chanTc(1,6)
			window.location.href="play.html"
		})
		$(".hx7").click(function(){
			tc.chanTc(1,7)
			window.location.href="play.html"
		})
		$(".hx8").click(function(){
			tc.chanTc(1,8)
			window.location.href="play.html"
		})
		$(".hx9").click(function(){
			tc.chanTc(1,9)
			window.location.href="play.html"
		})
		$(".hx10").click(function(){
			tc.chanTc(1,10)
			window.location.href="play.html"
		})
		$(".yy1").click(function(){
			tc.chanTc(5,1)
			window.location.href="play.html"
		})
		$(".yy2").click(function(){
			tc.chanTc(5,2)
			window.location.href="play.html"
		})
		$(".yy3").click(function(){
			tc.chanTc(5,3)
			window.location.href="play.html"
		})
		$(".yy4").click(function(){
			tc.chanTc(5,4)
			window.location.href="play.html"
		})
		$(".yy5").click(function(){
			tc.chanTc(5,5)
			window.location.href="play.html"
		})
		$(".yy6").click(function(){
			tc.chanTc(5,6)
			window.location.href="play.html"
		})
		$(".yy7").click(function(){
			tc.chanTc(5,7)
			window.location.href="play.html"
		})
		$(".yy8").click(function(){
			tc.chanTc(5,8)
			window.location.href="play.html"
		})
		$(".yy9").click(function(){
			tc.chanTc(5,9)
			window.location.href="play.html"
		})
		$(".yy10").click(function(){
			tc.chanTc(5,10)
			window.location.href="play.html"
		})
		$(".sx1").click(function(){
			tc.chanTc(3,1)
			window.location.href="play.html"
		})
		$(".sx2").click(function(){
			tc.chanTc(3,2)
			window.location.href="play.html"
		})
		$(".sx3").click(function(){
			tc.chanTc(3,3)
			window.location.href="play.html"
		})
		$(".sx4").click(function(){
			tc.chanTc(3,4)
			window.location.href="play.html"
		})
		$(".sx5").click(function(){
			tc.chanTc(3,5)
			window.location.href="play.html"
		})
		$(".sx6").click(function(){
			tc.chanTc(3,6)
			window.location.href="play.html"
		})
		$(".sx7").click(function(){
			tc.chanTc(3,7)
			window.location.href="play.html"
		})
		$(".sx8").click(function(){
			tc.chanTc(3,8)
			window.location.href="play.html"
		})
		$(".sx9").click(function(){
			tc.chanTc(3,9)
			window.location.href="play.html"
		})
		$(".sx10").click(function(){
			tc.chanTc(3,10)
			window.location.href="play.html"
		})
		$(".wl1").click(function(){
			tc.chanTc(4,1)
			window.location.href="play.html"
		})
		$(".wl2").click(function(){
			tc.chanTc(4,2)
			window.location.href="play.html"
		})
		$(".wl3").click(function(){
			tc.chanTc(4,3)
			window.location.href="play.html"
		})
		$(".wl4").click(function(){
			tc.chanTc(4,4)
			window.location.href="play.html"
		})
		$(".wl5").click(function(){
			tc.chanTc(4,5)
			window.location.href="play.html"
		})
		$(".wl6").click(function(){
			tc.chanTc(4,6)
			window.location.href="play.html"
		})
		$(".wl7").click(function(){
			tc.chanTc(4,7)
			window.location.href="play.html"
		})
		$(".wl8").click(function(){
			tc.chanTc(4,8)
			window.location.href="play.html"
		})
		$(".wl9").click(function(){
			tc.chanTc(4,9)
			window.location.href="play.html"
		})
		$(".wl10").click(function(){
			tc.chanTc(4,10)
			window.location.href="play.html"
		})
		$(".nav-xxb").addClass("show");
		
		$(".subnav ul li").hover(function(){
		    var index = $(".subnav ul li").index($(this));
			$(this).children("a").addClass("hover")
			$(this).siblings("li").children("a").removeClass("hover")
			$(".subnav ol").eq(index).addClass("show")
			$(".subnav ol").eq(index).siblings("ol").removeClass("show")
			$(".subnav ol li p").hide()
			$(".subnav ol li a.more").text("更多")
		})
				
		$(".zxkt a").hover(function(){
			$(this).children("ol").toggle();
			$(this).children("i").toggle()
		})
		
		$(".zxpj .left-right a").hover(function(){
			$(this).children("span").toggle();
			$(this).children("ul").toggle()
		})
		
		$("header #username").click(function(){
			var ul = $("header ul")
			if(ul.is(':hidden')){
				ul.show()
			}else{
				ul.hide()
			}
			
		})
				
		function alertInfo()
		{
		    $.ajax({
		        url: '/Interface/UserAccount/GetCurrentUser',
		        method: 'Get',
		        success: function (data) {
		            if (data.Rcode == 1) {
		                var html = [];
		                html.push('	<header>')
		                html.push('	<div class="main2">')
		                html.push(' <a class="fl" href="index.html"><img src="img/logo.png" /></a>')
		                html.push(' <span class="seach">')
		                html.push(' 	<input type="text" id="search_keywords" placeholder="小学，初中，高中课程" />')
		                html.push(' 	<a href="classlist.html" id="btn_search">搜索课程</a>')
		                html.push(' </span>')
		                html.push(' <a class="fr" href="login.html?type=logout" >退出 </a>')
		                html.push(' <a class="fr" href="#" id="usercenter">欢迎:' + data.Data.NickName + '</a>')
		                html.push('</div>')
		                html.push('</header>')
		                $("#headerHtml").empty();
		                $("#headerHtml").append(html.join(""));

		                var html2 = [];
		                html2.push('	<header>')
		                html2.push('	<div class="main2">')
		                html2.push(' <a class="fl" href="index.html"><img src="img/logo.png" /></a>')
		                /*html.push(' <a class="fr">注册</a>')
                        html.push(' <a class="fr" href="login.html">登录</a>')*/
		                html2.push(' <a id="username" class="fr"><img src="img/classlist/pic-16.png" />'+data.Data.NickName+'</a>')
		                html2.push(' <a class="fr" href="user-shopcar.html"><img src="img/classlist/pic-161.png" />购物车</a>')
		                html2.push('<ul><li><a>我的收藏</a></li><li><a>我的订单</a></li></ul>')
		                html2.push('</div>')
		                html2.push('</header>')


		                $("#headerHtml2").empty();
		                $("#headerHtml2").append(html2.join(""));

		                var html3 = [];
		                html3.push('<div class="user-center-top">')
		                html3.push('<div class="main">')
		                html3.push('<dl>')
		                html3.push('<dt class="fl">')
		                html3.push('<span>')
		                html3.push('<img src="img/user/pic-4.png" />')
		                html3.push('</span>')
		                html3.push('</dt>')
		                html3.push('<dd class="username">')
		                html3.push('' + data.Data.NickName + '')
		                
				        
		                html3.push('</dd>')
		                html3.push('<dd class="ljlearn">')
		                html3.push('累计学习<a>2</a>小时')
		                html3.push('</dd>')
		                html3.push('<dd class="gxqm">')
		                html3.push('这家伙很懒什么都没留下')
		                html3.push('</dd>')
						
		                html3.push('</dl>')
		                html3.push('</div>')
		                html3.push('</div>')

		                $("#user-centerHtml").empty();
		                $("#user-centerHtml").append(html3.join(""));
		            }
		            else {
		                var html = [];
		                html.push('	<header>')
		                html.push('	<div class="main2">')
		                html.push(' <a class="fl" href="index.html"><img src="img/logo.png" /></a>')
		                html.push(' <span class="seach">')
		                html.push(' 	<input type="text" id="search_keywords" placeholder="小学，初中，高中课程" />')
		                html.push(' 	<a href="classlist.html" id="btn_search">搜索课程</a>')
		                html.push(' </span>')
		                html.push(' <a class="fr" href="register.html" id="head_regit">注册</a>')
		                html.push(' <a class="fr" href="login.html" id="head_login">登录</a>')
		                html.push('</div>')
		                html.push('</header>')
		                $("#headerHtml").empty();
		                $("#headerHtml").append(html.join(""));

		                var html2 = [];
		                html2.push('	<header>')
		                html2.push('	<div class="main2">')
		                html2.push(' <a class="fl" href="index.html"><img src="img/logo.png" /></a>')
		                /*html.push(' <a class="fr">注册</a>')
                        html.push(' <a class="fr" href="login.html">登录</a>')*/
		                html2.push(' <a id="username" href="login.html" class="fr">未登录</a>')
		                html2.push(' <a class="fr" href="user-shopcar.html"><img src="img/classlist/pic-161.png" />购物车</a>')
		                html2.push('<ul><li><a>我的收藏</a></li><li><a>我的订单</a></li></ul>')
		                html2.push('</div>')
		                html2.push('</header>')

		                $("#headerHtml2").empty();
		                $("#headerHtml2").append(html2.join(""));

		                var html3 = [];
		                html3.push('<div class="user-center-top">')
		                html3.push('<div class="main">')
		                html3.push('<dl>')
		                html3.push('<dt class="fl">')
		                html3.push('<span>')
		                html3.push('<img src="img/user/pic-4.png" />')
		                html3.push('</span>')
		                html3.push('</dt>')
		                html3.push('<dd class="username">')
		                html3.push('测试账号')

		                html3.push('</dd>')
		                html3.push('<dd class="ljlearn">')
		                html3.push('累计学习<a>2</a>小时')
		                html3.push('</dd>')
		                html3.push('<dd class="gxqm">')
		                html3.push('这家伙很懒什么都没留下')
		                html3.push('</dd>')

		                html3.push('</dl>')
		                html3.push('</div>')
		                html3.push('</div>')

		                $("#user-centerHtml").empty();
		                $("#user-centerHtml").append(html3.join(""));
		            }
		        }
		    })
		}
		alertInfo();
				
		var publicHtml = {
				headHtml:function(m){
					var html = [];
				html.push('	<header>')
				html.push('	<div class="main2">')
				html.push(' <a class="fl" href="indexV2.html"><img src="img/logo.png" /></a>')
				html.push(' <span class="seach">')
				html.push(' 	<input type="text" id="search_keywords" placeholder="小学，初中，高中课程" />')
				html.push(' 	<a href="classlist.html" id="btn_search">搜索课程</a>')
				html.push(' </span>')
				html.push(' <a class="fr" href="register.html" id="head_regit">注册</a>')
				html.push(' <a class="fr" href="login.html" id="head_login">登录</a>')
				html.push('</div>')
				html.push('</header>')
					
				m.append(html.join(""))
					
			},
			headHtml2:function(m){
					var html = [];
				html.push('	<header>')
				html.push('	<div class="main2">')
				html.push(' <a class="fl" href="indexV2.html"><img src="img/logo.png" /></a>')
				/*html.push(' <a class="fr">注册</a>')
				html.push(' <a class="fr" href="login.html">登录</a>')*/
				html.push(' <a id="username" class="fr"><img src="img/classlist/pic-16.png" />账号1</a>')
				html.push(' <a class="fr" href="user-shopcar.html"><img src="img/classlist/pic-161.png" />购物车</a>')
				html.push('<ul><li><a>我的收藏</a></li><li><a>我的订单</a></li></ul>')
				html.push('</div>')
				html.push('</header>')
					
				m.append(html.join(""))
					
			},
			footerHtml:function(m){
				var html = [];
				
				html.push('	<footer>')
				html.push('<dl>')
				html.push('	<dd>')
				html.push('		<a>关于我们</a> |')
				html.push('		<a>联系我们</a> |')
				html.push('		<a>帮助中心</a> |')
				html.push('		<a>企业服务</a>')
				html.push('	</dd>')
				html.push('	<dd>')
				html.push('		合作机构:')
				html.push('		<a>黑龙江出版社</a>')
				html.push('		<a>辽海江出版社</a>')
				html.push('		<a>湖南江出版社</a>')
				html.push('		<a>21世纪出版社</a>')
				html.push('		<a>云南出版社</a>')
				html.push('		<a>世纪图书出版社</a>')
				html.push('		<a>青岛出版社</a>')
				html.push('	</dd>')
				html.push('	<dd>')
				html.push('		Copyright 2016-2017 congconglong.com 版权所有 京ICP备<a>11111111号</a>')
				html.push('	</dd>')
				html.push('</dl>')
				html.push('</footer>')
					m.append(html.join(""))
			},
			
			tcHtml:function(m){
				var html = [];
				html.push('	<div class="tc">')
				html.push('<div class="tc-window"></div>')
				html.push('<div class="tc-wrap">')
				html.push('<embed src="img/video/5/1.mp4" allowFullScreen="true" quality="high" width="800" height="600" align="middle" allowScriptAccess="always" type="application/x-shockwave-flash" autostart="false">')
          		html.push('</embed>')
				/*html.push('	<video src="img/video/1/01.mp4" type="video/mp4" controls="" width="100%" height="100%" preload>')*/
				/*html.push('		<source ></source>')*/
				/*html.push('	</video>')*/
				html.push('</div>')
				html.push('</div>')
					m.append(html.join(""))
			},
			tcHtml2:function(m){
				var html = [];
			html.push('	<div class="tc">')
			html.push('<div class="tc-window"></div>')
			html.push('	<div class="tc-wrap">')
			html.push('		<dl>')
			html.push('			<dt>温馨提示</dt>')
			html.push('			<dd class="tips">')
			html.push('				打包购买仅需<span>¥：180.00元</span>共包含<b>2</b>视频：')
			html.push('			</dd>')
			html.push('			<dd class="videolist">')
			html.push('				<ol>')
			html.push('					<li>')
			html.push('						<img src="img/classlist/pic-4.png" />')
			html.push('						<strong>课程一：易混淆动词词组</strong>')
			html.push('					</li>')
			html.push('					<li>')
			html.push('						<img src="img/classlist/pic-4.png" />')
			html.push('						<strong>课程二：易混淆动词词组</strong>')
			html.push('					</li>')
			html.push('				</ol>')
			html.push('			</dd>')
			html.push('			<dd class="clear"></dd>')
			html.push('			<dd>')
			html.push('				<a class="dbg-cancel">取消</a><a href="user-shopcar.html">加入购物车</a>')
			html.push('			</dd>')
			html.push('		</dl>')
			html.push('	</div>')
			html.push('	</div>')
					m.append(html.join(""))
			}
			
		}
		publicHtml.headHtml($("#headerHtml"));
		publicHtml.headHtml2($("#headerHtml2"));
		publicHtml.footerHtml($("#footerHtml"));
		publicHtml.tcHtml2($("#tcHtml2"));
		//publicHtml.tcHtml($("#tcHtml"));
})