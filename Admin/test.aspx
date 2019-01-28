<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Admin_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <!-- 两个js脚本文件-->
    <script src="../easyui/jquery.min.js"></script>
    <script src="../easyui/jquery.easyui.min.js"></script>
    <script src="../easyui/locale/easyui-lang-zh_CN.js"></script>


    <link href="../easyui/themes/default/easyui.css" rel="stylesheet" />
    <link href="../easyui/themes/icon.css" rel="stylesheet" />
      <script>
          var hh = "";
          var datas = "";
          function test() {
             
              $.ajax({
                  type: "post",
                  url: "test.aspx?action=jishi",
                  success: function (data) {
                      if (hh == "") {
                          hh = data;
                          $("input").val(hh);
                      } else {
                          if (data > hh) {
                              
                              hh = data;
                              $.messager.show({
                                  title: '消息',
                                  msg: '有新的东西进来了',
                              });
                              $("input").val(hh);
                              play_click('../mp3/news.mp3');
                              //alert("有新的东西进来了");
                          } else if (data < hh) {
                              $.messager.show({
                                  title: '消息',
                                  msg: '有的被删除了',
                              });
                              hh = data;
                              $("input").val(hh);
                          } else {
                              hh = data;
                              $("input").val(hh);
                          }
                      }
                      //if (data >hh) {
                      //    alert(data);
                      //}
                  },
                  error: function () {
                      //asyncRequest(); //服务器抛出错误后继续发送一个请求
                  }
              });
          }
          function play_click(url) {
              var div = document.getElementById('div1');
              div.innerHTML = '<embed src="' + url + '" loop="0" autostart="true" hidden="true"></embed>';
          }

          setInterval("test()", 2000); //每2秒钟执行一次test()
          $(function () {
              var x = 22;
              var y = 20;
              $("a.smallimage").hover(function (e) {  //绑定一个鼠标悬停时事件 
                  //新建一个p标签来存放大图片，this.rel就是获取到a标签的大图片的路径，然后追加到body中 
                  $("body").append('<p id="bigimage"><img src="' + this.rel + '" alt=""  style="z-index:999" /></p>');
                  //改变小图片的透明度为0.5，结合上面的CSS，看起来就象是图片变暗了 
                  $(this).find('img').stop().fadeTo('slow', 0.5);
                  //将鼠标的坐标和声明的x，y做运算并赋值给大图片绝对定位的坐标，然后以fadeIn的效果显示 
                  $("#bigimage").css({ top: (e.pageY - y) + 'px', left: (e.pageX + x) + 'px' }).fadeIn('fast');
              }, function () { //鼠标离开后  
                  //将变暗的图片复原 
                  $(this).find('img').stop().fadeTo('slow', 1);
                  //移除新增的p标签 
                  $("#bigimage").remove();
              });
              $("a.smallimage").mousemove(function (e) {  //绑定一个鼠标移动的事件 
                  //将鼠标的坐标和声明的x，y做运算并赋值给大图片绝对定位的坐标，这样大图片就能跟随图片而移动了 
                  $("#bigimage").css({ top: (e.pageY - y) + 'px', left: (e.pageX + x) + 'px' });
              });
          });
</script>
</head>
<body>
    <%-- 以下这个div只是用来播放音乐用的 --%>
  <div id="div1"></div>
    <input type="text" name="count"  />
    <div><%Response.Write(DateTime.Now.ToShortTimeString()); %></div>
    
    <a class="smallimage" rel='../Imges/1.jpg'>
                                                        <img alt="" src='../Imges/1.jpg' height="30" width="25"></a>
  
</body>
</html>
