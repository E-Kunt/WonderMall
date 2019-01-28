$(function () {
    function test2() {
        alert("我被执行拉");
    }
    setInterval("test2()", 3000); //每3秒钟执行一次test()
  
});
