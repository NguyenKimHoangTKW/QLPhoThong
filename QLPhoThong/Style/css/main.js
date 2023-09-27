userId = -1;
function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var c_value = document.cookie;
    var c_start = c_value.indexOf(" " + c_name + "=");
    if (c_start == -1) {
        c_start = c_value.indexOf(c_name + "=");
    }
    if (c_start == -1) {
        c_value = null;
    } else {
        c_start = c_value.indexOf("=", c_start) + 1;
        var c_end = c_value.indexOf(";", c_start);
        if (c_end == -1) {
            c_end = c_value.length;
        }
        c_value = unescape(c_value.substring(c_start, c_end));
    }
    return c_value;
}

function isMobile() {
    try {
        var user_agent = navigator.userAgent.toLowerCase();

        // Checks the user-agent
        if (user_agent != null) {
            // Checks if its a Windows browser but not a Windows Mobile browser
            if (user_agent.indexOf("windows") >= 0 && !user_agent.indexOf("windows ce") >= 0) {
                return false;
            }

            // Checks if it is a mobile browser
            var pattern = new RegExp("up.browser|up.link|windows ce|iphone|iemobile|webos|mini|mmp|symbian|midp|wap|phone|pocket|mobile|pda|psp", "gi");

            var pattern_ipad = new RegExp("kindle|iPad|ipad", "gi");
            if (user_agent.match(pattern_ipad)) {
                isIpad = true;
                return false;
            }

            if (user_agent.match(pattern)) {
                return true;
            }

            // Checks if the 4 first chars of the user-agent match any of the most popular user-agents
            var ua = ["acs-", "alav", "alca", "amoi", "audi", "aste", "avan", "benq", "bird", "blac", "blaz", "brew", "cell", "cldc", "cmd-", "dang", "doco", "eric", "hipt", "inno", "java", "jigs", "kddi", "keji", "leno", "lg-c", "lg-d", "lg-g", "lge-", "maui", "maxo", "midp", "mits", "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki", "opwv", "palm", "pana", "pant", "pdxg", "phil", "play", "pluc", "port", "prox", "qtek", "qwap", "sage", "sams", "sany", "sch-", "sec-", "send", "seri", "sgh-", "shar", "sie-", "siem", "smal", "smar", "sony", "sph-", "symb", "t-mo", "teli", "tim-", "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", "w3c ", "wap-", "wapa", "wapi", "wapp", "wapr", "webc", "winw", "winw", "xda", "xda-"];

            for (var i = 0; i < ua.length; i++) {
                if (ua[i] == user_agent.substr(0, 4)) {
                    return true;
                }
            }
        }
    } catch (e) {
    }
    return false;
}


function setUserSesstion(userId) {
    userId = userId;
}


function stripTagsHTML(content) {
    if (typeof (content) == 'undefined') {
        return "";
    }
    var text = $('<p>' + content + '</p>');
    text = text.text();
    if (text == undefined) {
        text = "";
    }
    return text;
}

function loadImage($img) {
    try {
        if (!$img.is('img')) {
            $img = $img.find('img');
        }
        if ($img.is('img')) {
            //if ( $img.attr( 'src' ).indexOf( 'grey.gif' ) != -1 ) {
            $img.attr('src', $img.data('original'));
            //}
        }
    } catch (e) {
    }
}
;

function getURLParams(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);

    if (results == null) {
        return "";
    } else {
        return decodeURI(results[1].split('+').join(' '));
    }
}

function showError(str) {
    $('#error span.error').html(str);
    $('#error').show(100);
}

function showLoading(id, isAppend) {
    if (id.indexOf('#') === 0) {
        id = id.substring(1);
    }
    var str = '<div class="loading"><div></div></div>';
    if (isAppend) {
        $('#' + id).append(str);
    } else {
        $('#' + id).html(str);
    }
}

function removeLoading(id) {
    if (id.indexOf('#') === 0) {
        id = id.substring(1);
    }
    $('#' + id).find('.loading').remove();
}


function mouseOverButtonCommand() {
    if (!$(this).find('a.btn-show-command').is('a')) {
        var buttonCommand = $('<a class="btn-show-command">&nbsp;</a>');
        $(this).append(buttonCommand);
    }
    if (($(this).find('.left a.avatar').attr('communityid') == userId && userId != -1) || (userId == -1000)) {
        $(this).find('a.btn-show-command').show();
    }
}

function mouseOutButtonCommand() {
    if (!$(this).find('a.btn-show-command').hasClass('selected')) {
        $(this).find('a.btn-show-command').hide();
    }
}

function showButtonDeleteWebtubeItemForUserPost(hasPermissions) {
    $('#list-item-webtube.page-items div.item').each(function () {
        if (hasPermissions || ($(this).find('.left a.avatar').attr('communityid') == userId)) {
            if (!$(this).find('a.btn-show-command').is('a')) {
                var buttonDelete = $('<a class="btn-show-command">&nbsp;</a>');
                buttonDelete.hide();
                $(this).append(buttonDelete);

                $(this).bind('mouseover', mouseOverButtonCommand).bind('mouseout', mouseOutButtonCommand);
                $(this).find('.btn-show-command').click(function () {
                    showMenuWebtubeItemUserPost($(this));
                });
            }
            $(this).attr('hasPermissions', hasPermissions);
            var title = 'click Ä‘á»ƒ xĂ³a ná»™i dung báº¡n Ä‘Ă£ gá»­i!';
            if (hasPermissions) {
                title = 'Click Ä‘á»ƒ xĂ³a ná»™i dung cá»§a ' + $(this).find('.right .title a').html() + ' Ä‘Ă£ gá»­i!';
            }

            $(this).find('a.btn-delete').attr('title', title);
        }
    });
}

function handleFormSearchForum(dialog, command) {
    dialog.find('#dialog-webtube-list').html("");
    dialog.find('#dialog-webtube-list').attr('start', 0)
    formSearchForum(dialog, command);
    return false;
}

function showMenuWebtubeItemUserPost(command) {
    command.parents('.item').find('.menu-command-item-webtbue').remove();
    if (command.hasClass('selected')) {
        command.removeClass('selected');
    } else {
        command.addClass('selected');
        var ul = $('<ul class="menu-command-item-webtbue"></ul>');
        ul.append('<li><a class="btn-delete">Xóa tin này</a></li>');
        ul.append('<li class="separator"></li>');
        ul.append('<li><a class="btn-move">Di chuyển sang diễn dàn</a></li>');
        ul.find('a.btn-move').on('click', function () {
            if (!$('.dialog-webtube').is('div')) {
                dialog = $('<div class="dialog-webtube"></div>');
                var form_search = $('<form class="form-forum"></form>');

                var input = $('<input type="text" name="key" placeholder ="Tìm diễn đàn" />');
                form_search.append(input);
                form_search.append('<span class="vs-close"></span><button><span></span></button>');
                var list = $('<ul id="dialog-webtube-list" start="0" limit="7"></ul>');
                dialog.append(form_search);
                dialog.append(list);
                dialog.dialog({
                    title: 'Di chuyển tới',
                    dialogClass: 'dialog-search-forum',
                    width: '400px',
                    autoOpen: true,
                    draggable: false,
                    resizable: false,
                    modal: true,
                    position: 'top',
                    show: {
                        effect: 'fade',
                        duration: 'fast'
                    },
                    hide: {
                        effect: 'fade',
                        duration: 'fast'
                    }
                });
                formSearchForum(dialog, command);

                form_search.submit(function () {
                    handleFormSearchForum(dialog, command);
                    return false;
                });

                input.on("keyup", function (e) {
                    clearTimeout($.data(this, 'timer'));

                    // Do Search
                    $(this).data('timer', setTimeout(function () {
                        handleFormSearchForum(dialog, command);
                    }, 500));
                });

                form_search.find('.vs-close').on('click', function () {
                    input.attr('value', "");
                    handleFormSearchForum(dialog, command);
                })
            } else {
                dialog.dialog('open');
            }
            dialog.find('.move').removeAttr('disabled');
            dialog.find('.loadding').remove();
            dialog.attr('webtubeItemId', command.parent().attr('webtubeitemid'))
        });
        command.parents('.item').append(ul);

        ul.find('.btn-delete').on('click', function () {
            deleteWebtubeItemUserPost(command.parent());
        })
    }

    $(document).mouseup(function (e) {
        if ($(e.target).closest('.btn-show-command.selected').length == 0) {
            command.removeClass('selected');
            command.hide();
            ul.hide();
        }
    });
}

//Di chuyen item tu webtube này sang webtube khác
function moveWebtubeItemUserPost(webtubeItemId, obj, command, dialog) {
    $(obj).parents('li.item-wtb').attr('style', 'position: relative;');
    $(obj).parents('li.item-wtb').append('<img class="loadding" src="/assets/global/img/ajax_loader.gif" />');
    $(obj).parents('li.item-wtb').find('.loadding').css({
        position: 'absolute',
        top: 10,
        right: 100
    });

    $(obj).attr("disabled", "disabled");
    var cfm = window.confirm("Bạn muốn di chuyển vào webtube này ?");
    if (cfm == true) {
        var data = {
            _f: 49,
            communityId: $('#community-id').html(),
            id: webtubeItemId,
            webtubeTruelifeId: $(obj).attr('truelifeId')
        };
        if (command.parent().attr('haspermissions') == 'false') {
            data._f = '311';
        }
        $.ajax({
            url: 'http://hub.vnedu.vn/offica/webtube/action',
            data: data,
            dataType: 'jsonp',
            jsonp: 'jsoncallback',
            success: function (response) {
                if (response.success) {
                    command.parent().remove();
                } else {
                    alert('Có lỗi trong quá trình di chuyển!');
                }
                $(obj).parents('li.item-wtb').find('.move').removeAttr('disabled');
                $(obj).parents('li.item-wtb').find('.loadding').remove();
                dialog.dialog('close');
            },
            error: function (response) {
                dialog.dialog('close');
            }
        });

    } else {
        $(obj).parents('li.item-wtb').find('.move').removeAttr('disabled');
        $(obj).parents('li.item-wtb').find('.loadding').remove();
    }
}

function deleteWebtubeItemUserPost(object) {
    object.append('<img class="loadding" src="/assets/global/img/ajax_loader.gif?f=9cTX6F6sk|qkZEBY4j77vCyaoS|Z00CZ|YCU0wP7KuQ=" />');
    object.find('.loadding').css({
        position: 'absolute',
        top: 0,
        right: 10
    });

    if (object.attr('hasPermissions') == 'true') {
        var data = {
            _f: 18,
            list: object.attr('webtubeItemId'),
            webtubeId: object.attr('webtubeId'),
            communityId: $('#community-id').html()
        };
    } else {
        var data = {
            _f: 308,
            list: object.attr('truelifeId'),
            truelifeId: $('#truelife-id').html()
        };
    }

    var cfm = window.confirm("Bạn muốn xóa mục này ?");
    if (cfm == true) {
        $.ajax({
            url: 'http://hub.vnedu.vn/offica/webtube/action',
            data: data,
            dataType: 'jsonp',
            jsonp: 'jsoncallback',
            success: function (response) {
                if (response.success) {
                    // Xóa thành công ẩn item đi
                    object.remove();
                    object.find('.loadding').remove();
                } else {
                    //có lỗi trong quá trình xử lý
                    object.find('.loadding').remove();
                    object.append('<div class="error" style="color: red; position: absolute; right: 0;top: 0;">Có lỗi trong quá trình xử lý vui lòng thực hiện lại!</div>');
                    object.find('.right').css({
                        opacity: '0.5'
                    });
                    setTimeout(function () {
                        object.find('div.error').remove();
                        object.find('.right').css({
                            opacity: '1'
                        });
                    }, 1000)
                }
            },
            error: function (response) {}
        });
    } else {
        object.find('.loadding').remove();
    }
}

function checkPermissionsWebtube() {
    $.ajax({
        url: '/service/post/checkPermissionWebtube',
        data: {
            communityId: $('#community-id').html()
        },
        dataType: 'json',
        success: function (response) {
            if (response.success) {
                //bạn có quyền quản trị webtube thì hiển thị nút xóa
                showButtonDeleteWebtubeItemForUserPost(true);
                userId = -1000;
            } else {
                //Nếu không có quyền quản trị thì kiểm tra bạn có phải là người tạo bài
                checkSenderWebtubeItemUserPost();
            }
        },
        error: function (response) {}
    });
}
//*****************************************************************//

