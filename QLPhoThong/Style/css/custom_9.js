/* ====================================================================
    Global Variables
==================================================================== */
var bodyV2 = $('body');
var leftColV2 = $('.leftCol');
var secondColV2 = $('.secondCol');
var searchOnOffV2 = $('#searchOnOff');
var toggleSearchV2 = $('#toggleSearch');
var searchInputV2 = $('.navbarTop form input');
var giaoVienHintV2 = $('.navItemLink .hint');
var giaoVienHintLiV2 = giaoVienHintV2.parents('li.navItem');

var urlFilterV2 = getURLParams('filter');
var startRecordV2 = 0;
var numberOfRecordV2 = 10;
var schoolParamsV2 = {
    _f: 31,
    filter: '',
    filterMethod: 'CONTAINS',
    limit: numberOfRecordV2,
    siteId: 337467,
    ownerId: 1398038,
    start: startRecordV2,
    type: 'file'
};

var searchTimeOutV2 = null;

/* ====================================================================
    Predefined Methods
==================================================================== */
function getURLParams(parameter) {
    var pageURL = decodeURIComponent(window.location.search.substring(1));
    var urlVariables = pageURL.split('&');

    for (var i = 0; i < urlVariables.length; i++) {
        var parameterName = urlVariables[i].split('=');
        // return empty string if there is no parameter in url
        if (parameterName.length < 2) {
            return "";
        }
        if (parameterName[0] == parameter) {
            return parameterName[1];
        }
    }
}

function showLoadingV2(container, type) {
    var loadingGif = '';
    
    switch (type) {
        case 'schoolList':
            loadingGif = '<div style="text-align: center;" class="showLoading">' +
                            '<img src="http://c2.cdn.truelife.vn/file/201310/2139158/ajax_loader.gif?f=9cTX6F6sk|qkZEBY4j77vCyaoS|Z00CZ|YCU0wP7KuQ="/>' +
                         '</div>';
            break;
        case 'searchInput':
            loadingGif = '<div style="position: absolute; top: 4px; right: 35px;" class="showLoading">' +
                            '<img src="http://c2.cdn.truelife.vn/layoutpores/201610/3158501/searchloading.gif?f=/mL5GTYbj4Mm3EbGPDxoQQ=="/>' +
                         '</div>';
            break;
    }

    if (type != false) {
        container.append(loadingGif);
    } else {
        container.find('.showLoading').remove();
    }
}

// Build result list for the search bar
function buildSearchResultList(result) {
    var resultHtml = '';

    // if has error
    if (result == 'error') {
        return '<div class="searchResults"><ul>' +
                    '<li><center><p>Có lỗi xảy ra. Xin hãy thử lại sau.</p></center></li>' +
                '</ul></div>';
    }
    // if found no results
    if (result.object.length == 0) {
        return '<div class="searchResults"><ul>' +
                    '<li><center><p>Không tìm thấy kết quả nào. Hãy thử cụm từ khác.</p></center></li>' +
                '</ul></div>';
    }

    $.each(result.object, function (key, value) {
        if(value.content) {
            var content = JSON.parse(value.content);

            if (content.folder) {
                // use default avatar if user dont use any
                if (!content.avatar) {
                    content.avatar = 'http://c2.cdn.truelife.vn/file/201310/2139162/avatar.png?f=VAvT4CuWOYacITVr8HKQOUTTUNG9wVVBdQ9ogCTvAQM=';
                }

                resultHtml +=   '<li>' +
                                    '<a href="' + content.url + '">' +
                                        '<div class="resultWrap">' +
                                            '<span class="imgWrap"><img src="' + content.avatar + '" alt=""></span>' +
                                            '<span class="resultTextWrap">' +
                                                '<p>' + content.name + '</p>' +
                                                '<p>' + content.folder.name + '</p>' +
                                            '</span>' +
                                        '</div>' +
                                    '</a>' +
                                '</li>';
            }
        }
    });

    return '<div class="searchResults"><ul>' + resultHtml + '</ul></div>';
}

// Build result list for the school page
function buildSchoolList(result) {
    var htmlResult = '';

    if(result == 'error') {
        return '<div class="col-xs-12"><h5>Đã xảy ra lỗi. Xin hãy thử lại sau.</h5></div>';
    }
    // if found no results
    if (result.object.length == 0) {
        return '<div class="col-xs-12"><center><h4>Không tìm thấy kết quả nào!</h4></center></div>';
    }

    $.each(result.object, function (key, value) {
        if(value.content) {
            var content = JSON.parse(value.content);
            var schoolAddress = '';

            if (content.folder) {
                schoolAddress = content.folder.name;
            }
            // use default avatar if user dont use any
            if (!content.avatar) {
                content.avatar = 'http://c2.cdn.truelife.vn/file/201310/2139162/avatar.png?f=VAvT4CuWOYacITVr8HKQOUTTUNG9wVVBdQ9ogCTvAQM=';
            }

            htmlResult +=   '<div class="col-xs-6">' +
                                '<li>' +
                                    '<div class="schoolItem">' +
                                        '<a href="' + content.url + '">' +
                                            '<span class="imgWrap"><img src="' + content.avatar + '"/></span>' +
                                            '<p class="schoolName">' + content.name + '</p>' +
                                            '<p class="schoolAddress">' + schoolAddress + '</p>' +
                                        '</a>' +
                                    '</div>' +
                                '</li>' +
                            '</div>';
        }
    });

    return htmlResult;
}

