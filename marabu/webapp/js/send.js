function send_prn() {
    
    let photo = document.getElementById("image-file").files[0] // get file from input
    let fileName = photo.name;
    let formData = new FormData();
    formData.append("photo", photo);
    // formData.append("user", JSON.stringify(user));   // you can add also some json data to formData like e.g. user = {name:'john', age:34}

    let xhr = new XMLHttpRequest();
    xhr.open("POST", 'print?file=' + fileName);
    xhr.onreadystatechange = function (e) {
        if (this.readyState == 4 && this.status == 200) {
            var text = this.responseText;
            var s = JSON.parse(text);
            id("result").innerHTML = "<img src='" + s.msg + "'/>"
        }
    };
    xhr.send(photo);//formData);
}