function getListWebtube(config, callback, containerId) {
    showLoading('list-item', true)
    var data = {
        start: 0,
        limit: 1000,
    };
    $.extend(data, config);

    $.ajax({
        url: '/service/hdsd/getList',
        type: 'post',
        dataType: 'json',
        data: data,
        success: function (response) {
            if (window[callback]) {
                window[callback](response);
            }
        },
        error: function () {}
    });
}
;

function getSettings(name, callback, params) {
    $.ajax({
        url: 'http://hub.vnedu.vn/offica/webtube/action',
        data: $.extend({
            _f: 31,
            ownerId: communityId,
            name: name
        }, params),
        dataType: 'jsonp',
        jsonp: 'jsoncallback',
        context: this,
        success: function (response) {
            if (response.success) {
                $('#notification').hide();
                if (window[callback]) {
                    window[callback](response);
                }
            } else {
                $('#notification').show();
            }
        },
        error: function (response) {
            $('#notification').show();
        }
    });
}
;

function getItemWebtube(response, containerId) {
    try {
        $('#' + containerId).html('');
        $('#' + containerId).html('<h1 class="title">' + response.header.webtube[0].name + '</h1>');

        if (response.object.length > 0) {
            $.each(response.object, function () {
                $('#' + containerId).append(showItem(this));
            });
        }
    } catch (e) {
    }
}
;

function prettyDate(dateString) {
    // old date string: 2012-10-24 15:47:04.694
    // new date string (ISO 8601): 2012-10-23T16:22:21+07:00

    if (typeof dateString == 'undefined') {
        return '';
    }

    dateString = dateString.replace(/(\d{4}-\d{2}-\d{2}) (\d{2}:\d{2}:\d{2}).\d{0,4}/gim, '$1T$2+07:00');

    var date = new Date(dateString);

    if (isNaN(date)) {
        try {
            var dateArray = /(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2})([+\-])(\d{2}):(\d{2})/.exec(dateString);

            if (dateArray[1]) {
                date = dateArray[1].split(/\D/);

                for (var i = 0, L = date.length; i < L; i++) {
                    date[i] = parseInt(date[i], 10) || 0;
                }

                date[1] -= 1;
                date = new Date(Date.UTC.apply(Date, date));

                if (!date.getDate()) {
                    date = NaN;
                } else if (dateArray[3]) {
                    var tz = (parseInt(dateArray[3], 10) * 60);
                    if (dateArray[4])
                        tz += parseInt(dateArray[4], 10);
                    if (dateArray[2] == '+')
                        tz *= -1;
                    if (tz)
                        date.setUTCMinutes(date.getUTCMinutes() + tz);
                }
            }
        } catch (e) {
            date = NaN;
        }
    }

    if (isNaN(date)) {
        return ''; //dateString incorrect
    }

    var now = new Date();
    var diff = (now.getTime() - date.getTime()) / 1000;
    var day_diff = Math.floor(diff / 86400);

    if (isNaN(day_diff) || day_diff < 0 || day_diff >= 7) {
        return 'hh:mm dd/MM/yyyy'
                .replace(/hh/, date.getHours())
                .replace(/mm/, date.getMinutes())
                .replace(/dd/, date.getDate())
                .replace(/MM/, date.getMonth() + 1)
                .replace(/yyyy/, date.getFullYear())
                ;
    }

    return day_diff == 0 && (
            diff < 60 && 'vài giây trước' ||
            diff < 120 && '1 phút trước' ||
            diff < 3600 && Math.floor(diff / 60) + ' phút trước' ||
            diff < 7200 && '1 giờ trước' ||
            diff < 86400 && Math.floor(diff / 3600) + ' giờ trước') ||
            day_diff == 1 && 'hôm qua' ||
            day_diff < 7 && day_diff + ' ngày trước' ||
            day_diff < 31 && Math.ceil(day_diff / 7) + ' tuần trước';
}


//************ HIEN THI THONG TIN CUA 1 USER POST TREN DIEN DAN ***************//
function createTipsyforElement(el, msg) {
    $(el).tipsy({trigger: 'manual', gravity: 'nw', fade: true});
    $(el).attr('original-title', msg);

    if ($(el).attr('hover') == 'over') {
        $(el).tipsy('show');
    }
    $(el).mouseout(function () {
        $(el).tipsy('hide');
    })
}

function getPropertiesOfCommunityById(objectItem) {
    if (typeof ($(objectItem).attr('hover')) != 'undefined') {
        $(objectItem).tipsy({trigger: 'hover', gravity: 'nw', fade: true});
        return;
    }
    $(objectItem).append('<img class="loadding" src="/assets/global/img/ajax_loader.gif" />');
    $(objectItem).find('.loadding').css({
        position: 'absolute',
        top: 0,
        left: 10,
        width: 24,
        height: 24
    });

    $(objectItem).css({position: 'relative'});
    $(objectItem).attr('hover', 'over');
    $(objectItem).css({
        'opacity': 0.5
    });

    $(objectItem).mouseout(function () {
        $(this).attr('hover', 'out');
    });

    if ($(this).parent().hasClass('fb-avatar')) {
        $(this).css({display: 'inline-block'});
    }

    var data = {
        communityId: $(objectItem).attr('communityId')
    };

    var ajax = $.ajax({
        url: '/service/user/getUserInfoForHover',
        type: 'post',
        dataType: 'json',
        data: data,
        success: function (response) {
            var mgs = '';
            if (response.success) {
                mgs = response.msg;
            } else {
                if (typeof response.data.authentication != 'undefined' && response.data.authentication == 1) {
                    mgs = 'Bạn phải đăng nhập để xem thông tin!';
                } else if (response.msg == '') {
                    mgs = "Không có thông tin!";
                } else {
                    mgs = 'Có lỗi trong quá trình xử lý vui lòng nhấn F5 để thư lại hoặc liên hệ tới số hotline: 1801260 để được hỗ trợ';
                }
            }

            createTipsyforElement(objectItem, mgs);
            getListCommunityByCommunity(mgs, $(objectItem).attr('communityId'));

            $(objectItem).css({'opacity': 1});
            $(objectItem).find('.loadding').remove();
        },
        timeout: 5000,
        error: function () {
            mgs = 'Có lỗi trong quá trình xử lý vui lòng nhấn F5 để thư lại hoặc liên hệ tới số hotline: 1801260 để được hỗ trợ';
            createTipsyforElement(objectItem, mgs);
            $(objectItem).css({'opacity': 1});
            $(objectItem).find('.loadding').remove();
        }
    });

}

function processAddChar(oldString, addString) {
    addString = $.trim(addString);
    if (typeof (addString) == 'undefined' || addString == "" || addString.length == null) {
        return oldString;
    }

    if (oldString == "") {
        oldString = addString;
    } else {
        oldString += ' - ' + addString;
    }
    return oldString;
}

function getListCommunityByCommunity(message, communityId) {
    if (message == "") {
        return;
    }
    $('#list-item-webtube.page-items .item a.avatar').each(function () {
        if ($(this).attr('communityid') == communityId) {
            $(this).attr('original-title', message);
            $(this).tipsy({trigger: 'hover', gravity: 'nw', fade: true});
            $(this).attr('hover', 'out');
            $(this).css({cursor: 'pointer'});
        }
    });
    $('#list-item-webtube.page-items .item .right .title a').each(function () {
        if ($(this).attr('communityid') == communityId) {
            $(this).attr('original-title', message);
            $(this).tipsy({trigger: 'hover', gravity: 'nw', fade: true});
            $(this).attr('hover', 'out');
            $(this).css({cursor: 'pointer'});
        }
    });

    $('.feedbacklist .fb-li a.homepage').each(function () {
        if ($(this).attr('communityid') == communityId) {
            $(this).attr('original-title', message);
            $(this).tipsy({trigger: 'hover', gravity: 'nw', fade: true});
            $(this).attr('hover', 'out');
            $(this).css({cursor: 'pointer'});
            if ($(this).parent().hasClass('fb-avatar')) {
                $(this).css({display: 'inline-block'});
            }
        }
    });
}

function feedbackCallback() {
    $('.feedbacklist .fb-li a.homepage').each(function () {
        if (typeof ($(this).attr('hasEventHover')) == 'undefined') {
            $(this).attr('hasEventHover', '1');
            $(this).mouseover(function () {
                getPropertiesOfCommunityById(this);
            });
            $(this).click(function () {
                checkLogin($(this).attr('communityId'));
            });
        }
    });
}

//************ HIEN THI THONG TIN CUA 1 USER POST TREN DIEN DAN ***************//

function getCountry(response) {
    try {
        if (response.header.webtube[0] && response.header.webtube[0].name == 'Tá»‰nh' && response.object[0]) {
            for (i = 0; i < response.object.length; i++) {
                if (response.object[i].itemType == 'folder.child') {
                    var opt_p = $('<option value="' + response.object[i].header + '">' + response.object[i].header + '</option>');

                    if (response.object[i].header == getURLParams('p')) {
                        opt_p.attr('selected', 'true');
                        getSettings(response.object[i].header, 'getCountry', {'limit': '64', 'type': 'folder.child'});
                    }

                    opt_p.appendTo('#province')
                }
            }
        } else if (response.header.webtube[0] && response.object[0]) {
            $('#district').html('');
            $('<option value="-1">Chá»n Quáº­n/Huyá»‡n</option>').appendTo('#district');
            for (i = 0; i < response.object.length; i++) {
                if (response.object[i].header != 'Tá»‰nh') {
                    var opt_d = $('<option value="' + response.object[i].header + '">' + response.object[i].header + '</option>');
                    if (response.object[i].header == getURLParams('d')) {
                        opt_d.attr('selected', 'true')
                    }

                    opt_d.appendTo('#district');
                }
            }
        }
    } catch (e) {
    }
}
;

function listSchool(response) {
    $('#list-item').find('.loading').remove();
    $('#list-item').find('.more').remove();
    $('#list-item').find('.no-data').remove();
    if (response.success && response.object.length > 0) {
        for (var i = 0; i < response.object.length; i++) {
            var item = $.parseJSON(response.object[i].content)
            var li = $('<li><table celspacing="0" cellpadding="0"><tr></tr></table></li>');
            if (item.avatar == null || item.avatar == '') {
                item.avatar = '/common/v1/image/avatar.png';
            }
            li.find('tr').append($('<td><img class="avatar" src="' + item.avatar + '" /></td>'));
            li.find('tr').append($('<td><span >' + item.name + '</span></td>'));
            li.attr('link', item.url)
            li.on('click', function () {
                var link = $(this).attr('link');
                location.href = link + '?p=' + $('#province').val() + '&d=' + $('#district').val() + '&k=' + $('#keyword').val();
            });

            if ($('#truelife-id').html() == item.truelifeId) {
                li.attr('class', 'active');
            }

            if (i == (limit - 1)) {
                $('#list-item').append('<li class="more"><i>Xem thêm</i></li>');
                $('#list-item .more').on('click', function () {
                    page++;
                    start = (page - 1) * (limit - 1);
                    //console.log('page:', page, '--start',start, '--limit', limit )
                    formSearchSchool(false, true);
                })

            } else {
                $('#list-item').append(li);
            }
        }
    } else {
        $('#list-item').html('<li class="no-data"> KhĂ´ng tĂ¬m tháº¥y káº¿t quáº£ nĂ o!</li>');
    }
}
;

function isEmpty(str) {
    if (str != '' && typeof (str) != 'undefined') {
        return true
    }
    return false;
}

function checkQuantriWebsite(communityId) {
    $.ajax({
        url: 'http://hub.vnedu.vn/offica/webtube/action',
        jsonp: "jsoncallback",
        dataType: 'jsonp',
        data: {
            _f: 70,
            communityId: communityId
        },
        success: function (response) {
            if (response.success && response.object.length > 0)
            {
                $('#school-detail .quantri').show();
            }
        },
        error: function () {}
    });
}

function formSearchSchool(isSubmit, isLoadMore) {
    showLoading('list-item', true);
    config = {name: "", filter: getURLParams('k'), 'start': start, 'limit': limit, 'type': 'file'}

    if (getURLParams('d') != -1) {
        config.name = getURLParams('d');
    } else {
        config.name = getURLParams('p');
    }

    if (!isSubmit && getURLParams('k') != "" && !isLoadMore) {
        $('#keyword').val(getURLParams('k'));
    }

    if ($('#province').val() != -1) {
        config.name = $('#province').val();
    }

    if ($('#district').val() != -1) {
        config.name = $('#district').val();
    }

    if ($('#keyword').val() != "") {
        config.filter = $('#keyword').val();
    } else {
        config.filter = "";
    }

    if (config.name == -1 || config.name == "") {
        config.name = $('#forder-school').val();
    }

    getListWebtube(config, 'listSchool');
}

