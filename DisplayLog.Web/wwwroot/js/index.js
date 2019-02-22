$(function () {
    $('#nav ul li').click(function () {
        console.log('dsssss');
        $(this).addClass('active');
        $(this).siblings().removeClass('active');
    });
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:19222/pushLogHub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on('ConnectionSuccess',function(message){
        console.log(message);
    });

    connection.start();
})