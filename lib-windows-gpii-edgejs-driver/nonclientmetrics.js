var dotnet = require("./dotnetfunctions.js");

exports.test_nonclientmetrics_syncrounous = function(scrollWidth) {
    var inputWrapper = {};
    inputWrapper.scrollWidth = scrollWidth;
    
    var originalScrollWidth = dotnet.get_scroll_width({}, true);
    console.log("Scroll width from .net is: " + originalScrollWidth);

    console.log("Setting scroll width to " + scrollWidth);
    newScrollWidth = dotnet.set_scroll_width(inputWrapper, true);
    console.log("new scroll width is: " + newScrollWidth);

    console.log("restoring original scrollwidth of " + originalScrollWidth);
    inputWrapper.scrollWidth = originalScrollWidth;

    currentScrollWidth = dotnet.set_scroll_width(inputWrapper, true);
    console.log("scroll width restored to: " + originalScrollWidth);
};

exports.get_nonclientmetrics_syncrounous = function(){
    return dotnet.get_nonclientmetrics({}, true);
};