function formSearchUserGuide(isSubmit) {
    limit = 40;
    start = (page - 1) * (limit - 1);
    showLoading('list-item', true);


    if ($('.box-search .filter li.selected').is('li')) {
        folderName = $('.box-search .filter li.selected').attr('filter');
    } else {
        folderName = $('#forder-userguide').attr('value');
    }

    config = {name: folderName, 'start': start, 'limit': limit, 'type': 'file', filter: ''}

    if (!isSubmit && getURLParams('k') != "") {
        $('#keyword').val(getURLParams('k'));
    }

    if ($('#keyword').val() != '') {
        config.filter = $('#keyword').val();
    }

    if (config.filter != '' && config.filter.length == 4 && $.isNumeric(config.filter)) {
        num_tab = Math.floor(parseInt(config.filter) / 1000);
        $('.box-search .filter li.selected').removeClass('selected');
        tab = $($('.box-search .filter li')[num_tab]);
        tab.addClass('selected');
        config.name = tab.attr('filter');
    }

    getListWebtube(config, 'listUserGuide');
}

function formSearchForum(dialog, command) {
    showLoading(dialog.find('#dialog-webtube-list').attr('id'), true);
    folderName = 'Diễn đàn';
    config = {
        name: folderName,
        'start': parseInt(dialog.find('#dialog-webtube-list').attr('start')),
        'limit': parseInt(dialog.find('#dialog-webtube-list').attr('limit')),
        'type': 'file'
    };

    if (dialog.find('input[name="key"]').val() != '') {
        config.filter = dialog.find('input[name="key"]').val();
    }

    var data = {
        _f: 31,
        start: 0,
        limit: 1000,
        filter: '',
        filterMethod: 'CONTAINS',
        ownerId: communityId
    };
    $.extend(data, config);
    $.ajax({
        url: 'http://hub.vnedu.vn/offica/webtube/action',
        jsonp: "jsoncallback",
        dataType: 'jsonp',
        data: data,
        success: function (response) {
            listForum(response, command, dialog);
        },
        error: function () {}
    });
}

function listForum(response, command, dialog) {
    var list = $('#dialog-webtube-list');
    list.find('.loading').remove();
    list.find('.no-data').remove();
    list.find('.more').remove();
    list.find('.move').removeAttr('disabled');

    if (response.success) {
        for (var i = 0; i < response.object.length; i++) {
            var item = $.parseJSON(response.object[i].content)
            var li = $('<li class="item-wtb"><table celspacing="0" cellpadding="0"><tr></tr></table></li>');
            var avatar = "";
            avatar = '<img style="width: 25px; margin-right: 10px;" src="/common/v1/image/faq.png"/>';
            li.find('tr').append($('<td align="left" width="20"><a href="' + item.url + '" target="_blank" >' + avatar + '</a></td>'));
            li.find('tr').append($('<td align="left"><a href="' + item.url + '" target="_blank" >' + item.name + '</a></td>'));
            li.find('tr').append($('<td align="right" valign="middle"><button style="margin-left: 10px;" truelifeId="' + item.truelifeId + '" class="move">Chuyển</button></td>'));
            if (i == (parseInt(list.attr('limit')) - 1)) {
                list.attr('start', parseInt(list.attr('start')) + parseInt(list.attr('limit')));
                list.append('<li class="more"><i>Xem thêm</i></li>');
                list.find('.more').on('click', function () {
                    formSearchForum(dialog, command);
                })
            } else {
                list.append(li);
            }
        }

        list.find('.move').on('click', function () {
            moveWebtubeItemUserPost(list.parent().attr('webtubeitemid'), this, command, dialog);
        });
    } else {
        list.html('<li class="no-data"><table><tr><td> Không tìm thấy kết quả nào!</td></tr></table></li>');
    }
}
;

function formSearchFAQ(isSubmit) {
    start = (page - 1) * (limit - 1);
    showLoading('list-item', true);
    folderName = $('#forder-userguide').attr('value');

    config = {name: folderName, 'start': start, 'limit': limit, 'type': 'file'};

    if (!isSubmit && getURLParams('k') != "") {
        $('#keyword').val(getURLParams('k'));
    }

    if ($('#keyword').val() != '') {
        config.filter = $('#keyword').val();
    }

    getListWebtube(config, 'listFAQ');
}

function listFAQ(response) {
    $('#list-item').find('.loading').remove();
    $('#list-item').find('.more').remove();
    $('#list-item').find('.no-data').remove();

    if (response.success) {
        for (var i = 0; i < response.object.length; i++) {
            var item = response.data[i];
            item['avatar'] = '/common/v1/image/faq.png';

            var li = $('<li><table celspacing="0" cellpadding="0"><tr></tr></table></li>');
            if (item.avatar != null && item.avatar != '') {


                li.find('tr').append($('<td><img class="avatar" src="' + item.avatar + '" /></td>'));
                li.find('tr').append($('<td><span >' + item.name + '</span></td>'));
            } else {
                li.find('tr').append($('<td collspan="2"><span >' + item.name + '</span></td>'));
            }

            if ($('#truelife-id').html() == item.truelifeId) {
                li.attr('class', 'active');
            }

            li.attr('link', item.url)
            li.attr('url', item.url)
            li.on('click', function () {
                var url = "";
                if ($('#keyword').val() != '')
                    url = '?k=' + $('#keyword').val();
                location.href = $(this).attr('url') + url;
            });

            if (i == (limit - 1)) {
                $('#list-item').append('<li class="more"><i>Xem thêm</i></li>');
                $('#list-item .more').on('click', function () {
                    page++;
                    start = (page - 1) * (limit - 1);
                    formSearchFAQ();
                })
            } else {
                $('#list-item').append(li);
            }
        }
    } else {
        $('#list-item').html('<li class="no-data"><table><tr><td> Không tìm thấy kết quả nào!</td></tr></table></li>');
    }
}
;

function listUserGuide(response) {
    $('#list-item').find('.loading').remove();
    $('#list-item').find('.more').remove();
    $('#list-item').find('.no-data').remove();
    var hsds_page = 0;
    if ($("#hsds_page").length > 0) {
        hsds_page = $("#hsds_page").text();
    }
    if (response.success && response.data.data.length > 0) {
        var data = response.data;
        for (var i = 0; i < data.data.length; i++) {
            var item = data.data[i];
            var li = $('<li><table celspacing="0" cellpadding="0"><tr></tr></table></li>');

            // if(item.avatar == '')
            item['avatar'] = '/common/v1/image/hdsd.png';
            if (item.avatar != null && item.avatar != '') {
                li.find('tr').append($('<td><img class="avatar" src="' + item.avatar + '" /></td>'));
                li.find('tr').append($('<td><span >' + item.name + '</span></td>'));
            } else {
                li.find('tr').append($('<td collspan="2"><span >' + item.name + '</span></td>'));
            }


            li.attr('link', '/huong-dan-su-dung/' + item.tab_url + '/' + item.slug);
            li.attr('url', '/huong-dan-su-dung/' + item.tab_url + '/' + item.slug);
            li.on('click', function () {
                var url = '?t=' + $('.box-search .filter li.selected').attr('filter');

                if ($('#keyword').val() != '')
                    url += '&k=' + $('#keyword').val();

                location.href = $(this).attr('url') + url;
            });
            if (hsds_page && hsds_page == item.id) {
                li.addClass('active');
            }

            if (i == (limit - 1)) {
                $('#list-item').append('<li class="more"><i>Xem thêm</i></li>');
                $('#list-item .more').on('click', function () {
                    page++;
                    start = (page - 1) * (limit - 1);
                    formSearchUserGuide();
                })
            } else {
                $('#list-item').append(li);
            }
        }
    } else {
        $('#list-item').html('<li class="no-data"><table><tr><td> Không tìm thấy kết quả nào!</td></tr></table></li>');
    }
}
;

function selectedMenu() {
    $('.menu-edu li a').each(function (index) {
        if ($(this).html() == $('#page-title').html() || $(this).html() == $('#forder-school').val() || $(this).html() == $('#forder-userguide').val() || $(this).html() == $('#folder-name').val())
            $(this).addClass('active')
    });

    if (location.pathname == '/')
        $('.menu-edu li:first-child a').addClass('active');
}

function listItemNext(start, limit) {
    $('#list-item-loading').show();
    $('#view-more').html('');
    started = start;
    requestListItemWebtube(truelifeId, limit, start, false);
}

function showItemWebtube(object) {
    if (object.itemType != 'section.user.channel' || typeof (object.creator) == 'undefined') {
        return;
    }
    var strHtml = "";

    var item = $('<div class="item" style="position: relative"></div>');

    item.attr('webtubeItemId', object.id)
            .attr('webtubeId', object.webtubeId)
            .attr('truelifeId', object.truelifeId);

    var leftItem = $('<div class="left"></div>');
    var rightItem = $('<div class="right"></div>');
    var contentRightItem = $('<div class="item-content"></div>');
    item.addClass('item-' + object.contentType);
    item.append(leftItem);
    item.append(rightItem);
    item.append('<div class="clear"></div>');
    leftItem.html('<a class="avatar" onmouseover="getPropertiesOfCommunityById(this);" onclick="checkLogin(' + object.creator.id + ')" communityId="' + object.creator.id + '"><img src="http://hub.vnedu.vn/offica/community/avatar?c=' + object.creator.id + '&amp;m=2" /></a>');

    rightItem.html('<div class="title"><a onmouseover="getPropertiesOfCommunityById(this)" onclick="checkLogin(' + object.creator.id + ')" communityId="' + object.creator.id + '">' + object.creator.name + '</a></div>');
    rightItem.find('.title').append('<span class="date"> gá»­i ' + prettyDate(object.lastModify) + '</span>');

    if (object.header) {
        rightItem.append('<div class="item-header">' + object.header + '</div>');
    }

    if (object.content) {
        switch (object.contentType) {
            case 'text':
                strHtml = replaceURL(object.content);
                break;
            case 'photo':
                if (object.content.indexOf('hub.vnedu.vn') > 0) {
                    strHtml = '<img src="' + replaceIMGURL(object.content, '1280x1280') + '" class="picture" />';
                } else {
                    strHtml = '<img src="' + object.content + '" class="picture" />';
                }
                break;
            case 'video':
                strHtml = '<div class="player" id="video-' + object.id + '"></div>';
                break;
            case 'embed':
                strHtml = '<div class="embed">' + object.content + '</div>';
                break;
            case 'audio':
                strHtml = '<div class="player" id="audio-' + object.id + '"></div>';
                break;
            case 'link':
                strHtml = '<a target="_blank" href="' + object.content + '">' + object.content + '</a>';
                break;
            case 'file':
                strHtml = 'Nhấn vào đây để download: <a target="_blank" href="' + object.content + '">' + object.content + '</a>';
                break;
        }
        contentRightItem.append(strHtml);
    }
    rightItem.append(contentRightItem);
    rightItem.append(showCommentItemWebtube(object));
    $('#list-item-webtube').append(item);
}

function configFeedback() {
    window.agentDesktop.newAction('Messenger.MESSAGE_INCOMING', {
        type: 1,
        destination: "feedback",
        content: {
            type: 'MESSAGE_PAC',
            attachment: {
                launcher: '.webtube-item-feedback-place',
                autoShow: true,
                postFile: true,
                formInit: {
                    s: 0,
                    cateTitlePlace: '#page-title',
                    cateId: 0
                },
                callback: 'feedbackCallback'
            },
            message: 'FeedBack.SHOW'
        }
    });
}

function isDouble(A, B) {
    if (A && B) {
        if (A.creatorId == B.creatorId) {
            if (A.header == B.header) {
                if (A.content && B.content) {
                    if (A.content == B.content) {
                        return true;
                    } else {
                        return false;
                    }
                } else if (!A.content && !B.content) {
                    return true
                } else {
                    return false;
                }
            } else {
                return false;
            }
        } else {
            return false;
        }
    } else if (!A && !B) {
        return true;
    } else {
        return false;
    }
}

function requestListItemWebtube(truelifeId, limit, start, isReload) {
    if (isReload) {
        $('#list-item-webtube').html("");
        start = 0;
    }
    var contentType = "";
    if ($('#content-type-filter li.selected').is('li')) {
        contentType = $('#content-type-filter .selected').attr('contentType');
    }

    var url = "https://hub.vnedu.vn/offica/webtube/action?_f=31&contentType=" + contentType + "&start=" + start + "&limit=" + limit + "&id=" + truelifeId + "&type=section.user.channel&jsoncallback=getListItemUserPost";
    YAHOO.util.Get.script(url, {
        onSuccess: function (o) {},
        onFailure: function (o) {
            $('#list-item-loading').html("Chưa có thông tin gì");
            $('#list-item-loading').attr('style', 'color: red');
        },
        onTimeout: function (o) {
            $('#list-item-loading').html("Chưa có thông tin gì");
            $('#list-item-loading').attr('style', 'color: red');
        },
        timeout: 10000
    });
}