// Show list school on the school page
function showListSchool() {
    var schoolList = $('.schoolListContainer_ext1 #list-item-school');
    var data = $.extend({}, schoolParamsV2);
    data.start = startRecordV2 * numberOfRecordV2;
    data.filter = urlFilterV2;

    $.ajax({
        url: 'http://truelife.vn/offica/webtube/action',
        dataType: 'jsonp',
        data: data,
        beforeSend: function () {
            showLoadingV2(schoolList, 'schoolList');
        },
        success: function (result) {
            showLoadingV2(schoolList, false);
            $('.viewMore').remove(); // remove old viewMore
            schoolList.append(buildSchoolList(result));

            if (result.object.length == numberOfRecordV2 && schoolList.length > 0) {
                secondColV2.append('<div class="col-xs-12 viewMore"><center><span>Xem thêm</span></center></div>');
            }

            // show list
            if(!schoolList.parent().is(':visible')) {
                schoolList.parent().show();
            }
            
            // to get next set of results
            startRecordV2++;
        },
        error: function () {
            showLoadingV2(schoolList, false);
            schoolList.html(buildSchoolList('error'));
        }
    });
}

// Show list school on the dropdown list (search bar)
function showSearchResults(searchForm, inputFilter) {
    var data = $.extend({}, schoolParamsV2);
    data.filter = inputFilter;
    
    $.ajax({
        url: 'http://truelife.vn/offica/webtube/action',
        dataType: 'jsonp',
        data: data,
        beforeSend: function () {
            showLoadingV2(searchForm, 'searchInput');
        },
        success: function (result) {
            $('.searchResults').remove(); // remove old results
            showLoadingV2(searchForm, false);
            searchForm.append(buildSearchResultList(result)); // append new results
        },
        error: function () {
            showLoadingV2(searchForm, false);
            searchForm.append(buildSearchResultList('error'));
            // console.log(xhr.status + xhr.statusText);
        }
    });
}

function showGiaoVienHint(numberOfShow) {
    if(numberOfShow == false) {
        // localStorage.setItem('numberOfHintShowed', 'false');
        localStorage.setItem('hintShowedTime', 'false');
        return;
    }
    
    if(bodyV2.width() <= 650) {
        return;
    }

    if(localStorage.getItem('hintShowedTime') == 'false') {
        // localStorage.setItem('numberOfHintShowed', 'false');
        return;
    }

    // Show by times
    /*var numberOfHintShowed = Number(localStorage.getItem("numberOfHintShowed"));

    if(numberOfHintShowed == 0) {
        localStorage.setItem("numberOfHintShowed", 1);
    }
    // numberOfShow is the max number of hint showed
    if(numberOfHintShowed >= 0 && numberOfHintShowed <= numberOfShow - 1) {
        giaoVienHintV2.velocity('fadeIn', { duration: 800 })
                    .velocity('fadeOut', { delay: 4000, duration: 2000 });

        localStorage.setItem("numberOfHintShowed", numberOfHintShowed + 1);
    }*/
    // ===========================
    // Show by day
    var hintShowedTime = Number(localStorage.getItem("hintShowedTime"));

    if(hintShowedTime == 0) {
        localStorage.setItem("hintShowedTime", Date.now());
    }
    // numberOfShow is the max number of days that show hint
    if(hintShowedTime == 0 || Date.now() - hintShowedTime < numberOfShow * 24 * 60 * 60 * 1000) {
        giaoVienHintLiV2.css('background-color', '#dddfe2');
        setTimeout(function() {
            giaoVienHintLiV2.css('background-color','');
        }, 60000);

        giaoVienHintV2.velocity('fadeIn', { delay: 1000, duration: 800 })
                    .velocity('fadeOut', { delay: 56000, duration: 2000 });
    }
}

/* ====================================================================
    Main Body
==================================================================== */
function doWhenWidthChange() {
    if (searchOnOffV2.is(':visible')) {
        searchOnOffV2.hide();
        toggleSearchV2.attr('class', 'glyphicon glyphicon-search');
        $('.searchResults').remove();
        // $('.navbarTop form input').val('');
    }
    // reset leftCol to original position when orientation change
    if (bodyV2.width() > 650) {
        leftColV2.css({'left': '0px'});
    }
    if (bodyV2.width() <= 650) {
        if(!$('#form-search-solienlac input').is(':focus')) {
            leftColV2.css({'left': '-500px'});
        }
    }

    bodyV2.css({'position': 'static'});
}

