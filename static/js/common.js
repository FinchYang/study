var common_url = "http://cgs.ytjj.gov.cn:5000/";
//var zhongyang_url = "http://192.168.10.94:5001/";
var zhongyang_url = "http://cgs.ytjj.gov.cn/";
//var common_url = "http://192.168.10.94:5000/";
//获取url中的参数
function GetQueryString(name){
     var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");
     var r = window.location.search.substr(1).match(reg);
     if(r!=null)return  unescape(r[2]); return null;
}

//错误信息
function returnErrorMsg(code) {
	if(code== 200002) {
		return "请求错误，请检查请求参数！";
	} else if(code == 200003) {
		return "程序错误";
	} else if(code == 200001) {
		return "无效的 token";
	} else if(code == 200004) {
		return "无效的 身份证";
	} else {
		return "未知错误";
	}

}

//返回上一页
$(".back").on("click",function(){
	window.history.go(-1);
})

/**
 * 是否微信内置浏览器打开
 * 
 * @returns {Boolean}
 */
function isWxBrowser() {
	// var ua = navigator.userAgent.toLowerCase();
	// if (ua.match(/MicroMessenger/i) == 'micromessenger') {
	// 	return true;
	// } else {
	// 	return false;
	// }
	return (typeof WeixinJSBridge !== "undefined");
}

function goBackOrClose() {
	if (document.referrer == "") {
		if (isWxBrowser()) {
			WeixinJSBridge.call('closeWindow');
		} else {
			window.close();
		}
	} else {
		window.history.go(-1);
	}
}

