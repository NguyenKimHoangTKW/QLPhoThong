(function(ad){function C(){aa||(aa=!0,J(Z,function(b){O(b)}))}function D(f,b){var a=ad.createElement("script");a.type="text/"+(f.type||"javascript"),a.src=f.src||f,a.async=!1,a.onreadystatechange=a.onload=function(){var c=a.readyState;!b.done&&(!c||/loaded|complete/.test(c))&&(b.done=!0,b())},(ad.body||ac).appendChild(a)}function E(d,c){if(d.state==P){return c&&c()}if(d.state==Q){return T.ready(d.name,c)}if(d.state==R){return d.onpreload.push(function(){E(d,c)})}d.state=Q,D(d.url,function(){d.state=P,c&&c(),J(X[d.name],function(b){O(b)}),H()&&aa&&J(X.ALL,function(b){O(b)})})}function F(d,c){d.state===undefined&&(d.state=R,d.onpreload=[],D({src:d.url,type:"cache"},function(){G(d)}))}function G(b){b.state=S,J(b.onpreload,function(c){c.call()})}function H(e){e=e||W;var d;for(var f in e){if(e.hasOwnProperty(f)&&e[f].state!=P){return !1}d=!0}return d}function I(b){return Object.prototype.toString.call(b)=="[object Function]"}function J(e,d){if(!!e){typeof e=="object"&&(e=[].slice.call(e));for(var f=0;f<e.length;f++){d.call(e,e[f],f)}}}function K(f){var e;if(typeof f=="object"){for(var h in f){f[h]&&(e={name:h,url:f[h]})}}else{e={name:M(f),url:f}}var g=W[e.name];if(g&&g.url===e.url){return g}W[e.name]=e;return e}function M(f){var e=f.split("/"),h=e[e.length-1],g=h.indexOf("?");return g!=-1?h.substring(0,g):h}function O(b){b._done||(b(),b._done=1)}var ac=ad.documentElement,ab,aa,Z=[],Y=[],X={},W={},V=ad.createElement("script").async===!0||"MozAppearance" in ad.documentElement.style||window.opera,U=window.head_conf&&head_conf.head||"head",T=window[U]=window[U]||function(){T.ready.apply(null,arguments)},S=1,R=2,Q=3,P=4;V?T.js=function(){var e=arguments,d=e[e.length-1],f={};I(d)||(d=null),J(e,function(b,a){b!=d&&(b=K(b),f[b.name]=b,E(b,d&&a==e.length-2?function(){H(f)&&O(d)}:null))});return T}:T.js=function(){var e=arguments,c=[].slice.call(e,1),f=c[0];if(!ab){Y.push(function(){T.js.apply(null,e)});return T}f?(J(c,function(b){I(b)||F(K(b))}),E(K(e[0]),I(f)?f:function(){T.js.apply(null,c)})):E(K(e[0]));return T},T.ready=function(a,g){if(a==ad){aa?O(g):Z.push(g);return T}I(a)&&(g=a,a="ALL");if(typeof a!="string"||!I(g)){return T}var e=W[a];if(e&&e.state==P||a=="ALL"&&H()&&aa){O(g);return T}var d=X[a];d?d.push(g):d=X[a]=[g];return T},T.ready(ad,function(){H()&&J(X.ALL,function(b){O(b)}),T.feature&&T.feature("domloaded",!0)});if(window.addEventListener){ad.addEventListener("DOMContentLoaded",C,!1),window.addEventListener("load",C,!1)}else{if(window.attachEvent){ad.attachEvent("onreadystatechange",function(){ad.readyState==="complete"&&C()});var N=1;try{N=window.frameElement}catch(L){}!N&&ac.doScroll&&function(){try{ac.doScroll("left"),C()}catch(b){setTimeout(arguments.callee,1);return}}(),window.attachEvent("onload",C)}}!ad.readyState&&ad.addEventListener&&(ad.readyState="loading",ad.addEventListener("DOMContentLoaded",handler=function(){ad.removeEventListener("DOMContentLoaded",handler,!1),ad.readyState="complete"},!1)),setTimeout(function(){ab=!0,J(Y,function(b){b()})},300)})(document);