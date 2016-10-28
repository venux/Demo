$(function () {
    var imgSrc = "http://attach.cuuju.com/fm_file/match/20160419/2016041910384236930.jpg";
    $("#img").attr("src", imgSrc);

    var newImgSrc = SetSubToImage(imgSrc,3);
    $("#newimg").attr("src", newImgSrc).on("error", function () {
        $(this).attr("src", imgSrc);
    });

    function SetSubToImage(imgStr, subNum) {
        //1.值为undefined的或路径包括undefined即为错误情况，返回原值
        if (imgStr === undefined || imgStr.indexOf("undefined") !== -1)
            return imgStr;
        //2.不包括http的即为相对路径，返回原值
        if (imgStr.indexOf("http") === -1)
            return imgStr;
        //3.包括sub的即为缩略图，返回原值
        if (imgStr.indexOf("sub") !== -1)
            return imgStr;
        //4.subNum不在1-4范围内的，返回原值，并在console输出错误提示
        if (parseInt(subNum, 10) === undefined || parseInt(subNum, 10) < 0 || parseInt(subNum, 10) > 4) {
            console.log("图片缩略图编号应在1-4范围内，已使用原图！");
            return imgStr;
        }
        //5.图片找不到，返回原值
        //var img = new Image();
        //img.src = imgStr;
        //img.onload = function () {//加载成功
        var splitIndex = imgStr.lastIndexOf("/") + 1;
        var imgPath = imgStr.substring(0, splitIndex);
        var imgName = imgStr.substring(splitIndex);
        var newImgStr = "";
        newImgStr = imgPath + "sub" + subNum + imgName;

        //var newImg = new Image();
        //newImg.src = newImgStr;
        //newImg.onload = function () {//加载成功
        return newImgStr;
        //};
        //newImg.onerror = function () {//加载失败，返回原图片
        //    console.log("缩略图不存在，已使用原图！");
        //    return imgStr;
        //};
        //};
        //img.onerror = function () {//加载失败，返回原图片
        //    console.log("图片地址不存在！");
        //    return imgStr;
        //};
    }
});