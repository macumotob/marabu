function send_prn() {
    
    let photo = document.getElementById("image-file").files[0] // get file from input
    if (!photo) {
        id("result").innerHTML = "<h3> select prn file ...</h3>"
        return;
    }
    let fileName = photo.name;
    id("msg").value = fileName;
    id("result").innerHTML = "<h3> wait ...</h3>"
    //formData.append("photo", photo);
    

    let xhr = new XMLHttpRequest();
    xhr.open("POST", 'print?file=' + fileName);
    xhr.onreadystatechange = function (e) {
        if (this.readyState == 4 && this.status == 200) {
            var text = this.responseText;
            var s = JSON.parse(text);
            id("result").innerHTML = "<img src='" + s.msg + "'/>"
            document.getElementById("image-file").value = "";
        }
    };
    xhr.send(photo);//formData);
}