function showCommentItemWebtube(object) {
    var strHtml = "";

    if (object.isComment == 1) {
        if (object.header != '' && object.header != null) {
            object.content = object.header;
        }

        strHtml = '<div class="tool">'
        strHtml += '<div postId="' + object.id + '" articleId="' + object.truelifeId + '" class="comment">' +
                '<div class="lead" style="display: none">' + stripTagsHTML(object.content).substring(0, 200) + '</div>' +
                '<div class="webtube-item-feedback-place" id="' + object.truelifeId + '" template="#webtube"></div>'
        strHtml += '</div>';
        strHtml += '<div class="clear"></div></div>';
    }

    return strHtml;
}

function replaceURL(url) {
    var str = [];
    var regExp = /^(\b(https?|ftp):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])$/gim;
    url = url.replace(regExp, '<a href="$1" target="_blank" title="$1">$1</a>')
    return url;
}

function getListItemUserPost(obj) {
    if (obj.success) {
        var object = obj.object;
        var length;
        if (object.length == 0) {
            $('#view-more').hide();
            $('#list-item-loading').hide();
            $('#list-item-webtube').html();
            return;
        }

        if (object.length == limit) {
            length = object.length - 1;
            isContinue = true;
        } else {
            length = object.length;
            isContinue = false;
        }

        for (i = 0; i < length; i++) {
            var temp = {};
            temp.creatorId = object[i].creatorId;
            temp.header = object[i].header;
            temp.content = object[i].content;

            if (!isDouble(temp, fristItem) && !isDouble(temp, secondItem)) {
                showItemWebtube(object[i]);
                fristItem = secondItem;
                secondItem = temp;
                onScreenItem++;
            }
        }

        head.ready(function () {
            if (window.agentDesktop && window.agentDesktop.agentFeedBack) {
                configFeedback();
            } else {
                var myTimer = setInterval(function () {
                    if (window.agentDesktop && window.agentDesktop.agentFeedBack) {
                        configFeedback();
                        window.clearInterval(myTimer);
                    }
                }, 1000);
            }
        })

        $('#list-item-loading').hide();

        if (isContinue) {
            $('#view-more').show();
            $('#view-more').html('<a id="btNext" onclick="listItemNext(' + (limit - 1 + started) + ', ' + limit + ')"></a>');
        } else {
            $('#view-more').hide();
        }

        if (!started) {
            fristCreateDate = (object[0].createDate).split(" ")[0];
        }

        if (object.length >= 2) {
            lastCreateDate = (object[object.length - 2].createDate).split(" ")[0];
        } else if (object.length > 0) {
            lastCreateDate = (object[0].createDate).split(" ")[0];
        }

        if (((new Date(fristCreateDate)) - new Date(lastCreateDate)) > 7948800000) {
            onScreenItem = limit + 1;
        }

        if (onScreenItem >= limit) {
            onScreenItem = 0;
        }
        checkPermissionsWebtube();
    } else {
        $('#view-more').hide();
        $('#list-item-loading').show();
        $('#list-item-loading').html('CĂ³ lá»—i xáº£y ra, xin thá»­ láº¡i sau');
        return;
    }

    for (i = 0; i <= obj.object.length - 1; i++) {
        if (obj.object[i].contentType == 'video') {
            setup_player({
                type: 'video',
                id: 'video-' + obj.object[i].id,
                file: obj.object[i].content,
                width: 497,
                height: 300
            });
        }
        if (obj.object[i].contentType == 'audio') {
            setup_player({
                type: 'audio',
                id: 'audio-' + obj.object[i].id,
                file: obj.object[i].content,
                width: 497,
                height: 40
            });
        }
    }
}

function getNewsFromSime() {
    var clientWidth = document.documentElement.clientWidth;
    simeWidth = clientWidth - 980;
    $('.zone-spliter-SCREEN').width(clientWidth);
    if (simeWidth >= 150) {

    } else {
        simeWidth = 150;
    }
    $('.zone-spliter-SCREEN-col-2').width(simeWidth);
    $('#P63447').width(simeWidth - 40);

    var url = "http://s1.megafun.vn/select?sort=date%20desc&q=%28%20*:*%20%29&start=0&fq=siteid:369&fq=type:*&hl.requireFieldMatch=true&fq=-cateid:(8661)%20object_type:article&rows=5&r=&wt=json&json.wrf=?";
    var P63447 = new PortletDataHandler('P63447', url, 'SEARCH', function () {
        $('#show_items_of_portlet_P63447_here .loading').hide();
        $('#show_items_of_portlet_P63447_here .item img').each(function () {
            this.src = this.src.replace('/normal/', '/original/');
            $(this).width('100%');
        })
    });
    P63447.loadPortletData();

}
;

function handlerFormSearchFAQ() {
    $('#list-item').html("");
    page = 1;
    formSearchFAQ(true);
    return false;
}

function renderMediaPlayer() {
    $('div.item .item-content div.player').each(function () {
        if (typeof $(this).attr('data-url') !== 'undefined' && $(this).attr('data-url') !== false) {
            var config = {
                type: 'video',
                id: $(this).attr('id'),
                file: $(this).attr('data-url'),
                width: 530,
                height: 300
            };
            if ($(this).attr('id').indexOf('audio-') > -1) {
                config.type = 'audio';
                config.height = '24';
            }

            setup_player(config);
        }
    })
}

var context;
var loginAction = false;
var tryLogout;
var usernameLogin = '';
var checkUsernamelogin = 1;
var onPage = "";
var communityId = 1398038;
var page = 1;
var start = 0;
var limit = 21;
var truelifeId = $('#truelife-id').html();
var friendPost = $('#friend-post').html();

$(document).ready(function () {

    renderMediaPlayer();

    selectedMenu();

    if ($('#form-search-school').is('form')) {
        getSettings('Tỉnh', 'getCountry', {'limit': '99', 'kind': 'folder'});

        $('#province').change(function () {
            $('#district').html('');
            $('<option value="-1">Chọn Quận/Huyện</option>').appendTo('#district');
            var province = this.value;
            $('#list-item').html('');
            if (province != -1) {
                $('#list-item').html('');
                getSettings(province, 'getCountry', {'limit': '64', 'type': 'folder.child'});
            }
            formSearchSchool(true, false);
        });

        $('#district').change(function () {
            $('#list-item').html('');
            formSearchSchool(true, false);
        });

        $('#form-search-school').submit(function () {
            $('#list-item').html("");
            page = 1;
            formSearchSchool(true, false);
            return false;
        })

        $('#register').on('click', function () {
            window.open('http://website.vnedu.vn/dang-ky-su-dung/thong-tin-san-pham/', '_blank');
        })

        if (getURLParams('page') != "") {
            page = getURLParams('page');
        }

        //Láº¥y táº¥t cáº£ cĂ¡c trÆ°á»ng
        var start = (page - 1) * (limit - 1);
        formSearchSchool(false, false);
    }

    if ($('#form-search-user-guide').is('form')) {
        $('#form-search-user-guide').submit(function () {
            $('#list-item').html("");
            page = 1;
            formSearchUserGuide(true);
            return false;
        })

        if (getURLParams('page') != "") {
            page = getURLParams('page');
        }

        if (getURLParams('t') != "") {
            $('.box-search .filter li').each(function () {
                if ($(this).attr('filter') == getURLParams('t')) {
                    $('.box-search .filter li').removeClass("selected");
                    $(this).addClass("selected");
                }
            })
        }

        formSearchUserGuide(false);

        if ($('#tlf-is-comment').html() == 1) {
            $('.guicauhoi').on('click', function () {
                $('.feedback-place textarea#content').focus();
            });
            $('.guicauhoi').show();
        }

        $('.box-search .filter li').each(function (index) {
            $(this).on('click', function () {
                $('#list-item').html("");
                page = 1;
                $('.box-search .filter li').removeClass("selected");
                $(this).addClass("selected");
                formSearchUserGuide(true);
            })
        })
    }

    if ($('#form-search-faq').is('form')) {
        $('#form-search-faq').submit(function () {
            handlerFormSearchFAQ();
            return false;
        })

        $("input#keyword").live("keyup", function (e) {
            // Set Timeout
            clearTimeout($.data(this, 'timer'));

            // Set Search String
            var filter = $(this).val();

            // Do Search
            $(this).data('timer', setTimeout(handlerFormSearchFAQ, 500));
        });

        if (getURLParams('page') != "") {
            page = getURLParams('page');
        }

        if (getURLParams('t') != "") {
            $('.box-search .filter li').each(function () {
                if ($(this).attr('filter') == getURLParams('t')) {
                    $('.box-search .filter li').removeClass("selected");
                    $(this).addClass("selected");
                }
            })
        }

        $(function () {
            friendPost = $('#friend-post').html();
            if (friendPost == 1) {
                $('.guicauhoi').on('click', function () {
                    $('.content-section textarea').focus();
                });
                $('.guicauhoi').show();
            }
        });

        formSearchFAQ(false);
    }

    if ($('#post-holder').is('div') && friendPost == 1) {
        started = 0;
        isContinue = false;
        onScreenItem = 0;
        fristItem = {};
        secondItem = {};
        fristCreateDate = "";
        lastCreateDate = "";
        ownerInfo = {};
    }



    var solienlac = new soLienLac({
        communityId: $('#community-id').html()
    });

    if ($('#form-search-solienlac').is('form')) {
        solienlac.searchPupil();
    }

    if ($('#form-search-solienlac-confirm').is('form')) {
        if (!$('#ad_sll_form').is('div')) {
            $('#bangdiem ').before('<div id="ad_sll_form" class="row"></div>');
        }

        solienlac.confirmPhoneNumberPupil();
    }
});


