/**
 * 省市县帮助类
 */
var ProvinceCityCountyDataHelper = {
    GetProvinceData: function () {
        var arr = [];

        $("#provincelist").find("option").each(function () {
            arr.push({
                id: $(this).attr("value")
                , parentid: -1
                , name: $(this).html().trim()
            });
        });

        return arr;
    }
    , GetCityData: function () {
        var arr = [];

        $("#citylist").find("option").each(function () {
            arr.push({
                id: $(this).attr("value")
                , parentid: $(this).attr("optionid")
                , name: $(this).html().trim()
            });
        });

        return arr;
    }
    , GetCountyData: function () {
        var arr = [];

        $("#countylist").find("option").each(function () {
            arr.push({
                id: $(this).attr("value")
                  , parentid: $(this).attr("optionid")
                  , name: $(this).html().trim()
            });
        });

        return arr;
    }
}

$(function () {
    console.time("总耗时：");

    var ProvinceCityCountyCacheData = [];
    
    console.time("获取数据耗时：");

    if (ProvinceCityCountyCacheData.length === 0) {
        $.ajax({
            method: "GET",
            url: "ProvinceCityCountyData.json",
            async: false //同步
            ,
            dataType: "json",
            success: function (data) {
                ProvinceCityCountyCacheData = data;
            }
        });
    }

    console.timeEnd("获取数据耗时：");



    console.time("绑定DOM-省耗时：");

    var provinceHTML = '';
    ProvinceCityCountyCacheData.ProvinceData.forEach(function (element, index, array) {
        //<option value="1135">浙江省</option>
        provinceHTML += '<option value="' + element.id + '">' + element.name + '</option>';
    });
    $(".selectP").append(provinceHTML);
    $(".selectP").ui_select();
    
    console.timeEnd("绑定DOM-省耗时：");
 
    //console.time("绑定DOM-市耗时：");

    //var cityHTML = '';
    //ProvinceCityCountyCacheData.CityData.forEach(function (element, index, array) {
    //    //<option style="disoptionlay: none;" optionid="1004" value="1005">
    //    cityHTML += '<option style="disoptionlay: none;" optionid="' + element.parentid + '" value="' + element.id + '">' + element.name + '</option>';
    //});
    //$(".selectC").append(cityHTML);
    $(".selectC").ui_select();

    //console.timeEnd("绑定DOM-市耗时：");



    //console.time("绑定DOM-县耗时：");

    //var countyHTML = '';
    //ProvinceCityCountyCacheData.CountyData.forEach(function (element, index, array) {
    //    countyHTML += '<option style="disoptionlay: none;" optionid="' + element.parentid + '" value="' + element.id + '">' + element.name + '</option>';
    //});
    //$(".selectA").append(countyHTML);
    $(".selectA").ui_select();

    //console.timeEnd("绑定DOM-县耗时：");
    
    SelectDefaultArea();
    
    //切换省
    $("body").off("click", ".selectP ~ .ui-select-list li").on("click", ".selectP ~ .ui-select-list li", function () {
        var p_id = $(this).attr("data-value");
        var array = "125,144,2592,984";
        $(this).parents(".ui-select-wrap").find(".ui-select-input").attr("dataid", p_id);
        if (array.indexOf(p_id) > -1) {
            //四大直辖市隐藏市选择框
            $(this).parents(".form-group").find(".selectC").parents(".ui-select-wrap").hide();

            $(this).parents(".form-group").find(".selectA").parents(".ui-select-wrap").find(".ui-select-input").html("县（镇）").attr("dataid", "");
            $(this).parents(".form-group").find(".selectA").parents(".ui-select-wrap").find(".ui-select-list li").hide();
            $(this).parents(".form-group").find(".selectA").parents(".ui-select-wrap").find(".ui-select-list li[pid='" + p_id + "']").show();
        } else {
            $(this).parents(".form-group").find(".selectC").parents(".ui-select-wrap").show();

            $(this).parents(".form-group").find(".selectC").parents(".ui-select-wrap").find(".ui-select-input").html("市").attr("dataid", "");
            $(this).parents(".form-group").find(".selectC").parents(".ui-select-wrap").find(".ui-select-list li").hide();
            $(this).parents(".form-group").find(".selectC").parents(".ui-select-wrap").find(".ui-select-list li[pid='" + p_id + "']").show();

            $(this).parents(".form-group").find(".selectA").parents(".ui-select-wrap").find(".ui-select-list li").hide();
            $(this).parents(".form-group").find(".selectA").parents(".ui-select-wrap").find(".ui-select-input").html("县（镇）").attr("dataid", "");
        }

        var cityHTML = '';
        ProvinceCityCountyCacheData.CityData.forEach(function (element, index, array) {
            //<option style="disoptionlay: none;" optionid="1004" value="1005">
            cityHTML += '<option style="disoptionlay: none;" optionid="' + element.parentid + '" value="' + element.id + '">' + element.name + '</option>';
        });
        $(this).parents(".form-group").find(".selectC").append(cityHTML);
        $(this).parents(".form-group").find(".selectC").ui_select();
    });

    //切换市
    $("body").off("click", ".selectC ~ .ui-select-list li").on("click", ".selectC ~ .ui-select-list li", function () {
        var p_id = $(this).attr("data-value");
        $(this).parents(".ui-select-wrap").find(".ui-select-input").attr("dataid", p_id);
        $(this).parents(".form-group").find(".selectA").parents(".ui-select-wrap").find(".ui-select-list li").hide();
        $(this).parents(".form-group").find(".selectA").parents(".ui-select-wrap").find(".ui-select-list li[pid='" + p_id + "']").show();
        $(this).parents(".form-group").find(".selectA").parents(".ui-select-wrap").find(".ui-select-input").html("县（镇）").attr("dataid", "");

        var countyHTML = '';
        ProvinceCityCountyCacheData.CountyData.forEach(function (element, index, array) {
            countyHTML += '<option style="disoptionlay: none;" optionid="' + element.parentid + '" value="' + element.id + '">' + element.name + '</option>';
        });
        $(this).parents(".form-group").find(".selectA").append(countyHTML);
        $(this).parents(".form-group").find(".selectA").ui_select();
    });

    //切换县
    $("body").off("click", ".selectA ~ .ui-select-list li").on("click", ".selectA ~ .ui-select-list li", function () {
        var p_id = $(this).attr("data-value");
        $(this).parents(".ui-select-wrap").find(".ui-select-input").attr("dataid", p_id);

        //切换后清空场地
        if ($(this).parents(".panel").find(".cdSitname").length !== 0) {
            $(this).parents(".panel").find(".cdSitname").val("").attr({
                "wdval": "",
                "jdval": "",
                "fieldname": "",
                "fieldid": ""
            });
        }
    });

    console.timeEnd("总耗时：");
});

function SelectDefaultArea() {
    $(".selectP").parents(".ui-select-wrap").find(".ui-select-input").html("北京市").attr({ "dataid": "125", "title": "北京市" }); //省
    $(".selectC").parents(".ui-select-wrap").hide();
    $(".selectA").parents(".ui-select-wrap").find(".ui-select-input").html("海淀区").attr({ "dataid": "132", "title": "海淀区" }); //省
    //$(".selectP ~ .ui-select-list li[data-value='125']:last").trigger("click"); //.parent().hide();
    //$(".selectP").parent().removeClass("focus");

    ////$(".selectC ~ .ui-select-list li[data-value='125']").trigger("click").parent().hide();
    //$(".selectA ~ .ui-select-list li[data-value='132']:last").trigger("click"); //.parent().hide();
    //$(".selectA").parent().removeClass("focus");
}


function PrintArray(arr) {
    return "[" + arr.map(function (item) {
        return JSON.stringify(item);
    }).join("\n,") + "]";
}