// show or hide search 
toggleSearchV2.click(function () {
    if (toggleSearchV2.attr('class') == 'glyphicon glyphicon-search') {
        toggleSearchV2.attr('class', 'glyphicon glyphicon-remove');
        // searchOnOffV2.velocity("slideDown", { duration: 500 });
        searchOnOffV2.show();
    } else {
        toggleSearchV2.attr('class', 'glyphicon glyphicon-search');
        searchInputV2.val('');
        $('.searchResults').remove();
        // searchOnOffV2.velocity("slideUp", { duration: 500 });
        searchOnOffV2.hide();
    }
    // searchOnOffV2.stop().slideToggle(500);
});
// show menu
$('#openMenu').click(function () {
    bodyV2.css({'position': 'fixed'});

    leftColV2.velocity({
        'left': '-1px'
    }, 500);  
});
// close left menu when user click on the outside area
bodyV2.click(function (e) {
    var target = $(e.target);

    if(leftColV2.css('left') == '-1px' && target.parents('.leftCol').length == 0 && !target.hasClass('leftCol')) { 
        e.preventDefault();
        // reset body and .leftCol to original state
        bodyV2.css({'position': 'static'});

        leftColV2.velocity({
            'left': '-500px'
        }, 400);
    }
});

// remove result list appended to search bar when user click outside
$('body > *').not('.searchResults').click(function () {
    $('.searchResults').remove();
});

// show hint for giao vien on left menu
if (typeof(Storage) !== 'undefined') {
    showGiaoVienHint(7);
}
// use this to reset localStorage
$('h4.navHeader').click(function() {
    localStorage.clear();
});

giaoVienHintV2.click(function(e){
    e.preventDefault();
})
giaoVienHintV2.mouseenter(function(){
    if(giaoVienHintV2.css('opacity') > 0.7) {
        giaoVienHintV2.velocity('stop');
    } else {
        giaoVienHintV2.velocity('stop').velocity('fadeIn', { duration: 400 });
    }
});
giaoVienHintV2.mouseleave(function(){
    $(this).hide();
});
giaoVienHintV2.find('h5').click(function(){
    giaoVienHintV2.hide();
});
giaoVienHintV2.find('button').click(function(){
    showGiaoVienHint(false);
    giaoVienHintV2.hide();
});

// click search button
$('.navbarTop form button').click(function (e) {
    var inputFilter = $(this).siblings('input').val();

    if(inputFilter.length < 3) return;

    window.location.href = 'http://vnedu.vn/v2-truong-hoc?filter=' + inputFilter;
    // window.location.href = 'file:///E:/Dropbox/Dropbox/Study/Programming/Arsenal/Projects/Working/vnEdu/VnEdu%20UI%202.0/truong_hoc.html?filter=' + inputFilter;
});

// search typing
searchInputV2.keyup(function (e) {
    var key = e.which;
    var searchForm = $(this).parent();
    var inputFilter = $(this).val(); //.replace(/\s/g, '+')
    // console.log(key); // use this to see keycode

    // when user press esc key, or input length < 3, close the result dropdown list
    if (key == 27 || inputFilter.length < 3) {
        $('.searchResults').remove();
        clearTimeout(searchTimeOutV2);
        return;
    }
    // go to result page when user press enter
    if (key == 13) {
        clearTimeout(searchTimeOutV2);
        window.location.href = 'http://vnedu.vn/v2-truong-hoc?filter=' + inputFilter;
        // window.location.href = 'file:///E:/Dropbox/Dropbox/Study/Programming/Arsenal/Projects/Working/vnEdu/VnEdu%20UI%202.0/truong_hoc.html?filter=' + inputFilter;
    }

    // clear previous ajax request
    clearTimeout(searchTimeOutV2);
    // set a new ajax request
    searchTimeOutV2 = setTimeout(function() {
        showSearchResults(searchForm, inputFilter);
    }, 900); // sends request 900ms after typing stops.
});

// show search result if there is filter parameter in url
if(urlFilterV2) {
    // keep keyword on search input after submitting
    searchInputV2.val(urlFilterV2);

    showListSchool(schoolParamsV2);
}
// show more results when user click on "Xem thêm"
secondColV2.on('click', '.viewMore', function () {
    showListSchool(schoolParamsV2);
});

/* ====================================================================
    Hot Fixes
==================================================================== */
// prevent submit form when user press enter or click on search button
$('.navbarTop form').submit(function (e) {
    e.preventDefault();
});

// z-index fixing
$('#notLoginedMenu a').click(function(e) {
    e.preventDefault();
    $('#post-holder').css('z-index', '1030');
});
$('body').on('click', '#post-holder', function() {
    $('#post-holder').css('z-index', '1111');
});
$('#post-holder').css('z-index', '1030');
// $('#bdoverlay').click(function(){
//     $('#post-holder').css('z-index', '1030');
// });
// show login modal when user hover Đăng nhập
$('#notLoginedMenu').hover(function(){
    $('#myModal').modal({
        show: true
    });

    $('#post-holder').css('z-index', '1030');
});