var soLienLac = function (config) {
    var self = this;
    $.extend(this, config);
    this.domain = "https://hocbadientu.vnedu.vn/sllservices/index.php";
    this.domainWeb = "";
    this.mgsRequestFalse = 'Bạn vui lòng nhấn F5 và thực hiện lại sau ít phút hoặc liên hệ tới số hotline: 18001260';
    this.msgForgetPass = '<p>Quên mật khẩu: nhắn tin gửi 8099 (1000VNĐ/SMS) với nội dung: <b>VNEDU   MK</b> để lấy lại mật khẩu</p>';
    this.msgChangePass = '<p>Đổi mật khẩu: nhắn tin gửi 8099 (1000VNĐ/SMS) với nội dung: <b>VNEDU   MK</b> [Mật khẩu mới]</p>';
    this.msgRegister = '<p>Nếu bạn chưa đăng ký sử dụng VNEDU cho <b>@STUDENT_NAME@</b>, bạn vui lòng tham khảo theo hướng dẫn tại đường link sau: <a href="/so-lien-lac">Sổ liên lạc<a/> để đăng ký dịch vụ Sổ liên lạc điện tử.</p>';
	if (getURLParams('tinh_id')) {
		$('#form-search-solienlac').find("select.tinh option").each(function() {
			if($(this).val() == getURLParams('tinh_id')) {
				$(this).prop("selected", true);
			}
		});
	}
	if(location.host.indexOf('vnedux.vn') > 0) {
        this.domain = "http://hocbadientu.vnedux.vn/index.php";
        this.domainWeb = "http://hocbadientu.vnedux.vn/";
    }

    this.searchPupil = function () {

        if ($('#form-search-solienlac').is('form')) {
            $('#form-search-solienlac').parent().attr('style', 'border-bottom: none;overflow: hidden;');
            
            $('#form-search-solienlac').find("select.tinh").change(function(){
                if($(this).find("select").val() == "0") {
                    $(this).parent().addClass('has-error');
                }else {
                    $(this).parent().removeClass('has-error');
                }
            })
            
            $('#form-search-solienlac').submit(function () {
                var res = grecaptcha.getResponse();
                if (res == "" || res == undefined || res.length == 0) {
                    $('#g-recaptcha-error').html("Vui lòng nhập xác thực");
                    return false;
                }else {
                    $('#g-recaptcha-error').html("");
                }

                if ($('#form-search-solienlac').find('#keyword').val() == "") {
                    $('#form-search-solienlac').find('#keyword').focus();
                    return false;
                }
                if($(this).find("select").val() == "0") {
                    $('#form-search-solienlac').find("select.tinh").parent().addClass('has-error');
                    return false;
                } 

                showLoading('sll-list-item', false);
                self.handlerFormSearchSoLienLac();
                grecaptcha.reset(grecaptchaId);
                return false;
            })
        }

        if (getURLParams('key')) {
            showLoading('sll-list-item', false);
            $('#form-search-solienlac').find('#keyword').val(getURLParams('key'));
			$('#form-search-solienlac').find('select.tinh').val(getURLParams('tinh_id'));
            this.handlerFormSearchSoLienLac();
        }
        if (getURLParams('mahs')) {
            this.getschoolYear();
        }

    }

    this.getschoolYear = function () {
        $.ajax({
            url: this.domain ,
            jsonp: "callback",
            dataType: 'jsonp',
            data: {
                call: 'solienlac.getDSNamhoc',
                mahocsinh: getURLParams('mahs'),
				tinh_id: $('#form-search-solienlac').find('select.tinh').val()
            },
            success: function (response) {
                for (var i = 0; i < response.length; i++) {
                    $('#schoolYear').append('<option value="' + response[i].nam_hoc + '">' + response[i].nam_hoc_text + '</option>')
                }

                self.getschoolDotDiem();
            },
            timeout: 500000,
            error: function (e) {
                $('#bangdiem').html(self.notice(self.mgsRequestFalse));
            }
        });
    }

    this.getschoolDotDiem = function () {
        $.ajax({
            url: this.domain ,
            jsonp: "callback",
            dataType: 'jsonp',
            data: {
                call: 'solienlac.getDSDotDiem',
                nam_hoc: $('#schoolYear').val(),
                tinh_id: $('#form-search-solienlac').find('select.tinh').val(),
                mahocsinh: getURLParams('mahs')
            },
            success: function (response) {
                $('#schoolDotDiem').append('<option value="0">Tất cả</option>')
                for (var i = 0; i < response.length; i++) {
                    $('#schoolDotDiem').append('<option value="' + response[i].id + '">' + response[i].ten_dd + '</option>')
                }

                $('#schoolDotDiem').parents('tr').show();
            },
            timeout: 500000,
            error: function (e) {
                $('#bangdiem').html(self.notice(self.mgsRequestFalse));
            }
        });
    }

    this.isUndefined = function (v) {
        if (typeof (v) == 'undefined') {
            return true;
        }

        return false;
    }

    this.isPhone = function (phone_number) {
        if ($.isNumeric(phone_number) && phone_number.length >= 10 && phone_number.length <= 11 && (phone_number.indexOf('01') == 0 || phone_number.indexOf('09') == 0)) {
            return true;
        } else {
            return false;
        }
    };

    this.handlerFormSearchSoLienLac = function () {
        $('#form-search-solienlac #submit').prop("disabled", true);
        $.ajax({
            url: this.domain ,
            jsonp: "callback",
            dataType: 'jsonp',
            data: {
                call: 'solienlac.search',
                search: $('#form-search-solienlac').find('#keyword').val(),
                tinh_id: $('#form-search-solienlac').find('.tinh').val()
            },
            success: function (response) {
                $('#form-search-solienlac #submit').prop("disabled", false);
                $('#sll-list-item').find('.loading').remove();
                if (response.length > 0)
                {
                    var list = response;
                    for (var i = 0; i < list.length; i++) {
                        var item = list[i];
                        var li = $('<li></li>');
                        if (item.ma_hoc_sinh == getURLParams('mahs')) {
                            li.addClass('active');
                        }

                        var url = "/so-lien-lac/tra-cuu-diem/index.php?";

                        url += 'mahs=' + item.ma_hoc_sinh
                        url += '&fullname=' + item.full_name.replace('(', "").replace(')', "")
                        url += '&key=' + $('#form-search-solienlac').find('#keyword').val();
                        url += '&tinh_id=' + $('#form-search-solienlac').find('.tinh').val();
                        url += '&cap=' + item.cap;

                        if (!self.isUndefined(item.full_name) && item.full_name != '') {
                            li.append($('<a data-href="' + url + '" data-locksll="' + item.lock_sll_net + '" style="cursor: pointer;"><h3>' + item.full_name + '</h3><div class="info"></div></a>'));
                        }

                        if (!self.isUndefined(item.ten_lop) && item.ten_lop != '') {
                            li.find('div.info').append('<span class="ten-lop">' + item.ten_lop + ',</span>');
                        }
                        if (!self.isUndefined(item.ten_truong) && item.ten_truong != '') {
                            li.find('div.info').append('<span class="ten-truong">' + item.ten_truong + '</span>');
                        }
                        if (!self.isUndefined(item.ten_huyen) && item.ten_huyen != '' && !self.isUndefined(item.ten_tinh) && item.ten_tinh != '') {
                            li.find('div.info').append(' - <span class="ten_huyen">' + item.ten_huyen + ' - ' + item.ten_tinh + '</span>');
                        }

                        $('#sll-list-item').append(li);
                    }
                    $('#sll-list-item li a').each(function () {
                        $(this).on('click', function () {
                            if ($(this).data('locksll') == 1) {
                                location.href = self.domainWeb + $(this).data('href')
                            } else {
                                alert('Hiện tại trường đang khóa tra cứu SLL, vui lòng liên hệ với Quản trị của nhà trường để biết thêm chi tiết.')
                            }
                        })
                    })
                } else {
                    $('#sll-list-item').html('<li class="no-data"><table><tr><td> Không tìm thấy kết quả nào!</td></tr></table></li>');
                }
            },
            timeout: 500000,
            error: function (e) {
                $('#sll-list-item').html(self.mgsRequestFalse);
            }
        });
    }

    this.confirmPhoneNumberPupil = function () {

        if ($('#form-search-solienlac-confirm').is('form')) {
            if (!getURLParams('mahs')) {
                if (location.host == 'tracuu.vnedu.vn')
                    location.href = '/so-lien-lac';
            }

            var formConfirm = $('#form-search-solienlac-confirm');

            if (getURLParams('cap') == 10) {
                formConfirm.hide();
                $('#bangdiem').html(self.notice("<span style='font-size: 15px;'>Chức năng tra cứu cho SLLĐT dành cho <b>cấp tiểu học</b> đang được nâng cấp</span>"));

            } else {
                this.msgRegister = this.msgRegister.replace('@STUDENT_MAHS@', getURLParams('mahs'))
                        .replace('@STUDENT_NAME@', getURLParams('fullname'));

                var msg = '<div style="text-align: left; color: #000; line-height: 16px;">' + self.msgRegister + '</div>';
                //formConfirm.find('.sll-register').attr('original-title', msg);
                //formConfirm.find('.sll-register').tipsy({trigger: 'hover', html: true, gravity: 'nw', fade: true});

                var msg = '<p style="margin-bottom: 5px;font-weight: bold;">Mật khẩu mặc định chính là số điện thoại đã đăng ký Sổ liên lạc điện tử của bạn.</p>';
                msg += this.msgForgetPass;
                msg += this.msgChangePass;
                msg = '<div style="text-align: left; color: #000;">' + msg + '</div>';

                formConfirm.find('#password').attr('original-title', msg);
                formConfirm.find('#password').tipsy({trigger: 'focus', html: true, gravity: 'nw', fade: true});
                formConfirm.find('#password').focus();

                formConfirm.find('.info').html("Để tra cứu điểm của học sinh <b>" + stripTagsHTML(getURLParams('fullname')) + "</b> xin vui lòng nhập mật khẩu để xác nhận thông tin!")


                formConfirm.submit(function () {
                    if (formConfirm.find('#password').val() == "") {
                        self.message(formConfirm.find('span.error'), 'Bạn chưa nhập mật khẩu!');
                        formConfirm.find('#password').focus();
                        return false;
                    }
                    showLoading('bangdiem', false);
                    self.handlerConfirmPhoneNumberPupil(formConfirm);
                    return false;
                })
            }
        }
    }

    this.message = function (obj, msg, time) {
        if (this.isUndefined(time)) {
            time = 2000;
        }
        if (this.isUndefined(obj)) {
            return;
        }
        obj.html(msg);
        obj.show();
        setTimeout(function () {
            obj.hide();
        }, time)
    }

    this.handlerConfirmPhoneNumberPupil = function (form) {
        $('#form-search-solienlac-confirm #submit').prop("disabled", true);
        $.ajax({
            url: this.domain ,
            jsonp: "callback",
            dataType: 'jsonp',
            data: {
                call: 'solienlac.checkSll',
                mahocsinh: getURLParams('mahs'),
                tinh_id: $('#form-search-solienlac').find('select.tinh').val(),
                password: form.find('#password').val(),
                namhoc: form.find('#schoolYear').val(),
                dot_diem_id: form.find('#schoolDotDiem').val()
            },
            success: function (response) {
                $('#form-search-solienlac-confirm #submit').prop("disabled", false);
                if (response.success) {
                    self.getPointOfPupilById(form);
                } else {
                    switch (response.msg) {
                        case 'Mật khẩu của bạn không đúng.':
                            $('#bangdiem').html(self.notice(self.msgForgetPass));
                            break;
                        case 'Mật khẩu của bạn không đúng.':
                            $('#bangdiem').html(self.notice(self.msgChangePass));
                            break;
                        case 'Nhà trường đang tạm khóa xem sổ liên lạc điện tử.':
                        case 'Học sinh này chưa đăng ký sử dụng DV SLL.':
                            $('#bangdiem').html(self.notice(self.msgRegister));
                            break;
                        default:
                            $('#bangdiem').html("");
                    }
                    self.message(form.find('span.error'), response.msg, 5000);
                }
            },
            timeout: 500000,
            error: function (e) {
                $('#bangdiem').html(self.notice(self.mgsRequestFalse));
            }
        });

    }

    this.getPointOfPupilById = function (form) {
        $.ajax({
            url: this.domain ,
            jsonp: "callback",
            dataType: 'jsonp',
            data: {
                call: 'solienlac.getSodiem',
                mahocsinh: getURLParams('mahs'),
                key: 'd33e425220d1f1184a9fb9a477055fd6',
                namhoc: form.find('#schoolYear').val(),
                tinh_id: $('#form-search-solienlac').find('select.tinh').val(),
                dot_diem_id: form.find('#schoolDotDiem').val()
            },
            success: function (response) {
                if (getURLParams('cap') == 1) {
                    if (typeof (response.length) != 'undefined' && response.length > 0) {
                        self.processDataCap1(response);
                    } else {
                        if (response.tinh_id == 57) {
                            self.processDataVNENThanhHoa(response);
                        } else {
                            self.processDataVNEN(response);
                        }

                    }
                } else {
                    if (typeof (response.lop) != 'undefined' && typeof (response.lop.mam_non) != 'undefined' && response.lop.mam_non == 1) {
                        self.renderMamNon(response.data)
                        self.renderSMS(response.sms);
                    } else if (typeof (response.diem) != 'undefined' && response.diem.length > 0) {
                        self.processDataCap23(response.diem);
                        self.renderDiemDanh(response.diemdanh);
                        self.renderNeNep(response.nenep);
                        self.renderThuPhi(response.thuphi);
                        self.renderSMS(response.sms);
                        self.renderTKB(response.tkb);
                    } else {
                        self.processDataCap23(response);
                    }
                }

            },
            timeout: 500000,
            error: function (e) {
                $('#bangdiem').html(self.notice(self.mgsRequestFalse));
            }
        });
    }

    this.renderDiemDanh = function (data) {
        if (typeof (data) == 'object' && data.length > 0) {
            var html = "<div>Điểm danh</div><table border='0' celspacing='0' celpadding='4'><tr class='tophead'><th>Nghỉ có phép</th><th>Nghỉ không phép</th><th>Bỏ tiết</th></tr><tr>";
            for (var i = 0; i < data.length; i++) {
                html += "<td>" + data[i] + "</td>";
            }
            html += "</tr></table>";
            $('#bangdiem').append(html);
        }
    }

    this.renderNeNep = function (data) {
        if (typeof (data) == 'object' && data.length > 0) {
            var html = "<div>Vi phạm nề nếp</div><table border='0' celspacing='0' celpadding='4'><tr class='tophead'><th>Ngày vi phạm</th><th>Loại vi phạm</th></tr>";
            for (var i = 0; i < data.length; i++) {
                html += "<tr><td>" + data[i].ngay_vp + "</td>";
                html += "<td>" + data[i].ten + "</td></tr>";
            }
            html += "</table>";
            $('#bangdiem').append(html);
        }
    }

    this.formatMoney = function (val) {
        val += '';
        var x = val.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    }

    this.renderThuPhi = function (data) {
        if (typeof (data) == 'object' && data.length > 0) {
            var html = "<div>Danh sách các khoản thu phí</div><table border='0' celspacing='0' celpadding='4'><tr class='tophead'><th>Tên dịch vụ</th><th>Mức phí</th></tr>";
            for (var i = 0; i < data.length; i++) {
                html += "<tr><td>" + data[i].ten_dich_vu + "</td>";
                html += "<td>" + this.formatMoney(data[i].muc_phi) + " VNĐ</td></tr>";
            }
            html += "</table>";
            $('#bangdiem').append(html);
        }
    }

    this.renderSMS = function (data) {
        if (typeof (data) == 'object' && data.length > 0) {
            var html = "<div>Danh sách tin đã gửi</div><table border='0' celspacing='0' celpadding='4'><tr class='tophead'><th>Ngày gửi</th><th>Nội dung</th></tr>";
            for (var i = 0; i < data.length; i++) {
                html += "<tr><td>" + data[i].time_stamp + "</td>";
                html += "<td>" + data[i].content + "</td></tr>";
            }
            html += "</table>";
            $('#bangdiem').append(html);
        }
    }

    this.renderTKB = function (data) {
        if (typeof (data) == 'string' && data.length > 0) {
            var html = "<div>Thời khóa biểu</div>" + data;
            $('#bangdiem').append(html);
        }
    }


    this.renderMamNon = function (data) {
        if (typeof (data) == 'object' && data.length > 0) {
            var chieucao = [];
            var cannang = [];
            var html = '<div style="font-size:28px;margin-bottom: 10px; color: #333;">Thông tin theo dõi phát triển của trẻ</div>';
            html += '<table border="0" celspacing="0" celpadding="6">' +
                    '<tr class="tophead">' +
                    '	<th>Tháng</th>' +
                    '	<th><b>Năm tuổi</b></th>' +
                    '	<th><b>Tháng tuổi</b></th>' +
                    '	<th><b>Chiều cao<br>(cm)</b></th>' +
                    '	<th><b>Loại<br>chiều cao</b></th>' +
                    '	<th><b>Cân nặng<br>(kg)</b></th>' +
                    '	<th><b>Loại<br>cân nặng</b></th>' +
                    '	<th><b>Nhận xét</b></th>' +
                    '</tr>';

            for ($i = 0; $i < data.length; $i++) {
                var item = data[$i];
                var cao = parseInt(item.chieu_cao);
                if (isNaN(cao)) {
                    cao = 0;
                }

                var nang = parseInt(item.can_nang);
                if (isNaN(nang)) {
                    nang = 0;
                }
                html += '<tr>' +
                        '<td style="font-weight: bold; text-align: center">' + item.thang + '</td>' +
                        '<td align="center">' + item.nam_tuoi + '</td>' +
                        '<td align="center">' + item.thang_tuoi + '</td>' +
                        '<td align="center">' + item.chieu_cao + '</td>' +
                        '<td>' + item.loai_chieu_cao + '</td>' +
                        '<td align="center">' + item.can_nang + '</td>' +
                        '<td>' + item.loai_can_nang + '</td>' +
                        '<td>' + item.nhan_xet + '</td>' +
                        '</tr>';

                chieucao.push({y: cao, 'label': 'Tháng ' + item.thang});
                cannang.push({y: nang, 'label': 'Tháng ' + item.thang});
            }

            html += '</table>';
            $('#bangdiem').html(html);


            $('#bangdiem').append('<div id="chartContainerChieuCao" style="height: 400px; width: 100%;"></div>');

            head.js('/common/v1/js/canvasjs.min.js');
            head.ready(function () {
                var chartChieuCao = new CanvasJS.Chart("chartContainerChieuCao", {
                    title: {
                        text: "Biểu đồ phát triển chiều cao"
                    },
                    axisX: {
                        interval: 1
                    },
                    data: [{
                            type: "line",
                            dataPoints: chieucao
                        }]
                });
                chartChieuCao.render();

                $('#bangdiem').append('<div id="chartContainerCanNang" style="height: 400px; width: 100%;"></div>');

                var chartCanNang = new CanvasJS.Chart("chartContainerCanNang", {
                    title: {
                        text: "Biểu đồ phát triển cân nặng"
                    },
                    axisX: {
                        interval: 1
                    },
                    data: [{
                            type: "line",
                            dataPoints: cannang
                        }]
                });
                chartCanNang.render();
            });

        }
    }

    this.notice = function (msg) {
        return '<span class="notice">' + msg + '</span>';
    }

    this.processDataVNENThanhHoa = function (list) {

        var table1 = $('<table border="0" celspacing="0" celpadding="6"></table>');
        var table2 = $('<table border="0" celspacing="0" celpadding="6"></table>');
        var table3 = '';
        var table4 = '';

        table1.append('<tr class="tophead" style="border-bottom: 1px solid #96C4DE;"><th colspan="6" style="padding: 3px;">Nhận xét thường xuyên kỳ I</th></tr>');
        table2.append('<tr class="tophead" style="border-bottom: 1px solid #96C4DE;"><th colspan="6" style="padding: 3px;">Nhận xét thường xuyên kỳ II</th></tr>');
        var tr = $('<tr class="tophead"></tr>');
        tr.append('<th width="80">Môn học</th>');
        tr.append('<th width="40">Tháng</th>');
        tr.append('<th width="20">Đợt</th>');
        tr.append('<th  width="100">Năng lực</th>');
        tr.append('<th  width="100">Phẩm chất</th>');
        tr.append('<th>Nhận xét</th>');
        table1.append(tr);
        if (typeof (list.hkI) != 'undefined' && typeof (list.hkI.nxtx) != 'undefined' && (list.hkI.nxtx).length > 0) {
            for (var i = 0; i < list.hkI.nxtx.length; i++) {
                table1.append(self.processNhanXetCap1VNENThanhHoa(list.hkI.nxtx[i], 1))
            }
        }
        if (typeof (list.hkI) != 'undefined' && typeof (list.hkI.ne_nep) != 'undefined' && (list.hkI.ne_nep).length > 0) {
            table3 += (self.processPointNeNep(list.hkI.ne_nep, 'I'));
        }
        if (typeof (list.hkII) != 'undefined' && typeof (list.hkII.ne_nep) != 'undefined' && (list.hkII.ne_nep).length > 0) {
            table4 += (self.processPointNeNep(list.hkII.ne_nep, 'II'));
        }

        if (typeof (list.hkI) != 'undefined' && typeof (list.hkI.diem_danh) != 'undefined' && (list.hkI.diem_danh).length > 0) {
            table3 += (self.processPointDiemDanh(list.hkI.diem_danh, 'I'));
        }
        if (typeof (list.hkII) != 'undefined' && typeof (list.hkII.diem_danh) != 'undefined' && (list.hkII.diem_danh).length > 0) {
            table4 += (self.processPointDiemDanh(list.hkII.diem_danh, 'II'));
        }
        var tr = $('<tr class="tophead"></tr>');
        tr.append('<th width="80">Môn học</th>');
        tr.append('<th width="40">Thawng</th>');
        tr.append('<th width="20">Äá»£t</th>');
        tr.append('<th width="100">Năng lực</th>');
        tr.append('<th width="100">Phẩm chất</th>');
        tr.append('<th>Nhận xét</th>');
        table2.append(tr);
        for (var i = 0; i < list.hkI.nxtx.length; i++) {
            table2.append(self.processNhanXetCap1VNENThanhHoa(list.hkII.nxtx[i], 2))
        }

        $('#bangdiem').html(table1);
        $('#bangdiem').append(table3);

        $('#bangdiem').append(this.processPointCap1VNEN(list.hkI.ck, 'I'));
        $('#bangdiem').append(table2);
        $('#bangdiem').append(table4);
        $('#bangdiem').append(this.processPointCap1VNEN(list.hkII.ck, 'II'));
    }

    this.processDataVNEN = function (list) {
        var table1 = $('<table border="0" celspacing="0" celpadding="4"></table>');
        var table2 = $('<table border="0" celspacing="0" celpadding="4"></table>');
        var table3 = '';
        var table4 = '';
        if (typeof (list) == 'undefined') {
            $('#bangdiem').html("Không tìm thấy thông tin.");
            return;
        }
        if (typeof (list.hkI) != 'undefined' && typeof (list.hkI.so_theo_doi_chat_luong) != 'undefined') {
            var tr = $('<tr class="tophead"></tr>');
            tr.append('<th width="50">Tháng</th>');
            tr.append('<th width="100">Môn/Giáo viên</th>');
            tr.append('<th width="80">Năng lực</th>');
            tr.append('<th width="80">Phẩm chất</th>');
            tr.append('<th>Nhận xét</th>');

            table1.append(tr);

            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_theo_doi_chat_luong.t1.list, 'Tháng 1'));
            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_theo_doi_chat_luong.t2.list, 'Tháng 2'));
            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_theo_doi_chat_luong.t3.list, 'Tháng 3'));
            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_theo_doi_chat_luong.t4.list, 'Tháng 4'));
            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_theo_doi_chat_luong.t5.list, 'Tháng 5'));
            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_theo_doi_chat_luong.t11.list, 'Cuối kỳ 1'));
        }

        if (typeof (list.hkI) != 'undefined' && typeof (list.hkI.so_nhan_xet) != 'undefined') {
            var tr = $('<tr class="tophead"></tr>');
            tr.append('<th width="50">Tháng</th>');
            tr.append('<th width="100">Môn học</th>');
            tr.append('<th width="80">Năng lực</th>');
            tr.append('<th width="80">Phẩm chất</th>');
            tr.append('<th>Nhận xét</th>');
            table1.append(tr);

           /* table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_nhan_xet.t1.list, 'Tháng 1'));
            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_nhan_xet.t2.list, 'Tháng 2'));
            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_nhan_xet.t3.list, 'Tháng 3'));
            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_nhan_xet.t4.list, 'Tháng 4'));
            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_nhan_xet.t5.list, 'Tháng 5'));*/
            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_nhan_xet.gk1.list, 'Giữa kỳ 1'));
            table1.append(self.processPointNhanXetCap1VNEN(list.hkI.so_nhan_xet.ck1.list, 'Cuối kỳ 1'));
        }

        if (typeof (list.hkI) != 'undefined' && typeof (list.hkI.ne_nep) != 'undefined' && (list.hkI.ne_nep).length > 0) {
            table3 += (self.processPointNeNep(list.hkI.ne_nep, 'I'));
        }
        if (typeof (list.hkII) != 'undefined' && typeof (list.hkII.ne_nep) != 'undefined' && (list.hkII.ne_nep).length > 0) {
            table4 += (self.processPointNeNep(list.hkII.ne_nep, 'II'));
        }

        if (typeof (list.hkI) != 'undefined' && typeof (list.hkI.diem_danh) != 'undefined' && (list.hkI.diem_danh).length > 0) {
            table3 += (self.processPointDiemDanh(list.hkI.diem_danh, 'I'));
        }
        if (typeof (list.hkII) != 'undefined' && typeof (list.hkII.diem_danh) != 'undefined' && (list.hkII.diem_danh).length > 0) {
            table4 += (self.processPointDiemDanh(list.hkII.diem_danh, 'II'));
        }

        if (typeof (list.hkII) != 'undefined' && typeof (list.hkII.so_theo_doi_chat_luong) != 'undefined') {
            var tr = $('<tr class="tophead"></tr>');
            tr.append('<th width="50">Tháng</th>');
            tr.append('<th width="100">Môn học</th>');
            tr.append('<th width="80">Năng lực</th>');
            tr.append('<th width="80">Phẩm chất</th>');
            tr.append('<th>Nhận xét</th>');
            table2.append(tr);
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_theo_doi_chat_luong.t6.list, 'Tháng 6'));
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_theo_doi_chat_luong.t7.list, 'Tháng 7'));
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_theo_doi_chat_luong.t8.list, 'Tháng 8'));
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_theo_doi_chat_luong.t9.list, 'Tháng 9'));
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_theo_doi_chat_luong.t10.list, 'Tháng 10'));
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_theo_doi_chat_luong.t12.list, 'Cuối kỳ 2'));
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_theo_doi_chat_luong.t12.list, 'Cuối kỳ 2'));
        }

        if (typeof (list.hkII) != 'undefined' && typeof (list.hkII.so_nhan_xet) != 'undefined') {
            var tr = $('<tr class="tophead"></tr>');
            tr.append('<th width="50">Tháng</th>');
            tr.append('<th width="100">Giáo viên</th>');
            tr.append('<th width="80">Năng lực</th>');
            tr.append('<th width="80">Phẩm chất</th>');
            tr.append('<th>Nhận xét</th>');
            table2.append(tr);
           /* table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_nhan_xet.t6.list, 'Tháng 6'));
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_nhan_xet.t7.list, 'Tháng 7'));
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_nhan_xet.t8.list, 'Tháng 8'));
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_nhan_xet.t9.list, 'Tháng 9'));
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_nhan_xet.t10.list, 'Tháng 10'));*/
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_nhan_xet.gk2.list, 'Giữa kỳ 2'));
            table2.append(self.processPointNhanXetCap1VNEN(list.hkII.so_nhan_xet.ck2.list, 'Cuối kỳ 2'));
        }

        $('#bangdiem').html(table1);
        $('#bangdiem').append(table3);
        if (typeof (list.hkI) != 'undefined') {
            $('#bangdiem').append(this.processPointCap1VNEN(list.hkI.tong_ket, 'I'));
        }
        $('#bangdiem').append(table2);
        $('#bangdiem').append(table4);

        if (typeof (list.hkI) != 'undefined') {
            $('#bangdiem').append(this.processPointCap1VNEN(list.hkII.tong_ket, 'II'));
        }
    }

    this.processDataCap1 = function (list) {

        $('#bangdiem').html(self.notice(self.msgChangePass));
        for (var i = 0; i < list.length; i++) {
            var item = list[i];
            var table = $('<table border="0" celspacing="0" celpadding="4"></table>');
            var tr = $('<tr class="tophead"></tr>');
            tr.append('<th>Môn tính điểm</th>');
            /*tr.append('<th>Tháng thứ 1</th>');
            tr.append('<th>Tháng thứ 2</th>');
            tr.append('<th>Tháng thứ 3</th>');
            tr.append('<th>Tháng thứ 4</th>');
            tr.append('<th>Tháng thứ 5</th>');*/
            tr.append('<th>Giữa kỳ</th>');
            tr.append('<th>Cuối kỳ</th>');
            tr.append('<th width="50">Học lực</th>');
            table.append(tr);
            for (var j = 0; j < item.mon_hoc.tinh_diem.length; j++) {
                table.append(self.processPointCap1(item.mon_hoc.tinh_diem[j], true, item.hoc_ky));
            }

            var tr = $('<tr class="tophead"></tr>');
            tr.append('<th>Môn nhận xét</th>');
            tr.append('<th>Nhận xét 1</th>');
            tr.append('<th>Nhận xét 2</th>');
            tr.append('<th>Nhận xét 3</th>');
            tr.append('<th>Nhận xét 4</th>');
            tr.append('<th>Nhận xét 5</th>');
            tr.append('<th></th>');
            tr.append('<th></th>');
            tr.append('<th width="50">Học lực</th>');
            table.append(tr);

            for (var j = 0; j < item.mon_hoc.nhan_xet.length; j++) {
                table.append(self.processPointCap1(item.mon_hoc.nhan_xet[j], false));
            }
            hoc_ky_display = item.hoc_ky;
            if (item.hoc_ky == 0) {
                hoc_ky_display = 'cả năm';
            }
            $('#bangdiem').append('<div>Kết quả học tập của <b>' + getURLParams('fullname') + '</b> trong học kỳ ' + hoc_ky_display + '</div>');
            $('#bangdiem').append(table);
        }
    }

    this.processDataCap23 = function (list) {
        $('#bangdiem').html(self.notice(self.msgChangePass));

        for (var i = 0; i < list.length; i++) {
            var item = list[i];
            hoc_ky_display = item.hoc_ky;
            if (item.hoc_ky == 0) {
                hoc_ky_display = 'cả năm';
            }
            var table = $('<table border="0" celspacing="0" celpadding="4"></table>');
            var tr = $('<tr class="tophead"></tr>');
            tr.append('<th  width="120">Môn học</th>');
            if (item.hoc_ky != 0) {
                if($('#schoolYear').val() >= 2020) {
                    tr.append('<th>Điểm ĐG thường xuyên</th>');
                    tr.append('<th>Giữa kỳ</th>');
                    tr.append('<th width="60">Cuối kỳ</th>');
                }else{
                    tr.append('<th>Điểm miệng</th>');
                    tr.append('<th>Điểm 15 phút</th>');
                    tr.append('<th>Điểm 1 tiết</th>');
                    tr.append('<th width="50">Học kỳ</th>');
                }
            }
            tr.append('<th width="50">TBM</th>');
            table.append(tr);
            for (var j = 0; j < item.mon_hoc.length; j++) {
                if (item.mon_hoc[j].loai_nhap_diem == 1)
                    table.append(self.processPoint(item.mon_hoc[j], item.hoc_ky));
            }


            if (item.hoc_ky != 0 && item.lop_type == 3) {
                var table_vnen = $('<table border="0" celspacing="0" celpadding="4"></table>');
                var tr_head_vnen = $('<tr class="tophead"></tr>');
                tr_head_vnen.append('<th>Môn học</th>');

                tr_head_vnen.append('<th>Nhận xét</th>');
                tr_head_vnen.append('<th>Năng lực</th>');
                tr_head_vnen.append('<th>Phẩm chất</th>');
                tr_head_vnen.append('<th>KTGK</th>');
                tr_head_vnen.append('<th>KTHK</th>');
                tr_head_vnen.append('<th>TBM</th>');
                tr_head_vnen.append('<th>XLCK</th>');


                table_vnen.append(tr_head_vnen);

                for (var j = 0; j < item.mon_hoc.length; j++) {
                    if (item.mon_hoc[j].loai_nhap_diem == 2) {
                        var tr_vnen = $('<tr></tr>');
                        tr_vnen.append('<td>' + item.mon_hoc[j].ten_mon_hoc + '</td>');
                        tr_vnen.append('<td>' + item.mon_hoc[j].nhan_xet + '</td>');
                        tr_vnen.append('<td>' + item.mon_hoc[j].nang_luc + '</td>');
                        tr_vnen.append('<td>' + item.mon_hoc[j].pham_chat + '</td>');
                        tr_vnen.append('<td>' + item.mon_hoc[j].ktgk + '</td>');
                        tr_vnen.append('<td>' + item.mon_hoc[j].kthk + '</td>');
                        tr_vnen.append('<td>' + item.mon_hoc[j].ktck + '</td>');
                        tr_vnen.append('<td>' + item.mon_hoc[j].xlck + '</td>');
                        table_vnen.append(tr_vnen);
                    }
                }
            }

            var table3 = '';
            var table4 = '';
            if (item.hoc_ky == 1) {
                if (typeof (item.ne_nep) != 'undefined' && (item.ne_nep).length > 0) {
                    table3 += (self.processPointNeNep(item.ne_nep, 'I'));
                }
                if (typeof (item.diem_danh) != 'undefined') {
                    table3 += (self.processPointDiemDanh(item.diem_danh, 'I'));
                }

            }
            if (item.hoc_ky == 2) {
                if (typeof (item.ne_nep) != 'undefined' && (item.ne_nep).length > 0) {
                    table4 += (self.processPointNeNep(item.ne_nep, 'II'));
                }
                if (typeof (item.diem_danh) != 'undefined') {
                    table4 += (self.processPointDiemDanh(item.diem_danh, 'II'));
                }
            }

            if (item.lop_type == 3) {
                var tongket = $('<table border="0" celspacing="0" celpadding="4" style="border-top: 1px solid #CCC;"></table>');
                tongket.append($('<tr><td width="100">Khen thưởng</td><td>' + item.tong_ket.khen_thuong + '</td></tr>'));
                tongket.append($('<tr><td>Năng lực</td><td>' + item.tong_ket.nang_luc + '</td></tr>'));
                tongket.append($('<tr><td>Phẩm chất</td><td>' + item.tong_ket.pham_chat + '</td></tr>'));
            } else {
                var tongket = $('<table border="0" celspacing="0" celpadding="4" style="border-top: 1px solid #CCC;"></table>');
                if(!item.isTT22){
                    tongket.append($('<tr><td>Điểm TB</td><td>' + item.tong_ket.diem_tk + '</td></tr>'));
                    tongket.append($('<tr><td>Xếp hạng</td><td>' + item.hang + '</td></tr>'));
                    tongket.append($('<tr><td width="100">Danh hiệu</td><td>' + item.tong_ket.danh_hieu + '</td></tr>'));
                    tongket.append($('<tr><td>Hạnh kiểm</td><td>' + item.tong_ket.hanh_kiem + '</td></tr>'));
                    tongket.append($('<tr><td>Học lực</td><td>' + item.tong_ket.hoc_luc + '</td></tr>'));
                }else {
                    if (item.hoc_ky == 0)  tongket.append($('<tr><td width="100">Danh hiệu</td><td>' + item.tong_ket.danh_hieu + '</td></tr>'));
                    tongket.append($('<tr><td>Kết quả rèn luyện</td><td>' + item.tong_ket.hanh_kiem + '</td></tr>'));
                    tongket.append($('<tr><td>Kết quả học tập</td><td>' + item.tong_ket.hoc_luc + '</td></tr>'));
                }

                if (item.hoc_ky == 0) {
                    tongket.append($('<tr><td>Kết quả</td><td>' + item.tong_ket.len_lop + '</td></tr>'));
                }
            }



            $('#bangdiem').append('<div style="margin: 10px 0;">Kết quả học tập của <b>' + getURLParams('fullname') + '</b> trong học kỳ ' + hoc_ky_display + '</div>');
            $('#bangdiem').append(tongket);
            $('#bangdiem').append('<div>Bảng điểm các môn học kỳ ' + hoc_ky_display + '</div>');

            $('#bangdiem').append(table);

            if (item.hoc_ky == 1) {
                $('#bangdiem').append(table_vnen);
                $('#bangdiem').append(table3);

            }
            if (item.hoc_ky == 2) {
                $('#bangdiem').append(table_vnen);
                $('#bangdiem').append(table4);

            }
        }
    }

    this.processPoint = function (monhoc, hoc_ky) {
        if (this.isUndefined(monhoc.ten_mon_hoc)) {
            return "<tr><td colspan='5'></td></tr>";
        }

        var tr = $('<tr></tr>');
        tr.append('<td class="monhoc" width="120">' + monhoc.ten_mon_hoc + '</td>');
        if (hoc_ky != 0) {
            if($('#schoolYear').val() < 2020) {
                tr.append(this.processTypePoint(monhoc.M))
                tr.append(this.processTypePoint(monhoc.P))
                tr.append(this.processTypePoint(monhoc.V))
                tr.append(this.processTypePoint(monhoc.HK))
            }else {
                tr.append(this.processTypePoint(monhoc.TX))
                tr.append(this.processTypePoint(monhoc.GK))
                tr.append(this.processTypePoint(monhoc.CK))
            }
        }
        tr.append(this.processTypePoint(monhoc.TK))
        return tr;
    }

    this.processPointCap1 = function (monhoc, isDiem, hoc_ky) {
        if (this.isUndefined(monhoc.ten_mon_hoc)) {
            return "<tr><td colspan='5'></td></tr>";
        }

        var tr = $('<tr></tr>');
        tr.append('<td class="monhoc">' + monhoc.ten_mon_hoc + '</td>');
        if (isDiem) {
            if (hoc_ky == 1) {
                tr.append(this.processTypePointCap1(monhoc.t1))
                tr.append(this.processTypePointCap1(monhoc.t2))
                tr.append(this.processTypePointCap1(monhoc.t3))
                tr.append(this.processTypePointCap1(monhoc.t4))
                tr.append(this.processTypePointCap1(monhoc.t5))
                tr.append(this.processTypePointCap1(monhoc.gk1, 'GK1'))
                tr.append(this.processTypePointCap1(monhoc.ck1, 'CK1'))
            } else {
                tr.append(this.processTypePointCap1(monhoc.t6))
                tr.append(this.processTypePointCap1(monhoc.t7))
                tr.append(this.processTypePointCap1(monhoc.t8))
                tr.append(this.processTypePointCap1(monhoc.t9))
                tr.append(this.processTypePointCap1(monhoc.t10))
                tr.append(this.processTypePointCap1(monhoc.gk2, 'GK2'))
                tr.append(this.processTypePointCap1(monhoc.ck2, 'CK2'))
            }

        } else {
            tr.append(this.processTypePoint(monhoc.nx_t1))
            tr.append(this.processTypePoint(monhoc.nx_t2))
            tr.append(this.processTypePoint(monhoc.nx_t3))
            tr.append(this.processTypePoint(monhoc.nx_t4))
            tr.append(this.processTypePoint(monhoc.nx_t5))
            tr.append("<td></td>")
            tr.append("<td></td>")
        }
        tr.append('<td>' + monhoc.hoc_luc + '</td>');
        return tr;
    }

    this.processNhanXetCap1VNENThanhHoa = function (list, hoc_ky) {
        var tr = $('<tr></tr>');
        tr.append('<td width="80" style="">' + list.ten_mon + '</td>')
        var td = $('<td colspan="5" style="padding: 0;"></td>');
        if (hoc_ky == '1') {
            td.append(self.processPointNhanXetCap1VNENThanhHoa(list.t1, 'Tháng 1'));
            td.append(self.processPointNhanXetCap1VNENThanhHoa(list.t2, 'Tháng 2'));
            td.append(self.processPointNhanXetCap1VNENThanhHoa(list.t3, 'Tháng 3'));
            td.append(self.processPointNhanXetCap1VNENThanhHoa(list.t4, 'Tháng 4'));
            td.append(self.processPointNhanXetCap1VNENThanhHoa(list.t5, 'Tháng 5'));
            tr.append(td);
        } else {
            td.append(self.processPointNhanXetCap1VNENThanhHoa(list.t6, 'Tháng 6'));
            td.append(self.processPointNhanXetCap1VNENThanhHoa(list.t7, 'Tháng 7'));
            td.append(self.processPointNhanXetCap1VNENThanhHoa(list.t8, 'Tháng 8'));
            td.append(self.processPointNhanXetCap1VNENThanhHoa(list.t9, 'Tháng 9'));
            td.append(self.processPointNhanXetCap1VNENThanhHoa(list.t10, 'Tháng 10'));
            tr.append(td);
        }

        return tr;
    }

    this.processPointNhanXetCap1VNENThanhHoa = function (items, thang) {
        if (items.content.length > 0) {
            var table1 = $('<table border="0" celspacing="0" celpadding="0" style="border: none;margin: 0;"></table>');
            var tr1 = $('<tr></tr>');
            tr1.append('<td  width="50" style="border-left: 0;padding: 0; border-bottom: 1px solid #ccc;">' + thang + '</td>');
            var table2 = $('<table border="0" celspacing="0" celpadding="0"  style="border: none;margin: 0;"></table>');
            for (var i = 0; i < items.content.length; i++) {
                item = items.content[i];
                var tr2 = $('<tr></tr>');
                tr2.append('<td width="20">' + item.dot_id + '</td>');
                tr2.append('<td  width="100">' + item.nang_luc + '</td>');
                tr2.append('<td  width="100">' + item.phamchat + '</td>');
                tr2.append('<td style="">' + item.nhan_xet + '</td>');
                table2.append(tr2);
            }
            var td1 = $('<td style="border-left: 0;padding: 0; border-bottom: 1px solid #ccc;"></td>');
            td1.append(table2);
            tr1.append(td1);
            table1.append(tr1);
            return table1;
        } else {
            return "";
        }
    }

    this.processPointCap1VNEN = function (ck, label) {
        var table = $('<table border="0" celspacing="0" celpadding="4"></table>');
        var tr = $('<tr class="tophead"  style="border-bottom: 1px solid #96C4DE;"><th colspan="6">Kết quả kỳ ' + label + '</th></tr>');
        table.append(tr);
        var tr = $('<tr class="tophead"></tr>');
        tr.append('<th>STT</th>');
        tr.append('<th>Tên môn</th>');
        tr.append('<th>Điểm kiểm tra giữa kỳ</th>');
        tr.append('<th>Xếp loại giữa kỳ</th>');
        tr.append('<th>Điểm kiểm tra cuối kỳ</th>');
        tr.append('<th>Xếp loại cuối kỳ</th>');
        table.append(tr);

        if (this.isUndefined(ck)) {
            table.append("<tr><td colspan='4'></td></tr>");
            return table;
        }

        if (ck.qk_cac_mon.length > 0) {
            for (var i = 0; i < ck.qk_cac_mon.length; i++) {
                var tr = $('<tr></tr>');
                var item = ck.qk_cac_mon[i];
                if (this.isUndefined(item.ktdk_gk))
                    item.ktdk_gk = "";
                if (this.isUndefined(item.xlck_gk))
                    item.xlck_gk = "";
                tr.append('<td>' + (i + 1) + '</td>');
                tr.append('<td>' + item.ten + '</td>');
                tr.append('<td>' + item.ktdk_gk + '</td>');
                tr.append('<td>' + item.xlck_gk + '</td>');
                tr.append('<td>' + item.ktdk + '</td>');
                tr.append('<td>' + item.xlck + '</td>');
                table.append(tr);
            }
        } else {
            table.append("<tr><td colspan='4'></td></tr>");
        }
		
		if(this.isUndefined(ck.kqht)) return table;
			
			if(!this.isUndefined(ck.kqht.nang_luc)){
				var tr = $('<tr class="tophead"></tr>');
				tr.append('<th colspan="2" style="text-align: center">Các năng lực</th>');
				tr.append('<th colspan="4" style="text-align: center">Mức đạt được</th>');
				table.append(tr);
				for (var i = 0; i < ck.kqht.nang_luc.length; i++) {
					var tr = $('<tr></tr>');
					tr.append('<td colspan="2">' + ck.kqht.nang_luc[i].name + '</td>');
					tr.append('<td colspan="4"  style="text-align: center">' + ck.kqht.nang_luc[i].value + '</td>');
					table.append(tr)
				}
			}
			
			if(!this.isUndefined(ck.kqht.pham_chat)){
				var tr = $('<tr class="tophead"></tr>');
				tr.append('<th colspan="2" style="text-align: center">Các phẩm chất</th>');
				tr.append('<th colspan="4" style="text-align: center">Mức đạt được</th>');
				table.append(tr);
				for (var i = 0; i < ck.kqht.pham_chat.length; i++) {
					var tr = $('<tr></tr>');
					tr.append('<td colspan="2">' + ck.kqht.pham_chat[i].name + '</td>');
					tr.append('<td colspan="4"  style="text-align: center">' + ck.kqht.pham_chat[i].value + '</td>');
					table.append(tr)
				}
			}

			
			if(!this.isUndefined(ck.kqht.khen_thuong)){
				var tr = $('<tr class="tophead"></tr>');
				tr.append('<th colspan="2" style="text-align: center">STT</th>');
				tr.append('<th colspan="4"  style="text-align: center">Khen thưởng</th>');
				table.append(tr);
				for (var i = 0; i < ck.kqht.khen_thuong.length; i++) {
					var tr = $('<tr></tr>');
					tr.append('<td colspan="2" style="text-align: center">' + (i + 1) + '</td>');
					tr.append('<td colspan="4">' + ck.kqht.khen_thuong[i].noi_dung + '</td>');
					table.append(tr)
				}
			}
		
        return table.append(tr);
    }

    this.processPointNhanXetCap1VNEN = function (list, label) {
        if (this.isUndefined(list)) {
            return "<tr><td colspan='5'></td></tr>";
        }

        var tr = $('<tr></tr>');
        tr.append('<td class="monhoc" width="50"  style="border-bottom: 1px solid #ccc;">' + label + '</td>');
        var table = $('<table style="width: 100%;border-bottom:1px solid #ccc;margin: 0;" border="0" celspacing="0" celpadding="4"></table>');
        if (list.length > 0) {
            for (var i = 0; i < list.length; i++) {
                var item = list[i];

                var tr_con = $('<tr></tr>')
                tr_con.append('<td   width="100" style="border-bottom: 1px solid #ccc;  border-left: 0;">' + item.ten + '</td>');
                tr_con.append('<td  width="80" style="border-bottom: 1px solid #ccc;">' + item.nang_luc + '</td>');
                tr_con.append('<td  width="80" style="border-bottom: 1px solid #ccc;">' + item.pham_chat + '</td>');
                tr_con.append('<td style="border-bottom: 1px solid #ccc;">' + item.nhan_xet + '</td>');
                table.append(tr_con);
            }
        }
        tr.append("<td colspan='4' class='list' style='padding:0;'></td>");
        tr.find('.list').append(table);

        return tr;
    }

    this.processPointNeNep = function (list, label) {
        var tableNN = '<table border="0" celspacing="0" celpadding="6">';
        tableNN += '<tr class="tophead" style="border-bottom: 1px solid #96C4DE;"><th colspan="3">Ná» náº¿p ká»³ ' + label + '</th></tr>' +
                '<tr class="tophead" style="border-bottom: 1px solid #96C4DE;"><th width="120">Ngày vi phạm</th>' +
                '<th width="100" colspan="2">Nội dung vi phạm</th></tr>';

        if (list.length > 0) {
            for (var i = 0; i < list.length; i++) {
                var item = list[i];
                tableNN +=
                        '<tr><td style="border-bottom: 1px solid #ccc;  border-left: 0;">' + item.ngay_vp + '</td>' +
                        '<td colspan=2 style="border-bottom: 1px solid #ccc;">' + item.ten + '</td></tr>';
            }
        }
        tableNN += '</table>';
        return tableNN;
    }

    this.processPointDiemDanh = function (list, label) {
        var tableDD = '<table border="0" celspacing="0" celpadding="6">';

        tableDD += '<tr class="tophead" style="border-bottom: 1px solid #96C4DE;"><th colspan="3">Điểm danh kỳ ' + label + '</th></tr>' +
                '<tr class="tophead" style="border-bottom: 1px solid #96C4DE;"><th width="100">Có phép</th>' +
                '<th width="100">Không phép</th>' +
                '<th width="100">Bỏ tiết</th></tr>';

        tableDD += '<tr><td style="border-bottom: 1px solid #ccc;  border-left: 0;text-align: center;">' + list[0] + '</td>' +
                '<td style="border-bottom: 1px solid #ccc;text-align: center;">' + list[1] + '</td>' +
                '<td  style="border-bottom: 1px solid #ccc;text-align: center;">' + list[2] + '</td></tr>';
        tableDD += '</table>';
        return tableDD;
    }

    this.processTypePoint = function (typePoint) {
        if (this.isUndefined(typePoint)) {
            return '<td style="text-align: left;"></td>';
        }
        var td = $('<td style="text-align: left;"></td>');
        for (var i = 0; i < typePoint.length; i++) {
            if (this.isUndefined(typePoint[i]) || this.isUndefined(typePoint[i].diem)) {
                return td;
            }
            var style = "display: inline-grid;width: 20px; text-align: center;";
            if (i > 0) {
                style += "padding-left: 3px; border-left: 1px solid #ddd; margin-left: 3px;";
            }
            if (typePoint[i].diem == 'Đ') {
                td.append('<span style="' + style + '">Đ</td>');
            } else {
                td.append('<span style="' + style + '">' + typePoint[i].diem + '</span>');
            }
        }

        return td;
    }
    this.processTypePointCap1 = function (typePoint, isKy) {
        if (this.isUndefined(typePoint)) {
            return "<td></td>";
        }
        var td = $('<td style="text-align: left;"></td>');
        td.append('<table border="0" celspacing="0" celpadding="0" style="border-top: 1px solid #CCC; margin-bottom: 0"></table>');
        for (var i = 0; i < typePoint.length; i++) {
            if (this.isUndefined(typePoint[i]) || this.isUndefined(typePoint[i].diem)) {
                return td;
            }

            if (!this.isUndefined(isKy)) {
                if (typePoint.length > 1) {
                    var td_ky = $('<tr></tr>');
                    var loai = "";
                    switch (typePoint[i].stt) {
                        case '1':
                            loai = 'Đọc';
                            break;
                        case '2':
                            loai = 'Viết';
                            break;
                        case '3':
                            loai = isKy;
                            break;
                    }
                    td_ky.append('<td class="loai">' + loai + '</td>');
                    td_ky.append('<td class="diem"></td>');
                    td_ky.find('td.diem').append(typePoint[i].diem);
                    td.find('table').append(td_ky);
                } else {
                    return td.append(typePoint[i].diem)
                }

            } else {
                if (typePoint[i].diem == 'Đ') {
                    td.append('<span style="width: 15px; display: inline-block; font-family:Wingdings; font-size:12pt; color:#464545; ">Đ</td>');
                } else {
                    td.append('<span style="width: 15px; display: inline-block;">' + typePoint[i].diem + '</span>');
                }
            }
        }
        return td;
    }
}

$.getScript("https://connect.facebook.net/vi_VN/all.js", function (data, textStatus, jqxhr) {
    if (textStatus == 'success' && $('.fb-like-box').is('div')) {
        window.fbAsyncInit = function () {
            FB.init({
                channelUrl: location.href,
                status: true,
                xfbml: true
            });
        };
    }
});
