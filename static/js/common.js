var common_url = "http://cgs.ytjj.gov.cn:5000/";
//var zhongyang_url = "http://192.168.10.94:5001/";
var zhongyang_url = "http://cgs.ytjj.gov.cn/";
//var common_url = "http://192.168.10.94:5000/";
//��ȡurl�еĲ���
function GetQueryString(name){
     var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");
     var r = window.location.search.substr(1).match(reg);
     if(r!=null)return  unescape(r[2]); return null;
}

//������Ϣ
function returnErrorMsg(code) {
	if(code== 200002) {
		return "��������������������";
	} else if(code == 200003) {
		return "�������";
	} else if(code == 200001) {
		return "��Ч�� token";
	} else if(code == 200004) {
		return "��Ч�� ���֤";
	} else {
		return "δ֪����";
	}

}

//������һҳ
$(".back").on("click",function(){
	window.history.go(-1);
})

/**
 * �Ƿ�΢�������������
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

