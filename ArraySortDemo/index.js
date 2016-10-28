var arr = [
            { type: "A", num: 0 },
            { type: "A", num: 1 },
            { type: "A", num: 2 },
            { type: "A", num: 3 },
            { type: "B", num: 0 },
            { type: "B", num: 1 },
            { type: "B", num: 2 },
            { type: "B", num: 3 }
];
printArr(arr);
//例：A0、A1、A2、A3、B0、B1、B2、B3处理后排序结果应为A0、A3、A2、A1、B0、B3、B2、B1
arr.sort(function (item1, item2) {
    if (item1.type > item2.type)
        return 1;
    else if (item1.type < item2.type)
        return -1;
    else {
        var num1 = parseInt(item1.num, 10);
        var num2 = parseInt(item2.num, 10);
        if (num1 === 0)
            return -1;
        else if (num2 === 0)
            return -1;
        else if (num1 > num2)
            return -1;
        else if (num1 < num2)
            return 1;
        else
            return 0;
    }
});
printArr(arr);
function printArr(arr) {
    var msg = "";

    arr.forEach(function(obj,index,arr) {
        msg += "type:" + obj.type + " num:" + obj.num + "\n";
    });

    console.info(msg);
}