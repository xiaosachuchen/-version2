$(document).ready(function () {
    $.ManagerShow();
    $.MenuShow();
    $.DataBaseMarkInit();  
})

jQuery.extend({
    //加载管理员基本信息
    ManagerShow: function () {
        $.ajax({
            url: '/RIPSP/Base/Account/GetLoginManager',
            method: 'Get',
            success: function (result) {
                if (result.Rcode == 1) {
                    $("#dropdown-user>a>div").html(result.Rdata.RealName);
                    $("#logout_a").on('click', function () {
                        $.ajax({
                            url: '/RIPSP/Base/Account/ManagerLogout',
                            method: 'Get',
                            success: function () {
                                document.location.href = '/index.shtml';
                            }
                        });
                    })




                } else {
                    $.ShowMessage(result.Rcode, result.Rmsg);
                    return;
                }
            }
        });      
    },
    //加载左侧菜单
    MenuShow: function () {
        $.ajax({
            url: '/RIPSP/Base/Account/GetMenuTree',
            method: 'Get',
            success: function (result) {
                if (result.Rcode == 1) {
                    $("#mainnav-menu").empty();
                    $.each(result.Rdata, function (i, item) {
                        $("#mainnav-menu").append('<li class="list-header">' + item.menuname + '</li>');
                        if (item.children.length > 0) {
                            var subli, sublink, linkurl;
                            $.each(item.children, function (i, subitem) {
                                subli = $('<li></li>');
                                subli.appendTo($("#mainnav-menu"));
                                sublink = $('<a id="menu_' + subitem.menuid + '"></a>');
                                sublink.appendTo(subli);
                                sublink.append('<i class="' + subitem.pagemark + '"></i>');
                                sublink.append('<span class="menu-title">' + subitem.menuname + '</span>');                                                                                                         
                                if (subitem.children.length > 0) {
                                    sublink.attr('href', '#');
                                    sublink.append('<i class="arrow"></i>');
                                    var subul = $('<ul class="collapse"></ul>');
                                    subul.appendTo(subli);
                                    var thirdli,thirdlink;
                                    $.each(subitem.children, function (i, thirditem) {
                                        thirdli = $('<li></li>');
                                        thirdli.appendTo(subul);
                                        thirdlink = $('<a id="menu_' + thirditem.menuid + '">' + thirditem.menuname + '</a>');
                                        thirdlink.appendTo(thirdli);
                                        linkurl = thirditem.path;
                                        if (linkurl != null) {
                                            if (linkurl.indexOf('?') > 0)
                                                linkurl = linkurl + '&navid=' + thirditem.menuid;
                                            else
                                                linkurl = linkurl + '?navid=' + thirditem.menuid;
                                        }
                                        thirdlink.attr('href', linkurl);
                                        if (thirdlink.isoutlink == 1)
                                            thirdlink.attr('target', '_blank');
                                    });
                                } else {
                                    linkurl = subitem.path;
                                    if (linkurl != null) {
                                        if (linkurl.indexOf('?') > 0)
                                            linkurl = linkurl + '&navid=' + subitem.menuid;
                                        else
                                            linkurl = linkurl + '?navid=' + subitem.menuid;
                                    }
                                    sublink.attr('href', linkurl);
                                    if(subitem.isoutlink==1)
                                        sublink.attr('target', '_blank');
                                }
                            })
                        }
                        $("#mainnav-menu").append('<li class="list-divider"></li>');
                    });
                    var navid = $.getUrlParam('navid');
                    var activelink = $("#mainnav-menu").find('a#menu_' + navid).parent('li');
                    if (!!activelink) {
                        activelink.addClass('active-link');
                        activelink = activelink.parent().prev('a');
                        if (!!activelink) {
                            activelink.click();
                            activelink.parent('li').addClass('active-sub active');
                        }
                    }
                    $(document).trigger('nifty.ready');
                   
                } else {
                    $.ShowMessage(result.Rcode, result.Rmsg);
                    return;
                }
            }
        });
    },
    //初始化按钮
    DataBaseMarkInit:function(){
        $("#toolbar button").on('click', function () {
            var mark = $(this).attr('data-mark');
            var markfun = eval('$.' + mark);
            if (markfun != undefined) {
                if (mark == 'Delete' || mark == 'BatchDelete') {
                    $.webalert({
                        text: '确认继续操作？',
                        type: 'confirm',
                        fun: function (data) {
                            if (data == true) {
                                markfun();
                            }
                        }
                    });
                } else {
                    markfun();
                }
            }
        });
        $("#search_btn").on('click', function () {
            $("#TableData").bootstrapTable('refresh');
        })
        $("#edit_form_sbtn").on('click', function () {
            //$(this).button('loading');
            //save当前按钮对象
            $.Save($(this));
        })
        $("#edit_dictype_sbtn").on('click', function () {
            $.Save_Dtype($(this));
        })
        $("#edit_form_rolesmenus").on('click', function () {
            $.Save_RolesMenus($(this));
        })
        $("#edit_form_magagerorg").on('click', function () {
            $.Save_Org($(this));
        });
        $("#edit_form_db").on('click', function () {
            $.Save_Db($(this));
        });
        $("#edit_form_ip").on('click', function () {
            $.IpSave($(this));
        });
        $("#edit_form_userForCus").on('click', function () {
            $.Save_Ufc($(this));
        });
        $("#edit_form_mgrole").on('click', function () {
            $.Save_MangDbRoles($(this));
        });
    },
    //显示错误提示
    ShowMessage: function (code, msg) {
        // -2：未登录或登录超时 warning
        // -1：系统错误，友情提示 error
        //  0：操作错误，友情提示 warning
        //  1：操作成功 success
        //  11:未选择项 info
        //  12:错选了多项 info
        switch (code) {
            case -2:
                $.webalert({
                    text: '未登录或登录超时',
                    timer: 2000,
                    type: 'warning',
                    fun: function () {
                        document.location.href = '/index.shtml';
                    }
                });                
                break;
            case -1:
                $.webalert({
                    text: '系统错误，请联系管理员',
                    timer: 3000,
                    type: 'error'
                });
                break;
            case 0:
                $.webalert({
                    text: msg,
                    timer: 3000,
                    type: 'warning'
                });
                break;
            case 1:
                $.webalert({
                    text: '操作成功',
                    timer: 2000,
                    type: 'success'
                });
                break;
            case 11:
                $.webalert({
                    text: '至少选择一项',
                    timer: 2000,
                    type: 'info'
                });
                break;
            case 12:
                $.webalert({
                    text: '只能选择一项',
                    timer: 2000,
                    type: 'info'
                });
                break;
            case 21:
                $.webalert({
                    text: '不允许操作系统数据',
                    timer: 2000,
                    type: 'info'
                });
                break;
        }
    },
    //获取url参数
    getUrlParam: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    },
});
