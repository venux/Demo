(function () {
    //蹴鞠各个站点URL
    var CujuURLModel = function () {
        this.communityURL = '';//社区站点
        this.matchURL = '';//赛事子站点
        this.clubURL = '';//球队子站点
        this.organURL = '';//主办商子站点
        this.venueURL = '';//场地子站点
        this.judgeURL = '';//裁判子站点
        this.appURL = '';//APP子站点
        this.serviceURL = '';//服务子站点
        this.activeURL = '';//活动子站点
    };

    //本地开发
    var developPaths = new CujuURLModel();
    developPaths.communityURL = 'http://localhost:15069/';//社区站点
    developPaths.matchURL = 'http://localhost:20362/match_community/';//赛事子站点
    developPaths.clubURL = 'http://localhost:10424/club_community/';//球队子站点
    developPaths.organURL = 'http://localhost:65220/organ_community/';//主办商子站点
    developPaths.venueURL = '';//场地子站点
    developPaths.judgeURL = 'http://localhost:16375/judge_community/';//裁判子站点（暂无）
    developPaths.appURL = '';//APP子站点
    developPaths.serviceURL = '';//服务子站点（暂无）
    developPaths.activeURL = 'http://localhost:16382/active_community/cuju/';//活动子站点

    //内网测试
    var testPaths = new CujuURLModel();
    testPaths.communityURL = 'http://192.168.4.93:8004/';//社区站点
    testPaths.matchURL = 'http://192.168.4.93:8008/';//赛事子站点
    testPaths.clubURL = 'http://192.168.4.93:8012/';//球队子站点
    testPaths.organURL = 'http://192.168.4.93:8011/';//主办商子站点
    testPaths.venueURL = 'http://192.168.4.93:8006/';//场地子站点
    testPaths.judgeURL = '';//裁判子站点
    testPaths.appURL = '';//APP子站点
    testPaths.serviceURL = 'http://192.168.4.93:8019/';//服务子站点
    testPaths.activeURL = '';//活动子站点

    //外网
    var Paths = new CujuURLModel();
    Paths.communityURL = 'http://www.cuuju.com/';//社区站点
    Paths.matchURL = 'http://match.cuuju.com/';//赛事子站点
    Paths.clubURL = 'http://club.cuuju.com/';//球队子站点
    Paths.organURL = 'http://organ.cuuju.com/';//主办商子站点
    Paths.venueURL = 'http://venue.cuuju.com/';//场地子站点
    Paths.judgeURL = '';//裁判子站点
    Paths.appURL = 'http://app.cuuju.com/';//APP子站点
    Paths.serviceURL = '';//服务子站点
    Paths.activeURL = 'http://active.cuuju.com/cuju/atList.htm';//活动子站点

    ////由于本js文件加载执行时候，后面的js文件暂未加载，所以可获取最后一个js文件地址,即本js文件
    //var jsURL = window.document.scripts[window.document.scripts.length - 1].src;
    //var type = GetQueryString("type", jsURL);

    //根据js后面的type参数来获取URLModel
    window.CujuURLModel = Paths;//GetPathByType(type);

    /*
     * 获取URL对应的参数值
     * @params name string:参数名
     * @params url string:带查询的URL地址
     * @return string:参数值
     */
    function GetQueryString(name, url) {
        if (url === undefined)
            url = window.location.search;
        else
            url = url.split("?")[1];

        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = url.substr(1).match(reg);

        if (r !== null)
            return unescape(r[2]);

        return null;
    }

    /*
     * 通过传入的类型获取URL路径
     * @params type string:类型（develop、test、默认外网）
     * @return CujuURLModel:蹴鞠URLModel
     */
    function GetPathByType(type) {
        if (type === "develop")
            return developPaths;
        else if (type === "test")
            return testPaths;
        else
            return Paths;
    }
})();