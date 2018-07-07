
String.prototype.replaceAll = function (find, replace) {
	var str = this;
	return str.replace(new RegExp(find, 'g'), replace);
};
String.prototype.isEmpty = function () {
  return (this.length === 0 || !this.trim());
};
function mb_escape (val) {
	if (typeof(val)!="string") return val;
	return val
	.replace(/[\\]/g, '\\\\')
	.replace(/[\/]/g, '\\/')
	.replace(/[\b]/g, '\\b')
	.replace(/[\f]/g, '\\f')
	.replace(/[\n]/g, '\\n')
	.replace(/[\r]/g, '\\r')
	.replace(/[\t]/g, '\\t')
	//.replace(/[\"]/g, '\\"')
    .replace(/\\'/g, "\\'");
}
function id(name) {
  var item = document.getElementById(name);
  return item;
}
function load_async(file,onsuccess) {
  var xhr = new XMLHttpRequest();
  xhr.open("get", file, true);
  xhr.setRequestHeader("Access-Control-Allow-Origin", "*");

  xhr.onreadystatechange = function () {
    if (xhr.readyState == 4) {
      if (xhr.status >= 200 && xhr.status < 300 || xhr.status == 304) {
        var text = xhr.responseText;
      //  var heders = xhr.getResponseHeader("content-type");
        onsuccess(text);
      }
    }
  };
  xhr.send(null);
}

function load_async_(file, onsuccess) {
  var xhr = new XMLHttpRequest();
  xhr.open("get", file, true);
  xhr.setRequestHeader("Access-Control-Allow-Origin", "*");
 // xhr.setRequestHeader('User-Agent', 'Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36');
  xhr.setRequestHeader('Accept', '*/*');
  //xhr.setRequestHeader("Host", "xyz.com:8000");
  xhr.setRequestHeader("Accept", "*/*");
  xhr.setRequestHeader("Accept-Language", "en-US,en;q=0.5");
  //xhr.setRequestHeader("Accept-Encoding", "gzip, deflate");
  //xhr.setRequestHeader("DNT", "1");
  //xhr.setRequestHeader("Referer", "http://localhost:8000/test/");
  //xhr.setRequestHeader("Origin", "http://localhost:8000");
  //xhr.setRequestHeader("Connection", "keep-alive");
  xhr.setRequestHeader("Pragma", "no-cache");
  xhr.setRequestHeader("Cache-Control", "no-cache");
  //xhr.setRequestHeader("Content-Length", 0);

  xhr.onreadystatechange = function () {
    if (xhr.readyState == 4) {
      if (xhr.status >= 200 && xhr.status < 300 || xhr.status == 304) {
        var text = xhr.responseText;
        //  var heders = xhr.getResponseHeader("content-type");
        onsuccess(text);
      }
    }
  };
  xhr.send(null);
}





function load_sync(file) {
  var xhr = new XMLHttpRequest();
  xhr.open("get", file, false);
  var result;
  xhr.onreadystatechange = function () {
    if (xhr.readyState == 4) {
      if (xhr.status >= 200 && xhr.status < 300 || xhr.status == 304) {
        var text = xhr.responseText;
        result = text;
      }
    }
  };
  xhr.send(null);
  return result;
}

function load_async_json(file, onsuccess) {
  load_async(file, function (text) {
    try{
//	 alert(text);
	  var s = mb_escape(text);
	//  s = text.replaceAll('\r','\\r');
      var x = eval('(' + s + ')');
      onsuccess(x);
    }
    catch (err) {
      alert("Error eval json:" + err + "\n" + text);
    }
  });
}
function load_async_script(file) {
  load_async(file, function (text) {
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.text = text;
    document.body.appendChild(script);
  });
}
function load_script(url,callback) {

  var script = document.createElement("script")
  script.type = "text/javascript";
  if (script.readyState) { //IE
    script.onreadystatechange = function () {
      if (script.readyState == "loaded" || script.readyState == "complete") {
        script.onreadystatechange = null;
        callback();
      }
    };
  } else { //Others
    script.onload = function () {
      callback();
    };
  }
  script.src = url;
  document.getElementsByTagName("head")[0].appendChild(script);
}
function load_style(url, callback) {

  var style = document.createElement("link")
  style.type = "text/css";
  style.rel = 'stylesheet';

  if (style.readyState) { //IE
    style.onreadystatechange = function () {
      if (style.readyState == "loaded" || style.readyState == "complete") {
        style.onreadystatechange = null;
        callback();
      }
    };
  } else { //Others
    style.onload = function () {
      callback();
    };
  }
  style.href = url;
  document.getElementsByTagName("head")[0].appendChild(style);
}
function forE(arr, callback) {
  for (var i = 0; i < arr.length; i++) {
    callback(arr[i]);
  }
}

//--------------------------------------------------------------------------

