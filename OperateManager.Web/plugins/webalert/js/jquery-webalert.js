(function ($) {
    //弹出框
    $.webalert = function (options) {
        var $dialog;
        var $dialogbody;
        var $webalerticon;
        var $dialogcontent;
        var $dialogtools;
        var $okbtn;
        var $cannelbtn;

        if ($(".jquery-webalert").length > 0)
            $(".jquery-webalert").remove();
        $dialog = $('<div class="jquery-webalert"><div class="webalert-body"><div class="webalert-icon"></div><h2 class="webalert-title"></h2><div class="webalert-content"></div><div class="webalert-tools"></div></div><div class="webalert-overlay"></div></div>');
        $dialog.appendTo($(document.body));
        $dialogbody = $('.webalert-body');
        $dialogcontent = $('.webalert-content');
        $webalerticon = $('.webalert-icon');
        $dialogtools = $('.webalert-tools');

        if (options.type == undefined || options.type == null)
            options.type = "info";
        if (options.type == "input")
            $dialogbody.css({ 'height': '230px' });
        $dialogbody.show(400);

        if (options.title == undefined || options.title == null)
            options.title = "";
        $dialogbody.children('.webalert-title').html(options.title);

        if (options.text == undefined || options.text == null)
            options.text = "";
        $dialogcontent.html(options.text);

        $okbtn = $('<button class="webalert-okbtn">确定</div>');
        $okbtn.appendTo($dialogtools);
        $okbtn.on('click', function () {
            if (options.type == 'input') {
                var val = $dialogcontent.find('input.userinput').eq(0).val();
                close(val);
            } else {
                close(true);
            }
        });
        if (options.type == "confirm" || options.type == "input") {    
            $cannelbtn = $('<button class="webalert-cannelbtn">取消</div>');
            $cannelbtn.appendTo($dialogtools);
            $cannelbtn.on('click', function () {
                close(false);
            });
        }
        switch (options.type) {
            case "success":
                $webalerticon.addClass('success');
                $webalerticon.fadeIn(2000).append("<span>✔</span>");
                break;
            case "warning":
                $webalerticon.addClass('warning');
                $webalerticon.fadeIn(2000).append("<span>！</span>");
                break;
            case "error":
                $webalerticon.addClass('error');
                $webalerticon.fadeIn(2000).append("<span>✘</span>");
                break;
            case "confirm":
                $webalerticon.addClass('confirm');
                $webalerticon.fadeIn(2000).append("<span>？</span>"); //☺
                break;
            case "input":               
                $webalerticon.remove();               
                var html = $dialogcontent.html();
                html += '<p class="input-content-p"><input class="userinput" name="" value=""/></p>';
                $dialogcontent.html(html);              
                break;
            default:
                $webalerticon.addClass('info');
                $webalerticon.fadeIn(2000).append("<span>i</span>");
                break;
        }       

        if (!isNaN(options.timer) && parseInt(options.timer) > 0) {
            var autoclose = setTimeout(function () {                
                clearTimeout(autoclose);
                close();
            }, parseInt(options.timer));
        }
        function close(data) {
            if ($dialog!=undefined) {
                $dialogbody.hide(200, function () {
                    $dialog.remove();
                    if (options.fun != undefined)
                        options.fun(data);
                });
            }
        }
    }
})(jQuery);